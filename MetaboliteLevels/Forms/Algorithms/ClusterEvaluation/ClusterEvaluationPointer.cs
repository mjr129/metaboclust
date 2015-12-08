using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
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
    class ClusterEvaluationPointer : IVisualisable
    {
        public readonly string ConfigurationDescription;
        public readonly string ParameterDescription;

        public string OverrideDisplayName { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// For tests run: Where the results (ClusterEvaluationConfigurationResults) are stored
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// For tests not yet run: The configuration
        /// </summary>
        public readonly ClusterEvaluationConfiguration Configuration;

        public bool Enabled { get; set; }

        /// <summary>
        /// Woo, new results!
        /// </summary>       
        public ClusterEvaluationPointer(string fileName, ClusterEvaluationPointer source)
        {
            this.FileName = fileName;
            this.ConfigurationDescription = source.ConfigurationDescription;
            this.ParameterDescription = source.ParameterDescription;
            this.Enabled = true; // It must be or we wouldn't have got here!
        }

        /// <summary>
        /// Woo, new test!
        /// </summary>    
        internal ClusterEvaluationPointer(ClusterEvaluationConfiguration configuration)
        {
            this.Enabled = true;
            this.Configuration = configuration;
            this.ParameterDescription = configuration.ParamsAsString;
            this.ConfigurationDescription = configuration.ClustererConfiguration.ToString();
        }

        /// <summary>
        /// Do we have results?
        /// </summary>
        public bool HasResults
        {
            get
            {
                return FileName != null;
            }
        }

        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.GetDisplayName(OverrideDisplayName, DefaultDisplayName);
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public string DefaultDisplayName
        {
            get
            {
                return ConfigurationDescription;
            }
        }

        public VisualClass VisualClass
        {
            get
            {
                return VisualClass.None;
            }
        }

        public UiControls.ImageListOrder GetIcon()
        {
            if (this.HasResults)
            {
                return UiControls.ImageListOrder.TestFull;
            }
            else
            {
                return UiControls.ImageListOrder.TestEmpty;
            }
        }

        public IEnumerable<InfoLine> GetInformation(Core core)
        {
            return null;
        }

        public IEnumerable<InfoLine> GetStatistics(Core core)
        {
            return null;
        }

        public IEnumerable<Column> GetColumns(Core core)
        {
            List<Column<ClusterEvaluationPointer>> ptr = new List<Column<ClusterEvaluationPointer>>();

            ptr.Add("Name", true, z => z.DisplayName);
            ptr.Add("Configuration", false, z => z.ConfigurationDescription);
            ptr.Add("Parameters", false, z => z.ParameterDescription);
            ptr.Add("File", false, z => z.FileName);

            return ptr;
        }

        public void RequestContents(ContentsRequest list)
        {
            // NA
        }
    }
}
