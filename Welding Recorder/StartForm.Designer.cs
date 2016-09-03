namespace Welding_Recorder
{
    partial class StartForm
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
            this.portsBox = new System.Windows.Forms.ComboBox();
            this.SerialPortLabel = new System.Windows.Forms.Label();
            this.DataCollectButton = new System.Windows.Forms.Button();
            this.AutoControlButton = new System.Windows.Forms.Button();
            this.SelectTemplateButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportDataBaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditHistoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditWorkerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManageTemplatesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SerialPortSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PortStatusImageBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortStatusImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenCloseButton
            // 
            this.OpenCloseButton.Location = new System.Drawing.Point(179, 28);
            this.OpenCloseButton.Name = "OpenCloseButton";
            this.OpenCloseButton.Size = new System.Drawing.Size(73, 23);
            this.OpenCloseButton.TabIndex = 34;
            this.OpenCloseButton.Text = "打开";
            this.OpenCloseButton.UseVisualStyleBackColor = true;
            this.OpenCloseButton.Click += new System.EventHandler(this.OpenCloseButton_Click);
            // 
            // portsBox
            // 
            this.portsBox.FormattingEnabled = true;
            this.portsBox.Location = new System.Drawing.Point(48, 30);
            this.portsBox.Name = "portsBox";
            this.portsBox.Size = new System.Drawing.Size(125, 20);
            this.portsBox.TabIndex = 36;
            // 
            // SerialPortLabel
            // 
            this.SerialPortLabel.AutoSize = true;
            this.SerialPortLabel.Location = new System.Drawing.Point(13, 33);
            this.SerialPortLabel.Name = "SerialPortLabel";
            this.SerialPortLabel.Size = new System.Drawing.Size(29, 12);
            this.SerialPortLabel.TabIndex = 37;
            this.SerialPortLabel.Text = "串口";
            this.SerialPortLabel.DoubleClick += new System.EventHandler(this.SerialPortLabel_DoubleClick);
            // 
            // DataCollectButton
            // 
            this.DataCollectButton.Location = new System.Drawing.Point(13, 56);
            this.DataCollectButton.Name = "DataCollectButton";
            this.DataCollectButton.Size = new System.Drawing.Size(269, 23);
            this.DataCollectButton.TabIndex = 38;
            this.DataCollectButton.Text = "数据采集";
            this.DataCollectButton.UseVisualStyleBackColor = true;
            this.DataCollectButton.Click += new System.EventHandler(this.DataCollectButton_Click);
            // 
            // AutoControlButton
            // 
            this.AutoControlButton.Location = new System.Drawing.Point(13, 85);
            this.AutoControlButton.Name = "AutoControlButton";
            this.AutoControlButton.Size = new System.Drawing.Size(269, 23);
            this.AutoControlButton.TabIndex = 39;
            this.AutoControlButton.Text = "自动控制";
            this.AutoControlButton.UseVisualStyleBackColor = true;
            this.AutoControlButton.Click += new System.EventHandler(this.AutoControlButton_Click);
            // 
            // SelectTemplateButton
            // 
            this.SelectTemplateButton.Location = new System.Drawing.Point(13, 114);
            this.SelectTemplateButton.Name = "SelectTemplateButton";
            this.SelectTemplateButton.Size = new System.Drawing.Size(269, 23);
            this.SelectTemplateButton.TabIndex = 40;
            this.SelectTemplateButton.Text = "选择模版";
            this.SelectTemplateButton.UseVisualStyleBackColor = true;
            this.SelectTemplateButton.Click += new System.EventHandler(this.SelectTemplateButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(293, 25);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportDataBaseMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.操作ToolStripMenuItem.Text = "文件";
            // 
            // ExportDataBaseMenuItem
            // 
            this.ExportDataBaseMenuItem.Name = "ExportDataBaseMenuItem";
            this.ExportDataBaseMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ExportDataBaseMenuItem.Text = "导出数据库文件";
            this.ExportDataBaseMenuItem.Click += new System.EventHandler(this.ExportDataBaseMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditHistoryMenuItem,
            this.EditWorkerMenuItem,
            this.ManageTemplatesMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // EditHistoryMenuItem
            // 
            this.EditHistoryMenuItem.Name = "EditHistoryMenuItem";
            this.EditHistoryMenuItem.Size = new System.Drawing.Size(148, 22);
            this.EditHistoryMenuItem.Text = "编辑焊接历史";
            this.EditHistoryMenuItem.Click += new System.EventHandler(this.EditHistoryMenuItem_Click);
            // 
            // EditWorkerMenuItem
            // 
            this.EditWorkerMenuItem.Name = "EditWorkerMenuItem";
            this.EditWorkerMenuItem.Size = new System.Drawing.Size(148, 22);
            this.EditWorkerMenuItem.Text = "编辑操作人员";
            // 
            // ManageTemplatesMenuItem
            // 
            this.ManageTemplatesMenuItem.Name = "ManageTemplatesMenuItem";
            this.ManageTemplatesMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ManageTemplatesMenuItem.Text = "管理焊接模版";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SerialPortSettingsMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // SerialPortSettingsMenuItem
            // 
            this.SerialPortSettingsMenuItem.Name = "SerialPortSettingsMenuItem";
            this.SerialPortSettingsMenuItem.Size = new System.Drawing.Size(124, 22);
            this.SerialPortSettingsMenuItem.Text = "串口设置";
            this.SerialPortSettingsMenuItem.Click += new System.EventHandler(this.SerialPortSettingsMenuItem_Click);
            // 
            // PortStatusImageBox
            // 
            this.PortStatusImageBox.Image = global::Welding_Recorder.Properties.Resources.Red_Ball;
            this.PortStatusImageBox.Location = new System.Drawing.Point(258, 27);
            this.PortStatusImageBox.Name = "PortStatusImageBox";
            this.PortStatusImageBox.Size = new System.Drawing.Size(24, 24);
            this.PortStatusImageBox.TabIndex = 35;
            this.PortStatusImageBox.TabStop = false;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 149);
            this.Controls.Add(this.SelectTemplateButton);
            this.Controls.Add(this.AutoControlButton);
            this.Controls.Add(this.DataCollectButton);
            this.Controls.Add(this.portsBox);
            this.Controls.Add(this.SerialPortLabel);
            this.Controls.Add(this.PortStatusImageBox);
            this.Controls.Add(this.OpenCloseButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.Text = "自动焊接控制";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortStatusImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PortStatusImageBox;
        private System.Windows.Forms.Button OpenCloseButton;
        private System.Windows.Forms.ComboBox portsBox;
        private System.Windows.Forms.Label SerialPortLabel;
        private System.Windows.Forms.Button DataCollectButton;
        private System.Windows.Forms.Button AutoControlButton;
        private System.Windows.Forms.Button SelectTemplateButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditHistoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SerialPortSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditWorkerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ManageTemplatesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportDataBaseMenuItem;
    }
}