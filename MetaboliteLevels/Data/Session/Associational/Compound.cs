using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
using System.Drawing.Drawing2D;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.Visualisables
{
    /// <summary>
    /// Chemical compounds
    /// (aka. Known metabolites)
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class Compound : IAssociational
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
        bool INameable.Enabled { get { return true; } set { } }

        /// <summary>
        /// Defining library.
        /// </summary>
        public readonly List<CompoundLibrary> Libraries = new List<CompoundLibrary>();

        /// <summary>
        /// CONSTRUCTOR
        /// </summary> 
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

        /// <summary>
        /// Helper function to replace old-style HTML symbols with unicode equivalent.
        /// </summary>                     
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

        /// <summary>
        /// Creates a StylisedCluster for plotting from this cluster.
        /// </summary>
        /// <param name="core">Core</param>
        /// <param name="highlight">What to highlight in the result</param>
        /// <returns>A StylisedCluster</returns>
        internal StylisedCluster CreateStylisedCluster(Core core, IAssociational highlight )
        {
            Cluster fakeCluster = new Cluster(this.DefaultDisplayName, null);
            Dictionary<Peak, LineInfo> colourInfo = new Dictionary<Peak, LineInfo>();
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
                Vector vec = vm.Vectors[index];
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

                colourInfo.Add(peak, new LineInfo(peak.DisplayName + ": " + sb.ToString(), Color.Black, DashStyle.Solid));
            }

            StylisedCluster r = new StylisedCluster(fakeCluster, this, colourInfo);
            r.IsFake = true;
            r.CaptionFormat = caption;
            r.WhatIsHighlighted = highlight;
            r.Highlight = toHighlight;
            return r;
        }

        /// <summary>
        /// IMPELEMENTS IVisualisable
        /// </summary>
        public string DisplayName
        {
            get { return IVisualisableExtensions.FormatDisplayName(this); }
        }

        /// <summary>
        /// IMPELEMENTS IVisualisable
        /// </summary>
        VisualClass IAssociational.VisualClass
        {
            get { return VisualClass.Compound; }
        }

        /// <summary>
        /// OVERRIDES Object
        /// </summary>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// IMPELEMENTS IVisualisable
        /// </summary>
        void IAssociational.RequestContents(ContentsRequest request)
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
                    request.AddExtraColumn("Num. in compound", "Number of peaks in {0} in this cluster");

                    Utilities.Counter<Cluster> counts = new Utilities.Counter<Cluster>();

                    foreach (Annotation p in Annotations)
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

        /// <summary>
        /// Gets the URL (used to "view online")
        /// </summary>
        public string Url
        {
            get // TODO: FIX THIS! It shouldn't reference MedicCyc!
            {
                return "http://mediccyc.noble.org/MEDIC/new-image?type=COMPOUND&object=" + Id;
            }
        }

        /// <summary>
        /// IMPELEMENTS IVisualisable
        /// </summary>               
        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<Compound>> columns = new List<Column<Compound>>();

            columns.Add("Name", EColumn.Visible, λ => λ.DefaultDisplayName);
            columns.Add("Comment", EColumn.Visible, λ => λ.Comment);
            columns.Add("Libraries", EColumn.None, λ => λ.Libraries);
            columns.Add("Mass", EColumn.None, λ => λ.Mass);
            columns.Add("Pathways", EColumn.None, λ => λ.Pathways);
            columns.Add( "Annotation status", EColumn.Visible, λ => λ.GetAnnotationStatus() );
            columns.Add( "Annotations\\Peaks", EColumn.Visible, λ => λ.Annotations.Select( z => z.Peak ) );
            columns.Add( "Annotations\\Status", EColumn.None, λ => λ.Annotations.Select( z => z.Status ) );
            columns.Add( "Annotations\\Compounds", EColumn.None, λ => λ.Annotations.Select( z => z.Compound ) );
            columns.Add( "Annotations\\Annotations", EColumn.None, λ => λ.Annotations );
            columns.Add("ID", EColumn.None, λ => λ.Id);
            columns.Add("Comment", EColumn.None, λ => λ.Comment);
            columns.Add("URL", EColumn.None, λ => λ.Url);

            core._compoundsMeta.ReadAllColumns(z => z.MetaInfo, columns);

            return columns;
        }               

        public EAnnotation GetAnnotationStatus()
        {
            // IMAGE
            if (this.Annotations.Count == 0)
            {
                return (EAnnotation)(-1);
            }
            else
            {
                return this.Annotations.Max( z => z.Status );
            }
        }

        /// <summary>
        /// IMPELEMENTS IVisualisable
        /// </summary>               
        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            if (this.Annotations.Count == 0)
            {
                return UiControls.ImageListOrder.Compound0;
            }
            else
            {
                switch (this.Annotations.Max( z => z.Status ))
                {
                    case EAnnotation.Tentative:
                        return UiControls.ImageListOrder.CompoundT;

                    case EAnnotation.Affirmative:
                        return UiControls.ImageListOrder.CompoundA;

                    case EAnnotation.Confirmed:
                        return UiControls.ImageListOrder.CompoundC;

                    default:
                        throw new SwitchException();
                }
            }
        }
    }
}
