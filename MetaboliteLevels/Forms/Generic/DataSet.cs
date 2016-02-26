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
using MetaboliteLevels.Properties;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics.Metrics;
using MetaboliteLevels.Algorithms.Statistics.Clusterers;
using MetaboliteLevels.Algorithms.Statistics.Trends;
using MetaboliteLevels.Algorithms.Statistics.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Corrections;
using System.IO;

namespace MetaboliteLevels.Forms.Generic
{
    /// <summary>
    /// This is a helper class, see DataSet(of T) for the main description.
    /// </summary>
    static class DataSet
    {
        /// <summary>
        /// Enum flags
        /// </summary>
        public static DataSet<T> ForFlagsEnum<T>(string title)
            where T : struct, IComparable, IFormattable, IConvertible // aka. Enum
        {
            return new DataSet<T>()
            {
                Title = title,
                List = EnumHelper.GetEnumFlags<T>(),
                ItemNameProvider = z => EnumHelper.ToUiString((Enum)(object)z),
                ItemDescriptionProvider = z => EnumHelper.ToDescription((Enum)(object)z),
                StringComparator = _EnumComparator<T>,
            };
        }

        /// <summary>
        /// Strings
        /// </summary>
        public static DataSet<int> ForString(string title, params string[] options)
        {
            return new DataSet<int>()
            {
                Title = title,
                List = options.Indices(),
                ItemNameProvider = z => options[z]
            };
        }

        /// <summary>
        /// Enum without flags
        /// </summary>
        public static DataSet<T> ForDiscreteEnum<T>(string title, T cancelValue)
            where T : struct, IComparable, IFormattable, IConvertible // aka. Enum
        {
            return new DataSet<T>()
            {
                Title = title,
                List = Enum.GetValues(typeof(T)).Cast<T>().Except(new T[] { cancelValue }),
                ItemNameProvider = z => EnumHelper.ToUiString((Enum)(object)z),
                ItemDescriptionProvider = z => EnumHelper.ToDescription((Enum)(object)z),
                StringComparator = _EnumComparator<T>,
                CancelValue = cancelValue,
            };
        }

        /// <summary>
        /// Creates a ConditionBox.
        /// </summary>
        public static DataSet<ObservationInfo> ForObservations(Core core)
        {
            return new DataSet<ObservationInfo>()
            {
                Core = core,
                Title = "Observations",
                List = core.Observations,
                ItemDescriptionProvider = z => "Group = " + z.Group.DisplayName + ", Time = " + z.Time + ", Replicate = " + z.Rep + "\r\nBatch = " + z.Batch + ", Acquisition = " + z.Acquisition
            };
        }

        /// <summary>
        /// Creates a ConditionBox.
        /// </summary>
        public static DataSet<ConditionInfo> ForConditions(Core core)
        {
            return new DataSet<ConditionInfo>()
            {
                Core = core,
                Title = "Conditions",
                List = core.Conditions,
                ItemDescriptionProvider = z => "Group = " + z.Group.DisplayName + ", Time = " + z.Time,
            };
        }

        /// <summary>
        /// Creates a ConditionBox.
        /// </summary>
        public static DataSet<Column> ForColumns(IEnumerable<Column> columns)
        {
            return new DataSet<Column>()
            {
                Title = "Columns",
                List = columns.Where(z => !z.IsAlwaysEmpty),
                ItemNameProvider = z => z.Id,
                ItemDescriptionProvider = z => z.OverrideDisplayName,
            };
        }

        /// <summary>
        /// Tests
        /// </summary>             
        public static DataSet<ClusterEvaluationPointer> ForTests(Core core)
        {
            return new DataSet<ClusterEvaluationPointer>()
            {
                Core = core,
                Title = "Test Results",
                List = core.EvaluationResultFiles,
                ItemNameProvider = z => z.DisplayName,
                ItemDescriptionProvider = z => "- CLUSTERER: " + z.Configuration.ParameterConfigAsString + "\r\n- VALUES: " + z.Configuration.ParameterValuesAsString + (z.FileName != null ? ("\r\n- FILENAME: " + z.FileName) : ""),
                ItemIconProvider = _GetIcon,
                ItemEditor = z => FrmEvaluateClusteringOptions.Show(z.Owner, core, z.DefaultValue, z.ReadOnly),
                ListChangeApplicator = z => core.EvaluationResultFiles.ReplaceAll(z.List),
                ListSupportsReorder = true,
            };
        }

