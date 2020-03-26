using Servant.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Servant
{
    public partial class BlurbListView : Form
    {
        // List of all the blurb created by the user 
        public static List<string[]> BLURBLIST = new List<string[]>();
        public Dictionary<string, Color> colors = new Dictionary<string, Color>();

        /// <summary>
        /// Class constructor
        /// </summary>
        public BlurbListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method when the Blurb window is closed
        /// </summary>
        private void BlurbView_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadBlurbList();
        }

        /// <summary>
        /// Event when the window loads 
        /// </summary>
        private void BlurbListView_Load(object sender, EventArgs e)
        {
            colors.Add("SteelBlue", Color.SteelBlue);
            colors.Add("Firebrick", Color.Firebrick);
            colors.Add("BurlyWood", Color.BurlyWood);
            colors.Add("Olive", Color.Olive);
            colors.Add("Teal", Color.Teal);
            colors.Add("DarkSlateBlue", Color.DarkSlateBlue);
            colors.Add("Purple", Color.Purple);

            string backgroundColor = ConfigurationManager.AppSettings["BackgroundColor"];
            panelMain.BackColor = colors[backgroundColor];
            LoadBlurbList();
        }

        /// <summary>
        /// Event to pop up the context menu strip once the user right click 
        /// </summary>
        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStripBlurb.Show(Cursor.Position);
                }
            }
        }

        /// <summary>
        /// Event when the user click the Add Blurb button
        /// </summary>
        private void buttonAddBlurb_Click(object sender, EventArgs e)
        {
            BlurbView newBlurb = new BlurbView(panelMain.BackColor);
            newBlurb.comboBoxFormat.SelectedIndex = newBlurb.comboBoxFormat.FindStringExact("Plain Text");
            InitBlurbView(newBlurb, "Add blurb");
        }

        /// <summary>
        /// Method to init a new blurb including the parameters 
        /// </summary>
        private void InitBlurbView(BlurbView newBlurb, string windowTitle)
        {
            newBlurb.FormClosed += new FormClosedEventHandler(BlurbView_FormClosed);
            newBlurb.ShowDialog();
        }

        /// <summary>
        /// Event when the user double click one of the blurb 
        /// </summary>
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditBlurb();
        }

        /// <summary>
        /// Event when the edit button is clicked 
        /// </summary>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditBlurb();
        }

        /// <summary>
        /// Event when the delete button is clicked 
        /// </summary>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete this item?", "Delete confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                string id = listView.SelectedItems[0].SubItems[4].Text;
                bool result = BlurbController.DeleteBlurb(id);

                if (result)
                {
                    MessageBox.Show("Item deleted successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Threre was an error deleting the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadBlurbList();
            }
        }

        /// <summary>
        /// Method to load the list of blurb 
        /// </summary>
        private void LoadBlurbList()
        {
            listView.Items.Clear();
            BLURBLIST = BlurbController.GetBlurbList();

            foreach (string[] blurb in BLURBLIST)
            {
                ListViewItem item = new ListViewItem(blurb);
                item.Font = new Font(item.Font, FontStyle.Regular);
                listView.Items.Add(item);
            }
        }

        /// <summary>
        /// Method to send the new values for the editing blurb
        /// </summary>
        private void EditBlurb()
        {
            BlurbView newBlurb = new BlurbView(panelMain.BackColor);
            string format = listView.SelectedItems[0].SubItems[2].Text;
            newBlurb.BlurbId = listView.SelectedItems[0].SubItems[4].Text;
            newBlurb.textBoxPattern.Text = listView.SelectedItems[0].SubItems[1].Text;
            newBlurb.comboBoxFormat.SelectedIndex = newBlurb.comboBoxFormat.FindStringExact(format);

            if (format == "Plain Text")
            {
                newBlurb.richTextBoxText.Text = listView.SelectedItems[0].SubItems[3].Text;
            }
            else if (format == "Rich Text Format (RTF)")
            {
                newBlurb.richTextBoxText.Rtf = listView.SelectedItems[0].SubItems[3].Text;
            }

            InitBlurbView(newBlurb, "Edit blurb");
        }

        private void buttonColors_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            contextMenuStripColors.Show(ptLowerLeft);
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedMenuItem = sender as ToolStripMenuItem;
            string color = clickedMenuItem.Text;
            panelMain.BackColor = colors[color];

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["BackgroundColor"].Value = color;
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (BLURBLIST.Count > 0)
            {
                ExportBlurbView exportBlurb = new ExportBlurbView(BLURBLIST, panelMain.BackColor);
                exportBlurb.ShowDialog();
            }
            else
            {
                MessageBox.Show("There are no Blurbs to export", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (theDialog.OpenFile() != null)
                    {
                        string line1;
                        StreamReader file = new StreamReader(theDialog.FileName);
                        int failedImportedBlurbs = 0;

                        while ((line1 = file.ReadLine()) != null)
                        {
                            string line2 = file.ReadLine().Trim();
                            string line3 = file.ReadLine();
                            // ACORDARSE DE LA LINEA DE ======================

                            bool result = BlurbController.SaveBlurb("", line1, line2, line3);
                            failedImportedBlurbs += (!result) ? 1 : 0;
                        }

                        file.Close();

                        if (failedImportedBlurbs == 0)
                        {
                            MessageBox.Show("All items were added successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("There was an error adding some items. Please check Servant file content is correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Method to validate if the blurb information is completed 
        /// </summary>
        private bool ValidateBlurb(string pattern, string format, string text)
        {
            return (!string.IsNullOrEmpty(pattern) && !string.IsNullOrEmpty(format) && (format == "Plain Text" || format == "Rich Text Format (RTF)") && !string.IsNullOrEmpty(text));
        }
    }
}
