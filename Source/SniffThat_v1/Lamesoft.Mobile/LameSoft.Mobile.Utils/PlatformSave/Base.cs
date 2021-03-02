using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LameSoft.Mobile.Utils.PlatformSave
{
	public abstract partial class Base : UserControl
	{
		private Menu _Menu;

		/// <summary>
		/// Gets or sets the menu.
		/// This only is needed if the Smartphone Platform is active.
		/// </summary>
		/// <value>The menu.</value>
		public Menu Menu
		{
			get
			{
				return _Menu;
			}
			set
			{
				_Menu = value;
				if (!_Menu.MenuItems.Contains(_MenuItem))
					_Menu.MenuItems.Add(_MenuItem);
			}
		}

		protected Platform _Platform;

		protected MenuItem _MenuItem;

		public Base()
		{
			InitializeComponent();

			//TODO Designer??

			try
			{
				_Platform = PlatformDetector.GetPlatform();
			}
			catch (Exception ex)
			{
				ex.ToString();
				_Platform = Platform.PocketPC;
			}

			if (_Platform == Platform.Smartphone)
			{
				_MenuItem = new MenuItem();
				_MenuItem.Click += new EventHandler(_MenuItem_Click);

				if (_Menu != null)
					_Menu.MenuItems.Add(_MenuItem);

				this.Visible = false;
			}

		}

		protected override void OnTextChanged(EventArgs e)
		{
			if (_MenuItem != null)
				_MenuItem.Text = Text;
			base.OnTextChanged(e);
		}

		void _MenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);
		}
	}
}
