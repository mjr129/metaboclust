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
        /// Original name of this variable.
        /// Use DisplayName to get the name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// User comments.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Which cluster has this been assigned to.
        /// </summary>
        public readonly AssignmentList Assignments = new AssignmentList();

        /// <summary>
        /// User comments.
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
        public Peak(int index, string name, PeakValueSet observations, PeakValueSet altObservations, ELcmsMode lcmsmode, decimal mz)
        {
            this.Index = index;
            this.Name = name;
            this.OriginalObservations = observations;
            this.Observations = observations;
            this.AltObservations = altObservations;
            this.Mz = mz;
            this.LcmsMode = lcmsmode;
        }

        public override string ToString()
        {
            return DisplayName + " (" + Maths.ArrayToString(Assignments.Clusters) + ")";
        }

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
        /// Inherited from IVisualisable. 
        /// </summary>
        public string DisplayName
        {
            get { return Title ?? Name; }
        }

        /// <summary>
        /// Inherited from IVisualisable. 
        /// </summary>
        public Image DisplayIcon
        {
            get
            {
                return Annotations.Count == 0 ? Resources.ObjLVariableU : Resources.ObjLVariable;
            }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public IEnumerable<InfoLine> GetInformation(Core core)
        {
            yield return new InfoLine("Alt. Observations", this.AltObservations != null);
            yield return new InfoLine("№ assignments", Assignments.List.Count);
            yield return new InfoLine("Comment", Comment);
            yield return new InfoLine("Flags", Maths.ArrayToString(CommentFlags));
            yield return new InfoLine("№ corrections", this.CorrectionChain.Count);
            yield return new InfoLine("Display name", this.DisplayName);
            yield return new InfoLine("Index", Index);
            yield return new InfoLine("LC-MS mode", this.LcmsMode.ToUiString());
            yield return new InfoLine("m/z", Mz);
            yield return new InfoLine("Display name", DisplayName);
            yield return new InfoLine("Original name", Name);
            yield return new InfoLine("Custom name", Title);
            yield return new InfoLine("№ Observations (all)", this.Observations.Raw.Length);
            yield return new InfoLine("№ Observations (trend)", this.Observations.Trend.Length);
            yield return new InfoLine("№ potential compounds", Annotations.Count);
            yield return new InfoLine("№ similar peaks", this.SimilarPeaks.Count);
            yield return new InfoLine("№ statistics", this.Statistics.Count);
            yield return new InfoLine("Class", this.VisualClass.ToUiString());

            foreach (InfoLine il in core._peakMeta.ReadAll(this.MetaInfo))
            {
                yield return il;
            }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public IEnumerable<InfoLine> GetStatistics(Core core)
        {
            foreach (var v in Statistics)
            {
                yield return new InfoLine(v.Key.ToString(), v.Value);
            }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public void RequestContents(ContentsRequest request)
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
                        request.AddHeader("Compounds", "Compounds in this pathway potentially represented by {0}.");

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
        /// Implements IVisualisable. 
        /// </summary>
        public VisualClass VisualClass
        {
            get { return VisualClass.Peak; }
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

        public IEnumerable<Column> GetColumns(Core core)
        {
            var columns = new List<Column<Peak>>();

            columns.Add("Name", true, λ => λ.DisplayName);
            columns.Add("Clusters\\All", false, λ => λ.Assignments.Clusters);
            columns.Add("Clusters\\All (scores)", true, λ => λ.Assignments.Scores);

            columns.Add(COLNAME_CLUSTERS_UNIQUE, true, λ => new HashSet<Cluster>(λ.Assignments.Clusters).ToArray());
            columns.Add("Clusters\\Grouped", false, λ => Maths.ArrayToString(λ.Assignments.List.OrderBy(z => z.Vector.Group.Id).Select(z => (z.Vector.Group != null ? (z.Vector.Group.ShortName + "=") : "") + z.Cluster.ShortName)));

            foreach (var group in core.Groups)
            {
                var closure = group;
                columns.Add("Clusters\\" + UiControls.ZEROSPACE + group.Name, false, λ => λ.Assignments.List.Where(z => z.Vector.Group == closure).Select(z => z.Cluster).ToArray());
                columns[columns.Count - 1].Colour = z => closure.Colour;
                columns.Add("Clusters\\" + UiControls.ZEROSPACE + group.Name + " (scores)", false, λ => λ.Assignments.List.Where(z => z.Vector.Group == closure).Select(z => z.Score).ToArray());
                columns[columns.Count - 1].Colour = z => closure.Colour;
            }

            foreach (var flag in core.Options.PeakFlags)
            {
                PeakFlag closure = flag;
                columns.Add("Flags\\" + UiControls.ZEROSPACE + flag, false, λ => λ.CommentFlags.Contains(closure) ? closure.Id : string.Empty);
                columns[columns.Count - 1].Colour = z => closure.Colour;
            }

            columns.Add("Clusters\\Groupless", false, λ => λ.Assignments.List.Where(z => z.Vector.Group == null).Select(z => z.Cluster).ToList());
            columns.Add("Clusters\\Groupless (scores)", false, λ => λ.Assignments.List.Where(z => z.Vector.Group == null).Select(z => z.Score).ToList());

            columns.Add("Flags\\All", false, λ => Maths.ArrayToString(λ.CommentFlags));


            columns.Add("Comment", false, λ => λ.Comment);

            foreach (var stat in core.Statistics)
            {
                ConfigurationStatistic closure = stat;
                columns.Add("Statistic\\" + stat.ToString(), false, λ => λ.GetStatistic(closure));
                columns[columns.Count - 1].Colour = z => UiControls.StatisticColour(closure, z.Statistics);
            }

            columns.Add("Compounds", false, λ => λ.Annotations.Select(λλ => λλ.Compound));
            columns.Add("Adducts", false, λ => λ.Annotations.Select(λλ => λλ.Adduct));
            columns.Add("Similar peaks", false, λ => λ.SimilarPeaks);

            core._peakMeta.ReadAllColumns(z => z.MetaInfo, columns);

            foreach (var ti in core.Groups)
            {
                int i = ti.Order;
                columns.Add("Mean\\" + core.GetTypeName(ti.Id), false, λ => λ.Observations.Mean[i]);
                columns.Add("Std.Dev\\" + core.GetTypeName(ti.Id), false, λ => λ.Observations.StdDev[i]);
            }

            foreach (var fi in core.PeakFilters)
            {
                var closure = fi;
                columns.Add("Filter\\" + fi.ToString(), false, z => fi.Test(z) ? "✔" : "✘");
            }

            return columns;
        }

        public int GetIcon()
        {
            return this.Annotations.Count == 0 ? UiControls.ImageListOrder.VariableU : UiControls.ImageListOrder.Variable;
        }
    }
}
