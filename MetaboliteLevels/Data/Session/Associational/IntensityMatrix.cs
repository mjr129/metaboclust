using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Data.Session.Associational
{     
    class IntensityMatrix
    {
        public double[][] Values;
        public Peak[] Rows;
        public object[] Columns;

        public IntensityMatrix( Peak[] rows, object[] columns )
        {
            Rows = rows;
            Columns = columns;
        }
    }
}
