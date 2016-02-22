using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    class CtlTextBox : TextBox
    {
        private readonly SolidBrush _brush;
        private string _watermark;

        public CtlTextBox()
        {
            _brush = new SolidBrush(Color.FromKnownColor(KnownColor.GrayText));
        }

        public string Watermark
        {
            get
            {
                return _watermark;
            }
            set
            {
                _watermark = value;
                Invalidate();
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0xF)
            {
                if (!string.IsNullOrEmpty(_watermark) && string.IsNullOrEmpty(Text))
                {
                    using (Graphics g = Graphics.FromHwnd(Handle))
                    {
                        g.Clear(Color.White);

                        g.DrawString(_watermark, Font, _brush, 0, 0);
                    }
                }
                return;
            }
        }
    }
}
