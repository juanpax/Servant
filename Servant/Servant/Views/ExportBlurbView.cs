using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Servant.Views
{
    public partial class ExportBlurbView : Form
    {
        public List<string[]> BLURBLIST = new List<string[]>();

        public ExportBlurbView(List<string[]> BLURBLIST, Color backgroundColor)
        {
            this.BLURBLIST = BLURBLIST;
            InitializeComponent();
            panelMain.BackColor = backgroundColor;
        }

        private void ExportBlurbView_Load(object sender, EventArgs e)
        {
            listView.Items.Clear();

            foreach (string[] blurb in BLURBLIST)
            {
                ListViewItem item = new ListViewItem(blurb);
                item.Font = new Font(item.Font, FontStyle.Regular);
                listView.Items.Add(item);
            }
        }

        private void buttonFileLocation_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    buttonFileLocation.Text = fbd.SelectedPath + "\\Servant.txt";
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (buttonFileLocation.Text != "Select file location..." && listView.CheckedItems.Count > 0)
            {
                string fileString = "";

                foreach (ListViewItem item in listView.CheckedItems)
                {
                    if (item.Checked)
                    {
                        fileString += item.SubItems[1].Text + "\n";
                        fileString += item.SubItems[2].Text + "\n";
                        fileString += item.SubItems[3].Text + "\n";
                        fileString += "================================================================= \n";
                    }
                }
                //Validate if the file already exist, so replace it 
                File.AppendAllText(buttonFileLocation.Text, fileString);
                labelLog.Text = "Blurbs successfully exported in " + buttonFileLocation.Text;
            }
            else
            {
                MessageBox.Show("Please fill all the required information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = checkBoxSelectAll.Checked;
            }
        }
    }
}
