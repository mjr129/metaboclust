using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Types.General;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.Associational
{         
    [Serializable]
    class IntensityMatrix : IExportProvider
    {
        public readonly double[][] Values;
        public readonly RowHeader[] Rows;
        public readonly ColumnHeader[] Columns;

        public int NumRows => Rows.Length;
        public int NumVectors => Rows.Length;
        public int NumCols => Columns.Length;

        public IntensityMatrix( Peak[] rows, ObservationInfo[] columns, double[][] values )
            : this(   rows.Select( z => new RowHeader( z, null ) ).ToArray(),
                  columns.Select( z => new ColumnHeader( z ) ).ToArray(),
                  values )

        {
            // NA
        }

        public override string ToString()
        {
            return NumRows + " vectors of " + NumCols + " values";
        }

        public ISpreadsheet ExportData(  )
        {
            Spreadsheet<double> r = new Spreadsheet<double>( NumRows, NumCols );

            r.ColNames.Set( Columns.Select( z => z.ToString() ) );
            r.RowNames.Set( Rows.Select( z => z.ToString() ) );

            for (int row = 0; row < NumRows; ++row)
            {
                for (int col = 0; col < NumCols; ++col)
                {
                    r[row, col] = Values[row][col];
                }
            }

            return r;
        }

        public IntensityMatrix( RowHeader[] rows, ColumnHeader[] columns, double[][] values )
        {
            Debug.Assert( rows.Length == values.Length, "IntensityMatrix number of rows mismatch." );
            Debug.Assert( values.Length == 0 || columns.Length == values[0].Length, "IntensityMatrix number of columns mismatch." );
                                   
            Rows = rows;
            Columns = columns;
            Values = values;
        }

        internal IntensityMatrix Subset( PeakFilter peakFilter, ObsFilter columnFilter, ESubsetFlags flags )
        {
            return Subset( peakFilter != null ? (Predicate<Peak>)peakFilter.Test : null,
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
                    rowIndices = Rows.Which( z => !peakFilter( z.Peak ) ).ToArray();
                }
            }
            else
            {
                if (peakFilter == null)
                {
                    rowIndices = Rows.Indices().ToArray();
                }
                else
                {
                    rowIndices = Rows.Which( z => peakFilter( z.Peak ) ).ToArray();
                }
            }

            // Get the ROW HEADERS
            RowHeader[] newRows = Rows.At( rowIndices ).ToArray();
                                  
            // Get the column indices
            int[] colIndices = (columnFilter !=null)? Columns.Which( z => columnFilter( z.Observation ) ).ToArray() : Columns.Indices().ToArray();

            // Get the COLUMN HEADERS
            ColumnHeader[] newCols = Columns.At( colIndices ).ToArray();

            // Get the VALUES
            double[][] newValues = Values.At( rowIndices ).Select( z => z.At( colIndices ).ToArray() ).ToArray(); 

            // Return the result
            return new IntensityMatrix( newRows, newCols, newValues );
        }

        /// <summary>
        /// Finds the FIRST vector representing the selected peak (or NULL if it does not exist).
        /// </summary>                                                                     
        internal Vector Find( Peak peak )
        {
            int index = FindIndex( new RowHeader( peak, null ) );

            if (index == -1)
            {
                return null;
            }

            return new Vector( this, index );
        }

        internal int FindIndex( RowHeader row )
        {
            return Rows.FirstIndexWhere( z=> z.Peak==row.Peak && z.Group==row.Group ); // TODO: Implement and use Equals
        }

        public VectorCollection Vectors => new VectorCollection( this );

        public bool HasSplitGroups => Rows.Length != 0 && Rows[0].Group != null;

        public bool IsTrend => Columns.Length != 0 && Columns[0].Observation.Acquisition == null;

        public IEnumerable<double> AllValues => Values.SelectMany( z => z );

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
                get { return new Vector( _owner, rowIndex ); } // TODO: This is okay if it's a one-off, but shouldn't be used all the time
            }

            public IEnumerator<Vector> GetEnumerator() // TODO: This is okay if it's a one-off, but shouldn't be used all the time
            {
                for (int n = 0; n < _owner.NumRows; n++)
                {
                    yield return this[n];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString()
            {
                return $"Vectors of {{{_owner}}}";
            }
        }

        [Serializable]
        public class RowHeader
        {
            public readonly Peak Peak;
            public readonly GroupInfo Group;

            public RowHeader( Peak peak, GroupInfo groupInfo )
            {
                Peak = peak;
                Group = groupInfo;
            }

            public override string ToString()
            {
                if (Group == null)
                {
                    return Peak.ToString();
                }
                else
                {
                    return Peak.ToString() + "∩" + Group.ToString();
                }
            }
        }

        [Serializable]
        public class ColumnHeader
        {
            public readonly ObservationInfo Observation;

            public ColumnHeader( ObservationInfo observation )
            {
                Observation = observation;
            }

            public override string ToString()
            {
                return Observation.ToString();
            }
        }

        public IntensityMatrix Provide => this;

        /// <summary>
        /// Creates a new intensity matrix with one row per experimental group
        /// </summary>                                  
        public IntensityMatrix SplitGroups()
        {
            HashSet<GroupInfo> groups = this.Columns.Select( z => z.Observation.Group ).Unique();
            GroupInfo split = new GroupInfo( "*", -1, new Range( 0, 0 ), string.Join( ", ", groups ), string.Join( "", groups.Select( z => z.DisplayShortName ) ), -1 );
            double[][] values = new double[this.Rows.Length * groups.Count][];
            RowHeader[] rowHeaders = new RowHeader[values.Length];
            ColumnHeader[] colHeaders = null;
            GroupInfo prevGroup = null;

            int n = 0;

            foreach (GroupInfo group in groups)
            {
                int[] colIndices = this.Columns.WhichInOrder( λp => λp.Observation.Group == group, λc => λc.Observation.Time ).ToArray();
                ColumnHeader[] cols = this.Columns.At( colIndices ).ToArray();

                if (colHeaders == null)
                {
                    colHeaders = new ColumnHeader[colIndices.Length];
                    prevGroup = group;

                    for (int col = 0; col < cols.Length; ++col)
                    {
                        colHeaders[col] = new ColumnHeader( new ObservationInfo( null, split, cols[col].Observation.Time ) );
                    }
                }
                else
                {
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

                for (int row = 0; row < NumRows; ++row)
                {
                    values[n] = this.Values[row].At( colIndices ).ToArray();
                    rowHeaders[n] = new RowHeader( this.Rows[row].Peak, group );
                    ++n;
                }
            }

            split.Range = Range.Find( colHeaders.Select( z => z.Observation.Time ) );

            return new IntensityMatrix( rowHeaders, colHeaders, values );
        }
    }

    [Flags]
    public enum ESubsetFlags
    {
        None,
        InvertPeakFilter
    }
}
