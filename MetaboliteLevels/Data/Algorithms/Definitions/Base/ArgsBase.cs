using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Base
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
    internal abstract class ArgsBase : Visualisable, IArgsBase<AlgoBase>
    {
        /// <summary>
        /// The user-inputtable parameters.
        /// </summary>
        [XColumn( "Parameters", EColumn.Visible | EColumn.Content )]
        public readonly object[] Parameters;

        private WeakReference<IMatrixProvider> _sourceProvider;

        [XColumn( "Source", EColumn.Visible )]
        public IMatrixProvider SourceProvider => _sourceProvider.GetTarget();

        [XColumn( "Source matrix", EColumn.None )]
        public IntensityMatrix SourceMatrix => SourceProvider?.Provide;

        public override string DefaultDisplayName
        {
            get
            {
                var algorithm = GetAlgorithmOrNull();

                return (algorithm?.DisplayName ?? Id)
                    + " (" + SourceMatrix.ToStringSafe() + ") "
                    + AlgoParameterCollection.ParamsToHumanReadableString( Parameters, algorithm );
            }
        }

        /// <summary>
        /// Constructor
        /// </summary> 
        protected ArgsBase( string id, IMatrixProvider sourceProvider, object[] parameters )
        {
            _sourceProvider = new WeakReference<IMatrixProvider>( sourceProvider );
            Id = id;
            Parameters = parameters;
        }

        public readonly string Id;

        public override UiControls.ImageListOrder Icon => UiControls.ImageListOrder.Point;

        /// <summary>
        /// Returns the display name of algorithm, or the ID if not found
        /// </summary>  
        [XColumn( "Algorithm", EColumn.Visible )]
        public string AlgoName => GetAlgorithmOrNull()?.DisplayName ?? Id;

        /// <summary>
        /// Gets cached algorithm or throws an exception if not found.
        /// </summary>
        public AlgoBase GetAlgorithmOrThrow()
        {
            var r = GetAlgorithmOrNull();

            if (r == null)
            {
                throw new KeyNotFoundException( $"Couldn't find the algorithm with ID = \"{Id}\". It has been moved or deleted or was created on a different computer. Put the algorithm back or select a different algorithm." );
            }

            return r;
        }

        /// <summary>
        /// Gets cached algorithm or throws returns null if not found.
        /// </summary>
        public AlgoBase GetAlgorithmOrNull() => Algo.Instance.All.Get( this.Id );

        /// <summary>
        /// Checks if the algorithm is available.
        /// </summary>
        /// <returns></returns>
        public bool CheckIsAvailable()
        {
            return GetAlgorithmOrNull() != null;
        }
    }

    [Serializable]
    internal class ArgsBase<TAlgo> :ArgsBase, IArgsBase<TAlgo>
        where TAlgo : AlgoBase
    {
        public new TAlgo GetAlgorithmOrThrow() => (TAlgo)base.GetAlgorithmOrThrow();

        public new TAlgo GetAlgorithmOrNull() => (TAlgo)base.GetAlgorithmOrNull();

        public ArgsBase( string id, IMatrixProvider sourceProvider, object[] parameters ) : base( id, sourceProvider, parameters )
        {
        }
    }


    internal interface IArgsBase<TAlgo>
        where TAlgo : AlgoBase
    {
        /// <summary>
        /// Strong typed version of base class.
        /// </summary>
        TAlgo GetAlgorithmOrThrow();

        /// <summary>
        /// Strong typed version of base class.
        /// </summary>
        TAlgo GetAlgorithmOrNull();
    }
}
