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
        public readonly string Name;
        public readonly string Description;
        public readonly IAlgoParameterType Type;

        public AlgoParameter(string name, string description, IAlgoParameterType type)
        {
            Name = name;
            Description = description;
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
