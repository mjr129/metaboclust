using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Forms
{
    internal interface ISelectionHolder
    {
        VisualisableSelection Selection { get; set; }
    }

    internal class VisualisableSelection
    {
        public readonly IVisualisable A;
        public readonly IVisualisable B;
        public readonly EActivateOrigin Origin;

        public VisualisableSelection(IVisualisable a, IVisualisable b, EActivateOrigin origin)
        {
            A = a;
            B = b;
            Origin = origin;
        }

        public VisualisableSelection(IVisualisable a, EActivateOrigin origin)
        {
            A = a;
            B = null;
            Origin = origin;
        }

        public override string ToString()
        {
            if (A == null)
            {
                return "No selection";
            }
            else if (B == null)
            {
                return A.DisplayName;
            }
            else
            {
                return A.DisplayName + " (in " + B.DisplayName + ")";
            }
        }
    }

    internal enum EActivateOrigin
    {
        None, // lists, replot, core change
        External,
        ClusterPlot,
        TreeView,
    }
}
