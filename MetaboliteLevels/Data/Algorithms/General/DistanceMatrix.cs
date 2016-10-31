using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// Represents a distance matrix.
    /// </summary>
    [Serializable]
    internal class DistanceMatrix
    {
        public readonly double[,] Values;           // Vector-vector distances
        public readonly IntensityMatrix ValueMatrix;    // The original vectors

        /// <summary>
        /// Constructor.
        /// </summary>  
        public DistanceMatrix(double[,] values, IntensityMatrix peaks )
        {
            this.Values = values;
            this.ValueMatrix = peaks;
        }    

        /// <summary>
        /// Returns the distance matrix for a set of peaks.
        /// </summary>
        public static DistanceMatrix Create(Core core, IntensityMatrix valueMatrix, ConfigurationMetric metric, ProgressReporter prog)
        {
            int n = valueMatrix.NumRows;

            int bytesRequired = (n * n) * 8;
            int mbRequired = bytesRequired / (1024 * 1024);
            int limit = core.Options.ObjectSizeLimit * 1024 * 1024;

            if (bytesRequired > limit)
            {
                // It ain't gonna happen. I'm not creating a distance matrix over 500Mb
                throw new InvalidOperationException("n = " + n + " input vectors. n * n = " + (n * n) + " elements in the distance matrix. 8 bytes per element gives " + (n * n * 8) + " bytes, or " + mbRequired + " megabytes. ObjectSizeLimit is " + core.Options.ObjectSizeLimit + " Mb. Reduce the number of input vectors by filtering the data, or change the limit from the preferences menu. Some algorithms also have the option to disable the distance matrix.");
            }

            double[,] s = new double[n, n];
            prog.Enter("Calculating distance matrix");

            for (int i = 0; i < n; i++)
            {
                prog.SetProgress(i, n);

                for (int j = 0; j < n; j++)
                {
                    s[i, j] = metric.Calculate(valueMatrix.Vectors[i], valueMatrix.Vectors[j]);
                }
            }

            prog.Leave();

            return new DistanceMatrix(s, valueMatrix);
        }

        /// <summary>
        /// Finds two vectors and returns the value.
        /// </summary>                              
        internal double Find(Peak vector1Peak, GroupInfo vector1Group, Peak vector2Peak, GroupInfo vector2Group)
        {
            IntensityMatrix.RowHeader h1 = new IntensityMatrix.RowHeader( vector1Peak, vector1Group );
            IntensityMatrix.RowHeader h2 = new IntensityMatrix.RowHeader( vector2Peak, vector2Group );

            int i = ValueMatrix.FindIndex(h1);
            int j = ValueMatrix.FindIndex(h2);
            return Values[i, j];
        }
    }
}