        /// <summary>
        /// Acquisitions
        /// </summary>
        public static DataSet<int> ForAcquisitions(Core core)
        {
            return new DataSet<int>()
            {
                Core = core,
                Title = "Time Points",
                List = core.Acquisitions,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
            };
        }

        /// <summary>
        /// Batches.
        /// </summary>
        public static DataSet<BatchInfo> ForBatches(Core core)
        {
            return new DataSet<BatchInfo>()
            {
                Core = core,
                Title = "Batches",
                List = core.Batches,
                ItemNameProvider = z => z.Id.ToString(),
                ItemDescriptionProvider = z => z.DisplayShortName + ": " + z.DisplayName + z.Comment.FormatIf("\r\nComment: ")
            };
        }

        /// <summary>
        /// Times
        /// </summary>
        public static DataSet<int> ForTimes(Core core)
        {
            return new DataSet<int>()
            {
                Core = core,
                Title = "Time Points",
                List = core.Times,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
            };
        }

        /// <summary>
        /// Replicates
        /// </summary>
        public static DataSet<int> ForReplicates(Core core)
        {
            return new DataSet<int>()
            {
                Core = core,
                Title = "Replicates",
                List = core.Reps,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
            };
        }

        /// <summary>
        /// Meta headers
        /// </summary>
        internal static DataSet<string> ForMetaHeaders(MetaInfoHeader headerCollection)
        {
            return new DataSet<string>()
            {
                Title = "Headers",
                List = headerCollection.Headers,
            };
        }

        /// <summary>
        /// Parameters
        /// </summary>
        internal static DataSet<int> ForParameters(Core core, AlgoParameter[] parameters, int selectedIndex, string message = null)
        {
            return new DataSet<int>()
            {
                Core = core,
                Title = "Parameters",
                List = parameters.Indices(),
                ItemNameProvider = z => parameters[z].Name + " (" + parameters[z].Type.ToUiString() + ")",
                ItemDescriptionProvider = z => "Parameter " + z.ToString(),
                CancelValue = int.MinValue,
            };
        }

        /// <summary>
        /// Clusters
        /// </summary>
        internal static DataSet<Cluster> ForClusters(Core core)
        {
            return new DataSet<Cluster>()
            {
                Core = core,
                Title = "Clusters",
                List = core.Clusters,
                ItemNameProvider = z => z.DisplayName,
                ItemDescriptionProvider = _GetComment
            };
        }

        /// <summary>
        /// Peaks
        /// </summary>
        internal static DataSet<Peak> ForPeaks(Core core)
        {
            return new DataSet<Peak>()
            {
                Core = core,
                Title = "Peaks",
                List = core.Peaks,
                ItemNameProvider = z => z.DisplayName,
                ItemDescriptionProvider = _GetComment
            };
        }

        /// <summary>
        /// All IVisualisables in core
        /// </summary>
        internal static DataSet<object> ForEverything(Core core)
        {
            var all = core.Peaks.Cast<object>()
                        .Concat(core.Clusters)
                        .Concat(core.Compounds)
                        .Concat(core.Adducts)
                        .Concat(core.Pathways)
                        .Concat(core.Assignments)
                        .Concat(core.AllClusterers)
                        .Concat(core.AllCorrections)
                        .Concat(core.AllObsFilters)
                        .Concat(core.AllPeakFilters)
                        .Concat(core.AllStatistics)
                        .Concat(core.AllTrends)
                        .Concat(core.Annotations)
                        .Concat(core.Batches)
                        .Concat(core.Conditions)
                        .Concat(core.EvaluationResultFiles)
                        .Concat(core.Groups)
                        .Concat(core.Observations)
                        .Concat(core.Reps.Cast<object>())
                        .Concat(core.Times.Cast<object>())
                        .Concat(Algo.Instance.All);

            return new DataSet<object>()
            {
                Core = core,
                Title = "All items",
                List = all,
                ItemNameProvider = z => z.GetType().Name.ToSmallCaps() + ": " + z.ToString(),
                ItemDescriptionProvider = z => (z is IVisualisable) ? "Selectable" : "Not selectable"
            };
        }

