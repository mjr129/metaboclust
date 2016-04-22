using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MSerialisers;

namespace MetaboliteLevels.Data.Visualisables
{
    /// <summary>
    /// Clusters (aka Patterns)
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class Cluster : IAssociational
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
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// Name of cluster
        /// </summary>
        public readonly string ShortName;

        /// <summary>
        /// Vectors used to generate cluster centre
        /// </summary>
        public readonly List<double[]> Exemplars = new List<double[]>();

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Cluster mode
        /// </summary>
        public EStates States;

        /// <summary>
        /// Average statistics of assignments
        /// </summary>          
        public readonly Dictionary<ConfigurationStatistic, double> Statistics = new Dictionary<ConfigurationStatistic, double>();

        /// <summary>
        /// Cluster centres
        /// </summary>
        public readonly List<double[]> Centres = new List<double[]>();

        /// <summary>
        /// Cluster assignments
        /// </summary>
        public readonly AssignmentList Assignments = new AssignmentList();

        /// <summary>
        /// Method used to generate the cluster
        /// </summary>
        public readonly ConfigurationClusterer Method;

        /// <summary>
        /// Comment flag counts for assignments
        /// </summary>                  
        public readonly Dictionary<PeakFlag, int> CommentFlags = new Dictionary<PeakFlag, int>();

        /// <summary>
        /// Related clusters
        /// Used only for clusteruniqueness
        /// </summary>
        public readonly HashSet<Cluster> Related = new HashSet<Cluster>();

