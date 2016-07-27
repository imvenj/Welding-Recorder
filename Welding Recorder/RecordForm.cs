using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;

namespace Welding_Recorder
{
    public partial class RecordForm : Form
    {
        private List<SerialPort> portsList = new List<SerialPort>();
        private List<byte> signalBuffer = new List<byte>(6);
        private DateTime timestamp = DateTime.Now;
        private bool isRecording = true;
        private List<Signal> signalCache = new List<Signal>(); // Signal cache to save recording process.
        private int currentSpeed = 0;
        private double currentTime = 0.0;

        private PlotView Plot = new PlotView();
        private LinearAxis axis1 = new LinearAxis();
        //private LineSeries s1 = new LineSeries { Title = "Speed", StrokeThickness = 1 };
        private ScatterSeries arcScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0xFF, 0x66, 0x77) };
        private ScatterSeries solderScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0xBB, 0x11, 0x66) };
        private ScatterSeries accScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0x88, 0x33, 0x44) };
        private ScatterSeries deaccScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0x55, 0x88, 0xAA) };
        private ScatterSeries rotateScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0x33, 0xAA, 0xDD) };
        private ScatterSeries reverseRotateScatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0x55, 0x99, 0x11) };
        
        private SerialPort CurrentSerialPort { get; set; }

        private SerialDataReceivedEventHandler dataReceivedEventHandler;

        private Random r = new Random(384739);

        public RecordForm(SerialPort p)
        {
            InitializeComponent();
            CurrentSerialPort = p;
            dataReceivedEventHandler = new SerialDataReceivedEventHandler(serialPortDataReceived);
            p.DataReceived += dataReceivedEventHandler;
        }

        /***************************************************************************
                               Form life cycle
        ****************************************************************************/

        private void RecordForm_Load(object sender, EventArgs e)
        {
            InitialRecordingUI();
            InitializePlot();
        }

        private void RecordForm_Closing(object sender, EventArgs e)
        {
            // Do nothing for now.
            // Maybe flush serial port cache?
            CurrentSerialPort.DataReceived -= dataReceivedEventHandler;
        }
        
        /***************************************************************************
                               Serial port event handlers start
        ****************************************************************************/
        
        private void button1_Click(object sender, EventArgs e)
        {
            logBox.Text = "";
        }
        
        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            var p = CurrentSerialPort;

            if (!p.IsOpen)
            {
                logBox.AppendText("端口未打开。\r\n");
                return;
            }
            else
            {
                sendMessage(p, p, 1024);
            }
        }
        
        private void serialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
#if (DEBUG)
            Console.WriteLine("Serial Port Data Receivered.");
