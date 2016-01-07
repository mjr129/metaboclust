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
        /// <summary>
        /// 
        /// </summary>
        public LegacyPathwayClusterer(string id, string name)
            : base(id, name)
        {
            Description = "Creates clusters based on the pathways in which the peaks' potential metabolites may be involved.";
        }

        /// <summary>
        /// 
        /// </summary>
        protected override IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog)
        {
            Dictionary<Pathway, Cluster> d = new Dictionary<Pathway, Cluster>();
            List<Cluster> result = new List<Cluster>();

            prog.Enter("Finding pathways");

            for (int index = 0; index < vmatrix.NumVectors; index++)
            {
                var vec = vmatrix.Vectors[index];
                Peak peak = vec.Peak;
                prog.SetProgress(index, vmatrix.NumVectors);

                foreach (Annotation c in peak.Annotations)
                {
                    foreach (Pathway p in c.Compound.Pathways)
                    {
                        Cluster pat;

                        if (!d.TryGetValue(p, out pat))
                        {
                            pat = new Cluster(p.DefaultDisplayName, tag);
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

            prog.Leave();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection();
        }

        public override bool SupportsObservationFilters { get { return false; } }

        public override bool RequiresDistanceMatrix { get { return false; } }

        public override bool SupportsDistanceMetrics { get { return false; } }
    }
}
