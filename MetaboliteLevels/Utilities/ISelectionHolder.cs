using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Utilities
{
    internal interface ISelectionHolder
    {
        VisualisableSelection Selection { get; set; }
    }       

    internal class VisualisableSelection
    {
        public readonly object Primary;
        public readonly object Secondary;

        public VisualisableSelection(object primary, object secondary )
        {
            Primary = primary;
            Secondary = secondary;
        }

        public VisualisableSelection( object primary )
        {
            Primary = primary;
            Secondary = null;
        }

        // OVERRIDE object
        public override string ToString()
        {
            if (Primary == null)
            {
                return "No selection";
            }
            else if (Secondary == null)
            {
                return Primary.GetType().Name.ToSmallCaps() + " " + Primary.ToString();
            }
            else
            {
                return Primary.GetType().Name.ToSmallCaps() + " " + Primary.ToString() + " :: " + Secondary.GetType().Name.ToSmallCaps() + " " + Secondary.ToString();
            }
        }

        // OVERRIDE object
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            VisualisableSelection vs = (VisualisableSelection)obj;

            return Primary == vs.Primary && Secondary == vs.Secondary;
        }

        // OVERRIDE object
        public override int GetHashCode()
        {
            return Primary.GetHashCode() % Secondary.GetHashCode();
        }
    }
}
