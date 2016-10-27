using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Properties;

namespace MetaboliteLevels.Data.Session.Associational
{
    internal class VisualisableWrapper : Visualisable
    {
        private IDataSet _set;
        private object _x;

        public VisualisableWrapper( IDataSet set, object x )
        {
            this._set = set;
            this._x = x;
        }      

        public override string DefaultDisplayName => this._set.UntypedName( this._x );

        public override string Comment
        {
            get
            {
                return this._set.UntypedDescription( this._x );
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
