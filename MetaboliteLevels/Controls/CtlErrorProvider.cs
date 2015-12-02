using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    class CtlErrorProvider
    {
        readonly ErrorProvider _ep = new ErrorProvider();

        internal void Clear()
        {
            _ep.Clear();
            _ep.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
        }

        public void ShowError(Control control, string message)
        {
            _ep.SetError(control, message);
            _ep.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
            _ep.SetIconPadding(control, 0);
        }
    }
}
