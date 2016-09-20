using System;
using System.Collections.Generic;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    // Enums used by some ArgsBase derivatives.

    // Delegates
    delegate double AlgoDelegate_Input1(double[] a);
    delegate double AlgoDelegate_EInput1(IEnumerable<double> a);
    // ReSharper disable once InconsistentNaming
    delegate double AlgoDelegate_Input2(double[] a, double[] b);

    /// <summary>
    /// Which input vector to get?
    /// </summary>
    [Flags]
    public enum EAlgoInput
    {
        A = 1,
        B = 2
    }

    /// <summary>
    /// How to get data
    /// </summary>
    public enum EAlgoSourceMode
    {
        None,       // The value takes no input (uses its own algorithm to determine the input)
        Full,       // A value for every observation
        Trend,      // A value for every item in the trend
    }

    /// <summary>
    /// Where to get input vector B from
    /// </summary>
    public enum EAlgoInputBSource
    {
        None,       // Nothing (i.e. is a single vector statistic such as the mean OR the stat does not support filters and uses its own algorithm to get the input vector)
        SamePeak,   // Same peak, different points
        Time,       // Points from the time coordinate
        AltPeak,    // Points from different peak
        DmPeak,     // Points from provided second peak (e.g. for calculating distance matrix - currently never used)
    }

    /// <summary>
    /// The data the correction is performed upon.
    /// </summary>
    public enum ECorrectionMode
    {
        None = 0,
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
        None,
        /// <summary>Correct via subtraction</summary>
        Subtract,
        /// <summary>Correct via division</summary>
        Divide,
    }
}
