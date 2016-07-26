namespace Welding_Recorder
{
    partial class RecordForm
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
            this.sendMessageInfoLabel = new System.Windows.Forms.Label();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.GangTaoTypeComboBox = new System.Windows.Forms.ComboBox();
            this.WeldingItemComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.WeldingCurrentTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.OperatorNameComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ArGasFlowTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.RoomTempTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.CancelFormButton = new System.Windows.Forms.Button();
            this.SaveRecordButton = new System.Windows.Forms.Button();
            this.PlotBox = new System.Windows.Forms.GroupBox();
            this.ForceStopButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendMessageInfoLabel
            // 
            this.sendMessageInfoLabel.AutoSize = true;
            this.sendMessageInfoLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sendMessageInfoLabel.Location = new System.Drawing.Point(549, 401);
            this.sendMessageInfoLabel.Name = "sendMessageInfoLabel";
            this.sendMessageInfoLabel.Size = new System.Drawing.Size(203, 12);
            this.sendMessageInfoLabel.TabIndex = 33;
            this.sendMessageInfoLabel.Text = "发送消息按钮仅用于单串口调试。-->";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(758, 396);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(75, 23);
            this.sendMessageButton.TabIndex = 18;
            this.sendMessageButton.Text = "发送消息";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(443, 396);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(75, 23);
            this.clearLogButton.TabIndex = 17;
            this.clearLogButton.Text = "清空日志";
            this.clearLogButton.UseVisualStyleBackColor = true;
            this.clearLogButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(443, 12);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(390, 378);
            this.logBox.TabIndex = 18;
            this.logBox.TabStop = false;
            this.logBox.Tag = "是";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "缸套规格";
            // 
            // GangTaoTypeComboBox
            // 
            this.GangTaoTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GangTaoTypeComboBox.FormattingEnabled = true;
            this.GangTaoTypeComboBox.Location = new System.Drawing.Point(79, 20);
            this.GangTaoTypeComboBox.Name = "GangTaoTypeComboBox";
            this.GangTaoTypeComboBox.Size = new System.Drawing.Size(108, 20);
            this.GangTaoTypeComboBox.TabIndex = 1;
            this.GangTaoTypeComboBox.TextChanged += new System.EventHandler(this.GangTaoTypeComboBox_TextChanged);
            // 
            // WeldingItemComboBox
            // 
            this.WeldingItemComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WeldingItemComboBox.FormattingEnabled = true;
            this.WeldingItemComboBox.Location = new System.Drawing.Point(271, 20);
            this.WeldingItemComboBox.Name = "WeldingItemComboBox";
            this.WeldingItemComboBox.Size = new System.Drawing.Size(108, 20);
            this.WeldingItemComboBox.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(212, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "焊接项目";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "焊接电流";
            // 
            // WeldingCurrentTextBox
            // 
            this.WeldingCurrentTextBox.Location = new System.Drawing.Point(79, 46);
            this.WeldingCurrentTextBox.Name = "WeldingCurrentTextBox";
            this.WeldingCurrentTextBox.Size = new System.Drawing.Size(89, 21);
            this.WeldingCurrentTextBox.TabIndex = 3;
            this.WeldingCurrentTextBox.TextChanged += new System.EventHandler(this.WeldingCurrentTextBox_TextChanged);
            // 
            // groupBox1
            // 
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
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 113);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "焊接信息";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(212, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 47;
            this.label15.Text = "操作人员";
            // 
            // OperatorNameComboBox
            // 
            this.OperatorNameComboBox.FormattingEnabled = true;
            this.OperatorNameComboBox.Location = new System.Drawing.Point(271, 75);
            this.OperatorNameComboBox.Name = "OperatorNameComboBox";
            this.OperatorNameComboBox.Size = new System.Drawing.Size(108, 20);
            this.OperatorNameComboBox.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(351, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 46;
            this.label13.Text = "L/min";
            // 
            // ArGasFlowTextBox
            // 
            this.ArGasFlowTextBox.Location = new System.Drawing.Point(271, 46);
            this.ArGasFlowTextBox.Name = "ArGasFlowTextBox";
            this.ArGasFlowTextBox.Size = new System.Drawing.Size(74, 21);
            this.ArGasFlowTextBox.TabIndex = 4;
            this.ArGasFlowTextBox.TextChanged += new System.EventHandler(this.ArGasFlowTextBox_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(212, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 44;
            this.label14.Text = "氩气流量";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(172, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 43;
            this.label11.Text = "℃";
            // 
            // RoomTempTextBox
            // 
            this.RoomTempTextBox.Location = new System.Drawing.Point(79, 73);
            this.RoomTempTextBox.Name = "RoomTempTextBox";
            this.RoomTempTextBox.Size = new System.Drawing.Size(89, 21);
            this.RoomTempTextBox.TabIndex = 5;
            this.RoomTempTextBox.TextChanged += new System.EventHandler(this.RoomTempTextBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 76);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 41;
            this.label12.Text = "室内温度";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(176, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 40;
            this.label10.Text = "A";
            // 
            // CancelFormButton
            // 
            this.CancelFormButton.Location = new System.Drawing.Point(758, 425);
            this.CancelFormButton.Name = "CancelFormButton";
            this.CancelFormButton.Size = new System.Drawing.Size(75, 23);
            this.CancelFormButton.TabIndex = 14;
            this.CancelFormButton.Text = "取消";
            this.CancelFormButton.UseVisualStyleBackColor = true;
            this.CancelFormButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveRecordButton
            // 
            this.SaveRecordButton.Enabled = false;
            this.SaveRecordButton.Location = new System.Drawing.Point(677, 425);
            this.SaveRecordButton.Name = "SaveRecordButton";
            this.SaveRecordButton.Size = new System.Drawing.Size(75, 23);
            this.SaveRecordButton.TabIndex = 15;
            this.SaveRecordButton.Text = "保存记录";
            this.SaveRecordButton.UseVisualStyleBackColor = true;
            this.SaveRecordButton.Click += new System.EventHandler(this.SaveRecordButton_Click);
            // 
            // PlotBox
            // 
            this.PlotBox.Location = new System.Drawing.Point(12, 131);
            this.PlotBox.Name = "PlotBox";
            this.PlotBox.Size = new System.Drawing.Size(415, 259);
            this.PlotBox.TabIndex = 45;
            this.PlotBox.TabStop = false;
            this.PlotBox.Text = "监控图";
            // 
            // ForceStopButton
            // 
            this.ForceStopButton.ForeColor = System.Drawing.Color.Red;
            this.ForceStopButton.Location = new System.Drawing.Point(596, 425);
            this.ForceStopButton.Name = "ForceStopButton";
            this.ForceStopButton.Size = new System.Drawing.Size(75, 23);
            this.ForceStopButton.TabIndex = 16;
            this.ForceStopButton.Text = "停止记录";
            this.ForceStopButton.UseVisualStyleBackColor = true;
            this.ForceStopButton.Click += new System.EventHandler(this.ForceStopButton_Click);
            // 
            // RecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 459);
            this.ControlBox = false;
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.ForceStopButton);
            this.Controls.Add(this.PlotBox);
            this.Controls.Add(this.SaveRecordButton);
            this.Controls.Add(this.CancelFormButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sendMessageInfoLabel);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.clearLogButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "焊接数据采集";
            this.Load += new System.EventHandler(this.RecordForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sendMessageInfoLabel;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox GangTaoTypeComboBox;
        private System.Windows.Forms.ComboBox WeldingItemComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox WeldingCurrentTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ArGasFlowTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox RoomTempTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox OperatorNameComboBox;
        private System.Windows.Forms.Button CancelFormButton;
        private System.Windows.Forms.Button SaveRecordButton;
        private System.Windows.Forms.GroupBox PlotBox;
        private System.Windows.Forms.Button ForceStopButton;
    }
}