using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Corrections;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Evaluation;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Definition;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Utilities;
using MGui.Helpers;
using MSerialisers;
using MSerialisers.Serialisers;

namespace MetaboliteLevels.Data.Session.General
{
    /// <summary>
    /// The session - everything!
    /// 
    /// This is the object that is saved when the user clicks SAVE!
    /// </summary>
    [Serializable]
    class Core
    {
        //
        // SESSION DATA
        //

        /// <summary>
        /// A unique identifier for the session
        /// </summary>
        public readonly Guid CoreGuid;

        /// <summary>
        /// The files we used to generate the session
        /// </summary>
        public DataFileNames FileNames;

        /// <summary>
        /// Session customisation, users love customisation
        /// </summary>
        public readonly CoreOptions Options;

        //
        // MAIN DATA
        //

        /// <summary>
        /// Main data - Intensity matrices
        /// </summary>
        [UndeferSerialisation( typeof( Peak ) )]
        private readonly List<OriginalData> _originalData;

        /// <summary>
        /// Main data - Aliases
        /// </summary>                          
        private readonly List<ProviderAlias> _alises;

        /// <summary>
        /// Main data - the peaks
        /// </summary>
        [UndeferSerialisation( typeof( Peak ) )]
        private readonly List<Peak> _peaks;

        /// <summary>
        /// Main data - the compounds
        /// </summary>
        [UndeferSerialisation( typeof( Compound ) )]
        private readonly List<Compound> _compounds;

        /// <summary>
        /// Main data - pathways
        /// </summary>
        [UndeferSerialisation( typeof( Pathway ) )]
        private readonly List<Pathway> _pathways;

        /// <summary>
        /// Main data - adducts
        /// </summary>
        [UndeferSerialisation( typeof( Adduct ) )]
        private readonly List<Adduct> _adducts;

        /// <summary>
        /// Main data - observations
        /// </summary>
        [UndeferSerialisation( typeof( ObservationInfo ) )]
        private readonly List<ObservationInfo> _observations;

        /// <summary>
        /// Main data - conditions (observations with replicates accounted for)
        /// </summary>
        [UndeferSerialisation( typeof( ObservationInfo ) )]
        private readonly List<ObservationInfo> _conditions;

        /// <summary>
        /// Main data - experimental groups
        /// </summary>
        [UndeferSerialisation( typeof( GroupInfo ) )]
        private readonly List<GroupInfo> _groups;

        /// <summary>
        /// Main data - LC-MS batches
        /// </summary>
        [UndeferSerialisation( typeof( BatchInfo ) )]
        private readonly List<BatchInfo> _batches;

        //
        // META HEADERS
        // We store the header titles only once (here) rather than with each item, because there
        // are thousands of items.
        //

        /// <summary>
        /// Meta data - for _peaks
        /// </summary>
        public readonly MetaInfoHeader _peakMeta;

        /// <summary>
        /// Meta data - for _compounds
        /// </summary>
        public readonly MetaInfoHeader _compoundsMeta;

        /// <summary>
        /// Meta data - for _pathways
        /// </summary>
        public readonly MetaInfoHeader _pathwaysMeta;

        /// <summary>
        /// Meta data - for _adducts
        /// </summary>
        public readonly MetaInfoHeader _adductsMeta;

        /// <summary>
        /// Meta data - for annotations (_peak::annotation and _compound::annotation)
        /// </summary>
        public readonly MetaInfoHeader _annotationsMeta;

        //
        // WORKFLOW
        //                                                         

        /// <summary>
        /// Trend generation functions
        /// (only one of these will be marked "::Enabled" - that's the same one that this::AvgSmoother is set to)
        /// </summary>
        private readonly List<ConfigurationTrend> _trends;

        /// <summary>
        /// Data corrections
        /// </summary>
        private readonly List<ConfigurationCorrection> _corrections;

        /// <summary>
        /// Statistics
        /// </summary>
        private readonly List<ConfigurationStatistic> _statistics;

        /// <summary>
        /// Clusterers
        /// </summary>
        private readonly List<ConfigurationClusterer> _clusterers;

        /// <summary>
        /// Peak filters
        /// </summary>
        private readonly List<PeakFilter> _peakFilters;

        /// <summary>
        /// Observation filters
        /// </summary>
        private readonly List<ObsFilter> _obsFilters;

        //
        // SPECIAL DATA
        //

        /// <summary>
        /// Cached information (not saved, but generated on create/load for speed of access)
        /// </summary>
        [NonSerialized]
        private CachedData _cache;

        /// <summary>
        /// Item GUIDs (used for saving references to items without having to save the items themselves)
        /// </summary>
        private Dictionary<Guid, WeakReference> _guids;

