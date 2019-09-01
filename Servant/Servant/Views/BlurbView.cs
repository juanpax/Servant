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
            string text = richTextBoxText.Rtf;

            if (ValidateBlurb(pattern, text))
            {
                bool result = BlurbController.SaveBlurb(id, pattern, text);

                if (result)
                {
                    string[] newBlurb = BlurbController.GetBlurb(pattern);
                    id = newBlurb[0];

                    MessageBox.Show("Item added successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Threre was an error adding the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the required information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            richTextBoxText.Rtf = "";
        }

        private bool ValidateBlurb(string pattern, string text)
        {
            return (!string.IsNullOrEmpty(pattern) && !string.IsNullOrEmpty(text));
        }
    }
}
