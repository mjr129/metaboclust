using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// An input vector
    /// </summary>
    [Serializable]
    internal sealed class Vector
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
            if (rowIndex < 0 || rowIndex >= source.NumRows)
            {
                throw new InvalidOperationException( $"Attempt to create a vector reference where the rowIndex {{{rowIndex}}} is outside the source dimensions: {{{source}}}." );
            }

            Source = source;
            RowIndex = rowIndex;
        }

        /// <summary>
        /// Generates a new vector with a unique IntensityMatrix.
        /// For transient plots, etc. since its not efficient to create a new matrix for every peak.
        /// </summary>                                                                     
        public Vector( double[] values, IntensityMatrix.RowHeader row, IntensityMatrix.ColumnHeader[] cols)
        {
            Source = new IntensityMatrix( new[] { row }, cols, new[] { values } );
            RowIndex = 0;
        }

        public override string ToString()
        {
            return Header.ToString();
        }

        /// <summary>
        /// Creates an intensity matrix containing this as its only observation
        /// Used for plots and previews, etc.
        /// </summary>                    
        public IntensityMatrix ToIntensityMatrix()
        {
            return new IntensityMatrix(  new[] { Source.Rows[RowIndex] }, Source.Columns, new[] { Values } );
        }

        /// <summary>
        /// Since vectors are created on-demand they need to have a deep Equals.
        /// </summary>                                                          
        public override bool Equals( object obj )
        {
            Vector b = obj as Vector;

            if (b == null)
            {
                return false;
            }                      

            return b.Source == Source && b.RowIndex == RowIndex;
        }

        /// <summary>
        /// As implements Equals.
        /// </summary>           
        public override int GetHashCode()
        {
            return RowIndex << 16 ^ Source.GetHashCode();
        }
    }
}
