namespace LameSoft.SniffThat.Plugins.KmlExportModule
{
    partial class SettingsPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tbExportFile = new System.Windows.Forms.TextBox();
			this.bSelectExportFile = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.lExportFile = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbExportFile
			// 
			this.tbExportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbExportFile.BackColor = System.Drawing.Color.LightGray;
			this.tbExportFile.Location = new System.Drawing.Point(3, 27);
			this.tbExportFile.Multiline = true;
			this.tbExportFile.Name = "tbExportFile";
			this.tbExportFile.ReadOnly = true;
			this.tbExportFile.Size = new System.Drawing.Size(273, 84);
			this.tbExportFile.TabIndex = 0;
			// 
			// bSelectExportFile
			// 
			this.bSelectExportFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bSelectExportFile.Location = new System.Drawing.Point(255, 3);
			this.bSelectExportFile.Name = "bSelectExportFile";
			this.bSelectExportFile.Size = new System.Drawing.Size(21, 20);
			this.bSelectExportFile.TabIndex = 1;
			this.bSelectExportFile.Text = "...";
			this.bSelectExportFile.Click += new System.EventHandler(this.bSelectExportFile_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Kml Files (*.kml)|*.kml";
			// 
			// lExportFile
			// 
			this.lExportFile.Location = new System.Drawing.Point(3, 4);
			this.lExportFile.Name = "lExportFile";
			this.lExportFile.Size = new System.Drawing.Size(75, 20);
			this.lExportFile.Text = "Export File:";
			// 
			// SettingsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.tbExportFile);
			this.Controls.Add(this.lExportFile);
			this.Controls.Add(this.bSelectExportFile);
			this.Name = "SettingsPanel";
			this.Size = new System.Drawing.Size(279, 209);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbExportFile;
        private System.Windows.Forms.Button bSelectExportFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lExportFile;
    }
}
