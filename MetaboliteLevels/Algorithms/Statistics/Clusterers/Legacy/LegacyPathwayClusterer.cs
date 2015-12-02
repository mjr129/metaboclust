using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers.Legacy
{
    class LegacyPathwayClusterer : ClustererBase
    {
        public LegacyPathwayClusterer(string id, string name)
            : base(id, name)
        {
        }

        protected override IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, IProgressReporter prog)
        {
            Dictionary<Pathway, Cluster> d = new Dictionary<Pathway, Cluster>();
            List<Cluster> result = new List<Cluster>();

            prog.ReportProgress("Finding pathways");

            for (int index = 0; index < vmatrix.NumVectors; index++)
            {
                var vec = vmatrix.Vectors[index];
                Peak peak = vec.Peak;
                prog.ReportProgress(index, vmatrix.NumVectors);

                foreach (Annotation c in peak.Annotations)
                {
                    foreach (Pathway p in c.Compound.Pathways)
                    {
                        Cluster pat;

                        if (!d.TryGetValue(p, out pat))
                        {
                            pat = new Cluster(p.Name, tag);
                            pat.States |= Data.Visualisables.Cluster.EStates.Pathway;
                            result.Add(pat);
                            d.Add(p, pat);
                        }

                        if (!pat.Assignments.Peaks.Contains(peak))
                        {
                            pat.Assignments.Add(new Assignment(vec, pat, double.NaN));
                        }
                    }
                }
            }

            return result;
        }

        public override AlgoParameters GetParams()
        {
            return new AlgoParameters(AlgoParameters.ESpecial.AlgorithmIgnoresObservationFilters | AlgoParameters.ESpecial.ClustererIgnoresDistanceMetrics | AlgoParameters.ESpecial.ClustererIgnoresDistanceMatrix, null);
        }
    }
}
