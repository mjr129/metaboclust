using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Corrections
{
    [Name("Correction configuration")]
    [Serializable]
    internal class ConfigurationCorrection : ConfigurationBase<AlgoBase, ArgsCorrection, ResultCorrection, SourceTracker>, IMatrixProvider
    {              
        protected override SourceTracker GetTracker()
        {
            return new SourceTracker( this.Args );
        }

        /// <summary>
        /// Like Correct(), but just gets the trend (for plots).
        /// </summary>
        internal double[] ExtractTrend(Core core, double[] raw, out IReadOnlyList<ObservationInfo> trendOrder)
        {
            if (!this.Args.IsUsingTrend)
            {
                trendOrder = null;
                return null;
            }

            var args = base.Args;
            var algo = args.GetAlgorithmOrThrow() as TrendBase;

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
                            return algo.SmoothByBatch(x, xOut, g, y, args.ToTrend());
                        }
                        else
                        {
                            IReadOnlyList<ObservationInfo> x = core.Observations;
                            IReadOnlyList<ObservationInfo> xOut = core.Observations;
                            double[] y = raw;
                            IReadOnlyList<BatchInfo> g = core.Batches;

                            trendOrder = xOut;
                            return algo.SmoothByBatch(x, xOut, g, y, args.ToTrend());
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
                        return algo.SmoothByType(x, xOut, g, y, args.ToTrend());
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

            if (!this.Args.IsUsingTrend)
            {
                var args = base.Args as ArgsCorrection;
                var algo = base.Args.GetAlgorithmOrThrow() as CorrectionBase;

                result = algo.Calculate(raw, args);
            }
            else
            {
                var args = base.Args;
                var algo = base.Args.GetAlgorithmOrThrow() as TrendBase;

                IReadOnlyList<ObservationInfo> trendOrder;
                double[] trend = this.ExtractTrend(core, raw, out trendOrder);
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

        protected override void OnRun( Core core, ProgressReporter prog )
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
        }

        protected override Image ResultIcon => Resources.ListIconResultCorrection;

        public IntensityMatrix Provide => this.Results?.Matrix;
        public ISpreadsheet ExportData()
        {
            return this.Provide?.ExportData();
        }
    }
}