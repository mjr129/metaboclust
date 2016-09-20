using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Singular;
using MGui.Datatypes;

namespace MetaboliteLevels.Utilities
{
    static class ParseElementCollectionExtensions
    {
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="propertyTarget">Elements {!xxx} will get the xxx property from this object, elements {xxx} will get the xxx value from QueryValue(xxx)</param>
        /// <returns>String</returns>
        public static string ConvertToString( this ParseElementCollection self, object propertyTarget, Core core )
        {
            var r = new StringBuilder();

            foreach (var x in self.Contents)
            {
                if (x.IsInBrackets)
                {
                    r.Append( Column.AsString( ColumnManager.QueryProperty( core, propertyTarget, x.Value ), EListDisplayMode.Content ) );
                }
                else
                {
                    r.Append( x.Value );
                }
            }

            return r.ToString();
        }
    }
}
