using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.Main
{
    /// <summary>
    /// Variables
    /// (aka. Peaks, Columns, Dependent variables, Uncontrolled variables, Y, etc.)
    /// 
    /// The independed variables are in the classes: ConditionInfo, GroupInfo, ObservationInfo
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class Peak : Associational
    {                                                  
        public const string ID_COLUMN_CLUSTERCOMBINATION = "Clusters\\Combination (for colours)";

        /// <summary>
        /// The ID (name) of the peak.
        /// </summary>
        [XColumn]
        public readonly string Id;        

        /// <summary>
        /// Comment flags.
        /// </summary>
        public List<PeakFlag> CommentFlags = new List<PeakFlag>();

        /// <summary>
        /// M/Z
        /// </summary>
        [XColumn("m/z", EColumn.Visible)]
        public readonly decimal Mz;

        /// <summary>
        /// Retention time
        /// </summary>
        [XColumn( "tʀ", EColumn.Visible )]
        public readonly decimal Rt;

        /// <summary>
        /// Other information the user loaded and may want to view in but the program doesn't actually need.
        /// </summary>
        public readonly MetaInfoCollection MetaInfo = new MetaInfoCollection();

        /// <summary>
        /// Potential compounds.
        /// </summary>
        [XColumn]
        public readonly List<Annotation> Annotations = new List<Annotation>();

        /// <summary>
        /// Similar peaks.
        /// </summary>
        [XColumn]
        public readonly List<Peak> SimilarPeaks = new List<Peak>();

        /// <summary>
        /// LC-MS Mode
        /// </summary>
        [XColumn]
        public readonly ELcmsMode LcmsMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Peak(string id, ELcmsMode lcmsmode, decimal mz, decimal rt)
        {                        
            this.Id = id;                               
            this.Mz = mz;
            this.Rt = rt;
            this.LcmsMode = lcmsmode;
        }

        public override EPrevent SupportsHide => EPrevent.Hide;

        /// <summary>     
        /// Default display name.
        /// </summary>
        public override string DefaultDisplayName=> this.Id;       

        /// <summary>
        /// Toggles a comment flag on and off.
        /// </summary>
        /// <param name="f">The flag to toggle</param>
        /// <returns>The new status of the flag</returns>
        public bool ToggleCommentFlag(PeakFlag f)
        {
            if (!this.CommentFlags.Contains(f))
            {
                this.CommentFlags.Add(f);
                return true;
            }
            else
            {
                this.CommentFlags.Remove(f);
                return false;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        protected override void OnFindAssociations(ContentsRequest request)
        {
            switch (request.Type)
            {            
                case EVisualClass.Peak:
                    request.Text = "Peaks with similar m/z to {0} (" + Maths.SignificantDigits((double)this.Mz, 5) + ")";
                    request.AddRange(this.SimilarPeaks);
                    break;

                case EVisualClass.Cluster:
                    request.Text = "Assigned clusters for {0}";
                    Assignment.AddHeaders(request);

                    foreach (var ass in this.FindAssignments(request.Core ))
                    {
                        request.Add(ass.Cluster, ass.GetHeaders());
                    }

                    break;

                case EVisualClass.Assignment:
                    request.Text = "Assignments for {0}";
                    request.AddRange( this.FindAssignments( request.Core ) );
                    break;


                case EVisualClass.Compound:
                    request.Text = "Potential compounds of {0}";

                    foreach (var annotation in this.Annotations)
                    {
                        request.Add(annotation.Compound);
                    }

                    break;

                case EVisualClass.Annotation:
                    request.Text = "Annotations for {0}";

                    foreach (var annotation in this.Annotations)
                    {
                        request.Add(annotation);
                    }

                    break;

                case EVisualClass.Adduct:
                    {
                        request.Text = "Adducts of potential compounds for {0}";

                        HashSet<Adduct> counter = new HashSet<Adduct>();

                        foreach (var c in this.Annotations)
                        {
                            counter.Add(c.Adduct);
                        }

                        foreach (var kvp in counter)
                        {
                            request.Add(kvp);
                        }
                    }
                    break;

                case EVisualClass.Pathway:
                    {
                        request.Text = "Pathways of potential compounds for {0}";
                        request.AddExtraColumn("Compounds", "Compounds in this pathway potentially represented by {0}.");

                        Dictionary<Pathway, List<Compound>> counter = new Dictionary<Pathway, List<Compound>>();

                        foreach (var pc in this.Annotations)
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
        public override EVisualClass AssociationalClass
        {
            get { return EVisualClass.Peak; }
        }

        /// <summary>
        /// Gets the statistic by the name of "x", otherwise NaN.
        /// </summary>
        internal double GetStatistic(ConfigurationStatistic x)
        {
            double v;

            if (x.Results.Results.TryGetValue(this, out v))
            {
                return v;
            }

            return double.NaN;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        public override void GetXColumns(ColumnCollection list, Core core)
        {
            var columns = list .Cast< Peak>();
                                                           

            columns.Add("Cluster assignments", EColumn.None, λ => λ.FindAssignments( core ).Select(z=> z.Cluster ));
            columns.Add(ID_COLUMN_CLUSTERCOMBINATION, EColumn.Advanced, z => StringHelper.ArrayToString(z.FindAssignments( core ).Select(zz=> zz.Cluster )));
            columns.Add("Cluster assignments\\As scores", EColumn.Advanced, λ => λ.FindAssignments( core ).Select(z=> z.Score ));

            columns.Add("Cluster assignments\\Unique", EColumn.Advanced, λ => new HashSet<Cluster>(λ.FindAssignments( core ).Select(z=> z.Cluster )).ToArray());
            columns.Add("Cluster assignments\\Grouped", EColumn.Advanced, λ => StringHelper.ArrayToString(λ.FindAssignments( core ).OrderBy(z => z.Vector.Group?.DisplayPriority).Select(z => (z.Vector.Group != null ? (z.Vector.Group.DisplayShortName + "=") : "") + z.Cluster.ShortName)));

            foreach (GroupInfo group in core.Groups)
            {
                var closure = group;
                columns.Add("Clusters assignments\\For " + group.DisplayName, EColumn.Advanced, λ => λ.FindAssignments( core ).Where(z => z.Vector.Group == closure).Select(z => z.Cluster).ToArray(), z => closure.Colour);
                columns.Add("Clusters assignments\\For " + group.DisplayName + " (scores)", EColumn.Advanced, λ => λ.FindAssignments( core ).Where(z => z.Vector.Group == closure).Select(z => z.Score).ToArray(), z => closure.Colour);
            }

            foreach (PeakFlag flag in core.Options.PeakFlags)
            {
                var closure = flag;
                columns.Add("Flags\\" + flag, EColumn.Advanced, λ => λ.CommentFlags.Contains(closure) ? closure.DisplayName : string.Empty, z => closure.Colour);
            }

            columns.Add("Cluster assignments\\For no group", EColumn.Advanced, λ => λ.FindAssignments( core ).Where(z => z.Vector.Group == null).Select(z => z.Cluster).ToList());
            columns.Add("Cluster assignments\\For no group (scores)", EColumn.Advanced, λ => λ.FindAssignments( core ).Where(z => z.Vector.Group == null).Select(z => z.Score).ToList());

            columns.Add("Flags\\All", EColumn.None, λ => StringHelper.ArrayToString(λ.CommentFlags), z=> z.CommentFlags.Count == 1 ? z.CommentFlags[0].Colour : Color.Black );
                                   
            foreach (ConfigurationStatistic stat in core.Statistics.WhereEnabled())
            {
                var closure = stat;
                columns.Add(
                    "Statistic\\" + stat.ToString(),
                    EColumn.IsStatistic,
                    λ => λ.GetStatistic(closure),
                    z => UiControls.StatisticColour( z.GetStatistic( closure ), stat.Results.Min, stat.Results.Max ) );   
            }

            columns.Add( "Annotations\\As compounds", EColumn.Advanced, λ => λ.Annotations.Select(λλ => λλ.Compound));
            columns.Add( "Annotations\\As adducts", EColumn.Advanced, λ => λ.Annotations.Select(λλ => λλ.Adduct));
            columns.Add( "Annotations\\As statuses", EColumn.Advanced, λ => λ.Annotations.Select( λλ => λλ.Status ) );

            core._peakMeta.ReadAllColumns(z => z.MetaInfo, columns);

            foreach (PeakFilter fi in core.PeakFilters)
            {
                var closure = fi;
                columns.Add("Passes filter\\" + fi.ToString(), EColumn.Advanced, z => fi.Test(z));
            }                        
        }

        [XColumn("Best annotation")]
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
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        public override Image Icon
        {
            get
            {
                // IMAGE
                if (this.Annotations.Count == 0)
                {
                    return Resources.ListIconPeak;
                }
                else
                {
                    switch (this.Annotations.Max( z => z.Status ))
                    {
                        case EAnnotation.Tentative:
                            return Resources.ListIconPeakTentative;

                        case EAnnotation.Affirmative:
                            return Resources.ListIconPeakAffirmed;

                        case EAnnotation.Confirmed:
                            return Resources.ListIconPeakConfirmed;

                        default:
                            throw new SwitchException();
                    }
                }
            }
        }

        public IEnumerable<Assignment> FindAssignments( Core core )
        {
            foreach (Cluster clu in core.Clusters)
            {
                foreach (Assignment ass in clu.Assignments.List.Where( z => z.Peak == this ))
                {
                    yield return ass;
                }
            }
        }
    }
}
