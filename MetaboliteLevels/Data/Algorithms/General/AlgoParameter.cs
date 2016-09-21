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
    internal class AlgoParameter
    {
        public readonly string Name; // name
        public readonly IAlgoParameterType Type; // type

        public AlgoParameter(string name, IAlgoParameterType type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Used in lists
        /// </summary>
        public override string ToString()
        {
            return Name + " (" + Type + ")";
        }
    }
}
