using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Forms.Algorithms.ClusterEvaluation
{
    /// <summary>
    /// A configuration and results.
    /// 
    /// This is what is saved to the files.
    /// </summary>
    [Serializable]
    class ClusterEvaluationResults
    {
        public readonly ClusterEvaluationConfiguration Configuration;
        public readonly List<ClusterEvaluationParameterResult> Results;

        public ClusterEvaluationResults(ClusterEvaluationConfiguration Configuration, List<ClusterEvaluationParameterResult> Results)
        {
            this.Configuration = Configuration;
            this.Results = Results;
        }
    }
}
