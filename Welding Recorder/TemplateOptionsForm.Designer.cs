namespace Welding_Recorder
{
    partial class TemplateOptionsForm
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
            this.WeldingItemComboBox = new System.Windows.Forms.ComboBox();
            this.GangTaoTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HistoriesListBox = new System.Windows.Forms.ListBox();
            this.ChooseButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WeldingItemComboBox
            // 
            this.WeldingItemComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WeldingItemComboBox.FormattingEnabled = true;
            this.WeldingItemComboBox.Location = new System.Drawing.Point(71, 12);
            this.WeldingItemComboBox.Name = "WeldingItemComboBox";
            this.WeldingItemComboBox.Size = new System.Drawing.Size(121, 20);
            this.WeldingItemComboBox.TabIndex = 0;
            this.WeldingItemComboBox.SelectedIndexChanged += new System.EventHandler(this.WeldingItemComboBox_SelectedIndexChanged);
            // 
            // GangTaoTypeComboBox
            // 
            this.GangTaoTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GangTaoTypeComboBox.FormattingEnabled = true;
            this.GangTaoTypeComboBox.Location = new System.Drawing.Point(258, 12);
            this.GangTaoTypeComboBox.Name = "GangTaoTypeComboBox";
            this.GangTaoTypeComboBox.Size = new System.Drawing.Size(121, 20);
            this.GangTaoTypeComboBox.TabIndex = 1;
            this.GangTaoTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.GangTaoTypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "焊接项目";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "缸套规格";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "焊接历史";
            // 
            // HistoriesListBox
            // 
            this.HistoriesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoriesListBox.FormattingEnabled = true;
            this.HistoriesListBox.ItemHeight = 12;
            this.HistoriesListBox.Location = new System.Drawing.Point(15, 65);
            this.HistoriesListBox.Name = "HistoriesListBox";
            this.HistoriesListBox.Size = new System.Drawing.Size(440, 136);
            this.HistoriesListBox.TabIndex = 5;
            // 
            // ChooseButton
            // 
            this.ChooseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseButton.Location = new System.Drawing.Point(299, 215);
            this.ChooseButton.Name = "ChooseButton";
            this.ChooseButton.Size = new System.Drawing.Size(75, 23);
            this.ChooseButton.TabIndex = 6;
            this.ChooseButton.Text = "选择";
            this.ChooseButton.UseVisualStyleBackColor = true;
            this.ChooseButton.Click += new System.EventHandler(this.ChooseButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(380, 215);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ChooseTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 250);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ChooseButton);
            this.Controls.Add(this.HistoriesListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GangTaoTypeComboBox);
            this.Controls.Add(this.WeldingItemComboBox);
            this.MinimumSize = new System.Drawing.Size(410, 250);
            this.Name = "ChooseTemplateForm";
            this.ShowIcon = false;
            this.Text = "选择焊接模板";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox WeldingItemComboBox;
        private System.Windows.Forms.ComboBox GangTaoTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox HistoriesListBox;
        private System.Windows.Forms.Button ChooseButton;
        private System.Windows.Forms.Button CancelButton;
    }
}