namespace Welding_Recorder
{
    partial class EditHistoryForm
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
            this.SignalsListView = new System.Windows.Forms.ListView();
            this.DeleteSelectedItemsButton = new System.Windows.Forms.Button();
            this.DoneButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DeSelectAllButton = new System.Windows.Forms.Button();
            this.TaskNameTextBox = new System.Windows.Forms.TextBox();
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SignalsListView
            // 
            this.SignalsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SignalsListView.Location = new System.Drawing.Point(12, 84);
            this.SignalsListView.Name = "SignalsListView";
            this.SignalsListView.Size = new System.Drawing.Size(720, 400);
            this.SignalsListView.TabIndex = 1;
            this.SignalsListView.UseCompatibleStateImageBehavior = false;
            // 
            // DeleteSelectedItemsButton
            // 
            this.DeleteSelectedItemsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteSelectedItemsButton.Location = new System.Drawing.Point(12, 499);
            this.DeleteSelectedItemsButton.Name = "DeleteSelectedItemsButton";
            this.DeleteSelectedItemsButton.Size = new System.Drawing.Size(105, 23);
            this.DeleteSelectedItemsButton.TabIndex = 2;
            this.DeleteSelectedItemsButton.Text = "删除选中的条目";
            this.DeleteSelectedItemsButton.UseVisualStyleBackColor = true;
            this.DeleteSelectedItemsButton.Click += new System.EventHandler(this.DeleteSelectedItemsButton_Click);
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.Location = new System.Drawing.Point(576, 499);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.TabIndex = 3;
            this.DoneButton.Text = "保存";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "请勾选你要删除的条目";
            // 
            // DeSelectAllButton
            // 
            this.DeSelectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeSelectAllButton.Location = new System.Drawing.Point(178, 499);
            this.DeSelectAllButton.Name = "DeSelectAllButton";
            this.DeSelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.DeSelectAllButton.TabIndex = 5;
            this.DeSelectAllButton.Text = "取消选择";
            this.DeSelectAllButton.UseVisualStyleBackColor = true;
            this.DeSelectAllButton.Click += new System.EventHandler(this.DeSelectAllButton_Click);
            // 
            // TaskNameTextBox
            // 
            this.TaskNameTextBox.Location = new System.Drawing.Point(72, 6);
            this.TaskNameTextBox.Name = "TaskNameTextBox";
            this.TaskNameTextBox.Size = new System.Drawing.Size(295, 21);
            this.TaskNameTextBox.TabIndex = 67;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 66;
            this.label6.Text = "任务书号";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(560, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 64;
            this.label15.Text = "操作人员";
            // 
            // OperatorNameComboBox
            // 
            this.OperatorNameComboBox.FormattingEnabled = true;
            this.OperatorNameComboBox.Location = new System.Drawing.Point(619, 35);
            this.OperatorNameComboBox.Name = "OperatorNameComboBox";
            this.OperatorNameComboBox.Size = new System.Drawing.Size(108, 20);
            this.OperatorNameComboBox.TabIndex = 65;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(699, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 63;
            this.label13.Text = "L/min";
            // 
            // ArGasFlowTextBox
            // 
            this.ArGasFlowTextBox.Location = new System.Drawing.Point(619, 6);
            this.ArGasFlowTextBox.Name = "ArGasFlowTextBox";
            this.ArGasFlowTextBox.Size = new System.Drawing.Size(74, 21);
            this.ArGasFlowTextBox.TabIndex = 62;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(560, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 61;
            this.label14.Text = "氩气流量";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(525, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 60;
            this.label11.Text = "℃";
            // 
            // RoomTempTextBox
            // 
            this.RoomTempTextBox.Location = new System.Drawing.Point(432, 33);
            this.RoomTempTextBox.Name = "RoomTempTextBox";
            this.RoomTempTextBox.Size = new System.Drawing.Size(89, 21);
            this.RoomTempTextBox.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(373, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 58;
            this.label12.Text = "室内温度";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(529, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 57;
            this.label10.Text = "A";
            // 
            // GangTaoTypeComboBox
            // 
            this.GangTaoTypeComboBox.FormattingEnabled = true;
            this.GangTaoTypeComboBox.Location = new System.Drawing.Point(72, 33);
            this.GangTaoTypeComboBox.Name = "GangTaoTypeComboBox";
            this.GangTaoTypeComboBox.Size = new System.Drawing.Size(108, 20);
            this.GangTaoTypeComboBox.TabIndex = 52;
            // 
            // WeldingCurrentTextBox
            // 
            this.WeldingCurrentTextBox.Location = new System.Drawing.Point(432, 6);
            this.WeldingCurrentTextBox.Name = "WeldingCurrentTextBox";
            this.WeldingCurrentTextBox.Size = new System.Drawing.Size(89, 21);
            this.WeldingCurrentTextBox.TabIndex = 56;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 51;
            this.label7.Text = "缸套规格";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(373, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 55;
            this.label9.Text = "焊接电流";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(200, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 53;
            this.label8.Text = "焊接项目";
            // 
            // WeldingItemComboBox
            // 
            this.WeldingItemComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WeldingItemComboBox.FormattingEnabled = true;
            this.WeldingItemComboBox.Location = new System.Drawing.Point(259, 33);
            this.WeldingItemComboBox.Name = "WeldingItemComboBox";
            this.WeldingItemComboBox.Size = new System.Drawing.Size(108, 20);
            this.WeldingItemComboBox.TabIndex = 54;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(657, 499);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 68;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 534);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.TaskNameTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.OperatorNameComboBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ArGasFlowTextBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.RoomTempTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.GangTaoTypeComboBox);
            this.Controls.Add(this.WeldingCurrentTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.WeldingItemComboBox);
            this.Controls.Add(this.DeSelectAllButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.DeleteSelectedItemsButton);
            this.Controls.Add(this.SignalsListView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(760, 550);
            this.Name = "EditHistoryForm";
            this.ShowIcon = false;
            this.Text = "编辑焊接记录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView SignalsListView;
        private System.Windows.Forms.Button DeleteSelectedItemsButton;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DeSelectAllButton;
        private System.Windows.Forms.TextBox TaskNameTextBox;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.Button cancelButton;
    }
}