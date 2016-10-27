using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.Session.Associational
{
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
        public readonly object Owner;

        /// <summary>
        /// Request - Type of results to get.
        /// </summary>
        public readonly EVisualClass Type;

        /// <summary>
        /// Request - Type of results to get.
        /// </summary>
        public readonly Type TypeAsType;

        /// <summary>
        /// Response - Title of the results.
        /// </summary>
        public string Text;

        /// <summary>
        /// Response - List of results
        /// </summary>
        public readonly List<IAssociation> Results = new List<IAssociation>();

        /// <summary>
        /// Extra columns
        /// </summary>
        public readonly List<Tuple<string, string>> ExtraColumns = new List<Tuple<string, string>>();

        /// <summary>
        /// CONSTRUCTOR
        /// </summary> 
        public ContentsRequest( Core core, object owner, EVisualClass type )
        {
            this.Core = core;
            this.Owner = owner;
            this.Type = type;
            this.TypeAsType = Translate( type );
        }

        private static Type Translate( EVisualClass type )
        {
            switch (type)
            {                      
                case EVisualClass.SpecialAll:
                case EVisualClass.SpecialMeta:
                case EVisualClass.SpecialAdvanced:
                case EVisualClass.SpecialStatistic: return typeof( Main.Associational.ColumnValuePair );
                case EVisualClass.Peak: return typeof( Peak );
                case EVisualClass.Cluster: return typeof( Cluster );
                case EVisualClass.Compound: return typeof( Compound );
                case EVisualClass.Adduct: return typeof( Adduct );
                case EVisualClass.Pathway: return typeof( Pathway );
                case EVisualClass.Assignment: return typeof( Assignment );
                case EVisualClass.Annotation: return typeof( Annotation );
                case EVisualClass.None: return typeof( object ); // Doesn't matter, the list will be empty. Can't be null.
                default: throw new SwitchException( type );
            }
        }

        public void Add<T>( T item, params object[] extraColumns )
        {
            if (typeof(T) != this.TypeAsType)
            {
                throw new InvalidOperationException( $"Expected the type of item {{{typeof( T ).ToString()}}} added to ContentsRequest to match the request {{{this.TypeAsType.Name}}}." );
            }

            this.Results.Add( new Association<T>( this, item, extraColumns ) );
        }

        /// <summary>
        /// Adds a range of items (this can't be done if there are unique columns).
        /// </summary>                                                             
        public void AddRange<T>( IEnumerable<T> items )
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    this.Add<T>( item );
                }
            }
        }

        internal void AddExtraColumn( string title, string description = null )
        {
            this.ExtraColumns.Add( Tuple.Create( title, description ) );
        }

        internal void AddRangeWithCounts<T>( Counter<T> counts )
        {
            foreach (KeyValuePair<T, int> kvp in counts)
            {
                this.Add( kvp.Key, kvp.Value );
            }
        }
    }
}
