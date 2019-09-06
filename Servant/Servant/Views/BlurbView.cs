using System;
using System.Windows.Forms;

namespace Servant
{
    public partial class BlurbView : Form
    {
        // This id the blurb id and it is going to be used to validate if the user is adding a new blurb or updating the information of one of the current ones.
        public string id = "";

        /// <summary>
        /// Class constructor
        /// </summary>
        public BlurbView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event when the save button is clicked
        /// </summary>
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

        /// <summary>
        /// Event when the reset button is clicked
        /// </summary>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            richTextBoxText.Rtf = "";
        }

        /// <summary>
        /// Event mouse over the pattern information icon
        /// </summary>
        private void pictureBoxPattern_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(pictureBoxPattern, "The pattern will be the combination of keys Servant is going to be looking for before pasting the Blurb content. Please do not combine capital and lowercase letters in the same pattern."); 
        }

        /// <summary>
        /// Event mouse over the format information icon
        /// </summary>
        private void pictureBoxFormat_Click(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(pictureBoxFormat, "Select the format of the text you want to save. RTF: Word or Outlook. Plain text: Any other kind of text editor."); 
        }

        /// <summary>
        /// Method to validate if the blurb information is completed 
        /// </summary>
        private bool ValidateBlurb(string pattern, string format, string text)
        {
            return (!string.IsNullOrEmpty(pattern) && !string.IsNullOrEmpty(format) && !string.IsNullOrEmpty(text));
        }
    }
}
