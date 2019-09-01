using System;
using System.Windows.Forms;

namespace Servant
{
    public partial class BlurbView : Form
    {
        public string id = "";

        public BlurbView()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string pattern = textBoxPattern.Text;
            string text = richTextBoxText.Text;

            if (ValidateBlurb(pattern, text))
            {
                BlurbController.SaveBlurb(id, pattern, text);
            }
            else
            {
                MessageBox.Show("Please fill all the required information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            richTextBoxText.Text = "";
        }

        private bool ValidateBlurb(string pattern, string text)
        {
            return (!string.IsNullOrEmpty(pattern) && !string.IsNullOrEmpty(text));
        }
    }
}
