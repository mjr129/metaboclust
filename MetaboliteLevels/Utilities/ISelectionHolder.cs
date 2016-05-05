using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MGui;
using MGui.Helpers;

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

        // OVERRIDE object
        public override string ToString()
        {
            if (Primary == null)
            {
                return "No selection";
            }
            else if (Secondary == null)
            {
                return Primary.GetType().Name.ToSmallCaps() + " " + Primary.DisplayName;
            }
            else
            {
                return Primary.GetType().Name.ToSmallCaps() + " " + Primary.DisplayName + " :: " + Secondary.GetType().Name.ToSmallCaps() + " " + Secondary.DisplayName;
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
