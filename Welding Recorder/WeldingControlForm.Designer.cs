namespace Welding_Recorder
{
    partial class WeldingControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenCloseButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.OperatorNameComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ArGasFlowTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.RoomTempTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.GangTaoTypeComboBox = new System.Windows.Forms.ComboBox();
            this.WeldingCurrentTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.WeldingItemComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PortStatusImageBox = new System.Windows.Forms.PictureBox();
            this.rateBox = new System.Windows.Forms.ComboBox();
            this.PortsBox = new System.Windows.Forms.ComboBox();
            this.base64CheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stopBitsBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.parityBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataBitsBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelFormButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.PlotBox = new System.Windows.Forms.GroupBox();
            this.StartWeldingButton = new System.Windows.Forms.Button();
            this.weldingProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.WeldingDetailsTextBox = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SignalSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.SendSignalButton = new System.Windows.Forms.Button();
            this.signalDebugTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortStatusImageBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenCloseButton
            // 
            this.OpenCloseButton.Location = new System.Drawing.Point(293, 74);
            this.OpenCloseButton.Name = "OpenCloseButton";
            this.OpenCloseButton.Size = new System.Drawing.Size(73, 23);
            this.OpenCloseButton.TabIndex = 16;
            this.OpenCloseButton.Text = "打开";
            this.OpenCloseButton.UseVisualStyleBackColor = true;
            this.OpenCloseButton.Click += new System.EventHandler(this.openCloseButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.OperatorNameComboBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.ArGasFlowTextBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.RoomTempTextBox);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.GangTaoTypeComboBox);
            this.groupBox1.Controls.Add(this.WeldingCurrentTextBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.WeldingItemComboBox);
            this.groupBox1.Location = new System.Drawing.Point(414, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 132);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "焊接信息";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(68, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(295, 21);
            this.textBox1.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 49;
            this.label6.Text = "任务书号";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(196, 105);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 47;
            this.label15.Text = "操作人员";
            // 
            // OperatorNameComboBox
            // 
            this.OperatorNameComboBox.FormattingEnabled = true;
            this.OperatorNameComboBox.Location = new System.Drawing.Point(255, 102);
            this.OperatorNameComboBox.Name = "OperatorNameComboBox";
            this.OperatorNameComboBox.Size = new System.Drawing.Size(108, 20);
            this.OperatorNameComboBox.TabIndex = 48;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(335, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 46;
            this.label13.Text = "L/min";
            // 
            // ArGasFlowTextBox
            // 
            this.ArGasFlowTextBox.Location = new System.Drawing.Point(255, 73);
            this.ArGasFlowTextBox.Name = "ArGasFlowTextBox";
            this.ArGasFlowTextBox.Size = new System.Drawing.Size(74, 21);
            this.ArGasFlowTextBox.TabIndex = 45;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(196, 76);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 44;
            this.label14.Text = "氩气流量";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(161, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 43;
            this.label11.Text = "℃";
            // 
            // RoomTempTextBox
            // 
            this.RoomTempTextBox.Location = new System.Drawing.Point(68, 100);
            this.RoomTempTextBox.Name = "RoomTempTextBox";
            this.RoomTempTextBox.Size = new System.Drawing.Size(89, 21);
            this.RoomTempTextBox.TabIndex = 42;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 41;
            this.label12.Text = "室内温度";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(165, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 40;
            this.label10.Text = "A";
            // 
            // GangTaoTypeComboBox
            // 
            this.GangTaoTypeComboBox.FormattingEnabled = true;
            this.GangTaoTypeComboBox.Location = new System.Drawing.Point(68, 47);
            this.GangTaoTypeComboBox.Name = "GangTaoTypeComboBox";
            this.GangTaoTypeComboBox.Size = new System.Drawing.Size(108, 20);
            this.GangTaoTypeComboBox.TabIndex = 35;
            // 
            // WeldingCurrentTextBox
            // 
            this.WeldingCurrentTextBox.Location = new System.Drawing.Point(68, 73);
            this.WeldingCurrentTextBox.Name = "WeldingCurrentTextBox";
            this.WeldingCurrentTextBox.Size = new System.Drawing.Size(89, 21);
            this.WeldingCurrentTextBox.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "缸套规格";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "焊接电流";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(196, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "焊接项目";
            // 
            // WeldingItemComboBox
            // 
            this.WeldingItemComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WeldingItemComboBox.FormattingEnabled = true;
            this.WeldingItemComboBox.Location = new System.Drawing.Point(255, 47);
            this.WeldingItemComboBox.Name = "WeldingItemComboBox";
            this.WeldingItemComboBox.Size = new System.Drawing.Size(108, 20);
            this.WeldingItemComboBox.TabIndex = 37;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PortStatusImageBox);
            this.groupBox2.Controls.Add(this.rateBox);
            this.groupBox2.Controls.Add(this.OpenCloseButton);
            this.groupBox2.Controls.Add(this.PortsBox);
            this.groupBox2.Controls.Add(this.base64CheckBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.stopBitsBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.parityBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dataBitsBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(414, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 104);
            this.groupBox2.TabIndex = 53;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "串口设置";
            // 
            // PortStatusImageBox
            // 
            this.PortStatusImageBox.Image = global::Welding_Recorder.Properties.Resources.Red_Ball;
            this.PortStatusImageBox.Location = new System.Drawing.Point(263, 72);
            this.PortStatusImageBox.Name = "PortStatusImageBox";
            this.PortStatusImageBox.Size = new System.Drawing.Size(24, 24);
            this.PortStatusImageBox.TabIndex = 33;
            this.PortStatusImageBox.TabStop = false;
            // 
            // rateBox
            // 
            this.rateBox.FormattingEnabled = true;
            this.rateBox.Items.AddRange(new object[] {
            "150",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.rateBox.Location = new System.Drawing.Point(246, 20);
            this.rateBox.Name = "rateBox";
            this.rateBox.Size = new System.Drawing.Size(120, 20);
            this.rateBox.TabIndex = 20;
            // 
            // PortsBox
            // 
            this.PortsBox.FormattingEnabled = true;
            this.PortsBox.Location = new System.Drawing.Point(62, 20);
            this.PortsBox.Name = "PortsBox";
            this.PortsBox.Size = new System.Drawing.Size(117, 20);
            this.PortsBox.TabIndex = 17;
            // 
            // base64CheckBox
            // 
            this.base64CheckBox.AutoSize = true;
            this.base64CheckBox.Location = new System.Drawing.Point(197, 78);
            this.base64CheckBox.Name = "base64CheckBox";
            this.base64CheckBox.Size = new System.Drawing.Size(60, 16);
            this.base64CheckBox.TabIndex = 32;
            this.base64CheckBox.Text = "Base64";
            this.base64CheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "串口";
            // 
            // stopBitsBox
            // 
            this.stopBitsBox.FormattingEnabled = true;
            this.stopBitsBox.Items.AddRange(new object[] {
            "1",
            "2"});
            this.stopBitsBox.Location = new System.Drawing.Point(246, 46);
            this.stopBitsBox.Name = "stopBitsBox";
            this.stopBitsBox.Size = new System.Drawing.Size(120, 20);
            this.stopBitsBox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "波特率";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "停止位";
            // 
            // parityBox
            // 
            this.parityBox.FormattingEnabled = true;
            this.parityBox.Items.AddRange(new object[] {
            "Even",
            "Mark",
            "None",
            "Odd",
            "Space"});
            this.parityBox.Location = new System.Drawing.Point(62, 72);
            this.parityBox.Name = "parityBox";
            this.parityBox.Size = new System.Drawing.Size(117, 20);
            this.parityBox.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "数据位";
            // 
            // dataBitsBox
            // 
            this.dataBitsBox.FormattingEnabled = true;
            this.dataBitsBox.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.dataBitsBox.Location = new System.Drawing.Point(62, 46);
            this.dataBitsBox.Name = "dataBitsBox";
            this.dataBitsBox.Size = new System.Drawing.Size(117, 20);
            this.dataBitsBox.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "校验位";
            // 
            // CancelFormButton
            // 
            this.CancelFormButton.Location = new System.Drawing.Point(729, 565);
            this.CancelFormButton.Name = "CancelFormButton";
            this.CancelFormButton.Size = new System.Drawing.Size(75, 23);
            this.CancelFormButton.TabIndex = 54;
            this.CancelFormButton.Text = "关闭";
            this.CancelFormButton.UseVisualStyleBackColor = true;
            this.CancelFormButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // clearLogButton
            // 
            this.clearLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearLogButton.Location = new System.Drawing.Point(315, 270);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(75, 23);
            this.clearLogButton.TabIndex = 47;
            this.clearLogButton.Text = "清空日志";
            this.clearLogButton.UseVisualStyleBackColor = true;
            this.clearLogButton.Click += new System.EventHandler(this.clearLogButton_Click);
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Location = new System.Drawing.Point(6, 49);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(384, 215);
            this.logBox.TabIndex = 46;
            this.logBox.Tag = "是";
            // 
            // PlotBox
            // 
            this.PlotBox.Location = new System.Drawing.Point(19, 339);
            this.PlotBox.Name = "PlotBox";
            this.PlotBox.Size = new System.Drawing.Size(383, 220);
            this.PlotBox.TabIndex = 58;
            this.PlotBox.TabStop = false;
            this.PlotBox.Text = "焊接曲线";
            // 
            // StartWeldingButton
            // 
            this.StartWeldingButton.Location = new System.Drawing.Point(648, 565);
            this.StartWeldingButton.Name = "StartWeldingButton";
            this.StartWeldingButton.Size = new System.Drawing.Size(75, 23);
            this.StartWeldingButton.TabIndex = 59;
            this.StartWeldingButton.Text = "开始焊接";
            this.StartWeldingButton.UseVisualStyleBackColor = true;
            this.StartWeldingButton.Click += new System.EventHandler(this.StartWeldingButton_Click);
            // 
            // weldingProgressBar
            // 
            this.weldingProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.weldingProgressBar.Location = new System.Drawing.Point(6, 20);
            this.weldingProgressBar.Name = "weldingProgressBar";
            this.weldingProgressBar.Size = new System.Drawing.Size(384, 23);
            this.weldingProgressBar.TabIndex = 60;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.weldingProgressBar);
            this.groupBox3.Controls.Add(this.logBox);
            this.groupBox3.Controls.Add(this.clearLogButton);
            this.groupBox3.Location = new System.Drawing.Point(408, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(396, 299);
            this.groupBox3.TabIndex = 61;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "焊接进度";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.WeldingDetailsTextBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(390, 321);
            this.groupBox4.TabIndex = 62;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "焊接程序信息";
            // 
            // WeldingDetailsTextBox
            // 
            this.WeldingDetailsTextBox.Location = new System.Drawing.Point(7, 20);
            this.WeldingDetailsTextBox.Multiline = true;
            this.WeldingDetailsTextBox.Name = "WeldingDetailsTextBox";
            this.WeldingDetailsTextBox.ReadOnly = true;
            this.WeldingDetailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.WeldingDetailsTextBox.Size = new System.Drawing.Size(377, 295);
            this.WeldingDetailsTextBox.TabIndex = 0;
            this.WeldingDetailsTextBox.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.signalDebugTextBox);
            this.groupBox5.Controls.Add(this.SendSignalButton);
            this.groupBox5.Controls.Add(this.SignalSelectionComboBox);
            this.groupBox5.Location = new System.Drawing.Point(811, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 575);
            this.groupBox5.TabIndex = 63;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Debug";
            // 
            // SignalSelectionComboBox
            // 
            this.SignalSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SignalSelectionComboBox.FormattingEnabled = true;
            this.SignalSelectionComboBox.Location = new System.Drawing.Point(8, 37);
            this.SignalSelectionComboBox.Name = "SignalSelectionComboBox";
            this.SignalSelectionComboBox.Size = new System.Drawing.Size(187, 20);
            this.SignalSelectionComboBox.TabIndex = 0;
            // 
            // SendSignalButton
            // 
            this.SendSignalButton.Location = new System.Drawing.Point(7, 65);
            this.SendSignalButton.Name = "SendSignalButton";
            this.SendSignalButton.Size = new System.Drawing.Size(187, 23);
            this.SendSignalButton.TabIndex = 1;
            this.SendSignalButton.Text = "发送信号";
            this.SendSignalButton.UseVisualStyleBackColor = true;
            this.SendSignalButton.Click += new System.EventHandler(this.SendSignalButton_Click);
            // 
            // signalDebugTextBox
            // 
            this.signalDebugTextBox.Location = new System.Drawing.Point(7, 94);
            this.signalDebugTextBox.Multiline = true;
            this.signalDebugTextBox.Name = "signalDebugTextBox";
            this.signalDebugTextBox.ReadOnly = true;
            this.signalDebugTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.signalDebugTextBox.Size = new System.Drawing.Size(186, 475);
            this.signalDebugTextBox.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 3;
            this.label16.Text = "选择一个信号";
            // 
            // WeldingControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 600);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.PlotBox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.StartWeldingButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CancelFormButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "WeldingControlForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "焊接控制";
            this.Load += new System.EventHandler(this.WeldingControlForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortStatusImageBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenCloseButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox OperatorNameComboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ArGasFlowTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox RoomTempTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox GangTaoTypeComboBox;
        private System.Windows.Forms.TextBox WeldingCurrentTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox WeldingItemComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox PortStatusImageBox;
        private System.Windows.Forms.ComboBox rateBox;
        private System.Windows.Forms.ComboBox PortsBox;
        private System.Windows.Forms.CheckBox base64CheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox stopBitsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox parityBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dataBitsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CancelFormButton;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.GroupBox PlotBox;
        private System.Windows.Forms.Button StartWeldingButton;
        private System.Windows.Forms.ProgressBar weldingProgressBar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox WeldingDetailsTextBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox signalDebugTextBox;
        private System.Windows.Forms.Button SendSignalButton;
        private System.Windows.Forms.ComboBox SignalSelectionComboBox;
    }
}