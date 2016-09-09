using System;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Visualisables;

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

        public readonly MatrixProducer Source;

        public IntensityMatrix SourceMatrix => Source.Product;

        /// <summary>
        /// Constructor
        /// </summary> 
        public ArgsBase( MatrixProducer source, object[] parameters)
        {
            Source = source;
            Parameters = parameters;
        }
        
        public sealed override string ToString(  )
        {
            return ToString( null );
        }

        public virtual string ToString( AlgoBase algorithm )
        {
            return Source?.Product.ToString() + "; " + AlgoParameterCollection.ParamsToHumanReadableString( Parameters, algorithm );
        }
    }
}
