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
        private bool isRecording = false;
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

        private Random r = new Random(384739);

        public RecordForm()
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
            string portName = PortsBox.Text.ToUpper();
            SerialPort port = getPortWithPortName(portName);
            if (port != null && port.IsOpen)
            {
                port.Close();
            }
        }

        /***************************************************************************
                               Serial port event handlers start
        ****************************************************************************/

        private void openCloseButton_Click(object sender, EventArgs e)
        {
            if (isRecording)
            {
                MessageBox.Show(this, "正在采集数据，此时不允许关闭串口。", "数据采集中...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void button1_Click(object sender, EventArgs e)
        {
            logBox.Text = "";
        }
        
        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            SerialPort p = getPortWithPortName(PortsBox.Text.ToUpper());
            if (p == null)
            {
                logBox.AppendText("端口未打开。\r\n");
                return;
            }
            else
            {
                sendMessage(p, p, 1024);
            }
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

                if (signalBuffer.Count == 6) // Signal catch finished.
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

        /***************************************************************************
                               Serial port helper methods start
        ****************************************************************************/

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

        private void sendMessage(SerialPort from, SerialPort to, object obj)
        {
            /* Simulate timing */
            sendMessageButton.Enabled = false;
            //TODO: Fixme
            byte[][] dataBytes = { 
                new byte[] { 0xFF, 0x01, 0x00, 0x08, 0x00, 0x09 }, // Arc Start
                new byte[] { 0xFF, 0x01, 0x00, 0x10, 0x00, 0x11 }, // Arc End
                new byte[] { 0xFF, 0x01, 0x00, 0x40, 0x00, 0x05 }, // Solder Start
                new byte[] { 0xFF, 0x01, 0x00, 0x09, 0x00, 0x91 }, // Rotate
                new byte[] { 0xFF, 0x01, 0x00, 0x04, 0x01, 0x04 }, // Acc step 1
                new byte[] { 0xFF, 0x01, 0x00, 0x04, 0x02, 0x07 }, // Acc step 2
                new byte[] { 0xFF, 0x01, 0x00, 0x02, 0x01, 0x02 }, // Deacc step 1
                new byte[] { 0xFF, 0x01, 0x00, 0x02, 0x02, 0x01 }, // Deacc step 1
                new byte[] { 0xFF, 0x01, 0x00, 0x60, 0x00, 0x61 }, // Rotate Stop
                new byte[] { 0xFF, 0x01, 0x00, 0x80, 0x00, 0x81 }, // Reverse Rotate
                new byte[] { 0xFF, 0x01, 0x00, 0x04, 0x01, 0x04 }, // Acc step 1
                new byte[] { 0xFF, 0x01, 0x00, 0x02, 0x01, 0x02 }, // Deacc step 1
                new byte[] { 0xFF, 0x01, 0x00, 0x70, 0x00, 0x71 }, // Reverse Rotate Stop
                new byte[] { 0xFF, 0x01, 0x00, 0x20, 0x00, 0x21 },  // Solder End (according to doc)
                new byte[] { 0xFF, 0x01, 0x00, 0x02, 0x00, 0x03 }  // Solder End (according to signal implementation)
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
            loadPortList();
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
                if (signal.Type == SignalType.ArcStart)
                {
                    isRecording = true;
                    this.UIThread(()=> { ForceStopButton.Visible = true; });
                }
                if (signal.Type == SignalType.SolderEnd)
                {
                    isRecording = false;
                    this.UIThread(() => {
                        SaveRecordButton.Enabled = true;  // enable save record button
                        ForceStopButton.Visible = false;
                    });
                }

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
                switch (signal.Type)
                {
                    case SignalType.ArcStart:
                        WriteToLogBox("焊接开始:\r\n");
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
                        MessageBox.Show(this, "焊接完成，你现在可以点击主界面上的“保存记录”，把本次焊接的数据保存到数据库。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                WriteToLogBox(signal.ToString());

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
                    currentSerials.Points.Add(point);
                    if (currentTime >= 10)
                    {
                        axis1.Maximum = Math.Floor(currentTime + 5);
                    }
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
                    var result = MessageBox.Show(this, "你还没有保存本次焊接的数据。是否关闭数据采集窗口并忽略焊接数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result != DialogResult.OK) 
                    {
                        // Do nothing.
                        return;
                    }
                    // ignore data and quit.
                }
                closeAllPorts();
                DialogResult = DialogResult.Cancel;
            }
            else // Prevent form close while recording.
            {
                MessageBox.Show(this, "不能在数据采集时关闭这个窗口。", "数据采集中...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void SaveRecordButton_Click(object sender, EventArgs e)
        {
            SaveRecordButton.Enabled = false; // Disable it.
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
                    history.Signals = signalCache;
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

        private void ForceStopButton_Click(object sender, EventArgs e)
        {
            if (isRecording)
            {
                var result = MessageBox.Show(this, "数据采集正在进行中。如果现在停止，您将丢失所有已采集的数据。\r\n\r\n是否停止采集？", "数据采集中...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    isRecording = false; // Stop recording
                    CancelButton_Click(CancelButton, e); // Quit and close 
                }
            }
        }
    }

}
