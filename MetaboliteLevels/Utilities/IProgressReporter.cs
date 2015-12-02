using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Utilities
{
    internal interface IProgressReporter
    {
        void ReportProgress(string title);
        void ReportProgress(int percent);
    }

    internal class EmptyProgressReporter : IProgressReporter
    {
        public void ReportProgress(string title)
        {
            // NA
        }

        public void ReportProgress(int percent)
        {
            // NA
        }
    }

    static class ProgressReporterExtensions
    {
        public static void ReportProgress(this IProgressReporter self, int current, int max)
        {
            self.ReportProgress((current * 100) / max);
        }

        public static void ReportProgress(this IProgressReporter self)
        {
            self.ReportProgress(-1);
        }

        public static void ReportProgress(this IProgressReporter self, int current, int max, int stage, int numStages)
        {
            self.ReportProgress(stage * max + current, numStages * max);
        }
    }
}
