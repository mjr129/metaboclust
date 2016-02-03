using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Represents a ready-to-go algorithm, containing an algorithm and its arguments.
    /// See the typed class ConfigurationBase(of TStat, TArgs) for more details.
    /// </summary>
    [Serializable]
    abstract class ConfigurationBase : IVisualisable
    {
        // Name/Comments are for info and can be changed without having to change the object and recalculate the statistic
        public string Error { get; protected set; }

        // ID and parameters for algorithm
        public readonly string Id;

        protected ConfigurationBase(string name, string comments, string id)
        {
            OverrideDisplayName = name;
            Comment = comments;
            Id = id;
            Enabled = true;
        }

        public bool HasError
        {
            get { return Error != null; }
        }

        public abstract bool IsAvailable { get; }
        public abstract string AlgoName { get; }
        public abstract string ArgsToString { get; }
        public abstract bool HasResults { get; }

        VisualClass IVisualisable.VisualClass { get { return VisualClass.None; } }

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

        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.GetDisplayName(OverrideDisplayName, DefaultDisplayName);
            }
        }

        public string DefaultDisplayName { get { return Description; } }

        public string OverrideDisplayName { get; set; }

        public string Comment { get; set; }

        public bool Enabled { get; set; }

        public abstract void ClearResults();

        public void SetError(Exception ex)
        {
            SetError(ex.Message);
        }

        public void SetError(string error)
        {
            ClearResults();
            Error = error;
        }

        public void ClearError()
        {
            Error = null;
        }

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

            return columns;
        }

        void IVisualisable.RequestContents(ContentsRequest list)
        {
            // NA
        }
    }

    internal static class ConfigurationBaseExtensions
    {
        public static IEnumerable<T> WhereEnabled<T>(this IEnumerable<T> self)
            where T : ConfigurationBase
        {
            return self.Where(z => z.Enabled);
        }
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

        public TResults Results { get; protected set; }

        protected ConfigurationBase(string name, string comments, string id, TArgs args)
            : base(name, comments, id)
        {
            Args = args;
        }

        public override void ClearResults()
        {
            SetResults(null);
        }

        public void SetResults(TResults results)
        {
            ClearError();
            Results = results;
        }

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

                if (r == null)
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
    }
}
