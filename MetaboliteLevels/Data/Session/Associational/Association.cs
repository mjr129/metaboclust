using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;

namespace MetaboliteLevels.Data.Session.Associational
{
    /// <summary>
    /// Throwaway object that "associates" two other objects.
    /// 
    /// These are obtained by calling FindAssociations on an Associational object.
    /// 
    /// The type parameter (T) is needed because the list views need to find the correct columns.
    /// </summary>
    [XColumnConcreteGeneratorAttribute]
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

            this.OriginalRequest = source;
            this.Associated = target;       
            this._extraColumnValues = extraColumnValues;
            this.Icon = (target as IIconProvider)?.Icon ?? Resources.IconUnknown;
        }

        public void GetXColumns( CustomColumnRequest request )
        {
            ColumnCollection<Association<T>> results = request.Results.Cast<Association<T>>();

            // Add association as-is (don't include a folder before it!)
            results.AddRange( ColumnManager.AddSubObject<Association<T>>( EColumn.Advanced, EColumn.Visible, request.Core, "", z => z.Associated, typeof(T) ) );

            // Add extra columns from original request
            if (this.OriginalRequest?.ExtraColumns != null)
            {
                for (int n = 0; n < this.OriginalRequest.ExtraColumns.Count; ++n)
                {
                    int closure = n;
                    Tuple<string, string> c = this.OriginalRequest.ExtraColumns[n];

                    results.Add( new Column<Association<T>>( c.Item1, EColumn.Visible, c.Item2, z => z.GetExtraColumnValue(closure), z => Color.Blue ) );
                }
            }

            request.NoAutomaticColumns = true; // Else conflicts
        }

        private object GetExtraColumnValue( int closure )
        {
            if (closure < 0 || closure >= _extraColumnValues.Length)
            {
                return "Missing column data";
            }

            return _extraColumnValues[closure];
        }

        public Image Icon { get; private set; }

        object IAssociation.Associated => this.Associated;

        public override string ToString()
        {
            return this.Associated.ToString();
        }
    }
}
