namespace LameSoft.SniffThat.Plugins.RadarVisualModule
{
    partial class RadarControl
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
            this.pnlRadar = new System.Windows.Forms.Panel();
            this.lRange = new System.Windows.Forms.Label();
            this.cbRange = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProtected = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pnlRadar
            // 
            this.pnlRadar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRadar.Location = new System.Drawing.Point(0, 27);
            this.pnlRadar.Name = "pnlRadar";
            this.pnlRadar.Size = new System.Drawing.Size(200, 123);
            // 
            // lRange
            // 
            this.lRange.Location = new System.Drawing.Point(3, 4);
            this.lRange.Name = "lRange";
            this.lRange.Size = new System.Drawing.Size(46, 20);
            this.lRange.Text = "Range:";
            // 
            // cbRange
            // 
            this.cbRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cbRange.Items.Add("0,1");
            this.cbRange.Items.Add("0,2");
            this.cbRange.Items.Add("0,3");
            this.cbRange.Items.Add("0,5");
            this.cbRange.Items.Add("1");
            this.cbRange.Items.Add("2");
            this.cbRange.Items.Add("3");
            this.cbRange.Items.Add("4");
            this.cbRange.Items.Add("5");
            this.cbRange.Location = new System.Drawing.Point(55, 2);
            this.cbRange.Name = "cbRange";
            this.cbRange.Size = new System.Drawing.Size(49, 22);
            this.cbRange.TabIndex = 2;
            this.cbRange.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(110, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.Text = "km";
            // 
            // cbProtected
            // 
            this.cbProtected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProtected.Checked = true;
            this.cbProtected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbProtected.Location = new System.Drawing.Point(147, 3);
            this.cbProtected.Name = "cbProtected";
            this.cbProtected.Size = new System.Drawing.Size(53, 20);
            this.cbProtected.TabIndex = 5;
            this.cbProtected.Text = "Prot.";
            this.cbProtected.CheckStateChanged += new System.EventHandler(this.cbProtected_CheckStateChanged);
            // 
            // RadarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlRadar);
            this.Controls.Add(this.cbProtected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbRange);
            this.Controls.Add(this.lRange);
            this.Name = "RadarControl";
            this.Size = new System.Drawing.Size(200, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRadar;
        private System.Windows.Forms.Label lRange;
        private System.Windows.Forms.ComboBox cbRange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbProtected;
    }
}
