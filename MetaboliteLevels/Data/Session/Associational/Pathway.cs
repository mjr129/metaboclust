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
using System.Drawing.Drawing2D;
using MetaboliteLevels.Data.Session.Associational;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Visualisables
{
    /// <summary>
    /// Pathways (essentially sets of compounds)
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class Pathway : Associational
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
        /// Source libraries.
        /// </summary>
        public readonly List<CompoundLibrary> Libraries = new List<CompoundLibrary>();

        /// <summary>
        /// Columns in the original data we didn't use for anything
        /// </summary>
        public readonly MetaInfoCollection MetaInfo = new MetaInfoCollection();

        /// <summary>
        /// Related pathways (because the source database says so)
        /// </summary>
        public readonly List<Pathway> RelatedPathways = new List<Pathway>();

        /// <summary>
        /// Compounds in this pathway
        /// </summary>
        public readonly List<Compound> Compounds = new List<Compound>();

        /// <summary>
        /// CONSTRUCTOR
        /// </summary> 
        public Pathway(CompoundLibrary tag, string name, string id)
        {
            if (tag != null)
            {
                this.Libraries.Add(tag);
            }

            this.Id = id;
            this._defaultName = !string.IsNullOrWhiteSpace(name) ? name : "Compounds not assigned to any pathway";
        }

        public override EPrevent SupportsHide => EPrevent.Hide;

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override string DefaultDisplayName
        {
            get
            {
                return _defaultName;
            }
        }    

        /// <summary>
        /// Creates a StylisedCluster for plotting from this pathway.
        /// </summary>
        /// <param name="core">Core</param>
        /// <param name="highlightContents">What to highlight in the plot</param>
        /// <returns>A StylisedCluster</returns>
        internal StylisedCluster CreateStylisedCluster(Core core, IntensityMatrix source, Associational highlightContents )
        {
            var colours = new Dictionary<Peak, LineInfo>();

            // Adduct: NA
            // Peak: Pathways for peak -> Peaks (THIS PEAK)
            // Cluster: Pathways for cluster -> Peaks (PEAKS IN CLUSTER)
            // Compound: Pathway for compound -> Peaks (PEAKS IN COMPOUND)
            // Pathway: NA
            const string caption1 = "Plot of peaks potentially representing compounds implicated in {0}.";
            string caption2 = " Colours represent peaks that share the same cluster(s).";
            string caption3;

            // Find out the peaks that we are supposed to highlight
            StylisedCluster.HighlightElement[] toHighlight = null;

            if (highlightContents != null)
            {
                switch (highlightContents.AssociationalClass)
                {
                    case EVisualClass.Compound:
                        Compound highlightCompound = (Compound)highlightContents;
                        toHighlight = highlightCompound.Annotations.Select(z => new StylisedCluster.HighlightElement(z, null)).ToArray();
                        caption3 = " Peaks potentially representing {1} are {HIGHLIGHTED}.";
                        break;

                    case EVisualClass.Cluster:
                        Cluster highlightCluster = (Cluster)highlightContents;
                        toHighlight = highlightCluster.Assignments.Vectors.Select(StylisedCluster.HighlightElement.FromVector).ToArray();
                        caption3 = " Peaks potentially representing compounds in {1} are {HIGHLIGHTED}.";

                        break;

                    case EVisualClass.Peak:
                        toHighlight = new StylisedCluster.HighlightElement[] { new StylisedCluster.HighlightElement((Peak)highlightContents, null) };
                        caption3 = " {1} is {HIGHLIGHTED}.";
                        break;

                    default:
                        caption3 = "";
                        break;
                }
            }
            else
            {
                caption3 = "";
            }


            bool highlightByCompound = false;

            // Make a list of the peaks in this pathway
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

            // Depending on the mode assign either each combination of compounds OR clusters a unique colour
            Cluster fakeCluster = new Cluster(this.DisplayName, null);
            List<List<Compound>> uniqueCompoundCombinations = new List<List<Compound>>();
            List<List<Cluster>> uniqueClusterCombinations = new List<List<Cluster>>();

            int cindex = -1;

            IntensityMatrix vm = source.Subset(z=>  peaks.ContainsKey( z ));

            for (int vIndex = 0; vIndex < vm.NumVectors; vIndex++)
            {
                var vec = vm.Vectors[vIndex];
                Peak peak = vec.Peak;
                var asses = vec.Peak.FindAssignments( core ).ToArray();
                List<Compound> compounds = peaks[peak];
                Color col;

                // Find or create peak in list
                if (highlightByCompound)
                {
                    int uniqueIndex = Maths.FindMatch(uniqueCompoundCombinations, compounds);

                    if (uniqueIndex == -1)
                    {
                        uniqueIndex = uniqueCompoundCombinations.Count;
                        uniqueCompoundCombinations.Add(compounds); // add list of peaks for this peak
                    }

                    if (uniqueCompoundCombinations[uniqueIndex].Count == 1)
                    {
                        col = Color.Black;
                    }
                    else
                    {
                        cindex++;

                        if (cindex == UiControls.BrightColours.Length)
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
                            col = UiControls.BrightColours[cindex % UiControls.BrightColours.Length];
                        }
                    }
                }
                else
                {
                    var l = asses.Select(z=> z.Cluster ).ToList();
                    int uniqueIndex = Maths.FindMatch(uniqueClusterCombinations, l);

                    if (uniqueIndex == -1)
                    {
                        uniqueIndex = uniqueClusterCombinations.Count;
                        uniqueClusterCombinations.Add(l); // add list of peaks for this peak
                    }

                    col = UiControls.BrightColours[uniqueIndex % UiControls.BrightColours.Length];
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


                string seriesName = peak.DisplayName + (!asses.IsEmpty() ? (" (" + StringHelper.ArrayToString( asses.Select(z=> z.Cluster )) + ")") : "") + ": " + legend.ToString();
                var li = new LineInfo(seriesName, col, DashStyle.Solid);
                colours.Add(peak, li);
            }

            var r = new StylisedCluster(fakeCluster, this, colours);
            r.IsFake = true;
            r.Highlight = toHighlight;
            r.CaptionFormat = (caption1 + caption2 + caption3);
            r.WhatIsHighlighted = highlightContents;
            return r;
        }          

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override EVisualClass AssociationalClass
        {
            get { return EVisualClass.Pathway; }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override void FindAssociations(ContentsRequest request)
        {
            switch (request.Type)
            {
                case EVisualClass.Peak:
                    {
                        request.Text = "Potential peaks of compounds in {0}";
                        request.AddExtraColumn("Compounds", "Compounds potentially representing this peak in {0}.");

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

                case EVisualClass.Annotation:
                    request.Text = "Annotations with compounds in {0}";

                    foreach (Compound c in this.Compounds)
                    {
                        request.AddRange(c.Annotations);
                    }
                    break;

                case EVisualClass.Cluster:
                    {
                        request.Text = "Clusters representing potential peaks of compounds in {0}";
                        request.AddExtraColumn("Peaks", "Number of peaks in this cluster in with compounds in {0}");
                        request.AddExtraColumn("Compounds", "Number of compounds with peaks in this cluster with peaks also in {0}");

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

                case EVisualClass.Compound:
                    {
                        request.Text = "Compounds in {0}";
                        request.AddExtraColumn("Clusters", "Clusters");  // TODO: This is actually generic to all compounds and not related to this Pathway class

                        foreach (var c in this.Compounds)
                        {
                            HashSet<Cluster> pats = new HashSet<Cluster>();

                            foreach (Annotation p in c.Annotations)
                            {
                                foreach (Cluster pat in p.Peak.FindAssignments(request.Core ).Select(z=> z.Cluster ))
                                {
                                    pats.Add(pat);
                                }
                            }

                            request.Add(c, (object)pats.ToArray());
                        }
                    }
                    break;

                case EVisualClass.Adduct:
                    break;

                case EVisualClass.Pathway:
                    {
                        request.Text = "Pathways related to {0}";

                        request.AddRange(RelatedPathways);
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// URL used for "view online" option
        /// </summary>
        public string Url
        {
            get
            {
                return "http://mediccyc.noble.org/MEDIC/new-image?type=PATHWAY&object=" + Id; // TODO: No!
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        public override IEnumerable<Column> GetColumns(Core core)
        {
            var result = new List<Column<Pathway>>();

            result.Add("ID", EColumn.None, λ => λ.Id);
            result.Add("Name", EColumn.Visible, λ => λ.DefaultDisplayName);
            result.Add("Library", EColumn.None, λ => λ.Libraries);
            result.Add("Compounds", EColumn.None, λ => λ.Compounds);
            result.Add("Compounds with peaks", EColumn.None, λ => λ.Compounds.Where(z => z.Annotations.Count != 0));
            result.Add("Comment", EColumn.None, λ => λ.Comment);
            result.Add("Libraries", EColumn.None, λ => λ.Libraries);
            result.Add("Related pathways", EColumn.None, λ => λ.RelatedPathways);
            result.Add("URL", EColumn.None, λ => λ.Url);        

            core._pathwaysMeta.ReadAllColumns(z => z.MetaInfo, result);

            return result;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        public override UiControls.ImageListOrder Icon=>UiControls.ImageListOrder.Pathway;
    }
}
