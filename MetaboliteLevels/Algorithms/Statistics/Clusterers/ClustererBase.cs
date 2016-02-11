using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers
{
    /// <summary>
    /// Clustering algorithm base class.
    /// </summary>
    abstract class ClustererBase : AlgoBase
    {
        protected ClustererBase(string id, string name)
            : base(id, name)
        {
            // NA
        }

        /// <summary>
        /// Designates support for observation input filters (e.g. "control, days 5-12" only).
        /// Most algorithms will support filters on the input vectors unless they themselves need to perform internal
        /// filtering of the input.
        ///</summary>
        public abstract bool SupportsObservationFilters { get; }

        /// <summary>
        /// For ClusterBase derivatives.
        /// Designates support for distance metrics.
        /// </summary>
        public abstract bool SupportsDistanceMetrics { get; }

        /// <summary>
        /// For ClusterBase derivatives.
        /// Designates requirement of a distance metrics.
        /// </summary>
        public abstract bool RequiresDistanceMatrix { get; }

        public ResultClusterer ExecuteAlgorithm(Core core, int isPreview, bool doNotCluster, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog, out ValueMatrix vmatrixOut, out DistanceMatrix dmatrixOut)
        {
            IReadOnlyList<Peak> peaks;

            if (isPreview > 0 && isPreview < core.Peaks.Count)
            {
                List<Peak> p = core.Peaks.ToList();
                p.Shuffle();

                p = p.GetRange(0, Math.Min(isPreview, p.Count)).ToList();

                // Make sure any seed peaks are in the list
                foreach (Peak peak in tag.Args.Parameters.OfType<WeakReference<Peak>>().Select(par => (par).GetTargetOrThrow()))
                {
                    p.Insert(0, peak);
                    p.RemoveAt(p.Count - 1);
                }

                peaks = p;
            }
            else
            {
                peaks = core.Peaks;
            }

            // FILTER PEAKS
            PeakFilter pfilter = args.PeakFilter ?? Settings.PeakFilter.Empty;

            Filter<Peak>.Results filter = pfilter.Test(peaks);
            Cluster insigs;

            if (filter.Failed.Count == 0)
            {
                insigs = null;
            }
            else
            {
                insigs = new Cluster("Insig", tag);
                insigs.States |= Data.Visualisables.Cluster.EStates.Insignificants;

                // We still need the vmatrix for plotting later
                ValueMatrix insigvMatrix = ValueMatrix.Create(filter.Failed, args.SourceMode == EAlgoSourceMode.Trend, core, args.ObsFilter, false, prog);

                for (int index = 0; index < insigvMatrix.NumVectors; index++)
                {
                    Vector p = insigvMatrix.Vectors[index];
                    insigs.Assignments.Add(new Assignment(p, insigs, double.NaN));
                }                      
            }

            // CREATE VMATRIX AND FILTER OBSERVATIONS
            bool useTrend = args.SourceMode != EAlgoSourceMode.Full;

            prog.Enter("Creating value matrix");
            ValueMatrix vmatrix = ValueMatrix.Create(filter.Passed, useTrend, core, args.ObsFilter, args.SplitGroups, prog);
            prog.Leave();

            prog.Enter("Creating distance matrix");
            DistanceMatrix dmatrix = RequiresDistanceMatrix ? DistanceMatrix.Create(core, vmatrix, args.Distance, prog) : null;
            prog.Leave();
            IEnumerable<Cluster> clusters;

            if (doNotCluster)
            {
                vmatrixOut = vmatrix;
                dmatrixOut = dmatrix;
                return null;
            }

            // CLUSTER USING VMATRIX OR DMATRIX
            prog.Enter("Clustering");
            clusters = Cluster(vmatrix, dmatrix, args, tag, prog);
            prog.Leave();

            vmatrixOut = vmatrix;
            dmatrixOut = dmatrix;

            List<Cluster> result = new List<Cluster>();
            result.Add(insigs);
            result.AddRange(clusters);
            return new ResultClusterer(result);
        }

        /// <summary>
        /// Clustering
        /// 
        /// If the cluster does't make use of the distance matrix OR the distance metric it should flag itself with DoesNotSupportDistanceMetrics.
        /// </summary>
        protected abstract IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog);

        protected static IEnumerable<Cluster> CreateClustersFromIntegers(ValueMatrix vmatrix, IList<int> clusters, ConfigurationClusterer tag)
        {
            Dictionary<int, Cluster> pats = new Dictionary<int, Cluster>();
            List<Cluster> r = new List<Cluster>();

            for (int n = 0; n < vmatrix.NumVectors; n++)
            {
                Vector p = vmatrix.Vectors[n];

                int c = clusters != null ? clusters[n] : 0;
                Cluster pat;

                if (!pats.TryGetValue(c, out pat))
                {
                    pat = new Cluster((pats.Count + 1).ToString(), tag);
                    pats.Add(c, pat);
                    r.Add(pat);
                }

                pat.Assignments.Add(new Assignment(p, pat, double.NaN));
            }

            return r;
        }

        protected abstract override AlgoParameterCollection CreateParamaterDesription();
    }
}
