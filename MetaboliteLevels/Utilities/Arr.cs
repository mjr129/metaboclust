using System;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using RDotNet;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Arr like a pirate, or R.
    /// </summary>
    class Arr
    {
        readonly REngine R;
        public static Arr Instance { get; private set; }

        public static void Initialize(string rBinPath)
        {
            Instance = new Arr(rBinPath);
        }

        public Arr(string rBinPath)
        {
            var envPath = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", envPath + ";" + rBinPath);
            R = REngine.GetInstance();
            R.Initialize();
            TestR();
        }

        public void TestR()
        {
            if (R.Evaluate("2+2").AsNumeric()[0] != 4)
            {
                throw new InvalidOperationException("Unexpected result from R.");
            }
        }

        public double TTest(Peak v, Core core, List<GroupInfo> type1, List<GroupInfo> type2)
        {
            var a = v.Observations.Raw.Corresponding(core.Observations, λ => type1.Contains(λ.Group));
            var b = v.Observations.Raw.Corresponding(core.Observations, λ => type2.Contains(λ.Group));

            return DoTTest(a, b);
        }

        public double DoTTest(IEnumerable<double> a, IEnumerable<double> b)
        {
            var v1 = R.CreateNumericVector(a);
            var v2 = R.CreateNumericVector(b);

            R.SetSymbol("a", v1);
            R.SetSymbol("b", v2);

            double result = R.Evaluate(@"t.test(a, b)$p.value").AsNumeric()[0];

            return result;
        }

        private static Range GetOverlappingTimeRange(Core core, IEnumerable<GroupInfo> types)
        {
            int min = int.MinValue; // sic
            int max = int.MaxValue;

            foreach (GroupInfo type in types)
            {
                int minA = int.MaxValue;
                int maxA = int.MinValue;

                foreach (ConditionInfo cond in core.Conditions)
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

        public double PcaAnova(Peak v, Core core, List<GroupInfo> types, List<int> reps)
        {
            Range times = GetOverlappingTimeRange(core, types);

            // Create a matrix thusly:
            // Control Replicate 1: <day1> <day2> <day3> ...
            // Control Replicate 2: <day1> <day2> <day3> ...
            // Control Replicate 3: <day1> <day2> <day3> ...
            // Drought Replicate 1: <day1> <day2> <day3> ...
            // Drought Replicate 2: <day1> <day2> <day3> ...
            // ...

            // Create and clear the matrix
            int rowCount = types.Count * reps.Count;
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
                groups[r] = types[r / reps.Count].Id;
            }

            // Fill out the values we know
            for (int i = 0; i < core.Observations.Count; i++)
            {
                ObservationInfo o = core.Observations[i];
                int typeIndex = types.IndexOf(o.Group);
                int repIndex = reps.IndexOf(o.Rep);

                if (times.Contains(o.Time) && typeIndex != -1 && repIndex != -1)
                {
                    int timeIndex = o.Time - times.Min;

                    int row = typeIndex * reps.Count + repIndex;
                    UiControls.Assert(double.IsNaN(matrix[row, timeIndex]), "Duplicate day/time/rep observations in dataset are not allowed.");
                    matrix[row, timeIndex] = v.Observations.Raw[i];
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
                        int repIndex = r % reps.Count;
                        int typeStart = r - repIndex;

                        double total = 0;
                        int count = 0;

                        for (int rep = 0; rep < reps.Count; rep++)
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

            var rMatrix = R.CreateNumericMatrix(matrix);
            var rVector = R.CreateNumericVector(groups);
            R.SetSymbol("a", rMatrix);
            R.SetSymbol("g", rVector);

            //R.Evaluate("write.csv(a, file = \"E:/MJR/Project/05. PEAS/AbstressData/Leaf/Positive/CCor/LP1131.cs.csv\")");

            try
            {
                double result = R.Evaluate(
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

        public void Pca(double[,] m, out double[,] scores, out double[,] loadings)
        {
            using (var v1 = R.CreateNumericMatrix(m))
            {
                R.SetSymbol("a", v1);

                R.Evaluate(@"p = prcomp(a)");

                scores = R.Evaluate(@"p$x").AsNumericMatrix().ToArray();
                loadings = R.Evaluate(@"p$rotation").AsNumericMatrix().ToArray();
            }             
        }

        public double Pearson(Peak v, Core core, GroupInfo type1)
        {
            IEnumerable<int> indices = core.Conditions.Which(λ => λ.Group == type1);
            IEnumerable<double> values = v.Observations.Trend.In(indices);
            IEnumerable<double> times = core.Conditions.In(indices).Select(λ => (double)λ.Time);

            double result;

            using (var v1 = R.CreateNumericVector(values))
            using (var v2 = R.CreateNumericVector(times))
            {

                R.SetSymbol("a", v1);
                R.SetSymbol("b", v2);
                result = R.Evaluate(@"cor(a, b)").AsNumeric()[0];

                // By taking medians we can get everything = 0 for the variables, which gives a NaN result
                if (double.IsNaN(result))
                {
                    result = 0;
                }
            }

            return result;
        }

        internal double Pearson(double[] p, double[] centre)
        {
            double result;

            using (var v1 = R.CreateNumericVector(p))
            using (var v2 = R.CreateNumericVector(centre))
            {
                R.SetSymbol("a", v1);
                R.SetSymbol("b", v2);
                result = R.Evaluate(@"cor(a, b)").AsNumeric()[0];
            }

            return -result; // negative result so small is still better
        }

        internal double Evaluate(string text)
        {
            return R.Evaluate(text).AsNumeric()[0];
        }

        internal int[] Cluster(double[,] distanceMatrix, double[,] valueMatrix, string script)
        {
            NumericMatrix dmat = null;
            NumericMatrix vmat = null;
            int[] result;

            try
            {
                if (distanceMatrix != null)
                {
                    dmat = R.CreateNumericMatrix(distanceMatrix);
                    R.SetSymbol("d", dmat);
                }

                if (valueMatrix != null)
                {
                    vmat = R.CreateNumericMatrix(valueMatrix);
                    R.SetSymbol("v", vmat);
                }

                result = R.Evaluate(script).AsInteger().ToArray();
            }
            finally
            {
                if (dmat != null)
                {
                    dmat.Dispose();
                }

                if (vmat != null)
                {
                    vmat.Dispose();
                }
            }

            return result;
        }

        /// <summary>
        /// Doesn't provide enough information.
        /// </summary>
        internal double[] Statistics(double[,] distanceMatrix, double[,] valueMatrix, string script)
        {
            NumericMatrix dmat = null;
            NumericMatrix vmat = null;
            double[] result;

            try
            {
                if (distanceMatrix != null)
                {
                    dmat = R.CreateNumericMatrix(distanceMatrix);
                    R.SetSymbol("d", dmat);
                }

                if (valueMatrix != null)
                {
                    vmat = R.CreateNumericMatrix(valueMatrix);
                    R.SetSymbol("v", vmat);
                }

                result = R.Evaluate(script).AsNumeric().ToArray();

            }
            finally
            {
                if (dmat != null)
                {
                    dmat.Dispose();
                }

                if (vmat != null)
                {
                    vmat.Dispose();
                }
            }

            return result;
        }

        internal double RunScriptDouble(RScript script, object[] inputs, object[] args)
        {
            ApplyInputs(script, inputs);
            ApplyArgs(script, args);

            return R.Evaluate(script.Script).AsNumeric()[0];
        }

        internal IEnumerable<int> RunScriptIntV(RScript script, object[] inputs, object[] args)
        {
            ApplyInputs(script, inputs);
            ApplyArgs(script, args);

            return R.Evaluate(script.Script).AsInteger();
        }

        internal IEnumerable<double> RunScriptDoubleV(RScript script, object[] inputs, object[] args)
        {
            ApplyInputs(script, inputs);
            ApplyArgs(script, args);

            return R.Evaluate(script.Script).AsNumeric();
        }

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
                        sym = R.CreateNumericVector((double[])obj);
                    }
                    else if (obj is double[,])
                    {
                        sym = R.CreateNumericMatrix((double[,])obj);
                    }
                    else if (obj is IEnumerable<double>)
                    {
                        sym = R.CreateNumericVector((IEnumerable<double>)obj);
                    }
                    else
                    {
                        throw new InvalidOperationException("Cannot create R object for obj: " + obj);
                    }

                    R.SetSymbol(name, sym);
                }
            }
        }

        private void ApplyArgs(RScript script, object[] args)
        {
            UiControls.Assert((args == null) == (script.RequiredParameters.Parameters == null));

            if (script.RequiredParameters.Parameters != null)
            {
                var req = script.RequiredParameters.Parameters;

                UiControls.Assert(req.Length == args.Length);

                for (int i = 0; i < req.Length; i++)
                {
                    var p = req[i];
                    object v = args[i];

                    switch (p.Type)
                    {
                        case AlgoParameters.EType.Double:
                            R.SetSymbol(p.Name, R.CreateNumeric((double)v));
                            break;

                        case AlgoParameters.EType.Integer:
                            R.SetSymbol(p.Name, R.CreateInteger((int)v));
                            break;

                        default:
                            throw new InvalidOperationException("ApplyArgs: " + p.Type + " on " + p.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Exports core data in R format.
        /// </summary>
        internal void Export(Core core, string fileName)
        {
            int np = core.Peaks.Count;

            var stats = core.Statistics.ToArray();
            string[] metas = core._peakMeta.Headers;

            double[][] mat_raw = new double[np][];
            double[][] mat_pat = new double[np][];
            double[][] mat_avg = new double[np][];
            //double[][] mat_val = new double[np][];
            double[][] mat_stats = new double[np][];
            string[][] mat_meta = new string[np][];

            int[][] mat_obs = new int[core.Observations.Count][];
            int[][] mat_con = new int[core.Conditions.Count][];

            for (int i = 0; i < core.Peaks.Count; i++)
            {
                Peak p = core.Peaks[i];

                mat_raw[i] = p.Observations.Raw;
                mat_avg[i] = p.Observations.Trend;
                //mat_val[i] = p.Values;

                mat_stats[i] = new double[stats.Length];
                for (int ki = 0; ki < stats.Length; ki++)
                {
                    ConfigurationStatistic k = stats[ki];
                    mat_stats[i][ki] = p.Statistics[k];
                }

                mat_meta[i] = new string[metas.Length];
                for (int ki = 0; ki < metas.Length; ki++)
                {
                    mat_meta[i][ki] = StringHelper.ArrayToString(p.MetaInfo.Read(ki));
                }

                mat_pat[i] = new double[core.Clusters.Count];
                for (int ki = 0; ki < core.Clusters.Count; ki++)
                {
                    Cluster k = core.Clusters[ki];

                    if (p.Assignments.IsInCluster(k))
                    {
                        double d = p.Assignments.Get(k).Score;
                        mat_pat[i][ki] = double.IsNaN(d) ? 0d : d;
                    }
                    else
                    {
                        mat_pat[i][ki] = double.NaN;
                    }
                }
            }

            for (int i = 0; i < core.Observations.Count; i++)
            {
                ObservationInfo o = core.Observations[i];

                mat_obs[i] = new int[3];
                mat_obs[i][0] = o.Group.Id;
                mat_obs[i][1] = o.Time;
                mat_obs[i][2] = o.Rep;
            }

            for (int i = 0; i < core.Conditions.Count; i++)
            {
                ConditionInfo o = core.Conditions[i];

                mat_con[i] = new int[2];
                mat_con[i][0] = o.Group.Id;
                mat_con[i][1] = o.Time;
            }

            string[] peakNames = core.Peaks.Select(z => z.DisplayName).ToArray();
            string[] patNames = core.Clusters.Select(z => z.DisplayName).ToArray();
            string[] obsNames = core.Observations.Select(z => z.ToString()).ToArray();
            string[] conNames = core.Conditions.Select(z => z.ToString()).ToArray();
            string[] statsNames = stats.Select(z => z.ToString()).ToArray();
            string[] metaNames = metas;
            string[] obsCols = { "type", "day", "rep" };
            string[] conCols = { "type", "day" };

            R.ClearGlobalEnvironment();

            string[] desc = "raw = Raw data as loaded|avg = Averaged data|vavg = Averaged data (used for clusters)|stats = Statistics|meta = Meta data|obs = Observation data (for raw)|con = Condition data (for avg)|vcon = Condition data (for vavg)|pat = Cluster assignment scores".Split('|');

            var rmat_raw = R.CreateDataFrame(mat_raw, peakNames, obsNames);
            var rmat_avg = R.CreateDataFrame(mat_avg, peakNames, conNames);
            //var rmat_val = R.CreateDataFrame(mat_val, peakNames, vconNames);
            var rmat_stats = R.CreateDataFrame(mat_stats, peakNames, statsNames);
            var rmat_meta = R.CreateDataFrame(mat_meta, peakNames, metaNames);
            var rmat_obs = R.CreateDataFrame(mat_obs, obsNames, obsCols);
            var rmat_con = R.CreateDataFrame(mat_con, conNames, conCols);
            var rmat_pat = R.CreateDataFrame(mat_pat, peakNames, patNames);
            var rmat_desc = R.CreateCharacterVector(desc);

            R.SetSymbol("x.desc", rmat_desc);
            R.SetSymbol("x.raw", rmat_raw);
            R.SetSymbol("x.avg", rmat_avg);
            R.SetSymbol("x.stats", rmat_stats);
            R.SetSymbol("x.meta", rmat_meta);
            R.SetSymbol("x.obs", rmat_obs);
            R.SetSymbol("x.con", rmat_con);
            R.SetSymbol("x.pat", rmat_pat);

            string cmd = "x = list(description = x.desc, raw = x.raw, avg = x.avg, stats = x.stats, meta = x.meta, obs = x.obs, con = x.con, pat = x.pat)\r\n"
                + string.Format(@"save(x, file = ""{0}"")", fileName.Replace(@"\", @"\\"));

            R.Evaluate(cmd);

            R.ClearGlobalEnvironment();
        }

        /// <summary>
        /// Calculates silhouette widths
        /// </summary>
        /// <param name="clusters">Cluster assignments</param>
        /// <param name="dmatrix">Distance matrix</param>
        /// <returns>"cluster"   "neighbor"  "sil_width"</returns>
        internal double[,] CalculateSilhouette(Cluster[] clusters, DistanceMatrix dmatrix)
        {
            var peaks = dmatrix.Peaks.ToArray();
            int[] assignmentVector = new int[peaks.Length];

            for (int peakIndex = 0; peakIndex < peaks.Length; peakIndex++)
            {
                Peak p = peaks[peakIndex];

                int clusterIndex = clusters.FirstIndexWhere(z => z.Assignments.Peaks.Contains(p));

                assignmentVector[peakIndex] = clusterIndex;
            }

            var vec = R.CreateIntegerVector(assignmentVector);
            NumericMatrix dmat;

            try
            {
                dmat = R.CreateNumericMatrix(dmatrix.Values);
            }
            catch
            {
                dmat = R.CreateNumericMatrix(dmatrix.NumPeaks, dmatrix.NumPeaks);

                for (int i = 0; i < dmatrix.NumPeaks; i++)
                {
                    for (int j = 0; j < dmatrix.NumPeaks; j++)
                    {
                        dmat[i, j] = dmatrix.Values[i, j];
                    }
                }
            }

            R.SetSymbol("assignments", vec);
            R.SetSymbol("dmatrix", dmat);

            NumericMatrix result = R.Evaluate("library(cluster); as.matrix(silhouette(assignments, dmatrix = dmatrix));").AsNumericMatrix();

            return result.ToArray();
        }
    }
}
