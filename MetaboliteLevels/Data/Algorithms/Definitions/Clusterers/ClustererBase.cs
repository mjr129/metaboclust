﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers
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

        public ResultClusterer ExecuteAlgorithm(Core core, int isPreview, bool doNotCluster, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog, out IntensityMatrix vmatrixOut, out DistanceMatrix dmatrixOut)
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
            PeakFilter pfilter = args.PeakFilter ?? PeakFilter.Empty;

            IntensityMatrix src = args.SourceMatrix;
            Filter<Peak>.Results filter = pfilter.Test(peaks);
            Cluster insigs;

            if (filter.Failed.Count == 0)
            {
                insigs = null;
            }
            else
            {
                insigs = new Cluster("Insig", tag);
                insigs.States |= Session.Main.Cluster.EStates.Insignificants;

                // We still need the vmatrix for plotting later
                IntensityMatrix operational = src.Subset( args.PeakFilter, args.ObsFilter, ESubsetFlags.InvertPeakFilter );

                if (args.SplitGroups)
                {
                    operational = operational.SplitGroups();
                }

                for (int index = 0; index < operational.NumRows; index++)
                {
                    Vector p = new Vector( operational, index );
                    insigs.Assignments.Add(new Assignment(p, insigs, double.NaN));
                }
            }

            // CREATE VMATRIX AND FILTER OBSERVATIONS
            PeakFilter temp = new PeakFilter( "filtered in", null, new[] { new PeakFilter.ConditionPeak( Filter.ELogicOperator.And, false, filter.Failed, Filter.EElementOperator.IsNot ) } );
            IntensityMatrix vmatrix = src.Subset( args.PeakFilter, args.ObsFilter, ESubsetFlags.None);

            if (args.SplitGroups)
            {
                vmatrix = vmatrix.SplitGroups();
            }

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

            if (insigs != null)
            {
                result.Add(insigs);
            }

            result.AddRange(clusters);
            return new ResultClusterer(result);
        }

        /// <summary>
        /// Clustering
        /// 
        /// If the cluster does't make use of the distance matrix OR the distance metric it should flag itself with DoesNotSupportDistanceMetrics.
        /// </summary>
        protected abstract IEnumerable<Cluster> Cluster(IntensityMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog);

        protected static IEnumerable<Cluster> CreateClustersFromIntegers(IntensityMatrix vmatrix, IList<int> clusters, ConfigurationClusterer tag)
        {
            Dictionary<int, Cluster> pats = new Dictionary<int, Cluster>();
            List<Cluster> r = new List<Cluster>();

            for (int n = 0; n < vmatrix.NumRows; n++)
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
