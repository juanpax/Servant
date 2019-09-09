using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Servant
{
    public partial class BlurbListView : Form
    {
        // List of all the blurb created by the user 
        public static List<string[]> BLURBLIST = new List<string[]>();

        // Random phrases dictionary
        private Dictionary<int, string> ServantPhrases = new Dictionary<int, string>();

        /// <summary>
        /// Class constructor
        /// </summary>
        public BlurbListView()
        {
            InitializeComponent();

            LoadRandomPhrases();
        }

        /// <summary>
        /// Method to load the list of random phrases
        /// </summary>
        private void LoadRandomPhrases()
        {
            ServantPhrases.Add(1, "No lo se Ricardo");
            ServantPhrases.Add(2, "La respuesta esta en tu corazon");
            ServantPhrases.Add(3, "Pa pa poom poom pra pra pra");
            ServantPhrases.Add(4, "Holly Molly!");
            ServantPhrases.Add(5, "Ahora no joven");
            ServantPhrases.Add(6, "OSCAR!");
            ServantPhrases.Add(7, "aagh, agh, aaaaahg....");
            ServantPhrases.Add(8, "10/10 doble puntaje");
            ServantPhrases.Add(9, "Me tenes arta!");
            ServantPhrases.Add(10, "Je je");
            ServantPhrases.Add(11, "HOLA JP!");
            ServantPhrases.Add(12, "OK");
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
            BlurbView newBlurb = new BlurbView();
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

        /// <summary>
        /// Event when the user click the Servant logo
        /// </summary>
        private void pictureBoxServantLogo_Click(object sender, EventArgs e)
        {
            int randomNumber = new Random().Next(1, 12);
            string randomPhrase = ServantPhrases[randomNumber];

            new ToolTip().SetToolTip(pictureBoxServantLogo, randomPhrase);
        }
    }
}