        /// <summary>
        /// Trends
        /// </summary>
        internal static DataSet<ConfigurationTrend> ForTrends(Core core)
        {
            return new DataSet<ConfigurationTrend>()
            {
                Core = core,
                Title = "Trends",
                List = core.AllTrends,
                ItemDescriptionProvider = _GetComment,
                ListSupportsReorder = true,
                BeforeListChangesApplied = z =>
                {
                    int numEnabledX = z.List.Count(zz => zz.Enabled);

                    if (numEnabledX == 0)
                    {
                        FrmMsgBox.ShowError(z.Owner, "A trendline must be defined.");
                        return false;
                    }
                    else if (numEnabledX > 1)
                    {
                        FrmMsgBox.ShowError(z.Owner, "Only one trend can be activated at once.");
                        return false;
                    }

                    z.Status = FrmEditUpdate.ShowTrendsChanged(z.Owner);
                    return true;
                },
                ListChangeApplicator = z =>
                {
                    bool updateStats = ((FrmEditUpdate.EChangeLevel)z.Status).HasFlag(FrmEditUpdate.EChangeLevel.Statistic);
                    bool updateClusters = ((FrmEditUpdate.EChangeLevel)z.Status).HasFlag(FrmEditUpdate.EChangeLevel.Cluster);
                    core.SetTrends(z.List, false, true, true, z.Progress);
                },
                ItemEditor = z =>
                    {
                        if (!_ShowEditPreamble(z.Owner, z.DefaultValue))
                        {
                            return null;
                        }

                        return FrmAlgoTrend.Show(z.Owner, core, z.DefaultValue, z.ReadOnly);
                    },
                AfterListChangesApplied = z => FrmMsgBox.ShowCompleted(z.owner, "Trends", FrmEditUpdate.GetUpdateMessage((FrmEditUpdate.EChangeLevel)z.Status)),
            };
        }

        /// <summary>
        /// ConfigurationCorrection
        /// </summary> 
        public static DataSet<ConfigurationCorrection> ForCorrections(Core core)
        {
            return new DataSet<ConfigurationCorrection>()
            {
                Core = core,
                Title = "Corrections",
                List = core.AllCorrections,
                ItemDescriptionProvider = _GetComment,
                ListSupportsReorder = true,
                BeforeListChangesApplied = z =>
                {
                    z.Status = FrmEditUpdate.ShowCorrectionsChanged(z.Owner);
                    return true;
                },
                AfterListChangesApplied = z =>
                {
                    FrmMsgBox.ShowCompleted(z.owner, "Data Corrections", FrmEditUpdate.GetUpdateMessage((FrmEditUpdate.EChangeLevel)z.Status));
                },
                ListChangeApplicator = z =>
                {
                    bool updateStats = ((FrmEditUpdate.EChangeLevel)z.Status).HasFlag(FrmEditUpdate.EChangeLevel.Statistic);
                    bool updateTrends = ((FrmEditUpdate.EChangeLevel)z.Status).HasFlag(FrmEditUpdate.EChangeLevel.Trend);
                    bool updateClusters = ((FrmEditUpdate.EChangeLevel)z.Status).HasFlag(FrmEditUpdate.EChangeLevel.Cluster);

                    core.SetCorrections(z.List, false, updateStats, updateTrends, updateClusters, z.Progress);
                },
                ItemEditor = z =>
                {
                    if (!_ShowEditPreamble(z.Owner, z.DefaultValue))
                    {
                        return null;
                    }

                    return FrmAlgoCorrection.Show(z.Owner, core, z.DefaultValue, z.ReadOnly);
                }
            };
        }

