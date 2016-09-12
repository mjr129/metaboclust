using System;
using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Clusterers;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured clustering algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationClusterer : ConfigurationBase<ClustererBase, ArgsClusterer, ResultClusterer>
    {         
        /// <summary>
        /// ACTION!
        /// </summary>
        internal ResultClusterer Cluster(Core core, int isPreview, ProgressReporter prog)
        {
            // Get results
            IntensityMatrix vmatrix;
            DistanceMatrix dmatrix;
            ResultClusterer results = this.GetAlgorithmOrThrow().ExecuteAlgorithm(core, isPreview, false, this.Args, this, prog, out vmatrix, out dmatrix);

            // Finalize statistics
            results.FinalizeResults(core, this.Args.Distance, vmatrix, dmatrix, this.Args.Statistics, prog);

            // Return results
            return results;
        }                       

        public override bool Run( Core core, ProgressReporter prog )
        {
            try
            {
                ResultClusterer results = this.Cluster( core, -1, prog );
                this.SetResults( results );
                return true;
            }
            catch (Exception ex)
            {                                
                this.SetError( ex );
                return false;
            }
        }
    }
}
