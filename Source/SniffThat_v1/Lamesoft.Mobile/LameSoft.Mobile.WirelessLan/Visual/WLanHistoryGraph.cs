//==========================================================================================
//
//		LameSoft.SniffThat.Core.VisualContext
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
//      - 06.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * file added
//      - 07.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * graphical improvements
//          * removed useless code
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LameSoft.Mobile.WirelessLan.Visual
{
    public partial class WLanHistoryGraph : UserControl
    {
        private WLanHistory _History;
        private int _Range = 100;
        private int _FrameWidth = 1;
        private const int C_BADSTRENGTHBEGIN = 70;

        /// <summary>
        /// Initializes a new instance of the <see cref="WLanHistoryGraph"/> class.
        /// </summary>
        public WLanHistoryGraph()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        /// <value>The history.</value>
        public WLanHistory History
        {
            get
            {
                return _History;
            }
            set
            {
                lock (this)
                {
                    _History = value;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>The range.</value>
        public int Range
        {
            get
            {
                return _Range;
            }
            set
            {
                lock (this)
                {
                    _Range = value;
                }
                Invalidate();
            }
        }

        private Rectangle FrameRect
        {
            get
            {
                return new Rectangle(this.ClientRectangle.X + 1, this.ClientRectangle.Y + 1, this.ClientRectangle.Width - 2, this.ClientRectangle.Height - 2);
            }
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //NOOP, everything is drawn by user code
            //base.OnPaintBackground(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            Graphics graphics = Graphics.FromImage(bmp);

            graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

            graphics.DrawRectangle(new Pen(Color.Black, _FrameWidth), FrameRect);

            lock (this)
            {
                DrawGraph(graphics);
            }

            graphics.Dispose();

            e.Graphics.DrawImage(bmp, 0, 0);
            //base.OnPaint(e);
        }

        private void DrawGraph(Graphics graphics)
        {
            if (_History != null)
            {
                Dictionary<int, WLanHistoryEntry> valuesToDraw = GetStrengthValues();

                Point[] polygonPoints = GetPolygonPoints(valuesToDraw);

                Point[] good = GetUpperPolygonPoints(polygonPoints, C_BADSTRENGTHBEGIN);
                graphics.FillPolygon(new SolidBrush(Color.Green), good);
                Point[] bad = GetLowerPolygonPoints(polygonPoints, C_BADSTRENGTHBEGIN);
                graphics.FillPolygon(new SolidBrush(Color.Red), bad);

                //Curve
                graphics.DrawLines(new Pen(Color.Black, 1), polygonPoints);
            }
        }

        private Point[] GetLowerPolygonPoints(Point[] points, int percent)
        {
            if (points.Length <= 0)
                return new Point[0];

            int min = ((FrameRect.Height - 2 * _FrameWidth) * percent) / 100;

            List<Point> result = new List<Point>();
            foreach (Point p in points)
            {
                if (p.Y < min)
                    result.Add(new Point(p.X, min));
                else
                    result.Add(p);
            }
            return result.ToArray();
        }

        private Point[] GetUpperPolygonPoints(Point[] points, int percent)
        {
            if (points.Length <= 0)
                return new Point[0];

            int max = ((FrameRect.Height - 2 * _FrameWidth) * percent) / 100;

            Point start = new Point(points[0].X, max);
            Point end = new Point(points[points.Length - 1].X, max);
            List<Point> result = new List<Point>();
            result.Add(start);
            result.AddRange(points);
            result.Add(end);
            //go the way back
            for (int i = points.Length - 1; i >= 0; i--)
            {
                if (points[i].Y > max)
                    result.Add(points[i]);
                else
                    result.Add(new Point(points[i].X, max));
            }

            return result.ToArray();
        }

        private Point[] GetPolygonPoints(Dictionary<int, WLanHistoryEntry> valuesToDraw)
        {
            int availableHeight = this.ClientRectangle.Bottom - 2 * _FrameWidth;
            List<Point> result = new List<Point>();
            List<int> offsets = new List<int>(valuesToDraw.Keys);
            offsets.Sort(new Comparison<int>(
                delegate(int x1, int x2)
                {
                    return x1 - x2;
                }));

            for (int i = 0; i < offsets.Count; i++)
            {
                int x = offsets[i];
                int y = _FrameWidth + availableHeight - ScaleHeightFromStrength(availableHeight, valuesToDraw[offsets[i]]);
                result.Add(new Point(x, y));
            }

            //Go to bottom at both ends of the graph
            if (offsets.Count > 0)
            {
                result.Insert(0, new Point(offsets[0], _FrameWidth + availableHeight));

                //Ensure a point at the right end of the graph
                int availableWidth = FrameRect.Width - 2 * _FrameWidth;
                result.Add(new Point(_FrameWidth + availableWidth, result[result.Count - 1].Y));

                result.Add(new Point(_FrameWidth + availableWidth, _FrameWidth + availableHeight));
            }

            return result.ToArray();
        }

        private int ScaleHeightFromStrength(int available, WLanHistoryEntry entry)
        {
            if (!entry.Available)
                return 0;
            double s = 100 - Math.Abs(entry.Strength);
            if (s < 0)
                return 0;
            return (int)(Math.Min(1d, s / 100) * available);
        }

        private Dictionary<int, WLanHistoryEntry> GetStrengthValues()
        {
            Dictionary<int, WLanHistoryEntry> map = new Dictionary<int, WLanHistoryEntry>();

            lock (this)
            {
                DateTime firstVisibleDateTime = DateTime.Now - TimeSpan.FromSeconds(_Range);

                int numOfPixels = FrameRect.Width - 2 * _FrameWidth;

                foreach (WLanHistoryEntry entry in _History.Entries)
                {
                    if (entry.DateTime < firstVisibleDateTime)
                        continue;

                    TimeSpan offset = entry.DateTime - firstVisibleDateTime;
                    int pixelOffset = _FrameWidth + (int)(numOfPixels * (offset.TotalMilliseconds / (_Range * 1000)));

                    double absStrength = Math.Abs(entry.Strength);

                    if (!map.ContainsKey(pixelOffset))
                        map.Add(pixelOffset, entry);
                    else if (Math.Abs(map[pixelOffset].Strength) < Math.Abs(entry.Strength))
                        map[pixelOffset] = entry;
                }
            }

            return map;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
