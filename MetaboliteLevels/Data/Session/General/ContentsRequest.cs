using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Session.General
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
        public readonly Associational.Associational Owner;

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
        public ContentsRequest( Core core, Associational.Associational owner, EVisualClass type )
        {
            this.Core = core;
            this.Owner = owner;
            this.Type = type;
        }

        public void Add( object item, params object[] extraColumns )
        {
            Results.Add( new Association( this, item, extraColumns ) );
        }

        /// <summary>
        /// Adds a range of items (this can't be done if there are unique columns).
        /// </summary>                                                             
        public void AddRange( IEnumerable items )
        {
            if (items != null)
            {
                foreach (object item in items)
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
        {
            foreach (KeyValuePair<T, int> kvp in counts.Counts)
            {
                Add( kvp.Key, kvp.Value );
            }
        }
    }
}
