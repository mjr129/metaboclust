using System.Linq;
using MetaboliteLevels.Utilities;
using System.Collections.Generic;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Data.Session;
using System;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers
{
    /// <summary>
    /// Represents script-based clustering algorithms.
    /// </summary>
    class ClustererScript : ClustererBase
    {
        public const string INPUTS = @"value.matrix=x,distance.matrix=-";
        public readonly RScript _script;
        private readonly bool _usesDistanceMatrix;

        public ClustererScript(string script, string id, string name)
            : base(id, name)
        {
            this._script = new RScript(script, INPUTS);

            UiControls.Assert(_script.IsInputPresent(0) || _script.IsInputPresent(1), "ClustererScript must take at least one of value matrix or distance matrix");

            _usesDistanceMatrix = this._script.CheckInputMask("01");

            Description = "Clusters based on an R script.";
        }

        public override bool SupportsDistanceMetrics { get { return _usesDistanceMatrix; } }
        public override bool RequiresDistanceMatrix { get { return _usesDistanceMatrix; } }
        public override bool SupportsObservationFilters { get { return true; } }

        protected override IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog)
        {
            object[] inputs =
            {
                _script.IsInputPresent(0) ? vmatrix.Flatten() : null,
                _script.IsInputPresent(1) ? dmatrix.Values : null
            };

            prog.Enter("Running script");
            prog.SetProgressMarquee();
            int[] clusters = Arr.Instance.RunScriptIntV(_script, inputs, args.Parameters).ToArray();
            prog.Leave();
            prog.Enter("Creating clusters");
            var result = CreateClustersFromIntegers(vmatrix, clusters, tag);
            prog.Leave();
            return result;
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return _script.RequiredParameters;
        }
    }
}
