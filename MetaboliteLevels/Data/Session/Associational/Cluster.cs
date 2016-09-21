using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Collections;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Types.General;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.Associational
{
    /// <summary>
    /// Clusters (aka Patterns)
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    internal class Cluster : Associational
    {
        /// <summary>
        /// State flags
        /// </summary>
        [Flags]
        public enum EStates
        {
            None = 0,   // None
            Insignificants = 1, // Represents insignificants
            Pathway = 2,    // Represents a pathway
        }

        /// <summary>
        /// Name of cluster
        /// </summary>
        [XColumn("Short name")]
        public readonly string ShortName;

        /// <summary>
        /// Vectors used to generate cluster centre
        /// </summary>
        [XColumn()]
        public readonly List<double[]> Exemplars = new List<double[]>();

        /// <summary>
        /// Cluster mode
        /// </summary>
        [XColumn()]
        public EStates States;

        /// <summary>
        /// Average statistics of assignments
        /// </summary>          
        public readonly Dictionary<ConfigurationStatistic, double> Statistics = new Dictionary<ConfigurationStatistic, double>();

        /// <summary>
        /// Cluster centres
        /// </summary>
        [XColumn()]
        public readonly List<double[]> Centres = new List<double[]>();

        /// <summary>
        /// Cluster assignments
        /// </summary>
        [XColumn(EColumn.Visible)]
        public readonly AssignmentList Assignments = new AssignmentList();

        /// <summary>
        /// Method used to generate the cluster
        /// </summary>
        [XColumn()]
        public readonly ConfigurationClusterer Method;

        /// <summary>
        /// Comment flag counts for assignments
        /// </summary>                  
        public readonly Dictionary<PeakFlag, int> CommentFlags = new Dictionary<PeakFlag, int>();

        /// <summary>
        /// Related clusters
        /// Used only for clusteruniqueness
        /// </summary>
        [XColumn("Related clusters")]
        public readonly HashSet<Cluster> Related = new HashSet<Cluster>();

        /// <summary>
        /// Statistics
        /// </summary>
        public readonly Dictionary<string, double> ClusterStatistics = new Dictionary<string, double>();

        public override EPrevent SupportsHide => EPrevent.Hide;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cluster(string shortName, ConfigurationClusterer creationReason)
        {
            this.ShortName = shortName;
            this.Method = creationReason;
        }


        /// <summary>
        /// Name of cluster.
        /// </summary>
        public override string DefaultDisplayName
        {
            get
            {
                if (Method != null)
                {
                    return Method.ToString() + " " + ShortName;
                }
                else
                {
                    return ShortName;
                }
            }
        }

        /// <summary>
        /// Creates a StylisedCluster (for plotting)
        /// </summary>
        /// <param name="core">Core</param>
        /// <param name="toHighlight">Which peak to highlight</param>
        /// <returns>The StylisedCluster</returns>
        public StylisedCluster CreateStylisedCluster(Core core, Associational toHighlight )
        {
            // Cluster: Peaks in cluster
            // Peak: Handled by selection - take no action
            // Compound: Peaks in compound

            StylisedCluster.HighlightElement[] highlight = null;
            string caption = "Peaks assigned to {0}.";

            if (toHighlight != null)
            {
                switch (toHighlight.AssociationalClass)
                {
                    case EVisualClass.Compound:
                        Compound highlightCompound = (Compound)toHighlight;
                        highlight = highlightCompound.Annotations.Select(StylisedCluster.HighlightElement.FromAnnotation).ToArray();
                        caption += " Peaks potentially representing {1} are {HIGHLIGHTED}.";
                        break;

                    case EVisualClass.Pathway:
                        highlight = toHighlight.FindAssociations(core, EVisualClass.Peak).Results.Cast<Peak>().Select(StylisedCluster.HighlightElement.FromPeak).ToArray();
                        caption += " Peaks potentially representing compounds in {1} are {HIGHLIGHTED}.";
                        break;

                    case EVisualClass.Peak:
                        Peak peak = (Peak)toHighlight;
                        highlight = new StylisedCluster.HighlightElement[] { new StylisedCluster.HighlightElement(peak, null) };
                        caption += " {1} is {HIGHLIGHTED}.";
                        break;

                    case EVisualClass.Assignment:
                        Assignment assignment = (Assignment)toHighlight;
                        highlight = new StylisedCluster.HighlightElement[] { new StylisedCluster.HighlightElement(assignment.Vector.Peak, assignment.Vector.Group) };
                        caption += " {1} is {HIGHLIGHTED}.";
                        break;
                }
            }

            StylisedCluster r = new StylisedCluster(this);
            r.Highlight = highlight;
            r.CaptionFormat = caption;
            r.WhatIsHighlighted = toHighlight;
            return r;
        }

        /// <summary>
        /// Calculates the averaged statistics for this cluster.
        /// Called when everything post-clustering but also when statistics have changed.
        /// </summary>
        public void CalculateAveragedStatistics()
        {
            //Statistics.Clear();

            //if (Assignments.Count == 0)
            //{
            //    return;
            //}

            //// Create
            //foreach (ConfigurationStatistic stat in Assignments.List[0].Peak.Statistics.Keys)
            //{
            //    Statistics.Add(stat, 0.0d);
            //}

            //// Sum
            //Counter<ConfigurationStatistic> counts = new Counter<ConfigurationStatistic>();

            //foreach (Assignment assignment in Assignments.List)
            //{
            //    foreach (var kvp in assignment.Peak.Statistics)
            //    {
            //        Statistics[kvp.Key] += kvp.Value;
            //        counts.Increment(kvp.Key);
            //    }
            //}

            //// Average
            //foreach (var kvp in Statistics.Keys.ToArray())
            //{
            //    Statistics[kvp] /= counts.Counts[kvp];
            //}
        }

        /// <summary>
        /// Calculates the sum of the comment flags on the constituent peaks.
        /// 
        /// Called when clustering is complete or when comment flags change.
        /// </summary>
        public void CalculateCommentFlags()
        {
            CommentFlags.Clear();

            foreach (Peak v in Assignments.Peaks)
            {
                foreach (PeakFlag flag in v.CommentFlags)
                {
                    if (CommentFlags.ContainsKey(flag))
                    {
                        CommentFlags[flag] = CommentFlags[flag] + 1;
                    }
                    else
                    {
                        CommentFlags.Add(flag, 1);
                    }
                }
            }
        }

        /// <summary>
        /// Calculates cluster centre.
        /// </summary>
        public void SetCentre(ECentreMode mode, ECandidateMode iMode)
        {
            GetCentre(mode, iMode, this.Centres);
        }

        /// <summary>
        /// Gets specified centre.
        /// </summary>
        public List<double[]> GetCentre(ECentreMode mode, ECandidateMode iMode)
        {
            List<double[]> centre = new List<double[]>();

            GetCentre(mode, iMode, centre);

            return centre;
        }

        /// <summary>
        /// Calculates cluster centre.
        /// </summary>
        private void GetCentre(ECentreMode mode, ECandidateMode iMode, List<double[]> centres)
        {
            centres.Clear();

            IEnumerable<double[]> list;
            int listCount;

            switch (iMode)
            {
                case ECandidateMode.Exemplars:
                    list = Exemplars;
                    listCount = Exemplars.Count;
                    break;

                case ECandidateMode.Assignments:
                    list = Assignments.Vectors.Select(z => z.Values);
                    listCount = Assignments.List.Count;
                    break;

                default:
                    throw new InvalidOperationException("Invalid switch. 9B5EF96B-53F3-4651-AF7D-60B27475C7B5");
            }

            if (list.IsEmpty()) // check this function
            {
                return;
            }

            switch (mode)
            {
                case ECentreMode.All:
                    //////////////////
                    // ALL EXEMPLARS
                    {
                        foreach (double[] e in list)
                        {
                            centres.Add(e);
                        }
                    }
                    break;

                case ECentreMode.Average:
                    ////////////////////////////
                    // AVERGE OF ALL EXEMPLARS
                    {
                        int numPoints = list.First().Length;

                        double[] centre = new double[numPoints];

                        for (int o = 0; o < numPoints; o++)
                        {
                            double total = 0;

                            foreach (double[] v in list)
                            {
                                total += v[o];
                            }

                            centre[o] = total / listCount;
                        }

                        centres.Add(centre);
                    }
                    break;
            }
        }

        /// <summary>
        /// Calculates the score for variable v against this cluster.
        /// The centres must have been called first by calling [SetCentre].
        /// </summary>
        public double CalculateScore(double[] vector, EDistanceMode distanceMode, ConfigurationMetric distanceMetric)
        {
            switch (distanceMode)
            {
                case EDistanceMode.ClosestCentre:
                    double bestDistance = double.MaxValue;

                    foreach (double[] centre in this.Centres)
                    {
                        double d = distanceMetric.Calculate(vector, centre);

                        if (d < bestDistance)
                        {
                            bestDistance = d;
                        }
                    }

                    return bestDistance;

                case EDistanceMode.AverageToAllCentres:
                    double totalDistance = 0;

                    foreach (double[] centre in this.Centres)
                    {
                        double d = distanceMetric.Calculate(vector, centre);

                        totalDistance += d;
                    }

                    return totalDistance / this.Centres.Count;

                default:
                    throw new InvalidOperationException("Invalid switch: " + distanceMode.ToString());
            }
        }           

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override EVisualClass AssociationalClass
        {
            get { return EVisualClass.Cluster; }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        protected override void OnFindAssociations(ContentsRequest request)
        {
            switch (request.Type)
            {
                case EVisualClass.Peak:
                    request.Text = "Peaks assigned to {0}";
                    Assignment.AddHeaders(request);

                    foreach (var ass in Assignments.List)
                    {
                        request.Add(ass.Peak, ass.GetHeaders());
                    }

                    break;

                case EVisualClass.Assignment:
                    request.Text = "Assignments to {0}";
                    request.AddRange(Assignments.List);
                    break;

                case EVisualClass.Cluster:
                    request.Text = "Clusters with peaks also in {0}";

                    foreach (Cluster c in request.Core.Clusters)
                    {
                        if (c != this)
                        {
                            if (c.Assignments.Peaks.Any(this.Assignments.Peaks.Contains))
                            {
                                request.Add(c);
                            }
                        }
                    }                          
                    break;

                case EVisualClass.Compound:
                    {
                        request.Text = "Potential compounds of peaks assigned to {0}";
                        request.AddExtraColumn("Peaks", "Peaks potentially representing this compound also in {0}");
                        Dictionary<Compound, List<Peak>> counter = new Dictionary<Compound, List<Peak>>();

                        foreach (var peak in Assignments.Peaks)
                        {
                            foreach (var c in peak.Annotations)
                            {
                                counter.GetOrNew(c.Compound).Add(peak);
                            }
                        }

                        foreach (var kvp in counter)
                        {
                            request.Add(kvp.Key, kvp.Value);
                        }
                    }
                    break;

                case EVisualClass.Adduct:
                    {
                        request.Text = "Adducts of potential compounds of peaks assigned to {0}";
                        request.AddExtraColumn("Compounds", "Compounds potentially representing {0} with this adduct");
                        Dictionary<Adduct, List<Compound>> counter = new Dictionary<Adduct, List<Compound>>();

                        foreach (var p in Assignments.Peaks)
                        {
                            foreach (var c in p.Annotations)
                            {
                                counter.GetOrNew(c.Adduct).Add(c.Compound);
                            }
                        }

                        foreach (var kvp in counter)
                        {
                            request.Add(kvp.Key, kvp.Value);
                        }
                    }
                    break;

                case EVisualClass.Pathway:
                    {
                        request.Text = "Pathways of potential compounds of peaks assigned to {0}";
                        request.AddExtraColumn("Compounds", "Compounds in this pathway with peaks in {0}");
                        request.AddExtraColumn("Peaks", "Peaks potentially representing compounds in this pathway in {0}");
                        Dictionary<Pathway, HashSet<Compound>> compounds = new Dictionary<Pathway, HashSet<Compound>>();
                        Dictionary<Pathway, HashSet<Peak>> peaks = new Dictionary<Pathway, HashSet<Peak>>();

                        foreach (var peak in Assignments.Peaks) // peaks in cluster
                        {
                            foreach (var comp in peak.Annotations) // compounds in peak
                            {
                                foreach (var path in comp.Compound.Pathways) // pathways in compound
                                {
                                    compounds.GetOrNew(path).Add(comp.Compound); // pathway += compound
                                    peaks.GetOrNew(path).Add(peak);              // pathway += peak
                                }
                            }
                        }

                        foreach (var kvp in compounds)
                        {
                            request.Add(kvp.Key, kvp.Value.ToArray(), peaks[kvp.Key].ToArray());
                        }
                    }
                    break;

                case EVisualClass.Annotation:
                    request.Text = "Annotations for peaks in {0}";

                    foreach (Peak peak in Assignments.Peaks)
                    {
                        request.AddRange(peak.Annotations);
                    }

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>    
        public override IEnumerable<Column> GetXColumns(Core core)
        {
            var result = new List<Column<Cluster>>();
                                                                 
            result.Add("Assignments\\As peaks", EColumn.Visible, λ => λ.Assignments.Peaks.ToArray());
            result.Add("Assignments\\As scores", EColumn.Advanced, λ => λ.Assignments.Scores.ToArray());

            foreach (GroupInfo group in core.Groups)
            {
                GroupInfo closure = group;
                result.Add("Assignments\\For " + group.DisplayName, EColumn.None, λ => λ.Assignments.List.Where(z => z.Vector.Group == closure).Select(z => z.Cluster).ToArray());
                result[result.Count - 1].Colour = z => closure.Colour;
            }             

            foreach (PeakFlag flag in core.Options.PeakFlags)
            {
                PeakFlag closure = flag;
                result.Add("Flags\\" + flag, EColumn.Advanced, λ => λ.CommentFlags.ContainsKey(closure) ? λ.CommentFlags[closure] : 0);
                result[result.Count - 1].Colour = z => closure.Colour;
            }

            result.Add("Flags\\Summary", EColumn.None, λ => λ.CommentFlags.Select(z => z.Key + " = " + z.Value), z => z.CommentFlags.Count != 1 ? Color.Black : z.CommentFlags.Keys.First().Colour);

            foreach (ConfigurationStatistic stat in core.AllStatistics.WhereEnabled())
            {
                ConfigurationStatistic closure = stat;
                result.Add("Average Statistic\\" + closure, EColumn.IsStatistic, λ => λ.Statistics.GetOrNan(closure));
            }

            foreach (string stat in core.GetClusterStatistics()) // TODO: No!
            {
                string closure = stat;
                result.Add("Cluster statistic\\" + closure, EColumn.IsStatistic, λ => λ.ClusterStatistics.GetOrNan(closure));
            }                                                        

            return result;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override UiControls.ImageListOrder Icon
        {
            get
            {
                // IMAGE
                if (this.States == EStates.Insignificants)
                {
                    return UiControls.ImageListOrder.ClusterU;
                }
                else if (this.States == EStates.Pathway)
                {
                    return UiControls.ImageListOrder.Pathway;
                }
                else
                {
                    return UiControls.ImageListOrder.Cluster;
                }
            }
        }
    }
}
