using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Trends
{
    /// <summary>
    /// Configured trend algorithm (see ConfigurationBase).
    /// </summary>
    [Name("Trend configuration")]
    [Serializable]
    internal sealed class ConfigurationTrend : ConfigurationBase<TrendBase, ArgsTrend, ResultTrend, SourceTracker>, IMatrixProvider
    {        
        public Vector CreateTrend( Core core, Vector vector )
        {
            double[] newValues = this.CreateTrend( vector.Observations, core.Conditions, core.Groups, vector.Values );
            IntensityMatrix temporary = new IntensityMatrix( new[] { vector.Peak }, core.Conditions.ToArray(), Vector.VectorToMatrix( newValues ) );
            return temporary.Vectors[0];
        }

        protected override SourceTracker GetTracker()
        {
            return new SourceTracker( this.Args );
        }

        internal double[] CreateTrend(IReadOnlyList<ObservationInfo> inOrder, IReadOnlyList<ObservationInfo> outOrder, IReadOnlyList<GroupInfo> typeInfo, IReadOnlyList< double> raw)
        {
            return this.Args.GetAlgorithmOrThrow().SmoothByType(inOrder, outOrder, typeInfo, raw, this.Args);
        }              

        public IntensityMatrix Provide => this.Results?.Matrix;

        protected override void OnRun( Core core, ProgressReporter prog )
        {         
            IntensityMatrix source = this.Args.SourceMatrix;
            double[,] results = null;

            for (int index = 0; index < source.NumRows; index++)
            {
                prog.SetProgress( index, source.NumRows );

                // Apply new trend
                // TODO: Should we be using core here?
                double[] r = this.CreateTrend( core.Observations, core.Conditions, core.Groups, source.Vectors[index] ); // obs

                if (results == null)
                {
                    results = new double[source.NumRows, r.Length];
                }

                ArrayHelper.CopyRow( r, results, index );
            }

            IntensityMatrix.RowHeader[] rows = source.Rows;
            IntensityMatrix.ColumnHeader[] cols = core.Conditions.Select( z => new IntensityMatrix.ColumnHeader( z ) ).ToArray();
            IntensityMatrix result = new IntensityMatrix( rows, cols, results );

            this.SetResults( new ResultTrend( result ) );
        }

        protected override Image ResultIcon => Resources.ListIconResultTrend;
        public ISpreadsheet ExportData()
        {
            return this.Provide?.ExportData();
        }
    }
}
