namespace LameSoft.SniffThat.WLanGraphVisualPlugin
{
    partial class WLanGraphControl
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
            this.cbAP1 = new System.Windows.Forms.ComboBox();
            this.cbAP2 = new System.Windows.Forms.ComboBox();
            this.lAccessPoint1 = new System.Windows.Forms.Label();
            this.lAccessPoint2 = new System.Windows.Forms.Label();
            this.whg2 = new LameSoft.Mobile.WirelessLan.Visual.WLanHistoryGraph();
            this.whg1 = new LameSoft.Mobile.WirelessLan.Visual.WLanHistoryGraph();
            this.SuspendLayout();
            // 
            // cbAP1
            // 
            this.cbAP1.DisplayMember = "SSID";
            this.cbAP1.Location = new System.Drawing.Point(109, 3);
            this.cbAP1.Name = "cbAP1";
            this.cbAP1.Size = new System.Drawing.Size(128, 22);
            this.cbAP1.TabIndex = 2;
            this.cbAP1.SelectedValueChanged += new System.EventHandler(this.cbAP1_SelectedValueChanged);
            // 
            // cbAP2
            // 
            this.cbAP2.DisplayMember = "SSID";
            this.cbAP2.Location = new System.Drawing.Point(109, 112);
            this.cbAP2.Name = "cbAP2";
            this.cbAP2.Size = new System.Drawing.Size(128, 22);
            this.cbAP2.TabIndex = 3;
            this.cbAP2.SelectedValueChanged += new System.EventHandler(this.cbAP1_SelectedValueChanged);
            // 
            // lAccessPoint1
            // 
            this.lAccessPoint1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lAccessPoint1.Location = new System.Drawing.Point(3, 3);
            this.lAccessPoint1.Name = "lAccessPoint1";
            this.lAccessPoint1.Size = new System.Drawing.Size(100, 20);
            this.lAccessPoint1.Text = "Access Point 1";
            // 
            // lAccessPoint2
            // 
            this.lAccessPoint2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lAccessPoint2.Location = new System.Drawing.Point(3, 112);
            this.lAccessPoint2.Name = "lAccessPoint2";
            this.lAccessPoint2.Size = new System.Drawing.Size(100, 20);
            this.lAccessPoint2.Text = "Access Point 2";
            // 
            // whg2
            // 
            this.whg2.Location = new System.Drawing.Point(3, 140);
            this.whg2.Name = "whg2";
            this.whg2.Size = new System.Drawing.Size(234, 75);
            this.whg2.TabIndex = 1;
            // 
            // whg1
            // 
            this.whg1.Location = new System.Drawing.Point(3, 31);
            this.whg1.Name = "whg1";
            this.whg1.Size = new System.Drawing.Size(234, 75);
            this.whg1.TabIndex = 0;
            // 
            // WLanGraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lAccessPoint2);
            this.Controls.Add(this.lAccessPoint1);
            this.Controls.Add(this.cbAP2);
            this.Controls.Add(this.cbAP1);
            this.Controls.Add(this.whg2);
            this.Controls.Add(this.whg1);
            this.Name = "WLanGraphControl";
            this.Size = new System.Drawing.Size(240, 229);
            this.ResumeLayout(false);

        }

        #endregion

        private LameSoft.Mobile.WirelessLan.Visual.WLanHistoryGraph whg1;
        private LameSoft.Mobile.WirelessLan.Visual.WLanHistoryGraph whg2;
        private System.Windows.Forms.ComboBox cbAP1;
        private System.Windows.Forms.ComboBox cbAP2;
        private System.Windows.Forms.Label lAccessPoint1;
        private System.Windows.Forms.Label lAccessPoint2;
    }
}
