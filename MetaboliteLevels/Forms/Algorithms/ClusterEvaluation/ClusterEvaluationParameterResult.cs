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
        public string Title { get; set; }
        public string Comments { get; set; }

        public ClusterEvaluationConfiguration Owner;

        public int ValueIndex;

        public List<ResultClusterer> Repetitions;

        private readonly string Name;

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
            Name = name;
            Owner = owner;
            ValueIndex = valueIndex;
            Repetitions = results;
        }

        public string DisplayName
        {
            get { return Title ?? Name; }
        }

        public Image DisplayIcon
        {
            get { return Resources.ObjPoint; }
        }

        public int GetIcon()
        {
            return UiControls.ImageListOrder.Point;
        }

        public VisualClass VisualClass
        {
            get { return VisualClass.None; }
        }

        public string Comment { get; set; }

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
            List<Column<ClusterEvaluationParameterResult>> res = new List<Column<ClusterEvaluationParameterResult>>();

            res.Add("Value", true, z => z.DisplayName);

            // 

            for (int n = 0; n < Repetitions.Count; n++)
            {
                int closure = n;
                int humanIndex = n + 1;

                res.Add("Rep " + humanIndex + "\\Number of clusters", false, z => (z.Repetitions[closure].Clusters) != null ? z.Repetitions[closure].Clusters.Length : 0);

                foreach (var k in this.Repetitions[0].ClustererStatistics.Keys) // shouldn't use "this" or "0"
                {
                    string closure2 = k;
                    res.Add("Rep " + humanIndex + "\\" + closure2, false, z => z.Repetitions[closure].ClustererStatistics.GetOrNan(closure2));
                }
            }


            res.Add("Rep AVG\\Number of clusters", false, z => z.Repetitions.Average(zz => (zz.Clusters != null) ? zz.Clusters.Length : 0));

            foreach (var k in this.Repetitions[0].ClustererStatistics.Keys) // shouldn't use "this" or "0"
            {
                string closure = k;
                res.Add("Rep AVG\\" + closure, false, z => z.Repetitions.Average(zz => zz.ClustererStatistics.GetOrNan(closure)));
            }

            //res.Add("Rep AVG\\Number of vectors", false, z => z.Results.Average(zz => zz.VMatrix.NumVectors));
            //res.Add("Rep AVG\\Length of vector", false, z => z.Results.Average(zz => zz.VMatrix.Vectors[0].Values.Length));

            return res;
        }

        public void RequestContents(ContentsRequest list)
        {
            // NA
        }
    }
}
