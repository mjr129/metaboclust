using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.General
{
    /// <summary>
    /// Methods for IVisualisable.
    /// </summary>
    internal static class IVisualisableExtensions
    {
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
            if (v == null)
            {
                return false;
            }

            return !v.SupportsHide.Has( EPrevent.Hide );
        }

        public static bool SupportsRename( this Visualisable v )
        {
            if (v == null)
            {
                return false;
            }

            return !v.SupportsHide.Has( EPrevent.Name );
        }

        public static bool SupportsComment( this Visualisable v )
        {
            if (v == null)
            {
                return false;
            }

            return !v.SupportsHide.Has( EPrevent.Comment );
        }
    }
}
