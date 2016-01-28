using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    class CtlSplitter : SplitContainer
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // NA
        }

        protected override void OnPaint(PaintEventArgs e)
        {        
            var s = this;
            int gripLineWidth = 9;

            if (this.Orientation == Orientation.Horizontal)
            {
                e.Graphics.FillRectangle(SystemBrushes.ControlDark, s.SplitterRectangle.X, s.SplitterDistance, s.SplitterRectangle.Width, s.SplitterWidth);
                e.Graphics.DrawLine(Pens.LightSlateGray, s.SplitterRectangle.X, s.SplitterDistance, s.SplitterRectangle.Width, s.SplitterDistance);
                e.Graphics.DrawLine(Pens.LightSlateGray, s.SplitterRectangle.X, s.SplitterDistance + s.SplitterWidth - 1, s.SplitterRectangle.Width, s.SplitterDistance + s.SplitterWidth - 1);

                using (Pen pen = new Pen(Color.Black, 2))
                {
                    pen.DashStyle = DashStyle.Dot;

                    e.Graphics.DrawLine(pen, ((s.SplitterRectangle.Width / 2) - (gripLineWidth / 2)), s.SplitterDistance + s.SplitterWidth / 2, ((s.SplitterRectangle.Width / 2) + (gripLineWidth / 2)), s.SplitterDistance + s.SplitterWidth / 2);
                }
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.ControlDark, s.SplitterDistance, s.SplitterRectangle.Y, s.SplitterWidth, s.SplitterRectangle.Height);
                e.Graphics.DrawLine(Pens.LightSlateGray, s.SplitterDistance, s.SplitterRectangle.Y, s.SplitterDistance, s.SplitterRectangle.Height);
                e.Graphics.DrawLine(Pens.LightSlateGray, s.SplitterDistance + s.SplitterWidth - 1, s.SplitterRectangle.Y, s.SplitterDistance + s.SplitterWidth - 1, s.SplitterRectangle.Height);

                using (Pen pen = new Pen(Color.Black, 2))
                {
                    pen.DashStyle = DashStyle.Dot;

                    e.Graphics.DrawLine(pen, s.SplitterDistance + s.SplitterWidth / 2, ((s.SplitterRectangle.Height / 2) - (gripLineWidth / 2)), s.SplitterDistance + s.SplitterWidth / 2, ((s.SplitterRectangle.Height / 2) + (gripLineWidth / 2)));
                }
            }
        }
    }
}
