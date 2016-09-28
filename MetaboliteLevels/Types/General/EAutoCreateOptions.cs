using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Types.General
{
    [Flags]
    enum EAutoCreateOptions
    {
        None,
        TTest = 1,
        Pearson = 2,
        MeanTrend = 4,
        MedianTrend = 8,
        UvScaleAndCentre = 16,
    }

    [Flags]
    public enum EClustererStatistics
    {
        None = 0x00,

        [Name("Cluster averages")]
        [Description("Calculate averages of assignment statistics for clusters")]
        ClusterAverages = 1 << 0,

        [Name("Algorithm averages")]
        [Description("Calculate averages of all assignment statistics")]
        AlgorithmAverages = 1 << 1,

        [Name("Include groups")]
        [Description("Calculate statistics for partial vectors corresponding to each experimental group (slow)")]
        IncludePartialVectorsForGroups = 1 << 2,

        [Name("Include filters")]
        [Description("Calculate statistics for partial vectors corresponding to the currently defined observation filters (very slow)")]
        IncludePartialVectorsForFilters = 1 << 3,

        [Name("Silhouette width")]
        [Description("Calculate the silhouette width for each assignment")]
        SilhouetteWidth = 1 << 4,

        [Name("BIC")]
        [Description("Calculate the bayesian information criterion (BIC) for the algorithm")]
        BayesianInformationCriterion = 1 << 5,

        [Name("Euclidean distances")]
        [Description("Calculate distance from cluster average (Euclidean distance metric)")]
        EuclideanFromAverage = 1 << 6,

        [Name("Selected distance")]
        [Description("Calculate distance from cluster average (selected distance metric)")]
        DistanceFromAverage = 1 << 7,           
    }
}
