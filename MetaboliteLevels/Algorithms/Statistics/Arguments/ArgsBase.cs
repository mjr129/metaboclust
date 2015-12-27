using System;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Base class for algorithm arguments
    /// 
    /// Arguments define what goes into the statistic.
    /// 
    /// These usually contain some sort of filter for the inputs (e.g. only the control group)
    /// and parameters for the algorithm itself (e.g. k = 3).
    /// </summary>
    [Serializable]
    abstract class ArgsBase
    {
        /// <summary>
        /// The user-inputtable parameters.
        /// </summary>
        public readonly object[] Parameters;

        /// <summary>
        /// Constructor
        /// </summary> 
        public ArgsBase(object[] parameters)
        {
            Parameters = parameters;
        }
        
        public sealed override string ToString()
        {
            return ToString(null);
        }

        public abstract string ToString(AlgoBase algorithm);
    }
}
