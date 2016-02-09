using System;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using System.Drawing;
using MetaboliteLevels.Data.DataInfo;
using MSerialisers;

namespace MetaboliteLevels.Data.Visualisables
{
    /// <summary>
    /// Variables
    /// (aka. Peaks, Columns, Dependent variables, Uncontrolled variables, Y, etc.)
    /// 
    /// The independed variables are in the classes: ConditionInfo, GroupInfo, ObservationInfo
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class Peak : IVisualisable
    {
        public const string COLNAME_CLUSTERS_UNIQUE = "Clusters\\Unique";

        /// <summary>
        /// UNIQUE
        /// Index of this variable.
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// The ID (name) of the peak.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// Which cluster has this been assigned to.
        /// </summary>
        public readonly AssignmentList Assignments = new AssignmentList();

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Comment flags.
        /// </summary>
        public List<PeakFlag> CommentFlags = new List<PeakFlag>();

        /// <summary>
        /// Statistics.
        /// </summary>
        public Dictionary<ConfigurationStatistic, double> Statistics = new Dictionary<ConfigurationStatistic, double>();

        /// <summary>
        /// Corrected variable data (index ≘ Core.Correction)
        /// </summary>
        public readonly List<PeakValueSet> CorrectionChain = new List<PeakValueSet>();

        /// <summary>
        /// Visible data (a pointer to OriginalObservations or the last item in CorrectionChain).
        /// </summary>
        public PeakValueSet Observations;

        /// <summary>
        /// Original variable data
        /// </summary>
        public PeakValueSet OriginalObservations;

        /// <summary>
        /// Alternative variable data
        /// </summary>
        public PeakValueSet AltObservations;

        /// <summary>
        /// Values used to compare variables [index ≘ Core.ValueCondition]
        /// </summary>
        // public double[] Values;

        /// <summary>
        /// M/Z
        /// </summary>
        public readonly decimal Mz;

        /// <summary>
        /// Other information the user loaded and may want to view in but the program doesn't actually need.
        /// </summary>
        public readonly MetaInfoCollection MetaInfo = new MetaInfoCollection();

        /// <summary>
        /// Potential compounds.
        /// </summary>
        public readonly List<Annotation> Annotations = new List<Annotation>();

        /// <summary>
        /// Similar peaks.
        /// </summary>
        public readonly List<Peak> SimilarPeaks = new List<Peak>();

        /// <summary>
        /// LC-MS Mode
        /// </summary>
        public readonly ELcmsMode LcmsMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Peak(int index, string id, PeakValueSet observations, PeakValueSet altObservations, ELcmsMode lcmsmode, decimal mz)
        {
            this.Index = index;
            this.Id = id;
            this.OriginalObservations = observations;
            this.Observations = observations;
            this.AltObservations = altObservations;
            this.Mz = mz;
            this.LcmsMode = lcmsmode;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// Unused (can't be disabled)
        /// </summary>
        bool ITitlable.Enabled { get { return true; } set { } }

        /// <summary>     
        /// Default display name.
        /// </summary>
        public string DefaultDisplayName
        {
            get
            {
                return Id;
            }
        }

        /// <summary>
        /// OVERRIDES Object
        /// </summary>         
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Toggles a comment flag on and off.
        /// </summary>
        /// <param name="f">The flag to toggle</param>
        /// <returns>The new status of the flag</returns>
        public bool ToggleCommentFlag(PeakFlag f)
        {
            if (!CommentFlags.Contains(f))
            {
                CommentFlags.Add(f);
                return true;
            }
            else
            {
                CommentFlags.Remove(f);
                return false;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string DisplayName
        {
            get { return IVisualisableExtensions.FormatDisplayName(OverrideDisplayName, DefaultDisplayName); }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        void IVisualisable.RequestContents(ContentsRequest request)
        {
            switch (request.Type)
            {
                case VisualClass.Peak:
                    request.Text = "Peaks with similar m/z to {0} (" + Maths.SignificantDigits((double)this.Mz, 5) + ")";
                    request.AddRange(this.SimilarPeaks);
                    break;

                case VisualClass.Cluster:
                    request.Text = "Assigned clusters for {0}";
                    Assignment.AddHeaders(request);

                    foreach (var ass in Assignments.List)
                    {
                        request.Add(ass.Cluster, ass.GetHeaders());
                    }

                    break;

                case VisualClass.Assignment:
                    request.Text = "Assignments for {0}";
                    request.AddRange(Assignments.List);
                    break;


                case VisualClass.Compound:
                    request.Text = "Potential compounds of {0}";

                    foreach (var annotation in Annotations)
                    {
                        request.Add(annotation.Compound);
                    }

                    break;

                case VisualClass.Annotation:
                    request.Text = "Annotations for {0}";

                    foreach (var annotation in Annotations)
                    {
                        request.Add(annotation);
                    }

                    break;

                case VisualClass.Adduct:
                    {
                        request.Text = "Adducts of potential compounds for {0}";

                        HashSet<Adduct> counter = new HashSet<Adduct>();

                        foreach (var c in Annotations)
                        {
                            counter.Add(c.Adduct);
                        }

                        foreach (var kvp in counter)
                        {
                            request.Add(kvp);
                        }
                    }
                    break;

                case VisualClass.Pathway:
                    {
                        request.Text = "Pathways of potential compounds for {0}";
                        request.AddExtraColumn("Compounds", "Compounds in this pathway potentially represented by {0}.");

                        Dictionary<Pathway, List<Compound>> counter = new Dictionary<Pathway, List<Compound>>();

                        foreach (var pc in Annotations)
                        {
                            foreach (var p in pc.Compound.Pathways)
                            {
                                counter.GetOrNew(p).Add(pc.Compound);
                            }
                        }

                        foreach (var kvp in counter)
                        {
                            request.Add(kvp.Key, kvp.Value);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        VisualClass IVisualisable.VisualClass
        {
            get { return VisualClass.Peak; }
        }

        /// <summary>
        /// Gets the statistic by the name of "x", otherwise NaN.
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
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            var columns = new List<Column<Peak>>();

            columns.Add("Name", EColumn.Visible, λ => λ.DisplayName);
            columns.Add("Comment", EColumn.None, λ => λ.Comment);
            columns.Add("№ corrections", EColumn.None, λ => λ.CorrectionChain.Count);
            columns.Add("Index", EColumn.None, λ => λ.Index);
            columns.Add("LC-MS mode", EColumn.None, λ => λ.LcmsMode);
            columns.Add("m/z", EColumn.None, λ => λ.Mz);
            columns.Add("Observations (all)", EColumn.None, λ => λ.Observations.Raw);
            columns.Add("Observations (trend)", EColumn.None, λ => λ.Observations.Trend);

            columns.Add("Clusters\\All", EColumn.None, λ => λ.Assignments.Clusters);
            columns.Add("Clusters\\Combination (for colours)", EColumn.None, z => StringHelper.ArrayToString(z.Assignments.Clusters));
            columns.Add("Clusters\\All (scores)", EColumn.None, λ => λ.Assignments.Scores);

            columns.Add(COLNAME_CLUSTERS_UNIQUE, EColumn.None, λ => new HashSet<Cluster>(λ.Assignments.Clusters).ToArray());
            columns.Add("Clusters\\Grouped", EColumn.None, λ => StringHelper.ArrayToString(λ.Assignments.List.OrderBy(z => z.Vector.Group?.Id).Select(z => (z.Vector.Group != null ? (z.Vector.Group.ShortName + "=") : "") + z.Cluster.ShortName)));

            foreach (GroupInfo group in core.Groups)
            {
                var closure = group;
                columns.Add("Clusters\\" + UiControls.ZEROSPACE + group.Name, EColumn.None, λ => λ.Assignments.List.Where(z => z.Vector.Group == closure).Select(z => z.Cluster).ToArray());
                columns[columns.Count - 1].Colour = z => closure.Colour;
                columns.Add("Clusters\\" + UiControls.ZEROSPACE + group.Name + " (scores)", EColumn.None, λ => λ.Assignments.List.Where(z => z.Vector.Group == closure).Select(z => z.Score).ToArray());
                columns[columns.Count - 1].Colour = z => closure.Colour;
            }

            foreach (PeakFlag flag in core.Options.PeakFlags)
            {
                var closure = flag;
                columns.Add("Flags\\" + UiControls.ZEROSPACE + flag, EColumn.None, λ => λ.CommentFlags.Contains(closure) ? closure.Id : string.Empty);
                columns[columns.Count - 1].Colour = z => closure.Colour;
            }

            columns.Add("Clusters\\Groupless", EColumn.None, λ => λ.Assignments.List.Where(z => z.Vector.Group == null).Select(z => z.Cluster).ToList());
            columns.Add("Clusters\\Groupless (scores)", EColumn.None, λ => λ.Assignments.List.Where(z => z.Vector.Group == null).Select(z => z.Score).ToList());

            columns.Add("Flags\\All", EColumn.None, λ => StringHelper.ArrayToString(λ.CommentFlags));


            columns.Add("Comment", EColumn.None, λ => λ.Comment);

            foreach (ConfigurationStatistic stat in core.ActiveStatistics)
            {
                var closure = stat;
                columns.Add("Statistic\\" + stat.ToString(), EColumn.Statistic, λ => λ.GetStatistic(closure));
                columns[columns.Count - 1].Colour = z => UiControls.StatisticColour(closure, z.Statistics);
            }

            columns.Add("Compounds", EColumn.None, λ => λ.Annotations.Select(λλ => λλ.Compound));
            columns.Add("Adducts", EColumn.None, λ => λ.Annotations.Select(λλ => λλ.Adduct));
            columns.Add("Similar peaks", EColumn.None, λ => λ.SimilarPeaks);

            core._peakMeta.ReadAllColumns(z => z.MetaInfo, columns);

            foreach (GroupInfo ti in core.Groups)
            {
                int i = ti.Order;
                columns.Add("Mean\\" + ti.Name, EColumn.None, λ => λ.Observations.Mean[i]);
                columns.Add("Std.Dev\\" + ti.Name, EColumn.None, λ => λ.Observations.StdDev[i]);
            }

            foreach (PeakFilter fi in core.AllPeakFilters)
            {
                var closure = fi;
                columns.Add("Filter\\" + fi.ToString(), EColumn.None, z => fi.Test(z) ? "✔" : "✘");
            }

            return columns;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            return this.Annotations.Count == 0 ? UiControls.ImageListOrder.VariableU : UiControls.ImageListOrder.Variable;
        }
    }
}
