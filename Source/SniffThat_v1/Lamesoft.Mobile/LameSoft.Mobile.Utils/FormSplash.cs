//==========================================================================================
//
//		LameSoft.Mobile.Utils.FormSplash
//		Copyright (c) 2006-2007, LameSoft (www.lamesoft.de)
//
//      This Code is provided under the LGPL. For more Information see 
//      http://www.gnu.org/licenses/lgpl.html
//
//      This library is free software; you can redistribute it and/or
//      modify it under the terms of the GNU Lesser General Public
//      License as published by the Free Software Foundation; either
//      version 2.1 of the License, or (at your option) any later version.
//
//      This library is distributed in the hope that it will be useful,
//      but WITHOUT ANY WARRANTY; without even the implied warranty of
//      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//      Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public
//      License along with this library; if not, write to the Free Software
//      Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
//
//      You are not allowed to remove or change this header!
//
//      If you change the source code in this file, you have to add a changelog entry.
//
//--------------- Changelog ----------------------------------------------------------------  
//
//      - 27.12.2006 (Michael Lamers, info@lamesoft.de): 
//          * added header
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace LameSoft.Mobile.Utils
{
    public partial class FormSplash : Form
    {
        private const int C_OFFSET = 5;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FormSplash"/> class.
        /// </summary>
        /// <param name="dueTime">The due time.</param>
        public FormSplash()
        {
            InitializeComponent();
        }

        private string _Header;

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public string Header
        {
            get { return _Header; }
            set 
            { 
                lock(this)
                    _Header = value;
                Image = _OriginalImage;
            }
        }

        private Image _OriginalImage;

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image
        {
            get
            {
                return _OriginalImage;
            }
            set
            {
                _OriginalImage = value;
                pbSplash.Image = GenerateImage();
            }
        }

        private Image GenerateImage()
        {
            Bitmap result = new Bitmap(_OriginalImage);
            Graphics g = Graphics.FromImage(result);
            SizeF textSize = g.MeasureString(_Header, this.Font);
            g.DrawString(_Header, this.Font, new SolidBrush(this.ForeColor), new RectangleF(this.ClientRectangle.Right - C_OFFSET - textSize.Width, this.ClientRectangle.Top + C_OFFSET, textSize.Width, textSize.Height));
            g.Dispose();
            return result;
        }

        private ImageList _ImageList;
        private List<PictureBox> _PictureBoxes = new List<PictureBox>();

        private const int C_IMAGESOFFSET = 2;

        /// <summary>
        /// Sets the progress bitmap.
        /// </summary>
        /// <param name="activeBitmap">The active bitmap.</param>
        /// <param name="inactiveBitmap">The inactive bitmap.</param>
        public void SetProgressBitmap(Bitmap activeBitmap, Bitmap inactiveBitmap)
        {
            if (_ImageList == null)
            {
                _ImageList = new ImageList();
                _ImageList.ImageSize = new Size(activeBitmap.Width, activeBitmap.Height);
            }

            PictureBox pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Left = (int)(_ImageList.Images.Count * (_ImageList.ImageSize.Width + C_IMAGESOFFSET)) + 8;
            pb.Height = _ImageList.ImageSize.Height;
            pb.Width = _ImageList.ImageSize.Width;
            pb.Top = lState.Top;// +lState.Height - pb.Height - (int)((lState.Height - pb.Height) / 2);
            pb.Image = activeBitmap;
            pb.Visible = true;

            this.Controls.Add(pb);
            _PictureBoxes.Add(pb);

            _ImageList.Images.Add(inactiveBitmap);

            pb.BringToFront();

            RefreshImages();

            pb.Invalidate();
        }

        private void RefreshImages()
        {
            //Set Pictureboxes to inactive
            for (int i = 0; i < _PictureBoxes.Count - 1; i++)
                _PictureBoxes[i].Image = _ImageList.Images[i];
        }
    }
}