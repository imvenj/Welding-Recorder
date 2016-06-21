using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var form = new RecordForm();
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

        private void newRecordButton_Click(object sender, EventArgs e)
        {
            if (historiesList.SelectedIndices.Count == 0)
            {
                MessageBox.Show(this, "要开始自动焊接，请首先选择一条历史记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // TODO: Start Control.
                var index = historiesList.SelectedIndex;
                var history = Histories[index];
                var weldingForm = new WeldingControlForm(history);
                var result = weldingForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    // Do more
                }
            }
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

        private void historiesList_DoubleClick(object sender, EventArgs e)
        {
            newRecordButton_Click(null, e);
        }
    }
}
