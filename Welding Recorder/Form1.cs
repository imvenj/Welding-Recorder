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
        public MainForm()
        {
            InitializeComponent();
            InitializeDataBase();
        }

        private void InitializeDataBase()
        {
            var db = new DataProcess();
        }

        private void startNewDataRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new RecordForm();
            var result = form.ShowDialog(this);
        }
    }
}
