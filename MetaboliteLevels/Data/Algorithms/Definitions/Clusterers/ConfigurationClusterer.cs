using System;
using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Clusterers;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured clustering algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationClusterer : ConfigurationBase<ClustererBase, ArgsClusterer, ResultClusterer>
    {
        public ConfigurationClusterer(string name, string comments, string clusterId, ArgsClusterer args)
            : base(name, comments, clusterId, args)
        {
            // NA
        }

        /// <summary>
        /// ACTION!
        /// </summary>
        internal ResultClusterer Cluster(Core core, int isPreview, ProgressReporter prog)
        {
            // Get results
            ValueMatrix vmatrix;
            DistanceMatrix dmatrix;
            ResultClusterer results = this.Cached.ExecuteAlgorithm(core, isPreview, false, this.Args, this, prog, out vmatrix, out dmatrix);

            // Finalize statistics
            results.FinalizeResults(core, this.Args.Distance, vmatrix, dmatrix, this.Args.Statistics, prog);

            // Return results
            return results;
        }

        protected sealed override IEnumerable<Column> GetExtraColumns(Core core)
        {
            List<Column<ConfigurationClusterer>> columns = new List<Column<ConfigurationClusterer>>();

            columns.AddSubObject(core, "Arguments\\Distance", z => z.Args.Distance);
            columns.AddSubObject(core, "Arguments\\Observation filter", z => z.Args.ObsFilter);
            columns.AddSubObject(core, "Arguments\\Peak pilter", z => z.Args.PeakFilter);
            columns.Add("Arguments\\Parameters", z => z.Args.Parameters);
            columns.Add("Arguments\\Source mode", z => z.Args.SourceMode);
            columns.Add("Arguments\\Split groups", z => z.Args.SplitGroups);
            columns.Add("Arguments\\Statistics", z => z.Args.Statistics);

            return columns;
        }
    }
}
