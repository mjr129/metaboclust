using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Evaluation
{
    /// <summary>
    /// Points to a clustering configuration OR result.
    /// 
    /// This either holds the filename where the test is saved (in the case of completed tests),
    /// or the test object itself (prior to competion - the results of the test object will be empty
    /// at this stage).
    /// </summary>
    [Serializable]
    class ClusterEvaluationPointer : Visualisable
    {
        /// <summary>
        /// For tests run: Where the results (ClusterEvaluationConfigurationResults) are stored
        /// </summary>
        [XColumn("File")]
        public readonly string FileName;

        /// <summary>
        /// The configuration (older versions only had this for tests not yet run, but now we keep
        /// a copy for tests run as well)
        /// </summary>
        [XColumn(EColumn.Decompose)]
        public readonly ClusterEvaluationConfiguration Configuration;                 

        /// <summary>
        /// CONSTRUCTOR
        /// From newly stored results and imported files
        /// </summary>       
        public ClusterEvaluationPointer(string fileName, ClusterEvaluationConfiguration configuration)
        {                                                                   
            this.FileName = fileName;
            this.Configuration = configuration;
        }

        /// <summary>
        /// CONSTRUCTOR
        /// From imported files with unknown data
        /// </summary>       
        public ClusterEvaluationPointer(string fileName)
        {                                                                   
            this.FileName = fileName;    
        }

        /// <summary>
        /// CONSTRUCTOR
        /// For a new test without results
        /// </summary>    
        internal ClusterEvaluationPointer(ClusterEvaluationConfiguration configuration)
        {                            
            this.Configuration = configuration;
        }

        /// <summary>
        /// Does this have any results?
        /// </summary>
        [XColumn(  )]
        public bool HasResults
        {
            get
            {
                return FileName != null;
            }
        }      

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override string DefaultDisplayName => Configuration.ClustererConfiguration.DisplayName;

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override Image Icon
        {
            get
            {
                if (this.HasResults)
                {
                    return Resources.ListIconResultVector;
                }
                else
                {
                    return Resources.ListIconResultPending;
                }
            }
        } 
    }
}
