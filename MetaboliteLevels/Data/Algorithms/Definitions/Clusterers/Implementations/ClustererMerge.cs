using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Clusterers;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations
{
    class ClustererMerge : ClustererBase
    {
        public ClustererMerge()
            : base( "ClustererMerge", "Merge existing clusters" )
        {
            // NA
        }

        public override bool RequiresDistanceMatrix => false;

        public override bool SupportsDistanceMetrics => false;

        public override bool SupportsObservationFilters => false;

        protected override IEnumerable<Cluster> Cluster( ValueMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog )
        {
            WeakReference<Cluster>[] param1 = (WeakReference<Cluster>[])args.Parameters[0];
            Cluster[] clusters = param1.Select( z => z.GetTargetOrThrow() ).ToArray();
            HashSet<Vector> results = new HashSet<Vector>();

            foreach (Cluster c in clusters)
            {
                foreach (Assignment a in c.Assignments.List)
                {
                    results.Add( Find(vmatrix.Vectors, a.Vector) );
                }
            }

            string names = "Merged: " + clusters.Select( z => z.ShortName ).JoinAsString( "+" );

            Cluster result = new Cluster( names, tag );

            foreach (Vector v in results)
            {
                result.Assignments.Add( new Assignment( v, result, double.NaN ) );
            }

            return new Cluster[] { result };
        }

        private Vector Find( Vector[] vectors, Vector vector )
        {
            throw new NotImplementedException();
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            AlgoParameter param1 = new AlgoParameter( "Clusters", EAlgoParameterType.WeakRefClusterArray );

            return new AlgoParameterCollection( param1 );
        }
    }
}
