using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations
{
    class ClustererCombine : ClustererBase
    {
        /// <summary>CONSTRUCTOR</summary>
        public ClustererCombine( string id, string name )
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
            Cluster result = new Cluster( "Com", tag );

            foreach (Vector vector in vmatrix.Vectors)
            {
                result.Assignments.Add( new Assignment( vector, result, double.NaN ) );
            }

            return new Cluster[] { result };
        }

        /// <summary>IMPLEMENTS ClustererBase</summary>
        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection( new[]
            {
                new AlgoParameter(  "Clusters",
                                    "The clustering algorithm that provided the clusters the new set of clusters will be based upon.",
                                    AlgoParameterTypes.WeakRefConfigurationClusterer )
            } );
        }
    }
}
