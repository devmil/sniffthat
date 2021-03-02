using System.Windows.Forms;
namespace LameSoft.SniffThat
{
    partial class FormSettings
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
            this.miSaveSettings = new System.Windows.Forms.MenuItem();
            this.miCancelSettings = new System.Windows.Forms.MenuItem();
            this.lGpsComPort = new System.Windows.Forms.Label();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.lGpsBaud = new System.Windows.Forms.Label();
            this.cbBaud = new System.Windows.Forms.ComboBox();
            this.lGpsInfoFile = new System.Windows.Forms.Label();
            this.tbGpsInfo = new System.Windows.Forms.TextBox();
            this.bSelectGpsInfo = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.nudGpsInfoRadius = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.lMaxGpsRadius = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpWireless = new System.Windows.Forms.TabPage();
            this.comboSoundFile = new System.Windows.Forms.ComboBox();
            this.bTest = new System.Windows.Forms.Button();
            this.lSoundFile = new System.Windows.Forms.Label();
            this.cbPlaySound = new System.Windows.Forms.CheckBox();
            this.bRefresh = new System.Windows.Forms.Button();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.lScanInterval = new System.Windows.Forms.Label();
            this.lWLanAdapter = new System.Windows.Forms.Label();
            this.cbWLan = new System.Windows.Forms.ComboBox();
            this.tpGps = new System.Windows.Forms.TabPage();
            this.tpModules = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.wbModuleInfo = new System.Windows.Forms.WebBrowser();
            this.tvModules = new System.Windows.Forms.TreeView();
            this.cmSettings = new System.Windows.Forms.ContextMenu();
            this.miSettings = new System.Windows.Forms.MenuItem();
            this.bStop = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpWireless.SuspendLayout();
            this.tpGps.SuspendLayout();
            this.tpModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miSaveSettings);
            this.mainMenu1.MenuItems.Add(this.miCancelSettings);
            // 
            // miSaveSettings
            // 
            this.miSaveSettings.Text = "Save";
            this.miSaveSettings.Click += new System.EventHandler(this.bSave_Click);
            // 
            // miCancelSettings
            // 
            this.miCancelSettings.Text = "Cancel";
            this.miCancelSettings.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lGpsComPort
            // 
            this.lGpsComPort.Location = new System.Drawing.Point(3, 4);
            this.lGpsComPort.Name = "lGpsComPort";
            this.lGpsComPort.Size = new System.Drawing.Size(100, 20);
            this.lGpsComPort.Text = "Gps Com Port:";
            // 
            // cbComPort
            // 
            this.cbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cbComPort.Items.Add("COM1:");
            this.cbComPort.Items.Add("COM2:");
            this.cbComPort.Items.Add("COM3:");
            this.cbComPort.Items.Add("COM4:");
            this.cbComPort.Items.Add("COM5:");
            this.cbComPort.Items.Add("COM6:");
            this.cbComPort.Items.Add("COM7:");
            this.cbComPort.Items.Add("COM8:");
            this.cbComPort.Items.Add("COM9:");
            this.cbComPort.Items.Add("COM10:");
            this.cbComPort.Location = new System.Drawing.Point(122, 2);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(77, 22);
            this.cbComPort.TabIndex = 0;
            // 
            // lGpsBaud
            // 
            this.lGpsBaud.Location = new System.Drawing.Point(3, 32);
            this.lGpsBaud.Name = "lGpsBaud";
            this.lGpsBaud.Size = new System.Drawing.Size(100, 20);
            this.lGpsBaud.Text = "Gps Baud:";
            // 
            // cbBaud
            // 
            this.cbBaud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBaud.Location = new System.Drawing.Point(122, 30);
            this.cbBaud.Name = "cbBaud";
            this.cbBaud.Size = new System.Drawing.Size(107, 22);
            this.cbBaud.TabIndex = 2;
            // 
            // lGpsInfoFile
            // 
            this.lGpsInfoFile.Location = new System.Drawing.Point(3, 97);
            this.lGpsInfoFile.Name = "lGpsInfoFile";
            this.lGpsInfoFile.Size = new System.Drawing.Size(100, 20);
            this.lGpsInfoFile.Text = "Gps Info File:";
            // 
            // tbGpsInfo
            // 
            this.tbGpsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGpsInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tbGpsInfo.Location = new System.Drawing.Point(3, 120);
            this.tbGpsInfo.Multiline = true;
            this.tbGpsInfo.Name = "tbGpsInfo";
            this.tbGpsInfo.ReadOnly = true;
            this.tbGpsInfo.Size = new System.Drawing.Size(226, 42);
            this.tbGpsInfo.TabIndex = 5;
            this.tbGpsInfo.TabStop = false;
            // 
            // bSelectGpsInfo
            // 
            this.bSelectGpsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSelectGpsInfo.Location = new System.Drawing.Point(207, 97);
            this.bSelectGpsInfo.Name = "bSelectGpsInfo";
            this.bSelectGpsInfo.Size = new System.Drawing.Size(22, 20);
            this.bSelectGpsInfo.TabIndex = 4;
            this.bSelectGpsInfo.Text = "...";
            this.bSelectGpsInfo.Click += new System.EventHandler(this.bSelectGpsInfo_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "OpenGeoDB Mobile (*.ogm)|*.ogm";
            // 
            // nudGpsInfoRadius
            // 
            this.nudGpsInfoRadius.Location = new System.Drawing.Point(122, 58);
            this.nudGpsInfoRadius.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudGpsInfoRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGpsInfoRadius.Name = "nudGpsInfoRadius";
            this.nudGpsInfoRadius.Size = new System.Drawing.Size(53, 22);
            this.nudGpsInfoRadius.TabIndex = 3;
            this.nudGpsInfoRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(181, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 20);
            this.label6.Text = "km";
            // 
            // lMaxGpsRadius
            // 
            this.lMaxGpsRadius.Location = new System.Drawing.Point(3, 52);
            this.lMaxGpsRadius.Name = "lMaxGpsRadius";
            this.lMaxGpsRadius.Size = new System.Drawing.Size(100, 36);
            this.lMaxGpsRadius.Text = "Max. Gps Info Radius:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpWireless);
            this.tabControl1.Controls.Add(this.tpGps);
            this.tabControl1.Controls.Add(this.tpModules);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 268);
            this.tabControl1.TabIndex = 31;
            // 
            // tpWireless
            // 
            this.tpWireless.Controls.Add(this.bStop);
            this.tpWireless.Controls.Add(this.comboSoundFile);
            this.tpWireless.Controls.Add(this.bTest);
            this.tpWireless.Controls.Add(this.lSoundFile);
            this.tpWireless.Controls.Add(this.cbPlaySound);
            this.tpWireless.Controls.Add(this.bRefresh);
            this.tpWireless.Controls.Add(this.nudInterval);
            this.tpWireless.Controls.Add(this.lScanInterval);
            this.tpWireless.Controls.Add(this.lWLanAdapter);
            this.tpWireless.Controls.Add(this.cbWLan);
            this.tpWireless.Location = new System.Drawing.Point(0, 0);
            this.tpWireless.Name = "tpWireless";
            this.tpWireless.Size = new System.Drawing.Size(240, 245);
            this.tpWireless.Text = "Wireless";
            // 
            // comboSoundFile
            // 
            this.comboSoundFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSoundFile.Location = new System.Drawing.Point(3, 132);
            this.comboSoundFile.Name = "comboSoundFile";
            this.comboSoundFile.Size = new System.Drawing.Size(234, 22);
            this.comboSoundFile.TabIndex = 13;
            // 
            // bTest
            // 
            this.bTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bTest.Location = new System.Drawing.Point(147, 107);
            this.bTest.Name = "bTest";
            this.bTest.Size = new System.Drawing.Size(42, 20);
            this.bTest.TabIndex = 12;
            this.bTest.Text = "Play";
            this.bTest.Click += new System.EventHandler(this.bTest_Click);
            // 
            // lSoundFile
            // 
            this.lSoundFile.Location = new System.Drawing.Point(3, 109);
            this.lSoundFile.Name = "lSoundFile";
            this.lSoundFile.Size = new System.Drawing.Size(100, 20);
            this.lSoundFile.Text = "Sound File:";
            // 
            // cbPlaySound
            // 
            this.cbPlaySound.Location = new System.Drawing.Point(3, 81);
            this.cbPlaySound.Name = "cbPlaySound";
            this.cbPlaySound.Size = new System.Drawing.Size(229, 20);
            this.cbPlaySound.TabIndex = 5;
            this.cbPlaySound.Text = "Play Open WLan Sound";
            // 
            // bRefresh
            // 
            this.bRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRefresh.Location = new System.Drawing.Point(215, 9);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(22, 20);
            this.bRefresh.TabIndex = 1;
            this.bRefresh.Text = "...";
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // nudInterval
            // 
            this.nudInterval.Location = new System.Drawing.Point(122, 39);
            this.nudInterval.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(93, 22);
            this.nudInterval.TabIndex = 2;
            this.nudInterval.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lScanInterval
            // 
            this.lScanInterval.Location = new System.Drawing.Point(3, 41);
            this.lScanInterval.Name = "lScanInterval";
            this.lScanInterval.Size = new System.Drawing.Size(119, 20);
            this.lScanInterval.Text = "Scan Interval (ms):";
            // 
            // lWLanAdapter
            // 
            this.lWLanAdapter.Location = new System.Drawing.Point(3, 11);
            this.lWLanAdapter.Name = "lWLanAdapter";
            this.lWLanAdapter.Size = new System.Drawing.Size(107, 20);
            this.lWLanAdapter.Text = "Wireless Adapter:";
            // 
            // cbWLan
            // 
            this.cbWLan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWLan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cbWLan.Location = new System.Drawing.Point(122, 9);
            this.cbWLan.Name = "cbWLan";
            this.cbWLan.Size = new System.Drawing.Size(87, 22);
            this.cbWLan.TabIndex = 0;
            // 
            // tpGps
            // 
            this.tpGps.AutoScroll = true;
            this.tpGps.Controls.Add(this.lGpsComPort);
            this.tpGps.Controls.Add(this.lMaxGpsRadius);
            this.tpGps.Controls.Add(this.label6);
            this.tpGps.Controls.Add(this.cbComPort);
            this.tpGps.Controls.Add(this.nudGpsInfoRadius);
            this.tpGps.Controls.Add(this.lGpsBaud);
            this.tpGps.Controls.Add(this.bSelectGpsInfo);
            this.tpGps.Controls.Add(this.cbBaud);
            this.tpGps.Controls.Add(this.tbGpsInfo);
            this.tpGps.Controls.Add(this.lGpsInfoFile);
            this.tpGps.Location = new System.Drawing.Point(0, 0);
            this.tpGps.Name = "tpGps";
            this.tpGps.Size = new System.Drawing.Size(232, 242);
            this.tpGps.Text = "Gps";
            // 
            // tpModules
            // 
            this.tpModules.Controls.Add(this.splitter1);
            this.tpModules.Controls.Add(this.wbModuleInfo);
            this.tpModules.Controls.Add(this.tvModules);
            this.tpModules.Location = new System.Drawing.Point(0, 0);
            this.tpModules.Name = "tpModules";
            this.tpModules.Size = new System.Drawing.Size(232, 242);
            this.tpModules.Text = "Modules";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 124);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(232, 3);
            // 
            // wbModuleInfo
            // 
            this.wbModuleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbModuleInfo.Location = new System.Drawing.Point(0, 124);
            this.wbModuleInfo.Name = "wbModuleInfo";
            this.wbModuleInfo.Size = new System.Drawing.Size(232, 118);
            this.wbModuleInfo.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbModuleInfo_Navigating);
            this.wbModuleInfo.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbModuleInfo_DocumentCompleted);
            // 
            // tvModules
            // 
            this.tvModules.CheckBoxes = true;
            this.tvModules.ContextMenu = this.cmSettings;
            this.tvModules.Dock = System.Windows.Forms.DockStyle.Top;
            this.tvModules.Location = new System.Drawing.Point(0, 0);
            this.tvModules.Name = "tvModules";
            this.tvModules.Size = new System.Drawing.Size(232, 124);
            this.tvModules.TabIndex = 0;
            this.tvModules.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvModules_AfterCheck);
            this.tvModules.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvModules_AfterSelect);
            // 
            // cmSettings
            // 
            this.cmSettings.MenuItems.Add(this.miSettings);
            // 
            // miSettings
            // 
            this.miSettings.Text = "Settings";
            this.miSettings.Click += new System.EventHandler(this.miSettings_Click);
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bStop.Location = new System.Drawing.Point(195, 107);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(42, 20);
            this.bStop.TabIndex = 17;
            this.bStop.Text = "Stop";
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormSettings_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tpWireless.ResumeLayout(false);
            this.tpGps.ResumeLayout(false);
            this.tpModules.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lGpsComPort;
        private System.Windows.Forms.ComboBox cbComPort;
        private System.Windows.Forms.MenuItem miCancelSettings;
        private System.Windows.Forms.MenuItem miSaveSettings;
        private Label lGpsBaud;
        private ComboBox cbBaud;
        private Label lGpsInfoFile;
        private TextBox tbGpsInfo;
        private Button bSelectGpsInfo;
        private OpenFileDialog openFileDialog1;
        private NumericUpDown nudGpsInfoRadius;
        private Label label6;
        private Label lMaxGpsRadius;
        private TabControl tabControl1;
        private TabPage tpWireless;
        private Button bRefresh;
        private NumericUpDown nudInterval;
        private Label lScanInterval;
        private Label lWLanAdapter;
        private ComboBox cbWLan;
        private TabPage tpGps;
        private TabPage tpModules;
        private TreeView tvModules;
        private WebBrowser wbModuleInfo;
        private Splitter splitter1;
		private ContextMenu cmSettings;
        private MenuItem miSettings;
        private Label lSoundFile;
        private CheckBox cbPlaySound;
        private Button bTest;
        private ComboBox comboSoundFile;
        private Button bStop;
    }
}