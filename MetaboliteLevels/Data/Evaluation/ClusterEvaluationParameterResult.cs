using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Evaluation
{
    /// <summary>
    /// Results for an individual parameter (see ClusterEvaluationResults)
    /// </summary>
    [Serializable]
    class ClusterEvaluationParameterResult : Visualisable
    {
        public override EPrevent SupportsHide => EPrevent.Hide;

        /// <summary>
        /// Conifguration these results were sourced from
        /// </summary>
        public ClusterEvaluationConfiguration Owner;

        /// <summary>
        /// 
        /// </summary>
        public int ValueIndex;

        /// <summary>
        /// Results of each parameter manipulation
        /// </summary>
        public List<ResultClusterer> Repetitions;

        /// <summary>
        /// Name of this result set
        /// </summary>
        private readonly string _defaultName;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>   
        public ClusterEvaluationParameterResult(string name, ClusterEvaluationConfiguration owner, int valueIndex, List<ResultClusterer> results)
        {
            _defaultName = name;
            Owner = owner;
            ValueIndex = valueIndex;
            Repetitions = results;
        }           

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override string DefaultDisplayName => _defaultName;

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override Image Icon => Resources.ListIconResultVector;

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override void GetXColumns(ColumnCollection list, Core core)
        {
            var res = list .Cast< ClusterEvaluationParameterResult>();

            for (int n = 0; n < Repetitions.Count; n++)
            {
                int closure = n;
                int humanIndex = n + 1;

                res.Add("Rep " + humanIndex + "\\Number of clusters", EColumn.None, z => (z.Repetitions[closure].Clusters) != null ? z.Repetitions[closure].Clusters.Length : 0);

                foreach (string k in this.Repetitions[0].ClustererStatistics.Keys) // shouldn't use "this" or "0"
                {
                    string closure2 = k;
                    res.Add("Rep " + humanIndex + "\\" + closure2, EColumn.None, z => z.Repetitions[closure].ClustererStatistics.GetOrNan(closure2));
                }
            } 

            res.Add("Rep AVG\\Number of clusters", EColumn.None, z => z.Repetitions.Average(zz => (zz.Clusters != null) ? zz.Clusters.Length : 0));

            foreach (string k in this.Repetitions[0].ClustererStatistics.Keys) // shouldn't use "this" or "0"
            {
                string closure = k;
                res.Add("Rep AVG\\" + closure, EColumn.None, z => z.Repetitions.Average(zz => zz.ClustererStatistics.GetOrNan(closure)));
            }               
        }    

        /// <summary>
        /// Recalculates the statistcal set.
        /// </summary>                      
        public void RecalculateStatistics(Core core, EClustererStatistics stats, ProgressReporter prog)
        {
            foreach (ResultClusterer result in Repetitions)
            {
                IntensityMatrix vmatrix;
                DistanceMatrix dmatrix;
                Owner.ClustererConfiguration.GetAlgorithmOrThrow().ExecuteAlgorithm(core, -1, true, Owner.ClustererConfiguration, null, prog, out vmatrix, out dmatrix);
                result.RecalculateStatistics(core, Owner.ClustererConfiguration.Distance, vmatrix, dmatrix, stats, prog);
            }
        }
    }
}
