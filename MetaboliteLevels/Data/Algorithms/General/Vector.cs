using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Algorithms
{
    /// <summary>
    /// An input vector
    /// </summary>
    [Serializable]
    internal class Vector
    {
        public readonly IntensityMatrix Source;
        public readonly int RowIndex;

        public Peak Peak => Source.Rows[RowIndex].Peak;
        public GroupInfo Group => Source.Rows[RowIndex].Group;
        public ObservationInfo[] Observations => Source.Columns.Select( z => z.Observation ).ToArray();
        public double[] Values => Source.Values[RowIndex];
        public int Index => RowIndex;

        public Vector( IntensityMatrix source, int rowIndex )
        {
            Source = source;
            RowIndex = rowIndex;
        }     

        public override string ToString()
        {
            if (Group == null)
            {
                return Peak.DisplayName;
            }
            else
            {
                return Peak.DisplayName + "∩" + Group.DisplayName;
            }
        }
    }
}
