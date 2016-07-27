using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Welding_Recorder
{
    public partial class MainForm : Form
    {
        private List<History> Histories { get; set; }

        public MainForm()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            historiesList.Items.Clear();
            var db = new DataProcess();
            Histories = db.HistoryList();
            Histories.ForEach((history) => {
                var name = history.Name.Trim();
                historiesList.Items.Add(name.Length == 0 ? "(未命名记录)" : name);
            });
            ShowHistory();
        }

        private void startNewDataRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new RecordForm(null);
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                UpdateUI();
            }
        }

        private void historiesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Currently only support single selection.
            ShowHistory();
        }
        
        private void ShowHistory()
        {
            if (historiesList.SelectedIndices.Count == 0)
            {
                historyDetailTextBox.Text = "请选择一条历史记录。";
            }
            else
            {
                var index = historiesList.SelectedIndex;
                if (index >= Histories.Count) // Invalid index.
                {
                    return; 
                }
                var history = Histories[index];
                
                historyDetailTextBox.Text = history.ToString();
            }
        }

        private void EditHistoryButton_Click(object sender, EventArgs e)
        {
            int index = historiesList.SelectedIndex;
            if (index != -1 && historiesList.SelectedIndices.Count == 1)
            {
                var history = Histories[index];
                var editForm = new EditHistoryForm(history);
                editForm.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(this, "请选择一条焊接记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deleteHistoryButton_Click(object sender, EventArgs e)
        {
            int index = historiesList.SelectedIndex;
            if (index != -1 && historiesList.SelectedIndices.Count == 1)
            {
                var result = MessageBox.Show(this, "即将删除本条焊接记录，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    var history = Histories[index];
                    history.Delete();
                    UpdateUI();
                }
            }
            else
            {
                MessageBox.Show(this, "请选择一条焊接记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
