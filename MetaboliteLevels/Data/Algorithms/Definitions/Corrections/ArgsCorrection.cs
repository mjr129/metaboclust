using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Session.General;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Corrections
{
    /// <summary>
    /// Arguments for CorrectionBase derivatives.
    /// 
    /// Nothing really special here!
    /// </summary>
    [Serializable]
    class ArgsCorrection : ArgsBase
    {
        public readonly ECorrectionMode Mode;       // (ONLY FOR TREND-BASED CORRECTIONS) How to run the correction
        public readonly ECorrectionMethod Method;   // (ONLY FOR TREND-BASED CORRECTIONS) What method we use
        public readonly GroupInfo ControlGroup;     // (ONLY FOR TREND-BASED CORRECTIONS) Control group (only used when [Mode] is [Control])
        public readonly ObsFilter Constraint;       // (ONLY FOR TREND-BASED CORRECTIONS) Constraint on the input vector

        public ArgsCorrection( string id, IMatrixProvider source, object[] parameters, ECorrectionMode mode, ECorrectionMethod method, GroupInfo controlGroup, ObsFilter constraint)
            : base( id, source, parameters )
        {
            Mode = mode;
            Method = method;
            ControlGroup = controlGroup;
            Constraint = constraint;
        }

        public bool IsUsingTrend => Mode != ECorrectionMode.None;

        public ArgsTrend ToTrend()
        {
            return new ArgsTrend( this.Id, this.SourceProvider, this.Parameters );
        }
    }
}
