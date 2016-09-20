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
    /// Used to track data used by a configuration.
    /// When inputs change this class detects that an update is required.
    /// </summary>
    internal class SourceTracker
    {
        /// <summary>
        /// Tracks <see cref="ArgsBase.SourceMatrix"/>
        /// </summary>
        private Guid _argsBase_SourceMatrix;

        /// <summary>
        /// CONSTRUCTOR
        /// Remembers the data specified by <paramref name="source"/>.
        /// </summary>    
        public SourceTracker( ArgsBase source )
        {
            _argsBase_SourceMatrix = source.SourceMatrix.Guid;
        }

        /// <summary>
        /// Checks for update.
        /// </summary>
        /// <param name="source">Should be the same object this was constructed with</param>
        /// <returns>true if an update is required, otherwise false.</returns>
        public bool NeedsUpdate( ArgsBase source )
        {
            if (_argsBase_SourceMatrix != source.SourceMatrix.Guid)
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Represents a configurared algorithm, containing the parameters and results.
    /// 
    /// This is a mutable object and can be modified by the user by changing the arguments.
    /// The results are discarded upon modification.
    /// 
    /// The arguments comprise an immutable object with the intention of all changes going through ConfigurationBase.  
    /// </summary>
    /// <typeparam name="TAlgo">Type of statistic</typeparam>
    /// <typeparam name="TArgs">Type of arguments</typeparam>
    /// <typeparam name="TResults">Type of results</typeparam>
    /// <typeparam name="TTracker">Type of source tracker</typeparam>
    [Serializable]
    internal abstract class ConfigurationBase<TAlgo, TArgs, TResults, TTracker> : ConfigurationBase, IBackup, IConfigurationBase<TArgs>
        where TAlgo : AlgoBase
        where TArgs : ArgsBase, IArgsBase<TAlgo>
        where TResults : ResultBase
        where TTracker : SourceTracker
    {
        /// <summary>
        /// Contains user parameters
        /// </summary>
        private TArgs _args;

        /// <summary>
        /// Flagged if the algorithm is disposed
        /// </summary>
        [NonSerialized] private bool _isDisposed;

        /// <summary>
        /// Contains error text (when run unsuccessfully)
        /// </summary>
        private string _error;

        /// <summary>
        /// Contains results (when run successfully)
        /// </summary>
        private TResults _results;

        /// <summary>
        /// The data with which the results were generated (when run)
        /// </summary>
        private TTracker _tracker;

        public override ArgsBase UntypedArgs => _args;

        /// <summary>
        /// Retrieves the algorithm arguments
        /// </summary>
        [XColumn( "Configuration\\", EColumn.Decompose )] public TArgs Args
        {
            get { return _args; }
            set
            {
                _args = value;
                ClearResults();
            }
        }

        /// <summary>
        /// Retrieves the algorithm results
        /// </summary>
        [XColumn( "Results\\", EColumn.Decompose )] public TResults Results => _results;

        /// <summary>
        /// Retrieves the Error (if Results is null)
        /// </summary>
        [XColumn] public sealed override string Error => _error;

        /// <summary>
        /// Retrieves if Dispose has been called
        /// </summary>
        public bool IsDisposed => _isDisposed;

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public sealed override string DisplayName => Args.DisplayName;

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public sealed override string DefaultDisplayName => Args.DefaultDisplayName;

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public sealed override string OverrideDisplayName
        {
            get { return Args.OverrideDisplayName; }
            set { Args.OverrideDisplayName = value; }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public sealed override string Comment
        {
            get { return Args.Comment; }
            set { Args.Comment = value; }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public sealed override bool Hidden
        {
            get { return Args.Hidden; }
            set { Args.Hidden = value; }
        }

        /// <summary>
        /// Disposes of the configuration
        /// (Not required - for sanity checking only)
        /// </summary>
        public sealed override void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                ClearResults();
            }
        }

        [XColumn( EColumn.Visible )] public EAlgoStatus Status
        {
            get
            {
                if (_isDisposed)
                {
                    return EAlgoStatus.Disposed;
                }
                else if (_results != null)
                {
                    return EAlgoStatus.Completed;
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
            Completed,
            Failed,
            Disposed
        }

        /// <summary>
        /// Clears existing results and errors, necessitatioing a recalculation at the next update
        /// </summary>
        public override void ClearResults()
        {
            _results = null;
            _error = null;
            _tracker = null;
        }

        /// <summary>
        /// Called by the derived class to register the results
        /// </summary>                                         
        protected void SetResults( TResults results )
        {
            if (Status != EAlgoStatus.Pending)
            {
                throw new InvalidOperationException( $"Attempt to call {{{nameof( SetResults )}}} when {{{nameof( Status )}}} is {{{Status}}}." );
            }

            _results = results;
            _error = null;
            _tracker = GetTracker();
        }

        /// <summary>
        /// Called by the derived class to register the results
        /// </summary>                                         
        protected void SetError( Exception error )
        {
            SetError( error.Message );
        }

        /// <summary>
        /// Called by the derived class to register the results
        /// </summary>                                         
        protected void SetError( string errorMessage )
        {
            if (Status != EAlgoStatus.Pending)
            {
                throw new InvalidOperationException( $"Attempt to call {{{nameof( SetError )}}} when {{{nameof( Status )}}} is {{{Status}}}." );
            }

            _results = null;
            _error = errorMessage;
            _tracker = GetTracker();
        }

        /// <summary>
        /// Asks the derived class to remember the data source (for update tracking)
        /// </summary>
        /// <returns></returns>
        protected abstract TTracker GetTracker();

        /// <summary>
        /// Runs the algorithm.
        /// </summary>         
        public sealed override bool Run( Core core, ProgressReporter prog )
        {
            ClearResults();

            try
            {
                OnRun( core, prog );
            }
            catch (Exception ex)
            {
                SetError( ex );
            }

            switch (Status)
            {
                case EAlgoStatus.Completed:
                    return true;
                case EAlgoStatus.Failed:
                    return false;

                default:
                    throw new ArgumentOutOfRangeException($"Expected Status to be Completed or Failed after {{{nameof( OnRun )}}} called, but {{{nameof( Status)}}} is {{{Status}}}.");
            }               
        }

        /// <summary>
        /// Runs the algorithm.
        /// The derived class should perform its calculations and call EITHER <see cref="SetResults"/> OR <see cref="SetError"/>.
        /// </summary>
        protected abstract void OnRun( Core core, ProgressReporter prog );

        /// <summary>
        /// Returns if the algorithm completed with an error
        /// </summary>
        public sealed override bool HasError => Error != null;

        /// <summary>
        /// Returns if the algorithm completed successfully
        /// </summary>
        public sealed override bool HasResults => Results != null;

        /// <summary>
        /// Determines if the configuration needs recalculating, either because it
        /// hasn't ever been calculated, or its inputs have changed since they're creation.   
        /// </summary>
        public sealed override bool NeedsUpdate
        {
            get { return Args != null && !Args.Hidden && (_tracker == null || _tracker.NeedsUpdate( Args )); }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
        public sealed override UiControls.ImageListOrder Icon
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

        void IBackup.Backup( BackupData data )
        {   
            data.Push( this._args );
            data.Push( this._error );
            data.Push( this._isDisposed );
            data.Push( this._results );
            data.Push( this._tracker );
            data.Push( this.OverrideDisplayName );
            data.Push( this.Comment );         
        }

        void IBackup.Restore( BackupData data )
        {
            data.Pull( ref this._args );
            data.Pull( ref this._error );
            data.Pull( ref this._isDisposed );
            data.Pull( ref this._results );
            data.Pull( ref this._tracker );
            this.OverrideDisplayName = data.Pull<string>(  );
            this.Comment = data.Pull<string>( );
        }
    }
}
