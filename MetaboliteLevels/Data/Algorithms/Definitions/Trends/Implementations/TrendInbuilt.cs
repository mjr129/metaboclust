using System.Collections.Generic;
using System.Linq;

namespace MetaboliteLevels.Algorithms.Statistics.Trends
{
    /// <summary>
    /// Base class for Trends which provides a nice wrapper to smooth each point individually.
    /// </summary>
    abstract class TrendInbuilt : TrendBase
    {
        public TrendInbuilt(string id, string name)
            : base(id, name)
        {
            // NA
        }

        protected sealed override double[] Smooth(IEnumerable<double> y, IEnumerable<int> xIn, IEnumerable<int> xOut, object[] args)
        {
            object arg = InterpretArgs(args);
            int[] xTarget = xOut.ToArray();
            double[] yIn = y.ToArray();
            double[] yOut = new double[xTarget.Length];

            for (int i = 0; i < xTarget.Length; i++)
            {
                yOut[i] = SmoothPoint(xIn, yIn, xTarget[i], arg);
            }

            return yOut;
        }

        /// <summary>
        /// Gets the smoothed value of the point.
        /// </summary>
        /// <param name="x">X coords</param>
        /// <param name="y">Y coords</param>
        /// <param name="xTarget">Index of point to smooth</param>
        /// <param name="arg">Argument from InterpretArgs</param>
        /// <returns>Smoothed value</returns>
        protected abstract double SmoothPoint(IEnumerable<int> x, double[] y, int xTarget, object arg);

        /// <summary>
        /// Interprets arguments (so complex arguments don't have to be decoded for every call to SmoothPoint).
        /// </summary>
        /// <param name="args">Input arguments</param>
        /// <returns>Refined arguments</returns>
        protected virtual object InterpretArgs(object[] args) { return null; }
    }
}