        /// <summary>
        /// Cluster optimisation results
        /// </summary>
        public readonly List<ClusterEvaluationPointer> EvaluationResultFiles = new List<ClusterEvaluationPointer>();

        //
        // ACCESSOR WRAPPERS
        //
        public IReadOnlyList<OriginalData> OriginalData              => this._originalData;
        public IReadOnlyList<int> Reps                               => this._cache._reps;
        public IReadOnlyList<int> Times                              => this._cache._times;
        public IReadOnlyList<GroupInfo> ConditionsOfInterest         => this._cache._conditionsOfInterest;
        public IReadOnlyList<int> Acquisitions                       => this._cache._acquisitions;
        public IReadOnlyList<GroupInfo> ControlConditions            => this._cache._controlConditions;
        public Range TimeRange                                       => this._cache._timeRange;
        public IReadOnlyList<GroupInfo> Groups                       => this._groups;
        public IReadOnlyList<BatchInfo> Batches                      => this._batches;
        public IEnumerable<Cluster> Clusters                         => this._clusterers.WhereEnabled().Where( z => z.Results != null ).SelectMany( z => z.Results.Clusters );
        public IReadOnlyList<Peak> Peaks                             => this._peaks;
        public List<Compound> Compounds                              => this._compounds;
        public IReadOnlyList<Pathway> Pathways                       => this._pathways;
        public List<Adduct> Adducts                                  => this._adducts;
        public IReadOnlyList<ObservationInfo> Observations           => this._observations;
        public IReadOnlyList<ObservationInfo> Conditions             => this._conditions;
        public IReadOnlyList<ConfigurationCorrection> Corrections => this._corrections;
        public IReadOnlyList<ConfigurationStatistic> Statistics   => this._statistics;
        public IReadOnlyList<ConfigurationClusterer> Clusterers   => this._clusterers;
        public IReadOnlyList<ConfigurationTrend> Trends           => this._trends;
        public IReadOnlyList<PeakFilter> PeakFilters              => this._peakFilters;
        public IReadOnlyList<ObsFilter> ObsFilters                => this._obsFilters;

        class CachedData
        {
            public readonly ReadOnlyCollection<int> _times;
            public readonly ReadOnlyCollection<int> _reps;
            public readonly Dictionary<string, GroupInfo> _groupsById;
            public readonly ReadOnlyCollection<GroupInfo> _conditionsOfInterest;
            public readonly ReadOnlyCollection<GroupInfo> _controlConditions;
            public readonly ReadOnlyCollection<int> _acquisitions;
            public readonly Range _timeRange;
            public readonly Range _repRange;

            public CachedData( Core core )
            {
                this._times = new HashSet<int>( core._observations.Select( z => z.Time ) ).ToList().AsReadOnly();
                this._reps = new HashSet<int>( core._observations.Select( z => z.Rep ) ).ToList().AsReadOnly();
                this._acquisitions = new HashSet<int>( core._observations.Select( z => z.Order ) ).ToList().AsReadOnly();

                this._groupsById = new Dictionary<string, GroupInfo>();

                foreach (var t in core._groups)
                {
                    this._groupsById.Add( t.Id, t );
                }

                this._conditionsOfInterest = new List<GroupInfo>( core.FileNames.ConditionsOfInterestString.Select( z => this._groupsById[z] ) ).AsReadOnly();
                this._controlConditions = new List<GroupInfo>( core.FileNames.ControlConditionsString.Select( z => this._groupsById[z] ) ).AsReadOnly();

                this._timeRange = new Range( this._times.Min(), this._times.Max() );
                this._repRange = new Range( this._reps.Min(), this._reps.Max() );
            }
        }

        /// <summary>
        /// Saves all data
        /// </summary>
        public void Save( string fileName, ProgressReporter prog )
        {
            XmlSettings.Save<Core>( fileName, this, null, prog );
        }

        /// <summary>
        /// Loads all data
        /// </summary>
        public static Core Load( string fileName, ProgressReporter progress )
        {
            Core result = XmlSettings.LoadOrDefault<Core>( fileName, null, null, progress );

            if (result != null)
            {
                result.FileNames.Session = fileName;
                result._cache = new CachedData( result );
                result.Options.Core = result;
            }

            return result;
        }

