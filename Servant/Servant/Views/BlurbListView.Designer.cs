namespace Servant
{
    partial class BlurbListView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlurbListView));
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelServantState = new System.Windows.Forms.Label();
            this.roundedButtonPlayPause = new Servant.Extra.RoundedButton();
            this.buttonAddBlurb = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonColor = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.contextMenuStripBlurb = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.contextMenuStripColors = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.steelBlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firebrickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.burlyWoodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oliveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkSlateBlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purpleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStripBlurb.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.contextMenuStripColors.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(23, 52);
            this.listView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(813, 314);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Pattern";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Format";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Text";
            this.columnHeader4.Width = 493;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.listView, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(859, 403);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelServantState);
            this.panel1.Controls.Add(this.roundedButtonPlayPause);
            this.panel1.Controls.Add(this.buttonAddBlurb);
            this.panel1.Location = new System.Drawing.Point(23, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 46);
            this.panel1.TabIndex = 1;
            // 
            // labelServantState
            // 
            this.labelServantState.AutoSize = true;
            this.labelServantState.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServantState.Location = new System.Drawing.Point(49, 18);
            this.labelServantState.Name = "labelServantState";
            this.labelServantState.Size = new System.Drawing.Size(306, 28);
            this.labelServantState.TabIndex = 3;
            this.labelServantState.Text = "Servant has been started!";
            // 
            // roundedButtonPlayPause
            // 
            this.roundedButtonPlayPause.BackgroundImage = global::Servant.Properties.Resources.pause;
            this.roundedButtonPlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.roundedButtonPlayPause.Dock = System.Windows.Forms.DockStyle.Left;
            this.roundedButtonPlayPause.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.roundedButtonPlayPause.FlatAppearance.BorderSize = 0;
            this.roundedButtonPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButtonPlayPause.Location = new System.Drawing.Point(0, 0);
            this.roundedButtonPlayPause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.roundedButtonPlayPause.Name = "roundedButtonPlayPause";
            this.roundedButtonPlayPause.Size = new System.Drawing.Size(49, 46);
            this.roundedButtonPlayPause.TabIndex = 2;
            this.roundedButtonPlayPause.Tag = "Pause";
            this.roundedButtonPlayPause.UseVisualStyleBackColor = true;
            // 
            // buttonAddBlurb
            // 
            this.buttonAddBlurb.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.buttonAddBlurb.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonAddBlurb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddBlurb.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddBlurb.ForeColor = System.Drawing.Color.White;
            this.buttonAddBlurb.Location = new System.Drawing.Point(682, 0);
            this.buttonAddBlurb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddBlurb.Name = "buttonAddBlurb";
            this.buttonAddBlurb.Size = new System.Drawing.Size(131, 46);
            this.buttonAddBlurb.TabIndex = 1;
            this.buttonAddBlurb.Text = "Add blurb";
            this.buttonAddBlurb.UseVisualStyleBackColor = false;
            this.buttonAddBlurb.Click += new System.EventHandler(this.buttonAddBlurb_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonColor);
            this.panel2.Controls.Add(this.buttonImport);
            this.panel2.Controls.Add(this.buttonExport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(20, 368);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(819, 35);
            this.panel2.TabIndex = 3;
            // 
            // buttonColor
            // 
            this.buttonColor.BackColor = System.Drawing.SystemColors.Control;
            this.buttonColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonColor.Font = new System.Drawing.Font("Century Gothic", 7.2F);
            this.buttonColor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonColor.Location = new System.Drawing.Point(0, 0);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(90, 35);
            this.buttonColor.TabIndex = 5;
            this.buttonColor.Text = "Color ▼";
            this.buttonColor.UseVisualStyleBackColor = false;
            this.buttonColor.Click += new System.EventHandler(this.buttonColors_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.BackColor = System.Drawing.SystemColors.Control;
            this.buttonImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonImport.Font = new System.Drawing.Font("Century Gothic", 7.2F);
            this.buttonImport.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonImport.Location = new System.Drawing.Point(639, 0);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(90, 35);
            this.buttonImport.TabIndex = 4;
            this.buttonImport.Text = "➥ Import";
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.BackColor = System.Drawing.SystemColors.Control;
            this.buttonExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonExport.Font = new System.Drawing.Font("Century Gothic", 7.2F);
            this.buttonExport.Location = new System.Drawing.Point(729, 0);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(90, 35);
            this.buttonExport.TabIndex = 3;
            this.buttonExport.Text = "➦ Export";
            this.buttonExport.UseVisualStyleBackColor = false;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // contextMenuStripBlurb
            // 
            this.contextMenuStripBlurb.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripBlurb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripBlurb.Name = "contextMenuStrip1";
            this.contextMenuStripBlurb.Size = new System.Drawing.Size(127, 56);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::Servant.Properties.Resources.edit;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::Servant.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.SteelBlue;
            this.panelMain.Controls.Add(this.tableLayoutPanel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(10);
            this.panelMain.Size = new System.Drawing.Size(879, 423);
            this.panelMain.TabIndex = 2;
            // 
            // contextMenuStripColors
            // 
            this.contextMenuStripColors.BackColor = System.Drawing.SystemColors.Control;
            this.contextMenuStripColors.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripColors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.steelBlueToolStripMenuItem,
            this.firebrickToolStripMenuItem,
            this.burlyWoodToolStripMenuItem,
            this.oliveToolStripMenuItem,
            this.tealToolStripMenuItem,
            this.darkSlateBlueToolStripMenuItem,
            this.purpleToolStripMenuItem});
            this.contextMenuStripColors.Name = "contextMenuStripColors";
            this.contextMenuStripColors.Size = new System.Drawing.Size(172, 172);
            // 
            // steelBlueToolStripMenuItem
            // 
            this.steelBlueToolStripMenuItem.Name = "steelBlueToolStripMenuItem";
            this.steelBlueToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.steelBlueToolStripMenuItem.Text = "SteelBlue";
            this.steelBlueToolStripMenuItem.Click += new System.EventHandler(this.ColorMenuItem_Click);
            // 
            // firebrickToolStripMenuItem
            // 
            this.firebrickToolStripMenuItem.Name = "firebrickToolStripMenuItem";
            this.firebrickToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.firebrickToolStripMenuItem.Text = "Firebrick";
            this.firebrickToolStripMenuItem.Click += new System.EventHandler(this.ColorMenuItem_Click);
            // 
            // burlyWoodToolStripMenuItem
            // 
            this.burlyWoodToolStripMenuItem.Name = "burlyWoodToolStripMenuItem";
            this.burlyWoodToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.burlyWoodToolStripMenuItem.Text = "BurlyWood";
            this.burlyWoodToolStripMenuItem.Click += new System.EventHandler(this.ColorMenuItem_Click);
            // 
            // oliveToolStripMenuItem
            // 
            this.oliveToolStripMenuItem.Name = "oliveToolStripMenuItem";
            this.oliveToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.oliveToolStripMenuItem.Text = "Olive";
            this.oliveToolStripMenuItem.Click += new System.EventHandler(this.ColorMenuItem_Click);
            // 
            // tealToolStripMenuItem
            // 
            this.tealToolStripMenuItem.Name = "tealToolStripMenuItem";
            this.tealToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.tealToolStripMenuItem.Text = "Teal";
            this.tealToolStripMenuItem.Click += new System.EventHandler(this.ColorMenuItem_Click);
            // 
            // darkSlateBlueToolStripMenuItem
            // 
            this.darkSlateBlueToolStripMenuItem.Name = "darkSlateBlueToolStripMenuItem";
            this.darkSlateBlueToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.darkSlateBlueToolStripMenuItem.Text = "DarkSlateBlue";
            this.darkSlateBlueToolStripMenuItem.Click += new System.EventHandler(this.ColorMenuItem_Click);
            // 
            // purpleToolStripMenuItem
            // 
            this.purpleToolStripMenuItem.Name = "purpleToolStripMenuItem";
            this.purpleToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.purpleToolStripMenuItem.Text = "Purple";
            this.purpleToolStripMenuItem.Click += new System.EventHandler(this.ColorMenuItem_Click);
            // 
            // BlurbListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 423);
            this.Controls.Add(this.panelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BlurbListView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servant";
            this.Load += new System.EventHandler(this.BlurbListView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.contextMenuStripBlurb.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.contextMenuStripColors.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBlurb;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        public Extra.RoundedButton roundedButtonPlayPause;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label labelServantState;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button buttonAddBlurb;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripColors;
        private System.Windows.Forms.ToolStripMenuItem steelBlueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firebrickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem burlyWoodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oliveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tealToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkSlateBlueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purpleToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonColor;
    }
}

