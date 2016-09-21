using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Welding_Recorder
{
    public partial class TemplateOptionsForm : Form
    {
        private List<History> histories;
        private Template template;
        
        public History ChosenHistory { get; private set; }

        public TemplateOptionsForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        public TemplateOptionsForm(Template tpl) : this()
        {
            template = tpl;

            var typeIndex = GangTaoTypeComboBox.Items.IndexOf(template.Type);
            GangTaoTypeComboBox.SelectedIndex = typeIndex;
            var itemIndex = WeldingItemComboBox.Items.IndexOf(template.Item);
            WeldingItemComboBox.SelectedIndex = itemIndex;
            var history = (from h in histories where h.Id == template.History.Id select h).ToList().First();
            var historyIndex = histories.IndexOf(history);
            HistoriesListBox.SelectedIndex = historyIndex;
        }

        private void InitializeUI()
        {
            LoadWeldingDataLists();
        }

        private void GangTaoTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHistoryListSelection();
        }

        private void WeldingItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHistoryListSelection();
        }

        private void UpdateHistoryListSelection()
        {
            //var type = GangTaoTypeComboBox.SelectedText;
            //var item = WeldingItemComboBox.SelectedText;
            //var db = new DataProcess();
        }

        private void LoadWeldingDataLists()
        {
            var db = new DataProcess();
            var gangtaoList = db.GangTaoList();
            GangTaoTypeComboBox.Items.Clear();
            gangtaoList.ForEach((item) => {
                GangTaoTypeComboBox.Items.Add(item);
            });
            var weldingItemList = db.WeldingItemList();
            WeldingItemComboBox.Items.Clear();
            weldingItemList.ForEach((item) => {
                WeldingItemComboBox.Items.Add(item);
            });
            histories = db.HistoryList();
            HistoriesListBox.Items.Clear();
            histories.ForEach((item) => {
                HistoriesListBox.Items.Add(item.ShortDescription());
            });
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            var type = GangTaoTypeComboBox.Text;
            var item = WeldingItemComboBox.Text;
            if (type == "")
            {
                MessageBox.Show(this, "请选择一个规格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (item == "")
            {
                MessageBox.Show(this, "请选择一个焊接项目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var index = HistoriesListBox.SelectedIndex;
            if (index < 0 || index >= histories.Count())
            {
                MessageBox.Show(this, "请选择一个历史纪录作为模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ChosenHistory = histories[index];
            var message = string.Format("即将使用“{0}”作为{1}({2})的焊接模板，是否正确？", ChosenHistory.ShortDescription(), item, type);
            var result = MessageBox.Show(this, message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //Save result and go.
                if (template != null)
                {
                    template.Type = type;
                    template.Item = item;
                    template.History = ChosenHistory;
                    template.Save();
                }
                else
                {
                    var dict = new Dictionary<string, object>();
                    dict["type"] = type;
                    dict["item"] = item;
                    var tpl = new Template(dict);
                    tpl.History = ChosenHistory;
                    tpl.Save();
                }
                DialogResult = DialogResult.OK;
            }
        }
    }
}
