using System;
using System.Collections.Generic;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
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
    internal abstract class ArgsBase : IVisualisable
    {
        /// <summary>
        /// The user-inputtable parameters.
        /// </summary>
        public readonly object[] Parameters;

        private WeakReference<IMatrixProvider> _sourceProvider;

        public IMatrixProvider SourceProvider => _sourceProvider.GetTarget();

        public IntensityMatrix SourceMatrix => SourceProvider?.Provide;

        public string DisplayName => IVisualisableExtensions.FormatDisplayName( this );

        public virtual string DefaultDisplayName
        {
            get
            {
                var algorithm = GetAlgorithmOrNull();

                return (algorithm?.DisplayName ?? Id)
                    + " (" + SourceMatrix.ToStringSafe() + ") "
                    + AlgoParameterCollection.ParamsToHumanReadableString( Parameters, algorithm );
            }
        }

        public string OverrideDisplayName { get; set; }

        public string Comment { get; set; }

        public bool Hidden { get; set; }

        /// <summary>
        /// Constructor
        /// </summary> 
        protected ArgsBase( string id, IMatrixProvider sourceProvider, object[] parameters)
        {
            _sourceProvider = new WeakReference<IMatrixProvider>( sourceProvider );
            Id = id;
            Parameters = parameters;
        }

        public readonly string Id;

        public sealed override string ToString()
        {
            return DisplayName;
        }  

        public UiControls.ImageListOrder GetIcon()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Column> GetColumns( Core core )
        {
            List<Column<ArgsBase>> columns = new List<Column<ArgsBase>>();

            columns.Add( "Name", EColumn.None, z => z.DisplayName );
            columns.Add( "Comments", EColumn.None, z => z.Comment );
            columns.Add( "Parameters", EColumn.None, z => z.Parameters );
            columns.Add( "Hidden", EColumn.Advanced, z => z.Hidden );
            columns.Add( "Algorithm\\Name", EColumn.None, z => z.AlgoName );       
            columns.Add( "Default name", EColumn.None, z => z.DefaultDisplayName );

            List<Column> allResults = new List<Column>();

            allResults.AddRange( columns );
            allResults.AddRange( GetExtraColumns( core ) );

            return allResults;
        }

        protected virtual IEnumerable<Column> GetExtraColumns( Core core )
        {
            return new Column[0];
        }

        /// <summary>
        /// Returns the display name of algorithm, or the ID if not found
        /// </summary>          
        public string AlgoName => GetAlgorithmOrNull()?.DisplayName ?? Id ;

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
}
