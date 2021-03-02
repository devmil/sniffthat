namespace LameSoft.SniffThat.Plugins.GpsInfoVisualModule
{
    partial class GpsInfoControl
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
            this.dgGpsInfos = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dgtbGpsInfo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtDistance = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lCurrentGpsInfo = new System.Windows.Forms.Label();
            this.dgtLat = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtLong = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // dgGpsInfos
            // 
            this.dgGpsInfos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgGpsInfos.BackgroundColor = System.Drawing.Color.White;
            this.dgGpsInfos.Location = new System.Drawing.Point(0, 43);
            this.dgGpsInfos.Name = "dgGpsInfos";
            this.dgGpsInfos.RowHeadersVisible = false;
            this.dgGpsInfos.Size = new System.Drawing.Size(201, 189);
            this.dgGpsInfos.TabIndex = 3;
            this.dgGpsInfos.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtbGpsInfo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtDistance);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtLat);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgtLong);
            this.dataGridTableStyle1.MappingName = "GpsInfos";
            // 
            // dgtbGpsInfo
            // 
            this.dgtbGpsInfo.HeaderText = "Gps Info";
            this.dgtbGpsInfo.MappingName = "Info";
            this.dgtbGpsInfo.Width = 100;
            // 
            // dgtDistance
            // 
            this.dgtDistance.HeaderText = "Dist.";
            this.dgtDistance.MappingName = "DistanceString";
            // 
            // lCurrentGpsInfo
            // 
            this.lCurrentGpsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lCurrentGpsInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lCurrentGpsInfo.Location = new System.Drawing.Point(3, 9);
            this.lCurrentGpsInfo.Name = "lCurrentGpsInfo";
            this.lCurrentGpsInfo.Size = new System.Drawing.Size(195, 20);
            // 
            // dgtLat
            // 
            this.dgtLat.HeaderText = "Latitude";
            this.dgtLat.MappingName = "Latitude";
            this.dgtLat.Width = 60;
            // 
            // dgtLong
            // 
            this.dgtLong.HeaderText = "Longitude";
            this.dgtLong.MappingName = "Longitude";
            this.dgtLong.Width = 60;
            // 
            // GpsInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lCurrentGpsInfo);
            this.Controls.Add(this.dgGpsInfos);
            this.Name = "GpsInfoControl";
            this.Size = new System.Drawing.Size(201, 232);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgGpsInfos;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dgtbGpsInfo;
        private System.Windows.Forms.DataGridTextBoxColumn dgtDistance;
        private System.Windows.Forms.Label lCurrentGpsInfo;
        private System.Windows.Forms.DataGridTextBoxColumn dgtLat;
        private System.Windows.Forms.DataGridTextBoxColumn dgtLong;
    }
}
