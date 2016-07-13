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
            this.SuspendLayout();
            // 
            // SignalsListView
            // 
            this.SignalsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SignalsListView.Location = new System.Drawing.Point(12, 46);
            this.SignalsListView.Name = "SignalsListView";
            this.SignalsListView.Size = new System.Drawing.Size(575, 367);
            this.SignalsListView.TabIndex = 1;
            this.SignalsListView.UseCompatibleStateImageBehavior = false;
            // 
            // DeleteSelectedItemsButton
            // 
            this.DeleteSelectedItemsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteSelectedItemsButton.Location = new System.Drawing.Point(12, 419);
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
            this.DoneButton.Location = new System.Drawing.Point(512, 419);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.TabIndex = 3;
            this.DoneButton.Text = "完成";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "请勾选你要删除的条目";
            // 
            // DeSelectAllButton
            // 
            this.DeSelectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeSelectAllButton.Location = new System.Drawing.Point(185, 419);
            this.DeSelectAllButton.Name = "DeSelectAllButton";
            this.DeSelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.DeSelectAllButton.TabIndex = 5;
            this.DeSelectAllButton.Text = "取消选择";
            this.DeSelectAllButton.UseVisualStyleBackColor = true;
            this.DeSelectAllButton.Click += new System.EventHandler(this.DeSelectAllButton_Click);
            // 
            // EditHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 454);
            this.Controls.Add(this.DeSelectAllButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.DeleteSelectedItemsButton);
            this.Controls.Add(this.SignalsListView);
            this.MaximizeBox = false;
            this.Name = "EditHistoryForm";
            this.Text = "EditHistoryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView SignalsListView;
        private System.Windows.Forms.Button DeleteSelectedItemsButton;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DeSelectAllButton;
    }
}