using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Utilities;
using System.Diagnostics;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Represents a configurared algorithm, containing the configuration and results.
    /// 
    /// This is a mutable object and can be modified by the user by changing the arguments.
    /// The results are discarded upon modification.
    /// 
    /// The arguments comprise an immutable object with the intention of all changes going through ConfigurationBase.
    /// 
    /// 
    /// </summary>
    /// <typeparam name="TAlgo">Type of statistic</typeparam>
    /// <typeparam name="TArgs">Type of arguments</typeparam>
    /// <typeparam name="TResults">Type of results</typeparam>
    [Serializable]
    internal abstract class ConfigurationBase<TAlgo, TArgs, TResults> : ConfigurationBase
        where TAlgo : AlgoBase
        where TArgs : ArgsBase
        where TResults : ResultBase
    {
        /// <summary>
        /// Flagged if the algorithm is disposed
        /// </summary>
        [NonSerialized]
        private bool _isDisposed;

        /// <summary>
        /// Contains error text (when run unsuccessfully)
        /// </summary>
        private string _error;

        /// <summary>
        /// Contains user parameters
        /// </summary>
        private TArgs _args;

        /// <summary>
        /// Contains results (when run successfully)
        /// </summary>
        private TResults _results;
                              
        /// <summary>
        /// Contains GUID of matrix used (when run)
        /// </summary>
        private Guid _sourceGuid;                       

        public override ArgsBase UntypedArgs => _args;

        /// <summary>
        /// Retrieves the algorithm arguments
        /// </summary>
        [XColumn("Configuration\\", EColumn.Decompose )]
        public TArgs Args
        {
            get
            {
                return _args;
            }
            set
            {
                _args = value;
                ClearResults();
            }
        }

        /// <summary>
        /// Retrieves the algorithm results
        /// </summary>
        [XColumn( "Results\\",EColumn.Decompose )]
        public TResults Results => _results;

        /// <summary>
        /// Retrieves the Error (if Results is null)
        /// </summary>
        [XColumn]
        public override string Error => _error;

        /// <summary>
        /// Retrieves if Dispose has been called
        /// </summary>
        public bool IsDisposed => _isDisposed;

        /// <summary>
        /// Called by the implementing class to register an error
        /// </summary>                                           
        protected void SetError( Exception ex )
        {
            SetError( ex.Message );
        }

        /// <summary>
        /// Called by the implementing class to register an error
        /// </summary>   
        protected void SetError( string error )
        {
            _results = null;
            _error = error;
            _sourceGuid = UntypedArgs.SourceMatrix.Guid;
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public override string DisplayName => Args.DisplayName;

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public override string DefaultDisplayName => Args.DefaultDisplayName;

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public override string OverrideDisplayName { get { return Args.OverrideDisplayName; } set { Args.OverrideDisplayName = value; } }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public override string Comment { get { return Args.Comment; } set { Args.Comment = value; } }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public override bool Hidden { get { return Args.Hidden; } set { Args.Hidden = value; } }

        /// <summary>
        /// Disposes of the configuration
        /// (Not required - for sanity checking only)
        /// </summary>
        public override void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                ClearResults();
            }
        }            

        [XColumn(EColumn.Visible )]
        public EAlgoStatus Status
        {
            get
            {
                if (_results != null)
                {
                    return EAlgoStatus.Success;
                }
                else if (_error != null)
                {
                    return EAlgoStatus.Failed;
                }
                else
                {
                    return EAlgoStatus.Pending;
                }
            }
        }

        public enum EAlgoStatus
        {
            Pending,
            Success,
            Failed,
        }

        /// <summary>
        /// Clears existing results and errors, necessitatioing a recalculation at the next update
        /// </summary>
        public override void ClearResults()
        {
            _results = null;
            _error = null;
            _sourceGuid = new Guid();
        }

        /// <summary>
        /// Called by the derived class to register the results
        /// </summary>                                         
        protected void SetResults( TResults results )
        {
            _results = results;
            _error = null;
            _sourceGuid = Args.SourceMatrix.Guid;
        }

        /// <summary>
        /// Runs the algorithm.
        /// The derived class should perform its calculations and call EITHER <see cref="SetResults"/> OR <see cref="SetError"/>.
        /// </summary>
        public abstract override bool Run( Core core, ProgressReporter prog );
                           
        /// <summary>
        /// Returns if the algorithm completed with an error
        /// </summary>
        public override bool HasError => Error != null;

        /// <summary>
        /// Returns if the algorithm completed successfully
        /// </summary>
        public override bool HasResults => Results != null;
                                  
        /// <summary>
        /// Determines if the configuration needs recalculating, either because it
        /// hasn't ever been calculated, or its inputs have changed since they're creation.
        /// 
        /// The base class takes care of the input matrix, but derived classes should account for
        /// any additional inputs they accept.
        /// 
        /// Changes to actual parameter values result in a call to <see cref="ClearResults"/>
        /// and do NOT need to be accounted for. Only indirect inputs where the target
        /// has since changed need to be accounted for.
        /// </summary>
        public override bool NeedsUpdate
        {
            get
            {
                return Args != null && !Args.Hidden && Args.SourceMatrix != null && _sourceGuid != Args.SourceMatrix.Guid;
            }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
        public override UiControls.ImageListOrder Icon
        {
            get
            {
                if (HasError)
                {
                    return UiControls.ImageListOrder.Warning;
                }
                else if (HasResults)
                {
                    return UiControls.ImageListOrder.TestFull;
                }
                else
                {
                    return UiControls.ImageListOrder.TestEmpty;
                }
            }
        }

        public TAlgo GetAlgorithmOrThrow()
        {
            return (TAlgo)Args.GetAlgorithmOrThrow();
        }
    }
}
