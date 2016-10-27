using System;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MGui;
using MGui.Helpers;
using RDotNet;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Arr like a pirate, or R.
    /// </summary>
    class Arr
    {
        /// <summary>
        /// R.NET Engine
        /// </summary>
        readonly REngine _r;

        private string _origEnvPath;

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static Arr Instance { get; private set; }

        /// <summary>
        /// Initialises the R instance.
        /// </summary>                     
        public static void Initialize(string rBinPath)
        {
            Instance = new Arr(rBinPath);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rBinPath">Where R binaries live</param>
        public Arr(string rBinPath)
        {
            var envPath = _origEnvPath ?? Environment.GetEnvironmentVariable("PATH");
            _origEnvPath = envPath;
            Environment.SetEnvironmentVariable("PATH", envPath + ";" + rBinPath);

            try
            {
                _r = REngine.GetInstance();
            }
            catch
            {
                // Looks like an error with the adapter that causes a fail first time if the path isn't in the registry
                _r = REngine.GetInstance();
            }

            _r.Initialize();
            TestR();
        }

        /// <summary>
        /// Checks if R is working and connected.
        /// </summary>
        public void TestR()
        {
            if (_r.Evaluate("2+2").AsNumeric()[0] != 4)
            {
                throw new InvalidOperationException("Unexpected result from R.");
            }
        }

        /// <summary>
        /// Gets the overlapping time range for the specified [groups].
        /// </summary>                                                 

        private static Range GetOverlappingTimeRange(Core core, IntensityMatrix source, IEnumerable<GroupInfo> groups)
        {
            int min = int.MinValue; // sic
            int max = int.MaxValue;

            foreach (GroupInfo type in groups)
            {
                int minA = int.MaxValue;
                int maxA = int.MinValue;

                foreach (ObservationInfo cond in source.Columns.Select(z=> z.Observation ))
                {
                    if (cond.Group == type)
                    {
                        minA = Math.Min(minA, cond.Time);
                        maxA = Math.Max(maxA, cond.Time);
                    }
                }

                UiControls.Assert(minA != int.MaxValue && maxA != int.MinValue, "Failed to determine overlapping time range (type).");

                min = Math.Max(minA, min);
                max = Math.Min(maxA, max);
            }

            UiControls.Assert(min != int.MinValue && max != int.MaxValue, "Failed to determine overlapping time range (all).");

            return new Range(min, max);
        }

        /// <summary>
        /// Does Arian's PCA-ANOVA idea.
        /// </summary>                
        public double PcaAnova(IntensityMatrix source, Peak peak, Core core, List<GroupInfo> types, List<int> replicates)
        {
            Range times = GetOverlappingTimeRange(core, source, types );

            // Create a matrix thusly:
            // Control Replicate 1: <day1> <day2> <day3> ...
            // Control Replicate 2: <day1> <day2> <day3> ...
            // Control Replicate 3: <day1> <day2> <day3> ...
            // Drought Replicate 1: <day1> <day2> <day3> ...
            // Drought Replicate 2: <day1> <day2> <day3> ...
            // ...

            // Create and clear the matrix
            int rowCount = types.Count * replicates.Count;
            int colCount = times.Count;
            double[,] matrix = new double[rowCount, colCount];

            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    matrix[r, c] = double.NaN;
                }
            }

            // Create the group vector
            double[] groups = new double[rowCount];

            for (int r = 0; r < rowCount; r++)
            {
                groups[r] = types[r / replicates.Count].Order;
            }

            double[] raw = source.Find( peak ).Values; // TODO: Dirty

            // Fill out the values we know
            for (int i = 0; i < core.Observations.Count; i++)
            {
                ObservationInfo o = core.Observations[i];
                int typeIndex = types.IndexOf(o.Group);
                int repIndex = replicates.IndexOf(o.Rep);

                if (times.Contains(o.Time) && typeIndex != -1 && repIndex != -1)
                {
                    int timeIndex = o.Time - times.Min;

                    int row = typeIndex * replicates.Count + repIndex;
                    UiControls.Assert(double.IsNaN(matrix[row, timeIndex]), "Duplicate day/time/rep observations in dataset are not allowed.");
                    matrix[row, timeIndex] = raw[i];
                }
            }

            // Guess missing values
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    if (double.IsNaN(matrix[r, c]))
                    {
                        // Missing values - average other values for this point
                        int repIndex = r % replicates.Count;
                        int typeStart = r - repIndex;

                        double total = 0;
                        int count = 0;

                        for (int rep = 0; rep < replicates.Count; rep++)
                        {
                            int newRow = typeStart + rep;

                            if (!double.IsNaN(matrix[newRow, c]))
                            {
                                total += matrix[newRow, c];
                                count += 1;
                            }
                        }

                        matrix[r, c] = total / count;
                    }
                }
            }

            // Now do that R stuff...

            var rMatrix = _r.CreateNumericMatrix(matrix);
            var rVector = _r.CreateNumericVector(groups);
            _r.SetSymbol("a", rMatrix);
            _r.SetSymbol("g", rVector);

            //R.Evaluate("write.csv(a, file = \"E:/MJR/Project/05. PEAS/AbstressData/Leaf/Positive/CCor/LP1131.cs.csv\")");

            try
            {
                double result = _r.Evaluate(
    @"p = prcomp(a)
f = data.frame(y = p$x[,1], group = factor(g))
fit = lm(y ~ group, f)
an = anova(fit)
pval = an$""Pr(>F)""[1]").AsNumeric()[0];

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong calculating PCA-ANOVA statistics. See inner exception for details. (Note that this error can occur if only 1 replicate is specified and PCA-ANOVA is calculated with missing values - make sure the replicates are specified correctly.)", ex);
            }
        }

        /// <summary>
        /// Does PCA.
        /// </summary>
        public void Pca(double[,] m, out double[,] scores, out double[,] loadings)
        {
            using (var v1 = _r.CreateNumericMatrix(m))
            {
                _r.SetSymbol("a", v1);

                _r.Evaluate(@"p = prcomp(a)");

                scores = _r.Evaluate(@"p$x").AsNumericMatrix().ToArray();
                loadings = _r.Evaluate(@"p$rotation").AsNumericMatrix().ToArray();
            }
        }

        /// <summary>
        /// Does PLSR.
        /// </summary>
        public void Plsr( double[,] m, double[] r, out double[,] scores, out double[,] loadings )
        {
            using (var v1 = _r.CreateNumericMatrix( m ))
            using (var v2 = _r.CreateNumericVector( r ))
            {
                _r.SetSymbol( "a", v1 );
                _r.SetSymbol( "b", v2 );

                _r.Evaluate( "library(pls)\r\np = plsr(b ~ a)" );

                scores = _r.Evaluate( @"p$scores" ).AsNumericMatrix().ToArray();
                loadings = _r.Evaluate( @"p$loadings" ).AsNumericMatrix().ToArray();
            }
        }

        /// <summary>
        /// Evaluates the expression.
        /// </summary>               

        internal double Evaluate(string text)
        {
            return _r.Evaluate(text).AsNumeric()[0];
        }

        /// <summary>
        /// Runs an RScript object that returns a double.
        /// </summary>                                   
        internal double RunScriptDouble(RScript script, object[] inputs, object[] args)
        {
            ApplyInputs(script, inputs);
            ApplyArgs(script, args);

            return _r.Evaluate(script.Script).AsNumeric()[0];
        }

        /// <summary>
        /// Runs an RScript object that returns a integer vector.
        /// </summary>                                   
        internal IEnumerable<int> RunScriptIntV(RScript script, object[] inputs, object[] args)
        {
            ApplyInputs(script, inputs);
            ApplyArgs(script, args);

            return _r.Evaluate(script.Script).AsInteger();
        }

        /// <summary>
        /// Runs an RScript object that returns a double vector.
        /// </summary>                                   
        internal IEnumerable<double> RunScriptDoubleV(RScript script, object[] inputs, object[] args)
        {
            ApplyInputs(script, inputs);
            ApplyArgs(script, args);

            return _r.Evaluate(script.Script).AsNumeric();
        }

        /// <summary>
        /// (Private) Applies the inputs of an RScript object.
        /// </summary>                                        
        private void ApplyInputs(RScript script, object[] inputs)
        {
            UiControls.Assert(inputs.Length == script.InputNames.Length, "Number of inputs requested by script must match number of inputs provided.");

            for (int i = 0; i < inputs.Length; i++)
            {
                string name = script.InputNames[i];

                if (name != null)
                {
                    var obj = inputs[i];
                    SymbolicExpression sym;

                    if (obj is double[])
                    {
                        sym = _r.CreateNumericVector((double[])obj);
                    }
                    else if (obj is double[,])
                    {
                        sym = _r.CreateNumericMatrix((double[,])obj);
                    }
                    else if (obj is IEnumerable<double>)
                    {
                        sym = _r.CreateNumericVector((IEnumerable<double>)obj);
                    }
                    else if (obj is int[])
                    {
                        sym = _r.CreateIntegerVector( (int[])obj );
                    }
                    else if (obj is int[,])
                    {
                        sym = _r.CreateIntegerMatrix( (int[,])obj );
                    }
                    else if (obj is IEnumerable<int>)
                    {
                        sym = _r.CreateIntegerVector( (IEnumerable<int>)obj );
                    }
                    else
                    {
                        throw new InvalidOperationException("Cannot create R object for obj: " + obj);
                    }

                    _r.SetSymbol(name, sym);
                }
            }
        }

        /// <summary>
        /// (Private) Applies the arguments of an RScript object.
        /// </summary>            
        private void ApplyArgs(RScript script, object[] args)
        {
            args = args ?? new object[0]; // Todo: Necessary for legacy only

            UiControls.Assert((args.Length != 0) == script.RequiredParameters.HasCustomisableParams, "No arguments provided when algorithm has customisable parameters, or arguments provided when the algorithm has no customisable parameters." );

            if (script.RequiredParameters.HasCustomisableParams)
            {
                var req = script.RequiredParameters;

                UiControls.Assert(req.Count == args.Length, "Argument count passed in doesn't match the count expected by the script");

                for (int i = 0; i < req.Count; i++)
                {
                    var p = req[i];
                    object v = args[i];

                    p.Type.SetSymbol( _r, p.Name, v );
                }
            }
        }     
    }
}
