using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSerialisers;

namespace MetaboliteLevels.Forms.Algorithms.ClusterEvaluation
{
    /// <summary>
    /// Points to a clustering configuration OR result.
    /// 
    /// This either holds the filename where the test is saved (in the case of completed tests),
    /// or the test object itself (prior to competion - the results of the test object will be empty
    /// at this stage).
    /// </summary>
    [Serializable]
    class ClusterEvaluationPointer
    {
        public readonly string ConfigurationDescription;
        public readonly string TestParameterDescription;

        /// <summary>
        /// For tests run: Where the results (ClusterEvaluationConfigurationResults) are stored
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// For tests not yet run: The configuration
        /// </summary>
        public readonly ClusterEvaluationConfiguration Configuration;

        public ClusterEvaluationPointer(string fileName, ClusterEvaluationResults results)
        {
            this.FileName = fileName;
            this.ConfigurationDescription = results.Configuration.ParamsAsString;
            this.TestParameterDescription = results.Configuration.ClustererConfiguration.ToString();
        }

        internal ClusterEvaluationPointer(ClusterEvaluationConfiguration configuration)
        {
            this.Configuration = configuration;
            this.TestParameterDescription = configuration.ParamsAsString;
            this.ConfigurationDescription = configuration.ClustererConfiguration.ToString();
        }          

        public bool HasResults
        {
            get
            {
                return FileName != null;
            }
        }
    }
}
