using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Algorithms.ClusterEvaluation
{                    
    /// <summary>
    /// A configuration for a test (either to be run or already run)
    /// </summary>
    class ClusterEvaluationConfiguration
    {
        public readonly ConfigurationClusterer ClustererConfiguration;

        public readonly int ParameterIndex;
        public readonly object[] ParameterValues;
        public readonly int NumberOfRepeats;

        public readonly string[] ParameterValuesAsString;
        public readonly double[] ParameterValuesAsDouble;
        public readonly string ParameterName;
        public readonly string ClustererDescription;

        public ClusterEvaluationConfiguration(ConfigurationClusterer clustererConfiguration, int parameter, object[] values, int numberOfTimes)
        {
            this.ClustererConfiguration = clustererConfiguration;
            this.ParameterIndex = parameter;
            this.ParameterValues = values;
            this.NumberOfRepeats = numberOfTimes;

            this.ParameterName = clustererConfiguration.Cached.GetParams().Parameters[ParameterIndex].Name;
            this.ClustererDescription = clustererConfiguration.Description;
            this.ParameterValuesAsString = ParameterValues.Select(AlgoParameters.ParamToString).ToArray();
            this.ParameterValuesAsDouble = ParameterValues.Select(ToDouble).ToArray();
        }

        private static double ToDouble(object arg)
        {
            if (arg is IConvertible)
            {
                return Convert.ToDouble(arg);
            }
            else
            {
                return double.NaN;
            }
        }

        public override string ToString()
        {
            return ClustererDescription + " : " + ParamsAsString;
        }

        public string ParamsAsString
        {
            get
            {
                return ParameterName + " = " + NumberOfRepeats + " × { " + StringHelper.ArrayToString(ParameterValues, AlgoParameters.ParamToString) + " }";
            }
        }

        public string Name
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }
    }
}
