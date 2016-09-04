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
    public partial class EditTemplateForm : Form
    {

        private List<Template> SortedTemplates { get; set; }

        public EditTemplateForm()
        {
            InitializeComponent();
            PopulateListViewContent();
            //SortedTemplates = new List<Template>();
        }


        private void PopulateListViewContent()
        {
            TemplateListView.Columns.Add("", 40);
            TemplateListView.Columns.Add("焊接项目", 100);
            TemplateListView.Columns.Add("缸套规格", 100);
            TemplateListView.Columns.Add("模板", 500);
            TemplateListView.View = View.Details;
            TemplateListView.GridLines = true;
            TemplateListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            TemplateListView.FullRowSelect = true;
            TemplateListView.CheckBoxes = true;

            PopulateData();
        }

        private void PopulateData()
        {
            var db = new DataProcess();
            var templates = db.TemplateList();
            var sortedTemplates = from tpl in templates orderby tpl.Item, int.Parse(tpl.Type) select tpl;
            SortedTemplates = sortedTemplates.ToList();
            SortedTemplates.ForEach((tpl) =>
            {
                TemplateListView.Items.Add(tpl.ToListItem());
            });
            TemplateListLabel.Text = string.Format("焊接模板(共{0}条)", SortedTemplates.Count());
        }

        private void ReloadData()
        {
            TemplateListView.Items.Clear();
            PopulateData();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var chooseTemplate = new TemplateOptionsForm();
            var result = chooseTemplate.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ReloadData();
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var indices = TemplateListView.CheckedIndices;
            if (indices.Count <= 0)
            {
                MessageBox.Show(this, "未勾选任何模板条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(this, "即将删除你勾选的模板条目，模板条目删除后将无法恢复。\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }

            foreach (var index in indices)
            {
                var template = SortedTemplates[int.Parse(index.ToString())];
                template.Delete();
            }
            ReloadData();
            MessageBox.Show(this, "操作成功！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var indices = TemplateListView.CheckedIndices;
            if (indices.Count != 1)
            {
                MessageBox.Show(this, "请勾选一个（只能选一个）模板条目。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var index = indices[0];
            var template = SortedTemplates[int.Parse(index.ToString())];

            var editTemplate = new TemplateOptionsForm(template);
            var result = editTemplate.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ReloadData();
            }
        }
    }
}
