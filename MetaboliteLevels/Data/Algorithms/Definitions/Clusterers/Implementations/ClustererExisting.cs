using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers
{
    /// <summary>
    /// Assigns observations to existing clusters directly, but with the new input vector.
    /// </summary>
    class ClustererExisting : ClustererBase
    {
        /// <summary>CONSTRUCTOR</summary>
        public ClustererExisting(string id, string name)
            : base (id, name )
        {
            // NA
        }

        /// <summary>IMPLEMENTS ClustererBase</summary>
        public override bool RequiresDistanceMatrix => false;

        /// <summary>IMPLEMENTS ClustererBase</summary>
        public override bool SupportsDistanceMetrics => false;

        /// <summary>IMPLEMENTS ClustererBase</summary>
        public override bool SupportsObservationFilters => true;

        /// <summary>IMPLEMENTS ClustererBase</summary>
        protected override IEnumerable<Cluster> Cluster( IntensityMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog )
        {
            Dictionary<Cluster, Cluster> result = new Dictionary<Cluster,Cluster>();
            WeakReference<ConfigurationClusterer> wrMethod= (WeakReference < ConfigurationClusterer > )args.Parameters[0];
            ConfigurationClusterer method = wrMethod.GetTargetOrThrow();

            foreach (Vector vector in vmatrix.Vectors)
            {
                Cluster existingCluster = vector.Peak.Assignments.List.First( z => z.Cluster.Method == method ).Cluster;
                Cluster newCluster = result.GetOrCreate( existingCluster, xc => new Cluster( xc.ShortName, tag ) );

                newCluster.Assignments.Add( new Assignment( vector, newCluster, double.NaN ) );
            }

            return result.Values;
        }

        /// <summary>IMPLEMENTS ClustererBase</summary>
        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection( new[] { new AlgoParameter( "Source clusters", EAlgoParameterType.WeakRefConfigurationClusterer ) } );
        }
    }
}
