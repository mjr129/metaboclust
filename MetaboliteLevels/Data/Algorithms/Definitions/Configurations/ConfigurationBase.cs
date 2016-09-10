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

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Represents a ready-to-go algorithm, containing an algorithm and its arguments.
    /// See the typed class ConfigurationBase(of TStat, TArgs) for more details.
    /// </summary>
    [Serializable]
    abstract class ConfigurationBase : IDisposable, IVisualisable
    {
        // ID and parameters for algorithm
        public readonly string Id;

        protected ConfigurationBase(string name, string comments, string id)
        {
            Debug.Assert(id != null);

            OverrideDisplayName = name;
            Comment = comments;
            Id = id;
            Enabled = true;
        }

        public virtual bool HasError { get; }

        public abstract bool IsAvailable { get; }
        public abstract string AlgoName { get; }
        public abstract string ArgsToString { get; }
        public abstract bool HasResults { get; }

        private bool _isDisposed;
        public bool IsDisposed => _isDisposed;

        public string Description
        {
            get
            {
                return AlgoName + "; " + ArgsToString;
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                ClearResults();
            }
        }

        public abstract void ClearResults();

        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.FormatDisplayName(this);
            }
        }

        public string DefaultDisplayName { get { return Description; } }

        public string OverrideDisplayName { get; set; }

        public string Comment { get; set; }

        public bool Enabled { get; set; }

        public virtual bool NeedsUpdate { get; }



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

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<ConfigurationBase>> columns = new List<Column<ConfigurationBase>>();

            columns.Add("Name", EColumn.Visible, z => z.DisplayName);
            columns.Add("Comments", EColumn.None, z => z.Comment);
            columns.Add("Enabled", EColumn.None, z => z.Enabled);
            columns.Add("Algorithm\\Name", EColumn.None, z => z.AlgoName);
            columns.Add("Arguments\\Summary", EColumn.None, z => z.ArgsToString);
            columns.Add("Default name", EColumn.None, z => z.DefaultDisplayName);
            columns.Add("Description", EColumn.None, z => z.Description);
            columns.Add("Results\\Error message", EColumn.None, z => z.Error);
            columns.Add("Results\\Has error", EColumn.None, z => z.HasError);
            columns.Add("Results\\Has results", EColumn.None, z => z.HasResults);
            columns.Add("Algorithm\\ID", EColumn.None, z => z.Id);
            columns.Add("Algorithm\\Is available", EColumn.None, z => z.IsAvailable);

            List<Column> allResults = new List<Column>();

            allResults.AddRange(columns);
            allResults.AddRange(GetExtraColumns(core));

            return allResults;
        }

        public abstract string Error { get;  }

        protected abstract IEnumerable<Column> GetExtraColumns(Core core);

        internal abstract bool Run( Core core, ProgressReporter prog );
    }

    /// <summary>
    /// Represents a ready-to-go algorithm, containing an algorithm and its arguments.
    /// 
    /// Algorithms are stored with an ID so they can be retrieved without having to store the actual algorithm with all the user's saved data.
    /// Once retrieved the algorithm itself is remembered only for the lifetime of the program.
    /// </summary>
    /// <typeparam name="TStat">Type of statistic</typeparam>
    /// <typeparam name="TArgs">Type of arguments</typeparam>
    [Serializable]
    abstract class ConfigurationBase<TStat, TArgs, TResults> : ConfigurationBase
        where TStat : AlgoBase
        where TArgs : ArgsBase
        where TResults : ResultBase
    {
        public readonly TArgs Args;

        // Cached algorithm object
        // (we cache this since we only want to store the ID in the save file not the whole algorithm object!)
        [NonSerialized]
        private WeakReference<TStat> _cached;

        [NonSerialized]
        private bool _hasGotCache;

        private TResults _results;

        public TResults Results => _results;

        private Guid _sourceGuid;

        protected ConfigurationBase(string name, string comments, string id, TArgs args)
            : base(name, comments, id)
        {
            Debug.Assert(args != null);

            Args = args;
        }

        public override string Error => _error;
        private string _error;

        public void SetError( Exception ex )
        {
            SetError( ex.Message );
        }

        private void SetError( string error )
        {
            _results = null;
            _error = error;
            _sourceGuid = Args.SourceMatrix.Guid;
        }

        public override void ClearResults()
        {
            _results = null;
            _error = null;
            _sourceGuid = new Guid();
        }

        protected void SetResults(TResults results)
        {                  
            _results = results;
            _error = null;
            _sourceGuid = Args.SourceMatrix.Guid;
        }

        internal abstract override bool Run( Core core, ProgressReporter prog );

        /// <summary>
        /// If the statistic is available to perform calculations.
        /// (false for statistics pointing to missing scripts or those just used to give names to program created values).
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                return TryGetCached() != null;
            }
        }

        /// <summary>
        /// Gets cached algorithm or throws an exception if not found.
        /// </summary>
        public TStat Cached
        {
            get
            {
                TStat r = TryGetCached();

                if (r == null && Id != null)
                {
                    throw new InvalidOperationException(string.Format(
                                 "Attempt to use a non-existent algorithm (ID = \"{0}\"). The algorithm may not have been installed on this computer, or it may have been renamed. Install this algorithm and try again or remove the algorithm from your calculations.",
                                 Id));
                }

                return r;
            }
        }

        /// <summary>
        /// Gets cached algorithm or null if not found.
        /// </summary>
        private TStat TryGetCached()
        {
            if (Id == null)
            {
                return null;
            }

            if (_hasGotCache)
            {
                TStat fromCache;

                if (_cached.TryGetTarget(out fromCache))
                {
                    return fromCache;
                }

                // --> Cache expired - look for it by name again
                _cached = null;
            }

            TStat toCache = (TStat)Algo.Instance.All.Get(Id);

            if (toCache == null)
            {
                return null;
            }

            _cached = new WeakReference<TStat>(toCache);

            _hasGotCache = true;

            return toCache;
        }

        public override bool HasResults
        {
            get { return Results != null; }
        }

        public override string AlgoName
        {
            get
            {
                return IsAvailable ? Cached.Name : Id;
            }
        }

        public override string ArgsToString
        {
            get
            {
                return Args == null ? string.Empty : Args.ToString(TryGetCached());
            }
        }

        public override bool NeedsUpdate
        {
            get { return Enabled && Args.SourceMatrix != null && _sourceGuid != Args.SourceMatrix.Guid; }
        }
    }
}
