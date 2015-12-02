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
    class Cluster : IVisualisable
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
        ///User comments.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Name of cluster
        /// </summary>
        public readonly string ShortName;

        /// <summary>
        /// Vectors used to generate cluster centre
        /// </summary>
        public readonly List<double[]> Exemplars = new List<double[]>();

        /// <summary>
        /// User comments.
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
                return Title ?? Name;
            }
        }

        /// <summary>
        /// Name of cluster.
        /// </summary>
        public string Name
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
        public StylisedCluster CreateStylisedCluster(Core core, IVisualisable toHighlight)
        {
            // Cluster: Peaks in cluster
            // Peak: Handled by selection - take no action
            // Compound: Peaks in compound

            IEnumerable<IVisualisable> highlight = null;
            string caption = "Peaks assigned to {0}.";

            if (toHighlight != null)
            {
                switch (toHighlight.VisualClass)
                {
                    case VisualClass.Compound:
                        highlight = toHighlight.GetContents(core, VisualClass.Peak).Keys;
                        caption += " Peaks potentially representing {1} are shown in red.";
                        break;

                    case VisualClass.Pathway:
                        highlight = toHighlight.GetContents(core, VisualClass.Peak).Keys;
                        caption += " Peaks potentially representing compounds in the {1} are shown in red.";
                        break;

                    case VisualClass.Peak:
                        //highlight = new List<IVisualisable>() { toHighlight };
                        //caption += " {1} is highlighted in red.";
                        break;
                }
            }

            StylisedCluster r = new StylisedCluster(this);
            r.Highlight = highlight;
            r.CaptionFormat = caption;
            r.Source = toHighlight;
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

        public override string ToString()
        {
            return DisplayName + " (" + Assignments.Count + " assignments)";
        }

        /// <summary>
        /// Inherited from IVisualisable. 
        /// </summary>
        public Image DisplayIcon
        {
            get { return Properties.Resources.ObjLCluster; }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public IEnumerable<InfoLine> GetInformation(Core core)
        {
            yield return new InfoLine("№ assignments", this.Assignments.Count);
            yield return new InfoLine("№ centres", this.Centres.Count);
            yield return new InfoLine("Comments", Comment);
            yield return new InfoLine("Flags", Maths.ArrayToString(CommentFlags));
            yield return new InfoLine("Display name", DisplayName);
            yield return new InfoLine("№ exemplars", this.Exemplars.Count);
            yield return new InfoLine("Method", this.Method?.ToString());
            yield return new InfoLine("Name", this.DisplayName);
            yield return new InfoLine("Override name", this.Title);
            yield return new InfoLine("№ related clusters", this.Related.Count);
            yield return new InfoLine("Short name", this.ShortName);
            yield return new InfoLine("States", this.States.ToUiString());
            yield return new InfoLine("№ statistics", this.Statistics.Count);
            yield return new InfoLine("Class", this.VisualClass.ToUiString());
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public IEnumerable<InfoLine> GetStatistics(Core core)
        {
            foreach (var kvp in Statistics)
            {
                yield return new InfoLine(kvp.Key.ToString(), kvp.Value);
            }

            foreach (var kvp in ClusterStatistics)
            {
                yield return new InfoLine(kvp.Key.ToString(), kvp.Value);
            }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public VisualClass VisualClass
        {
            get { return VisualClass.Cluster; }
        }

        /// <summary>
        /// Implements IVisualisable. 
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
                    request.Text = "Clusters related to {0}";

                    request.AddRange(Related);
                    break;

                case VisualClass.Compound:
                    {
                        request.Text = "Potential compounds of peaks assigned to {0}";
                        request.AddHeader("Peaks", "Peaks potentially representing this compound also in {0}");
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
                        request.AddHeader("Compounds", "Compounds potentially representing {0} with this adduct");
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
                        request.AddHeader("Compounds", "Compounds in this pathway with peaks in {0}");
                        request.AddHeader("Peaks", "Peaks potentially representing compounds in this pathway in {0}");
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
                    request.Text = "Annotations for peaks in cluster {0}";

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
        /// Gets the statistic by the name of "x" otherwise NaN.
        /// </summary>
        internal double GetStatistic(ConfigurationStatistic x)
        {
            double v;

            if (Statistics.TryGetValue(x, out v))
            {
                return v;
            }

            return double.NaN;
        }


        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public IEnumerable<Column> GetColumns(Core core)
        {
            var result = new List<Column<Cluster>>();

            result.Add("Method Name", false, λ => λ.Method.ToString());
            result.Add("Method №", true, λ => 1 + core.Clusterers.IndexOf(λ.Method));
            result.Add("Name", true, λ => λ.DisplayName);
            result.Add("Assignments\\All", false, λ => λ.Assignments.Peaks.ToArray());
            result.Add("Assignments\\All (scores)", false, λ => λ.Assignments.Scores.ToArray());

            foreach (GroupInfo group in core.Groups)
            {
                GroupInfo closure = group;
                result.Add("Assignments\\" + UiControls.ZEROSPACE + group.Name, false, λ => λ.Assignments.List.Where(z => z.Vector.Group == closure).Select(z => z.Cluster).ToArray());
                result[result.Count - 1].Colour = z => closure.Colour;
            }

            result.Add("Exemplars", false, λ => λ.Exemplars);
            result.Add("State", false, λ => λ.States.ToUiString());
            result.Add("Comment", false, λ => λ.Comment);

            foreach (PeakFlag flag in core.Options.PeakFlags)
            {
                PeakFlag closure = flag;
                result.Add("Flag\\" + flag, false, λ => λ.CommentFlags.ContainsKey(closure) ? closure.Id : "");
                result[result.Count - 1].Colour = z => closure.Colour;
            }

            foreach (ConfigurationStatistic stat in core.Statistics)
            {
                ConfigurationStatistic closure = stat;
                result.Add("Average Statistic\\" + closure, false, λ => λ.Statistics.GetOrNan(closure));
            }

            foreach (string stat in this.ClusterStatistics.Keys) // TODO: No!
            {
                string closure = stat;
                result.Add("Cluster statistic\\" + closure, false, λ => λ.ClusterStatistics.GetOrNan(closure));
            }

            return result;
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public int GetIcon()
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
