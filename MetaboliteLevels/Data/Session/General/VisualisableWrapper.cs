using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Data.Session.General
{
    internal class VisualisableWrapper : IVisualisable
    {
        private IDataSet _set;
        private object _x;

        public VisualisableWrapper( IDataSet set, object x )
        {
            _set = set;
            _x = x;
        }

        public string Comment
        {
            get { return null; }
            set {/*NA*/}
        }

        public string DefaultDisplayName => _set.UntypedName( _x );

        public string DisplayName => DefaultDisplayName;

        public bool Hidden
        {
            get { return false; }
            set {/*NA*/}
        }

        public string OverrideDisplayName
        {
            get { return null; }
            set {/*NA*/}
        }

        public EVisualClass VisualClass => EVisualClass.None;

        public IEnumerable<Column> GetColumns( Core core )
        {
            List<Column<VisualisableWrapper>> columns = new List<Column<VisualisableWrapper>>();

            columns.Add( "Name", EColumn.Visible, z => z._set.UntypedName( z._x ) );
            columns.Add( "Description", EColumn.Visible, z => z._set.UntypedDescription( z._x ) );

            return columns;
        }

        public UiControls.ImageListOrder GetIcon() => UiControls.ImageListOrder.Info;

        public void RequestContents( ContentsRequest list )
        {
            // NA
        }        
    }
}
