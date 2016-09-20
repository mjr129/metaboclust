using System;
using System.Text;
using MetaboliteLevels.Algorithms.Statistics.Corrections;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Settings;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
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
