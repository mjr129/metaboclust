using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MSerialisers;

namespace MetaboliteLevels.Utilities
{
    internal interface IProgressReporter
    {
        void ReportProgressDetails(string title, int percent);
    }

    class ProgressParallelHandler
    {
        private int count;
        private int current;
        private ProgressReporter progressReporter;

        public ProgressParallelHandler(ProgressReporter progressReporter, int count)
        {
            this.progressReporter = progressReporter;
            this.count = count;
        }

        public void SafeIncrement()
        {
            lock(this)
            {
                ++current;
                progressReporter.SetProgress(current, count);
            }
        }
    }

    internal class ProgressReporter : ISerialiserReceiver
    {
        private const int MAX_UPDATE_TIME = 250;
        private readonly IProgressReporter _destination;
        private Stack<string> _texts = new Stack<string>();
        private int _throwOnCancel = 0;
        private Stopwatch _lastUpdate = Stopwatch.StartNew();
        private Stopwatch _lastLaze = Stopwatch.StartNew();
        private int _percent;
        private bool _continue;
        private volatile bool _lazyMode;
        private volatile bool _allowContinue;

        public ProgressReporter(IProgressReporter destination)
        {
            _destination = destination;
            _allowContinue = true;
        }

        public bool SetProgress(int current, int max)
        {
            return SetProgress((current * 100) / max);
        }

        public bool SetProgressMarquee()
        {
            return SetProgress(-1);
        }

        public void DisableThrowOnCancel()
        {
            _throwOnCancel++;
        }

        public void ReenableThrowOnCancel()
        {
            _throwOnCancel--;
        }

        public bool SetProgress(int percent)
        {
            _percent = percent;

            return ReportProgress();
        }

        public void SetLazyModeAsync(bool lazy)
        {
            _lazyMode = lazy;
        }

        public void SetCancelAsync(bool cancel)
        {
            _allowContinue = !cancel;
        }

        private bool ReportProgress()
        {
            if (_lastUpdate.ElapsedMilliseconds > MAX_UPDATE_TIME)
            {
                string text = StringHelper.ArrayToString(_texts.Reverse(), " → ");

                if (_lazyMode && _lastLaze.ElapsedMilliseconds > 10000)
                {
                    _destination.ReportProgressDetails("[LAZE] " + text, _percent);
                    Thread.Sleep(1000);
                    _lastLaze.Restart();
                }

                _destination.ReportProgressDetails(text, _percent);

                _lastUpdate.Restart();
            }

            if (!_allowContinue && (_throwOnCancel == 0))
            {
                throw new TaskCanceledException("The task was cancelled.");
            }

            return _allowContinue;
        }

        public bool Enter(string text)
        {
            PopContinue();
            _texts.Push(text);
            return ReportProgressText();
        }

        private void PopContinue()
        {
            if (_continue)
            {
                _texts.Pop();
                _continue = false;
            }
        }

        public bool Continue(string text)
        {
            PopContinue();
            _continue = true;
            _texts.Push(text);
            return ReportProgressText();
        }

        public void Leave()
        {
            PopContinue();
            _texts.Pop();
            ReportProgressText();
        }

        void ISerialiserReceiver.NotifyDeserialising(IEnumerable<string> depths, string niceName)
        {
            if (_lastUpdate.ElapsedMilliseconds > MAX_UPDATE_TIME)
            {
                Continue(StringHelper.ArrayToString(depths.Reverse(), ".") + " (" + niceName + ")");
            }
        }

        private bool ReportProgressText()
        {
            return ReportProgress();
        }

        public bool SetProgress(int current, int max, int stage, int numStages)
        {
            return SetProgress(stage * max + current, numStages * max);
        }

        internal static ProgressReporter GetEmpty()
        {
            return new ProgressReporter(new EmptyProgressReporter());
        }

        private class EmptyProgressReporter : IProgressReporter
        {
            void IProgressReporter.ReportProgressDetails(string title, int percent)
            {
                // NA
            }
        }

        internal ProgressParallelHandler CreateParallelHandler(int count)
        {
            return new ProgressParallelHandler(this, count);
        }  
    }
}
