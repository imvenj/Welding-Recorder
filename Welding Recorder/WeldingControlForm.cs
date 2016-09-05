using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.Text.RegularExpressions;

namespace Welding_Recorder
{
    public partial class WeldingControlForm : Form
    {
        public History History;
        private List<byte> signalBuffer = new List<byte>(6);
        private DateTime timestamp = DateTime.Now;
        private bool isControlling = false;
        private PlotView Plot = new PlotView();
        private LinearAxis axis1 = new LinearAxis();
        private Signal currentSentSignal;
        private List<Timer> timerCache = new List<Timer>();
        private Timer counterdownTimer = null;

        private SerialPort CurrentSerialPort { get; set; }
        private SerialDataReceivedEventHandler dataReceivedEventHandler;
        public bool AutoControl { get; set; }
        private bool hardStop = false; // Disconnect with signal.
#if DEBUG
        private int currentStep = 0;
        private Signal[] debugSignals = {
                new Signal(SignalType.ArcStart),
                new Signal(SignalType.ArcEnd),
                new Signal(SignalType.SolderStart),
                new Signal(SignalType.SolderEnd),
                new Signal(SignalType.Acceleration),
                new Signal(SignalType.Deceleration),
                new Signal(SignalType.RotateStart),
                new Signal(SignalType.RotateEnd),
                new Signal(SignalType.RevolveStart),
                new Signal(SignalType.AutoControlStart),
                new Signal(SignalType.AutoControlEnd),
                new Signal(SignalType.CollectStart),
                new Signal(SignalType.CollectEnd),
                new Signal(SignalType.ChooseTemplate),
            };
#endif

        public WeldingControlForm(SerialPort serialPort)
        {
            InitializeComponent();
            CurrentSerialPort = serialPort;
            dataReceivedEventHandler = new SerialDataReceivedEventHandler(serialPortDataReceived);
            CurrentSerialPort.DataReceived += dataReceivedEventHandler;
            History = History.LatestHistory();
        }

        public WeldingControlForm(SerialPort serialPort, History history) : this(serialPort)
        {
            History = history;
        }
        
        private void WeldingControlForm_Load(object sender, EventArgs e)
        {
            InitialWeldingControlUI();
            InitializePlot();
            
            if (AutoControl)
            {
                CountDownToWeiding();
            }
        }

        /***************************************************************************
                           Serial port event handlers start
        ****************************************************************************/
    
        private void clearLogButton_Click(object sender, EventArgs e)
        {
            logBox.Text = "";
#if DEBUG
            signalDebugTextBox.Text = "";
#endif
        }
        
        private void serialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;

            int bytesInBuffer = port.BytesToRead;

