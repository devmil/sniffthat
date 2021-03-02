namespace LameSoft.SniffThat.Plugins.GpsVisualModule
{
    partial class GpsVisualizer
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
            this.pnlSats = new System.Windows.Forms.Panel();
            this.lLongitude = new System.Windows.Forms.Label();
            this.lLatitude = new System.Windows.Forms.Label();
            this.lAltitude = new System.Windows.Forms.Label();
            this.lSpeedText = new System.Windows.Forms.Label();
            this.lLong = new System.Windows.Forms.Label();
            this.lLat = new System.Windows.Forms.Label();
            this.lAlt = new System.Windows.Forms.Label();
            this.lSpeed = new System.Windows.Forms.Label();
            this.lGpsInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlSats
            // 
            this.pnlSats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSats.Location = new System.Drawing.Point(3, 103);
            this.pnlSats.Name = "pnlSats";
            this.pnlSats.Size = new System.Drawing.Size(270, 118);
            // 
            // lLongitude
            // 
            this.lLongitude.Location = new System.Drawing.Point(3, 0);
            this.lLongitude.Name = "lLongitude";
            this.lLongitude.Size = new System.Drawing.Size(75, 20);
            this.lLongitude.Text = "Longitude:";
            // 
            // lLatitude
            // 
            this.lLatitude.Location = new System.Drawing.Point(3, 20);
            this.lLatitude.Name = "lLatitude";
            this.lLatitude.Size = new System.Drawing.Size(75, 20);
            this.lLatitude.Text = "Latitude:";
            // 
            // lAltitude
            // 
            this.lAltitude.Location = new System.Drawing.Point(3, 40);
            this.lAltitude.Name = "lAltitude";
            this.lAltitude.Size = new System.Drawing.Size(75, 20);
            this.lAltitude.Text = "Altitude:";
            // 
            // lSpeedText
            // 
            this.lSpeedText.Location = new System.Drawing.Point(3, 60);
            this.lSpeedText.Name = "lSpeedText";
            this.lSpeedText.Size = new System.Drawing.Size(75, 20);
            this.lSpeedText.Text = "Speed:";
            // 
            // lLong
            // 
            this.lLong.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lLong.Location = new System.Drawing.Point(84, 0);
            this.lLong.Name = "lLong";
            this.lLong.Size = new System.Drawing.Size(98, 20);
            // 
            // lLat
            // 
            this.lLat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lLat.Location = new System.Drawing.Point(84, 20);
            this.lLat.Name = "lLat";
            this.lLat.Size = new System.Drawing.Size(98, 20);
            // 
            // lAlt
            // 
            this.lAlt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lAlt.Location = new System.Drawing.Point(84, 40);
            this.lAlt.Name = "lAlt";
            this.lAlt.Size = new System.Drawing.Size(98, 20);
            // 
            // lSpeed
            // 
            this.lSpeed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lSpeed.Location = new System.Drawing.Point(84, 60);
            this.lSpeed.Name = "lSpeed";
            this.lSpeed.Size = new System.Drawing.Size(98, 20);
            // 
            // lGpsInfo
            // 
            this.lGpsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lGpsInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lGpsInfo.Location = new System.Drawing.Point(3, 80);
            this.lGpsInfo.Name = "lGpsInfo";
            this.lGpsInfo.Size = new System.Drawing.Size(270, 20);
            // 
            // GpsVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lGpsInfo);
            this.Controls.Add(this.lSpeed);
            this.Controls.Add(this.lAlt);
            this.Controls.Add(this.lLat);
            this.Controls.Add(this.lLong);
            this.Controls.Add(this.lSpeedText);
            this.Controls.Add(this.lAltitude);
            this.Controls.Add(this.lLatitude);
            this.Controls.Add(this.lLongitude);
            this.Controls.Add(this.pnlSats);
            this.Name = "GpsVisualizer";
            this.Size = new System.Drawing.Size(276, 224);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSats;
        private System.Windows.Forms.Label lLongitude;
        private System.Windows.Forms.Label lLatitude;
        private System.Windows.Forms.Label lAltitude;
        private System.Windows.Forms.Label lSpeedText;
        private System.Windows.Forms.Label lLong;
        private System.Windows.Forms.Label lLat;
        private System.Windows.Forms.Label lAlt;
        private System.Windows.Forms.Label lSpeed;
        private System.Windows.Forms.Label lGpsInfo;
    }
}
