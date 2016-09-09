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

        public int Length => Values.Length;
        public IntensityMatrix.RowHeader Header => Source.Rows[RowIndex];
        public IntensityMatrix.ColumnHeader[] ColHeaders => Source.Columns;

        public Vector( IntensityMatrix source, int rowIndex )
        {
            Source = source;
            RowIndex = rowIndex;
        }

        /// <summary>
        /// Generates a new vector with a unique IntensityMatrix.
        /// For transient plots, etc. since its not efficient to create a new matrix for every peak.
        /// </summary>                                                                     
        public Vector( double[] values, IntensityMatrix.RowHeader row, IntensityMatrix.ColumnHeader[] cols)
        {
            Source = new IntensityMatrix( "single", null, new[] { row }, cols, new[] { values } );
            RowIndex = 0;
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

        /// <summary>
        /// Creates an intensity matrix containing this as its only observation
        /// Used for plots and previews, etc.
        /// </summary>                    
        public IntensityMatrix ToIntensityMatrix()
        {
            return new IntensityMatrix( "Single", null, new[] { Source.Rows[RowIndex] }, Source.Columns, new[] { Values } );
        }
    }
}