            byte[] readBytes = new byte[bytesInBuffer];
            try
            {
                port.Read(readBytes, 0, bytesInBuffer);
            }
            catch (TimeoutException)
            {
                MessageBox.Show(this, "数据读取超时。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(this, "非法操作。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            catch (Exception)
            {
                MessageBox.Show(this, "未知错误。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

#if (DEBUG)
            WriteToDebugBox(string.Format("Serial Port Data Receivered: {0}", BitConverter.ToString(readBytes)));
#endif

            for (int i = 0; i < bytesInBuffer; i++)
            {
                var b = readBytes[i];

                if (b == 0xFF)
                {
                    signalBuffer.Clear();
                    timestamp = DateTime.Now;
                }

                signalBuffer.Add(b);

                if (signalBuffer.Count == Signal.LENGTH) // Signal catch finished.
                {
#if (DEBUG)
                    Console.Write("Signal content: ");
                    for (int j = 0; j < signalBuffer.Count; j++)
                    {
                        Console.Write("{0}", signalBuffer[j]);
                    }
                    Console.WriteLine();
#endif
                    Signal signal = new Signal(signalBuffer.ToArray(), timestamp);
                    
                    try
                    {
                        if (signal.Type != currentSentSignal.Type) // Currently only check signal type
                        {
                            var message = string.Format("信号传输错误：\r\n\r\n发送的信号是：{0}\r\n收到的信号是：{1}.\r\n", currentSentSignal.ToString(), signal.ToString());
                            //Ignore error.
                            this.UIThread(() =>
                            {
                                WriteToLogBox(message);
                            });
                        }
                    }
                    catch (Exception exp)
                    {
                        // Ignore
#if DEBUG
                        Console.WriteLine(exp.StackTrace);
#endif
                    }
                    
                    this.UIThread(() =>
                    {
                        var message = string.Format(" - 收到响应信号“{0}”", signal.ToString());
                        WriteToLogBox(message);
                    });

                    SignalProcess(signal);
                }
            }
        }

        /***************************************************************************
                               Serial port helper methods start
        ****************************************************************************/


        private void SignalProcess(Signal signal)
        {
#if (DEBUG)
            string message = "";
#endif
            if (signal.isValid())
            {
#if (DEBUG)
                //signalCache.Add(signal);
                if (signal.Step != int.MaxValue)
                {
                    message = signal.Type.ToString() + " step " + signal.Step + " detected.\r\n";
                }
                else
                {
                    message = signal.Type.ToString() + " detected.\r\n";
                }
#endif
                if (signal.Type == SignalType.AutoControlEnd)
                {
                    if (!isControlling)
                    {
                        if (counterdownTimer != null)
                        {
                            counterdownTimer.Stop();
                        }
                    }
                    else
                    {
                        timerCache.ForEach((t) => { t.Stop(); });
                        hardStop = true; // stop from signal.
                        isControlling = false; // not controlling.
                        AutoControl = false; // Auto control is over.
                        this.UIThread(() =>
                        {
                            StartWeldingButton.Enabled = true;
                            WriteToLogBox("焊接被手动终止！");
                        });
                    }
                }
                if (signal.Type == SignalType.AutoControlStart && isControlling == false)
                {
                    AutoControl = true;
                    this.UIThread(() =>
                    {
                        CountDownToWeiding();
                    });
                }
            }
#if (DEBUG)
            else
            {
                message = "Invalid signal: " + signal.Type.ToString() + " step " + signal.Step + " detected.\r\n";
            }

            Console.WriteLine(message);
#endif
        }


        // Form initialization
        private void InitialWeldingControlUI()
        {
            loadWeldingDataLists();
            InitializeOtherUI();
#if DEBUG
            PopSignalCombo();
#endif
        }

#if DEBUG
        private void PopSignalCombo()
        {
            SignalSelectionComboBox.Items.Clear();
            debugSignals.ToList().ForEach((s) => {
                SignalSelectionComboBox.Items.Add(s);
            });
            SignalSelectionComboBox.SelectedIndex = 10;
        }
        
        private void SendSignalButton_Click(object sender, EventArgs e)
        {
            SerialPort p = CurrentSerialPort;
            if (p == null)
            {
                WriteToDebugBox("端口未打开。");
                return;
            }
            if (SignalSelectionComboBox.SelectedIndex < 0 || SignalSelectionComboBox.SelectedIndex >= debugSignals.Length)
            {
                return;
            }
            var combo = SignalSelectionComboBox;
            var signal = debugSignals[SignalSelectionComboBox.SelectedIndex];
            if (signal.Type == SignalType.Acceleration || signal.Type == SignalType.Deceleration)
            {
                currentStep = signal.Type == SignalType.Acceleration ? currentStep + 1 : currentStep - 1;
                signal = new Signal(signal.Type, currentStep);
            }

            var data = signal.RawBytes;
            WriteToDebugBox(string.Format("发送指令:{0}, raw: {1}", signal.ToString(), signal.ToHexString()));
            p.Write(data, 0, data.Length);
            currentSentSignal = signal;
        }

        private void WriteToDebugBox(string content)
        {
            this.UIThread(() =>
            {
                try
                {
                    signalDebugTextBox.AppendText(content + "\r\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            });
        }
#endif

        private void InitializeOtherUI()
        {
            //Detail box
            WeldingDetailsTextBox.Text = History.ToString();
            var timer = new Timer();
            timer.Interval = 250;
            timer.Tick += new EventHandler((s, evt) => {
                if (isControlling && currentSentSignal.Type != SignalType.SolderEnd)
                {
                    var value = weldingProgressBar.Value;
                    try
                    {
                        weldingProgressBar.Value += 250;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(string.Format("Progress bar restore to previous value: {0}", value));
                        weldingProgressBar.Value = value;
                    }
                }
            });
            timer.Start();
        }

        // Load static data from database to UI.
        private void loadWeldingDataLists()
        {
            var db = new DataProcess();
            var gangtaoList = db.GangTaoList();
            gangtaoList.ForEach((item) => {
                GangTaoTypeComboBox.Items.Add(item);
            });
            var operatorList = db.OperatorList();
            operatorList.ForEach((item) => {
                OperatorNameComboBox.Items.Add(item);
            });
            var weldingItemList = db.WeldingItemList();
            weldingItemList.ForEach((item) => {
                WeldingItemComboBox.Items.Add(item);
            });
        }

        private void InitializePlot()
        {
            //TODO: Plot the history data.
            Plot.Model = new PlotModel();
            Plot.Dock = DockStyle.Fill;
            PlotBox.Controls.Add(Plot);
            

            Plot.Model.PlotType = PlotType.XY;
            Plot.Model.Background = OxyColor.FromRgb(255, 255, 255);
            Plot.Model.TextColor = OxyColor.FromRgb(0, 0, 0);

            axis1.Position = AxisPosition.Bottom;
            axis1.Minimum = -1.0;
            axis1.Maximum = 10.0;
            axis1.Title = "运行时间";
            axis1.Unit = "秒";
            Plot.Model.Axes.Add(axis1);

            var axis2 = new LinearAxis();
            axis2.Position = AxisPosition.Left;
            axis2.Minimum = -15.0;
            axis2.Maximum = 15.0;
            axis2.Title = "转速";
            axis2.Unit = "档";
            Plot.Model.Axes.Add(axis2);

            // add Series and Axis to plot model
            ScatterSeries arcScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0xFF, 0x66, 0x77) };
            ScatterSeries solderScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0xBB, 0x11, 0x66) };
            ScatterSeries accScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0x88, 0x33, 0x44) };
            ScatterSeries deaccScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0x55, 0x88, 0xAA) };
            ScatterSeries rotateScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0x33, 0xAA, 0xDD) };
            ScatterSeries reverseRotateScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0x55, 0x99, 0x11) };

            Plot.Model.Series.Add(arcScatterSeries);
            Plot.Model.Series.Add(solderScatterSeries);
            Plot.Model.Series.Add(accScatterSeries);
            Plot.Model.Series.Add(deaccScatterSeries);
            Plot.Model.Series.Add(rotateScatterSeries);
            Plot.Model.Series.Add(reverseRotateScatterSeries);

            var signals = History.Signals;
            var signalCount = signals.Count;
            
            Plot.Model.InvalidatePlot(true); // Invalidate it.
            
            var totalTime = (int)((signals.Last().Timestamp - signals.First().Timestamp).TotalMilliseconds); // Ignore time less tham 1ms.
            axis1.Maximum = Math.Floor(totalTime / 1000.0 + 5);

            for (int i = 0; i < signalCount; i++)
            {
                var signal = signals[i];
                var currentSpeed = 0;

                var delta = (int)((signals[i].Timestamp - signals.First().Timestamp).TotalMilliseconds);

                ScatterSeries currentSerials = null;
                switch (signal.Type)
                {
                    case SignalType.ArcStart:
                        currentSerials = arcScatterSeries;
                        break;
                    case SignalType.ArcEnd:
                        currentSerials = arcScatterSeries;
                        break;
                    case SignalType.SolderStart:
                        currentSerials = solderScatterSeries;
                        break;
                    case SignalType.SolderEnd:
                        currentSerials = solderScatterSeries;
                        break;
                    case SignalType.Acceleration:
                        currentSerials = accScatterSeries;
                        currentSpeed = signal.Step;
                        break;
                    case SignalType.Deceleration:
                        currentSerials = deaccScatterSeries;
                        currentSpeed = signal.Step; // TODO: Fixme, should be minus while reverse rotate.
                        break;
                    case SignalType.RevolveStart:
                        currentSerials = reverseRotateScatterSeries;
                        break;
                    case SignalType.RevolveEnd:
                        currentSerials = reverseRotateScatterSeries;
                        break;
                    case SignalType.Unknown:
                        currentSerials = rotateScatterSeries; // TODO: Fix it.
                        break;
                    default:
                        currentSerials = rotateScatterSeries; // TODO: Fix it.
                        break;
                }
                var point = new ScatterPoint(delta / 1000.0, currentSpeed, 3);
                currentSerials.Points.Add(point);
            }
        }

        // Validate text input
        private void UpdateControlColor(Control sender, bool isFloat = false)
        {
            string text = sender.Text;
            text = text.Trim();
            sender.Text = text;

            var property = sender.GetType().GetProperty("SelectionStart");
            property?.SetValue(sender, sender.Text.Length + 1, null);

            if (isValidNumberInput(text, isFloat))
            {
                sender.BackColor = Color.White;
            }
            else
            {
                sender.BackColor = Color.Red;
            }
        }

        private bool isValidNumberInput(string text, bool isFloatNumber = false)
        {
            string pattern;
            if (isFloatNumber)
            {
                pattern = "^(\\s?\\s?\\d+(\\.\\d{0,3}){0,1}\\s?)?$"; // Negtive number is excluded.
            }
            else
            {
                pattern = "^(\\s?-?\\s?\\d+\\s?)?$";
            }
            Regex regex = new Regex(pattern);

            return regex.IsMatch(text);
        }

        private void startWelding(SerialPort p)
        {
            StartWeldingButton.Enabled = false;
            // Progress bar
            WriteToLogBox("焊接开始...\r\n");

            var signals = History.Signals;
            var totalTime = History.Signals.Last().Delta;
            weldingProgressBar.Minimum = 0;
            weldingProgressBar.Maximum = totalTime;
            weldingProgressBar.Value = 0;

            CurrentSerialPort = p;
            executeSignalGroup(0);
        }

        private void executeSignalGroup(int currentIndex)
        {
            var p = CurrentSerialPort;
            var progressBarStart = weldingProgressBar.Value;
            var signals = History.Signals;
            var signalCount = signals.Count;
            timerCache.Clear();
            for (int i = 0; i < signals.Count; i++)
            {
                var sig = signals[i];
                if (sig.Delta == 0)
                {
                    var data = sig.RawBytes;
                    WriteToLogBox(string.Format("发送指令({0}/{1}): {2}", i + 1, signalCount, sig.ToString()));
                    p.Write(data, 0, data.Count());
                    currentSentSignal = sig;
                    isControlling = true;
                }
                else
                {
                    Timer timer = new Timer();
                    timerCache.Add(timer);
                    timer.Interval = sig.Delta;
                    var data = sig.RawBytes;
                    var k = i;
                    var ttl = sig.Delta;
                    timer.Tick += new EventHandler((s, evt) =>
                    {
                        timer.Stop();
                        weldingProgressBar.Value = progressBarStart + ttl;
                        WriteToLogBox(string.Format("发送指令({0}/{1}): {2}", k + 1, signalCount, sig.ToString()));
                        p.Write(data, 0, data.Count());
                        this.currentSentSignal = sig;
                        if (k == signalCount - 1) // last order
                        {
                            isControlling = false;
                            FinishUpWelding();
                        }
                    });
                    timer.Start();
                }
            }
        }

        private void WriteToLogBox(string content)
        {
            this.UIThread(() => {
                try
                {
                    logBox.AppendText(content + "\r\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                
            });
        }

        private void FinishUpWelding()
        {
            StartWeldingButton.Enabled = true;
            SaveAutoWeldHistory();
            isControlling = false;
            if (AutoControl)
            {
                // Remove event handler when close.
                CurrentSerialPort.DataReceived -= dataReceivedEventHandler;
                DialogResult = DialogResult.OK;
            }
        }

        private void CountDownToWeiding()
        {
            WriteToLogBox("即将开始自动焊接...");
            Timer timer = new Timer();
            counterdownTimer = timer;
            timer.Interval = 1000;
            int counter = 5;
            timer.Tick += new EventHandler((s, evt) =>
            {
                if (counter < 1)
                {
                    // Start welding control.
                    timer.Stop();
                    StartWeldingButton.PerformClick();
                }
                else
                {
                    WriteToLogBox(string.Format("{0}...", counter));
                }
                counter--;
            });
            timer.Start();
        }

        /***************************************************************************
                              General Event handlers start
        ****************************************************************************/

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Remove event handler when close.
            CurrentSerialPort.DataReceived -= dataReceivedEventHandler;
            if (isControlling) { // not controlling will never be executed. 
                if (hardStop)
                {
                    hardStop = false; // reset to default value.
                    DialogResult = DialogResult.Cancel;
                }
                else  // Prompt before close while welding.
                {
                    var result = MessageBox.Show(this, "焊接时退出焊接控制窗口将导致焊接中的产品报废，请勿在实际焊接时停止自动控制!!!\r\n\r\n是否停止焊接控制？", "焊接中...", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                    if (result == DialogResult.OK)
                    {
                        // Stop signals
                        timerCache.ForEach((t) => { t.Stop(); });
                        isControlling = false;
                        DialogResult = DialogResult.Cancel;
                    }
                }
            }
            else
            {
                WriteToLogBox("\r\n焊接完成。");
                DialogResult = DialogResult.OK;
            }
        }
        
        private void SaveAutoWeldHistory()
        {
            //SaveRecordButton.Enabled = false; // Disable it.
            var dict = new Dictionary<string, object>();
            dict["task_name"] = TaskNameTextBox.Text.Trim();
            dict["gangtao_type"] = GangTaoTypeComboBox.Text.Trim();
            dict["welding_item"] = WeldingItemComboBox.Text.Trim();
            dict["welding_current"] = WeldingCurrentTextBox.Text.Trim();
            dict["ar_flow"] = ArGasFlowTextBox.Text.Trim();
            dict["room_temperature"] = RoomTempTextBox.Text.Trim();
            var op = OperatorNameComboBox.Text.Trim();
            dict["operator"] = op;
            dict["history_id"] = History.Id;

            if (op != "") // Valid op.
            {
                var db = new DataProcess();
                var ops = db.OperatorList();
                if (!ops.Contains(op))
                {
                    db.addOperator(op); // Save operator
                }
            }

            // If all OK, close.
            DateTime dt = DateTime.Now;
            dict["created_at"] = dt;
            try
            {
                dict["name"] = "";
                var history = new AutoWeldHistory(dict);
                history.Save();
            }
            catch (Exception excp)
            {
#if DEBUG
                Console.WriteLine(excp.StackTrace);
#endif
                throw;
            }
        }

        private void GangTaoTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            UpdateControlColor((ComboBox)sender);
        }

        private void WeldingCurrentTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateControlColor((TextBox)sender, true);
        }

        private void ArGasFlowTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateControlColor((TextBox)sender, true);
        }

        private void RoomTempTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateControlColor((TextBox)sender, true);
        }

        private void StartWeldingButton_Click(object sender, EventArgs e)
        {
            //
            SerialPort p = CurrentSerialPort;
            if (p == null)
            {
                logBox.AppendText("端口未打开。\r\n");
                return;
            }
            startWelding(p);
        }
    }
}
