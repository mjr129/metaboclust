using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// Parameter (e.g. k for k-means)
    /// </summary>
    public class AlgoParameter
    {
        public readonly string Name; // name
        public readonly EAlgoParameterType Type; // type

        public AlgoParameter(string name, EAlgoParameterType type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Used in lists
        /// </summary>
        public override string ToString()
        {
            return Name + " (" + Type.ToUiString().ToSmallCaps() + ")";
        }
    }
}
