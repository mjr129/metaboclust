using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Session.General
{        
    /// <summary>
    /// Throwaway object that "associates" two other objects.
    /// 
    /// These are obtained by calling FindAssociations on an Associational object.
    /// </summary>
    class Association : IColumnProvider, IIconProvider
    {
        public ContentsRequest OriginalRequest { get; }
                                                                            
        public object Associated { get; }

        private readonly object[] _extraColumnValues;

        public Association( ContentsRequest source, object target, object[] extraColumnValues )
        {
            OriginalRequest = source;
            Associated = target;
            _extraColumnValues = extraColumnValues;
            Icon = (target as IIconProvider)?.Icon ?? UiControls.ImageListOrder.Unknown;
        }

        public IEnumerable<Column> GetXColumns( Core core )
        {
            List<Column<Association>> results = new List<Column<Association>>();

            if (OriginalRequest?.ExtraColumns != null)
            {
                for (int n = 0; n < OriginalRequest.ExtraColumns.Count; ++n)
                {
                    int closure = n;
                    Tuple<string, string> c = OriginalRequest.ExtraColumns[n];

                    results.Add( new Column<Association>( c.Item1, EColumn.Visible, c.Item2, z => z._extraColumnValues[closure], z => Color.Blue ) );

                }
            }                                                                                                                                                      

            return results.Cast<Column>().Concat( GetExtraColumns( core ) );
        }

        protected virtual IEnumerable<Column> GetExtraColumns( Core core )
        {
            return new Column[0];
        }

        public UiControls.ImageListOrder Icon { get; private set; }    
    }
}
