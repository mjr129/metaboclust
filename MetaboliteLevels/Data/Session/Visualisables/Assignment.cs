using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Viewers.Lists;
using System.IO;
using MetaboliteLevels.Data.DataInfo;
using MSerialisers;

namespace MetaboliteLevels.Data.Visualisables
{
    [Serializable]
    [DeferSerialisation]
    class Assignment : IVisualisable
    {
        /// <summary>
        /// Peak
        /// </summary>
        public Vector Vector { get; private set; }

        /// <summary>
        /// Cluster
        /// </summary>
        public Cluster Cluster { get; private set; }

        /// <summary>
        /// Assignment score (usually the result of the distance metric)
        /// May be double.NaN if not applicable
        /// </summary>
        public double Score { get; private set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Statistics (from Silhouette Width)
        /// </summary>
        public Cluster NextNearestCluster = null;

        /// <summary>
        /// Statistics
        /// </summary>
        public Dictionary<string, double> AssignmentStatistics = new Dictionary<string, double>();

        /// <summary>
        /// Only to speed up statistic calculations
        /// </summary>
        [NonSerialized]
        public double[] CurrentStatisticVector;

        /// <summary>
        /// Ctor.
        /// </summary>
        public Assignment(Vector vector, Cluster cluster, double score)
        {
            this.Vector = vector;
            this.Cluster = cluster;
            this.Score = score;
        }

        /// <summary>
        /// Gets the vector of the peak at the time it was assigned into the cluster.
        /// </summary>
        public Peak Peak
        {
            get { return Vector.Peak; }
        }

        /// <summary>
        /// Safety check - null values when removed from list.
        /// </summary>
        public void Discard()
        {
            // Don't null VECTOR or CLUSTER or we won't be able to actually remove them from the lists
            Score = 1111111111;
        }

        /// <summary>
        /// For debugging.
        /// </summary>
        public override string ToString()
        {
            if (double.IsNaN(Score))
            {
                return Peak.DisplayName + " ∈ " + Cluster.DisplayName;
            }
            else
            {
                return Peak.DisplayName + " ∈ " + Cluster.DisplayName + " (d=" + Score + ")";
            }
        }

        internal static void AddHeaders(ContentsRequest request)
        {
            request.AddHeader("Score", "The score for the assignment of {0} to this cluster.");
            request.AddHeader("Group", "The group to which the cluster is assigned (only visible for individual group clustering).");
            //request.AddHeader("Next Nearest Cluster", "The next nearest cluster.");
            //request.AddHeader("Silhouette width", "The silhouette width.");
        }

        internal object[] GetHeaders()
        {
            return new object[]
                   {
                       this.Score,
                       this.Vector.Group
                   };
        }

        public string DisplayName
        {
            get
            {
                if (Title != null)
                {
                    return Title;
                }
                else if (this.Vector.Group == null)
                {
                    return this.Cluster.DisplayName + "···" + this.Peak.DisplayName;
                }
                else
                {
                    return this.Cluster.DisplayName + "···" + this.Peak.DisplayName + "···" + this.Vector.Group.ShortName;
                }
            }
        }

        public Image DisplayIcon
        {
            get { return Resources.ObjLAssignment; }
        }

        public int GetIcon()
        {
            return UiControls.ImageListOrder.Assignment;
        }

        public VisualClass VisualClass
        {
            get { return VisualClass.Assignment; }
        }

        public IEnumerable<InfoLine> GetInformation(Core core)
        {
            return GetInformation2(core, this.Peak).Concat(GetInformation2(core, this.Cluster));
        }

        private static IEnumerable<InfoLine> GetInformation2(Core core, IVisualisable vis)
        {
            return vis.GetInformation(core).Select(z => new InfoLine(vis.DisplayName + "\\" + z.Field, z.Value));
        }

        private static IEnumerable<InfoLine> GetStatistics2(Core core, IVisualisable vis)
        {
            return vis.GetStatistics(core).Select(z => new InfoLine(vis.DisplayName + "\\" + z.Field, z.Value));
        }

        public IEnumerable<InfoLine> GetStatistics(Core core)
        {
            return GetStatistics2(core, this.Peak).Concat(GetStatistics2(core, this.Cluster));
        }

        public IEnumerable<Column> GetColumns(Core core)
        {
            var cols = new List<Column<Assignment>>();

            cols.Add("Assignment", true, z => z.DisplayName);
            cols.Add("Group", true, z => z.Vector.Group);
            cols.Add("Comment", false, z => z.Comment);
            cols.Add("Vector", false, z => z.Vector.Values);

            foreach (Column<Peak> col in this.Peak.GetColumns(core))
            {
                Column<Peak> closure = col;
                cols.Add("Peak\\" + closure.Id, col.Visible, z => closure.GetRow(z.Peak));
            }

            foreach (Column<Cluster> col in this.Cluster.GetColumns(core))
            {
                Column<Cluster> closure = col;
                cols.Add("Cluster\\" + closure.Id, col.Visible, z => closure.GetRow(z.Cluster));
            }

            foreach (var kvp in this.AssignmentStatistics.Keys)
            {
                var closure = kvp;
                cols.Add("Assignment statistic\\" + closure, false, z => z.AssignmentStatistics.GetOrNan(closure));
            }

            cols.Add("Assignment statistic\\Score", true, z => z.Score);
            cols.Add("Assignment statistic\\Next nearest cluster", false, z => z.NextNearestCluster);

            return cols;
        }

        public void RequestContents(ContentsRequest request)
        {
            switch (request.Type)
            {
                case VisualClass.Peak:
                    this.Cluster.RequestContents(request);
                    request.Text = "Peaks from the same cluster as {0}";
                    break;

                case VisualClass.Cluster:
                    this.Peak.RequestContents(request);
                    request.Text = "Clusters for the same peak as {0}";
                    break;

                case VisualClass.Assignment:
                    this.Peak.RequestContents(request);
                    request.Text = "Assignments for the same peak as {0}";
                    break;

                case VisualClass.Compound:
                    this.Peak.RequestContents(request);
                    request.Text = "Potential compounds of the peak in {0}";
                    break;

                case VisualClass.Adduct:
                    this.Peak.RequestContents(request);
                    request.Text = "Adducts of potential compounds of the peak in {0}";
                    break;

                case VisualClass.Pathway:
                    this.Peak.RequestContents(request);
                    request.Text = "Pathways of potential compounds of the peak in {0}";
                    break;

                case VisualClass.Annotation:
                    break;

                default:
                    break;
            }
        }
    }
}
