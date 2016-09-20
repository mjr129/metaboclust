using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Singular;

namespace MetaboliteLevels.Data.Evaluation
{
    /// <summary>
    /// A configuration and results.
    /// 
    /// This is what is saved to the files.
    /// </summary>
    [Serializable]
    class ClusterEvaluationResults
    {
        /// <summary>
        /// GUID of the core used to create these results
        /// </summary>
        public readonly Guid CoreGuid;

        /// <summary>
        /// The test configuration
        /// </summary>
        public readonly ClusterEvaluationConfiguration Configuration;

        /// <summary>
        /// The list of results
        /// </summary>
        public readonly List<ClusterEvaluationParameterResult> Results;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary> 
        public ClusterEvaluationResults(Core core, ClusterEvaluationConfiguration configuration, List<ClusterEvaluationParameterResult> results)
        {
            this.CoreGuid = core.CoreGuid;
            this.Configuration = configuration;
            this.Results = results;
        }

        /// <summary>
        /// OVERRIDES Object
        /// </summary>         
        public override string ToString()
        {
            return Configuration.ToString();
        }
    }
}
