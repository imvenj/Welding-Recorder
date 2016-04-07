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
        private List<SerialPort> portsList = new List<SerialPort>();
        private List<byte> signalBuffer = new List<byte>(6);
        private DateTime timestamp = DateTime.Now;
        private bool isControlling = false;
        private PlotView Plot = new PlotView();
        private LinearAxis axis1 = new LinearAxis();
        private Signal currentSentSignal;
        private SerialPort currentSerialPort;
        private List<Timer> timerCache = new List<Timer>();

        public WeldingControlForm()
        {
            InitializeComponent();

            PortsBox.SelectedIndexChanged += new EventHandler(portsBox_SelectedIndexChanged);
            PortsBox.DropDown += new EventHandler(portsBox_DropDown);

            // 波特率默认选择9600。 // BaudRate
            rateBox.SelectedIndex = 6;
            // 校验位默认选择None。 // Parity
            parityBox.SelectedIndex = 2;
            // 数据位默认选8。      // DataBit
            dataBitsBox.SelectedIndex = 3;
            // 停止位              // StopBits
            stopBitsBox.SelectedIndex = 0;
        }

        public WeldingControlForm(History history) : this()
        {
            History = history;
        }
        
        private void WeldingControlForm_Load(object sender, EventArgs e)
        {
            InitialWeldingControlUI();
            InitializePlot();
        }

        /***************************************************************************
                           Serial port event handlers start
    ****************************************************************************/

        private void openCloseButton_Click(object sender, EventArgs e)
        {
            if (isControlling)
            {
                MessageBox.Show(this, "正在运行焊接程序，此时不允许关闭串口。", "焊接中...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                string portName = PortsBox.Text.ToUpper();
                SerialPort port = getPortWithPortName(portName);
                if (port != null && port.IsOpen)
                {
                    closePortWithName(portName);
                }
                else
                {
                    openPortWithName(portName);
                }
            }
        }

        private void clearLogButton_Click(object sender, EventArgs e)
        {
            logBox.Text = "";
        }

        private void portsBox_DropDown(object sender, EventArgs e)
        {
            loadPortList();
        }

        private void portsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox pb = (ComboBox)sender;
            string selectedPortName = ((string)pb.SelectedItem).ToUpper();
            SerialPort port = getPortWithPortName(selectedPortName);
            if (port != null && port.IsOpen)
            {
                OpenCloseButton.Text = "关闭";
            }
            else
            {
                OpenCloseButton.Text = "打开";
            }
        }

        private void serialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
#if (DEBUG)
            Console.WriteLine("Serial Port Data Receivered.");

#endif
            SerialPort port = (SerialPort)sender;
            bool isBase64 = base64CheckBox.Checked;

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
                    try
                    {
                        if (signal.Type != currentSentSignal.Type) // Currently only check signal type
                        {
                            var message = string.Format("信号传输错误：\r\n\r\n发送的信号是：{0}\r\n收到的信号是：{1}.\r\n\r\n是否开始手工操作？", currentSentSignal.ToString(), signal.ToString());
                            this.UIThread(() =>
                            {
                                if (MessageBox.Show(this, message, "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                { // Manual override.
                                    timerCache.ForEach((t) => {
                                        t.Stop();
                                    });
                                }
                            });
                            // TODO: Do more with this critical error.
                        } // TODO: Maybe check timestamp later.
                          // process received data for debug
                        SignalProcess(signal);
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
                }
            }
        }

        /***************************************************************************
                               Serial port helper methods start
        ****************************************************************************/

#if (DEBUG)
        private void SignalProcess(Signal signal)
        {
            string message = "";
            if (signal.isValid())
            {
                //signalCache.Add(signal);
                if (signal.Step != int.MaxValue)
                {
                    message = signal.Type.ToString() + " step " + signal.Step + " detected.\r\n";
                }
                else
                {
                    message = signal.Type.ToString() + " detected.\r\n";
                }
            }
            else
            {
                message = "Invalid signal: " + signal.Type.ToString() + " step " + signal.Step + " detected.\r\n";
            }

            Console.WriteLine(message);
        }
