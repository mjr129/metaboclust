using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Trends;
using MetaboliteLevels.Data.DataInfo;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured trend algorithm (see ConfigurationBase).
    /// </summary>
    [Name("Trend configuration")]
    [Serializable]
    sealed class ConfigurationTrend : ConfigurationBase<TrendBase, ArgsTrend, ResultTrend, SourceTracker>, IMatrixProvider
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

            IntensityMatrix result = new IntensityMatrix( source.Rows, source.Columns, results );

            SetResults( new ResultTrend( result ) );
        }
    }
}
