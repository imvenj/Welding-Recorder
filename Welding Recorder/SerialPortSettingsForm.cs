using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Welding_Recorder
{
    public partial class SerialPortSettingsForm : Form
    {
        private Dictionary<string, Object> defaultSettings;
        private Dictionary<string, Object> DefaultSettings
        {
            get
            {
                if (defaultSettings == null)
                {
                    defaultSettings = new Dictionary<string, Object>();
                    defaultSettings.Add("SerialPortName", "");
                    defaultSettings.Add("Baudrate", "9600");
                    defaultSettings.Add("DataBits", "8");
                    defaultSettings.Add("StopBit", "1");
                    defaultSettings.Add("Parity", "None");
                    defaultSettings.Add("Based64", false);
                }
                return defaultSettings;
            }
        }

        private Dictionary<string, Object> currentSettings;
        private Dictionary<string, Object> CurrentSettings
        {
            get
            {
                if (currentSettings == null)
                {
                    currentSettings = new Dictionary<string, Object>();
                    currentSettings.Add("SerialPortName", "");
                    currentSettings.Add("Baudrate", "9600");
                    currentSettings.Add("DataBits", "8");
                    currentSettings.Add("StopBit", "1");
                    currentSettings.Add("Parity", "None");
                    currentSettings.Add("Based64", false);
                }
                return currentSettings;
            }
        }

        
        public SerialPortSettingsForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        void InitializeUI()
        {
            loadPortList();
            loadCurrentSettingsToUI();
        }

        public void loadCurrentSettingsToUI()
        {
            // Load current settings.
            CurrentSettings["SerialPortName"] = Properties.Settings.Default.SerialPortName;
            CurrentSettings["Baudrate"] = Properties.Settings.Default.Baudrate;
            CurrentSettings["DataBits"] = Properties.Settings.Default.DataBits;
            CurrentSettings["StopBit"] = Properties.Settings.Default.StopBit;
            CurrentSettings["Parity"] = Properties.Settings.Default.Parity;
            CurrentSettings["Based64"] = Properties.Settings.Default.Based64;
            // fill UI
            portsBox.Text = Properties.Settings.Default.SerialPortName;
            rateBox.SelectedIndex = rateBox.Items.IndexOf(Properties.Settings.Default.Baudrate);
            dataBitsBox.SelectedIndex = dataBitsBox.Items.IndexOf(Properties.Settings.Default.DataBits);
            stopBitsBox.SelectedIndex = stopBitsBox.Items.IndexOf(Properties.Settings.Default.StopBit);
            parityBox.SelectedIndex = parityBox.Items.IndexOf(Properties.Settings.Default.Parity);
            base64CheckBox.Checked = Properties.Settings.Default.Based64;
        }

        private bool SaveSettings(Dictionary<string, Object> dict)
        {
            Properties.Settings.Default.SerialPortName = (string)dict["SerialPortName"];
            Properties.Settings.Default.Baudrate = dict["Baudrate"].ToString();
            Properties.Settings.Default.DataBits = dict["DataBits"].ToString();
            Properties.Settings.Default.StopBit = dict["StopBit"].ToString();
            Properties.Settings.Default.Parity = dict["Parity"].ToString();
            Properties.Settings.Default.Based64 = (bool)dict["Based64"];
            try
            {
                Properties.Settings.Default.Save();
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.Message);
                return false;
            }
            return true;
        }

        private bool resetToDefaultSettings()
        {
            return SaveSettings(DefaultSettings);
        }

        private bool saveCurrentSettings()
        {
            updateCurrentSettings();
            return SaveSettings(CurrentSettings);
        }

        private void updateCurrentSettings()
        {
            CurrentSettings["SerialPortName"] = portsBox.Text;
            CurrentSettings["Baudrate"] = rateBox.Items[rateBox.SelectedIndex];
            CurrentSettings["DataBits"] = dataBitsBox.Items[dataBitsBox.SelectedIndex];
            CurrentSettings["StopBit"] = stopBitsBox.Items[stopBitsBox.SelectedIndex];
            CurrentSettings["Parity"] = parityBox.Items[parityBox.SelectedIndex];
            CurrentSettings["Based64"] = base64CheckBox.Checked;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveCurrentSettings())
            {
                MessageBox.Show("已保存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
            }
            // Right now, do nothing...
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadPortList()
        {
            // 初始化端口列表
            string[] ports = SerialPort.GetPortNames();
            portsBox.Items.Clear();
            foreach (var port in ports)
            {
                portsBox.Items.Add(port);
            }
            if (portsBox.Items.Count > 0)
            {
                portsBox.SelectedIndex = 0;
            }
        }
    }
}
