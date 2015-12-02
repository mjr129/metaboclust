using System;
using System.Windows.Forms;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Viewers.Lists
{
    class ShowContextMenuEventArgs : EventArgs
    {
        public readonly IVisualisable selection;
        public readonly Control control;
        public readonly int x;
        public readonly int y;

        public ShowContextMenuEventArgs(IVisualisable selection, Control control, int x, int y)
        {
            this.selection = selection;
            this.control = control;
            this.x = x;
            this.y = y;
        }
    }
}
