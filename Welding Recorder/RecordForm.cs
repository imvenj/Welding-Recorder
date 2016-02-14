using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Welding_Recorder
{
    public partial class RecordForm : Form
    {
        private List<SerialPort> portsList = new List<SerialPort>();
        private List<byte> signalBuffer = new List<byte>(6);
        private DateTime timestamp = DateTime.Now;

        public RecordForm()
        {
            InitializeComponent();

            portsBox.SelectedIndexChanged += new EventHandler(portsBox_SelectedIndexChanged);
            portsBox.DropDown += new EventHandler(portsBox_DropDown);

            // 波特率默认选择9600。 // BaudRate
            rateBox.SelectedIndex = 6;
            // 校验位默认选择None。 // Parity
            parityBox.SelectedIndex = 2;
            // 数据位默认选8。      // DataBit
            dataBitsBox.SelectedIndex = 3;
            // 停止位              // StopBits
            stopBitsBox.SelectedIndex = 0;
        }

        private void RecordForm_Load(object sender, EventArgs e)
        {
            loadPortList();
        }

        private void openCloseButton_Click(object sender, EventArgs e)
        {
            string portName = portsBox.Text.ToUpper();
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

        private void Form1_Closing(object sender, EventArgs e)
        {
            string portName = portsBox.Text.ToUpper();
            SerialPort port = getPortWithPortName(portName);
            if (port != null && port.IsOpen)
            {
                port.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logBox.Text = "";
        }

        private void clearDataButton_Click(object sender, EventArgs e)
        {
            dataOutputBox.Text = "";
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            SerialPort p = getPortWithPortName(portsBox.Text.ToUpper());
            if (p == null)
            {
                logBox.Text += "端口未打开。\r\n";
                return;
            }
            else
            {
                sendMessage(p, p, 1024);
            }
        }

        /**  Event Handlers  **/

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
                openCloseButton.Text = "关闭";
            }
            else
            {
                openCloseButton.Text = "打开";
            }
        }

        private void serialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            bool isBase64 = base64CheckBox.Checked;

            byte[] readBytes = { 0x00 };
            try
            {
                port.Read(readBytes, 0, 1);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("数据读取超时。");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("非法操作。");
                throw;
            }
            catch (Exception)
            {
                MessageBox.Show("未知错误。");
            }

            byte b = readBytes[0];
            if (signalBuffer.Count() == 6 && b == 0xFF) // Signal start
            {
                timestamp = DateTime.Now; // Save TimeStamp for signal start.
                signalBuffer.Clear();
                signalBuffer.Add(b);
            }
            else if (signalBuffer.Count() < 6) // Signal continue 
            {
                signalBuffer.Add(b);
            }
            else
            {
                // Do nothing.
            }

            if (signalBuffer.Count() == 6) // Signal catch finished.
            {
                Signal signal = new Signal(signalBuffer.ToArray(), timestamp);
                string message = "";

                if (signal.isValid())
                {
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

                this.UIThread(() => this.dataOutputBox.Text += message);
            }
        }

        /**  Helpers  **/

        private void loadPortList()
        {
            // 初始化端口列表
            string[] ports = SerialPort.GetPortNames();
            portsBox.Items.Clear();
            foreach (var port in ports)
            {
                portsBox.Items.Add(port);
            }
        }

        private void openPortWithName(string portName)
        {
            if (String.IsNullOrEmpty(portName))
            {
                MessageBox.Show("请选择一个串口或输入串口名称。");
                return;
            }
            else if (!isPortNameValid(portName.ToUpper()))
            {
                MessageBox.Show("串口名不合法或串口不存在。");
                return;
            }
            SerialPort p = getPortWithPortName(portName);
            if (p != null)
            {
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
                try
                {
                    p.Open();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("端口被占用。");
                    return;
                    //throw;
                }
                portsList.Add(p);
                logBox.Text += portName + "已打开。" + "共打开了" + portsList.Count + "个串口。\r\n";
                openCloseButton.Text = "关闭";
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
                    logBox.Text += portName + "已关闭。" + "共打开了" + portsList.Count + "个串口。\r\n";
                    openCloseButton.Text = "打开";
                    dataOutputBox.Text = ""; //清空数据框。
                }
                else
                {
                    logBox.Text += "端口" + portName + "关闭失败。\r\n";
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
            byte[] dataBytes = { 0x08 };
            from.Write(dataBytes, 0, dataBytes.Count());
        }
    }

    public static class ControlExtensions
    {
        public static void UIThread(this Control @this, Action code)
        {
            if (@this.InvokeRequired)
            {
                @this.BeginInvoke(code);
            }
            else
            {
                code.Invoke();
            }
        }
    }
}
}
