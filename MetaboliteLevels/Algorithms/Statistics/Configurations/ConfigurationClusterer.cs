using System;
using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Clusterers;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured clustering algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    class ConfigurationClusterer : ConfigurationBase<ClustererBase, ArgsClusterer, ResultClusterer>
    {
        public ConfigurationClusterer(string name, string comments, string clusterId, ArgsClusterer args)
            : base(name, comments, clusterId, args)
        {
            // NA
        }

        /// <summary>
        /// ACTION!
        /// </summary>
        internal ResultClusterer Cluster(Core core, int isPreview, IProgressReporter prog)
        {
            // Get results
            ValueMatrix vmatrix;
            DistanceMatrix dmatrix;
            ResultClusterer results = this.Cached.Calculate(core, isPreview, this.Args, this, prog, out vmatrix, out dmatrix);

            // Finalize statistics
            results.FinalizeResults(core, this.Args.Distance, vmatrix, dmatrix, this.Args.Statistics, prog);

            // Return results
            return results;
        }
    }
}