        /// <summary>
        /// Statistics
        /// </summary>
        public readonly Dictionary<string, double> ClusterStatistics = new Dictionary<string, double>();

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// Unused (can't be disabled)
        /// </summary>
        bool INameable.Enabled { get { return true; } set { } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cluster(string shortName, ConfigurationClusterer creationReason)
        {
            this.ShortName = shortName;
            this.Method = creationReason;
        }

        /// <summary>
        /// Display name of cluster
        /// </summary>
        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.FormatDisplayName(this);
            }
        }

        /// <summary>
        /// Name of cluster.
        /// </summary>
        public string DefaultDisplayName
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
        public StylisedCluster CreateStylisedCluster(Core core, IAssociational toHighlight )
        {
            // Cluster: Peaks in cluster
            // Peak: Handled by selection - take no action
            // Compound: Peaks in compound

            StylisedCluster.HighlightElement[] highlight = null;
            string caption = "Peaks assigned to {0}.";

            if (toHighlight != null)
            {
                switch (toHighlight.VisualClass)
                {
                    case VisualClass.Compound:
                        Compound highlightCompound = (Compound)toHighlight;
                        highlight = highlightCompound.Annotations.Select(StylisedCluster.HighlightElement.FromAnnotation).ToArray();
                        caption += " Peaks potentially representing {1} are {HIGHLIGHTED}.";
                        break;

                    case VisualClass.Pathway:
                        highlight = toHighlight.GetContents(core, VisualClass.Peak).Keys.Cast<Peak>().Select(StylisedCluster.HighlightElement.FromPeak).ToArray();
                        caption += " Peaks potentially representing compounds in {1} are {HIGHLIGHTED}.";
                        break;

                    case VisualClass.Peak:
                        Peak peak = (Peak)toHighlight;
                        highlight = new StylisedCluster.HighlightElement[] { new StylisedCluster.HighlightElement(peak, null) };
                        caption += " {1} is {HIGHLIGHTED}.";
                        break;

                    case VisualClass.Assignment:
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
            Statistics.Clear();

            if (Assignments.Count == 0)
            {
                return;
            }

            // Create
            foreach (ConfigurationStatistic stat in Assignments.List[0].Peak.Statistics.Keys)
            {
                Statistics.Add(stat, 0.0d);
            }

            // Sum
            Counter<ConfigurationStatistic> counts = new Counter<ConfigurationStatistic>();

            foreach (Assignment assignment in Assignments.List)
            {
                foreach (var kvp in assignment.Peak.Statistics)
                {
                    Statistics[kvp.Key] += kvp.Value;
                    counts.Increment(kvp.Key);
                }
            }

            // Average
            foreach (var kvp in Statistics.Keys.ToArray())
            {
                Statistics[kvp] /= counts.Counts[kvp];
            }
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
        /// OVERRIDES Object
        /// </summary>         
        public override string ToString()
        {
            return DisplayName + " (" + Assignments.Count + " assignments)";
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public VisualClass VisualClass
        {
            get { return VisualClass.Cluster; }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public void RequestContents(ContentsRequest request)
        {
            switch (request.Type)
            {
                case VisualClass.Peak:
                    request.Text = "Peaks assigned to {0}";
                    Assignment.AddHeaders(request);

                    foreach (var ass in Assignments.List)
                    {
                        request.Add(ass.Peak, ass.GetHeaders());
                    }

                    break;

                case VisualClass.Assignment:
                    request.Text = "Assignments to {0}";
                    request.AddRange(Assignments.List);
                    break;

                case VisualClass.Cluster:
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

                case VisualClass.Compound:
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

                case VisualClass.Adduct:
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

                case VisualClass.Pathway:
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

                case VisualClass.Annotation:
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
        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            return GetColumns(core);
        }

        /// <summary>
        /// Static version of GetColumns
        /// </summary>
        public static IEnumerable<Column> GetColumns(Core core)
        {
            var result = new List<Column<Cluster>>();

            result.Add("Method Name", EColumn.None, λ => λ.Method.ToString());
            result.Add("Method №", EColumn.None, λ => 1 + core.AllClusterers.WhereEnabled().IndexOf(λ.Method));
            result.Add("Name", EColumn.Visible, λ => λ.DisplayName);
            result.Add("Comments", EColumn.None, λ => λ.Comment);
            result.Add("Assignments\\All", EColumn.Visible, λ => λ.Assignments.Peaks.ToArray());
            result.Add("Assignments\\All (scores)", EColumn.None, λ => λ.Assignments.Scores.ToArray());

            foreach (GroupInfo group in core.Groups)
            {
                GroupInfo closure = group;
                result.Add("Assignments\\" + UiControls.ZEROSPACE + group.DisplayName, EColumn.None, λ => λ.Assignments.List.Where(z => z.Vector.Group == closure).Select(z => z.Cluster).ToArray());
                result[result.Count - 1].Colour = z => closure.Colour;
            }

            result.Add("Exemplars", EColumn.None, λ => λ.Exemplars);
            result.Add("State", EColumn.None, λ => λ.States.ToUiString());
            result.Add("Comment", EColumn.None, λ => λ.Comment);

            foreach (PeakFlag flag in core.Options.PeakFlags)
            {
                PeakFlag closure = flag;
                result.Add("Flag\\" + flag, EColumn.None, λ => λ.CommentFlags.ContainsKey(closure) ? λ.CommentFlags[closure] : 0);
                result[result.Count - 1].Colour = z => closure.Colour;
            }

            result.Add("Flag\\(all)", EColumn.None, λ => λ.CommentFlags.Select(z => z.Key + " = " + z.Value), z => z.CommentFlags.Count != 1 ? Color.Black : z.CommentFlags.Keys.First().Colour);

            foreach (ConfigurationStatistic stat in core.AllStatistics.WhereEnabled())
            {
                ConfigurationStatistic closure = stat;
                result.Add("Average Statistic\\" + closure, EColumn.Statistic, λ => λ.Statistics.GetOrNan(closure));
            }

            foreach (string stat in core.GetClusterStatistics()) // TODO: No!
            {
                string closure = stat;
                result.Add("Cluster statistic\\" + closure, EColumn.Statistic, λ => λ.ClusterStatistics.GetOrNan(closure));
            }

            result.Add("№ centres", EColumn.None, λ => λ.Centres.Count);
            result.Add("Related clusters", EColumn.None, λ => λ.Related);
            result.Add("Short name", EColumn.None, λ => λ.ShortName);

            return result;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public UiControls.ImageListOrder GetIcon()
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
