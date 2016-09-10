using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui.Helpers;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.Associational
{         
    [Serializable]
    [DeferSerialisation]
    class IntensityMatrix : IProvider<IntensityMatrix>
    {                           
        public readonly double[][] Values;
        public readonly RowHeader[] Rows;
        public readonly ColumnHeader[] Columns;
        public readonly Guid Guid;

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

        public IntensityMatrix( RowHeader[] rows, ColumnHeader[] columns, double[][] values )
        {
            Debug.Assert( rows.Length == values.Length, "IntensityMatrix number of rows mismatch." );
            Debug.Assert( values.Length == 0 || columns.Length == values[0].Length, "IntensityMatrix number of columns mismatch." );

            Guid = Guid.NewGuid();
            Rows = rows;
            Columns = columns;
            Values = values;
        }         

        internal IntensityMatrix Subset( PeakFilter peakFilter, ObsFilter columnFilter = null, bool splitGroups=false, bool invertPeakFilter =false)
        {
            return Subset( peakFilter != null ? (Predicate<Peak>)peakFilter.Test : null,
                columnFilter != null ? (Predicate<ObservationInfo>)columnFilter.Test : null, 
                splitGroups, 
                invertPeakFilter );
        }

        internal IntensityMatrix Subset( Predicate<Peak> peakFilter, Predicate<ObservationInfo> columnFilter=null, bool splitGroups =false, bool invertPeakFilter=false )
        {
            int[] rowIndices;

            if (invertPeakFilter)
            {
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

            RowHeader[] newRows = Rows.At( rowIndices ).ToArray();

            if (!splitGroups)
            {  
                int[] colIndices = Columns.Which( z => columnFilter( z.Observation ) ).ToArray();
                ColumnHeader[] newCols = Columns.At( colIndices ).ToArray();
                double[][] newValues = Values.At( rowIndices ).Select( z => z.At( colIndices ).ToArray() ).ToArray(); 

                return new IntensityMatrix( newRows, newCols, newValues );
            }
            else
            {
                var groups = this.Columns.Select( z => z.Observation.Group ).Unique();

                List<ColumnHeader> newCols = new List<ColumnHeader>();
                List<double[]> newValues = new List<double[]>();

                foreach (GroupInfo g in groups)
                {
                    int[] colIndices = Columns.Which( z => z.Observation.Group == g && columnFilter( z.Observation ) ).ToArray();

                    newCols.AddRange( Columns.At( colIndices ) );
                    newValues.AddRange( Values.At( rowIndices ).Select( z => z.At( colIndices ).ToArray() ) );
                }                                                                                   

                return new IntensityMatrix( newRows.ToArray(), newCols.ToArray(), newValues.ToArray() );
            }
        }

        internal Vector Find( Peak peak )
        {
            return new Vector( this, FindIndex( new RowHeader( peak, null ) ) );
        }

        internal int FindIndex( RowHeader row )
        {
            return Rows.FirstIndexWhere( z=> z.Peak==row.Peak && z.Group==row.Group ); // TODO: Implement and use Equals
        }

        public VectorCollection Vectors => new VectorCollection( this );

        public bool HasSplitGroups => Rows.Length != 0 && Rows[0].Group != null;

        public bool IsTrend => Columns.Length != 0 && Columns[0].Observation.Acquisition == null;

        /// <summary>
        /// TODO: Horrible workaround, remove it!
        /// </summary>
        public class VectorCollection : IEnumerable<MetaboliteLevels.Algorithms.Vector>
        {
            private IntensityMatrix owner;

            public VectorCollection( IntensityMatrix intensityMatrix )
            {
                this.owner = intensityMatrix;
            }
                        
            public MetaboliteLevels.Algorithms.Vector this[int rowIndex]
            {
                get { return new MetaboliteLevels.Algorithms.Vector( owner, rowIndex ); } // TODO: This is okay if it's a one-off, but shouldn't be used all the time
            }

            public IEnumerator<Vector> GetEnumerator() // TODO: This is okay if it's a one-off, but shouldn't be used all the time
            {
                for (int n = 0; n < owner.NumRows; n++)
                {
                    yield return this[n];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
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
        }

        [Serializable]
        public class ColumnHeader
        {
            public readonly ObservationInfo Observation;

            public ColumnHeader( ObservationInfo observation )
            {
                Observation = observation;
            }
        }

        public IntensityMatrix Provide => this;
    }
}
