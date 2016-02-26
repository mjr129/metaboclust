using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Forms.Startup;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System.Diagnostics;
using MetaboliteLevels.Algorithms.Statistics.Clusterers;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Forms.Algorithms;
using System.Collections;
using MSerialisers;
using MetaboliteLevels.Forms.Algorithms.ClusterEvaluation;
using MSerialisers.Serialisers;

namespace MetaboliteLevels.Data.Session
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
        /// Main data - the peaks
        /// </summary>
        [UndeferSerialisation(typeof(Peak))]
        private readonly List<Peak> _peaks;

        /// <summary>
        /// Main data - the compounds
        /// </summary>
        [UndeferSerialisation(typeof(Compound))]
        private readonly List<Compound> _compounds;

        /// <summary>
        /// Main data - pathways
        /// </summary>
        [UndeferSerialisation(typeof(Pathway))]
        private readonly List<Pathway> _pathways;

        /// <summary>
        /// Main data - adducts
        /// </summary>
        [UndeferSerialisation(typeof(Adduct))]
        private readonly List<Adduct> _adducts;

        /// <summary>
        /// Main data - observations
        /// </summary>
        [UndeferSerialisation(typeof(ObservationInfo))]
        private readonly List<ObservationInfo> _observations;

        /// <summary>
        /// Main data - conditions (observations with replicates accounted for)
        /// </summary>
        [UndeferSerialisation(typeof(ConditionInfo))]
        private readonly List<ConditionInfo> _conditions;

        /// <summary>
        /// Main data - experimental groups
        /// </summary>
        [UndeferSerialisation(typeof(GroupInfo))]
        private readonly List<GroupInfo> _groups;

        /// <summary>
        /// Main data - LC-MS batches
        /// </summary>
        [UndeferSerialisation(typeof(BatchInfo))]
        private readonly List<BatchInfo> _batches;

        /// <summary>
        /// Currently visible clusters
        /// (The complete set of clusters, including disabled ones can be obtained by iterating _clusterers::results)
        /// </summary>
        [UndeferSerialisation(typeof(Cluster))]
        private readonly List<Cluster> _clusters;

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
        /// The trend generation function
        /// </summary>
        public ConfigurationTrend AvgSmoother { get; private set; }

        /// <summary>
        /// A special trend generation function used to produce the "minimum" line on graphs
        /// </summary>
        public ConfigurationTrend MinSmoother { get; private set; }

        /// <summary>
        /// A special trend generation function used to produce the "maximum" line on graphs
        /// </summary>
        public ConfigurationTrend MaxSmoother { get; private set; }

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
        public IReadOnlyList<int> Reps { get { return _cache._reps; } }
        public IReadOnlyList<int> Times { get { return _cache._times; } }
        public IReadOnlyList<GroupInfo> ConditionsOfInterest { get { return _cache._conditionsOfInterest; } }
        public IReadOnlyList<int> Acquisitions { get { return _cache._acquisitions; } }
        public IReadOnlyList<GroupInfo> ControlConditions { get { return _cache._controlConditions; } }
        public Range TimeRange { get { return _cache._timeRange; } }    

        public IReadOnlyList<GroupInfo> Groups { get { return _groups; } }
        public IReadOnlyList<BatchInfo> Batches { get { return _batches; } }
        public IReadOnlyList<Cluster> Clusters { get { return _clusters; } }
        public IReadOnlyList<Peak> Peaks { get { return _peaks; } }
        public IReadOnlyList<Compound> Compounds { get { return _compounds; } }
        public IReadOnlyList<Pathway> Pathways { get { return _pathways; } }
        public IReadOnlyList<Adduct> Adducts { get { return _adducts; } }
        public IReadOnlyList<ObservationInfo> Observations { get { return _observations; } }
        public IReadOnlyList<ConditionInfo> Conditions { get { return _conditions; } }

        public IReadOnlyList<ConfigurationCorrection> AllCorrections { get { return _corrections; } }
        public IReadOnlyList<ConfigurationStatistic> AllStatistics { get { return _statistics; } }
        public IReadOnlyList<ConfigurationClusterer> AllClusterers { get { return _clusterers; } }
        public IReadOnlyList<ConfigurationTrend> AllTrends { get { return _trends; } }
        public IReadOnlyList<PeakFilter> AllPeakFilters { get { return _peakFilters; } }
        public IReadOnlyList<ObsFilter> AllObsFilters { get { return _obsFilters; } }                       

        class CachedData
        {
            public readonly ReadOnlyCollection<int> _times;
            public readonly ReadOnlyCollection<int> _reps;
            public readonly Dictionary<int, GroupInfo> _groupsById;
            public readonly ReadOnlyCollection<GroupInfo> _conditionsOfInterest;
            public readonly ReadOnlyCollection<GroupInfo> _controlConditions;
            public readonly ReadOnlyCollection<int> _acquisitions;
            public readonly Range _timeRange;
            public readonly Range _repRange;

            public CachedData(Core core)
            {
                _times = new HashSet<int>(core._observations.Select(z => z.Time)).ToList().AsReadOnly();
                _reps = new HashSet<int>(core._observations.Select(z => z.Rep)).ToList().AsReadOnly();
                _acquisitions = new HashSet<int>(core._observations.Select(z => z.Acquisition)).ToList().AsReadOnly();

                _groupsById = new Dictionary<int, GroupInfo>();

                foreach (var t in core._groups)
                {
                    _groupsById.Add(t.Id, t);
                }

                _conditionsOfInterest = new List<GroupInfo>(core.FileNames.ConditionsOfInterest.Select(z => _groupsById[z])).AsReadOnly();
                _controlConditions = new List<GroupInfo>(core.FileNames.ControlConditions.Select(z => _groupsById[z])).AsReadOnly();

                _timeRange = new Range(_times.Min(), _times.Max());
                _repRange = new Range(_reps.Min(), _reps.Max());
            }
        }

        /// <summary>
        /// Saves all data
        /// </summary>
        public void Save(string fileName, ProgressReporter prog)
        {
            XmlSettings.SaveToFile<Core>(fileName, this, SerialisationFormat.Infer, null, prog);
        }

        /// <summary>
        /// Loads all data
        /// </summary>
        public static Core Load(string fileName, ProgressReporter progress)
        {
            Core result = XmlSettings.LoadFromFile<Core>(fileName, SerialisationFormat.Infer, progress);

            if (result != null)
            {
                result._cache = new CachedData(result);
            }

            return result;
        }

        /// <summary>
        /// Main constructor.
        /// </summary>
        public Core(DataFileNames fileNames, FrmDataLoad.DataSet data, List<Compound> compounds, List<Pathway> pathways, MetaInfoHeader compMeta, MetaInfoHeader pathMeta, List<Adduct> adducts, MetaInfoHeader adductsHeader, MetaInfoHeader annotationsHeader)
        {
            this.CoreGuid = Guid.NewGuid();

            this.FileNames = fileNames;
            this.Options = new CoreOptions();
            this.Options.ViewTypes = new List<GroupInfo>(data.Types.OrderBy(z => z.Id));

            this._adducts = adducts;
            this._clusters = new List<Cluster>();
            this._peaks = data.Peaks;
            this._compounds = compounds;
            this._pathways = pathways;
            this._observations = data.Observations;
            this._conditions = data.Conditions;
            this._groups = data.Types;
            this._batches = data.Batches;
            _peakMeta = data.PeakMetaHeader;
            _compoundsMeta = compMeta;
            _pathwaysMeta = pathMeta;
            _adductsMeta = adductsHeader;
            _annotationsMeta = annotationsHeader;

            this.AvgSmoother = data.AvgSmoother;
            this.MinSmoother = data.MinSmoother;
            this.MaxSmoother = data.MaxSmoother;

            this._clusterers = new List<ConfigurationClusterer>();
            this._statistics = new List<ConfigurationStatistic>();
            this._corrections = new List<ConfigurationCorrection>();
            this._trends = new List<ConfigurationTrend> { this.AvgSmoother };
            this._peakFilters = new List<PeakFilter>();
            this._obsFilters = new List<ObsFilter>();

            this._cache = new CachedData(this);
        }   

        /// <summary>
        /// Rates the current assignments based on SUM( |x - c(x)|)^2 (the k-means function).
        /// </summary>
        internal void QuantifyAssignments(out bool warning, out double sumPeakClusterScore, out int numPeaks, out double sumPeakClusterScoreSigsOnly, out int numSigPeaks, out double sumWorstTen, out double sumHighestTen, out int numTen, List<Tuple<Peak, double>> allScores)
        {
            sumPeakClusterScore = 0;
            numPeaks = 0;
            sumPeakClusterScoreSigsOnly = 0;
            numSigPeaks = 0;
            sumHighestTen = 0;
            sumWorstTen = 0;
            numTen = 0;
            warning = false;

            ArgsMetric args = new ArgsMetric(null);
            ConfigurationMetric metric = new ConfigurationMetric("temp", null, Algo.ID_METRIC_EUCLIDEAN, args);

            // Iterate clusters
            foreach (Cluster pat in this.Clusters)
            {
                // Get scores in this cluster
                List<double> scoresInThisCluster = new List<double>();

                // Iterate assignments
                foreach (Assignment assignment in pat.Assignments.List)
                {
                    // Get score for PEAK-CLUSTER
                    if (pat.Centres.Count == 0)
                    {
                        pat.SetCentre(ECentreMode.Average, ECandidateMode.Assignments);
                        warning = true;
                    }

                    double peakClusterScore = pat.CalculateScore(assignment.Vector.Values, EDistanceMode.ClosestCentre, metric);
                    peakClusterScore = peakClusterScore * peakClusterScore;
                    sumPeakClusterScore += peakClusterScore;
                    numPeaks++;

                    allScores.Add(new Tuple<Peak, double>(assignment.Peak, peakClusterScore));

                    if (pat.States != Cluster.EStates.None)
                    {
                        scoresInThisCluster.Add(peakClusterScore);
                        sumPeakClusterScoreSigsOnly += peakClusterScore;
                        numSigPeaks++;
                    }
                }

                scoresInThisCluster.Sort();

                for (int n = 0; n < scoresInThisCluster.Count / 10; n++)
                {
                    sumHighestTen += scoresInThisCluster[n];
                    sumWorstTen += scoresInThisCluster[scoresInThisCluster.Count - n - 1];
                    numTen++;
                }
            }
        }

        /// <summary>
        /// Sets the correction methods
        /// 
        /// Trend         : automatically applied to new correction data
        /// Statistics    : updated
        /// Cluster stats : updated
        /// Clustering    : not changed - must be manually reperformed
        /// </summary>
        internal bool SetCorrections(IEnumerable<ConfigurationCorrection> newList, bool refreshAll, bool updateStatistics, bool updateTrends, bool updateClusters, ProgressReporter reportProgress)
        {
            bool result = true;

            // Report                        
            reportProgress.Enter("Applying corrections...");

            if (newList == null)
            {
                newList = this.AllCorrections;
                UiControls.Assert(refreshAll, "SetCorrections: Why set to the same without refresh?");
            }

            // Get first change
            var newListEnabled = newList.WhereEnabled().ToList();
            int firstChange = refreshAll ? 0 : ArrayHelper.GetIndexOfFirstDifference(_corrections, newListEnabled);

            // Remove old results
            _ClearCorrections(firstChange);

            for (int index = firstChange; index < _corrections.Count; index++)
            {
                _corrections[index].ClearResults();
            }

            List<int> invalidIndices = new List<int>();
            bool isInvalid = false;

            // Add new results
            for (int corIndex = firstChange; corIndex < newListEnabled.Count; corIndex++)
            {
                var algo = newListEnabled[corIndex];

                isInvalid = false;

                try
                {
                    // For each peak
                    for (int peakIndex = 0; peakIndex < Peaks.Count; peakIndex++)
                    {
                        if (reportProgress != null)
                        {
                            reportProgress.SetProgress(peakIndex, Peaks.Count, corIndex - firstChange, newListEnabled.Count);
                        }

                        Peak x = Peaks[peakIndex];
                        PeakValueSet src = (corIndex == 0) ? x.OriginalObservations : x.CorrectionChain[corIndex - 1];
                        double[] corrected = algo.Calculate(this, src.Raw);

                        PeakValueSet corS = new PeakValueSet(this, corrected);
                        x.CorrectionChain.Add(corS);

                        if (corrected.Any(double.IsNaN))
                        {
                            isInvalid = true;
                        }
                    }

                    algo.SetResults(new ResultCorrection());
                }
                catch (Exception ex)
                {
                    InvalidateSubsequentCorrections(newListEnabled, ex, null, corIndex);
                    result = false;
                    isInvalid = false;
                    break;
                }

                if (isInvalid)
                {
                    invalidIndices.Add(corIndex);
                }
            }

            // Check validity
            if (isInvalid)
            {
                result = false;
                InvalidateSubsequentCorrections(newListEnabled, null, "One or more peaks contain NaN/Infinite values that were not remedied by the end of the correction chain. NaN/Infinite values were encountered as a result of this algorithm.", invalidIndices.ToArray());
            }

            // Set final observations
            foreach (var x in Peaks)
            {
                x.Observations = x.CorrectionChain.Count == 0 ? x.OriginalObservations : x.CorrectionChain[x.CorrectionChain.Count - 1];
            }

            // Set result
            this._corrections.ReplaceAll(newList);

            // Update statistics
            if (updateTrends)
            {
                SetTrends(null, true, updateStatistics, updateClusters, reportProgress);
            }
            else
            {
                if (updateStatistics)
                {
                    UiControls.Assert(false, "SetCorrections: Would expect trends to be updated if statistics are.");
                    SetStatistics(null, true, reportProgress);
                }

                if (updateClusters)
                {
                    UiControls.Assert(false, "SetCorrections: No reason to update clusters if trend unchanged.");
                    SetClusterers(null, true, reportProgress);
                }
            }

            reportProgress.Leave();

            return result;
        }

        /// <summary>
        /// Returns the set of cluster statistics
        /// </summary>                           
        internal HashSet<string> GetClusterStatistics() // TODO: Not very efficient, this should be cached!
        {
            HashSet<string> result = new HashSet<string>();

            foreach (Cluster cluster in Clusters)
            {
                foreach (string statistic in cluster.ClusterStatistics.Keys)
                {
                    result.Add(statistic);
                }
            }

            return result;
        }

        private void InvalidateSubsequentCorrections(List<ConfigurationCorrection> list, Exception responsibleError, string responsibleErrorMessage, params int[] invalidIndices)
        {
            int firstInvalidIndex = invalidIndices[0];

            // Clear everything after from peaks
            _ClearCorrections(firstInvalidIndex);

            // Disable everything after
            for (int ci2 = firstInvalidIndex; ci2 < list.Count; ci2++)
            {
                list[ci2].Enabled = false;
                list[ci2].SetError("Disabled due to an error in a previous correction.");
            }

            foreach (int invalidIndex in invalidIndices)
            {
                var responsibleCorrection = list[invalidIndex];

                if (responsibleError != null)
                {
                    responsibleCorrection.SetError(responsibleError);
                }
                else if (responsibleErrorMessage != null)
                {
                    responsibleCorrection.SetError(responsibleErrorMessage);
                }
                else
                {
                    throw new ArgumentException("Missing an argument.");
                }
            }

            list.RemoveRange(firstInvalidIndex, list.Count - firstInvalidIndex);
        }

        /// <summary>
        /// Clears corrections from results (after [firstChange]).
        /// </summary>                                                                               
        private void _ClearCorrections(int firstChange)
        {
            foreach (var x in Peaks)
            {
                if (x.CorrectionChain.Count > (firstChange - 1))
                {
                    x.CorrectionChain.RemoveRange(firstChange, x.CorrectionChain.Count - firstChange);
                }
            }
        }

        /// <summary>
        /// Sets the statistics
        /// Since the statistics themselves are immutable we don't need to update e.g. clusters which use them as filters.
        /// 
        /// Trend         : don't use statistics (ignored)
        /// Corrections   : don't use statistics (ignored)
        /// Cluster stats : updated
        /// Clustering    : don't use statistics (ignored)
        /// </summary>
        internal bool SetStatistics(IEnumerable<ConfigurationStatistic> newList, bool refreshAll, ProgressReporter reportProgress)
        {
            bool result = true;

            // Report               
            reportProgress.Enter("Calculating statistics...");

            if (newList == null)
            {
                newList = this._statistics;
                UiControls.Assert(refreshAll, "SetStatistics: Expected refresh if unchanged");
            }

            // Get changes
            var newListEnabled = newList.WhereEnabled().ToList();
            int firstChange = refreshAll ? 0 : ArrayHelper.GetIndexOfFirstDifference(this._statistics, newListEnabled);

            // Remove old results
            for (int i = firstChange; i < this._statistics.Count; i++)
            {
                var stat = this._statistics[i];

                _ClearStatistic(stat);
            }

            // Add new results
            var actList = newListEnabled.ToList();

            for (int statIndex = firstChange; statIndex < newListEnabled.Count; statIndex++)
            {
                var stat = newListEnabled[statIndex];
                Debug.WriteLine("Adding statistic: " + stat.ToString());
                double max = double.MinValue;
                double min = double.MaxValue;

                try
                {
                    for (int peakIndex = 0; peakIndex < Peaks.Count; peakIndex++)
                    {
                        reportProgress.SetProgress(peakIndex, Peaks.Count, statIndex - firstChange, newListEnabled.Count - firstChange);

                        Peak x = Peaks[peakIndex];
                        double value = stat.Calculate(this, x);
                        max = Math.Max(max, value);
                        min = Math.Max(min, value);
                        x.Statistics[stat] = value;
                    }

                    stat.SetResults(new ResultStatistic(min, max));
                }
                catch (Exception ex)
                {
                    result = false;
                    actList.Remove(stat);
                    _ClearStatistic(stat);
                    stat.SetError(ex);
                }
            }

            // Set result                         
            this._statistics.ReplaceAll(newList);

            // Update clusters
            foreach (var x in Clusters)
            {
                x.CalculateAveragedStatistics();
            }

            reportProgress.Leave();

            return result;
        }

        private void _ClearStatistic(ConfigurationStatistic stat)
        {
            foreach (var x in Peaks)
            {
                x.Statistics.Remove(stat);
            }

            stat.Enabled = false;
            stat.ClearError();
            stat.ClearResults();
        }

        /// <summary>
        /// Sets the trend.
        /// 
        /// Statistics    : updated accordingly.
        /// Corrections   : don't use the trend (ignored)
        /// Cluster stats : updated with statistics
        /// Clustering    : not changed - must be manually reperformed
        /// </summary>
        internal bool SetTrends(IEnumerable<ConfigurationTrend> newList, bool refreshAll, bool updateStatistics, bool updateClusters, ProgressReporter info)
        {
            bool result = true;

            // Report
            info.Enter("Calculating trends...");

            if (newList == null)
            {
                newList = _trends;
                UiControls.Assert(refreshAll, "SetTrend: Expected refresh if unchanged");
            }

            // Get changes
            var newListEnabled = newList.WhereEnabled().ToList();

            // Validate
            int numberEnabled = newListEnabled.Count();

            if (numberEnabled != 1)
            {
                throw new InvalidOperationException("SetTrend: Provided with " + numberEnabled + " enabled trends (expected 1).");
            }

            // Set result
            var trend = newListEnabled.First();

            try
            {
                _ApplyTrend(info, trend);
            }
            catch (Exception ex)
            {
                result = false;
                trend.SetError(ex);
                trend.Enabled = false;
                trend = CreateFallbackTrend();

                newListEnabled = new List<ConfigurationTrend> { trend };
                newList = newList.Concat(newListEnabled);

                _ApplyTrend(info, trend);
            }

            // Set lists
            this.AvgSmoother = trend;
            _trends.ReplaceAll(newList);

            // Update statistics
            if (updateStatistics)
            {
                SetStatistics(null, true, info);
            }

            if (updateClusters)
            {
                SetClusterers(null, true, info);
            }

            info.Leave();

            return result;
        }

        private ConfigurationTrend CreateFallbackTrend()
        {
            return new ConfigurationTrend(null, null, Algo.ID_TREND_FLAT_MEAN, new ArgsTrend(null));
        }

        private void _ApplyTrend(ProgressReporter info, ConfigurationTrend trend)
        {
            for (int index = 0; index < Peaks.Count; index++)
            {
                info.SetProgress(index, Peaks.Count);

                Peak p = Peaks[index];

                // Apply new trend
                p.Observations.Trend = trend.CreateTrend(Observations, Conditions, Groups, p.Observations.Raw); // obs

                if (p.AltObservations != null)
                {
                    p.AltObservations.Trend = trend.CreateTrend(Observations, Conditions, Groups, p.AltObservations.Raw); // alt. obs
                }

                for (int i = 0; i < p.CorrectionChain.Count; i++)
                {
                    p.CorrectionChain[i].Trend = trend.CreateTrend(Observations, Conditions, Groups, p.CorrectionChain[i].Raw); // chain obs
                }
            }

            trend.SetResults(new ResultTrend(Peaks.Count));
        }

        /// <summary>
        /// Sets clusters and applies clustering algorithm.
        /// </summary>
        public bool SetClusterers(IEnumerable<ConfigurationClusterer> newList, bool refreshAll, ProgressReporter reportProgress)
        {
            bool result = true;

            // Report                        
            reportProgress.Enter("Calculating clusters...");

            if (newList == null)
            {
                newList = this._clusterers;
                UiControls.Assert(refreshAll, "SetClusterers: Expected refresh if unchanged");
            }

            // Get changes
            var newListEnabled = newList.WhereEnabled().ToList();

            // Remove obsolete clusters
            foreach (ConfigurationClusterer config in IVisualisableExtensions.WhereEnabled(this.AllClusterers))
            {
                if (!newListEnabled.Contains(config))
                {
                    this._ClearCluster(config);
                }
            }

            // Create new clusters
            foreach (ConfigurationClusterer config in newListEnabled.ToList())
            {
                if (!IVisualisableExtensions.WhereEnabled(this.AllClusterers).Contains(config))
                {
                    try
                    {
                        ResultClusterer results = config.Cluster(this, -1, reportProgress);
                        config.SetResults(results);
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        this._ClearCluster(config);
                        config.SetError(ex);
                        newListEnabled.Remove(config);
                        continue;
                    }
                }
            }

            // Set new ones                       
            this._clusterers.ReplaceAll(newList);

            // Set the enabled clusters
            this._clusters.Clear();

            foreach (Peak peak in Peaks)
            {
                peak.Assignments.ClearAll();
            }

            foreach (ConfigurationClusterer config in _clusterers)
            {
                if (config.HasResults)
                {
                    this._clusters.AddRange(config.Results.Clusters);

                    // ...to Peaks
                    foreach (Cluster cluster in config.Results.Clusters)
                    {
                        foreach (var a in cluster.Assignments.List)
                        {
                            a.Peak.Assignments.Add(a);
                        }
                    }
                }
            }

            reportProgress.Leave();

            return result;
        }

        private void _ClearCluster(ConfigurationClusterer config)
        {
            config.Enabled = false;
            config.ClearResults();
            config.ClearError();
        }

        /// <summary>
        /// Adds and applies a single new clustering algorithm.
        /// </summary>
        public void AddClusterer(ConfigurationClusterer toAdd, ProgressReporter setProgress)
        {
            List<ConfigurationClusterer> existing = new List<ConfigurationClusterer>(AllClusterers);
            existing.Add(toAdd);
            this.SetClusterers(existing.ToArray(), false, setProgress);
        }

        /// <summary>
        /// Sets the filters.
        /// Since the filters themselves are immutable we don't need to update e.g. clusters which use them.
        /// </summary>
        internal void SetPeakFilters(IEnumerable<PeakFilter> alist)
        {
            this._peakFilters.ReplaceAll(alist);
        }

        /// <summary>
        /// Sets the filters.
        /// Since the filters themselves are immutable we don't need to update e.g. clusters which use them.
        /// </summary>
        internal void SetObsFilters(IEnumerable<ObsFilter> alist)
        {
            this._obsFilters.ReplaceAll(alist);
        }

        /// <summary>
        /// Adds and a single new observation filter.
        /// </summary>
        internal void AddObsFilter(ObsFilter toAdd)
        {
            List<ObsFilter> existing = new List<ObsFilter>(AllObsFilters);
            existing.Add(toAdd);
            this.SetObsFilters(existing.ToArray());
        }

        /// <summary>
        /// Adds and a single new peak filter.
        /// </summary>
        internal void AddPeakFilter(PeakFilter toAdd)
        {
            List<PeakFilter> existing = new List<PeakFilter>(AllPeakFilters);
            existing.Add(toAdd);
            this.SetPeakFilters(existing.ToArray());
        }

        /// <summary>
        /// All assignments
        /// </summary>
        public IEnumerable<Assignment> Assignments
        {
            get { return Clusters.SelectMany(z => z.Assignments.List); }
        }

        /// <summary>
        /// All annotations
        /// </summary>
        public IEnumerable<Annotation> Annotations
        {
            get { return Peaks.SelectMany(z => z.Annotations); }
        }

        /// <summary>
        /// Gets object GUIDs (prior to partial (de)serialisation)
        /// </summary>                                   
        public LookupByGuidSerialiser GetLookups()
        {
            if (_guids == null)
            {
                _guids = new Dictionary<Guid, WeakReference>();
            }

            LookupByGuidSerialiser result = new LookupByGuidSerialiser(_guids);

            result.Add(typeof(Peak), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(Compound), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(Pathway), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(Adduct), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(PeakFilter), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(ObsFilter), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(ConfigurationCorrection), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(ConfigurationStatistic), LookupByGuidSerialiser.ETypeBehaviour.Default);
            //result.Add(typeof(ConfigurationClusterer), LookupByGuidSerialiser.ETypeBehaviour.Default); <-- Don't add these because they can be temporary
            result.Add(typeof(ConfigurationTrend), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(GroupInfo), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(ObservationInfo), LookupByGuidSerialiser.ETypeBehaviour.Default);
            result.Add(typeof(ConditionInfo), LookupByGuidSerialiser.ETypeBehaviour.Default);

            return result;
        }

        /// <summary>
        /// Retrieves and sets object GUIDs (after partial serialisation)
        /// </summary>                                   
        /// <returns>If anything has changed</returns>
        public bool SetLookups(LookupByGuidSerialiser src)
        {
            if (src.HasLookupTableChanged)
            {
                src.GetLookupTable(_guids);
                return true;
            }

            return false;
        }
    }
}
