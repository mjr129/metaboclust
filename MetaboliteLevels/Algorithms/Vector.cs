using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Algorithms
{
    /// <summary>
    /// An input vector
    /// </summary>
    [Serializable]
    internal class Vector
    {
        public readonly Peak Peak;                          // Where I come from
        public readonly GroupInfo Group;                    // Where I come from (can be null)
        public readonly ConditionInfo[] Conditions;         // What each of the values represent (only one of Conditions or Observations is set)
        public readonly ObservationInfo[] Observations;     // What each of the values represent (only one of Conditions or Observations is set)
        public readonly double[] Values;                    // The values in the vector
        public readonly int Index;                          // My index in the ValueMatrix

        public Vector(Peak peak, GroupInfo group, ConditionInfo[] conditions, ObservationInfo[] observations, double[] values, int index)
        {
            Peak = peak;
            Group = group;
            Conditions = conditions;
            Observations = observations;
            Values = values;
            Index = index;
        }

        internal bool DiffGroups(Vector vector)
        {
            return Conditions != vector.Conditions || Observations != vector.Observations;
        }

        public override string ToString()
        {
            if (Group == null)
            {
                return Peak.DisplayName;
            }
            else
            {
                return Peak.DisplayName + "∩" + Group.Name;
            }
        }
    }
}
