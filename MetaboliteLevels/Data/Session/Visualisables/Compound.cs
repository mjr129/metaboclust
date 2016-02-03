using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers;
using MetaboliteLevels.Viewers.Charts;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Settings;
using MSerialisers;

namespace MetaboliteLevels.Data.Visualisables
{
    /// <summary>
    /// Chemical compounds
    /// (aka. Known metabolites)
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class Compound : IVisualisable
    {
        /// <summary>
        /// Compound name
        /// </summary>
        private readonly string _defaultName;

        /// <summary>
        /// Unique ID
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Mass
        /// </summary>
        public decimal Mass;

        /// <summary>
        /// Implicated pathways
        /// </summary>
        public readonly List<Pathway> Pathways = new List<Pathway>();

        /// <summary>
        /// Variables potentially representing this compound
        /// </summary>
        public readonly List<Annotation> Annotations = new List<Annotation>();

        /// <summary>
        /// Meta info
        /// </summary>
        public readonly MetaInfoCollection MetaInfo = new MetaInfoCollection();

        /// <summary>
        /// User provided comments.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// User provided name.
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// Unused (can't be disabled)
        /// </summary>
        bool ITitlable.Enabled { get { return true; } set { } }

        /// <summary>
        /// Defining library.
        /// </summary>
        public readonly List<CompoundLibrary> Libraries = new List<CompoundLibrary>();

        public Compound(CompoundLibrary tag, string name, string id, decimal mz)
        {
            if (tag != null)
            {
                this.Libraries.Add(tag);
            }

            this._defaultName = RemoveHtml(name);
            this.Id = id;
            this.Mass = mz;
        }

        private static string RemoveHtml(string name)
        {
            name = name.Replace("&alpha;", "α");
            name = name.Replace("&beta;", "β");
            name = name.Replace("&gamma;", "γ");
            name = name.Replace("&delta;", "δ");
            name = name.Replace("&epsilon;", "ε");
            name = name.Replace("&omega;", "ω");
            name = name.Replace("&Alpha;", "Α");
            name = name.Replace("&Beta;", "Β");
            name = name.Replace("&Gamma;", "Γ");
            name = name.Replace("&Delta;", "Δ");
            name = name.Replace("&Epsilon;", "Ε");
            name = name.Replace("&omega;", "Ω");
            name = name.Replace("&Psi;", "Ψ");
            name = name.Replace("&psi;", "ψ");
            name = name.Replace("&Xi;", "Ξ");
            name = name.Replace("&xi;", "ξ");
            name = name.Replace("&Chi;", "Χ");
            name = name.Replace("&chi;", "χ");
            return name;
        }

        /// <summary>
        /// Default display name.
        /// </summary>
        public string DefaultDisplayName
        {
            get
            {
                return _defaultName;
            }
        }

        internal StylisedCluster CreateStylisedCluster(Core core, IVisualisable highlight)
        {
            Cluster fakeCluster = new Cluster(this.DefaultDisplayName, null);
            var colourInfo = new Dictionary<Peak, LineInfo>();
            string caption = "Plot of peaks potentially representing {0}.";

            // Compound --> Peaks in compound
            // Adduct: NA
            // Peak: Peak
            // Cluster: Peaks in cluster
            // Pathway: None

            StylisedCluster.HighlightElement[] toHighlight = null;

            if (highlight != null)
            {
                switch (highlight.VisualClass)
                {
                    case VisualClass.Peak:
                        toHighlight = new StylisedCluster.HighlightElement[] { new StylisedCluster.HighlightElement((Peak)highlight, null) };
                        caption += " {1} is shown in red.";
                        break;

                    case VisualClass.Cluster:
                        Cluster highlightCluster = (Cluster)highlight;
                        toHighlight = highlightCluster.Assignments.Vectors.Select(StylisedCluster.HighlightElement.FromVector).ToArray();
                        caption += " Peaks in {1} are shown in red.";
                        break;
                }
            }

            Peak[] peaks = this.Annotations.Select(z => z.Peak).ToArray();

            ValueMatrix vm = ValueMatrix.Create(peaks, true, core, ObsFilter.Empty, false, ProgressReporter.GetEmpty());

            for (int index = 0; index < vm.NumVectors; index++)
            {
                var vec = vm.Vectors[index];
                Peak peak = vec.Peak;

                fakeCluster.Assignments.Add(new Assignment(vec, fakeCluster, double.NaN));

                StringBuilder sb = new StringBuilder();

                if (peak.Annotations.Count > 5)
                {
                    sb.Append(this.DefaultDisplayName + " OR " + (peak.Annotations.Count - 1) + " others");
                }
                else
                {
                    sb.Append(this.DefaultDisplayName);

                    foreach (Annotation c2 in peak.Annotations)
                    {
                        if (c2.Compound != this)
                        {
                            sb.Append(" OR ");
                            sb.Append(c2.Compound.DefaultDisplayName + " (" + c2.Adduct.DefaultDisplayName + ")");
                        }
                    }
                }

                colourInfo.Add(peak, new LineInfo(peak.DisplayName + ": " + sb.ToString(), Color.Black, ChartDashStyle.Solid));
            }

            var r = new StylisedCluster(fakeCluster, this, colourInfo);
            r.IsFake = true;
            r.CaptionFormat = caption;
            r.Source = highlight;
            r.Highlight = toHighlight;
            return r;
        }

        /// <summary>
        /// Inherited from IVisualisable. 
        /// </summary>
        public string DisplayName
        {
            get { return IVisualisableExtensions.GetDisplayName(OverrideDisplayName, DefaultDisplayName); }
        }

        /// <summary>
        /// Inherited from IVisualisable. 
        /// </summary>
        public Image REMOVE_THIS_FUNCTION
        {
            get { return Annotations.Count == 0 ? Resources.ObjLCompoundU : Resources.ObjLCompound; }
        }          

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public VisualClass VisualClass
        {
            get { return VisualClass.Compound; }
        }

        /// <summary>
        /// Debugging.
        /// </summary>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public void RequestContents(ContentsRequest request)
        {
            switch (request.Type)
            {
                case VisualClass.Peak:
                    request.Text = "Potential peaks for {0}";

                    foreach (var pp in this.Annotations)
                    {
                        request.Add(pp.Peak);
                    }
                    break;

                case VisualClass.Cluster:
                    request.Text = "Clusters representing potential peaks of {0}";
                    request.AddHeader("Num. in compound", "Number of peaks in {0} in this cluster");

                    Counter<Cluster> counts = new Counter<Cluster>();

                    foreach (var p in Annotations)
                    {
                        foreach (Cluster pat in p.Peak.Assignments.Clusters)
                        {
                            counts.Increment(pat); // assume there is only one of each cluster per peak so the count holds
                        }
                    }

                    request.AddRangeWithCounts(counts);

                    break;

                case VisualClass.Annotation:
                    request.Text = "Annotations using {0}";
                    request.AddRange(Annotations);
                    break;

                case VisualClass.Compound:
                    break;

                case VisualClass.Adduct:
                    break;

                case VisualClass.Pathway:
                    request.Text = "List of pathways implicating {0}";
                    request.AddRange(this.Pathways);
                    break;

                default:
                    break;
            }
        }

        public string Url
        {
            get
            {
                return "http://mediccyc.noble.org/MEDIC/new-image?type=COMPOUND&object=" + Id;
            }
        }

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {                      
            var columns = new List<Column<Compound>>();

            columns.Add("Name", EColumn.Visible, λ => λ.DefaultDisplayName);
            columns.Add("Comment", EColumn.Visible, λ => λ.Comment);
            columns.Add("Libraries", EColumn.None, λ => λ.Libraries);
            columns.Add("Mass", EColumn.None, λ => λ.Mass);
            columns.Add("Pathways", EColumn.None, λ => λ.Pathways);
            columns.Add("Annotations", EColumn.Visible, λ => λ.Annotations);
            columns.Add("ID", EColumn.None, λ => λ.Id);
            columns.Add("Comment", EColumn.None, λ => λ.Comment);
            columns.Add("Mass", EColumn.None, λ => λ.Mass);
            columns.Add("URL", EColumn.None, λ => λ.Url);

            core._compoundsMeta.ReadAllColumns(z => z.MetaInfo, columns);

            return columns;
        }

        public UiControls.ImageListOrder GetIcon()
        {
            // IMAGE
            if (this.Annotations.Count == 0)
            {
                return UiControls.ImageListOrder.CompoundU;
            }
            else
            {
                return UiControls.ImageListOrder.Compound;
            }
        }
    }
}
