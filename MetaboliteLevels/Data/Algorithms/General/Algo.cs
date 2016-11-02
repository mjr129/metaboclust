using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations.Legacy;
using MetaboliteLevels.Data.Algorithms.Definitions.Corrections;
using MetaboliteLevels.Data.Algorithms.Definitions.Corrections.Implementations;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics.Implementations;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Implementations;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends.Implementations;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// Holds the list of all algorithms available to the user.
    /// 
    /// Algorithms are stored with an ID so they can be retrieved without having to store then with all the user's saved data.
    /// </summary>
    class Algo
    {
        // IDs of some algorithms so we can retrieve them elsewhere in the program
        public const string ID_CORRECTION_UV_SCALE_AND_CENTRE = @"UV_SCALE_AND_CENTRE";
        public const string ID_METRIC_EUCLIDEAN = @"EUCLIDEAN";
        public const string ID_METRIC_TTEST = @"T_TEST";
        public const string ID_METRIC_PEARSON = @"PEARSON";
        public const string ID_METRIC_PEARSONDISTANCE = @"PEARSON_DISTANCE";
        public const string ID_TREND_NAN = @"TREND_NAN";
        public const string ID_TREND_FLAT_MEAN = @"FLAT_MEAN";
        public const string ID_TREND_MOVING_MEAN = @"MOVING_MEAN";
        public const string ID_TREND_MOVING_MEDIAN = @"MOVING_MEDIAN";
        public const string ID_TREND_MOVING_MINIMUM = @"MOVING_MIN";
        public const string ID_TREND_MOVING_MAXIMUM = @"MOVING_MAX";
        public const string ID_STATS_MIN = @"STATS_MIN";
        public const string ID_STATS_MAX = @"STATS_MAX";
        public const string ID_STATS_ABSMAX = @"STATS_ABSMAX";
        public const string ID_COMBINE = @"COMBINE";
        public const string ID_KMEANSWIZ = @"KMEANSWIZ";
        public const string ID_DKMEANSPPWIZ = @"DKMEANSPPWIZ";
        public const string ID_PATFROMPATH = @"PATFROMPATH";

        // R scripts for some tests
        private const string SCRIPT_TTEST = @"t.test(a, b)$p.value";
        private const string SCRIPT_PEARSON = @"cor(a, b)";

        // Delegates
        private delegate T Delegate_Constructor<T>(string scriptText, string id, string name, string fileName);

        // Our stores of algorithms, by category
        public readonly AlgoCollection<AlgoBase> All = new AlgoCollection<AlgoBase>();                      // All algorithms
        public readonly AlgoCollection<MetricBase> Metrics = new AlgoCollection<MetricBase>();              // Metrics (statistical algorithms which support quick calculate)
        public readonly AlgoCollection<StatisticBase> Statistics = new AlgoCollection<StatisticBase>();     // Statistical algorithms
        public readonly AlgoCollection<ClustererBase> Clusterers = new AlgoCollection<ClustererBase>();     // Clusterer algorithms
        public readonly AlgoCollection<TrendBase> Trends = new AlgoCollection<TrendBase>();                 // Trend generating algorithms
        public readonly AlgoCollection<CorrectionBase> Corrections = new AlgoCollection<CorrectionBase>();  // Correction algorithms

        // Instance of this class
        public static Algo Instance { get; private set; }

        /// <summary>
        /// Initialises this singleton
        /// </summary>
        public static void Initialise()
        {
            Instance = new Algo();
        }

        /// <summary>
        /// Constructior
        /// </summary>
        private Algo()
        {
            Rebuild();
        }

        /// <summary>
        /// Rebuilds the cache of statistics
        /// </summary>
        internal void Rebuild()
        {
            // Clear dat
            All.Clear();
            Statistics.Clear();
            Trends.Clear();
            Clusterers.Clear();
            Metrics.Clear();
            Corrections.Clear();

            // Metrics
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.Canberra, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.Chebyshev, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.Cosine, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.Euclidean, ID_METRIC_EUCLIDEAN, "Euclidean", true ) );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.Hamming, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.Jaccard, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.MAE, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.Manhattan, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.MSE, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.Pearson, ID_METRIC_PEARSONDISTANCE, "Pearson distance", true ) { Comment = "This is a distance measure equal to 1 - r, where r is the Pearson Correlation Coefficient." } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.SAD, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( MathNet.Numerics.Distance.SSD, true ) { Hidden = true } );
            Statistics.Add( new MetricInbuilt( Maths.Qian, @"QIAN", "Qian", false ) );
            Statistics.Add( new MetricInbuilt( Maths.QianDistance, @"QIAN_DISTANCE", "Qian × -1 (distance)", false ) );

            Statistics.Add( new MetricScript( SCRIPT_TTEST, ID_METRIC_TTEST, "t-test (p)", null ) { Comment = "Conducts a t-test and returns the p-value" } );
            Statistics.Add( new MetricScript( SCRIPT_PEARSON, ID_METRIC_PEARSON, "Pearson (r)", null ) );

            // Statistics
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.InterquartileRange, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.Kurtosis, true ) { Hidden = true } );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.LowerQuartile, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.Maximum, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.Mean, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.Median, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.Minimum, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.RootMeanSquare, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.Skewness, true ) { Hidden = true } );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.StandardDeviation, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.UpperQuartile, true ) );
            Statistics.Add( new StatisticInbuilt( MathNet.Numerics.Statistics.Statistics.Variance, true ) );

            Statistics.Add( new StatisticPcaAnova( @"PCA_ANOVA", "PCA-ANOVA" ) { Hidden = true, Comment = "Constructs a matrix representing a CONDITION and REPLICATE for each row (TIME for each column) and uses PCA to reduce this to 1 dimension. Uses ANOVA to determine if there is a difference between groups of replicates for each CONDITION. It is recommended to constrain this method to CONDITIONS of interest and only use REPLICATES with comparable sets (CONDITIONs and TIMEs) of data since missing values are guessed based on the average of the other replicates." } );

            // Derived statistics
            Statistics.Add( new StatisticConsumer( Maths.Mean, "STATS_MEAN", "*Mean (of other statistics)" ) );
            Statistics.Add( new StatisticConsumer( Maths.Median, "STATS_MEDIAN", "*Median (of other statistics)" ) );
            Statistics.Add( new StatisticConsumer( Maths.AbsMax, ID_STATS_ABSMAX, "*Absolute Maximum (of other statistics)" ) );
            Statistics.Add( new StatisticConsumer( Maths.AbsMin, "STATS_ABSMIN", "*Absolute Minimum (of other statistics)" ) );
            Statistics.Add( new StatisticConsumer( Maths.Max, ID_STATS_MAX, "*Maximum (of other statistics)" ) );
            Statistics.Add( new StatisticConsumer( Maths.Min, ID_STATS_MIN, "*Minimum (of other statistics)" ) );
            Statistics.Add( new StatisticConsumer( Maths.Sum, "STATS_SUM", "*Sum (of other statistics)" ) );
            Statistics.Add( new StatisticConsumer( Maths.NegSum, "STATS_NEGSUM", "*Negative sum (of other statistics)" ) );

            // Trends
            Trends.Add( new TrendFlatLine( z => double.NaN, ID_TREND_NAN, "No trend" ) { Hidden = true } );
            Trends.Add( new TrendFlatLine( Maths.Mean, ID_TREND_FLAT_MEAN, "Straight line across mean" ) );
            Trends.Add( new TrendFlatLine( Maths.Median, "FLAT_MEDIAN", "Straight line across median" ) );

            Trends.Add( new TrendAverage( Maths.Median, ID_TREND_MOVING_MEDIAN, "Moving median" ) { Comment = "Creates a trend representing an average (where the window width is 1) or a moving average of the time-points. This is also suitable for calculating the average of non-time-course data." } );
            Trends.Add( new TrendAverage( Maths.Mean, ID_TREND_MOVING_MEAN, "Moving mean" ) { Comment = "Creates a trend representing an average (where the window width is 1) or a moving average of the time-points. This is also suitable for calculating the average of non-time-course data." } );
            Trends.Add( new TrendAverage( Maths.Min, ID_TREND_MOVING_MINIMUM, "Moving minimum" ) { Hidden = true } );
            Trends.Add( new TrendAverage( Maths.Max, ID_TREND_MOVING_MAXIMUM, "Moving maximum" ) { Hidden = true } );

            // Corrections
            Corrections.Add( new CorrectionScript( @"scale(y)", ID_CORRECTION_UV_SCALE_AND_CENTRE, "UV Scale and centre", null ) );
            Corrections.Add( new CorrectionScript( @"scale(y, center = FALSE)", @"SCALE_NO_C", "UV Scale", null ) );
            Corrections.Add( new CorrectionScript( @"scale(y, scale = FALSE)", @"CENTRE_NO_S", "Center", null ) );
            Corrections.Add( new CorrectionDirtyRectify( @"ZERO_MISSING", "Zero invalid values" ) );

            // Clusterers         
            Clusterers.Add( new ClustererCombine( ID_COMBINE, "Combine clusters" ) { Comment = "Creates one cluster containing all peaks in the input matrix - used for combining clusters or creating clusters from peak filters.", Hidden = true } );
            Clusterers.Add( new LegacyKMeansClusterer( ID_KMEANSWIZ, "k-means (LLoyd algorithm, using random starting centroids)" ) { Hidden = true, Comment = "A version of k-means clustering built into the software. This is a legacy function, use of the equivalent R function is now recommended." } );
            Clusterers.Add( new LegacyDkMeansPpClusterer( ID_DKMEANSPPWIZ, "k-means (LLoyd algorithm, using d-means++ starting centroids)" ) { Hidden = true, Comment = "Invokes the inbuilt k-means clustering algorithm using d-k-means++ starting centres. This algorithm is configured through serval parameters and using the guided wizard provided with the software is recommended." } );
            Clusterers.Add( new ClustererReclusterer( "RECLUSTERER", "k-means (LLoyd algorithm, starting with existing cluster centroids)" ) { Hidden = true, Comment = "Invokes the inbuilt k-means clustering algorithm using user-defined starting centres. These centres are defined by a previous set of clusters." } );
            Clusterers.Add( new LegacyPathwayClusterer( ID_PATFROMPATH, "*Cluster to pathways" ) { Hidden = true, Comment = "Creates clusters based on the pathways in which the peaks' potential metabolites may be involved." } );
            Clusterers.Add( new ClustererExisting( "EXISTING", "*Cluster new vectors based on existing clusters" ) { Hidden = true } );
            Clusterers.Add( new ClustererUniqueness( "UNIQCLUST", "*Cluster new vectors based on existing cluster combinations" ) { Hidden = true } );
            Clusterers.Add( new ClustererAffinityPropagation( "AFFINITY", "Affinity propagation (inbuilt)" ) { Hidden = true, Comment = "Clusters using Affinity Propogation. This is a legacy function, use of the equivalent R function is now recommended." } );

            // From files
            PopulateFiles( Statistics, UiControls.EInitialFolder.FOLDER_STATISTICS, ( txt, id, name, fn ) => new StatisticScript( txt, id, name, fn ) );
            PopulateFiles( Statistics, UiControls.EInitialFolder.FOLDER_METRICS, ( txt, id, name, fn ) => new MetricScript( txt, id, name, fn ) );
            PopulateFiles( Trends, UiControls.EInitialFolder.FOLDER_TRENDS, ( txt, id, name, fn ) => new TrendScript( txt, id, name, fn ) );
            PopulateFiles( Clusterers, UiControls.EInitialFolder.FOLDER_CLUSTERERS, ( txt, id, name, fn ) => new ClustererScript( txt, id, name, fn ) );
            PopulateFiles( Corrections, UiControls.EInitialFolder.FOLDER_CORRECTIONS, ( txt, id, name, fn ) => new CorrectionScript( txt, id, name, fn ) );

            // Derivative collections
            Metrics.AddRange( Statistics.Where( z => z.IsMetric && ((MetricBase)z).SupportsQuickCalculate ).Cast<MetricBase>() );
            All.AddRange( Statistics );
            All.AddRange( Trends );
            All.AddRange( Clusterers );
            All.AddRange( Corrections );
        }

        /// <summary>
        /// Adds the algorithms stored on disk and in the SCRIPTS.RESX file.
        /// </summary>                         
        private static void PopulateFiles<T>(AlgoCollection<T> targetCollection, UiControls.EInitialFolder searchFolder, Delegate_Constructor<T> constructorMethod)
                    where T : AlgoBase
        {
            string folder = UiControls.GetOrCreateFixedFolder( searchFolder );
            string resourcePrefix = "scripts~" + Path.GetFileName( folder ).ToLower() + "~";

            ResourceSet resources = UiControls.GetScriptsResources();

            // Search Scripts.resx
            foreach (DictionaryEntry resource in resources)
            {
                string key = (string)resource.Key;

                if (key.StartsWith( resourcePrefix ))
                {
                    string subPart = key.Substring( resourcePrefix.Length );
                    string id = GetId( searchFolder, subPart, true );
                    targetCollection.Add( constructorMethod( Encoding.UTF8.GetString( (byte[])resource.Value ), id, subPart.Replace( "_", " " ), null ) );
                }
            }

            // Search folder
            foreach (string fileName in Directory.GetFiles( folder, "*.r" ))
            {
                string id = GetId( searchFolder, fileName, false );
                string name = Path.GetFileName( fileName );
                targetCollection.Add( constructorMethod( File.ReadAllText( fileName ), id, name, fileName ) );
            }
        }

        /// <summary>
        /// Retrieves/generates the ID for a script based algorithm loaded from disk.
        /// </summary>
        /// <param name="folder">Folder the script is in (one of the FOLDER_* constants)</param>
        /// <param name="fileName">Full or partial filename of the script</param>
        /// <param name="isInternal">Is internal (part of Scripts.resx)</param>
        /// <returns>The ID</returns>
        public static string GetId(UiControls.EInitialFolder folder, string fileName, bool isInternal)
        {
            if (isInternal)
            {
                return "RES:" + folder.ToUiString().ToUpper() + "\\" + Path.GetFileNameWithoutExtension( fileName );
            }
            else
            {
                return "FILE:" + folder.ToUiString().ToUpper() + "\\" + Path.GetFileNameWithoutExtension( fileName );
            }
        }

        /// <summary>
        /// Gets a filename from an ID.
        /// </summary>                 
        public static string GetFileName(string id)
        {
            if (id.StartsWith( "FILE:" ))
            {
                return Path.Combine(UiControls.StartupPath, id.Substring(7) + ".r");
            }

            return null;
        }
    }
}
