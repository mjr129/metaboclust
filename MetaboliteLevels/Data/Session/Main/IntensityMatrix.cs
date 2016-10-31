using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.General;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Main
{         
    [Serializable]
    class IntensityMatrix : IExportProvider
    {
        public readonly double[,] Values;
        public readonly RowHeader[] Rows;
        public readonly ColumnHeader[] Columns;

        public int NumRows => this.Rows.Length;
        public int NumVectors => this.Rows.Length;
        public int NumCols => this.Columns.Length;

        public IntensityMatrix( Peak[] rows, ObservationInfo[] columns, double[,] values )
            : this(   rows.Select( z => new RowHeader( z, null ) ).ToArray(),
                  columns.Select( z => new ColumnHeader( z ) ).ToArray(),
                  values )

        {
            // NA
        }

        public override string ToString()
        {
            return this.NumRows + " vectors of " + this.NumCols + " values";
        }

        public ISpreadsheet ExportData(  )
        {
            Spreadsheet<double> r = new Spreadsheet<double>( this.NumRows, this.NumCols );

            r.ColNames.Set( this.Columns.Select( z => z.ToString() ) );
            r.RowNames.Set( this.Rows.Select( z => z.ToString() ) );

            for (int row = 0; row < this.NumRows; ++row)
            {
                for (int col = 0; col < this.NumCols; ++col)
                {
                    r[row, col] = this.Values[row,col];
                }
            }

            return r;
        }

        public IntensityMatrix( RowHeader[] rows, ColumnHeader[] columns, double[,] values )
        {
            Debug.Assert( rows.Length == values.GetLength(0), "IntensityMatrix number of rows mismatch." );
            Debug.Assert( columns.Length == values.GetLength(1), "IntensityMatrix number of columns mismatch." );
                                   
            this.Rows = rows;
            this.Columns = columns;
            this.Values = values;
        }

        internal IntensityMatrix Subset( PeakFilter peakFilter, ObsFilter columnFilter, ESubsetFlags flags )
        {
            return this.Subset( peakFilter != null ? (Predicate<Peak>)peakFilter.Test : null,
                columnFilter != null ? (Predicate<ObservationInfo>)columnFilter.Test : null, 
                flags );
        }

        internal IntensityMatrix Subset( Predicate<Peak> peakFilter, Predicate<ObservationInfo> columnFilter, ESubsetFlags flags )
        {
            // Get the ROWS involved in the subset
            int[] rowIndices;

            if (flags.Has(ESubsetFlags.InvertPeakFilter))
            {
                // Inverted filter
                if (peakFilter == null)
                {
                    rowIndices = new int[0];
                }
                else
                {
                    rowIndices = this.Rows.Which( z => !peakFilter( z.Peak ) ).ToArray();
                }
            }
            else
            {
                if (peakFilter == null)
                {
                    rowIndices = this.Rows.Indices().ToArray();
                }
                else
                {
                    rowIndices = this.Rows.Which( z => peakFilter( z.Peak ) ).ToArray();
                }
            }

            // Get the ROW HEADERS
            RowHeader[] newRows = this.Rows.At( rowIndices ).ToArray();
                                  
            // Get the column indices
            int[] colIndices = (columnFilter !=null)? this.Columns.Which( z => columnFilter( z.Observation ) ).ToArray() : this.Columns.Indices().ToArray();

            // Get the COLUMN HEADERS
            ColumnHeader[] newCols = this.Columns.At( colIndices ).ToArray();

            // Get the VALUES
            double[,] newValues = new double[rowIndices.Length, colIndices.Length];

            for (int row = 0; row < rowIndices.Length; ++row)
            {
                int origRowIndex = rowIndices[row];

                for(int col = 0; col < colIndices.Length; ++col)
                {
                    int origColIndex = colIndices[col];

                    newValues[row, col] = Values[origRowIndex, origColIndex];
                }
            }                                                                                   

            // Return the result
            return new IntensityMatrix( newRows, newCols, newValues );
        }

        /// <summary>
        /// Finds the FIRST vector representing the selected peak (or NULL if it does not exist).
        /// </summary>                                                                     
        internal Vector Find( Peak peak )
        {
            int index = this.FindIndex( new RowHeader( peak, null ) );

            if (index == -1)
            {
                return null;
            }

            return new Vector( this, index );
        }

        internal int FindIndex( RowHeader row )
        {
            return this.Rows.FirstIndexWhere( z=> z.Peak==row.Peak && z.Group==row.Group ); // TODO: Implement and use Equals
        }

        public VectorCollection Vectors => new VectorCollection( this );

        public bool HasSplitGroups => this.Rows.Length != 0 && this.Rows[0].Group != null;

        public bool IsTrend => this.Columns.Length != 0 && this.Columns[0].Observation.Acquisition == null;

        public IEnumerable<double> AllValues => this.Values.Cast<double>();

        /// <summary>
        /// TODO: Horrible workaround, remove it!
        /// </summary>
        public class VectorCollection : IEnumerable<Vector>
        {
            private IntensityMatrix _owner;

            public VectorCollection( IntensityMatrix intensityMatrix )
            {
                this._owner = intensityMatrix;
            }
                        
            public Vector this[int rowIndex]
            {
                get { return new Vector( this._owner, rowIndex ); } // TODO: This is okay if it's a one-off, but shouldn't be used all the time
            }

            public IEnumerator<Vector> GetEnumerator() // TODO: This is okay if it's a one-off, but shouldn't be used all the time
            {
                for (int n = 0; n < this._owner.NumRows; n++)
                {
                    yield return this[n];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public override string ToString()
            {
                return $"Vectors of {{{this._owner}}}";
            }
        }

        [Serializable]
        public class RowHeader
        {
            public readonly Peak Peak;
            public readonly GroupInfo Group;

            public RowHeader( Peak peak, GroupInfo groupInfo )
            {
                this.Peak = peak;
                this.Group = groupInfo;
            }

            public override string ToString()
            {
                if (this.Group == null)
                {
                    return this.Peak.ToString();
                }
                else
                {
                    return this.Peak.ToString() + "∩" + this.Group.ToString();
                }
            }
        }

        [Serializable]
        public class ColumnHeader
        {
            public readonly ObservationInfo Observation;

            public ColumnHeader( ObservationInfo observation )
            {
                this.Observation = observation;
            }

            public override string ToString()
            {
                return this.Observation.ToString();
            }
        }

        public IntensityMatrix Provide => this;

        /// <summary>
        /// Creates a new intensity matrix with one row per experimental group
        /// </summary>                                  
        public IntensityMatrix SplitGroups()
        {
            // Get the groups involved
            HashSet<GroupInfo> groups = this.Columns.Select( z => z.Observation.Group ).Unique();

            // Create a fake group (since the new observations/columns will have no group)
            GroupInfo split = new GroupInfo( "*", -1, new Range( 0, 0 ), string.Join( ", ", groups ), string.Join( "", groups.Select( z => z.DisplayShortName ) ), -1 );
            int numNewRows = this.Rows.Length * groups.Count;

            // Write in sequence
            int newRow = 0;

            // Create the new value matrix -
            double[,] newValues = null;
            ColumnHeader[] colHeaders = null;
            RowHeader[] rowHeaders = new RowHeader[numNewRows];
            GroupInfo prevGroup = null; // Used for error messages

            // - Iterate groups
            foreach (GroupInfo group in groups)
            {
                // Get columns for this group
                int[] colIndices = this.Columns.WhichInOrder( λp => λp.Observation.Group == group, λc => λc.Observation.Time ).ToArray();
                ColumnHeader[] cols = this.Columns.At( colIndices ).ToArray();

                if (colHeaders == null)
                {
                    // First group creates the column headers and data matrix
                    colHeaders = new ColumnHeader[colIndices.Length];      

                    for (int col = 0; col < cols.Length; ++col)
                    {
                        colHeaders[col] = new ColumnHeader( new ObservationInfo( null, split, cols[col].Observation.Time ) );
                    }

                    newValues = new double[numNewRows, colHeaders.Length];
                }
                else
                {
                    // Subsequent groups just assert the data order matches
                    if (colHeaders.Length != cols.Length)
                    {
                        // User error (probably a missing filter)
                        throw new InvalidOperationException(
                            $"Attempt to SPLIT GROUPS on an intensity matrix but there are not the same number of TIME POINTS in each group. For instance {group} has {cols.Length} time points but {prevGroup} has {colHeaders.Length} time points. If SPLIT GROUPS is being used make sure to create a filter to ensure each group contains the same number of time points." );
                    }

                    for (int col = 0; col < cols.Length; ++col)
                    {
                        if (colHeaders[col].Observation.Time != cols[col].Observation.Time)
                        {
                            // User error (probably a weird filter) or program error (failure to order input data correctly)
                            throw new InvalidOperationException(
                            $"Attempt to SPLIT GROUPS on an intensity matrix but the TIME POINTS are not in the same order in each group. For instance {group} has time points {{{string.Join( ", ", cols.Select( z => z.Observation.Time ) )}}} but {prevGroup} has time points {{{string.Join( ", ", colHeaders.Select( z => z.Observation.Time ) ) }}}. If SPLIT GROUPS is being used make sure to create a filter to ensure each group contains the same time points." );
                        }
                    }
                }

                // Copy values
                for (int row = 0; row < this.NumRows; ++row)
                {   
                    rowHeaders[newRow] = new RowHeader( this.Rows[row].Peak, group );

                    for (int newCol = 0; newCol < colIndices.Length; ++newCol)
                    {
                        newValues[newRow, newCol] = this.Values[row, colIndices[newCol]];
                    }

                    ++newRow;
                }

                prevGroup = group;
            }

            // Set group range
            split.Range = Range.Find( colHeaders.Select( z => z.Observation.Time ) );

            // Return the result
            return new IntensityMatrix( rowHeaders, colHeaders, newValues );
        }
    }

    /// <summary>
    /// Flags for creating an intensity matrix subset
    /// </summary>
    [Flags]
    public enum ESubsetFlags
    {
        None,
        InvertPeakFilter
    }
}
