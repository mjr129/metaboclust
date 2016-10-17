using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Configurations
{
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
        [XColumn( "Description", EColumn.Visible)]
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

        public EAlgoStatus Status
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
                prog.Log( $"The following algorithm failed to complete: {{{this}}} due to the error {{{ex.Message}}}. Please see the algorithm results for specific details.", ELogLevel.Error );
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
            get { return Args != null && !Args.Hidden && (_tracker == null || _tracker.NeedsUpdate(GetTracker())); }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
        public sealed override Image Icon
        {
            get
            {
                if (HasError)
                {
                    return Resources.MnuWarning;
                }
                else if (HasResults)
                {
                    return ResultIcon;
                }
                else
                {
                    return Resources.ListIconResultPending;
                }
            }
        }

        protected abstract Image ResultIcon { get; }

        public override void GetXColumns( ColumnCollection list, Core core )
        {
            var results = list.Cast<ConfigurationBase<TAlgo, TArgs, TResults, TTracker>>();

            results.Add( "Status", EColumn.Visible, z => z.Status, z =>
                                                                   {
                                                                       switch (z.Status)
                                                                       {
                                                                           case EAlgoStatus.Pending:
                                                                               return Color.Olive;
                                                                           case EAlgoStatus.Completed:
                                                                               return Color.Green;
                                                                           case EAlgoStatus.Failed:
                                                                               return Color.Red;
                                                                           case EAlgoStatus.Disposed:
                                                                               return Color.Magenta;
                                                                           default:
                                                                               throw new ArgumentOutOfRangeException();
                                                                       }
                                                                   } );
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
            this.OverrideDisplayName = data.Pull<string>();
            this.Comment = data.Pull<string>();
        }   
    }
}