        /// <summary>
        /// Main constructor.
        /// </summary>
        public Core( DataFileNames fileNames, FrmActDataLoad.DataSet data, List<Compound> compounds, List<Pathway> pathways, MetaInfoHeader compMeta, MetaInfoHeader pathMeta, List<Adduct> adducts, MetaInfoHeader adductsHeader, MetaInfoHeader annotationsHeader )
        {
            this.CoreGuid = Guid.NewGuid();

            this.FileNames = fileNames;
            this.Options = new CoreOptions() { Core = this };
            this.Options.ViewTypes = new List<GroupInfo>( data.Types.OrderBy( z => z.DisplayPriority ) );

            this._adducts = adducts;             
            this._peaks = data.Peaks;
            this._compounds = compounds;
            this._pathways = pathways;
            this._observations = data.Observations;
            this._conditions = data.Conditions;
            this._groups = data.Types;
            this._batches = data.Batches;

            this._peakMeta = data.PeakMetaHeader;
            this._compoundsMeta = compMeta;
            this._pathwaysMeta = pathMeta;
            this._adductsMeta = adductsHeader;
            this._annotationsMeta = annotationsHeader;

            this._clusterers = new List<ConfigurationClusterer>();
            this._statistics = new List<ConfigurationStatistic>();
            this._corrections = new List<ConfigurationCorrection>();
            this._trends = new List<ConfigurationTrend>();
            this._peakFilters = new List<PeakFilter>();
            this._obsFilters = new List<ObsFilter>();
            this._alises = new List<ProviderAlias>();

            this._cache = new CachedData( this );

            this._originalData = new List<OriginalData>();
            this._originalData.Add( data.IntensityMatrix );

            if (data.AltIntensityMatrix != null)
            {
                this._originalData.Add( data.AltIntensityMatrix );
            }

            this._alises.Add( new ProviderAlias( this, EProviderAlias.LastCorrection, null ) { OverrideDisplayName = "Latest correction" });
            this._alises.Add( new ProviderAlias( this, EProviderAlias.LastTrend, null ) { OverrideDisplayName = "Latest trend" } );
        }

        private void GenericReplace<T>( List<T> current, IEnumerable<T> replacement, ProgressReporter info )
            where T : ConfigurationBase
        {
            // Clear old values
            foreach (T old in current.Where( z => !replacement.Contains( z ) ))
            {
                old.Dispose();
            }

            current.ReplaceAll( replacement );
        }

        private bool GenericUpdate<T>( string title, List<T> current, ProgressReporter info )
            where T : ConfigurationBase
        {
            bool result = false;

            // Report                        
            using (info.Section( title ))
            {
                foreach (T item in current)
                {
                    if (item.NeedsUpdate)
                    {
                        using (info.Section( item.DisplayName ))
                        {
                            result = true;
                            info.Log( $"Updating: {item.DisplayName}", ELogLevel.Information );
                            item.Run( this, info );
                        }
                    }
                }
            }

            return result;
        }

        private void UpdateAll( ProgressReporter prog )
        {
            bool anyUpdated;

            do
            {
                anyUpdated = false;
                anyUpdated |= this.GenericUpdate( "Updating corrections", this._corrections, prog );
                anyUpdated |= this.GenericUpdate( "Updating trends", this._trends, prog );
                anyUpdated |= this.GenericUpdate( "Updating statistics", this._statistics, prog );
                anyUpdated |= this.GenericUpdate( "Updating clusters", this._clusterers, prog );
            } while (anyUpdated);
        }

        /// <summary>
        /// Sets the correction methods
        /// </summary>              
        internal void SetCorrections( IEnumerable<ConfigurationCorrection> newList, ProgressReporter prog, bool noUpdate )
        {
            this.GenericReplace( this._corrections, newList, prog );

            if (!noUpdate)
            {
                this.UpdateAll( prog );
            }
        }

        /// <summary>
        /// Returns the set of cluster statistics
        /// </summary>                           
        internal HashSet<string> GetClusterStatistics() // TODO: Not very efficient, this should be cached!
        {
            HashSet<string> result = new HashSet<string>();

            foreach (Cluster cluster in this.Clusters)
            {
                foreach (string statistic in cluster.ClusterStatistics.Keys)
                {
                    result.Add( statistic );
                }
            }

            return result;
        }

        /// <summary>
        /// Sets the statistics.
        /// </summary>
        internal void SetStatistics( IEnumerable<ConfigurationStatistic> newList, ProgressReporter prog, bool noUpdate )
        {
            this.GenericReplace( this._statistics, newList, prog );

            if (!noUpdate)
            {
                this.UpdateAll( prog );
            }

            // TODO: x.CalculateAveragedStatistics();
        }

        /// <summary>
        /// Sets the trends.
        /// </summary>
        internal void SetTrends( IEnumerable<ConfigurationTrend> newList, ProgressReporter prog, bool noUpdate )
        {
            this.GenericReplace( this._trends, newList, prog );

            if (!noUpdate)
            {
                this.UpdateAll( prog );
            }
        }

