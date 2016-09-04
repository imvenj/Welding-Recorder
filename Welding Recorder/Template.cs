using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Welding_Recorder
{
    public class Template
    {
        public long? Id { get; set; }
        public string Type { get; set; }
        public string Item { get; set; }
        public History History { get; set; }

        public Template(Dictionary<string, object> meta)
        {
            Type = (string)meta["type"];
            Item = (string)meta["item"];
        }

        public Template Save()
        {
            var db = new DataProcess();
            if (History == null)
            {
                throw new TemplateException("Template should have a history reference.");
            }
            else
            {
                if (Id == null)
                {
                    Id = db.saveTemplate(this);
                }
                else
                {
                    db.updateTemplate(this);
                }
                return this;
            }
        }
        
        public void Delete()
        {
            var db = new DataProcess();
            if (Id != null)
            {
                db.deleteTemplate(this);
            }
        }

        public ListViewItem ToListItem()
        {
            var row = new ListViewItem();
            row.SubItems.Add(Item);
            row.SubItems.Add(Type);
            row.SubItems.Add(History.ShortDescription());
            
            return row;
        }

        public override string ToString()
        {
            return string.Format("焊接“{0}({1})”的模板。", Item, Type);
        }

        public string ShortDescription()
        {
            return string.Format("{0}\t{1}\t{2}", Item, Type, History.ShortDescription());
        }

    }

    public class TemplateException : Exception
    {
        public TemplateException() : base() { }
        public TemplateException(string message) : base(message) { }
        public TemplateException(string message, Exception e) : base(message, e) { }
    }

}
