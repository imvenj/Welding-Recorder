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
    public partial class ChooseTemplate : Form
    {
        private List<Template> SortedTemplates { get; set; }
        public Template ChosenTemplate;

        public ChooseTemplate()
        {
            InitializeComponent();
            PopulateData();
        }

        private void PopulateData()
        {
            var db = new DataProcess();
            var templates = db.TemplateList();
            var sortedTemplates = from tpl in templates orderby tpl.Item, int.Parse(tpl.Type) select tpl;
            SortedTemplates = sortedTemplates.ToList();
            TemplatesListBox.Items.Clear();
            SortedTemplates.ForEach((template) => {
                TemplatesListBox.Items.Add(template.ShortDescription());
            });
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            var index = TemplatesListBox.SelectedIndex;
            if (index >=0 && index < SortedTemplates.Count())
            {
                ChosenTemplate = SortedTemplates[index];
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(this, "未选择任何模板。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ChooseTemplate_Shown(object sender, EventArgs e)
        {
            if (SortedTemplates.Count() == 0)
            {
                if (MessageBox.Show(this, "你还没有创建任何模板，请通过 “编辑” - “管理焊接模板” 菜单，创建焊接模板之后再使用模板焊接功能。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
    }
}
