using System;
using System.Windows.Forms;

namespace Welding_Recorder
{
    public partial class InputBox : Form
    {
        private string Prompt { get; set; }
        private string Title { get; set; }
        private string DefaultResponse { get; set; }
        public string InputResult
        {
            get
            {
                return contentBox.Text;
            }
        }

        public InputBox(string prompt, string title, string defaultResponse) : this()
        {
            Prompt = prompt;
            Title = title;
            DefaultResponse = defaultResponse;
            // UI
            Text = title;
            promptLabel.Text = Prompt;
            contentBox.Text = defaultResponse;
        }

        public InputBox()
        {
            InitializeComponent();
            contentBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnter);
        }
        
        private void InputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            contentBox.SelectAll();
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
