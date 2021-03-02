//==========================================================================================
//
//		LameSoft.Mobile.Gps.Visual.SatelliteViewer
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LameSoft.Mobile.Gps;

namespace LameSoft.Mobile.Gps.Visual
{
    public partial class SatelliteViewer : UserControl
    {
        public SatelliteViewer()
        {
            InitializeComponent();
            _Satellites = new List<GpsSatellite>();
        }

        private IList<GpsSatellite> _Satellites;

        public void SetSatellites(IList<GpsSatellite> satellites)
        {
            _Satellites = new List<GpsSatellite>();
            foreach (GpsSatellite sat in satellites)
            {
                if (Convert.ToInt32(sat.PRN) > -1)
                    _Satellites.Add(sat);
            }

            (_Satellites as List<GpsSatellite>).Sort(new SatelliteComparer());

            this.Refresh();
        }

        private Rectangle FrameRect
        {
            get
            {
                return new Rectangle(this.ClientRectangle.X + 1, this.ClientRectangle.Y + 1, this.ClientRectangle.Width -2, this.ClientRectangle.Height - 2);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap doubleBuffer = new Bitmap(this.Width, this.Height);

            Graphics g = Graphics.FromImage(doubleBuffer);
            g.Clear(this.BackColor);

            if (_Satellites.Count <= 0)
                DrawEmpty(g);
            else
            {
                DrawFrame(g, FrameRect);
                int currLeft = 2;
                int satWidth = ((this.Width - 4) / _Satellites.Count);
                foreach (GpsSatellite s in _Satellites)
                {
                    DrawSatellite(s, satWidth, currLeft, g);
                    currLeft += satWidth;
                }
            }

            e.Graphics.DrawImage(doubleBuffer, 0, 0);

            g.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        private Brush _ActiveFillBrush = new SolidBrush(Color.DarkGreen);
        private Brush _InActiveFillBrush = new SolidBrush(Color.LightGray);

        private Brush _ActiveTextBrush = new SolidBrush(Color.LightGray);
        private Brush _InActiveTextBrush = new SolidBrush(Color.Black);

        private void DrawSatellite(GpsSatellite s, int satWidth, int currLeft, Graphics graphics)
        {
            int offset = 5;
            Rectangle r = new Rectangle(currLeft, offset, satWidth, this.Height - 2*offset);
            DrawFrame(graphics, r, Color.LightGray);
            

            int fillHeight = Math.Min((r.Height * (s.SNR)) / 60, r.Height);

            Brush b = (s.Active ? _ActiveFillBrush : _InActiveFillBrush);
            //Brush bText = (s.Active ? _ActiveTextBrush : _InActiveTextBrush);
            Brush bText = _InActiveTextBrush;

            graphics.FillRectangle(b, r.Left + 1, r.Top + r.Height - fillHeight, r.Width - 2, fillHeight);

            SizeF stringSize = graphics.MeasureString(s.PRN, this.Font);

            Rectangle stringRect = new Rectangle();

            stringRect.Width = (int)Math.Ceiling(stringSize.Width);
            stringRect.Height = (int)Math.Ceiling(stringSize.Height);

            stringRect.X = r.Left + (int)((r.Width - stringRect.Width) / 2);
            stringRect.Y = r.Top + (int)((r.Height - stringRect.Height) / 2);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //RectangleF rf = new RectangleF(r.X, r.Y, r.Width, r.Height);
            RectangleF rf = new RectangleF(stringRect.X, stringRect.Y, stringRect.Width, stringRect.Height);

            stringRect.Inflate(2, 2);
            //stringRect.X -= 1;
            //stringRect.Y -= 1;

            graphics.FillRectangle(new SolidBrush(Color.White), stringRect);
            
            graphics.DrawString(s.PRN, this.Font, bText, rf, sf);

            DrawFrame(graphics, stringRect, Color.Black);
        }

        private void DrawEmpty(Graphics graphics)
        {
            DrawFrame(graphics, FrameRect);
            RectangleF rf = new RectangleF(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            graphics.DrawString("No Satellites in view...", this.Font, new SolidBrush(this.ForeColor), rf, sf);
        }

        private void DrawFrame(Graphics graphics, Rectangle rectangle, Color color)
        {
            graphics.DrawRectangle(new Pen(color), rectangle);
        }

        private void DrawFrame(Graphics graphics, Rectangle rectangle)
        {
            DrawFrame(graphics, rectangle, Color.Black);
        }
    }

    class SatelliteComparer : IComparer<GpsSatellite>
    {
        #region IComparer<GpsSatellite> Members

        public int Compare(GpsSatellite x, GpsSatellite y)
        {
            return Convert.ToInt32(x.PRN) - Convert.ToInt32(y.PRN);
        }

        #endregion
    }
}
