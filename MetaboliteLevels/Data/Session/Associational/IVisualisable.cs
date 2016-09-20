using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Associational
{
    /// <summary>
    /// Stuff that shows in lists.
    /// </summary>
    [Serializable]
    internal abstract class Visualisable : IColumnProvider
    {
        /// <summary>
        /// Displayed name of this item.
        /// </summary>
        [XColumn("Name", EColumn.Visible)]
        public virtual string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty( OverrideDisplayName ))
                {
                    return DefaultDisplayName;
                }
                else
                {
                    return OverrideDisplayName;
                }
            }
        }

        public sealed override string ToString() => DisplayName;

        /// <summary>
        /// Assigned name of this item.
        /// </summary>                               
        public abstract string DefaultDisplayName { get; }

        /// <summary>
        /// User overriden name of this item
        /// </summary>                               
        public virtual string OverrideDisplayName { get; set; }

        /// <summary>
        /// Comments applied to this item.
        /// </summary>
        [XColumn]
        public virtual string Comment { get; set; }

        /// <summary>
        /// Is this object hidden from view
        /// </summary>   
        public virtual bool Hidden { get; set; }

        /// <summary>
        /// Icon for this item (as an index).
        /// </summary>
        public abstract UiControls.ImageListOrder Icon { get; }

        /// <summary>
        /// STATIC
        /// Gets columns
        /// </summary>
        public virtual IEnumerable<Column> GetXColumns( Core core )
        {
            return null;
        }

        public virtual EPrevent SupportsHide => EPrevent.None;       
    }

    internal class XColumnAttribute : NameAttribute
    {
        public XColumnAttribute( EColumn special, string description = null )
            : this(null, special, description )
        {
        }


        public XColumnAttribute( string name = null, EColumn special = EColumn.None, string description = null)
            : base( name )
        {
            Special = special;
            Description = description;
        }

        public string Description { get; set; }

        public EColumn Special { get; set; }
    }

    [Flags]
    enum EPrevent
    {
        None,
        Hide=1,
        Comment=2,
        Name =4,
    }

    /// <summary>
    /// Stuff that can have associations.
    /// Peaks...clusters...adducts...metabolites...pathways.
    /// </summary>
    [Serializable]
    internal abstract class Associational : Visualisable
    {
        /// <summary>
        /// Gets related items.
        /// </summary>
        public abstract void FindAssociations( ContentsRequest list );

        /// <summary>
        /// VisualClass of IVisualisable
        /// </summary>
        public abstract EVisualClass AssociationalClass { get; }
    }

    /// <summary>
    /// Classes able to provide previews for IVisualisable derivatives.
    /// </summary>
    interface IPreviewProvider
    {
        /// <summary>
        /// Provides a preview for the specified IVisualisable.
        /// </summary>
        Image ProvidePreview( Size size, object target );
    }

    interface IBackup
    {
        void Backup( BackupData data );
        void Restore( BackupData data );
    }

    class BackupManager
    {
        private Dictionary<IBackup, BackupData> _data = new Dictionary<IBackup, BackupData>();

        public void Backup( object item, string reason )
        {
            var itemt = item as IBackup;

            if (itemt == null)
            {
                return;
            }

            if (!_data.ContainsKey( itemt ))
            {
                _data.Add( itemt, new BackupData( itemt ) );
            }
        }

        public void RestoreAll()
        {
            foreach (var kvp in _data)
            {
                kvp.Value.Restore();
            }

            _data.Clear();
        }
    }

    class BackupData
    {
        private IBackup _target;
        private Queue _data = new Queue();

        public BackupData( IBackup target )
        {
            _target = target;
            _target.Backup( this );
        }

        public void Restore()
        {
            _target.Restore( this );
        }

        public void Push( object x )
        {
            _data.Enqueue( x );
        }

        public void Pull<T>( ref T x )
        {
            x = (T)_data.Dequeue();
        }

        internal T Pull<T>()
        {
            return (T)_data.Dequeue();
        }
    }

    /// <summary>
    /// Methods for IVisualisable.
    /// </summary>
    internal static class IVisualisableExtensions
    {
        /// <summary>
        /// Can be used with QueryProperty to search for internal properties.
        /// </summary>
        public const string SYMBOL_PROPERTY = "prop:";

        /// <summary>
        /// (EXTENSION) (MJR) Retrieves a request for contents with the specified parameters.
        /// </summary>
        public static ContentsRequest GetContents( this Associational self, Core core, EVisualClass type )
        {
            ContentsRequest cl = new ContentsRequest( core, self, type );
            self.FindAssociations( cl );
            return cl;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>    
        public static string FormatDisplayName( Visualisable visualisable )
        {
            return string.IsNullOrEmpty( visualisable.OverrideDisplayName ) ? visualisable.DefaultDisplayName : visualisable.OverrideDisplayName;
        }

        /// <summary>
        /// (EXTENSION) (MJR) Gets the display name, or "nothing" if null.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string SafeGetDisplayName( this Visualisable self )
        {
            if (self == null)
            {
                return "nothing";
            }

            return self.DisplayName;
        }

        /// <summary>
        /// (EXTENSION) (MJR) Gets the enabled elements of an IVisualisable enumerable.
        /// </summary>                                               
        public static IEnumerable<T> WhereEnabled<T>( this IEnumerable<T> self )
            where T : Visualisable
        {
            return self.Where( z => !z.Hidden );
        }

        /// <summary>
        /// (EXTENSION) (MJR) Gets the enabled elements of an IVisualisable enumerable.
        /// </summary>     
        public static IEnumerable<T> WhereEnabled<T>( this IEnumerable<T> self, bool onlyEnabled )
            where T : Visualisable
        {
            return onlyEnabled ? WhereEnabled( self ) : self;
        }       

        public static bool SupportsDisable( this Visualisable v )
        {
            return !v.SupportsHide.Has( EPrevent.Hide );
        }                                                                        

        public static bool SupportsRename( this Visualisable v )
        {
            return !v.SupportsHide.Has( EPrevent.Name );
        }

        public static bool SupportsComment( this Visualisable v )
        {
            return !v.SupportsHide.Has( EPrevent.Comment );
        }
    }

    class Association : Visualisable
    {
        [XColumn( "Target\\", EColumn.Visible | EColumn.Decompose )]
        public readonly Associational WrappedValue;
        public Associational SourceValue => _owner.Owner;
        public readonly ContentsRequest _owner;
        private readonly object[] _extraColumns;

        public override string Comment
        {
            get { return WrappedValue.Comment; }

            set { WrappedValue.Comment = value; }
        }

        public Association( ContentsRequest source, Associational target, object[] extraColumns )
        {
            _owner = source;
            WrappedValue = target;
            _extraColumns = extraColumns;
        }

        public override string DefaultDisplayName => WrappedValue.DefaultDisplayName;

        public override string DisplayName => WrappedValue.DisplayName;

        public override bool Hidden
        {
            get { return WrappedValue.Hidden; }
            set { WrappedValue.Hidden = value; }
        }

        public override string OverrideDisplayName
        {
            get { return WrappedValue.OverrideDisplayName; }

            set { WrappedValue.OverrideDisplayName = value; }
        }

        public override IEnumerable<Column> GetXColumns( Core core )
        {
            List<Column<Association>> results = new List<Column<Association>>();

            if (_owner?.ExtraColumns != null)
            {
                for (int n = 0; n < _owner.ExtraColumns.Count; ++n)
                {
                    var closure = n;
                    var c = _owner.ExtraColumns[n];

                    results.Add( new Column<Association>( c.Item1, EColumn.Visible, c.Item2, z => z._extraColumns[closure], z => Color.Blue ) );

                }
            }

            return results.Cast<Column>().Concat( GetExtraColumns( core ) );
        }

        protected virtual IEnumerable<Column> GetExtraColumns( Core core )
        {
            return new Column[0];
        }

        public override UiControls.ImageListOrder Icon => WrappedValue.Icon;
    }

    /// <summary>
    /// Request for contents from an IVisualisable.
    /// </summary>
    internal class ContentsRequest
    {
        /// <summary>
        /// Request - Core
        /// </summary>
        public readonly Core Core;

        /// <summary>
        /// Request - Called upon
        /// </summary>
        public readonly Associational Owner;

        /// <summary>
        /// Request - Type of results to get.
        /// </summary>
        public readonly EVisualClass Type;

        /// <summary>
        /// Response - Title of the results.
        /// </summary>
        public string Text;

        /// <summary>
        /// Response - List of results
        /// </summary>
        public readonly List<Association> Results = new List<Association>();

        /// <summary>
        /// Extra columns
        /// </summary>
        public readonly List<Tuple<string, string>> ExtraColumns = new List<Tuple<string, string>>();

        /// <summary>
        /// CONSTRUCTOR
        /// </summary> 
        public ContentsRequest( Core core, Associational owner, EVisualClass type )
        {
            this.Core = core;
            this.Owner = owner;
            this.Type = type;
        }

        public void Add<T>( T item, params object[] extraColumns )
            where T : Associational
        {
            Results.Add( new Association( this, item, extraColumns ) );
        }

        /// <summary>
        /// Adds a range of items (this can't be done if there are unique columns).
        /// </summary>                                                             
        public void AddRange( IEnumerable<Associational> items )
        {
            if (items != null)
            {
                foreach (Associational item in items)
                {
                    Add( item );
                }
            }
        }

        internal void AddExtraColumn( string title, string description )
        {
            ExtraColumns.Add( Tuple.Create( title, description ) );
        }

        internal void AddRangeWithCounts<T>( Counter<T> counts )
            where T : Associational
        {
            foreach (var kvp in counts.Counts)
            {
                Add( kvp.Key, kvp.Value );
            }
        }
    }

    /// <summary>
    /// Types of IVisualisable.
    /// </summary>
    public enum EVisualClass
    {
        None,
        Info,
        Statistic,
        Peak,
        Cluster,
        Compound,
        Adduct,
        Pathway,
        Assignment,
        Annotation,
    }
}
