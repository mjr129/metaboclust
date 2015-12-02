using System;
using MetaboliteLevels.Data.DataInfo;
using System.Collections.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Settings;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for a trend used as a correction method.
    /// </summary>
    [Serializable]
    class ArgsTrendAsCorrection : ArgsTrend
    {
        public readonly ECorrectionMode Mode;
        public readonly ECorrectionMethod Method;
        public readonly GroupInfo ControlGroup;
        public readonly ObsFilter Constraint;

        public ArgsTrendAsCorrection(ECorrectionMode mode, ECorrectionMethod method, GroupInfo controlGroup, ObsFilter constraint, object[] args)
            : base(args)
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

    /// <summary>
    /// The data the correction is performed upon.
    /// </summary>
    public enum ECorrectionMode
    {
        /// <summary>Correct batchwise</summary>
        Batch = 1,
        /// <summary>Correct for the control group</summary>
        Control = 2,
    }

    /// <summary>
    /// The data the correction is performed upon.
    /// </summary>
    public enum ECorrectionMethod
    {
        /// <summary>Correct via subtraction</summary>
        Subtract,
        /// <summary>Correct via division</summary>
        Divide,
    }
}