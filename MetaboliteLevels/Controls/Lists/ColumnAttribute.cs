using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Controls.Lists
{
    internal class XColumnAttribute : NameAttribute
    {
        public XColumnAttribute( EColumn special, string description = null )
            : this( null, special, description )
        {
        }


        public XColumnAttribute( string name = null, EColumn special = EColumn.None, string description = null )
            : base( name )
        {
            Special = special;
            Description = description;
        }

        public string Description { get; set; }

        public EColumn Special { get; set; }
    }
}
