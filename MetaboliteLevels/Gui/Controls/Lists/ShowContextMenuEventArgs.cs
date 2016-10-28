using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Gui.Controls.Lists
{
    class ShowContextMenuEventArgs : EventArgs
    {
        public readonly object Selection;
        public readonly Control Control;
        public readonly int X;
        public readonly int Y;

        public ShowContextMenuEventArgs(object selection, Control control, int x, int y)
        {
            this.Selection = selection;
            this.Control = control;
            this.X = x;
            this.Y = y;
        }
    }
}
