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
    /// Represents a configurared algorithm.                                                   
    /// </summary>
    /// <typeparam name="TStat">Type of statistic</typeparam>
    /// <typeparam name="TArgs">Type of arguments</typeparam>
    /// <typeparam name="TResults">Type of results</typeparam>
    [Serializable]
    internal abstract class ConfigurationBase<TStat, TArgs, TResults> : IConfigurationBase
        where TStat : AlgoBase
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

        /// <summary>
        /// Backing field
        /// </summary>
        public object[] Parameters { get; private set; }

        /// <summary>
        /// Retrieves the algorithm arguments
        /// </summary>
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
        public TResults Results => _results;

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => DisplayName;

        /// <summary>
        /// Retrieves the Error (if Results is null)
        /// </summary>
        public string Error => _error;

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
            _sourceGuid = Args.SourceMatrix.Guid;
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public string DisplayName => IVisualisableExtensions.FormatDisplayName( this );

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public string DefaultDisplayName => AlgoName + "; " + ArgsToString;

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Disposes of the configuration
        /// (Not required - for sanity checking only)
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                ClearResults();
            }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
        IEnumerable<Column> IVisualisable.GetColumns( Core core )
        {
            List<Column<IConfigurationBase>> columns = new List<Column<IConfigurationBase>>();

            columns.Add( "Name", EColumn.Visible, z => z.DisplayName );
            columns.Add( "Comments", EColumn.None, z => z.Comment );
            columns.Add( "Hidden", EColumn.None, z => z.Hidden );
            columns.Add( "Algorithm\\Name", EColumn.None, z => z.AlgoName );
            columns.Add( "Arguments\\Summary", EColumn.None, z => z.ArgsToString );
            columns.Add( "Default name", EColumn.None, z => z.DefaultDisplayName );
            columns.Add( "Results\\Error message", EColumn.None, z => z.Error );
            columns.Add( "Results\\Has error", EColumn.None, z => z.HasError );
            columns.Add( "Results\\Has results", EColumn.None, z => z.HasResults );

            List<Column> allResults = new List<Column>();

            allResults.AddRange( columns );
            allResults.AddRange( GetExtraColumns( core ) );

            return allResults;
        }

        /// <summary>
        /// Imlplemented by the derived class to supplement <see cref="GetColumns"/>.
        /// </summary>                                                               
        protected virtual IEnumerable<Column> GetExtraColumns( Core core )
        {
            // NA
            return new Column[0];
        }

        /// <summary>
        /// Clears existing results and errors, necessitatioing a recalculation at the next update
        /// </summary>
        public void ClearResults()
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
        public abstract bool Run( Core core, ProgressReporter prog );

        /// <summary>
        /// Checks if the algorithm is available.
        /// </summary>
        /// <returns></returns>
        public bool CheckIsAvailable()
        {
            return GetAlgorithm() != null;
        }

        /// <summary>
        /// Gets cached algorithm or throws an exception if not found.
        /// </summary>
        public TStat GetAlgorithm() => (TStat)Algo.Instance.All.Get( Args.Id );

        /// <summary>
        /// Returns if the algorithm completed with an error
        /// </summary>
        public bool HasError => Error != null;

        /// <summary>
        /// Returns if the algorithm completed successfully
        /// </summary>
        public bool HasResults => Results != null;

        /// <summary>
        /// Returns the display name of algorithm
        /// </summary>          
        public string AlgoName
        {
            get
            {
                var alg = GetAlgorithm();
                return alg != null ? alg.DisplayName : Args.Id;
            }
        }

        /// <summary>
        /// Returns the 
        /// </summary>
        public string ArgsToString => Args == null ? string.Empty : Args.ToString( GetAlgorithm() );

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
        public virtual bool NeedsUpdate
        {
            get
            {
                return !Hidden && Args.SourceMatrix != null && _sourceGuid != Args.SourceMatrix.Guid;
            }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
        UiControls.ImageListOrder IVisualisable.GetIcon()
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
}
