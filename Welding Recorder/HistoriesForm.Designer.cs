namespace Welding_Recorder
{
    partial class HistoriesForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RenameButton = new System.Windows.Forms.Button();
            this.deleteHistoryButton = new System.Windows.Forms.Button();
            this.historiesList = new System.Windows.Forms.ListBox();
            this.historiesLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.EditHistoryButton = new System.Windows.Forms.Button();
            this.historyDetailTextBox = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.RenameButton);
            this.splitContainer1.Panel1.Controls.Add(this.deleteHistoryButton);
            this.splitContainer1.Panel1.Controls.Add(this.historiesList);
            this.splitContainer1.Panel1.Controls.Add(this.historiesLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CloseButton);
            this.splitContainer1.Panel2.Controls.Add(this.EditHistoryButton);
            this.splitContainer1.Panel2.Controls.Add(this.historyDetailTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(638, 363);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.TabIndex = 1;
            // 
            // RenameButton
            // 
            this.RenameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RenameButton.Location = new System.Drawing.Point(4, 337);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(75, 23);
            this.RenameButton.TabIndex = 3;
            this.RenameButton.Text = "重命名";
            this.RenameButton.UseVisualStyleBackColor = true;
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // deleteHistoryButton
            // 
            this.deleteHistoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteHistoryButton.Location = new System.Drawing.Point(87, 337);
            this.deleteHistoryButton.Name = "deleteHistoryButton";
            this.deleteHistoryButton.Size = new System.Drawing.Size(75, 23);
            this.deleteHistoryButton.TabIndex = 2;
            this.deleteHistoryButton.Text = "删除";
            this.deleteHistoryButton.UseVisualStyleBackColor = true;
            this.deleteHistoryButton.Click += new System.EventHandler(this.deleteHistoryButton_Click);
            // 
            // historiesList
            // 
            this.historiesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.historiesList.FormattingEnabled = true;
            this.historiesList.ItemHeight = 12;
            this.historiesList.Location = new System.Drawing.Point(4, 20);
            this.historiesList.Name = "historiesList";
            this.historiesList.Size = new System.Drawing.Size(158, 304);
            this.historiesList.TabIndex = 1;
            this.historiesList.SelectedIndexChanged += new System.EventHandler(this.historiesList_SelectedIndexChanged);
            // 
            // historiesLabel
            // 
            this.historiesLabel.AutoSize = true;
            this.historiesLabel.Location = new System.Drawing.Point(4, 4);
            this.historiesLabel.Name = "historiesLabel";
            this.historiesLabel.Size = new System.Drawing.Size(53, 12);
            this.historiesLabel.TabIndex = 0;
            this.historiesLabel.Text = "记录列表";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(391, 337);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = " 关闭";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // EditHistoryButton
            // 
            this.EditHistoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditHistoryButton.Location = new System.Drawing.Point(299, 337);
            this.EditHistoryButton.Name = "EditHistoryButton";
            this.EditHistoryButton.Size = new System.Drawing.Size(86, 23);
            this.EditHistoryButton.TabIndex = 4;
            this.EditHistoryButton.Text = "编辑焊接信号";
            this.EditHistoryButton.UseVisualStyleBackColor = true;
            this.EditHistoryButton.Click += new System.EventHandler(this.EditHistoryButton_Click);
            // 
            // historyDetailTextBox
            // 
            this.historyDetailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.historyDetailTextBox.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.historyDetailTextBox.Location = new System.Drawing.Point(4, 4);
            this.historyDetailTextBox.Multiline = true;
            this.historyDetailTextBox.Name = "historyDetailTextBox";
            this.historyDetailTextBox.ReadOnly = true;
            this.historyDetailTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.historyDetailTextBox.Size = new System.Drawing.Size(462, 327);
            this.historyDetailTextBox.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 387);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // HistoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 387);
            this.ControlBox = false;
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "HistoriesForm";
            this.Text = "焊接历史";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox historiesList;
        private System.Windows.Forms.Label historiesLabel;
        private System.Windows.Forms.Button deleteHistoryButton;
        private System.Windows.Forms.TextBox historyDetailTextBox;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button EditHistoryButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button RenameButton;
    }
}