        /// <summary>
        /// ConfigurationClusterer
        /// </summary>
        internal static DataSet<ConfigurationClusterer> ForClusterers(Core core)
        {
            return new DataSet<ConfigurationClusterer>()
            {
                Core = core,
                Title = "Clusterers",
                List = core.AllClusterers,
                ItemDescriptionProvider = _GetComment,
                ListSupportsReorder = true,
                ListChangeApplicator = z => core.SetClusterers(z.List, false, z.Progress),
                ItemEditor = z =>
                {
                    if (!_ShowEditPreamble(z.Owner, z.DefaultValue))
                    {
                        return null;
                    }

                    return FrmAlgoCluster.Show(z.Owner, core, z.DefaultValue, z.ReadOnly, false);
                },

                AfterListChangesApplied = z => FrmMsgBox.ShowCompleted(z.owner, "Clustering", FrmEditUpdate.GetUpdateMessage(FrmEditUpdate.EChangeLevel.Cluster))
            };
        }

        /// <summary>
        /// Metrics
        /// </summary>            
        internal static DataSet<MetricBase> ForMetricAlgorithms(Core core)
        {
            return new DataSet<MetricBase>()
            {
                Core = core,
                Title = "Distance metrics",
                List = Algo.Instance.Metrics,
                ItemEditor = z => _ShowScriptEditor<MetricBase>(z, "Metric", MetricScript.INPUTS, UiControls.EInitialFolder.FOLDER_METRICS),
                ListChangesOnEdit = true,
                BeforeItemChanged = _ScriptReplace,
            };
        }

        /// <summary>
        /// TrendBase
        /// </summary>            
        internal static DataSet<TrendBase> ForTrendAlgorithms(Core core)
        {
            return new DataSet<TrendBase>()
            {
                Core = core,
                Title = "Trend algorithms",
                List = Algo.Instance.Trends,
                ItemEditor = z => _ShowScriptEditor(z, "Trend algorithm", TrendScript.INPUTS, UiControls.EInitialFolder.FOLDER_TRENDS),
                ListChangesOnEdit = true,
                BeforeItemChanged = _ScriptReplace,
            };
        }

        /// <summary>
        /// ClustererBase
        /// </summary>            
        internal static DataSet<ClustererBase> ForClustererAlgorithms(Core core)
        {
            return new DataSet<ClustererBase>()
            {
                Core = core,
                Title = "Clustering algorithms",
                List = Algo.Instance.Clusterers,
                ItemEditor = z => _ShowScriptEditor(z, "Clustering algorithm", ClustererScript.INPUTS, UiControls.EInitialFolder.FOLDER_CLUSTERERS),
                ListChangesOnEdit = true,
                BeforeItemChanged = _ScriptReplace,
            };
        }

