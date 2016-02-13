namespace Welding_Recorder
{
    partial class MainForm
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
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.创建新纪录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNewControlProcedureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNewDataRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.historiesLabel = new System.Windows.Forms.Label();
            this.historiesList = new System.Windows.Forms.ListBox();
            this.newRecordButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.创建新纪录ToolStripMenuItem,
            this.加载ToolStripMenuItem,
            this.startNewDataRecordToolStripMenuItem,
            this.startNewControlProcedureToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.文件ToolStripMenuItem.Text = "File";
            // 
            // 创建新纪录ToolStripMenuItem
            // 
            this.创建新纪录ToolStripMenuItem.Name = "创建新纪录ToolStripMenuItem";
            this.创建新纪录ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.创建新纪录ToolStripMenuItem.Text = "New Database...";
            // 
            // 加载ToolStripMenuItem
            // 
            this.加载ToolStripMenuItem.Name = "加载ToolStripMenuItem";
            this.加载ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.加载ToolStripMenuItem.Text = "Load Database...";
            // 
            // preferenceToolStripMenuItem
            // 
            this.preferenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.preferenceToolStripMenuItem.Name = "preferenceToolStripMenuItem";
            this.preferenceToolStripMenuItem.Size = new System.Drawing.Size(82, 21);
            this.preferenceToolStripMenuItem.Text = "Preference";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.editToolStripMenuItem,
            this.preferenceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(656, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // startNewControlProcedureToolStripMenuItem
            // 
            this.startNewControlProcedureToolStripMenuItem.Name = "startNewControlProcedureToolStripMenuItem";
            this.startNewControlProcedureToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.startNewControlProcedureToolStripMenuItem.Text = "Start control...";
            // 
            // startNewDataRecordToolStripMenuItem
            // 
            this.startNewDataRecordToolStripMenuItem.Name = "startNewDataRecordToolStripMenuItem";
            this.startNewDataRecordToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.startNewDataRecordToolStripMenuItem.Text = "Start record...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.newRecordButton);
            this.splitContainer1.Panel1.Controls.Add(this.historiesList);
            this.splitContainer1.Panel1.Controls.Add(this.historiesLabel);
            this.splitContainer1.Size = new System.Drawing.Size(656, 344);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 1;
            // 
            // historiesLabel
            // 
            this.historiesLabel.AutoSize = true;
            this.historiesLabel.Location = new System.Drawing.Point(4, 4);
            this.historiesLabel.Name = "historiesLabel";
            this.historiesLabel.Size = new System.Drawing.Size(59, 12);
            this.historiesLabel.TabIndex = 0;
            this.historiesLabel.Text = "Histories";
            // 
            // historiesList
            // 
            this.historiesList.FormattingEnabled = true;
            this.historiesList.ItemHeight = 12;
            this.historiesList.Location = new System.Drawing.Point(4, 20);
            this.historiesList.Name = "historiesList";
            this.historiesList.Size = new System.Drawing.Size(163, 280);
            this.historiesList.TabIndex = 1;
            // 
            // newRecordButton
            // 
            this.newRecordButton.Location = new System.Drawing.Point(6, 309);
            this.newRecordButton.Name = "newRecordButton";
            this.newRecordButton.Size = new System.Drawing.Size(68, 23);
            this.newRecordButton.TabIndex = 2;
            this.newRecordButton.Text = "New";
            this.newRecordButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 369);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Welding Recorder";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 创建新纪录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startNewDataRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startNewControlProcedureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button newRecordButton;
        private System.Windows.Forms.ListBox historiesList;
        private System.Windows.Forms.Label historiesLabel;
    }
}

