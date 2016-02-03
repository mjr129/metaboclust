using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Forms.Algorithms.ClusterEvaluation
{
    /// <summary>
    /// Results for an individual parameter (see ClusterEvaluationResults)
    /// </summary>
    [Serializable]
    class ClusterEvaluationParameterResult : IVisualisable
    {
        public string OverrideDisplayName { get; set; }

        public string Comments { get; set; }

        /// <summary>
        /// Not used (meaningless).
        /// </summary>
        bool ITitlable.Enabled { get { return true; } set { } }

        public ClusterEvaluationConfiguration Owner;

        public int ValueIndex;

        public List<ResultClusterer> Repetitions;

        public string DefaultDisplayName
        {
            get
            {
                return _defaultName;
            }
        }

        private readonly string _defaultName;

        public override string ToString()
        {
            return DisplayName;
        }

        public object Values
        {
            get
            {
                if (Owner.ParameterValues == null)
                {
                    return "?"; // TODO
                }

                return Owner.ParameterValues[ValueIndex];
            }
        }

        public ClusterEvaluationParameterResult(string name, ClusterEvaluationConfiguration owner, int valueIndex, List<ResultClusterer> results)
        {
            _defaultName = name;
            Owner = owner;
            ValueIndex = valueIndex;
            Repetitions = results;
        }

        public string DisplayName
        {
            get { return IVisualisableExtensions.GetDisplayName(OverrideDisplayName, DefaultDisplayName); }
        }

        public Image REMOVE_THIS_FUNCTION
        {
            get { return Resources.ObjPoint; }
        }

        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            return UiControls.ImageListOrder.Point;
        }

        VisualClass IVisualisable.VisualClass
        {
            get { return VisualClass.None; }
        }

        public string Comment { get; set; } 

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<ClusterEvaluationParameterResult>> res = new List<Column<ClusterEvaluationParameterResult>>();

            res.Add("Value", EColumn.Visible, z => z.DisplayName);

            // 

            for (int n = 0; n < Repetitions.Count; n++)
            {
                int closure = n;
                int humanIndex = n + 1;

                res.Add("Rep " + humanIndex + "\\Number of clusters", EColumn.None, z => (z.Repetitions[closure].Clusters) != null ? z.Repetitions[closure].Clusters.Length : 0);

                foreach (var k in this.Repetitions[0].ClustererStatistics.Keys) // shouldn't use "this" or "0"
                {
                    string closure2 = k;
                    res.Add("Rep " + humanIndex + "\\" + closure2, EColumn.None, z => z.Repetitions[closure].ClustererStatistics.GetOrNan(closure2));
                }
            }


            res.Add("Rep AVG\\Number of clusters", EColumn.None, z => z.Repetitions.Average(zz => (zz.Clusters != null) ? zz.Clusters.Length : 0));

            foreach (var k in this.Repetitions[0].ClustererStatistics.Keys) // shouldn't use "this" or "0"
            {
                string closure = k;
                res.Add("Rep AVG\\" + closure, EColumn.None, z => z.Repetitions.Average(zz => zz.ClustererStatistics.GetOrNan(closure)));
            }                                                                                                                        

            return res;
        }

        void IVisualisable.RequestContents(ContentsRequest list)
        {
            // NA
        }
    }
}
