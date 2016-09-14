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
using MetaboliteLevels.Viewers.Charts;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Visualisables
{
    [Serializable]
    [DeferSerialisation]
    internal class Assignment : Associational
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
        /// Statistics (from Silhouette Width)
        /// </summary>
        public Cluster NextNearestCluster = null;

        /// <summary>
        /// Statistics
        /// </summary>
        public Dictionary<string, double> AssignmentStatistics = new Dictionary<string, double>();

        public override EPrevent SupportsHide => EPrevent.Hide;

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

        internal static void AddHeaders(ContentsRequest request)
        {
            request.AddExtraColumn("Score", "The score for the assignment of {0} to this cluster.");
            request.AddExtraColumn("Group", "The group to which the cluster is assigned (only visible for individual group clustering).");
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

        public override string DefaultDisplayName
        {
            get
            {
                if (this.Vector.Group == null)
                {
                    return this.Cluster.DisplayName + "···" + this.Peak.DisplayName;
                }
                else
                {
                    return this.Cluster.DisplayName + "···" + this.Peak.DisplayName + "···" + this.Vector.Group.DisplayShortName;
                }
            }
        }                

        public override UiControls.ImageListOrder Icon=>UiControls.ImageListOrder.Assignment;

        public override EVisualClass AssociationalClass=> EVisualClass.Assignment;

        public override IEnumerable<Column> GetColumns(Core core)
        {
            var cols = new List<Column<Assignment>>();

            cols.Add("Assignment", EColumn.Visible, z => z.DisplayName);
            cols.Add("Group", EColumn.Visible, z => z.Vector.Group);
            cols.Add("Comment", EColumn.None, z => z.Comment);
            cols.Add("Vector", EColumn.None, z => z.Vector.Values);

            cols.AddSubObject(core, "Cluster", z => z.Cluster);
            cols.AddSubObject(core, "Peak", z => z.Peak);

            foreach (var kvp in this.AssignmentStatistics.Keys)
            {
                var closure = kvp;
                cols.Add("Assignment statistic\\" + closure, EColumn.Statistic, z => z.AssignmentStatistics.GetOrNan(closure));
            }

            cols.Add("Assignment statistic\\Score", EColumn.Visible, z => z.Score);
            cols.Add("Assignment statistic\\Next nearest cluster", EColumn.None, z => z.NextNearestCluster);

            return cols;
        }

        public override void FindAssociations(ContentsRequest request)
        {
            switch (request.Type)
            {
                case EVisualClass.Peak:
                    request.Add(this.Peak);
                    request.Text = "Peak for {0}";
                    break;

                case EVisualClass.Cluster:
                    request.Add(this.Cluster);
                    request.Text = "Clusters for {0}";
                    break;

                case EVisualClass.Assignment:   
                case EVisualClass.Compound:    
                case EVisualClass.Adduct:   
                case EVisualClass.Pathway:   
                case EVisualClass.Annotation:        
                default:
                    break;
            }
        }

        internal StylisedCluster CreateStylisedCluster(Core core, Visualisable toHighlight)
        {        
            Cluster fakeCluster = new Cluster(DisplayName, null);
            fakeCluster.Assignments.Add(this);

            StylisedCluster c = new StylisedCluster(fakeCluster, this, null);
            c.IsFake = true;
            c.CaptionFormat = "Plot of {0}.";

            return c;
        }
    }
}
