using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.Definitions.Corrections.Implementations;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics.Implementations;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends.Implementations;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Evaluation;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Forms.Text;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Types.UI
{
    /// <summary>
    /// This class maps pretty much everything to the DataSet class, which specifies how things
    /// are displayed and edited.
    /// 
    /// See DataSet(of T) for the main description.
    /// </summary>
    static class DataSet
    {
        public delegate IDataSet Provider( Core core );

        /// <summary>
        /// An enum with flags
        /// </summary>
        public static DataSet<T> ForFlagsEnum<T>( Core core, string title )
            where T : struct, IComparable, IFormattable, IConvertible // aka. Enum
        {
            return new DataSet<T>()
            {
                Core = core,
                ListTitle = title,
                ListSource = EnumHelper.GetEnumFlags<T>(),
                ItemTitle = z => EnumHelper.ToUiString( (Enum)(object)z ),
                ItemDescription = z => EnumHelper.ToDescription( (Enum)(object)z ),
                StringComparator = _EnumComparator<T>,
            };
        }

        /// <summary>
        /// An arbitrary list of strings
        /// </summary>
        public static DataSet<int> ForString( Core core, string title, params string[] options )
        {
            return new DataSet<int>()
            {
                Core = core,
                ListTitle = title,
                ListSource = options.Indices(),
                ItemTitle = z => options[z],
            };
        }

        /// <summary>
        /// An enum without flags
        /// </summary>
        public static DataSet<T> ForDiscreteEnum<T>( Core core, string title, T cancelValue )
            where T : struct, IComparable, IFormattable, IConvertible // aka. Enum
        {
            return new DataSet<T>()
            {
                Core = core,
                ListTitle = title,
                ListSource = Enum.GetValues( typeof( T ) ).Cast<T>().Except( new T[] { cancelValue } ),
                ItemTitle = z => EnumHelper.ToUiString( (Enum)(object)z ),
                ItemDescription = z => EnumHelper.ToDescription( (Enum)(object)z ),
                StringComparator = _EnumComparator<T>,
                CancelValue = cancelValue,
            };
        }

        /// <summary>
        /// The session's observations.
        /// </summary>
        public static DataSet<ObservationInfo> ForObservations( Core core )
        {
            return new DataSet<ObservationInfo>()
            {
                Core = core,
                ListTitle = "Observations",
                ListSource = core.Observations,
                ItemDescription = z => "Group = " + z.Group.DisplayName + ", Time = " + z.Time + ", Replicate = " + z.Rep + "\r\nBatch = " + z.Batch + ", Acquisition = " + z.Acquisition,
                Icon = Resources.IconAdduct
            };
        }

        /// <summary>
        /// The session's observations.
        /// </summary>
        public static DataSet<ObservationInfo> ForConditions( Core core )
        {
            return new DataSet<ObservationInfo>()
            {
                Core = core,
                ListTitle = "Conditions",
                ListSource = core.Conditions,
                ItemDescription = z => "Group = " + z.Group.DisplayName + ", Time = " + z.Time + ", Replicate = " + z.Rep + "\r\nBatch = " + z.Batch + ", Acquisition = " + z.Acquisition,
                Icon = Resources.IconCondition,
            };
        }

        /// <summary>
        /// The columns of a listview.
        /// </summary>
        public static DataSet<Column> ForColumns( IEnumerable<Column> columns )
        {
            return new DataSet<Column>()
            {
                ListTitle = "Columns",
                ListSource = columns.Where( z => !z.IsAlwaysEmpty ),
                ItemTitle = z => z.Id,
                ItemDescription = z => z.OverrideDisplayName,
            };
        }

        /// <summary>
        /// The session's cluster-optimistation tests
        /// </summary>             
        public static DataSet<ClusterEvaluationPointer> ForTests( Core core )
        {
            return new DataSet<ClusterEvaluationPointer>()
            {
                Core = core,
                ListTitle = "Test Results",
                ListSource = core.EvaluationResultFiles,
                ItemTitle = _GetDisplayName,
                ItemDescription = z => "- CLUSTERER: " + z.Configuration.ParameterConfigAsString + "\r\n- VALUES: " + z.Configuration.ParameterValuesAsString + (z.FileName != null ? ("\r\n- FILENAME: " + z.FileName) : ""),
                HandleEdit = z => FrmEvaluateClusteringOptions.Show( z.Owner, core, z.DefaultValue, z.ReadOnly ),
                HandleCommit = z => core.EvaluationResultFiles.ReplaceAll( z.List ),
                ListSupportsReorder = true,
            };
        }

        /// <summary>
        /// The session's acquisition indices
        /// </summary>
        public static DataSet<int> ForAcquisitions( Core core )
        {
            return new DataSet<int>()
            {
                Core = core,
                ListTitle = "Acquisition Indices",
                ListSource = core.Acquisitions,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
            };
        }

        /// <summary>
        /// The session's experimental groups
        /// </summary>
        public static DataSet<GroupInfo> ForGroups( Core core )
        {
            return new DataSet<GroupInfo>()
            {
                Core = core,
                ListTitle = "Experimental Groups",
                ListSource = core.Groups,
                ItemTitle = _GetDisplayName,
                ItemDescription = z => z.DisplayShortName + ": " + z.DisplayName,
                StringComparator = _TypeNameComparator,
                HandleEdit = z => { return FrmEditGroupBase.Show( z.Owner, z.DefaultValue, z.ReadOnly ) ? z.DefaultValue : null; },
                Icon = Resources.IconGroups,
            };
        }


        /// <summary>
        /// The session's batches.
        /// </summary>
        public static DataSet<BatchInfo> ForBatches( Core core )
        {
            return new DataSet<BatchInfo>()
            {
                Core = core,
                ListTitle = "Batches",
                ListSource = core.Batches,
                ItemTitle = _GetDisplayName,
                ItemDescription = z => z.DisplayShortName + z.Comment.FormatIf( "\r\nComment: " ),
                StringComparator = _TypeNameComparator,
                HandleEdit = z => { return FrmEditGroupBase.Show( z.Owner, z.DefaultValue, z.ReadOnly ) ? z.DefaultValue : null; },
                Icon = Resources.IconGroups,
            };
        }

        /// <summary>
        /// The session's timepoints
        /// </summary>
        public static DataSet<int> ForTimes( Core core )
        {
            return new DataSet<int>()
            {
                Core = core,
                ListTitle = "Time Points",
                ListSource = core.Times,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
                Icon = Resources.IconTime,
            };
        }

        /// <summary>
        /// The session's replicate indices
        /// </summary>
        public static DataSet<int> ForReplicates( Core core )
        {
            return new DataSet<int>()
            {
                Core = core,
                ListTitle = "Replicates",
                ListSource = core.Reps,
                CancelValue = int.MinValue,
                IntegerBehaviour = true,
                Icon = Resources.IconReplicate,
            };
        }

        /// <summary>
        /// The headers for a particular metadata (misc user data) set
        /// </summary>
        public static DataSet<string> ForMetaHeaders( MetaInfoHeader headerCollection )
        {
            return new DataSet<string>()
            {
                ListTitle = "Headers",
                ListSource = headerCollection.Headers,
                Icon = Resources.IconInformation,
            };
        }

        /// <summary>
        /// The parameters for an agrotithm
        /// </summary>
        public static DataSet<int> ForParameters( Core core, AlgoParameter[] parameters, int selectedIndex, string message = null )
        {
            return new DataSet<int>()
            {
                Core = core,
                ListTitle = "Parameters",
                ListSource = parameters.Indices(),
                ItemTitle = z => parameters[z].Name + " (" + parameters[z].Type.ToString() + ")",
                ItemDescription = z => "Parameter " + z.ToString(),
                CancelValue = int.MinValue,
            };
        }

        /// <summary>
        /// The session's clusters
        /// </summary>
        public static DataSet<Cluster> ForClusters( Core core )
        {
            return new DataSet<Cluster>()
            {
                Core = core,
                ListTitle = "Clusters",
                ListSource = core.Clusters,
                ItemTitle = _GetDisplayName,
                ItemDescription = _GetComment,
                Icon = Resources.IconCluster,
            };
        }

        public static DataSet<Pathway> ForPathways( Core core )
        {
            return new DataSet<Pathway>()
            {
                Core = core,
                ListTitle = "Pathways",
                ListSource = core.Pathways,
                ItemTitle = _GetDisplayName,
                ItemDescription = _GetComment,
                Icon = Resources.IconPathway,
            };
        }

        public static DataSet<Adduct> ForAdducts( Core core )
        {
            return new DataSet<Adduct>()
            {
                Core = core,
                ListTitle = "Adducts",
                ListSource = core.Adducts,
                ItemTitle = _GetDisplayName,
                ItemDescription = _GetComment,
                Icon = Resources.IconAdduct,
            };
        }

        public static DataSet<Compound> ForCompounds( Core core )
        {
            return new DataSet<Compound>()
            {
                Core = core,
                ListTitle = "Compounds",
                ListSource = core.Compounds,
                ItemTitle = _GetDisplayName,
                ItemDescription = _GetComment,
                Icon = Resources.IconCompound,
            };
        }

        /// <summary>
        /// The session's peaks
        /// </summary>
        public static DataSet<Peak> ForPeaks( Core core )
        {
            return new DataSet<Peak>()
            {
                Core = core,
                ListTitle = "Peaks",
                ListSource = core.Peaks,
                ItemTitle = _GetDisplayName,
                ItemDescription = _GetComment,
                Icon = Resources.IconPeak,
            };
        }

        /// <summary>
        /// Datasets
        /// </summary>         
        public static DataSet<IDataSet> ForDatasetProviders(Core core)
        {
            return new DataSet<IDataSet>()
            {
                Core = core,
                ListTitle = "Datasets",
                ListSource = _EnumerateDatasetProviders( core ),
                ItemTitle = z => z.Title,
                ItemDescription = z => z.SubTitle,
                Icon = Resources.IconPeak,
            };
        }

        private static IEnumerable<IDataSet> _EnumerateDatasetProviders( Core core)
        {
            List<IDataSet> results = new List<IDataSet>();

            foreach (MethodInfo method in typeof( DataSet ).GetMethods( BindingFlags.Public | BindingFlags.Static ))
            {
                if (!typeof( IDataSet ).IsAssignableFrom( method.ReturnType ))
                {
                    continue;
                }

                var mParams = method.GetParameters();

                if (mParams.Length != 1 || mParams[0].ParameterType != typeof( Core ))
                {
                    continue;
                }

                if (method.Name == nameof( ForDatasetProviders ))
                {
                    continue;
                }

                results.Add( (IDataSet)method.Invoke( null, new object[] { core } ) );
            }

            return results;
        }

        public static DataSet<Annotation> ForAnnotations( Core core )
        {
            return new DataSet<Annotation>()
            {
                Core = core,
                ListTitle = "Annotations",
                ListSource = core.Annotations,
                ItemTitle = _GetDisplayName,
                ItemDescription = _GetComment,
                Icon = Resources.IconAnnotation,
            };
        }

        public static DataSet<Assignment> ForAssignments( Core core )
        {
            return new DataSet<Assignment>()
            {
                Core = core,
                ListTitle = "Assignments",
                ListSource = core.Assignments,
                ItemTitle = _GetDisplayName,
                ItemDescription = _GetComment,
                Icon = Resources.IconPeak,
            };
        }

        /// <summary>
        /// The session's peaks
        /// </summary>
        public static DataSet<Vector> ForVectors( Core core, IntensityMatrix matrix )
        {
            return new DataSet<Vector>()
            {
                Core = core,
                ListTitle = "Vectors",
                ListSource = matrix.Vectors,
                ItemTitle = z => z.ToString(),
                ItemDescription = z => z.Peak.Comment,
                Icon = Resources.IconPeak,
            };
        }

        /// <summary>
        /// Pretty much everything in the session in one go (all IVisualisables in Core).
        /// </summary>
        public static DataSet<object> ForEverything( Core core )
        {
            List<object> all = new List<object>();

            foreach (EDataSet x in Enum.GetValues( typeof( EDataSet ) ))
            {
                all.AddRange( For( x, core ).UntypedGetList(false).Cast<object>() );
            }

            return new DataSet<object>()
            {
                Core = core,
                ListTitle = "All items",
                ListSource = all,
                ItemTitle = z => z.GetType().Name.ToSmallCaps() + ": " + z.ToString(),
                ItemDescription = z => (z is Visualisable) ? "Selectable" : "Not selectable",
                Icon = Resources.IconCore,
            };
        }

        /// <summary>
        /// The session's trends
        /// </summary>
        public static DataSet<ConfigurationTrend> ForTrends( Core core )
        {
            return new DataSet<ConfigurationTrend>()
            {
                Core = core,
                ListTitle = "Trends",
                ListSource = core.Trends,
                ItemDescription = _GetComment,
                ListSupportsReorder = true,
                Icon = Resources.IconScriptTrend,  
                ItemsReferenceList = true,
                HandleCommit = z =>
                {             
                    core.SetTrends( z.List, z.Progress, z.Transient );
                },
                HandleEdit = z =>
                    {   
                        if (!_ShowEditPreamble( z.Cast<ConfigurationBase>() ))
                        {
                            return null;
                        }

                        return CommitEdit( FrmEditConfigurationTrend.Show( z.Owner, core, z.DefaultValue?.Args, z.ReadOnly ), z );   
                    },                                                                                
            };
        }

        /// <summary>
        /// The session's corrections
        /// </summary> 
        public static DataSet<ConfigurationCorrection> ForCorrections( Core core )
        {
            return new DataSet<ConfigurationCorrection>()
            {
                Core = core,
                ListTitle = "Corrections",
                ListSource = core.Corrections,
                ItemDescription = _GetComment,
                ListSupportsReorder = true,
                Icon = Resources.IconScriptCorrect,
                ItemsReferenceList = true,        
                HandleCommit = z =>
                {
                    core.SetCorrections( z.List, z.Progress, z.Transient );
                },
                HandleEdit = z =>
                {
                    if (!_ShowEditPreamble( z.Cast<ConfigurationBase>() ))
                    {
                        return null;
                    }

                    return CommitEdit( FrmEditConfigurationCorrection.Show( z.Owner, core, z.DefaultValue?.Args, z.ReadOnly ), z );
                }
            };
        }

        /// <summary>
        /// The session's clustering algorithms
        /// </summary>
        public static DataSet<ConfigurationClusterer> ForClusterers( Core core )
        {
            return new DataSet<ConfigurationClusterer>()
            {
                Core = core,
                ListTitle = "Clusterers",
                ListSource = core.Clusterers,
                ItemDescription = _GetComment,
                ListSupportsReorder = true,
                HandleCommit = z => core.SetClusterers( z.List, z.Progress, z.Transient ),
                Icon = Resources.IconScriptCluster,
                ItemsReferenceList = true,
                HandleEdit = z =>
                {
                    if (!_ShowEditPreamble( z.Cast<ConfigurationBase>() ))
                    {
                        return null;
                    }

                    return CommitEdit( FrmEditConfigurationCluster.Show( z.Owner, core, z.DefaultValue?.Args, z.ReadOnly, false ), z );
                }
            };
        }

        /// <summary>
        /// All available metrics (not just those in use)
        /// </summary>            
        public static DataSet<MetricBase> ForMetricAlgorithms( Core core )
        {
            return new DataSet<MetricBase>()
            {
                Core = core,
                ListTitle = "Distance metrics",
                ListSource = Algo.Instance.Metrics,
                HandleEdit = z => _ShowScriptEditor<MetricBase>( z, UiControls.EInitialFolder.FOLDER_METRICS ),
                ListIsSelfUpdating = true,
                BeforeItemChanged = _ScriptReplace,
            };
        }

        /// <summary>
        /// All intensity matrix providers
        /// </summary>            
        public static DataSet<IMatrixProvider> ForMatrixProviders( Core core )
        {
            return new DataSet<IMatrixProvider>()
            {
                Core = core,
                ListTitle = "Intensity matrices",
                ListSource = core.Matrices,
                ItemTitle = λ => λ.ToString(),
                ItemDescription = λ => λ.GetType().ToUiString(),
            };
        }

        /// <summary>
        /// All available trend algorithms (not just those in use)
        /// </summary>            
        public static DataSet<TrendBase> ForTrendAlgorithms( Core core )
        {
            return new DataSet<TrendBase>()
            {
                Core = core,
                ListTitle = "Trend algorithms",
                ListSource = Algo.Instance.Trends,
                HandleEdit = z => _ShowScriptEditor( z, UiControls.EInitialFolder.FOLDER_TRENDS ),
                ListIsSelfUpdating = true,
                BeforeItemChanged = _ScriptReplace,
                Icon = Resources.IconR,
            };
        }

        /// <summary>
        /// All available algorithms (not just those in use)
        /// </summary>            
        public static DataSet<AlgoBase> ForAllAlgorithms( Core core )
        {
            return new DataSet<AlgoBase>()
            {
                Core = core,
                ListTitle = "Algorithms",
                ListSource = Algo.Instance.All,
                HandleEdit = z => _ShowScriptEditor( z ),
                ListIsSelfUpdating = true,
                BeforeItemChanged = _ScriptReplace,
                Icon = Resources.IconR,
            };
        }

        /// <summary>
        /// All available clustering algorithms (not just those in use)
        /// </summary>            
        public static DataSet<ClustererBase> ForClustererAlgorithms( Core core )
        {
            return new DataSet<ClustererBase>()
            {
                Core = core,
                ListTitle = "Clustering algorithms",
                ListSource = Algo.Instance.Clusterers,
                HandleEdit = z => _ShowScriptEditor( z, UiControls.EInitialFolder.FOLDER_CLUSTERERS ),
                ListIsSelfUpdating = true,
                BeforeItemChanged = _ScriptReplace,
                Icon = Resources.IconR,
            };
        }

        /// <summary>
        /// All available trend and correction algorithms (not just those in use)
        /// </summary>            
        public static DataSet<AlgoBase> ForTrendAndCorrectionAlgorithms( Core core )
        {
            return new DataSet<AlgoBase>()
            {
                Core = core,
                ListTitle = "Correction algorithms",
                ListSource = Algo.Instance.Trends.Cast<AlgoBase>().Concat( Algo.Instance.Corrections ),
                HandleEdit = z => _ShowScriptEditor( z, UiControls.EInitialFolder.FOLDER_TRENDS, UiControls.EInitialFolder.FOLDER_CORRECTIONS ),
                ListIsSelfUpdating = true,
                BeforeItemChanged = _ScriptReplace,
                Icon = Resources.IconR,
            };
        }

        /// <summary>
        /// All available statistics (not just those in use)
        /// </summary>                  
        public static DataSet<StatisticBase> ForStatisticsAlgorithms( Core core )
        {
            return new DataSet<StatisticBase>()
            {
                Core = core,
                ListTitle = "Statistics",
                ListSource = Algo.Instance.Statistics,
                HandleEdit = z => _ShowScriptEditor( z, UiControls.EInitialFolder.FOLDER_STATISTICS, UiControls.EInitialFolder.FOLDER_METRICS ),
                ListIsSelfUpdating = true,
                BeforeItemChanged = _ScriptReplace,
                Icon = Resources.IconR,
            };
        }

        /// <summary>
        /// The session's statistics (those in use)
        /// </summary>
        public static DataSet<ConfigurationStatistic> ForStatistics( Core core )
        {
            return new DataSet<ConfigurationStatistic>()
            {
                Core = core,
                ListTitle = "Statistics",
                ListSource = core.Statistics,
                ItemDescription = _GetComment,
                ItemsReferenceList = true,
                HandleCommit = z => core.SetStatistics( z.List, z.Progress, z.Transient ),
                ListSupportsReorder = true,
                Icon = Resources.IconScriptStatistic,
                HandleEdit = z =>
                {
                    if (!_ShowEditPreamble( z.Cast<ConfigurationBase>() ))
                    {
                        return null;
                    }

                    return CommitEdit(FrmEditConfigurationStatistic.Show( z.Owner, z.DefaultValue?.Args, core, z.ReadOnly ), z );
                }
            };
        }

        private static T CommitEdit<T, U>( U result, DataSet<T>.EditItemArgs args )
            where T : class, IConfigurationBase<U>, new()
        {
            if (result == null)
            {
                return null;
            }

            T t;

            if (args.WorkOnCopy || args.DefaultValue == null)
            {
                t = new T();
            }
            else
            {
                t = args.DefaultValue;
            }

            t.Args = result;

            return t;
        }

        /// <summary>
        /// The session's peak (user-comment) flags
        /// </summary>
        public static DataSet<PeakFlag> ForPeakFlags( Core core )
        {
            return new DataSet<PeakFlag>()
            {
                Core = core,
                ListTitle = "Peak Flags",
                ListDescription = "These flags can be used to assign categories or labels to data",
                ListSource = core.Options.PeakFlags,
                ItemDescription = _GetComment,
                ListSupportsReorder = true,
                HandleEdit = z =>
                {
                    var val = z.WorkOnCopy ? z.DefaultValue.Clone() : (z.DefaultValue ?? new PeakFlag());
                    return FrmEditPeakFlag.Show( z.Owner, val, z.ReadOnly ) ? val : null;
                },
                ListIsSelfUpdating = true,
                Icon = Resources.ListIconFlag,
            };
        }

        /// <summary>
        /// All session's peak-filters
        /// </summary>
        public static DataSet<PeakFilter> ForPeakFilter( Core core )
        {
            return new DataSet<PeakFilter>()
            {
                Core = core,
                ListTitle = "Peak Filters",
                ListSource = core.PeakFilters,
                ItemDescription = z => z.ParamsAsString() + z.Comment.FormatIf( "\r\nComments: " ),
                HandleCommit = z => core.SetPeakFilters( z.List ),
                ListSupportsReorder = true,
                Icon = Resources.ListIconFilter,
                HandleEdit = z =>
                {
                    var newList = DataSet.ForPeakFilterConditions( core, z.DefaultValue ).ShowListEditor( z.Owner, z.ReadOnly ? FrmBigList.EShow.ReadOnly : FrmBigList.EShow.Acceptor, null );

                    if (newList == null || z.ReadOnly)
                    {
                        return null;
                    }

                    return new PeakFilter( z.DefaultValue?.OverrideDisplayName, z.DefaultValue?.Comment, newList );
                },
            };
        }

        /// <summary>
        /// All the filters for a particular observation filter
        /// </summary>
        public static DataSet<ObsFilter.Condition> ForObsFilterConditions( Core core, ObsFilter of )
        {
            return new DataSet<ObsFilter.Condition>()
            {
                Core = core,
                ListTitle = "Observation filter conditions",
                ListSource = of != null ? of.Conditions.Cast<ObsFilter.Condition>() : new ObsFilter.Condition[0],
                HandleEdit = z => FrmEditObsFilterCondition.Show( z.Owner, core, z.DefaultValue, z.ReadOnly ),
                ListSupportsReorder = true,
                Icon = Resources.ListIconFilter,
            };
        }

        /// <summary>
        /// All the filters for a particular peak filter
        /// </summary>
        public static DataSet<PeakFilter.Condition> ForPeakFilterConditions( Core core, PeakFilter of )
        {
            return new DataSet<PeakFilter.Condition>()
            {
                Core = core,
                ListTitle = "Peak filter conditions",
                ListSource = of != null ? of.Conditions.Cast<PeakFilter.Condition>() : new PeakFilter.Condition[0],
                HandleEdit = z => FrmEditPeakFilterCondition.Show( z.Owner, core, z.DefaultValue, z.ReadOnly ),
                ListSupportsReorder = true,
                Icon = Resources.ListIconFilter,
            };
        }

        /// <summary>
        /// The session's observation filters
        /// </summary>
        public static DataSet<ObsFilter> ForObsFilter( Core core )
        {
            return new DataSet<ObsFilter>()
            {
                Core = core,
                ListTitle = "Observation Filters",
                ListSource = core.ObsFilters,
                ItemDescription = z => z.ParamsAsString() + z.Comment.FormatIf( "\r\nComments: " ),
                Icon = Resources.ListIconFilter,
                HandleEdit = z =>
                {
                    var newlist = DataSet.ForObsFilterConditions( core, z.DefaultValue ).ShowListEditor( z.Owner, z.ReadOnly ? FrmBigList.EShow.ReadOnly : FrmBigList.EShow.Acceptor, null );

                    if (newlist == null || z.ReadOnly)
                    {
                        return null;
                    }

                    return new ObsFilter( z.DefaultValue?.OverrideDisplayName, z.DefaultValue?.Comment, newlist );
                },
                HandleCommit = z => core.SetObsFilters( z.List ),
                ListSupportsReorder = true,
            };
        }
        /// <summary>
        /// Private helper method: Gets comment for an IVisualisable
        /// </summary>
        private static string _GetComment( Visualisable vis )
        {
            if (!string.IsNullOrEmpty( vis.Comment ))
            {
                return "Comments: " + vis.Comment;
            }

            return null;
        }

        /// <summary>
        /// Private helper method: Gets display name for an IVisualisable
        /// </summary>
        private static string _GetDisplayName( Visualisable z )
        {
            return z.DisplayName;
        }

        /// <summary>
        /// Private helper method: Gets comment for a ConfigurationBase
        /// </summary>
        private static string _GetComment( ConfigurationBase z )
        {
            return z.DisplayName + z.Comment.FormatIf( "\r\nComments: " );
        }

        /// <summary>
        /// Private helper method: Compares enums to strings
        /// </summary>
        private static bool _EnumComparator<T>( string name, T value )
        {
            string a = ((Enum)(object)value).ToUiString().ToUpper();
            string b = value.ToString().ToUpper();
            string c = ((int)(object)value).ToString().ToUpper();

            name = name.ToUpper();

            return a == name || b == name || c == name;
        }

        /// <summary>
        /// Private helper method: Compares experimental group names to strings
        /// </summary>
        private static bool _TypeNameComparator( string name, GroupInfoBase group )
        {
            name = name.ToUpper();

            return group.DisplayName.ToUpper() == name || group.DisplayShortName.ToUpper() == name || group.Id == name;
        }

        /// <summary>
        /// Private helper method: Shows error and remove results dialogues
        /// </summary>
        private static bool _ShowEditPreamble( DataSet<ConfigurationBase>.EditItemArgs args )
        {
            if (args.DefaultValue != null)
            {
                if (args.DefaultValue.HasError)
                {
                    MsgBoxButton[] btns = {
                                                     new   MsgBoxButton(args.ReadOnly ? "View" : "Edit", Resources.MnuAccept, DialogResult.Yes),
                                                     new   MsgBoxButton("Clear error", Resources.MnuDelete, DialogResult.No),
                                                     new   MsgBoxButton("Cancel", Resources.MnuCancel, DialogResult.Cancel)};

                    switch (FrmMsgBox.Show( args.Owner, "Error report", "Last time this configuration was run it reported an error", args.DefaultValue.Error, Resources.MsgWarning, btns ))
                    {
                        case DialogResult.Yes:
                            break;

                        case DialogResult.No:
                            args.DefaultValue.ClearResults();
                            return false;

                        case DialogResult.Cancel:
                            return false;

                        default:
                            throw new SwitchException();
                    }
                }

                if (args.DefaultValue.HasResults && !args.ReadOnly)
                {
                    string text1 = "The configuration to be modified has results associated with it";
                    string text2 = "Changing this configuration will result in the loss of the associated results.";

                    MsgBoxButton[] btns = {  new MsgBoxButton( "Replace", Resources.MnuAccept, DialogResult.No),
                                                        new MsgBoxButton( "Cancel", Resources.MnuCancel, DialogResult.Cancel)};

                    switch (FrmMsgBox.Show( args.Owner, args.Owner.Text, text1, text2, Resources.MsgHelp, btns, FrmMsgBox.EDontShowAgainId.EditWithResults, DialogResult.No ))
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
        /// Private helper method: Checks script editability
        /// </summary>    
        private static void _ScriptReplace( Form owner, AlgoBase find, AlgoBase replace )
        {
            if (find == null || find == replace)
            {
                // Nothing removed or item reinserted
                return;
            }

            if (find.Script == null)
            {
                FrmMsgBox.ShowInfo( owner, "Unavailable", "This algorithm is stored in binary format and cannot be viewed or modified." );
                return;
            }

            if (find.Script.FileName == null)
            {
                FrmMsgBox.ShowInfo( owner, "Unavailable", "This script is inbuilt. Make a copy if you wish to modify it." );
                return;
            }

            if (replace == null) // Direct delete
            {
                if (FrmMsgBox.ShowYesNo( owner, "Delete script file from disk?", find.Script.FileName, Resources.MsgWarning ))
                {
                    File.Delete( find.Script.FileName );
                    Algo.Instance.Rebuild();
                }
            }
        }

        public static IDataSet For( EDataSet dataSet, Core core )
        {
            switch (dataSet)
            {
                case EDataSet.Acquisitions:
                    return DataSet.ForAcquisitions( core );  

                case EDataSet.Batches:
                    return DataSet.ForBatches( core );  

                case EDataSet.Clusterers:
                    return DataSet.ForClusterers( core );

                case EDataSet.Clusters:
                    return DataSet.ForClusters( core );

                case EDataSet.Conditions:
                    return DataSet.ForConditions( core );

                case EDataSet.Groups:
                    return DataSet.ForGroups( core );

                case EDataSet.Observations:
                    return DataSet.ForObservations( core );

                case EDataSet.ObservationFilters:
                    return DataSet.ForObsFilter( core );
                                                 
                case EDataSet.PeakFilters:
                    return DataSet.ForPeakFilter( core );
                                                   
                case EDataSet.PeakFlags:
                    return DataSet.ForPeakFlags( core );
                                                   
                case EDataSet.Peaks:
                    return DataSet.ForPeaks( core );
                                                   
                case EDataSet.Replicates:
                    return DataSet.ForReplicates( core );
                                                   
                case EDataSet.Statistics:
                    return DataSet.ForStatistics( core );
                                                   
                case EDataSet.Evaluations:
                    return DataSet.ForTests( core );
                                                   
                case EDataSet.Times:
                    return DataSet.ForTimes( core );
                                                   
                case EDataSet.Trends:
                    return DataSet.ForTrends( core );
                                                   
                case EDataSet.Corrections:
                    return DataSet.ForCorrections( core );

                case EDataSet.ClusteringAlgorithms:
                    return DataSet.ForClustererAlgorithms( core );
                                               
                case EDataSet.MetricAlgorithms:
                    return DataSet.ForMetricAlgorithms( core );
                                                   
                case EDataSet.StatisticsAlgorithms:
                    return DataSet.ForStatisticsAlgorithms( core );
                             
                case EDataSet.TrendAlgorithms:
                    return DataSet.ForTrendAlgorithms( core );
                                      
                case EDataSet.TrendAndCorrectionAlgorithms:
                    return DataSet.ForTrendAndCorrectionAlgorithms( core );
                                      
                case EDataSet.AllAlgorithms:
                    return DataSet.ForAllAlgorithms( core );

                case EDataSet.Assignments:
                    return DataSet.ForAssignments( core );

                case EDataSet.Annotations:
                    return DataSet.ForAnnotations( core );

                case EDataSet.Pathways:
                    return DataSet.ForPathways( core );

                case EDataSet.Compounds:
                    return DataSet.ForCompounds( core );

                case EDataSet.Adducts:
                    return DataSet.ForAdducts( core );

                default:
                    throw new SwitchException( dataSet );
            }
        }        

        /// <summary>
        /// Private helper method: Shows the script editor.
        /// </summary>    
        private static T _ShowScriptEditor<T>( DataSet<T>.EditItemArgs z, params UiControls.EInitialFolder[] folders )
          where T : AlgoBase
        {
            string fileName;
            string defaultContent;
            UiControls.EInitialFolder folder;

            if (z.DefaultValue != null)
            {
                if (z.DefaultValue.Script == null)
                {
                    FrmMsgBox.ShowInfo( z.Owner, "Unavailable", "This algorithm is inbuilt and cannot be removed." );
                    return null;
                }

                fileName = z.DefaultValue.Script.FileName;

                if (fileName == null && !z.WorkOnCopy)
                {
                    FrmMsgBox.ShowError( z.Owner, "This script is locked for editing, consider working on a copy instead." );
                    return null;
                }

                defaultContent = z.DefaultValue.Script.Script;

                if (z.DefaultValue is MetricScript)
                {
                    folder = UiControls.EInitialFolder.FOLDER_METRICS;
                }
                else if (z.DefaultValue is StatisticScript)
                {
                    folder = UiControls.EInitialFolder.FOLDER_STATISTICS;
                }
                else if (z.DefaultValue is ClustererScript)
                {
                    folder = UiControls.EInitialFolder.FOLDER_CLUSTERERS;
                }
                else if (z.DefaultValue is CorrectionScript)
                {
                    folder = UiControls.EInitialFolder.FOLDER_CORRECTIONS;
                }
                else if (z.DefaultValue is TrendScript)
                {
                    folder = UiControls.EInitialFolder.FOLDER_TRENDS;
                }
                else
                {
                    throw new SwitchException( z.DefaultValue.GetType() );
                }
            }
            else
            {
                fileName = null;
                defaultContent = null;

                if (folders.Length == 1)
                {
                    folder = folders[0];
                }
                else
                {
                    folder = FrmSelectAlgorithmType.Show( z.Owner, folders );

                    if (folder == UiControls.EInitialFolder.None)
                    {
                        return null;
                    }
                }
            }

            string title, inputTable;

            switch (folder)
            {
                case UiControls.EInitialFolder.FOLDER_CLUSTERERS:
                    title = "Clustering Algorithm";
                    inputTable = ClustererScript.INPUT_TABLE;
                    break;

                case UiControls.EInitialFolder.FOLDER_CORRECTIONS:
                    title = "Correction";
                    inputTable = CorrectionScript.INPUT_TABLE;
                    break;

                case UiControls.EInitialFolder.FOLDER_METRICS:
                    title = "Metric";
                    inputTable = MetricScript.INPUT_TABLE;
                    break;

                case UiControls.EInitialFolder.FOLDER_STATISTICS:
                    title = "Statistic";
                    inputTable = StatisticScript.INPUT_TABLE;
                    break;

                case UiControls.EInitialFolder.FOLDER_TRENDS:
                    title = "Smoothing Algorithm";
                    inputTable = TrendScript.INPUT_TABLE;
                    break;

                default:
                    throw new SwitchException( folder );
            }

            string newFile = FrmInputScript.Show( z.Owner, title, inputTable, folder, fileName, z.WorkOnCopy, defaultContent, z.ReadOnly );

            if (newFile == null)
            {
                return null;
            }

            Algo.Instance.Rebuild();
            string id = Algo.GetId( folder, newFile, false );
            return (T)Algo.Instance.All.First( zz => zz.Id == id );
        }    
    }

    /// <summary>
    /// Datasets as an enum
    /// </summary>
    public enum EDataSet
    {
        [Name( "Data\\Peaks" )]
        Peaks,

        [Name( "Data\\Pathways" )]
        Pathways,

        [Name( "Data\\Compounds" )]
        Compounds,

        [Name( "Data\\Assignments" )]
        Assignments,

        [Name( "Data\\Clusters" )]
        Clusters,

        [Name( "Data\\Adducts" )]
        Adducts,

        [Name( "Data\\Annotations" )]
        Annotations,

        [Name( "Data\\Acquisition indices" )]
        Acquisitions,

        [Name( "Data\\Batches" )]
        Batches,

        [Name( "Data\\Experimental conditions" )]
        Conditions,

        [Name( "Data\\Timepoints" )]
        Times,

        [Name( "Data\\Replicate indices" )]
        Replicates,

        [Name( "Data\\Peak flags" )]
        PeakFlags,

        [Name( "Data\\Experimental observations" )]
        Observations,

        [Name( "Data\\Experimental groups" )]
        Groups,

        [Name( "Workflow\\Peak filters" )]
        PeakFilters,

        [Name( "Workflow\\Observation filters" )]
        ObservationFilters,

        [Name( "Workflow\\Corrections" )]
        Corrections,

        [Name( "Workflow\\Trends" )]
        Trends,

        [Name( "Workflow\\Statistics" )]
        Statistics,

        [Name( "Workflow\\Clusterers" )]
        Clusterers,

        [Name( "Workflow\\Clustering evaluations" )]
        Evaluations,

        [Name( "Algorithms\\Trends and corrections" )]
        TrendAndCorrectionAlgorithms,

        [Name( "Algorithms\\Trends" )]
        TrendAlgorithms,

        [Name( "Algorithms\\Statistics" )]
        StatisticsAlgorithms,

        [Name( "Algorithms\\Metrics" )]
        MetricAlgorithms,

        [Name( "Algorithms\\Clustering" )]
        ClusteringAlgorithms,

        [Name( "Algorithms\\All" )]
        AllAlgorithms,
    }
}
