using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Types.UI;
using MGui.Helpers;
using RDotNet;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// Represents a type of parameter requested for an algorithm.
    /// </summary>
    internal interface IAlgoParameterType
    {
        /// <summary>
        /// Converts text (from user input) to the parameter type.
        /// </summary>                                            
        /// <returns>Result, or null on failure.</returns>
        [CanBeNull] object FromString( Core core, string text );

        /// <summary>
        /// Opens a GUI browse to select a value for the parameter type.
        /// </summary>                                                  
        /// <returns>Result, or null if cancelled.</returns>
        [CanBeNull] object Browse( Form owner, Core core, object sel );

        /// <summary>
        /// Creates an R symbol in <paramref name="rEngine"/> with the specified <paramref name="name"/> and <paramref name="value"/>.
        /// </summary>                                    
        void SetSymbol( REngine rEngine, string name, object value );

        /// <summary>
        /// Obtains the name of this type, used in user R scripts.
        /// </summary>
        [NotNull] string Name { get; }

        /// <summary>
        /// Obtains tracking details (i.e. pointers to the current results held by objects).
        /// See <see cref="SourceTracker"/> for details.
        /// </summary>
        /// <param name="param">Parameter value to track</param>
        /// <returns>Null, or one or more references which will be used to test for changes to this parameter.</returns>
        [CanBeNull] WeakReference[] TrackChanges( object param );
    }

    /// <summary>
    /// Contains the concrete implementations of <see cref="IAlgoParameterType"/>.
    /// </summary>
    internal static class AlgoParameterTypes
    {
        public static IAlgoParameterType Integer                       = new _Integer();
        public static IAlgoParameterType Double                        = new _Double();
        public static IAlgoParameterType WeakRefStatisticArray         = new _WeakRefStatisticArray();
        public static IAlgoParameterType WeakRefPeak                   = new _WeakRefPeak();
        public static IAlgoParameterType Group                         = new _Group();
        public static IAlgoParameterType WeakRefConfigurationClusterer = new _WeakRefConfigurationClusterer();
        public static IAlgoParameterType WeakRefClusterArray           = new _WeakRefClusterArray();

        public static Dictionary<string, IAlgoParameterType> GetKeys()
        {
            return GetAll().ToDictionary( z => z.Name );
        }

        public static IEnumerable<IAlgoParameterType> GetAll()
        {
            yield return Integer;
            yield return Double;
            yield return WeakRefStatisticArray;
            yield return WeakRefPeak;
            yield return Group;
            yield return WeakRefConfigurationClusterer;
            yield return WeakRefClusterArray;
        }

        private abstract class _AlgoParameterType<T> : IAlgoParameterType
        {
            public virtual string Name => GetType().ToUiString();

            public WeakReference[] TrackChanges( object param )
            {
                return this.OnTrackChanges( (T)param );
            }

            protected virtual WeakReference[] OnTrackChanges( T param )
            {
                return null;
            }

            public object FromString( Core core, string text )
            {
                text = text.Trim().ToUpper();
                return OnFromString( core, text );
            }

            public object Browse( Form owner, Core core, object value )
            {
                return OnBrowse( owner, core, value );
            }

            public virtual void SetSymbol( REngine rEngine, string name, object value )
            {
                throw new InvalidOperationException( "ApplyArgs: " + ToString() + " on " + name );
            }

            protected abstract object OnBrowse( Form owner, Core _core, object value );

            protected abstract object OnFromString( Core core, string text );

            public override string ToString()
            {
                return Name.ToSmallCaps();
            }
        }

        [Name( "Integer" )]
        private class _Integer : _AlgoParameterType<int>
        {
            protected override object OnFromString( Core core, string text )
            {
                int vi;

                if (text == "MAX")
                {
                    return int.MaxValue;
                }
                else if (text == "MIN")
                {
                    return int.MinValue;
                }
                else if (int.TryParse( text, out vi ))
                {
                    return vi;
                }
                else
                {
                    return null;
                }
            }

            protected override object OnBrowse( Form owner, Core _core, object value )
            {
                FrmMsgBox.ButtonSet[] btns =
                {
                    new FrmMsgBox.ButtonSet( "MAX", Resources.MnuUp, DialogResult.Yes ),
                    new FrmMsgBox.ButtonSet( "MIN", Resources.MnuDown, DialogResult.No ),
                    new FrmMsgBox.ButtonSet( "Cancel", Resources.MnuCancel, DialogResult.Cancel )
                };

                switch (FrmMsgBox.Show( owner, "Select Integer", null, "Select a value or enter a custom value into the textbox", Resources.MsgHelp, btns, DialogResult.Cancel, DialogResult.Cancel ))
                {
                    case DialogResult.Yes:
                        return (object)int.MaxValue;

                    case DialogResult.No:
                        return (object)int.MinValue;

                    default:
                        return null;
                }
            }

            public override void SetSymbol( REngine rEngine, string name, object value )
            {
                rEngine.SetSymbol( name, rEngine.CreateInteger( (int)value ) );
            }
        }

        [Name( "Double" )]
        private class _Double : _AlgoParameterType<double>
        {
            protected override object OnFromString( Core core, string text )
            {
                double vd;

                if (text == "MAX")
                {
                    return double.MaxValue;
                }
                else if (text == "MIN")
                {
                    return double.MinValue;
                }
                else if (double.TryParse( text, out vd ))
                {
                    return vd;
                }
                else
                {
                    return null;
                }
            }

            public override void SetSymbol( REngine rEngine, string name, object value )
            {
                rEngine.SetSymbol( name, rEngine.CreateNumeric( (double)value ) );
            }

            protected override object OnBrowse( Form owner, Core _core, object value )
            {
                {
                    FrmMsgBox.ButtonSet[] btns =
                    {
                        new FrmMsgBox.ButtonSet( "MAX", Resources.MnuUp, DialogResult.Yes ),
                        new FrmMsgBox.ButtonSet( "MIN", Resources.MnuDown, DialogResult.No ),
                        new FrmMsgBox.ButtonSet( "Cancel", Resources.MnuCancel, DialogResult.Cancel )
                    };

                    switch (FrmMsgBox.Show( owner, "Select Double", null, "Select a value or enter a custom value into the textbox", Resources.MsgHelp, btns, DialogResult.Cancel, DialogResult.Cancel ))
                    {
                        case DialogResult.Yes:
                            return (object)double.MaxValue;

                        case DialogResult.No:
                            return (object)double.MinValue;

                        default:
                            return null;
                    }
                }
            }
        }

        [Name( "Statistic[]" )]
        private class _WeakRefStatisticArray : _AlgoParameterType<WeakReference<ConfigurationStatistic>[]>
        {
            protected override WeakReference[] OnTrackChanges( WeakReference<ConfigurationStatistic>[] param )
            {
                return param.Select( z => new WeakReference( z.GetTargetOrThrow().Results) ).ToArray();
            }

            protected override object OnFromString( Core core, string text )
            {
                string[] e2 = text.Split( ",;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries );

                WeakReference<ConfigurationStatistic>[] r = new WeakReference<ConfigurationStatistic>[e2.Length];
                ConfigurationStatistic[] opts = IVisualisableExtensions.WhereEnabled( core.AllStatistics ).ToArray();

                for (int n = 0; n < e2.Length; n++)
                {
                    int uid;

                    if (!int.TryParse( e2[n].Trim(), out uid ))
                    {
                        return null;
                    }

                    if (uid < 0 || uid >= opts.Length)
                    {
                        return null;
                    }

                    ConfigurationStatistic stat = opts[uid];

                    if (stat == null)
                    {
                        return null;
                    }

                    r[n] = new WeakReference<ConfigurationStatistic>( stat );
                }

                return r;
            }

            protected override object OnBrowse( Form owner, Core _core, object value )
            {
                var tvalue = (WeakReference<ConfigurationStatistic>[])value;
                IEnumerable<ConfigurationStatistic> def = tvalue.Select( z => z.GetTarget() ).Where( z => z != null );
                IEnumerable<ConfigurationStatistic> sel = DataSet.ForStatistics( _core ).ShowCheckList( owner, def );

                if (sel == null)
                {
                    return null;
                }

                return sel.Select( z => new WeakReference<ConfigurationStatistic>( z ) ).ToArray();
            }
        }

        [Name( "Peak" )]     
        private class _WeakRefPeak : _AlgoParameterType<WeakReference<Peak>>
        {
            protected override object OnBrowse( Form owner, Core _core, object value )
            {
                var sel = DataSet.ForPeaks( _core ).ShowList( owner, ((WeakReference<Peak>)value).GetTarget() );

                if (sel == null)
                {
                    return null;
                }

                return new WeakReference<Peak>( sel );
            }

            protected override object OnFromString( Core core, string text )
            {
                string peakName = text;

                Peak peak = core.Peaks.FirstOrDefault( z => z.DisplayName.ToUpper() == peakName );

                if (peak != null)
                {
                    return new WeakReference<Peak>( peak );
                }
                else
                {
                    return null;
                }
            }
        }

        [Name( "Group" )]
        private class _Group : _AlgoParameterType<GroupInfo>
        {
            protected override object OnFromString( Core core, string text )
            {
                string el = text.Trim();
                return core.Groups.FirstOrDefault( z => z.Id == el );
            }

            protected override object OnBrowse( Form owner, Core _core, object value )
            {
                return DataSet.ForGroups( _core ).ShowList( owner, (GroupInfo)value );
            }
        }

        [Name( "Clusterer" )]
        private class _WeakRefConfigurationClusterer : _AlgoParameterType<WeakReference<ConfigurationClusterer>>
        {
            protected override WeakReference[] OnTrackChanges( WeakReference<ConfigurationClusterer> param )
            {
                return new[] { new WeakReference( param.GetTargetOrThrow().Results) };
            }

            protected override object OnBrowse( Form owner, Core _core, object value )
            {
                ConfigurationClusterer def = ((WeakReference<ConfigurationClusterer>)value).GetTarget();
                var sel = DataSet.ForClusterers( _core ).ShowList( owner, def );

                if (sel == null)
                {
                    return null;
                }

                return new WeakReference<ConfigurationClusterer>( sel );
            }

            protected override object OnFromString( Core core, string text )
            {
                int ival;

                if (!int.TryParse( text, out ival ))
                {
                    return null;
                }

                ConfigurationClusterer[] opts = IVisualisableExtensions.WhereEnabled( core.AllClusterers ).ToArray();

                if (ival < 0 || ival >= opts.Length)
                {
                    return null;
                }

                return new WeakReference<ConfigurationClusterer>( opts[ival] );
            }
        }

        [Name( "Cluster[]" )]
        private class _WeakRefClusterArray : _AlgoParameterType<WeakReference<Cluster>[]>
        {
            protected override object OnFromString( Core core, string text )
            {
                int ival;

                if (!int.TryParse( text, out ival ))
                {
                    return null;
                }

                Cluster[] opts = core.Clusters.ToArray(); // TODO: Efficient?

                if (ival < 0 || ival >= opts.Length)
                {
                    return null;
                }

                return new WeakReference<Cluster>( opts[ival] );
            }

            protected override object OnBrowse( Form owner, Core _core, object value )
            {
                Cluster def = ((WeakReference<Cluster>)value).GetTarget();
                var sel = DataSet.ForClusters( _core ).ShowList( owner, def );

                if (sel == null)
                {
                    return null;
                }

                return new WeakReference<Cluster>( sel );
            }
        }
    }   
}
