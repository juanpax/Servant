using System;
using System.Windows.Forms;

namespace Servant
{
    public partial class BlurbView : Form
    {
        public string id = "";

        // document this class 

        public BlurbView()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string pattern = textBoxPattern.Text;
            string format = comboBoxFormat.GetItemText(comboBoxFormat.SelectedItem);
            string text = (format == "Plain Text") ? richTextBoxText.Text: richTextBoxText.Rtf;  

            if (ValidateBlurb(pattern, format, text))
            {
                bool result = BlurbController.SaveBlurb(id, pattern, format, text);

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

        private bool ValidateBlurb(string pattern, string format, string text)
        {
            return (!string.IsNullOrEmpty(pattern) && !string.IsNullOrEmpty(format) && !string.IsNullOrEmpty(text));
        }

        private void pictureBoxPattern_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(pictureBoxPattern, "something"); /// add content here 
        }

        private void pictureBoxFormat_Click(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(pictureBoxFormat, "something"); /// add content here 
        }
    }
}
