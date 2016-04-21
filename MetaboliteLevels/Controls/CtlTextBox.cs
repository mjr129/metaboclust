using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    /// <summary>
    /// A simple subclass of textbox that provides a watermark feature.
    /// 
    /// The watermark is displayed when no text is entered.
    /// </summary>
    class CtlTextBox : TextBox
    {
        private static readonly SolidBrush _watermarkBrush;
        private string _watermark;

        /// <summary>
        /// Static constructor
        /// </summary>
        static CtlTextBox()
        {
            _watermarkBrush = new SolidBrush(Color.FromKnownColor(KnownColor.GrayText));
        }

        /// <summary>
        /// Gets or sets the watermark.
        /// </summary>
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

        /// <summary>
        /// Override WndProc to draw the watermark
        /// </summary>              
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

                        g.DrawString(_watermark, Font, _watermarkBrush, 0, 0);
                    }
                }
                return;
            }
        }
    }
}
