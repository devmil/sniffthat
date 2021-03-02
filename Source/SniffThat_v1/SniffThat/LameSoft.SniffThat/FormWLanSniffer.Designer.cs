namespace LameSoft.SniffThat
{
    partial class FormWLanSniffer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.miMenu = new System.Windows.Forms.MenuItem();
            this.miVisuals = new System.Windows.Forms.MenuItem();
            this.miAccessPoints = new System.Windows.Forms.MenuItem();
            this.miLoad = new System.Windows.Forms.MenuItem();
            this.miMerge = new System.Windows.Forms.MenuItem();
            this.miSave = new System.Windows.Forms.MenuItem();
            this.miClear = new System.Windows.Forms.MenuItem();
            this.miExport = new System.Windows.Forms.MenuItem();
            this.miRefreshGpsInfo = new System.Windows.Forms.MenuItem();
            this.miSettings = new System.Windows.Forms.MenuItem();
            this.miScan = new System.Windows.Forms.MenuItem();
            this.miGps = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.sBar = new System.Windows.Forms.StatusBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tcVisuals = new System.Windows.Forms.TabControl();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lDBVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lHead = new System.Windows.Forms.Label();
            this.tcVisuals.SuspendLayout();
            this.tpAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miMenu);
            this.mainMenu1.MenuItems.Add(this.miExit);
            // 
            // miMenu
            // 
            this.miMenu.MenuItems.Add(this.miVisuals);
            this.miMenu.MenuItems.Add(this.miAccessPoints);
            this.miMenu.MenuItems.Add(this.miSettings);
            this.miMenu.MenuItems.Add(this.miScan);
            this.miMenu.MenuItems.Add(this.miGps);
            this.miMenu.Text = "Menu";
            // 
            // miVisuals
            // 
            this.miVisuals.Text = "Visuals";
            // 
            // miAccessPoints
            // 
            this.miAccessPoints.MenuItems.Add(this.miLoad);
            this.miAccessPoints.MenuItems.Add(this.miMerge);
            this.miAccessPoints.MenuItems.Add(this.miSave);
            this.miAccessPoints.MenuItems.Add(this.miClear);
            this.miAccessPoints.MenuItems.Add(this.miExport);
            this.miAccessPoints.MenuItems.Add(this.miRefreshGpsInfo);
            this.miAccessPoints.Text = "AccessPoints";
            // 
            // miLoad
            // 
            this.miLoad.Text = "Load";
            this.miLoad.Click += new System.EventHandler(this.miLoad_Click);
            // 
            // miMerge
            // 
            this.miMerge.Text = "Merge";
            this.miMerge.Click += new System.EventHandler(this.miMerge_Click);
            // 
            // miSave
            // 
            this.miSave.Text = "Save";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miClear
            // 
            this.miClear.Text = "Clear";
            this.miClear.Click += new System.EventHandler(this.miClear_Click);
            // 
            // miExport
            // 
            this.miExport.Text = "Export";
            this.miExport.Click += new System.EventHandler(this.miExport_Click);
            // 
            // miRefreshGpsInfo
            // 
            this.miRefreshGpsInfo.Text = "Refresh Gps Info";
            this.miRefreshGpsInfo.Click += new System.EventHandler(this.miRefreshGpsInfo_Click);
            // 
            // miSettings
            // 
            this.miSettings.Text = "Settings";
            this.miSettings.Click += new System.EventHandler(this.miSettings_Click);
            // 
            // miScan
            // 
            this.miScan.Text = "Scan";
            this.miScan.Click += new System.EventHandler(this.miScan_Click);
            // 
            // miGps
            // 
            this.miGps.Text = "Gps";
            this.miGps.Click += new System.EventHandler(this.miGps_Click);
            // 
            // miExit
            // 
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // sBar
            // 
            this.sBar.Location = new System.Drawing.Point(0, 246);
            this.sBar.Name = "sBar";
            this.sBar.Size = new System.Drawing.Size(240, 22);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Mobile WLan Sniffer (*.mws)|*.mws";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Mobile WLan Sniffer (*.mws)|*.mws";
            // 
            // tcVisuals
            // 
            this.tcVisuals.Controls.Add(this.tpAbout);
            this.tcVisuals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcVisuals.Location = new System.Drawing.Point(0, 0);
            this.tcVisuals.Name = "tcVisuals";
            this.tcVisuals.SelectedIndex = 0;
            this.tcVisuals.Size = new System.Drawing.Size(240, 246);
            this.tcVisuals.TabIndex = 3;
            this.tcVisuals.SelectedIndexChanged += new System.EventHandler(this.tcVisuals_SelectedIndexChanged);
            // 
            // tpAbout
            // 
            this.tpAbout.Controls.Add(this.pictureBox1);
            this.tpAbout.Controls.Add(this.lDBVersion);
            this.tpAbout.Controls.Add(this.label1);
            this.tpAbout.Controls.Add(this.lHead);
            this.tpAbout.Location = new System.Drawing.Point(0, 0);
            this.tpAbout.Name = "tpAbout";
            this.tpAbout.Size = new System.Drawing.Size(240, 223);
            this.tpAbout.Text = "About";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(230, 120);
            // 
            // lDBVersion
            // 
            this.lDBVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lDBVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lDBVersion.Location = new System.Drawing.Point(3, 69);
            this.lDBVersion.Name = "lDBVersion";
            this.lDBVersion.Size = new System.Drawing.Size(230, 17);
            this.lDBVersion.Text = "DB Version";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 30);
            this.label1.Text = "(c) 2007 by LameSoft\r\nhttp://www.lamers-software.de";
            // 
            // lHead
            // 
            this.lHead.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lHead.Location = new System.Drawing.Point(7, 9);
            this.lHead.Name = "lHead";
            this.lHead.Size = new System.Drawing.Size(226, 20);
            this.lHead.Text = "SniffThat";
            // 
            // FormWLanSniffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tcVisuals);
            this.Controls.Add(this.sBar);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FormWLanSniffer";
            this.Text = "SniffThat";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormWLanSniffer_Closing);
            this.Load += new System.EventHandler(this.FormWLanSniffer_Load);
            this.tcVisuals.ResumeLayout(false);
            this.tpAbout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miMenu;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem miSettings;
        private System.Windows.Forms.StatusBar sBar;
        protected System.Windows.Forms.MenuItem miScan;
        private System.Windows.Forms.MenuItem miAccessPoints;
        private System.Windows.Forms.MenuItem miLoad;
        private System.Windows.Forms.MenuItem miSave;
        private System.Windows.Forms.MenuItem miClear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuItem miMerge;
        private System.Windows.Forms.MenuItem miGps;
        private System.Windows.Forms.MenuItem miExport;
        private System.Windows.Forms.TabControl tcVisuals;
        private System.Windows.Forms.MenuItem miVisuals;
        private System.Windows.Forms.MenuItem miRefreshGpsInfo;
        private System.Windows.Forms.TabPage tpAbout;
        private System.Windows.Forms.Label lHead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lDBVersion;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

