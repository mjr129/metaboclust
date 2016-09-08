using System.Linq;
using MetaboliteLevels.Utilities;
using System.Collections.Generic;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Data.Session;
using System;
using MetaboliteLevels.Data.Session.Associational;
using MGui.Helpers;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers
{
    /// <summary>
    /// Represents script-based clustering algorithms.
    /// </summary>
    class ClustererScript : ClustererBase
    {
        public const string INPUT_TABLE
= @"value.matrix,       x,  ""Numeric matrix of size n*m. The intensity matrix of the n input vectors, each of length m.""
    distance.matrix,    -,  Numeric matrix of size n*n. The distance matrix corresponding the value matrix calculated using the user's chosen distance metric. Requesting this parameter will incur an additional performance cost to calculate the distance matrix.
    RETURNS,            ,   Numeric vector of size n. The assigned clusters of the input vectors.
    SUMMARY,            ,   Clusters a set of input vectors.";

        public readonly RScript _script;
        private readonly bool _usesDistanceMatrix;

        public ClustererScript(string script, string id, string name, string fileName)
            : base(id, name)
        {
            this._script = new RScript(script, INPUT_TABLE, fileName);

            UiControls.Assert(_script.IsInputPresent(0) || _script.IsInputPresent(1), "ClustererScript must take at least one of value matrix or distance matrix");

            _usesDistanceMatrix = this._script.CheckInputMask("01");

            Description = "Clusters based on an R script.";
        }

        public override bool SupportsDistanceMetrics { get { return _usesDistanceMatrix; } }
        public override bool RequiresDistanceMatrix { get { return _usesDistanceMatrix; } }
        public override bool SupportsObservationFilters { get { return true; } }
        public override RScript Script => _script;

        protected override IEnumerable<Cluster> Cluster( IntensityMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog)
        {
            object[] inputs =
            {
                _script.IsInputPresent(0) ? vmatrix.Values.Flatten() : null,
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
