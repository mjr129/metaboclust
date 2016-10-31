using System;
using System.Collections;
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
    /// Doesn't actually store the values, just references the source matrix and row index.
    /// </summary>
    [Serializable]
    internal sealed class Vector : IReadOnlyList<double>
    {
        // Data fields...
        public readonly IntensityMatrix Source;
        public readonly int RowIndex;

        // Wrapped stuff...
        public Peak Peak => Source.Rows[RowIndex].Peak;
        public GroupInfo Group => Source.Rows[RowIndex].Group;
        public ObservationInfo[] Observations => Source.Columns.Select( z => z.Observation ).ToArray();
        public IReadOnlyList<double> Values => this;
        public int Index => RowIndex;              
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
            double[,] valuesAsMatrix = VectorToMatrix( values );

            Source = new IntensityMatrix( new[] { row }, cols, valuesAsMatrix );
            RowIndex = 0;
        }

        public static double[,] VectorToMatrix( IReadOnlyList<double> values )
        {
            double[,] valuesAsMatrix = new double[1, values.Count];

            for (int col = 0; col < values.Count; ++col)
            {
                valuesAsMatrix[0, col] = values[col];
            }

            return valuesAsMatrix;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return new VectorEnumerator( this );
        }

        private class VectorEnumerator : IEnumerator<double>
        {
            private Vector vector;
            private int index;

            public VectorEnumerator( Vector vector )
            {
                this.index = -1;
                this.vector = vector;
            }

            public void Dispose()
            {
               // NA
            }

            public bool MoveNext()
            {
                ++index;
                return index < vector.Count;
            }

            public void Reset()
            {
                index = -1;
            }

            public double Current => vector[index];

            object IEnumerator.Current => this.Current;
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
            return new IntensityMatrix(  new[] { Source.Rows[RowIndex] }, Source.Columns, VectorToMatrix(this) );
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count => Source.NumCols;

        public double this[ int colIndex ] => Source.Values[RowIndex, colIndex];
    }
}
