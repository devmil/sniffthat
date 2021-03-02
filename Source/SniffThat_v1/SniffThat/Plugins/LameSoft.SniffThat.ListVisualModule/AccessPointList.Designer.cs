namespace LameSoft.SniffThat.Plugins.ListVisualModule
{
    partial class AccessPointList
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
            this.dgAccessPoints = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dgtbPos = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbColProtected = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbColSSID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbColStrength = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbColMAC = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbColLong = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbColLatitude = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbGpsInfo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbGpsInfoDistance = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbSignalStrengthDB = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbSupportedRates = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbFirstSeen = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbLastSeen = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtbPriv = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lOrderBy = new System.Windows.Forms.Label();
            this.cbOrderBy = new System.Windows.Forms.ComboBox();
            this.lStatus = new System.Windows.Forms.Label();
            this.cbInvert = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dgAccessPoints
            // 
            this.dgAccessPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAccessPoints.BackgroundColor = System.Drawing.Color.White;
            this.dgAccessPoints.Location = new System.Drawing.Point(3, 39);
            this.dgAccessPoints.Name = "dgAccessPoints";
            this.dgAccessPoints.RowHeadersVisible = false;
            this.dgAccessPoints.Size = new System.Drawing.Size(265, 218);
            this.dgAccessPoints.TabIndex = 2;
            this.dgAccessPoints.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbPos);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbColProtected);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbColSSID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbColStrength);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbColMAC);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbColLong);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbColLatitude);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbGpsInfo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbGpsInfoDistance);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbSignalStrengthDB);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbSupportedRates);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbFirstSeen);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbLastSeen);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbPriv);
            // 
            // dgtbPos
            // 
            this.dgtbPos.HeaderText = "Nr";
            this.dgtbPos.MappingName = "Nr";
            this.dgtbPos.Width = 40;
            // 
            // dgtbColProtected
            // 
            this.dgtbColProtected.HeaderText = "Prot.";
            this.dgtbColProtected.MappingName = "Protected";
            this.dgtbColProtected.Width = 40;
            // 
            // dgtbColSSID
            // 
            this.dgtbColSSID.HeaderText = "SSID";
            this.dgtbColSSID.MappingName = "SSID";
            this.dgtbColSSID.Width = 80;
            // 
            // dgtbColStrength
            // 
            this.dgtbColStrength.HeaderText = "Strength";
            this.dgtbColStrength.MappingName = "Strength";
            this.dgtbColStrength.Width = 70;
            // 
            // dgtbColMAC
            // 
            this.dgtbColMAC.HeaderText = "AP Mac";
            this.dgtbColMAC.MappingName = "MacAddressString";
            this.dgtbColMAC.Width = 80;
            // 
            // dgtbColLong
            // 
            this.dgtbColLong.HeaderText = "Longitude";
            this.dgtbColLong.MappingName = "Longitude";
            this.dgtbColLong.Width = 60;
            // 
            // dgtbColLatitude
            // 
            this.dgtbColLatitude.HeaderText = "Latitude";
            this.dgtbColLatitude.MappingName = "Latitude";
            this.dgtbColLatitude.Width = 60;
            // 
            // dgtbGpsInfo
            // 
            this.dgtbGpsInfo.HeaderText = "Gps Info";
            this.dgtbGpsInfo.MappingName = "GpsInfo";
            this.dgtbGpsInfo.Width = 150;
            // 
            // dgtbGpsInfoDistance
            // 
            this.dgtbGpsInfoDistance.HeaderText = "Distance";
            this.dgtbGpsInfoDistance.MappingName = "GpsInfoDistanceString";
            this.dgtbGpsInfoDistance.Width = 80;
            // 
            // dgtbSignalStrengthDB
            // 
            this.dgtbSignalStrengthDB.HeaderText = "DB";
            this.dgtbSignalStrengthDB.MappingName = "StrengthDB";
            // 
            // dgtbSupportedRates
            // 
            this.dgtbSupportedRates.HeaderText = "Supported Rates";
            this.dgtbSupportedRates.MappingName = "SupportedRatesString";
            this.dgtbSupportedRates.Width = 150;
            // 
            // dgtbFirstSeen
            // 
            this.dgtbFirstSeen.HeaderText = "First Seen";
            this.dgtbFirstSeen.MappingName = "FirstSeenString";
            this.dgtbFirstSeen.Width = 120;
            // 
            // dgtbLastSeen
            // 
            this.dgtbLastSeen.HeaderText = "Last Seen";
            this.dgtbLastSeen.MappingName = "LastSeenString";
            this.dgtbLastSeen.Width = 120;
            // 
            // dgtbPriv
            // 
            this.dgtbPriv.HeaderText = "Priv.";
            this.dgtbPriv.MappingName = "Privacy";
            // 
            // lOrderBy
            // 
            this.lOrderBy.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lOrderBy.Location = new System.Drawing.Point(3, 5);
            this.lOrderBy.Name = "lOrderBy";
            this.lOrderBy.Size = new System.Drawing.Size(84, 16);
            this.lOrderBy.Text = "Order By";
            // 
            // cbOrderBy
            // 
            this.cbOrderBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOrderBy.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbOrderBy.Location = new System.Drawing.Point(93, 3);
            this.cbOrderBy.Name = "cbOrderBy";
            this.cbOrderBy.Size = new System.Drawing.Size(175, 20);
            this.cbOrderBy.TabIndex = 4;
            // 
            // lStatus
            // 
            this.lStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lStatus.Location = new System.Drawing.Point(3, 258);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(265, 14);
            this.lStatus.Text = "Status";
            // 
            // cbInvert
            // 
            this.cbInvert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInvert.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbInvert.Location = new System.Drawing.Point(3, 22);
            this.cbInvert.Name = "cbInvert";
            this.cbInvert.Size = new System.Drawing.Size(265, 15);
            this.cbInvert.TabIndex = 6;
            this.cbInvert.Text = "Inverted";
            // 
            // AccessPointList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cbOrderBy);
            this.Controls.Add(this.dgAccessPoints);
            this.Controls.Add(this.cbInvert);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.lOrderBy);
            this.Name = "AccessPointList";
            this.Size = new System.Drawing.Size(271, 273);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgAccessPoints;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbColProtected;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbColSSID;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbColStrength;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbColMAC;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbColLong;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbColLatitude;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbGpsInfo;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbGpsInfoDistance;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbSignalStrengthDB;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbPos;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbSupportedRates;
		private System.Windows.Forms.DataGridTextBoxColumn dgtbFirstSeen;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbLastSeen;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbPriv;
        private System.Windows.Forms.Label lOrderBy;
        private System.Windows.Forms.ComboBox cbOrderBy;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.CheckBox cbInvert;

    }
}