        /// <summary>
        /// Trends and corrections
        /// </summary>            
        internal static DataSet<AlgoBase> ForTrendAndCorrectionAlgorithms(Core core)
        {
            return new DataSet<AlgoBase>()
            {
                Core = core,
                Title = "Trend algorithms",
                List = Algo.Instance.Trends.Cast<AlgoBase>().Concat(Algo.Instance.Corrections),
                ItemEditor = z =>
                {
                    if (z.DefaultValue is TrendBase)
                    {
                        return _ShowScriptEditor(z, "Trend", TrendScript.INPUTS, UiControls.EInitialFolder.FOLDER_TRENDS);
                    }
                    else if (z.DefaultValue is CorrectionBase)
                    {
                        return _ShowScriptEditor(z, "Correction", CorrectionScript.INPUTS, UiControls.EInitialFolder.FOLDER_CORRECTIONS);
                    }

                    switch (FrmMsgBox.Show(z.Owner, "Statistics", null, "Create a new trend or correction?", Resources.MsgHelp, new[]
                    {
                        new FrmMsgBox.ButtonSet("Trend", Resources.ObjLScriptStatistic, DialogResult.Yes),
                        new FrmMsgBox.ButtonSet("Correction", Resources.ObjLScriptStatistic, DialogResult.No),
                        new FrmMsgBox.ButtonSet("Cancel", Resources.MnuCancel, DialogResult.Cancel)
                    }))
                    {
                        case DialogResult.Yes:
                            return _ShowScriptEditor(z, "Trend", TrendScript.INPUTS, UiControls.EInitialFolder.FOLDER_TRENDS);

                        case DialogResult.No:
                            return _ShowScriptEditor(z, "Correction", CorrectionScript.INPUTS, UiControls.EInitialFolder.FOLDER_CORRECTIONS);

                        default:
                            return null;
                    }
                },
                ListChangesOnEdit = true,
                BeforeItemChanged = _ScriptReplace,
            };
        }

        /// <summary>
        /// StatisticBase
        /// </summary>                  
        internal static DataSet<StatisticBase> ForStatisticsAlgorithms(Core core)
        {
            return new DataSet<StatisticBase>()
            {
                Core = core,
                Title = "Statistics",
                List = Algo.Instance.Statistics,
                ItemEditor = z =>
                {
                    if (z.DefaultValue is StatisticBase)
                    {
                        return _ShowScriptEditor(z, "Statistic", StatisticScript.INPUTS, UiControls.EInitialFolder.FOLDER_STATISTICS);
                    }
                    else if (z.DefaultValue is MetricBase)
                    {
                        return _ShowScriptEditor(z, "Metric", MetricScript.INPUTS, UiControls.EInitialFolder.FOLDER_METRICS);
                    }

                    switch (FrmMsgBox.Show(z.Owner, "Statistics", null, "Create a new statistic or metric?", Resources.MsgHelp, new[]
                    {
                        new FrmMsgBox.ButtonSet("Statistic", Resources.ObjLScriptStatistic, DialogResult.Yes),
                        new FrmMsgBox.ButtonSet("Metric", Resources.ObjLScriptStatistic, DialogResult.No),
                        new FrmMsgBox.ButtonSet("Cancel", Resources.MnuCancel, DialogResult.Cancel)
                    }))
                    {
                        case DialogResult.Yes:
                            return _ShowScriptEditor(z, "Statistic", StatisticScript.INPUTS, UiControls.EInitialFolder.FOLDER_STATISTICS);

                        case DialogResult.No:
                            return _ShowScriptEditor(z, "Metric", MetricScript.INPUTS, UiControls.EInitialFolder.FOLDER_METRICS);

                        default:
                            return null;
                    }
                },
                ListChangesOnEdit = true,
                BeforeItemChanged = _ScriptReplace,
            };
        }

        /// <summary>
        /// ConfigurationStatistic
        /// </summary>
        internal static DataSet<ConfigurationStatistic> ForStatistics(Core core)
        {
            return new DataSet<ConfigurationStatistic>()
            {
                Core = core,
                Title = "Statistics",
                List = core.AllStatistics,
                ItemDescriptionProvider = _GetComment,
                ListChangeApplicator = z => core.SetStatistics(z.List, false, z.Progress),
                ListSupportsReorder = true,
                ItemEditor = z =>
                {
                    if (!_ShowEditPreamble(z.Owner, z.DefaultValue))
                    {
                        return null;
                    }

                    return FrmAlgoStatistic.Show(z.Owner, z.DefaultValue, core, z.ReadOnly);
                },
                AfterListChangesApplied = z => FrmMsgBox.ShowCompleted(z.owner, "Staticics", FrmEditUpdate.GetUpdateMessage(FrmEditUpdate.EChangeLevel.Statistic)),
            };
        }

