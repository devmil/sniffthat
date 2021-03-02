//==========================================================================================
//
//		LameSoft.Mobile.Gps.Visual.GpsRadar
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
using System.Threading;

namespace LameSoft.Mobile.Gps.Visual
{
    public partial class GpsRadar : UserControl
    {
        private const int _CircleOffset = 5;

        private Color _RadarColor = Color.DarkGreen;

        private List<GpsRadarItem> _Positions = new List<GpsRadarItem>();

        private GpsPosition _Center = new GpsPosition(0, 0);

        private double _DirectionDegrees = 0;

        /// <summary>
        /// Gets or sets the direction degrees.
        /// </summary>
        /// <value>The direction degrees.</value>
        public double DirectionDegrees
        {
            get { return _DirectionDegrees; }
            set 
            { 
                _DirectionDegrees = value;
                this.Invoke(new ThreadStart(this.Invalidate));
            }
        }

        /// <summary>
        /// Scale for the Exp function
        /// </summary>
        private double _Scale = 1;

        /// <summary>
        /// The Range as base for measuring the Resolution
        /// </summary>
        private const double _ScaleRange = 0.9d;

        /// <summary>
        /// Gets or sets the center.
        /// </summary>
        /// <value>The center.</value>
        public GpsPosition Center
        {
            get 
            { 
                return _Center; 
            }
            set 
            { 
                _Center = value;
                this.Invoke(new ThreadStart(this.Invalidate));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsRadar"/> class.
        /// </summary>
        public GpsRadar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the resolution in km.
        /// </summary>
        /// <value>The resolution.</value>
        public double Resolution
        {
            get
            {
                return (-Math.Log(1 - _ScaleRange)) / _Scale;
            }
            set
            {
                _Scale = (-Math.Log(1 - _ScaleRange)) / value;
                this.Invoke(new ThreadStart(this.Invalidate));
            }
        }

        public void SetPositions(IList<GpsRadarItem> positions)
        {
            _Positions = new List<GpsRadarItem>(positions);
            _Positions.Sort(new GpsRadarItemComparer(_Center, false));
            this.Invoke(new ThreadStart(this.Invalidate));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap buffer = new Bitmap(this.Width, this.Height);
            Graphics bufferGraphics = Graphics.FromImage(buffer);

            bufferGraphics.Clear(this.BackColor);

            DrawRadarCircle(bufferGraphics);

            e.Graphics.DrawImage(buffer, 0, 0);

            base.OnPaint(e);

            bufferGraphics.Dispose();

            buffer.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        private Rectangle RadarRect
        {
            get
            {
                int rectLength = Math.Min(this.Width - 2 * _CircleOffset, this.Height - 2 * _CircleOffset);
                int xOffset = (this.Width - rectLength) / 2;
                int yOffset = (this.Height - rectLength) / 2;

                return new Rectangle(xOffset, yOffset, rectLength, rectLength);
            }
        }

        private Pen InsideRadarPen
        {
            get
            {
                return new Pen(Color.LawnGreen, 1);
            }
        }

        private void DrawRadarCircle(Graphics g)
        {
            //g.FillRectangle(new SolidBrush(_RadarColor), this.ClientRectangle);
            Rectangle r = RadarRect;
            g.FillEllipse(new SolidBrush(_RadarColor), r);
            
            int x1 = r.X + r.Width / 2;
            int x2 = x1;
            int y1 = r.Y;
            int y2 = r.Y + r.Height;
            g.DrawLine(InsideRadarPen, x1, y1, x2, y2);

            x1 = r.X;
            x2 = r.X + r.Width;
            y1 = r.Y + r.Height / 2;
            y2 = y1;
            g.DrawLine(InsideRadarPen, x1, y1, x2, y2);

            DrawDirection(g, _DirectionDegrees);

            g.DrawEllipse(InsideRadarPen, r);

            foreach (GpsRadarItem gpsp in new List<GpsRadarItem>(_Positions))
            {
                DrawPosition(g, gpsp);
            }
        }

        private void DrawDirection(Graphics g, double directionDegrees)
        {
            Rectangle r = RadarRect;

            double radArrow = Math.PI / 3;

            Point center = new Point(r.Left + r.Width / 2, r.Top + r.Height / 2);

            Point arrowEnd = new Point(center.X + r.Width / 6, center.Y);
            
            int arrowSideLength = r.Width / 12;

            Point arrowEndTop = new Point(arrowEnd.X - (int)(Math.Cos(radArrow / 2) * arrowSideLength), arrowEnd.Y - (int)(Math.Sin(radArrow / 2) * arrowSideLength));

            Point arrowEndBottom = new Point(arrowEndTop.X, arrowEnd.Y + (arrowEnd.Y - arrowEndTop.Y));

            double rotateRad = directionDegrees * Math.PI / 180;

            Point rotArrowEnd = Rotate(arrowEnd, center, rotateRad);

            g.DrawLine(InsideRadarPen, center.X, center.Y, rotArrowEnd.X, rotArrowEnd.Y);
            g.DrawLine(InsideRadarPen, center.X, center.Y, center.X - (rotArrowEnd.X - center.X), center.Y + (center.Y - rotArrowEnd.Y));

            g.DrawLines(InsideRadarPen,
                new Point[]
                {
                    Rotate(arrowEndTop, center, rotateRad),
                    rotArrowEnd,
                    Rotate(arrowEndBottom, center, rotateRad)
                });
        }

        private Point Rotate(Point p, Point center, double angle)
        {
            Point result = new Point();

            double sideLength = Math.Sqrt(Math.Pow(p.X - center.X, 2) + Math.Pow(p.Y - center.Y, 2));

            double startAngle = Math.Tan((double)(center.Y - p.Y) / (double)(p.X - center.X));

            result.X = center.X + (int)(Math.Cos(startAngle + angle) * sideLength);
            result.Y = center.Y - (int)(Math.Sin(startAngle + angle) * sideLength);

            return result;
        }

        private void DrawPosition(Graphics g, GpsRadarItem pos)
        {
            if ((_Center != null) && (!_Center.Equals(new GpsPosition(0,0))))
            {
                Rectangle r = RadarRect;

                double directkm = GpsHelper.GetDistance(_Center.Latitude, pos.Position.Latitude, _Center.Longitude, pos.Position.Longitude);

                double yKm = GpsHelper.GetDistance(_Center.Latitude, pos.Position.Latitude, _Center.Longitude, _Center.Longitude);
                double xKm = GpsHelper.GetDistance(_Center.Latitude, _Center.Latitude, _Center.Longitude, pos.Position.Longitude);

                yKm *= Math.Sign(pos.Position.Latitude - _Center.Latitude);
                xKm *= Math.Sign(pos.Position.Longitude - _Center.Longitude);

                double alpha = Math.Atan2(yKm, xKm);

                double scaledDirectPx = (1 - Math.Exp(-(_Scale * directkm))) * (double)(r.Width / 2);

                double diffX = Math.Cos(alpha) * scaledDirectPx;
                double diffY = Math.Sin(alpha) * scaledDirectPx;

                double y = r.Y + (double)r.Height / 2 - diffY;
                double x = r.X + (double)r.Width / 2 + diffX;

                SizeF stringSize = g.MeasureString(pos.Text, this.Font);
                double stringX = x - 3;
                double stringY = y + 7;

                if (stringX + stringSize.Width > this.ClientRectangle.Width)
                    stringX = r.Left + r.Width - stringSize.Width;

                Point[] ps = new Point[]
                {
                    new Point((int)x, (int)y),
                    new Point((int)x + 2, (int)y + 4),
                    new Point((int)x - 2, (int)y + 4)
                };

                if (stringY + stringSize.Height > this.ClientRectangle.Height)
                {
                    stringY = y - stringSize.Height - 8;
                    Point[] ps2 = new Point[3];
                    ps2[0] = ps[0];
                    ps2[1] = new Point((int)x + 2, (int)y - 4);
                    ps2[2] = new Point((int)x - 2, (int)y - 4);

                    ps = ps2;
                }

                g.FillPolygon(new SolidBrush(Color.Black), ps);
                g.DrawPolygon(InsideRadarPen, ps);

                Rectangle rect = new Rectangle((int)(stringX - 1), (int)(stringY - 1), (int)(stringSize.Width + 2), (int)(stringSize.Height + 2));

                g.FillRectangle(new SolidBrush(_RadarColor), rect);
                g.DrawRectangle(InsideRadarPen, rect);

                g.DrawString(pos.Text, this.Font, new SolidBrush(InsideRadarPen.Color), (float)stringX, (float)stringY);
            }
        }
    }

    internal class GpsRadarItemComparer : IComparer<GpsRadarItem>
    {
        GpsPosition _Center = null;
        bool _ShortestDistanceFirst;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsRadarItemComparer"/> class.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="shortestDistanceFirst">if set to <c>true</c> [shortest distance first].</param>
        internal GpsRadarItemComparer(GpsPosition center, bool shortestDistanceFirst)
        {
            _Center = center;
            _ShortestDistanceFirst = shortestDistanceFirst;
        }

        #region IComparer<GpsRadarItem> Members

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.
        /// </returns>
        public int Compare(GpsRadarItem x, GpsRadarItem y)
        {
            double distX = GpsHelper.GetDistance(_Center.Latitude, x.Position.Latitude, _Center.Longitude, x.Position.Longitude);
            double distY = GpsHelper.GetDistance(_Center.Latitude, y.Position.Latitude, _Center.Longitude, y.Position.Longitude);

            if(distX == distY)
                return 0;

            if (_ShortestDistanceFirst)
                return (distX < distY) ? -1 : 1;
            else
                return (distX > distY) ? -1 : 1;
        }

        #endregion
    }
}