        /// <summary>
        /// Sets clusters and applies clustering algorithm.
        /// </summary>
        public void SetClusterers( IEnumerable<ConfigurationClusterer> newList, ProgressReporter prog, bool noUpdate )
        {
            this.GenericReplace( this._clusterers, newList, prog );

            if (!noUpdate)
            {
                this.UpdateAll( prog );
            }
        }

        /// <summary>
        /// Adds and applies a single new clustering algorithm.
        /// </summary>
        public void AddClusterer( ConfigurationClusterer toAdd, ProgressReporter setProgress )
        {
            List<ConfigurationClusterer> existing = new List<ConfigurationClusterer>( this.Clusterers );
            existing.Add( toAdd );
            this.SetClusterers( existing.ToArray(), setProgress, false );
        }

        /// <summary>
        /// Sets the filters.
        /// Since the filters themselves are immutable we don't need to update e.g. clusters which use them.
        /// </summary>
        internal void SetPeakFilters( IEnumerable<PeakFilter> alist )
        {
            this._peakFilters.ReplaceAll( alist );
        }

        /// <summary>
        /// Sets the filters.
        /// Since the filters themselves are immutable we don't need to update e.g. clusters which use them.
        /// </summary>
        internal void SetObsFilters( IEnumerable<ObsFilter> alist )
        {
            this._obsFilters.ReplaceAll( alist );
        }

        /// <summary>
        /// Adds and a single new observation filter.
        /// </summary>
        internal void AddObsFilter( ObsFilter toAdd )
        {
            List<ObsFilter> existing = new List<ObsFilter>( this.ObsFilters );
            existing.Add( toAdd );
            this.SetObsFilters( existing.ToArray() );
        }

        /// <summary>
        /// Adds and a single new peak filter.
        /// </summary>
        internal void AddPeakFilter( PeakFilter toAdd )
        {
            List<PeakFilter> existing = new List<PeakFilter>( this.PeakFilters );
            existing.Add( toAdd );
            this.SetPeakFilters( existing.ToArray() );
        }

        /// <summary>
        /// All assignments
        /// </summary>
        public IEnumerable<Assignment> Assignments
        {
            get { return this.Clusters.SelectMany( z => z.Assignments.List ); }
        }

        /// <summary>
        /// All annotations
        /// </summary>
        public IEnumerable<Annotation> Annotations
        {
            get { return this.Peaks.SelectMany( z => z.Annotations ); }
        }

        /// <summary>
        /// Gets object GUIDs (prior to partial (de)serialisation)
        /// </summary>                                   
        public LookupByGuidSerialiser GetLookups()
        {
            if (this._guids == null)
            {
                this._guids = new Dictionary<Guid, WeakReference>();
            }

            LookupByGuidSerialiser result = new LookupByGuidSerialiser( this._guids );

            result.Add( typeof( Peak ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( Compound ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( Pathway ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( Adduct ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( PeakFilter ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( ObsFilter ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( ConfigurationCorrection ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( ConfigurationStatistic ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            //result.Add(typeof(ConfigurationClusterer), LookupByGuidSerialiser.ETypeBehaviour.Default); <-- Don't add these because they can be temporary
            result.Add( typeof( ConfigurationTrend ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( GroupInfo ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( ObservationInfo ), LookupByGuidSerialiser.ETypeBehaviour.Default );
            result.Add( typeof( ObservationInfo ), LookupByGuidSerialiser.ETypeBehaviour.Default );

            return result;
        }

        /// <summary>
        /// Retrieves and sets object GUIDs (after partial serialisation)
        /// </summary>                                   
        /// <returns>If anything has changed</returns>
        public bool SetLookups( LookupByGuidSerialiser src )
        {
            if (src.HasLookupTableChanged)
            {
                src.GetLookupTable( this._guids );
                return true;
            }

            return false;
        }

        public ProviderAlias FindMatrixAlias(EProviderAlias source)
        {
            return this._alises.Where( z => z.Source == source ).First();
        }

        public IEnumerable<IMatrixProvider> Matrices
        {
            get
            {
                List<IMatrixProvider> results = new List<IMatrixProvider>();

                foreach (OriginalData matrix in this._originalData)
                {
                    results.Add( matrix );
                }

                foreach (ProviderAlias alias in this._alises)
                {
                    results.Add( alias );
                }

                foreach (ConfigurationTrend trend in this._trends)
                {
                    results.Add( trend );
                }

                foreach (ConfigurationCorrection correction in this._corrections)
                {
                    results.Add( correction );
                }   

                return results;
            }
        }
    }
}