        /// <summary>
        /// PeakFlag
        /// </summary>
        internal static DataSet<PeakFlag> ForPeakFlags(Core core)
        {
            return new DataSet<PeakFlag>()
            {
                Core = core,
                Title = "Peak Flags",
                List = core.Options.PeakFlags,
                ItemDescriptionProvider = z => z.Comments.FormatIf("\r\nComments: "),
                ListSupportsReorder = true,
            };
        }

        /// <summary>
        /// PeakFilter
        /// </summary>
        internal static DataSet<PeakFilter> ForPeakFilter(Core core)
        {
            return new DataSet<PeakFilter>()
            {
                Core = core,
                Title = "Peak Filters",
                List = core.AllPeakFilters,
                ItemDescriptionProvider = z => z.ParamsAsString() + z.Comment.FormatIf("\r\nComments: "),
                ListChangeApplicator = z => core.SetPeakFilters(z.List),
                ListSupportsReorder = true,
                ItemEditor = z =>
                {
                    var newList = DataSet.ForPeakFilterConditions(core, z.DefaultValue).ShowListEditor(z.Owner, z.ReadOnly, null);

                    if (newList == null)
                    {
                        return null;
                    }

                    return new PeakFilter(z.DefaultValue?.OverrideDisplayName, z.DefaultValue?.Comment, newList);
                },
            };
        }

        /// <summary>
        /// ObsFilter.Condition
        /// </summary>
        internal static DataSet<ObsFilter.Condition> ForObsFilterConditions(Core core, ObsFilter of)
        {
            return new DataSet<ObsFilter.Condition>()
            {
                Core = core,
                Title = "Observation filter conditions",
                List = of.Conditions.Cast<ObsFilter.Condition>(),
                ItemEditor = z => FrmObservationFilterCondition.Show(z.Owner, core, z.DefaultValue, z.ReadOnly),
                ListSupportsReorder = true,
            };
        }

        /// <summary>
        /// PeakFilter.Condition
        /// </summary>
        internal static DataSet<PeakFilter.Condition> ForPeakFilterConditions(Core core, PeakFilter of)
        {
            return new DataSet<PeakFilter.Condition>()
            {
                Core = core,
                Title = "Peak filter conditions",
                List = of.Conditions.Cast<PeakFilter.Condition>(),
                ItemEditor = z => FrmPeakFilterCondition.Show(z.Owner, core, z.DefaultValue, z.ReadOnly),
                ListSupportsReorder = true,
            };
        }

        /// <summary>
        /// ObsFilter
        /// </summary>
        internal static DataSet<ObsFilter> ForObsFilter(Core core)
        {
            return new DataSet<ObsFilter>()
            {
                Core = core,
                Title = "Observation Filters",
                List = core.AllObsFilters,
                ItemDescriptionProvider = z => z.ParamsAsString() + z.Comment.FormatIf("\r\nComments: "),
                ItemEditor = z =>
                {
                    var newlist = DataSet.ForObsFilterConditions(core, z.DefaultValue).ShowListEditor(z.Owner, z.ReadOnly, null);

                    if (newlist == null)
                    {
                        return null;
                    }

                    return new ObsFilter(z.DefaultValue?.OverrideDisplayName, z.DefaultValue?.Comment, newlist);
                },
                ListChangeApplicator = z => core.SetObsFilters(z.List),
                ListSupportsReorder = true,
            };
        }

