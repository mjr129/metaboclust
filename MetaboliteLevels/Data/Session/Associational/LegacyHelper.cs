using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Data.Session.Associational
{
    static class LegacyHelper
    {
        public static PeakValueSet Get_AltObservations( this Peak peak, Core core )
        {
            throw new NotImplementedException();
        }

        public static PeakValueSet Get_Observations( this Peak peak, Core core )
        {
            throw new NotImplementedException();
        }

        public static PeakValueSet Get_CorrectionChain( this Peak peak, Core core, int index )
        {
            throw new NotImplementedException();
        }

        public static PeakValueSet Get_OriginalObservations( this Peak peak, Core core )
        {
            throw new NotImplementedException();
        }

        public static double[] Get_Observations_Trend( this Peak peak, Core core )
        {
            return Get_Observations( peak, core ).Trend;
        }

        public static double[] Get_Observations_Raw( this Peak peak, Core core )
        {
            return Get_Observations( peak, core ).Raw;
        }

        internal static Vector Create_Vector( IntensityMatrix matrix, int rowIndex )
        {
            return new MetaboliteLevels.Algorithms.Vector( matrix, rowIndex );
        }
    }
}
