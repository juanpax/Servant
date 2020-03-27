using Servant.Extra;
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
        public BlurbView(Color backgroundColor)
        {
            InitializeComponent();
            panelMain.BackColor = backgroundColor;
        }

        /// <summary>
        /// Method to load default values over the BlurbView
        /// </summary>
        private void BlurbView_Load(object sender, EventArgs e)
        {
            comboBoxFont.DataSource = FontFamily.Families;
            comboBoxFont.SelectedIndex = comboBoxFont.FindStringExact("Calibri");
            comboBoxFontSize.SelectedItem = "11";
        }

        /// <summary>
        /// Method to draw each font family value with their own font family style
        /// </summary>
        private void comboBoxFont_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            FontFamily fontFamily = (FontFamily)comboBox.Items[e.Index];
            Font font = new Font(fontFamily, comboBox.Font.SizeInPoints);

            e.DrawBackground();
            e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
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
        /// Method to apply Bold, Italic, Underline and Strikeout styles over selected text
        /// </summary>
        private void buttonApplyStyle_Click(object sender, EventArgs e)
        {
            string styleButton = ((Button)sender).Name;
            FontStyle fontStyle =
                (styleButton == "Bold") ? FontStyle.Bold :
                (styleButton == "Italic") ? FontStyle.Italic :
                (styleButton == "Underline") ? FontStyle.Underline :
                (styleButton == "Strikeout") ? FontStyle.Strikeout :
                FontStyle.Regular;

            string currentStyle = richTextBoxText.SelectionFont.Style.ToString();

            Font newFont =
                (currentStyle.Contains(styleButton)) ? new Font(richTextBoxText.SelectionFont, richTextBoxText.SelectionFont.Style & ~fontStyle) :
                new Font(richTextBoxText.SelectionFont, richTextBoxText.SelectionFont.Style | fontStyle);

            richTextBoxText.SelectionFont = newFont;
            ApplyOverSelection();
        }

        /// <summary>
        /// Method to apply left, center, right and justify over selected text
        /// </summary>
        private void buttonApplyAlignment_Click(object sender, EventArgs e)
        {
            string alignmentButton = ((Button)sender).Name;
            string currentAlignment = richTextBoxText.SelectionAlignment.ToString();

            HorizontalAlignment horizontalAlignment =
                (alignmentButton == "Left") ? HorizontalAlignment.Left :
                (alignmentButton == "Center" && currentAlignment == "Center") ? HorizontalAlignment.Left :
                (alignmentButton == "Center") ? HorizontalAlignment.Center :
                (alignmentButton == "Right" && currentAlignment == "Right") ? HorizontalAlignment.Left :
                (alignmentButton == "Right") ? HorizontalAlignment.Right :
                HorizontalAlignment.Left;

            richTextBoxText.SelectionAlignment = horizontalAlignment;
            ApplyOverSelection();
        }

        /// <summary>
        /// Method to restrict only integers on the font size combobox 
        /// </summary>
        private void comboBoxFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Method to change font size over selected text 
        /// </summary>
        private void comboBoxFontSize_TextChanged(object sender, EventArgs e)
        {
            int emSize;
            if (int.TryParse(comboBoxFontSize.Text, out emSize))
            {
                richTextBoxText.SelectionFont = new Font(richTextBoxText.SelectionFont.FontFamily, emSize, richTextBoxText.Font.Style);
                ApplyOverSelection();
            }
        }

        /// <summary>
        /// Method to change font style over selected text
        /// </summary>
        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontFamily fontFamily = new FontFamily(comboBoxFont.Text);
            richTextBoxText.SelectionFont = new Font(fontFamily, richTextBoxText.SelectionFont.Size, richTextBoxText.Font.Style);
            ApplyOverSelection();
        }

        /// <summary>
        /// Method to apply color change on the selected text
        /// </summary>
        private void buttonFontColor_Click(object sender, EventArgs e)
        {
            Point point = buttonFontColor.PointToScreen(Point.Empty);

            using (ColorDialogExtension cd = new ColorDialogExtension(point.X - 5, point.Y + 28, "Color"))
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    richTextBoxText.SelectionColor = cd.Color;
                    panelColor.BackColor = cd.Color;
                    ApplyOverSelection();
                }
            }
        }

        /// <summary>
        /// Method to apply changes (style, alignment, font family and size) over selected text
        /// </summary>
        private void ApplyOverSelection()
        {
            int selstart = richTextBoxText.SelectionStart;
            int sellength = richTextBoxText.SelectionLength;

            richTextBoxText.SelectionStart = richTextBoxText.SelectionStart + richTextBoxText.SelectionLength;
            richTextBoxText.SelectionLength = 0;
            richTextBoxText.Select(selstart, sellength);
        }
    }
}

