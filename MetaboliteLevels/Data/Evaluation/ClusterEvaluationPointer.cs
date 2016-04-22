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
        /// <summary>
        /// For tests run: Where the results (ClusterEvaluationConfigurationResults) are stored
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// The configuration (older versions only had this for tests not yet run, but now we keep
        /// a copy for tests run as well)
        /// </summary>
        public readonly ClusterEvaluationConfiguration Configuration;

        /// <summary>
        /// IMPLEMENTS IVisualisable 
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable 
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable 
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// CONSTRUCTOR
        /// From newly stored results and imported files
        /// </summary>       
        public ClusterEvaluationPointer(string fileName, ClusterEvaluationConfiguration configuration)
        {
            this.Enabled = true; // It must be or we wouldn't have got here!
            this.FileName = fileName;
            this.Configuration = configuration;
        }

        /// <summary>
        /// CONSTRUCTOR
        /// From imported files with unknown data
        /// </summary>       
        public ClusterEvaluationPointer(string fileName)
        {
            this.Enabled = true;
            this.FileName = fileName;    
        }

        /// <summary>
        /// CONSTRUCTOR
        /// For a new test without results
        /// </summary>    
        internal ClusterEvaluationPointer(ClusterEvaluationConfiguration configuration)
        {
            this.Enabled = true;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Does this have any results?
        /// </summary>
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
        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.FormatDisplayName(this);
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string DefaultDisplayName
        {
            get
            {
                return Configuration.ClustererConfiguration.DisplayName;
            }
        }    

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        UiControls.ImageListOrder IVisualisable.GetIcon()
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

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<ClusterEvaluationPointer>> ptr = new List<Column<ClusterEvaluationPointer>>();

            ptr.Add("Name", EColumn.Visible, z => z.DisplayName);
            ptr.Add("Comment", EColumn.None, z => z.Comment);
            ptr.Add("Enabled", EColumn.None, z => z.Enabled);
            ptr.Add("Default name", EColumn.None, z => z.DefaultDisplayName);
            ptr.Add("File", EColumn.None, z => z.FileName);
            ptr.Add("HasResults", EColumn.None, z => z.HasResults);
            ptr.AddSubObject(core, "Configuration", z => z.Configuration);

            return ptr;
        }    
    }
}
