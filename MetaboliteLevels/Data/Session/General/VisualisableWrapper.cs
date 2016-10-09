using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;

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

        public override string Comment
        {
            get
            {
                return _set.UntypedDescription( _x );
            }        
            set
            {
                // NA
            }
        }

        public override EPrevent SupportsHide => EPrevent.Hide | EPrevent.Name | EPrevent.Comment;        

        public override Image Icon=> Resources.IconPoint;  
    }
}