        /// <summary>
        /// GroupInfo
        /// </summary>
        internal static DataSet<GroupInfo> ForGroups(Core core)
        {
            return new DataSet<GroupInfo>()
            {
                Core = core,
                Title = "Experimental Groups",
                List = core.Groups,
                ItemNameProvider = z => z.DisplayName,
                ItemDescriptionProvider = z => z.DisplayShortName + ": " + z.DisplayName,
                StringComparator = _TypeNameComparator,
                ItemEditor = z => { return FrmEditExpGroup.Show(z.Owner, z.DefaultValue, z.ReadOnly) ? z.DefaultValue : null; }
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

        /// <summary>
        /// Helper method: Shows error / remove results dialogues
        /// </summary>
        private static bool _ShowEditPreamble(Form owner, ConfigurationBase toEdit)
        {
            if (toEdit != null)
            {
                if (toEdit.HasError)
                {
                    FrmMsgBox.ButtonSet[] btns = {
                                                     new   FrmMsgBox.ButtonSet("Continue", Resources.MnuAccept, DialogResult.Yes),
                                                     new   FrmMsgBox.ButtonSet("Clear error", Resources.MnuDelete, DialogResult.No),
                                                     new   FrmMsgBox.ButtonSet("Cancel", Resources.MnuCancel, DialogResult.Cancel)};

                    switch (FrmMsgBox.Show(owner, "Error report", "Last time this configuration was run it reported an error", toEdit.Error, Resources.MsgWarning, btns))
                    {
                        case DialogResult.Yes:
                            break;

                        case DialogResult.No:
                            toEdit.ClearError();
                            break;

                        case DialogResult.Cancel:
                            return false;

                        default:
                            throw new SwitchException();
                    }
                }

                if (toEdit.HasResults)
                {
                    string text1 = "The configuration to be modified has results associated with it";
                    string text2 = "Changing this configuration will result in the loss of the associated results.";

                    FrmMsgBox.ButtonSet[] btns = {  new FrmMsgBox.ButtonSet( "Replace", Resources.MnuAccept, DialogResult.No),
                                                        new FrmMsgBox.ButtonSet( "Cancel", Resources.MnuCancel, DialogResult.Cancel)};

                    switch (FrmMsgBox.Show(owner, owner.Text, text1, text2, Resources.MsgHelp, btns, "FrmBigList.EditConfig", DialogResult.No))
                    {
                        case DialogResult.No:
                            break;

                        case DialogResult.Cancel:
                            return false;

                        default:
                            throw new SwitchException();
                    }
                }
            }

            return true;
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
        /// Helper function, removes scripts.
        /// </summary>    
        private static void _ScriptReplace(Form owner, AlgoBase find, AlgoBase replace)
        {
            if (find == null || find == replace)
            {
                // Nothing removed or item reinserted
                return;
            }

            if (find.Script == null)
            {
                FrmMsgBox.ShowError(owner, "This algorithm is inbuilt and cannot be removed.");
                return;
            }

            if (find.Script.FileName == null)
            {
                FrmMsgBox.ShowError(owner, "This script is locked for editing, consider working on a copy instead.");
                return;
            }

            if (FrmMsgBox.ShowYesNo(owner, "Delete script file?", find.Script.FileName, Resources.MsgWarning))
            {
                File.Delete(find.Script.FileName);
                Algo.Instance.Rebuild();
            }
        }

        /// <summary>
        /// Helper function, shows the script editor.
        /// </summary>    
        private static T _ShowScriptEditor<T>(DataSet<T>.EditItemArgs z, string title, string inputs, UiControls.EInitialFolder folder)
          where T : AlgoBase
        {
            string fileName;
            string defaultContent;

            if (z.DefaultValue != null)
            {
                if (z.DefaultValue.Script == null)
                {
                    FrmMsgBox.ShowError(z.Owner, "This algorithm is inbuilt and cannot be viewed or edited.");
                    return null;
                }

                fileName = z.DefaultValue.Script.FileName;

                if (fileName == null && !z.WorkOnCopy)
                {
                    FrmMsgBox.ShowError(z.Owner, "This script is locked for editing, consider working on a copy instead.");
                    return null;
                }

                defaultContent = z.DefaultValue.Script.Script;
            }
            else
            {
                fileName = null;
                defaultContent = null;
            }

            string newFile = FrmRScript.Show(z.Owner, title, inputs, folder, fileName, z.WorkOnCopy, defaultContent, z.ReadOnly);

            if (newFile == null)
            {
                return null;
            }

            Algo.Instance.Rebuild();
            string id = Algo.GetId(folder, newFile);
            return (T)Algo.Instance.All.First(zz => zz.Id == id);
        }
    }
}
