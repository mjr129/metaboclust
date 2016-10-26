using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Configurations
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
            double[] newValues = CreateTrend( vector.Observations, core.Conditions, core.Groups, vector.Values );
            IntensityMatrix temporary = new IntensityMatrix( new[] { vector.Peak }, core.Conditions.ToArray(), new[] { newValues } );
            return temporary.Vectors[0];
        }

        protected override SourceTracker GetTracker()
        {
            return new SourceTracker( Args );
        }

        internal double[] CreateTrend(IReadOnlyList<ObservationInfo> inOrder, IReadOnlyList<ObservationInfo> outOrder, IReadOnlyList<GroupInfo> typeInfo, double[] raw)
        {
            return Args.GetAlgorithmOrThrow().SmoothByType(inOrder, outOrder, typeInfo, raw, this.Args);
        }              

        public IntensityMatrix Provide => Results.Matrix;

        protected override void OnRun( Core core, ProgressReporter prog )
        {         
            IntensityMatrix source = this.Args.SourceMatrix;
            double[][] results = new double[source.NumRows][];

            for (int index = 0; index < source.NumRows; index++)
            {
                prog.SetProgress( index, source.NumRows );

                // Apply new trend
                // TODO: Should we be using core. here?
                results[index] = this.CreateTrend( core.Observations, core.Conditions, core.Groups, source.Values[index] ); // obs
            }

            IntensityMatrix.RowHeader[] rows = source.Rows;
            IntensityMatrix.ColumnHeader[] cols = core.Conditions.Select( z => new IntensityMatrix.ColumnHeader( z ) ).ToArray();
            IntensityMatrix result = new IntensityMatrix( rows, cols, results );

            SetResults( new ResultTrend( result ) );
        }

        protected override Image ResultIcon => Resources.ListIconResultTrend;
        public ISpreadsheet ExportData()
        {
            return Provide?.ExportData();
        }
    }
}
