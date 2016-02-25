using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Algorithms;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Forms.Algorithms.ClusterEvaluation;

namespace MetaboliteLevels.Forms.Generic
{
    /// <summary>
    /// Helper method for creating common ListValueSet(of T)s
    /// </summary>
    static class ListValueSet
    {
        /// <summary>
        /// Enum flags
        /// </summary>
        public static ListValueSet<T> ForFlagsEnum<T>(string title)
            where T : struct, IComparable, IFormattable, IConvertible // aka. Enum
        {
            return new ListValueSet<T>()
            {
                Title = title,
                List = EnumHelper.GetEnumFlags<T>(),
                Namer = z => EnumHelper.ToUiString((Enum)(object)z),
                Describer = z => EnumHelper.ToDescription((Enum)(object)z),
                Comparator = _EnumComparator<T>,
            };
        }

        /// <summary>
        /// Strings
        /// </summary>
        public static ListValueSet<int> ForString(string title, params string[] options)
        {
            return new ListValueSet<int>()
            {
                Title = title,
                List = options.Indices(),
                Namer = z => options[z]
            };
        }

        /// <summary>
        /// Enum without flags
        /// </summary>
        public static ListValueSet<T> ForDiscreteEnum<T>(string title, T cancelValue)
            where T : struct, IComparable, IFormattable, IConvertible // aka. Enum
        {
            return new ListValueSet<T>()
            {
                Title = title,
                List = Enum.GetValues(typeof(T)).Cast<T>().Except(new T[] { cancelValue }),
                Namer = z => EnumHelper.ToUiString((Enum)(object)z),
                Describer = z => EnumHelper.ToDescription((Enum)(object)z),
                Comparator = _EnumComparator<T>,
                CancelValue = cancelValue,
            };
        }

        /// <summary>
        /// Creates a ConditionBox.
        /// </summary>
        public static ListValueSet<ObservationInfo> ForObservations(Core core, bool onlyEnabled)
        {
            return new ListValueSet<ObservationInfo>()
            {
                Title = "Observations",
                List = core.Observations.WhereEnabled(onlyEnabled),
                Describer = z => "Group = " + z.Group.DisplayName + ", Time = " + z.Time + ", Replicate = " + z.Rep + "\r\nBatch = " + z.Batch + ", Acquisition = " + z.Acquisition
            };
        }

        /// <summary>
        /// Creates a ConditionBox.
        /// </summary>
        public static ListValueSet<ConditionInfo> ForConditions(Core core, bool onlyEnabled)
        {
            return new ListValueSet<ConditionInfo>()
            {
                Title = "Conditions",
                List = core.Conditions.WhereEnabled(onlyEnabled),
                Describer = z => "Group = " + z.Group.DisplayName + ", Time = " + z.Time
            };
        }

        /// <summary>
        /// Creates a ConditionBox.
        /// </summary>
        public static ListValueSet<Column> ForColumns(IEnumerable<Column> columns)
        {
            return new ListValueSet<Column>()
            {
                Title = "Columns",
                List = columns.Where(z => !z.IsAlwaysEmpty),
                Namer = z => z.Id,
                Describer = z => z.OverrideDisplayName,
            };
        }

        /// <summary>
        /// Tests
        /// </summary>             
        public static ListValueSet<ClusterEvaluationPointer> ForTests(Core core, bool onlyEnabled)
        {
            return new ListValueSet<ClusterEvaluationPointer>()
            {
                Title = "Test Results",
                List = core.EvaluationResultFiles.WhereEnabled(onlyEnabled),
                Namer = z => z.DisplayName,
                Describer = z => "- CLUSTERER: " + z.Configuration.ParameterConfigAsString + "\r\n- VALUES: " + z.Configuration.ParameterValuesAsString + (z.FileName != null ? ("\r\n- FILENAME: " + z.FileName) : ""),
                IconProvider = _GetIcon,
                ItemEditor = (x, y, z) => FrmEvaluateClusteringOptions.Show(x, core, y, z),
                ListApplier = z => core.EvaluationResultFiles.ReplaceAll(z)
            };
        }

        /// <summary>
        /// Acquisitions
        /// </summary>
        public static ListValueSet<int> ForAcquisitions(Core core)
        {
            return new ListValueSet<int>()
            {
                Title = "Time Points",
                List = core.Acquisitions,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
            };
        }

        /// <summary>
        /// Helper function for [ForTests]
        /// </summary>         
        private static UiControls.ImageListOrder _GetIcon(ClusterEvaluationPointer input)
        {
            if (input.HasResults)
            {
                return UiControls.ImageListOrder.TestFull;
            }
            else
            {
                return UiControls.ImageListOrder.TestEmpty;
            }
        }

