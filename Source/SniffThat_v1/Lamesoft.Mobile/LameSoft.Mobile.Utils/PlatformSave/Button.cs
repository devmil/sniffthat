using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LameSoft.Mobile.Utils.PlatformSave
{
	public partial class Button : Base
	{
		private System.Windows.Forms.Button _Button;

		public Button()
		{
			InitializeComponent();

			if (_Platform != Platform.Smartphone)
			{
				_Button = new System.Windows.Forms.Button();
				_Button.Dock = DockStyle.Fill;
				this.Controls.Add(_Button);

				_Button.Click += new EventHandler(b_Click);
				_Button.DoubleClick += new EventHandler(b_DoubleClick);
				_Button.LostFocus += new EventHandler(b_LostFocus);
				_Button.KeyDown += new KeyEventHandler(b_KeyDown);
				_Button.KeyPress += new KeyPressEventHandler(b_KeyPress);
				_Button.KeyUp += new KeyEventHandler(b_KeyUp);
				_Button.GotFocus += new EventHandler(b_GotFocus);
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			if (_Button != null)
				_Button.Text = Text;
			base.OnTextChanged(e);
		}

		void b_GotFocus(object sender, EventArgs e)
		{
			OnGotFocus(e);
		}

		void b_KeyUp(object sender, KeyEventArgs e)
		{
			OnKeyUp(e);
		}

		void b_KeyPress(object sender, KeyPressEventArgs e)
		{
			OnKeyPress(e);
		}

		void b_KeyDown(object sender, KeyEventArgs e)
		{
			OnKeyDown(e);
		}

		void b_LostFocus(object sender, EventArgs e)
		{
			OnLostFocus(e);
		}

		void b_DoubleClick(object sender, EventArgs e)
		{
			OnDoubleClick(e);
		}

		void b_Click(object sender, EventArgs e)
		{
			OnClick(e);
		}


	}
}