#endif
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
                    SignalProcess(signal);
                }
            }
        }
        
        private void sendMessage(SerialPort from, SerialPort to, object obj)
        {
            /* Simulate timing */
            sendMessageButton.Enabled = false;
            //TODO: Fixme
            byte[][] dataBytes = { 
                new byte[] { 0xFF, 0x01, 0x00, 0x08, 0x00, 0x00, 0x09 }, // Arc Start
                new byte[] { 0xFF, 0x01, 0x00, 0x10, 0x00, 0x00, 0x11 }, // Arc End
                new byte[] { 0xFF, 0x01, 0x00, 0x40, 0x00, 0x00, 0x41 }, // Solder Start
                new byte[] { 0xFF, 0x01, 0x00, 0x90, 0x00, 0x00, 0x91 }, // Rotate
                new byte[] { 0xFF, 0x01, 0x00, 0x04, 0x01, 0x00, 0x04 }, // Acc step 1
                new byte[] { 0xFF, 0x01, 0x00, 0x04, 0x02, 0x00, 0x07 }, // Acc step 2
                new byte[] { 0xFF, 0x01, 0x00, 0x02, 0x01, 0x00, 0x02 }, // Deacc step 1
                new byte[] { 0xFF, 0x01, 0x00, 0x60, 0x00, 0x00, 0x61 }, // Rotate Stop
                new byte[] { 0xFF, 0x01, 0x00, 0x20, 0x00, 0x00, 0x21 }, // Solder End (First round)
                new byte[] { 0xFF, 0x01, 0x00, 0x80, 0x00, 0x00, 0x81 }, // Revolve start
                new byte[] { 0xFF, 0x01, 0x00, 0x70, 0x00, 0x00, 0x71 }, // Revolve Stop
                new byte[] { 0xFF, 0x01, 0x00, 0x40, 0x00, 0x00, 0x41 }, // Solder Start
                new byte[] { 0xFF, 0x01, 0x00, 0x90, 0x00, 0x00, 0x91 }, // Rotate
                new byte[] { 0xFF, 0x01, 0x00, 0x04, 0x02, 0x00, 0x07 }, // Acc step 2
                new byte[] { 0xFF, 0x01, 0x00, 0x60, 0x00, 0x00, 0x61 }, // Rotate Stop
                new byte[] { 0xFF, 0x01, 0x00, 0x20, 0x00, 0x00, 0x21 }, // Solder End (Second round)
                new byte[] { 0xFF, 0x01, 0x00, 0x30, 0x00, 0x00, 0x31 }, // Solder End (Second round)
            };

            int counter = 0;
            Timer timer = new Timer();
            timer.Interval = r.Next(1000, 3000); // Random interval.
            timer.Tick += new EventHandler((sender, e) => {
                if (counter == dataBytes.Length)
                {
                    sendMessageButton.Enabled = true;
                    timer.Stop();
                    return;
                }
                var data = dataBytes[counter];
                from.Write(data, 0, data.Count());
                counter++;
            });
            timer.Start();
        }

        /***************************************************************************
                               General helper methods start
        ****************************************************************************/

        // Form initialization
        private void InitialRecordingUI()
        {
            loadWeldingDataLists();
        }

        // Load static data from database to UI.
        private void loadWeldingDataLists()
        {
#if DEBUG
            sendMessageInfoLabel.Visible = true;
            sendMessageButton.Visible = true;
#else
            sendMessageInfoLabel.Visible = false;
            sendMessageButton.Visible = false;
#endif
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

            if (WeldingItemComboBox.Items.Count > 0)
            {
                WeldingItemComboBox.SelectedIndex = 0;
            }
        }
        
        private void InitializePlot()
        {
            Plot.Model = new PlotModel();
            Plot.Dock = DockStyle.Fill;
            PlotBox.Controls.Add(Plot);
            //this.Controls.Add(Plot);

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
            Plot.Model.Series.Add(arcScatterSeries);
            Plot.Model.Series.Add(solderScatterSeries);
            Plot.Model.Series.Add(accScatterSeries);
            Plot.Model.Series.Add(deaccScatterSeries);
            Plot.Model.Series.Add(rotateScatterSeries);
            Plot.Model.Series.Add(reverseRotateScatterSeries);
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

        private void SignalProcess(Signal signal)
        {
#if DEBUG
            string message = "";
#endif
            if (signal.isValid())
            {
                if (signal.Type == SignalType.CollectEnd)
                {
                    isRecording = false;
                    StopRecordAndSaveData();
                    return;
                }
                signalCache.Add(signal);
#if DEBUG
                if (signal.Step != int.MaxValue)
                {
                    message = signal.Type.ToString() + " step " + signal.Step + " detected.\r\n";
                }
                else
                {
                    message = signal.Type.ToString() + " detected.\r\n";
                }
#endif
                updatePlotWithSignal(signal);
            }
#if DEBUG
            else
            {
                message = "Invalid signal: " + signal.Type.ToString() + " step " + signal.Step + " detected.\r\n";
            }

            Console.WriteLine(message);
#endif
        }

        private void updatePlotWithSignal(Signal signal)
        {
            this.UIThread(() => {
                ScatterSeries currentSerials = null;
                currentSpeed = 0;
                switch (signal.Type)
                {
                    case SignalType.ArcStart:
                        WriteToLogBox("\r\n焊接程序开始:\r\n");
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
                    case SignalType.RotateStart:
                        currentSerials = rotateScatterSeries;
                        break;
                    case SignalType.RotateEnd:
                        currentSerials = rotateScatterSeries;
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

                WriteToLogBox("- " + signal.ToString());

                Plot.Model.InvalidatePlot(true);
                var signals = signalCache;
                var signalCount = signals.Count;

                if (signalCount == 1) // First point
                {
                    var point = new ScatterPoint(currentTime, currentSpeed, 3);
                    arcScatterSeries.Points.Add(point);
                }
                if (signalCount > 1)
                {
                    var previousSignal = signals[signalCount - 2];
                    var currentSignal = signals[signalCount - 1];

                    TimeSpan span = currentSignal.Timestamp - previousSignal.Timestamp;
                    currentTime += span.TotalSeconds;
                    var point = new ScatterPoint(currentTime, currentSpeed, 3);
                    Console.WriteLine("CurrentTime: " + currentTime);
                    if (Math.Ceiling(currentTime) >= 10)
                    {
                        axis1.Maximum = Math.Floor(currentTime + 5);
                    }
                    currentSerials.Points.Add(point);
                }
                // Create Line series
            });
        }

        private void WriteToLogBox(string content)
        {
            this.UIThread(() => {
                logBox.AppendText(content + "\r\n");
            });
        }

        /***************************************************************************
                              General Event handlers start
        ****************************************************************************/

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (!isRecording)
            { // Not recording...
                if (signalCache.Count > 0) // Data exist.
                {
                    SaveSignalDataAndClose();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
            else // Prevent form close while recording.
            {
                if (signalCache.Count > 0) {
                    var result = MessageBox.Show(this, "你正在记录焊接数据，退出窗口将中断数据记录。推荐你通过发送停止记录信号来关闭本对话框。\r\n\r\n真的要退出本窗口吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result != DialogResult.OK)
                    {
                        // Do nothing.
                        return;
                    }
                    else
                    {
                        SaveSignalDataAndClose();
                    }
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void SaveSignalDataAndClose()
        {
            var dict = new Dictionary<string, object>();
            dict["gangtao_type"] = GangTaoTypeComboBox.Text.Trim();
            dict["welding_item"] = WeldingItemComboBox.Text.Trim();
            dict["welding_current"] = WeldingCurrentTextBox.Text.Trim();
            dict["ar_flow"] = ArGasFlowTextBox.Text.Trim();
            dict["room_temperature"] = RoomTempTextBox.Text.Trim();
            var op = OperatorNameComboBox.Text.Trim();
            dict["operator"] = op;

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
                //Fixme: Generate a meaningful name.
                dict["name"] = "";
                var history = new History(dict);
                history.Signals = signalCache;
                history.Save();
                Console.WriteLine("Signal history saved.");
            }
            catch (Exception excp)
            {
                //TODO: Save Result and crash.
#if DEBUG
                Console.WriteLine(excp.StackTrace);
#endif
                throw;
            }

            DialogResult = DialogResult.OK;
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

        private void StopRecordAndSaveData()
        {
            // Remove event handler when record stop
            CurrentSerialPort.DataReceived -= dataReceivedEventHandler;
            if (signalCache.Count == 0) // Nothing recorded, just do nothing...
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                WriteToLogBox("\r\n本次焊接数据记录完成！正在保存焊接数据...\r\n");
                this.UIThread(() => {
                    SaveSignalDataAndClose();
                });
            }
        }
    }

}
