using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Singular;

namespace MetaboliteLevels.Controls.Lists
{
    interface IColumnProvider
    {
        IEnumerable<Column> GetXColumns(Core core);
    }
}
