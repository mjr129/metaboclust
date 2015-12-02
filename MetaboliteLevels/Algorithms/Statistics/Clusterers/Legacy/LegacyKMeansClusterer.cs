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
    internal class LegacyKMeansClusterer : ClustererBase
    {
        public LegacyKMeansClusterer(string id, string name)
            : base(id, name)
        {
        }

        protected override IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix UNUSED, ArgsClusterer args, ConfigurationClusterer tag, IProgressReporter prog)
        {
            // GET OPTIONS
            int k = (int)args.Parameters[0];

            // CREATE RANDOM CENTRES
            Random rnd = new Random();

            var potentialCentres = new List<Vector>(vmatrix.Vectors);
            List<Cluster> clusters = new List<Cluster>();

            for (int n = 0; n < k; n++)
            {
                int random = rnd.Next(potentialCentres.Count);

                Cluster p = new Cluster((clusters.Count + 1).ToString(), tag);
                Vector vec = potentialCentres[random];
                p.Exemplars.Add(vec.Values);

                potentialCentres.RemoveAt(random);

                clusters.Add(p);
            }

            // Assign to exemplars
            prog.ReportProgress("Initialising assignments");
            LegacyClustererHelper.Assign(vmatrix, clusters, Settings.ECandidateMode.Exemplars, args.Distance, prog);

            // Centre
            LegacyClustererHelper.PerformKMeansCentering(vmatrix, clusters, args.Distance, prog);

            return clusters;
        }

        public override AlgoParameters GetParams()
        {
            var parameters = new[] { new AlgoParameters.Param("k", AlgoParameters.EType.Integer) };
            return new AlgoParameters(AlgoParameters.ESpecial.ClustererIgnoresDistanceMatrix, parameters);
        }
    }
}
