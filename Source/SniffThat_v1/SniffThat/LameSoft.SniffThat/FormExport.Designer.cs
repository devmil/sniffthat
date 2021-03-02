namespace LameSoft.SniffThat
{
    partial class FormExport
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
            this.miStartExport = new System.Windows.Forms.MenuItem();
            this.miCancelExport = new System.Windows.Forms.MenuItem();
            this.cbExportPlugin = new System.Windows.Forms.ComboBox();
            this.lExportTo = new System.Windows.Forms.Label();
            this.pnlPluginSettings = new System.Windows.Forms.Panel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.cbOnlyVisible = new System.Windows.Forms.CheckBox();
            this.cbOnlyNotProtected = new System.Windows.Forms.CheckBox();
            this.lFilter = new System.Windows.Forms.Label();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miStartExport);
            this.mainMenu1.MenuItems.Add(this.miCancelExport);
            // 
            // miStartExport
            // 
            this.miStartExport.Text = "Export";
            this.miStartExport.Click += new System.EventHandler(this.miExport_Click);
            // 
            // miCancelExport
            // 
            this.miCancelExport.Text = "Cancel";
            this.miCancelExport.Click += new System.EventHandler(this.miCancel_Click);
            // 
            // cbExportPlugin
            // 
            this.cbExportPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExportPlugin.Location = new System.Drawing.Point(86, 85);
            this.cbExportPlugin.Name = "cbExportPlugin";
            this.cbExportPlugin.Size = new System.Drawing.Size(151, 22);
            this.cbExportPlugin.TabIndex = 0;
            this.cbExportPlugin.SelectedIndexChanged += new System.EventHandler(this.cbExportPlugin_SelectedIndexChanged);
            // 
            // lExportTo
            // 
            this.lExportTo.Location = new System.Drawing.Point(3, 87);
            this.lExportTo.Name = "lExportTo";
            this.lExportTo.Size = new System.Drawing.Size(77, 20);
            this.lExportTo.Text = "Export to...";
            // 
            // pnlPluginSettings
            // 
            this.pnlPluginSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPluginSettings.Location = new System.Drawing.Point(3, 113);
            this.pnlPluginSettings.Name = "pnlPluginSettings";
            this.pnlPluginSettings.Size = new System.Drawing.Size(234, 152);
            this.pnlPluginSettings.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPluginSettings_Paint);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.Controls.Add(this.cbOnlyVisible);
            this.pnlFilter.Controls.Add(this.cbOnlyNotProtected);
            this.pnlFilter.Location = new System.Drawing.Point(3, 24);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(234, 55);
            this.pnlFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPluginSettings_Paint);
            // 
            // cbOnlyVisible
            // 
            this.cbOnlyVisible.Location = new System.Drawing.Point(3, 29);
            this.cbOnlyVisible.Name = "cbOnlyVisible";
            this.cbOnlyVisible.Size = new System.Drawing.Size(228, 20);
            this.cbOnlyVisible.TabIndex = 1;
            this.cbOnlyVisible.Text = "Only currently visible Access Points";
            // 
            // cbOnlyNotProtected
            // 
            this.cbOnlyNotProtected.Location = new System.Drawing.Point(3, 3);
            this.cbOnlyNotProtected.Name = "cbOnlyNotProtected";
            this.cbOnlyNotProtected.Size = new System.Drawing.Size(228, 20);
            this.cbOnlyNotProtected.TabIndex = 0;
            this.cbOnlyNotProtected.Text = "Only Not Protected Access Points";
            // 
            // lFilter
            // 
            this.lFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lFilter.Location = new System.Drawing.Point(3, 4);
            this.lFilter.Name = "lFilter";
            this.lFilter.Size = new System.Drawing.Size(100, 17);
            this.lFilter.Text = "Filter";
            // 
            // FormExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lFilter);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlPluginSettings);
            this.Controls.Add(this.lExportTo);
            this.Controls.Add(this.cbExportPlugin);
            this.Menu = this.mainMenu1;
            this.Name = "FormExport";
            this.Text = "Export";
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.pnlFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbExportPlugin;
        private System.Windows.Forms.Label lExportTo;
        private System.Windows.Forms.Panel pnlPluginSettings;
        private System.Windows.Forms.MenuItem miStartExport;
        private System.Windows.Forms.MenuItem miCancelExport;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lFilter;
        private System.Windows.Forms.CheckBox cbOnlyNotProtected;
        private System.Windows.Forms.CheckBox cbOnlyVisible;
    }
}