namespace Welding_Recorder
{
    partial class SerialPortSettingsForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rateBox = new System.Windows.Forms.ComboBox();
            this.portsBox = new System.Windows.Forms.ComboBox();
            this.base64CheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stopBitsBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.parityBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataBitsBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rateBox);
            this.groupBox2.Controls.Add(this.portsBox);
            this.groupBox2.Controls.Add(this.base64CheckBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.stopBitsBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.parityBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dataBitsBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 104);
            this.groupBox2.TabIndex = 54;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "串口设置";
            // 
            // rateBox
            // 
            this.rateBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rateBox.FormattingEnabled = true;
            this.rateBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
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
            // portsBox
            // 
            this.portsBox.FormattingEnabled = true;
            this.portsBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.portsBox.Location = new System.Drawing.Point(62, 20);
            this.portsBox.Name = "portsBox";
            this.portsBox.Size = new System.Drawing.Size(117, 20);
            this.portsBox.TabIndex = 17;
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
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "默认串口";
            // 
            // stopBitsBox
            // 
            this.stopBitsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.parityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.dataBitsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(246, 122);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 55;
            this.saveButton.Text = "保存";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(327, 122);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 56;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SerialPortSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 152);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SerialPortSettingsForm";
            this.Text = "串口设置";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox rateBox;
        private System.Windows.Forms.ComboBox portsBox;
        private System.Windows.Forms.CheckBox base64CheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox stopBitsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox parityBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dataBitsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}