        /// <summary>
        /// Batches.
        /// </summary>
        public static ListValueSet<BatchInfo> ForBatches(Core core, bool onlyEnabled)
        {
            return new ListValueSet<BatchInfo>()
            {
                Title = "Batches",
                List = core.Batches.WhereEnabled(onlyEnabled),
                Namer = z => z.Id.ToString(),
                Describer = z => z.DisplayShortName + ": " + z.DisplayName + z.Comment.FormatIf("\r\nComment: ")
            };
        }

        /// <summary>
        /// Times
        /// </summary>
        public static ListValueSet<int> ForTimes(Core core)
        {
            return new ListValueSet<int>()
            {
                Title = "Time Points",
                List = core.Times,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
            };
        }

        /// <summary>
        /// Replicates
        /// </summary>
        public static ListValueSet<int> ForReplicates(Core core)
        {
            return new ListValueSet<int>()
            {
                Title = "Replicates",
                List = core.Reps,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
            };
        }

        /// <summary>
        /// Meta headers
        /// </summary>
        internal static ListValueSet<string> ForMetaHeaders(MetaInfoHeader headerCollection)
        {
            return new ListValueSet<string>()
            {
                Title = "Headers",
                List = headerCollection.Headers,
            };
        }

        /// <summary>
        /// Parameters
        /// </summary>
        internal static ListValueSet<int> ForParameters(AlgoParameter[] parameters, int selectedIndex, string message = null)
        {
            return new ListValueSet<int>()
            {
                Title = "Parameters",
                List = parameters.Indices(),
                Namer = z => parameters[z].Name + " (" + parameters[z].Type.ToUiString() + ")",
                Describer = z => "Parameter " + z.ToString(),
                CancelValue = int.MinValue,
            };
        }

        /// <summary>
        /// Clusters
        /// </summary>
        internal static ListValueSet<Cluster> ForClusters(Core core, bool onlyEnabled)
        {
            return new ListValueSet<Cluster>()
            {
                Title = "Clusters",
                List = core.Clusters.WhereEnabled(onlyEnabled),
                Namer = z => z.DisplayName,
                Describer = _GetComment
            };
        }

        /// <summary>
        /// Peaks
        /// </summary>
        internal static ListValueSet<Peak> ForPeaks(Core core, bool onlyEnabled)
        {
            return new ListValueSet<Peak>()
            {
                Title = "Peaks",
                List = core.Peaks.WhereEnabled(onlyEnabled),
                Namer = z => z.DisplayName,
                Describer = _GetComment
            };
        }

        /// <summary>
        /// All IVisualisables in core
        /// </summary>
        internal static ListValueSet<IVisualisable> ForEverything(Core core)
        {
            var all = core.Peaks.Cast<IVisualisable>().Concat(core.Clusters).Concat(core.Compounds).Concat(core.Adducts).Concat(core.Pathways).Concat(core.Assignments);

            return new ListValueSet<IVisualisable>()
            {
                Title = "All Items",
                List = all,
                Namer = z => z.VisualClass.ToUiString() + ": " + z.DisplayName,
                Describer = _GetComment
            };
        }

        /// <summary>
        /// Clusterers
        /// </summary>
        internal static ListValueSet<ConfigurationClusterer> ForClusterers(Core core, bool onlyEnabled)
        {
            return new ListValueSet<ConfigurationClusterer>()
            {
                Title = "Clusterers",
                List = core.AllClusterers.WhereEnabled(onlyEnabled),
                Describer = _GetComment,
                ListEditorObsolete = f => FrmBigList.ShowAlgorithms(f, core, FrmBigList.EAlgorithmType.Clusters, null)
            };
        }

        /// <summary>
        /// Statistics
        /// </summary>
        internal static ListValueSet<ConfigurationStatistic> ForStatistics(Core core, bool onlyEnabled)
        {
            return new ListValueSet<ConfigurationStatistic>()
            {
                Title = "Statistics",
                List = core.AllStatistics.WhereEnabled(onlyEnabled),
                Describer = _GetComment,
                ListEditorObsolete = f => FrmBigList.ShowAlgorithms(f, core, FrmBigList.EAlgorithmType.Statistics, null)
            };
        }

        /// <summary>
        /// Peak flags
        /// </summary>
        internal static ListValueSet<PeakFlag> ForPeakFlags(Core core)
        {
            return new ListValueSet<PeakFlag>()
            {
                Title = "Peak Flags",
                List = core.Options.PeakFlags,
                Describer = z => z.Comments.FormatIf("\r\nComments: "),
                ListEditorObsolete = f => FrmOptions.Show(f, core)
            };
        }

