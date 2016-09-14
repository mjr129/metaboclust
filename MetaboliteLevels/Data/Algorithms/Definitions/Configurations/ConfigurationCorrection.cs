using System;
using System.Collections;
using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Trends;
using System.Linq;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Algorithms.Statistics.Corrections;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Algorithms.Statistics.Inputs;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Viewers.Lists;
using MGui.Helpers;
using MGui.Datatypes;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    [Name("Correction configuration")]
    [Serializable]
    internal class ConfigurationCorrection : ConfigurationBase<AlgoBase, ArgsBase, ResultCorrection>, IMatrixProvider
    {               
        public bool IsUsingTrend
        {
            get
            {
                return Args is ArgsTrendAsCorrection;
            }
        }              

        public ArgsTrendAsCorrection ArgsT
        {
            get
            {
                return (ArgsTrendAsCorrection)Args;
            }
        }               

        /// <summary>
        /// Like Correct(), but just gets the trend (for plots).
        /// </summary>
        internal double[] ExtractTrend(Core core, double[] raw, out IReadOnlyList<ObservationInfo> trendOrder)
        {
            if (!IsUsingTrend)
            {
                trendOrder = null;
                return null;
            }

            var args = base.Args as ArgsTrendAsCorrection;
            var algo = base.GetAlgorithmOrThrow() as TrendBase;

            switch (args.Mode)
            {
                case ECorrectionMode.Batch:
                    {
                        if (args.Constraint != null)
                        {
                            IEnumerable<int> xI = core.Observations.Which(args.Constraint.Test);
                            ObservationInfo[] x = core.Observations.At(xI).ToArray();
                            IReadOnlyList<ObservationInfo> xOut = core.Observations;
                            IReadOnlyList<BatchInfo> g = core.Batches;
                            double[] y = raw.At(xI).ToArray();

                            trendOrder = xOut;
                            return algo.SmoothByBatch(x, xOut, g, y, args);
                        }
                        else
                        {
                            IReadOnlyList<ObservationInfo> x = core.Observations;
                            IReadOnlyList<ObservationInfo> xOut = core.Observations;
                            double[] y = raw;
                            IReadOnlyList<BatchInfo> g = core.Batches;

                            trendOrder = xOut;
                            return algo.SmoothByBatch(x, xOut, g, y, args);
                        }
                    }

                case ECorrectionMode.Control:
                    {
                        IEnumerable<int> xI = core.Observations.Which(z => z.Group == args.ControlGroup);
                        ObservationInfo[] x = core.Observations.At(xI).ToArray();
                        ObservationInfo[] xOut = core.Conditions.Where(z => z.Group == args.ControlGroup).ToArray();
                        double[] y = raw.At(xI).ToArray();
                        GroupInfo[] g = new[] { args.ControlGroup };

                        trendOrder = xOut;
                        return algo.SmoothByType(x, xOut, g, y, args);
                    }

                default:
                    throw new SwitchException(args.Mode);
            }
        }

        /// <summary>
        /// Executes the correction for a set of raw values (in core.observation order).
        /// </summary>
        public double[] Calculate(Core core, double[] raw)
        {
            double[] result;

            if (!IsUsingTrend)
            {
                var args = base.Args as ArgsCorrection;
                var algo = base.GetAlgorithmOrThrow() as CorrectionBase;

                result = algo.Calculate(raw, args);
            }
            else
            {
                var args = base.Args as ArgsTrendAsCorrection;
                var algo = base.GetAlgorithmOrThrow() as TrendBase;

                IReadOnlyList<ObservationInfo> trendOrder;
                double[] trend = ExtractTrend(core, raw, out trendOrder);
                IReadOnlyList<ObservationInfo> resultOrder = core.Observations;
                result = new double[raw.Length];

                switch (args.Mode)
                {
                    case ECorrectionMode.Batch:
                        {
                            // We know that the trend order for batch correction (above) is the core order
                            // so we can save time by not converting the indices
                            switch (args.Method)
                            {
                                case ECorrectionMethod.Divide:
                                    for (int i = 0; i < trend.Length; i++)
                                    {
                                        result[i] = raw[i] / trend[i];
                                    }
                                    break;

                                case ECorrectionMethod.Subtract:
                                    for (int i = 0; i < raw.Length; i++)
                                    {
                                        result[i] = raw[i] - trend[i];
                                    }
                                    break;

                                default:
                                    throw new SwitchException(args.Method);
                            }
                        }
                        break;

                    case ECorrectionMode.Control:
                        {
                            // Here the trend will only represent the control group
                            for (int i = 0; i < raw.Length; i++)
                            {
                                ObservationInfo obs = resultOrder[i];

                                int j = trendOrder.FirstIndexWhere(z => z.Time == obs.Time); // TODO: Awful linear search

                                if (j == -1)
                                {
                                    // TODO: Time point for control outside range for observation
                                    // This should be handled by truncation of the range and removal of QCs, for now let it pass
                                    // throw new InvalidOperationException("The observation " + obs + " needs to be corrected using the corrected data for the same timepoint, but t = " + obs.Time + " cannot be found in the generated trend: " + StringHelper.ArrayToString(trendOrder.OrderBy(z => z.Time)) + "\r\nYou may need to truncate datapoints outside this range prior to correction.");
                                    continue;
                                }

                                switch (args.Method)
                                {
                                    case ECorrectionMethod.Divide:
                                        result[i] = raw[i] / trend[j];
                                        break;

                                    case ECorrectionMethod.Subtract:
                                        result[i] = raw[i] - trend[j];
                                        break;

                                    default:
                                        throw new SwitchException(args.Method);
                                }
                            }
                        }
                        break;

                    default:
                        throw new SwitchException(args.Mode);
                }
            }

            return result;
        }                                 

        public override bool Run( Core core, ProgressReporter prog )
        {
            try
            {
                // For each peak
                IntensityMatrix source = this.Args.SourceMatrix;
                double[][] results = new double[source.NumRows][];

                for (int peakIndex = 0; peakIndex < source.NumRows; peakIndex++)
                {
                    prog.SetProgress( peakIndex, source.NumRows );
                    Peak x = source.Rows[peakIndex].Peak;
                    results[peakIndex] =this.Calculate( core, source.Values[peakIndex] );
                }

                IntensityMatrix imresult = new IntensityMatrix( source.Rows, source.Columns, results );

                this.SetResults( new ResultCorrection( imresult ) );
                return true;
            }
            catch (Exception ex)
            {
                this.SetError( ex );
                return false;
            }
        }

        public IntensityMatrix Provide => Results.Matrix;
    }
}