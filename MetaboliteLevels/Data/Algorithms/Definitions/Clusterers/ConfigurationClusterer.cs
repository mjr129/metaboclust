using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers
{
    /// <summary>
    /// Configured clustering algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationClusterer : ConfigurationBase<ClustererBase, ArgsClusterer, ResultClusterer, SourceTracker>
    {         
        /// <summary>
        /// ACTION!
        /// </summary>
        internal ResultClusterer Cluster(Core core, int isPreview, ProgressReporter prog)
        {
            // Get results
            IntensityMatrix vmatrix;
            DistanceMatrix dmatrix;
            ResultClusterer results = this.Args.GetAlgorithmOrThrow().ExecuteAlgorithm(core, isPreview, false, this.Args, this, prog, out vmatrix, out dmatrix);

            // Finalize statistics
            results.FinalizeResults(core, this.Args.Distance, vmatrix, dmatrix, this.Args.Statistics, prog);

            // Return results
            return results;
        }

        protected override SourceTracker GetTracker()
        {
            return new SourceTracker( Args );
        }

        protected override void OnRun( Core core, ProgressReporter prog )
        {                      
            ResultClusterer results = this.Cluster( core, -1, prog );
            this.SetResults( results );
        }
    }
}
