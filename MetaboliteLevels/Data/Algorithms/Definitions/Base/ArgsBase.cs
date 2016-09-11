using System;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Visualisables;
using MGui.Helpers;

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
    internal abstract class ArgsBase
    {
        /// <summary>
        /// The user-inputtable parameters.
        /// </summary>
        public readonly object[] Parameters;

        private WeakReference<IProvider<IntensityMatrix>> _sourceProvider;

        public IProvider<IntensityMatrix> SourceProvider => _sourceProvider.GetTarget();

        public IntensityMatrix SourceMatrix => SourceProvider?.Provide;

        /// <summary>
        /// Constructor
        /// </summary> 
        protected ArgsBase( string id, IProvider<IntensityMatrix> sourceProvider, object[] parameters)
        {
            _sourceProvider = new WeakReference<IProvider<IntensityMatrix>>( sourceProvider );
            Id = id;
            Parameters = parameters;
        }

        public readonly string Id;

        public sealed override string ToString(  )
        {
            return ToString( null );
        }

        public virtual string ToString( AlgoBase algorithm )
        {
            return SourceMatrix.ToStringSafe() + "; " + AlgoParameterCollection.ParamsToHumanReadableString( Parameters, algorithm );
        }
    }
}
