using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Welding_Recorder
{
    public partial class HistoriesForm : Form
    {
        private List<History> Histories { get; set; }
        private bool isAutoWeld;

        public HistoriesForm(bool autoWeld = false)
        {
            InitializeComponent();
            UpdateUI(autoWeld);
            isAutoWeld = autoWeld;
        }

        private void UpdateUI(bool autoWeld = false)
        {
            historiesList.Items.Clear();
            var db = new DataProcess();
            if (autoWeld)
            {
                var list = db.AutoWeldHistoryList();
                if (Histories == null)
                {
                    Histories = new List<History>();
                }
                else
                {
                    Histories.Clear();
                }
                list.ForEach((item) => {
                    Histories.Add(item);
                });
                Text = "控制记录";
            }
            else
            {
                Histories = db.HistoryList();
                Text = "焊接历史";
            }
            Histories.ForEach((history) => {
                var name = history.Name.Trim();
                var defaultName = autoWeld ? "(未命名控制记录)" : "(未命名焊接记录)";
                historiesList.Items.Add(name.Length == 0 ? defaultName : name);
            });
            ShowHistory();
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
                historyDetailTextBox.Text = "请选择一个列表项。";
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
                var editForm = new EditHistoryForm(history, isAutoWeld);
                var result = editForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ShowHistory();
                }
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
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            int index = historiesList.SelectedIndex;
            if (index != -1 && historiesList.SelectedIndices.Count == 1)
            {
                var history = Histories[index];
                var inputBox = new InputBox("请输入新的记录名", "重命名焊接记录", history.Name);
                var result = inputBox.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    history.Name = inputBox.InputResult;
                    history.Save();
                    historiesList.Items[index] = history.Name;
                    ShowHistory();
                }
            }
        }
    }
}
