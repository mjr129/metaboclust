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
        public readonly IVisualisable Primary;
        public readonly IVisualisable Secondary;  

        public VisualisableSelection(IVisualisable primary, IVisualisable secondary)
        {
            Primary = primary;
            Secondary = secondary;   
        }

        public VisualisableSelection(IVisualisable primary)
        {
            Primary = primary;
            Secondary = null;    
        }

        public override string ToString()
        {
            if (Primary == null)
            {
                return "No selection";
            }
            else if (Secondary == null)
            {
                return Primary.DisplayName;
            }
            else
            {
                return Primary.DisplayName + "::" + Secondary.DisplayName;
            }
        }
    }                            
}
