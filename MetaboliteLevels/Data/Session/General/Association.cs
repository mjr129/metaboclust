using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Session.General
{
    interface IAssociation
    {
        [NotNull] ContentsRequest OriginalRequest { get; }

        [NotNull] object Associated { get; }
    }

    /// <summary>
    /// Throwaway object that "associates" two other objects.
    /// 
    /// These are obtained by calling FindAssociations on an Associational object.
    /// 
    /// The type parameter (T) is needed because the list views need to see different types of associations differently in order
    /// to find use the correct columns.
    /// </summary>
    internal sealed class Association<T> : IColumnProvider, IIconProvider, IAssociation
    {
        [NotNull] public ContentsRequest OriginalRequest { get; }
                        
        [NotNull] public T Associated { get; }

        private readonly object[] _extraColumnValues;

        public Association( [NotNull] ContentsRequest source, [NotNull] T target, object[] extraColumnValues )
        {
            if (target == null)
            {
                throw new ArgumentNullException( "target" );
            }

            if (source == null)
            {
                throw new ArgumentNullException( "source" );
            }

            OriginalRequest = source;
            Associated = target;       
            _extraColumnValues = extraColumnValues;
            Icon = (target as IIconProvider)?.Icon ?? Resources.IconUnknown;
        }

        public void GetXColumns( ColumnCollection list, Core core )
        {
            ColumnCollection<Association<T>> results = list.Cast<Association<T>>();

            // Add association as-is
            results.AddRange( ColumnManager.AddSubObject<Association<T>>( EColumn.Advanced, EColumn.Visible, core, "Association\\", z => z.Associated, typeof(T) ) );

            // Add extra columns from original request
            if (OriginalRequest?.ExtraColumns != null)
            {
                for (int n = 0; n < OriginalRequest.ExtraColumns.Count; ++n)
                {
                    int closure = n;
                    Tuple<string, string> c = OriginalRequest.ExtraColumns[n];

                    results.Add( new Column<Association<T>>( c.Item1, EColumn.Visible, c.Item2, z => z._extraColumnValues[closure], z => Color.Blue ) );
                }
            }                
        }

        public Image Icon { get; private set; }

        object IAssociation.Associated => Associated;

        public override string ToString()
        {
            return Associated.ToString();
        }
    }
}
