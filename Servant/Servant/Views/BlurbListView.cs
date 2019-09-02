using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Servant
{
    public partial class BlurbListView : Form
    {
        // document this class 

        public static List<string[]> BLURBLIST = BlurbController.GetBlurbList();

        public BlurbListView()
        {
            InitializeComponent();
        }

        // This method is going to refresh the BLURBLIST in general when the Blurb window is closed
        private void BlurbView_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadBlurbList();
        }

        private void buttonAddBlurb_Click(object sender, EventArgs e)
        {
            BlurbView newBlurb = new BlurbView();
            InitBlurbView(newBlurb, "Add blurb");
        }

        private void BlurbListView_Load(object sender, EventArgs e)
        {
            LoadBlurbList();
        }

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

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditBlurb();
        }

        private void InitBlurbView(BlurbView newBlurb, string windowTitle)
        {
            newBlurb.labelTitle.Text = windowTitle;
            newBlurb.FormClosed += new FormClosedEventHandler(BlurbView_FormClosed);
            newBlurb.ShowDialog();
        }

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

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditBlurb();
        }

        private void EditBlurb()
        {
            BlurbView newBlurb = new BlurbView();
            newBlurb.id = listView.SelectedItems[0].SubItems[0].Text;
            newBlurb.textBoxPattern.Text = listView.SelectedItems[0].SubItems[1].Text;
            newBlurb.comboBoxFormat.SelectedIndex = newBlurb.comboBoxFormat.FindStringExact(listView.SelectedItems[0].SubItems[2].Text);
            newBlurb.richTextBoxText.Rtf = listView.SelectedItems[0].SubItems[3].Text;
            InitBlurbView(newBlurb, "Edit blurb");
        }

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
    }
}
