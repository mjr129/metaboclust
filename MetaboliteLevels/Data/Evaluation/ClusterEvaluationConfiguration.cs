using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Evaluation
{
    /// <summary>
    /// A configuration for a test (either to be run or already run)
    /// </summary>
    [Serializable]
    class ClusterEvaluationConfiguration : Visualisable
    {
        /// <summary>
        /// The configuration of the clusterer to use
        /// </summary>
        [XColumn( "Configuration\\", EColumn.Decompose )]
        public readonly ArgsClusterer ClustererConfiguration;

        /// <summary>
        /// Index of the parameter to manipulate
        /// </summary>
        [XColumn( "Parameter\\Index" )]
        public readonly int ParameterIndex;

        /// <summary>
        /// Values of the specified parameter to test
        /// </summary>
        [XColumn( "Parameter\\Test values" )]
        public readonly object[] ParameterValues;

        /// <summary>
        /// Number of times to repeat each test
        /// </summary>
        [XColumn()]
        public readonly int NumberOfRepeats;

        /// <summary>
        /// Name of the manipulated parameter
        /// </summary>
        [XColumn("Parameter\\Name")]
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
        public ClusterEvaluationConfiguration( ArgsClusterer clustererConfiguration, int parameterIndex, object[] values, int numberOfRepeats)
        {
            // Make sure we have no results here, they will make the save massive and are never used!
            //UiControls.Assert(!clustererConfiguration.HasResults, "Didn't expect any results in ClusterEvaluationConfiguration::ClustererConfiguration.");

            this.ClustererConfiguration = clustererConfiguration;
            this.ParameterIndex = parameterIndex;
            this.ParameterValues = values;
            this._guid = Guid.NewGuid();
            this.NumberOfRepeats = numberOfRepeats;
            this.ParameterName = clustererConfiguration.GetAlgorithmOrThrow().Parameters[ParameterIndex].Name;
        }        

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>           
        public override UiControls.ImageListOrder Icon => UiControls.ImageListOrder.TestEmpty;  

        /// <summary>
        /// The parameter configuation as a string.
        /// </summary>
        [XColumn( "Parameter\\Summary" )]
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
        [XColumn( "Parameter\\Values (as text)" )]
        public string ParameterValuesAsString
        {
            get
            {
                return StringHelper.ArrayToString(ParameterValues, AlgoParameterCollection.ParamToString, ", ");
            }
        }             

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override string DefaultDisplayName => ClustererConfiguration.DisplayName + " : " + ParameterConfigAsString;             
    }
}