#endif

        private void loadPortList()
        {
            // 初始化端口列表
            string[] ports = SerialPort.GetPortNames();
            PortsBox.Items.Clear();
            foreach (var port in ports)
            {
                PortsBox.Items.Add(port);
            }
            if (PortsBox.Items.Count > 0)
            {
                PortsBox.SelectedIndex = 0;
            }
        }

        private void updateUIWithPort(string portName)
        {
            var port = getPortWithPortName(portName);
            if (port.IsOpen)
            {
                PortStatusImageBox.Image = Properties.Resources.Green_Ball;
                OpenCloseButton.Text = "关闭";
            }
            else
            {
                PortStatusImageBox.Image = Properties.Resources.Red_Ball;
                OpenCloseButton.Text = "打开";
            }
        }

        private void openPortWithName(string portName)
        {
            if (String.IsNullOrEmpty(portName))
            {
                MessageBox.Show(this, "请选择一个串口或输入串口名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!isPortNameValid(portName.ToUpper()))
            {
                MessageBox.Show(this, "串口名不合法或串口不存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SerialPort p = getPortWithPortName(portName);
            if (p != null)
            {
                if (!portsList.Contains(p))
                {
                    portsList.Add(p);
                }
                return;
            }
            else
            {
                p = new SerialPort(portName);
                p.BaudRate = Convert.ToInt32(rateBox.Text);
                p.Parity = translateStringToParity(parityBox.Text);
                p.DataBits = Convert.ToInt32(dataBitsBox.Text);
                p.StopBits = translateStringToStopBits(stopBitsBox.Text);
                p.DataReceived += new SerialDataReceivedEventHandler(serialPortDataReceived);
                p.ErrorReceived += new SerialErrorReceivedEventHandler((sender, e) => {
                    var evt = e;
                    Console.WriteLine(e.EventType);
                });

                try
                {
                    p.Open();
                    // Disable PortsBox after port opened.
                    PortsBox.Enabled = false;
                    PortStatusImageBox.Image = Properties.Resources.Green_Ball;
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show(this, "端口被占用。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //throw;
                }
                portsList.Add(p);
                logBox.AppendText(portName + "已打开。" + "共打开了" + portsList.Count + "个串口。\r\n");
                OpenCloseButton.Text = "关闭";
            }
        }

        private void closePortWithName(string portName)
        {
            SerialPort p = getPortWithPortName(portName);
            if (p != null)
            {
                p.Close();
                if (!p.IsOpen)
                {
                    portsList.Remove(p);
                    logBox.AppendText(portName + "已关闭。" + "共打开了" + portsList.Count + "个串口。\r\n");
                    OpenCloseButton.Text = "打开";
                    // Re-enable PortsBox after port closed.
                    PortsBox.Enabled = true;
                    PortStatusImageBox.Image = Properties.Resources.Red_Ball;
                }
                else
                {
                    logBox.AppendText("端口" + portName + "关闭失败。\r\n");
                }
            }
        }

        private SerialPort getPortWithPortName(string portname)
        {
            foreach (var p in portsList)
            {
                if (p.PortName == portname)
                {
                    return p;
                }
            }

            return null;
        }

        private void closeAllPorts()
        {
            var ports = SerialPort.GetPortNames();

            foreach (var port in ports)
            {
                closePortWithName(port);
            }
        }

        private Parity translateStringToParity(string str)
        {
            Parity p = Parity.None;
            switch (str)
            {
                case "Even":
                    p = Parity.Even;
                    break;
                case "Mark":
                    p = Parity.Mark;
                    break;
                case "Space":
                    p = Parity.Space;
                    break;
                case "Odd":
                    p = Parity.Odd;
                    break;
                case "None":
                default:
                    p = Parity.None;
                    break;
            }
            return p;
        }

        private StopBits translateStringToStopBits(string str)
        {
            StopBits p = StopBits.One;
            switch (str)
            {
                case "2":
                    p = StopBits.Two;
                    break;
                case "1":
                default:
                    p = StopBits.One;
                    break;
            }
            return p;
        }

        private bool isPortNameValid(string portName)
        {
            bool result = false;
            var names = SerialPort.GetPortNames();
            foreach (var name in names)
            {
                if (name == portName)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        // Form initialization
        private void InitialWeldingControlUI()
        {
            loadPortList();
            loadWeldingDataLists();
            InitializeOtherUI();
        }

        private void InitializeOtherUI()
        {
            //Detail box
            WeldingDetailsTextBox.Text = History.ToString();
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

            if (WeldingItemComboBox.Items.Count > 0)
            {
                WeldingItemComboBox.SelectedIndex = 0;
            }
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
            var totalTime = History.SignalGroups.Sum(g => g.Last().Delta );
            weldingProgressBar.Minimum = 0;
            weldingProgressBar.Maximum = totalTime;
            weldingProgressBar.Value = 0;

            currentSerialPort = p;
            executeSignalGroup(0);
        }

        private void executeSignalGroup(int currentIndex)
        {
            var p = currentSerialPort;
            var group = History.SignalGroups[currentIndex];
            WriteToLogBox(string.Format("执行第{0}组子过程", currentIndex + 1));
            var signalCount = group.Count;
            var progressBarStart = weldingProgressBar.Value;

            for (int i = 0; i < signalCount; i++)
            {
                var sig = group[i];
                if (sig.Delta == 0)
                {
                    var data = sig.RawBytes;
                    WriteToLogBox(string.Format("发送指令({0}/{1}): {2}", i + 1, signalCount, sig.ToString()));
                    p.Write(data, 0, data.Count());
                    currentSentSignal = sig;
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
                            currentIndex += 1;
                            if (currentIndex < History.SignalGroups.Count)
                            {
                                var message = string.Format("第{0}阶段焊接完成。是否需要更换焊材？\r\n\r\n如果是，请在更换完成后点击“确定”继续焊接；\r\n否则请直接点击“确定”继续焊接。", currentIndex);
                                if (MessageBox.Show(this, message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    executeSignalGroup(currentIndex);
                                }
                            }
                            else
                            {
                                FinishUpWelding();
                            }
                        }
                    });
                    timer.Start();
                }
            }
        }

        private void WriteToLogBox(string content)
        {
            this.UIThread(() => {
                logBox.AppendText(content + "\r\n");
            });
        }

        private void FinishUpWelding()
        {
            StartWeldingButton.Enabled = true;
            if (MessageBox.Show(this, "焊接完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                WriteToLogBox("\r\n焊接完成。\r\n");
            }
            
        }

        /***************************************************************************
                              General Event handlers start
        ****************************************************************************/

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (!isControlling)
            { // Not recording...
                //TODO: The chance to save welding control
                closeAllPorts();
                DialogResult = DialogResult.Cancel;
            }
            else // Prevent form close while recording.
            {
                MessageBox.Show(this, "不能在焊接时关闭这个窗口。", "焊接中...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // TODO: Fixme: Always save welding control result!!!
        private void SaveRecordButton_Click(object sender, EventArgs e)
        {
            //SaveRecordButton.Enabled = false; // Disable it.
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
            var inputBox = new InputBox("请输入焊接记录标题", "提示", "记录 - " + dt.ToLongDateString() + " " + dt.ToLongTimeString());
            var result = inputBox.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                try
                {
                    dict["name"] = inputBox.InputResult;
                    var history = new History(dict);
                    //history.Signals = signalCache;
                    history.Save();
                }
                catch (Exception excp)
                {
                    //TODO: Save Result and crash.
#if DEBUG
                    Console.WriteLine(excp.StackTrace);
#endif
                    throw;
                }

                if (MessageBox.Show(this, "已保存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    closeAllPorts();
                    DialogResult = DialogResult.OK;
                }
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
            SerialPort p = getPortWithPortName(PortsBox.Text.ToUpper());
            if (p == null)
            {
                logBox.AppendText("端口未打开。\r\n");
                return;
            }
            // TODO: if signalCache not empty, this is another chance to save data.
            var result = MessageBox.Show(this, "即将用此程序开始控制焊接。\r\n\r\n是否开始？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                startWelding(p);
            }
            else
            {
                return;
            }
            
        }
    }
    
}
