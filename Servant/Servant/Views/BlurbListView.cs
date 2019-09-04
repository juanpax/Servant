using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Servant
{
    public partial class BlurbListView : Form
    {
        // List of all the blurb created by the user 
        public static List<string[]> BLURBLIST = BlurbController.GetBlurbList();

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
            BlurbView newBlurb = new BlurbView();
            InitBlurbView(newBlurb, "Add blurb");
        }

        /// <summary>
        /// Method to init a new blurb including the parameters 
        /// </summary>
        private void InitBlurbView(BlurbView newBlurb, string windowTitle)
        {
            newBlurb.labelTitle.Text = windowTitle;
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
                string id = listView.SelectedItems[0].SubItems[0].Text;
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
            BLURBLIST = BlurbController.GetBlurbList();
            listView.Items.Clear();

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
            BlurbView newBlurb = new BlurbView();
            newBlurb.id = listView.SelectedItems[0].SubItems[0].Text;
            newBlurb.textBoxPattern.Text = listView.SelectedItems[0].SubItems[1].Text;
            newBlurb.comboBoxFormat.SelectedIndex = newBlurb.comboBoxFormat.FindStringExact(listView.SelectedItems[0].SubItems[2].Text);
            newBlurb.richTextBoxText.Rtf = listView.SelectedItems[0].SubItems[3].Text;
            InitBlurbView(newBlurb, "Edit blurb");
        }
    }
}
