using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session;

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
        public readonly Guid CoreGuid;
        public readonly ClusterEvaluationConfiguration Configuration;
        public readonly List<ClusterEvaluationParameterResult> Results;

        public ClusterEvaluationResults(Core core, ClusterEvaluationConfiguration Configuration, List<ClusterEvaluationParameterResult> Results)
        {
            this.CoreGuid = core.CoreGuid;
            this.Configuration = Configuration;
            this.Results = Results;
        }

        public override string ToString()
        {
            return Configuration.ToString();
        }
    }
}
