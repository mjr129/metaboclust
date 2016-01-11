using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers;
using MetaboliteLevels.Viewers.Charts;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Settings;
using MSerialisers;

namespace MetaboliteLevels.Data.Visualisables
{
    /// <summary>
    /// Pathways (essentially sets of compounds)
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class Pathway : IVisualisable
    {
        /// <summary>
        /// Pathway name
        /// </summary>
        private readonly string _defaultName;

        /// <summary>
        /// Unique ID
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// User comments.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// User comments.
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// Source libraries.
        /// </summary>
        public readonly List<CompoundLibrary> Libraries = new List<CompoundLibrary>();

        public readonly MetaInfoCollection MetaInfo = new MetaInfoCollection();

        public readonly List<Pathway> RelatedPathways = new List<Pathway>();

        /// <summary>
        /// Compounds in this pathway
        /// </summary>
        public readonly List<Compound> Compounds = new List<Compound>();

        /// <summary>
        /// Unused (can't be disabled)
        /// </summary>
        bool ITitlable.Enabled { get { return true; } set { } }

        public Pathway(CompoundLibrary tag, string name, string id)
        {
            if (tag != null)
            {
                this.Libraries.Add(tag);
            }

            this.Id = id;
            this._defaultName = !string.IsNullOrWhiteSpace(name) ? name : "Compounds not assigned to any pathway";
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
        /// Debugging.
        /// </summary>
        public override string ToString()
        {
            return DisplayName + " (" + Compounds.Count + ")";
        }

        internal StylisedCluster CreateStylisedCluster(Core core, IVisualisable highlightContents)
        {
            var colours = new Dictionary<Peak, LineInfo>();

            // Adduct: NA
            // Peak: Pathways for peak -> Peaks (THIS PEAK)
            // Cluster: Pathways for cluster -> Peaks (PEAKS IN PATTERN)
            // Compound: Pathway for compound -> Peaks (PEAKS IN COMPOUND)
            // Pathway: NA

            StylisedCluster.HighlightElement[] toHighlight = null;
            const string caption1 = "Plot of peaks potentially representing compounds implicated in {0}.";
            string caption2 = " Sets of peaks that may represent the same compound or set of compounds are shown in the same colour. Other peaks are shown in black.";
            string caption3 = "";

            if (highlightContents != null)
            {
                switch (highlightContents.VisualClass)
                {
                    case VisualClass.Compound:
                        Compound highlightCompound = (Compound)highlightContents;
                        toHighlight = highlightCompound.Annotations.Select(z => new StylisedCluster.HighlightElement(z, null)).ToArray();
                        caption3 = " Peaks potentially representing {1} are shown in red.";
                        break;

                    case VisualClass.Cluster:
                        Cluster highlightCluster = (Cluster)highlightContents;
                        toHighlight = highlightCluster.Assignments.Vectors.Select(StylisedCluster.HighlightElement.FromVector).ToArray();
                        caption3 = " Peaks potentially representing compounds in {1} are shown in red.";

                        break;

                    case VisualClass.Peak:
                        toHighlight = new StylisedCluster.HighlightElement[] { new StylisedCluster.HighlightElement((Peak)highlightContents, null) };
                        caption3 = " {1} is shown in red.";
                        break;
                }
            }

            // Make a list of the variables and the compounds in this cluster they might represent
            Dictionary<Peak, List<Compound>> peaks = new Dictionary<Peak, List<Compound>>();

            foreach (Compound compound in this.Compounds)
            {
                foreach (Annotation annotation in compound.Annotations)
                {
                    Peak peak = annotation.Peak;

                    if (peaks.ContainsKey(peak))
                    {
                        peaks[peak].Add(compound);
                    }
                    else
                    {
                        peaks[peak] = new List<Compound>();
                        peaks[peak].Add(compound);
                    }
                }
            }

            // Assign each combination of compounds a unique colour
            Cluster fakeCluster = new Cluster(this.DefaultDisplayName, null);
            List<List<Compound>> uniqueCombinations = new List<List<Compound>>();
            Color[] colors = { Color.Blue, Color.Green, Color.Olive, Color.DarkRed, Color.DarkMagenta, Color.DarkCyan, Color.LightGreen, Color.LightBlue, Color.Magenta, Color.Cyan, Color.YellowGreen };
            int cindex = -1;

            ValueMatrix vm = ValueMatrix.Create(peaks.Keys.ToArray(), true, core, ObsFilter.Empty, false, ProgressReporter.GetEmpty());

            for (int vIndex = 0; vIndex < vm.NumVectors; vIndex++)
            {
                var vec = vm.Vectors[vIndex];
                Peak peak = vec.Peak;
                List<Compound> compounds = peaks[peak];

                // Find or create peak in list
                int uniqueIndex = Maths.FindMatch(uniqueCombinations, compounds);

                if (uniqueIndex == -1)
                {
                    uniqueIndex = uniqueCombinations.Count;
                    uniqueCombinations.Add(compounds); // add list of peaks for this peak
                }

                StringBuilder legend = new StringBuilder();
                int xCount = 0;

                foreach (Annotation potential in peak.Annotations)
                {
                    if (this.Compounds.Contains(potential.Compound))
                    {
                        if (legend.Length != 0)
                        {
                            legend.Append(" OR ");
                        }

                        legend.Append(potential.Compound.DefaultDisplayName);
                    }
                    else
                    {
                        xCount++;
                    }
                }

                if (xCount != 0)
                {
                    legend.Append(" OR " + xCount + " others not in this pathway");
                }

                fakeCluster.Assignments.Add(new Assignment(vec, fakeCluster, double.NaN));

                Color col;

                if (uniqueCombinations[uniqueIndex].Count == 1)
                {
                    col = Color.Black;
                }
                else if (cindex == colors.Length)
                {
                    col = Color.Black;
                }
                else
                {
                    cindex++;

                    if (cindex == colors.Length)
                    {
                        // If there are too many don't bother
                        col = Color.Black;
                        caption2 = "";

                        foreach (var lii in colours)
                        {
                            lii.Value.Colour = Color.Black;
                        }
                    }
                    else
                    {
                        col = colors[cindex % colors.Length];
                    }
                }

                string seriesName = peak.DisplayName + (!peak.Assignments.List.IsEmpty() ? (" (" + StringHelper.ArrayToString(peak.Assignments.Clusters) + ")") : "") + ": " + legend.ToString();
                var li = new LineInfo(seriesName, col, System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid);
                colours.Add(peak, li);
            }

            var r = new StylisedCluster(fakeCluster, this, colours);
            r.IsFake = true;
            r.Highlight = toHighlight;
            r.CaptionFormat = (caption1 + caption2 + caption3);
            r.Source = highlightContents;
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
            get { return Properties.Resources.ObjLPathway; }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public IEnumerable<InfoLine> GetInformation(Core core)
        {
            yield return new InfoLine("№ compounds", Compounds.Count);
            yield return new InfoLine("Display name", DisplayName);
            yield return new InfoLine("Id", Id);
            yield return new InfoLine("№ libraries", this.Libraries.Count);
            yield return new InfoLine("Name", DefaultDisplayName);
            yield return new InfoLine("№ related pathways", this.RelatedPathways.Count);
            yield return new InfoLine("URL", this.Url);
            yield return new InfoLine("Class", this.VisualClass.ToUiString());

            foreach (InfoLine il in core._pathwaysMeta.ReadAll(this.MetaInfo))
            {
                yield return il;
            }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public IEnumerable<InfoLine> GetStatistics(Core core)
        {
            return new InfoLine[0];
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public VisualClass VisualClass
        {
            get { return VisualClass.Pathway; }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public void RequestContents(ContentsRequest request)
        {
            switch (request.Type)
            {
                case VisualClass.Peak:
                    {
                        request.Text = "Potential peaks of compounds in {0}";
                        request.AddHeader("Compounds", "Compounds potentially representing this peak in {0}.");

                        Dictionary<Peak, List<Compound>> dict = new Dictionary<Peak, List<Compound>>();

                        foreach (Compound c in this.Compounds)
                        {
                            foreach (Annotation p in c.Annotations)
                            {
                                dict.GetOrNew(p.Peak).Add(c);
                            }
                        }

                        foreach (var kvp in dict)
                        {
                            request.Add(kvp.Key, kvp.Value);
                        }
                    }
                    break;

                case VisualClass.Annotation:
                    request.Text = "Annotations with compounds in {0}";

                    foreach (Compound c in this.Compounds)
                    {
                        request.AddRange(c.Annotations);
                    }
                    break;

                case VisualClass.Cluster:
                    {
                        request.Text = "Clusters representing potential peaks of compounds in {0}";
                        request.AddHeader("Peaks", "Number of peaks in this cluster in with compounds in {0}");
                        request.AddHeader("Compounds", "Number of compounds with peaks in this cluster with peaks also in {0}");

                        foreach (Cluster cluster in request.Core.Clusters)
                        {
                            var peaks = new HashSet<Peak>();
                            var comps = new HashSet<Compound>();

                            foreach (Peak peak in cluster.Assignments.Peaks)
                            {
                                foreach (Annotation comp in peak.Annotations)
                                {
                                    comps.Add(comp.Compound);

                                    if (comp.Compound.Pathways.Contains(this))
                                    {
                                        peaks.Add(peak);
                                    }
                                }
                            }

                            if (peaks.Count != 0)
                            {
                                request.Add(cluster, peaks.ToArray(), comps.ToArray());
                            }
                        }
                    }
                    break;

                case VisualClass.Compound:
                    {
                        request.Text = "Compounds in {0}";
                        request.AddHeader("Clusters", "Clusters");  // TODO: This is actually generic to all compounds and not related to this Pathway class

                        foreach (var c in this.Compounds)
                        {
                            HashSet<Cluster> pats = new HashSet<Cluster>();

                            foreach (Annotation p in c.Annotations)
                            {
                                foreach (Cluster pat in p.Peak.Assignments.Clusters)
                                {
                                    pats.Add(pat);
                                }
                            }

                            request.Add(c, (object)pats.ToArray());
                        }
                    }
                    break;

                case VisualClass.Adduct:
                    break;

                case VisualClass.Pathway:
                    {
                        request.Text = "Pathways related to {0}";

                        request.AddRange(RelatedPathways);
                    }
                    break;

                default:
                    break;
            }
        }

        public string Url
        {
            get
            {
                return "http://mediccyc.noble.org/MEDIC/new-image?type=PATHWAY&object=" + Id;
            }
        }


        public IEnumerable<Column> GetColumns(Core core)
        {
            var result = new List<Column<Pathway>>();

            result.Add("ID", false, λ => λ.Id);
            result.Add("Name", true, λ => λ.DefaultDisplayName);
            result.Add("Library", false, λ => λ.Libraries);
            result.Add("Compounds", false, λ => λ.Compounds);
            result.Add("Comment", false, λ => λ.Comment);

            core._pathwaysMeta.ReadAllColumns(z => z.MetaInfo, result);

            return result;
        }

        public UiControls.ImageListOrder GetIcon()
        {
            return UiControls.ImageListOrder.Pathway;
        }
    }
}