        /// <summary>
        /// Peak filters
        /// </summary>
        internal static ListValueSet<PeakFilter> ForPeakFilter(Core core, bool onlyEnabled)
        {
            return new ListValueSet<PeakFilter>()
            {
                Title = "Peak Filters",
                List = core.AllPeakFilters.WhereEnabled(onlyEnabled),
                Describer = z => z.ParamsAsString() + z.Comment.FormatIf("\r\nComments: "),
                ListApplier = z => core.SetPeakFilters(z),
                ItemEditor = (o, d, r) =>
                {
                    var nnew = ListValueSet.ForPeakFilterConditions(core, d, false).ShowBigList(o, core, r ? EViewMode.ReadAndComment : EViewMode.Write);

                    if (nnew == null)
                    {
                        return null;
                    }

                    return new PeakFilter(d?.OverrideDisplayName, d?.Comment, nnew);
                },
            };
        }

        /// <summary>
        /// Observation filter conditions
        /// </summary>
        internal static ListValueSet<ObsFilter.Condition> ForObsFilterConditions(Core core, ObsFilter of, bool onlyEnabled)
        {
            return new ListValueSet<ObsFilter.Condition>()
            {
                Title = "Observation filter conditions",
                List = of.Conditions.Cast<ObsFilter.Condition>().WhereEnabled(onlyEnabled),
                ItemEditor = (o, d, r) => FrmObservationFilterCondition.Show(o, core, d, r)
            };
        }

        /// <summary>
        /// Peak filter conditions
        /// </summary>
        internal static ListValueSet<PeakFilter.Condition> ForPeakFilterConditions(Core core, PeakFilter of, bool onlyEnabled)
        {
            return new ListValueSet<PeakFilter.Condition>()
            {
                Title = "Peak filter conditions",
                List = of.Conditions.Cast<PeakFilter.Condition>().WhereEnabled(onlyEnabled),
                ItemEditor = (o, d, r) => FrmPeakFilterCondition.Show(o, core, d, r)
            };
        }

        /// <summary>
        /// Observation filters
        /// </summary>
        internal static ListValueSet<ObsFilter> ForObsFilter(Core core, bool onlyEnabled)
        {
            return new ListValueSet<ObsFilter>()
            {
                Title = "Observation Filters",
                List = core.AllObsFilters.WhereEnabled(onlyEnabled),
                Describer = z => z.ParamsAsString() + z.Comment.FormatIf("\r\nComments: "),
                ItemEditor = (o, d, r) =>
                {
                    var nnew = ListValueSet.ForObsFilterConditions(core, d, false).ShowBigList(o, core, r ? EViewMode.ReadAndComment : EViewMode.Write);

                    if (nnew == null)
                    {
                        return null;
                    }

                    return new ObsFilter(d?.OverrideDisplayName, d?.Comment, nnew);
                },
                ListApplier = z => core.SetObsFilters(z)
            };
        }

        /// <summary>
        /// Experimental groups
        /// </summary>
        internal static ListValueSet<GroupInfo> ForGroups(Core core, bool onlyEnabled)
        {
            return new ListValueSet<GroupInfo>()
            {
                Title = "Experimental Groups",
                List = core.Groups.WhereEnabled(onlyEnabled).OrderBy(z => z.Id),
                Namer = z => z.DisplayName,
                Describer = z => z.DisplayShortName + ": " + z.DisplayName,
                Comparator = _TypeNameComparator,
                ItemEditor = (o, d, r) => { return FrmEditExpGroup.Show(o, d, r) ? d : null; }
            };
        }

        /// <summary>
        /// Helper method: Gets comment
        /// </summary>
        private static string _GetComment(IVisualisable vis)
        {
            if (!string.IsNullOrEmpty(vis.Comment))
            {
                return "Comments: " + vis.Comment;
            }

            return null;
        }

        /// <summary>
        /// Helper method: Gets comment
        /// </summary>
        private static string _GetComment(ConfigurationBase z)
        {
            return z.Description + z.Comment.FormatIf("\r\nComments: ");
        }

        /// <summary>
        /// Helper method: Compares enums
        /// </summary>
        private static bool _EnumComparator<T>(string name, T value)
        {
            string a = ((Enum)(object)value).ToUiString().ToUpper();
            string b = value.ToString().ToUpper();
            string c = ((int)(object)value).ToString().ToUpper();

            name = name.ToUpper();

            return a == name || b == name || c == name;
        }

        /// <summary>
        /// Helper method: Compares type names
        /// </summary>
        private static bool _TypeNameComparator(string name, GroupInfo group)
        {
            name = name.ToUpper();

            return group.DisplayName.ToUpper() == name || group.DisplayShortName.ToUpper() == name || group.Id.ToString() == name;
        }
    }
}
