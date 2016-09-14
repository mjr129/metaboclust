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
    internal class VisualisableWrapper : Visualisable
    {
        private IDataSet _set;
        private object _x;

        public VisualisableWrapper( IDataSet set, object x )
        {
            _set = set;
            _x = x;
        }      

        public override string DefaultDisplayName => _set.UntypedName( _x );

        public override EPrevent SupportsHide => EPrevent.Hide | EPrevent.Name;

        public override IEnumerable<Column> GetColumns( Core core )
        {
            List<Column<VisualisableWrapper>> columns = new List<Column<VisualisableWrapper>>();

            columns.Add( "Name", EColumn.Visible, z => z._set.UntypedName( z._x ) );
            columns.Add( "Description", EColumn.Visible, z => z._set.UntypedDescription( z._x ) );

            return columns;
        }

        public override UiControls.ImageListOrder Icon=> UiControls.ImageListOrder.Point;  
    }
}
