using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Forms.Algorithms.ClusterEvaluation
{
    /// <summary>
    /// A configuration for a test (either to be run or already run)
    /// </summary>
    [Serializable]
    class ClusterEvaluationConfiguration : IVisualisable
    {
        /// <summary>
        /// The configuration of the clusterer to use
        /// </summary>
        public readonly ConfigurationClusterer ClustererConfiguration;

        /// <summary>
        /// Index of the parameter to manipulate
        /// </summary>
        public readonly int ParameterIndex;

        /// <summary>
        /// Values of the specified parameter to test
        /// </summary>
        public readonly object[] ParameterValues;

        /// <summary>
        /// Number of times to repeat each test
        /// </summary>
        public readonly int NumberOfRepeats;

        /// <summary>
        /// Name of the manipulated parameter
        /// </summary>
        public readonly string ParameterName;

        /// <summary>
        /// A unique ID for this configuration.
        /// </summary>
        private readonly Guid _guid;

        /// <summary>
        /// ACCESSOR: _guid
        /// </summary>
        public Guid Guid { get { return _guid; } }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary> 
        public ClusterEvaluationConfiguration(ConfigurationClusterer clustererConfiguration, int parameterIndex, object[] values, int numberOfRepeats)
        {
            // Make sure we have no results here, they will make the save massive and are never used!
            UiControls.Assert(!clustererConfiguration.HasResults, "Didn't expect any results in ClusterEvaluationConfiguration::ClustererConfiguration.");

            this.ClustererConfiguration = clustererConfiguration;
            this.ParameterIndex = parameterIndex;
            this.ParameterValues = values;
            this._guid = Guid.NewGuid();
            this.NumberOfRepeats = numberOfRepeats;
            this.ParameterName = clustererConfiguration.Cached.Parameters[ParameterIndex].Name;
        }

        /// <summary>
        /// OVERRIDES Object.
        /// </summary>           
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>           
        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            return UiControls.ImageListOrder.TestEmpty;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>               
        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<ClusterEvaluationConfiguration>> columns = new List<Column<ClusterEvaluationConfiguration>>();

            columns.Add("Name", EColumn.Visible, z => z.DisplayName);
            columns.Add("Test parameter\\Name", EColumn.None, z => z.ParameterName);
            columns.Add("Test parameter\\Summary", EColumn.None, z => z.ParameterConfigAsString);
            columns.Add("Test parameter\\Index", EColumn.None, z => z.ParameterIndex);
            columns.Add("Test parameter\\Test values", EColumn.None, z => z.ParameterValuesAsString);
            columns.Add("Test parameter\\Number of test values", EColumn.None, z => z.ParameterValues.Length);
            columns.Add("Test parameter\\Test value list", EColumn.None, z => z.ParameterValues);
            columns.Add("Number of repeats", EColumn.None, z => z.NumberOfRepeats);
            columns.Add("GUID", EColumn.None, z => z.Guid.ToString());
            columns.AddSubObject(core, "Clusterer", z => z.ClustererConfiguration);

            return columns;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>               
        void IVisualisable.RequestContents(ContentsRequest list)
        {
            // NA
        }

        /// <summary>
        /// The parameter configuation as a string.
        /// </summary>
        public string ParameterConfigAsString
        {
            get
            {
                return ParameterName + " = " + NumberOfRepeats + " × { " + ParameterValuesAsString + " }";
            }
        }

        /// <summary>
        /// The parameter values as a string.
        /// </summary>
        public string ParameterValuesAsString
        {
            get
            {
                return StringHelper.ArrayToString(ParameterValues, AlgoParameterCollection.ParamToString, ", ");
            }
        }

        /// <summary>
        /// Legacy equivalent of IVisualisable.OverrideDisplayName.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        VisualClass IVisualisable.VisualClass
        {
            get
            {
                return VisualClass.None;
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
        public string DefaultDisplayName
        {
            get
            {
                return ClustererConfiguration.DisplayName + " : " + ParameterConfigAsString;
            }
        }

        /// <summary>
        /// IMPLEMENTS ITitlable
        /// (Wraps an existing member for backwards compatibility before this class extended the interface)
        /// </summary>
        string ITitlable.OverrideDisplayName
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }
    }
}
