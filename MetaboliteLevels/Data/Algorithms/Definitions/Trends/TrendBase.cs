using System;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Algorithms.Statistics.Trends
{
    /// <summary>
    /// Base class for trends.
    /// 
    /// Trends calculate a trend-line (a double[]) containing a "Y" for each "X" from a set of inputs (which may or may not have multiple "Y"'s per "X").
    /// 
    /// Typically: x = time and y = intensity for calculating trends with time , but this can also be x = acquisition order and y = intensity, for batch correction.
    /// </summary>
    abstract class TrendBase : AlgoBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public TrendBase(string id, string name)
            : base(id, name)
        {
            // NA
        }

        /// <summary>
        /// Gets parameters.
        /// </summary>
        protected abstract override AlgoParameterCollection CreateParamaterDesription();

        /// <summary>
        /// Smooths input data by type with x = time.
        /// </summary>
        internal double[] SmoothByType(IReadOnlyList<ObservationInfo> inputOrder, IReadOnlyList<ConditionInfo> outputOrder, IReadOnlyList<GroupInfo> groups, double[] raw, ArgsTrend a)
        {
            return this.SmoothByType(inputOrder, outputOrder, groups, raw, a.Parameters);
        }

        /// <summary>
        /// Smooths input data by type with x = time.
        /// </summary>
        protected double[] SmoothByType(IReadOnlyList<ObservationInfo> inputOrder, IReadOnlyList<ConditionInfo> outputOrder, IReadOnlyList<GroupInfo> groups, double[] raw, object[] args)
        {
            double[] r = new double[outputOrder.Count];

            foreach (GroupInfo g in groups)
            {
                // Get indices for this TYPE
                IEnumerable<int> xI = inputOrder.Which(z => z.Group == g);
                IEnumerable<int> xOutI = outputOrder.Which(z => z.Group == g);

                // Get obs/cond/values for this TYPE
                IEnumerable<int> x = inputOrder.At( xI).Select(z => z.Time);
                IEnumerable<int> xOut = outputOrder.At( xOutI).Select(z => z.Time);
                IEnumerable<double> y = raw.At( xI);

                // Smooth this line
                double[] yOut = Smooth(y, x, xOut, args);

                // Replace the values
                r.ReplaceIn(xOutI, yOut);
            }

            return r;
        }

        /// <summary>
        /// Smooths data by batch with x = acquisition order.
        /// </summary>
        internal double[] SmoothByBatch(IReadOnlyList<ObservationInfo> x, IReadOnlyList<ObservationInfo> xOut, IEnumerable<BatchInfo> batches, double[] raw, ArgsTrend a)
        {
            return this.SmoothByBatch(x, xOut, batches, raw, a.Parameters);
        }

        /// <summary>
        /// Smooths data by batch with x = acquisition order.
        /// </summary>
        protected double[] SmoothByBatch(IReadOnlyList<ObservationInfo> inputOrder, IReadOnlyList<ObservationInfo> outputOrder, IEnumerable<BatchInfo> batches, double[] raw, object[] args)
        {
            double[] r = new double[outputOrder.Count];

            foreach (BatchInfo b in batches)
            {
                // Get indices for this BATCH
                IEnumerable<int> xI = inputOrder.Which(z => z.Batch == b);
                IEnumerable<int> xOutI = outputOrder.Which(z => z.Batch == b);

                // Get obs/cond/values for this BATCH
                IEnumerable<int> x = inputOrder.At( xI).Select(z => z.Acquisition);
                IEnumerable<int> xOut = outputOrder.At( xOutI).Select(z => z.Acquisition);
                IEnumerable<double> y = raw.At( xI);

                // Smooth this line
                double[] yOut = Smooth(y, x, xOut, args);

                // Replace the values
                r.ReplaceIn(xOutI, yOut);
            }

            return r;
        }

        /// <summary>
        /// Smooths data by TGroup with specified x
        /// </summary>
        // TODO: Is this ever needed?!
        protected double[] GenericSmooth<TIn, TOut, TGroup>(TIn[] inputOrder, TOut[] outputOrder, IEnumerable<TGroup> batches, double[] raw, object[] args, Converter<TIn, TGroup> getGroupIn, Converter<TOut, TGroup> getGroupOut, Converter<TIn, int> getXIn, Converter<TOut, int> getXOut)
        {
            double[] r = new double[outputOrder.Length];

            foreach (TGroup b in batches)
            {
                // Get indices for this BATCH
                IEnumerable<int> xI = inputOrder.Which(z => getGroupIn(z).Equals(b));
                IEnumerable<int> xOutI = outputOrder.Which(z => getGroupOut(z).Equals(b));

                // Get obs/cond/values for this BATCH
                IEnumerable<int> x = inputOrder.At( xI).Select(z => getXIn(z));
                IEnumerable<int> xOut = outputOrder.At( xOutI).Select(z => getXOut(z));
                IEnumerable<double> y = raw.At( xI);

                // Smooth this line
                double[] yOut = Smooth(y, x, xOut, args);

                // Replace the values
                r.ReplaceIn(xOutI, yOut);
            }

            return r;
        }

        /// <summary>
        /// Performs actual smoothing
        /// </summary>
        /// <param name="y">Y of inputs</param>
        /// <param name="x">X of inputs (x.len = y.len)</param>
        /// <param name="xOut">X of outputs</param>
        /// <param name="args">Algorithm parameters as requested by GetParams()</param>
        /// <returns>yOut: Y of outputs (yOut.len = xOut.len)</returns>
        protected abstract double[] Smooth(IEnumerable<double> y, IEnumerable<int> x, IEnumerable<int> xOut, object[] args);
    }
}
