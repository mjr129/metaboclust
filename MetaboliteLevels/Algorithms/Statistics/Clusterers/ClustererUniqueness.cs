using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using RefAss = System.Tuple<MetaboliteLevels.Data.DataInfo.GroupInfo, MetaboliteLevels.Data.Visualisables.Cluster>;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers
{
    /// <summary>
    /// A clusterer that finds the unique combinations of existing clusters.
    /// </summary>
    class ClustererUniqueness : ClustererBase
    {
        public ClustererUniqueness(string id, string name)
            : base(id, name)
        {
        }

        protected override IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog)
        {
            ConfigurationClusterer config = ((WeakReference<ConfigurationClusterer>)args.Parameters[0]).GetTarget();
            List<List<Assignment>> uniqueCombinations = new List<List<Assignment>>();
            List<Cluster> newClusters = new List<Cluster>();
            List<ConditionInfo[]> conditions = new List<ConditionInfo[]>();
            List<ObservationInfo[]> observations = new List<ObservationInfo[]>();

            prog.Enter("Finding unique matches");

            for (int vmatIndex = 0; vmatIndex < vmatrix.NumVectors; vmatIndex++)
            {
                Vector vp = vmatrix.Vectors[vmatIndex];
                Peak peak = vp.Peak;
                prog.SetProgress(vmatIndex, vmatrix.NumVectors);

                List<Assignment> pats = new List<Assignment>(peak.Assignments.List
                                                         .Where(z => config.Results.Clusters.Contains(z.Cluster))
                                                         .OrderBy(z => z.Vector.Group.Id));
                //.Select(z => new RefAss(z.Vector.Group, z.Cluster)));

                int index = FindMatch(uniqueCombinations, pats);
                Cluster pat;

                if (index == -1)
                {
                    uniqueCombinations.Add(pats);

                    string name = StringHelper.ArrayToString(pats, z => z.Vector.Group.ShortName + "." + z.Cluster.ShortName, " / ");

                    pat = new Cluster(name, tag);

                    // Centre (merge centres)
                    IEnumerable<double[]> centres = pats.Select(z => z.Cluster.Centres.First());
                    pat.Centres.Add(centres.SelectMany(z => z).ToArray());

                    // Vector (merge vectors)
                    if (pats[0].Vector.Conditions != null)
                    {
                        conditions.Add(pats.Select(z => z.Vector.Conditions).SelectMany(z => z).ToArray());
                    }
                    else
                    {
                        conditions.Add(null);
                    }

                    if (pats[0].Vector.Observations != null)
                    {
                        observations.Add(pats.Select(z => z.Vector.Observations).SelectMany(z => z).ToArray());
                    }
                    else
                    {
                        observations.Add(null);
                    }

                    // Relations (all clusters)
                    pat.Related.AddRange(pats.Select(z => z.Cluster).Unique());

                    foreach (Cluster pat2 in pat.Related)
                    {
                        if (!pat2.Related.Contains(pat))
                        {
                            pat2.Related.Add(pat);
                        }
                    }

                    index = newClusters.Count;
                    newClusters.Add(pat);
                }


                pat = newClusters[index];

                double[] values = pats.Select(z => z.Vector.Values).SelectMany(z => z).ToArray();
                Vector v = new Vector(peak, null, conditions[index], observations[index], values);
                pat.Assignments.Add(new Assignment(v, pat, pats.Count));
            }

            prog.Leave();

            return newClusters;
        }

        private int FindMatch(List<List<Assignment>> uniqueCombinations, List<Assignment> pats)
        {
            for (int index = 0; index < uniqueCombinations.Count; index++)
            {
                var list = uniqueCombinations[index];
                UiControls.Assert(list.Count == pats.Count);

                if (IsEqual(pats, list))
                {
                    return index;
                }
            }

            return -1;
        }

        private static bool IsEqual(List<Assignment> pats, List<Assignment> list)
        {
            for (int index = 0; index < pats.Count; index++)
            {
                Assignment v = pats[index];
                Assignment t = list[index];

                UiControls.Assert(v.Vector.Group == t.Vector.Group);

                if (t.Cluster != v.Cluster)
                {
                    return false;
                }
            }

            return true;
        }

        public override AlgoParameters GetParams()
        {
            AlgoParameters.Param[] @params = new AlgoParameters.Param[]
                                       {
                                           new AlgoParameters.Param("source", AlgoParameters.EType.WeakRefConfigurationClusterer)
                                       };
            return new AlgoParameters(AlgoParameters.ESpecial.ClustererIgnoresDistanceMetrics | AlgoParameters.ESpecial.AlgorithmIgnoresObservationFilters | AlgoParameters.ESpecial.ClustererIgnoresDistanceMatrix, @params);
        }
    }
}
