using System;
using System.Drawing;
using System.Windows.Forms;

namespace Servant
{
    public partial class BlurbView : Form
    {
        // This id the blurb id and it is going to be used to validate if the user is adding a new blurb or updating the information of one of the current ones.
        public string BlurbId = "";

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
            string text = (format == "Plain Text") ? richTextBoxText.Text : richTextBoxText.Rtf;

            if (ValidateBlurb(pattern, format, text))
            {
                bool result = BlurbController.SaveBlurb(BlurbId, pattern, format, text);

                if (result)
                {
                    DialogResult dialogResult = MessageBox.Show("Item added successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dialogResult == DialogResult.OK)
                    {
                        Close();
                    }
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
            new ToolTip().SetToolTip(pictureBoxPattern, "The pattern will be the combination of keys Servant is going to be looking for before pasting the Blurb content.");
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

        /// <summary>
        /// Method to get the paste event and transform text to understandable format. 
        /// This is intended to fix the issue when copying the text from OneNote into Servant
        /// </summary>
        private void richTextBoxText_KeyDown(object sender, KeyEventArgs e)
        {
            bool ctrlV = e.Modifiers == Keys.Control && e.KeyCode == Keys.V;
            bool shiftIns = e.Modifiers == Keys.Shift && e.KeyCode == Keys.Insert;

            if (ctrlV || shiftIns)
            {
                string textAsRTF = Clipboard.GetText(TextDataFormat.Rtf);

                if (textAsRTF == "")
                {
                    Clipboard.SetText(Clipboard.GetText());
                }
            }
        }

        /// <summary>
        /// Method to apply Bold, Italic, Underline and Strikeout styles over the text
        /// </summary>
        private void buttonApplyStyle_Click(object sender, EventArgs e)
        {
            string style = ((Button)sender).Name;
            FontStyle fontStyle =
                (style == "Bold") ? FontStyle.Bold :
                (style == "Italic") ? FontStyle.Italic :
                (style == "Underline") ? FontStyle.Underline :
                (style == "Strikeout") ? FontStyle.Strikeout : 
                FontStyle.Regular;

            string currentStyle = richTextBoxText.SelectionFont.Style.ToString();

            Font newFont = 
                (currentStyle.Contains(style)) ? new Font(richTextBoxText.Font, richTextBoxText.SelectionFont.Style & ~fontStyle) :
                new Font(richTextBoxText.Font, richTextBoxText.SelectionFont.Style | fontStyle);

            int selstart = richTextBoxText.SelectionStart;
            int sellength = richTextBoxText.SelectionLength;

            richTextBoxText.SelectionFont = newFont;
            richTextBoxText.SelectionStart = richTextBoxText.SelectionStart + richTextBoxText.SelectionLength;
            richTextBoxText.SelectionLength = 0;
            richTextBoxText.SelectionFont = richTextBoxText.Font;
            richTextBoxText.Select(selstart, sellength);
        }
    }
}

