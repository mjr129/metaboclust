using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MGui;
using MGui.Helpers;
using MSerialisers;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Classes which ProgressReporter can report to. 
    /// </summary>
    internal interface IProgressReceiver
    {
        void ReportProgressDetails(ProgressReporter.ProgInfo info);
    }

    /// <summary>
    /// Handles ProgressReporter where multiple threads do work.
    /// </summary>
    class ProgressParallelHandler
    {
        private readonly ProgressReporter _progressReporter;
        private readonly int _count;
        private int _current;

        /// <summary>
        /// Creates a ProgressParallelHandler.
        /// </summary>
        /// <param name="progressReporter">Target to report to</param>
        /// <param name="count">How many operations will take place.</param>
        public ProgressParallelHandler(ProgressReporter progressReporter, int count)
        {
            this._progressReporter = progressReporter;
            this._count = count;
        }

        /// <summary>
        /// THREAD SAFE. 
        /// Increments the progress bar by 1 (out of the initial count).
        /// </summary>
        public void SafeIncrement()
        {
            lock (this)
            {
                ++_current;
                _progressReporter.SetProgress(_current, _count);
            }
        }
    }    

    /// <summary>
    /// Contains functions for reporting progress and handling cancellation safely.
    /// </summary>
    internal class ProgressReporter
    {
        class ProgSection : IDisposable
        {
            private readonly ProgressReporter _owner;
            private bool _disposedValue = false; // To detect redundant calls

            public ProgSection( ProgressReporter owner )
            {
                _owner = owner;
            }
                                                                         
            public void Dispose()
            {
                if (!_disposedValue)
                {                      
                    _owner.Leave();
                    _disposedValue = true;
                }
            }                      
        }

        private const int MAX_UPDATE_TIME = 250;
        private readonly IProgressReceiver _destination;
        private Stack<string> _texts = new Stack<string>();
        private int _throwOnCancel = 0;
        private Stopwatch _lastUpdate = Stopwatch.StartNew();
        private int _percent;
        private string _continue;       
        private volatile bool _allowContinue;
        private bool _forceUpdate;
        private long _bytes;
        private List<LogRecord> _logs = new List<LogRecord>();

        public IReadOnlyList<LogRecord> Logs => _logs;
                    
        public class LogRecord : IIconProvider
        {
            [XColumn( EColumn.Visible )]
            public int Order;

            [XColumn( EColumn.Visible)]
            public readonly ELogLevel Level;

            [XColumn( EColumn.Visible )]
            public readonly string Message;

            public LogRecord( int order, string message, ELogLevel level )
            {
                this.Order = order;
                this.Message = message;
                this.Level = level;
            }

            Image IIconProvider.Icon
            {
                get
                {
                    switch (Level)
                    {   
                        case ELogLevel.Information: return Resources.ListIconInformation;
                        default:
                        case ELogLevel.Error: 
                        case ELogLevel.Warning: return Resources.MnuWarning;
                    }
                }
            }
        }

        public void Log( string message, ELogLevel level )
        {
            _logs.Add( new LogRecord( _logs.Count + 1, message, level ) );
        }

        /// <summary>
        /// Constructor
        /// 
        /// Creates a ProgressReporter that feeds its results back to [destination].
        /// </summary>                                                              
        public ProgressReporter(IProgressReceiver destination)
        {
            _destination = destination;
            _allowContinue = true;
        }

        /// <summary>
        /// Sets the progress bar completion amount.
        /// </summary>                   
        /// <returns>See ReportProgress.</returns>
        public bool SetProgress(int current, int max, int stage, int numStages)
        {
            return SetProgress(stage * max + current, numStages * max);
        }

        /// <summary>
        /// Sets the progress bar completion amount.
        /// </summary>                   
        /// <returns>See ReportProgress.</returns>
        public bool SetProgress(int current, int max)
        {
            return SetProgress((current * 100) / max);
        }

        /// <summary>
        /// Sets the progress bar completion amount.
        /// </summary>                   
        /// <returns>See ReportProgress.</returns>
        public bool SetProgress(int percent)
        {
            _percent = percent;

            return ReportProgress();
        }

        /// <summary>
        /// Sets the progress bar to marquee mode.
        /// </summary>                   
        /// <returns>See ReportProgress.</returns>
        public bool SetProgressMarquee()
        {
            if (_percent == -1)
            {
                return ReportProgress();
            }

            _percent = -1;
            return Update(); // force an update since marquee usually indicates we won't get another chance!
        }

        /// <summary>
        /// Calls <see cref="Enter"/> and creates an IDisposable object which calls <see cref="Leave"/> on disposal.
        /// </summary>                                                                                              
        /// <example>
        /// using (progressReporter.Section("Load data...")
        /// {
        ///     ... load some data ...
        /// }
        /// </example>
        public IDisposable Section( string text )
        {
            Enter( text );
            return new ProgSection( this );
        }

        /// <summary>
        /// Adds to the progress bar text.
        /// Must be coupled with a call to Leave to remove the text.
        /// </summary>                                              
        public bool Enter(string text)
        {
            PopContinue();
            _texts.Push(text);
            return ReportProgress();
        }

        /// <summary>
        /// Removes the last added progress bar text.
        /// Must be preceeded  with a call to Enter to add the text.
        /// </summary>              
        public void Leave()
        {
            PopContinue();
            _texts.Pop();
            ReportProgress();
        }

        /// <summary>
        /// Adds to the the progress bar text.
        /// The text is automatically removed on the next call to Enter, Leave or Continue. 
        /// </summary>                                                                     
        public bool Continue(string text)
        {
            PopContinue();
            _continue = text;
            return ReportProgress();
        }

        /// <summary>
        /// Sets the progress byte counter.
        /// </summary>                     
        public void SetBytes(long size)
        {
            PopContinue();
            _bytes = size;
            ReportProgress();
        }

        /// <summary>
        /// Forces progress reporter to update the UI.
        /// Use sparingly to avoid a UI backlog.
        /// </summary>
        public bool Update()
        {
            _forceUpdate = true;
            return ReportProgress();
        }

        /// <summary>
        /// Removes the text, if any, added by Continue.
        /// </summary>
        private void PopContinue()
        {                       
            _continue = null;
            _bytes = 0;
        }

        /// <summary>
        /// By default the progress reporter will throw a cancel exception to signal that the user
        /// has requested cancellation when the background operation attempts to report progress.
        /// 
        /// Calling this function prevents the exception being thrown and instead the progress
        /// reporting functions return false to indicate cancellation.
        /// 
        /// Call this for operations that report progress but should not be cancelled, such as
        /// file-writes, or where the background operation prefers exceptionless cancellation.
        /// </summary>
        public void DisableThrowOnCancel()
        {
            _throwOnCancel++;
        }

        /// <summary>
        /// Undoes the last call to DisableThrowOnCancel().
        /// </summary>
        public void ReenableThrowOnCancel()
        {
            _throwOnCancel--;
        }        

        /// <summary>
        /// This function is used to control the reporter by the UI and is not for reporting progress.
        /// Tells the progress reporter to cancel the operation and the next opportunity.
        /// </summary>                 
        public void SetCancelAsync(bool cancel)
        {
            _allowContinue = !cancel;
        }

        public class ProgInfo
        {
            public string Text;
            public string CText;
            public int Percent;

            public ProgInfo(string text, int percent, string bytes)
            {
                this.Text = text;
                this.Percent = percent;
                this.CText = bytes;
            }
        }

        /// <summary>
        /// Private.
        /// 
        /// The progress reporting function. Pushes progress reports out at a predefined interval.
        /// (Note continous updates cause a backlog in the UI, so this is avoided.)
        /// </summary>
        /// <returns>true to indicate continuation, false to indicate the user has requested
        /// the operation be cancelled. Unless DisableThrowOnCancel has been called the result
        /// will always be true and a TaskCanceledException will be thrown to instead indicate
        /// cancellation.</returns>
        private bool ReportProgress()
        {
            if (_lastUpdate.ElapsedMilliseconds > MAX_UPDATE_TIME || _forceUpdate)
            {
                // Update!

                // Get text
                string text = StringHelper.ArrayToString(_texts.Reverse(), " – " );

                // Get continue text
                string ctext;

                if (_continue != null)
                {
                    ctext = _continue;
                }
                else if (_bytes != 0)
                {
                    ctext = StringHelper.DisplayBytes(_bytes);
                }
                else
                {
                    ctext = null;
                }   

                _destination.ReportProgressDetails(new ProgInfo(text, _percent, ctext));

                _lastUpdate.Restart();
                _forceUpdate = false;
            }

            if (!_allowContinue && (_throwOnCancel == 0))
            {                                                        
                throw new TaskCanceledException("The task was cancelled.");
            }

            return _allowContinue;
        }       

        /// <summary>
        /// Gets a progress reporter that does nothing, use for short running tasks to avoid
        /// blocking the UI!
        /// </summary>                                 
        internal static ProgressReporter GetEmpty()
        {
            return new ProgressReporter(new EmptyProgressReporter());
        }

        /// <summary>
        ///  A progress reporter that does nothing.
        ///  Returned by the GetEmpty function.
        /// </summary>
        private class EmptyProgressReporter : IProgressReceiver
        {
            void IProgressReceiver.ReportProgressDetails(ProgInfo info)
            {
                // NA
            }
        }

        /// <summary>
        /// Creates a ProgressParallelHandler, used to report progress from multi-threaded
        /// operations.
        /// </summary>
        internal ProgressParallelHandler CreateParallelHandler(int count)
        {
            return new ProgressParallelHandler(this, count);
        }
    }
}
