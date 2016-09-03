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
    public partial class OperatorsForm : Form
    {
        public OperatorsForm()
        {
            InitializeComponent();
            LoadOperatorsList();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var input = new InputBox("请输入一个名字：", "新增操作者", "");
            var result = input.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var name = input.InputResult.Trim();
                bool nameExists = false;
                foreach (string n in OperatorsListBox.Items) {
                    if (n == name)
                    {
                        nameExists = true;
                        break;
                    }
                }

                if (!nameExists)
                {
                    var db = new DataProcess();
                    db.addOperator(name);
                    OperatorsListBox.Items.Add(name);
                }
                else
                {
                    MessageBox.Show(this, "名字已存在，请勿重复添加。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void LoadOperatorsList()
        {
            OperatorsListBox.Items.Clear();
            var db = new DataProcess();
            var operatorList = db.OperatorList();
            operatorList.ForEach((item) => {
                OperatorsListBox.Items.Add(item);
            });
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var name = OperatorsListBox.SelectedItem;
            if (name != null)
            {
                var db = new DataProcess();
                db.deleteOperator(name.ToString());
                LoadOperatorsList();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
