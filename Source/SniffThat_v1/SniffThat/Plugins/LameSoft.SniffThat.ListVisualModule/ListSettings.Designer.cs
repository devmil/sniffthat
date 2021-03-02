namespace LameSoft.SniffThat.Plugins.ListVisualModule
{
    partial class ListSettings
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
			this.lvColumns = new System.Windows.Forms.ListView();
			this.pnlbUp = new System.Windows.Forms.Panel();
			this.pnlbDown = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// lvColumns
			// 
			this.lvColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvColumns.CheckBoxes = true;
			this.lvColumns.Location = new System.Drawing.Point(3, 3);
			this.lvColumns.Name = "lvColumns";
			this.lvColumns.Size = new System.Drawing.Size(206, 238);
			this.lvColumns.TabIndex = 0;
			this.lvColumns.View = System.Windows.Forms.View.List;
			this.lvColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvColumns_ItemCheck);
			// 
			// pnlbUp
			// 
			this.pnlbUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlbUp.Location = new System.Drawing.Point(215, 3);
			this.pnlbUp.Name = "pnlbUp";
			this.pnlbUp.Size = new System.Drawing.Size(75, 25);
			// 
			// pnlbDown
			// 
			this.pnlbDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlbDown.Location = new System.Drawing.Point(215, 34);
			this.pnlbDown.Name = "pnlbDown";
			this.pnlbDown.Size = new System.Drawing.Size(75, 25);
			// 
			// ListSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnlbDown);
			this.Controls.Add(this.pnlbUp);
			this.Controls.Add(this.lvColumns);
			this.Name = "ListSettings";
			this.Size = new System.Drawing.Size(293, 244);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ListView lvColumns;
		private System.Windows.Forms.Panel pnlbUp;
		private System.Windows.Forms.Panel pnlbDown;
    }
}
