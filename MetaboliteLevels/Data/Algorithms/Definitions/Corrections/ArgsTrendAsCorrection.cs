using System;
using MetaboliteLevels.Data.DataInfo;
using System.Collections.Generic;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Settings;
using MGui.Helpers;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for a trend used as a correction method.
    /// </summary>
    [Serializable]
    class ArgsTrendAsCorrection : ArgsTrend
    {
        public readonly ECorrectionMode Mode;       // How to run the correction
        public readonly ECorrectionMethod Method;   // What method we use
        public readonly GroupInfo ControlGroup;     // Control group (only used when [Mode] is [Control])
        public readonly ObsFilter Constraint;       // Constraint on the input vector

        public ArgsTrendAsCorrection( MatrixProducer source, ECorrectionMode mode, ECorrectionMethod method, GroupInfo controlGroup, ObsFilter constraint, object[] args)
            : base( source, args )
        {
            Mode = mode;
            Method = method;
            Constraint = constraint;
            ControlGroup = controlGroup;
        }

        public override string ToString(AlgoBase algorithm)
        {
            return Mode.ToUiString() + " " + Method.ToUiString() + " " + base.ToString(algorithm);
        }
    }                                   
}