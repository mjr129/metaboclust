using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Gui.Forms.Editing;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    /// <summary>
    /// The waiting dialogue, containing a progress bar and cancel button.
    /// 
    /// The task to be performed is passed into <see cref="Show"/>.
    /// Usage: FrmWait.Show( ... )
    /// </summary>
    public partial class FrmWait : Form
    {
        private Callable _function;
        private Stopwatch _operationTimer = Stopwatch.StartNew();
        private bool _allowClose;
        private Thread _thread;
        private Info _info;
        private ProgressReporter _prog;

        private class Info : IProgressReceiver
        {
            private readonly FrmWait _form;                                  

            public Info(FrmWait frmWait)
            {
                this._form = frmWait;
            }

            void IProgressReceiver.ReportProgressDetails(ProgressReporter.ProgInfo info)
            {
                this._form.backgroundWorker1.ReportProgress(0, info);
            }
        }

        private abstract class Callable
        {
            public Exception Error;
            public object Result { get; private set; }

            internal void Invoke(ProgressReporter info)
            {
                this.Result = this.OnInvoke(info);
            }

            protected abstract object OnInvoke(ProgressReporter info);
        }

        private class Callable<TResult, TArgs> : Callable
        {
            public TArgs _args;

            // Info and args
            public Func<ProgressReporter, TArgs, TResult> InfoArgsWithResult;
            public Action<ProgressReporter, TArgs> InfoArgsWithoutResult;

            // Info
            public Func<ProgressReporter, TResult> InfoWithResult;
            public Action<ProgressReporter> InfoWithoutResult;

            // Args
            public Func<TArgs, TResult> ArgsWithResult;
            public Action<TArgs> ArgsWithoutResult;

            // None
            public Func<TResult> WithResult;
            public Action WithoutResult;

            protected override object OnInvoke(ProgressReporter info)
            {
                if (this.InfoArgsWithResult != null)
                {
                    return this.InfoArgsWithResult(info, this._args);
                }
                else if (this.InfoArgsWithoutResult != null)
                {
                    this.InfoArgsWithoutResult(info, this._args);
                    return null;
                }
                else if (this.InfoWithResult != null)
                {
                    return this.InfoWithResult(info);
                }
                else if (this.InfoWithoutResult != null)
                {
                    this.InfoWithoutResult(info);
                    return null;
                }
                else if (this.ArgsWithResult != null)
                {
                    return this.ArgsWithResult(this._args);
                }
                else if (this.ArgsWithoutResult != null)
                {
                    this.ArgsWithoutResult(this._args);
                    return null;
                }
                else if (this.WithResult != null)
                {
                    return this.WithResult();
                }
                else if (this.WithoutResult != null)
                {
                    this.WithoutResult();
                    return null;
                }
                else
                {
                    throw new InvalidOperationException("No delegate provided.");
                }
            }
        }

        private class NA
        {
        }

        internal static void Show(Form owner, string title, string subtitle, Action action)
        {
            Show(owner, title, subtitle, new Callable<NA, NA> { WithoutResult = action });
        }

        internal static void Show(Form owner, string title, string subtitle, Action<ProgressReporter> action)
        {
            Show(owner, title, subtitle, new Callable<NA, NA> { InfoWithoutResult = action });
        }

        internal static void Show<TArgs>(Form owner, string title, string subtitle, Action<TArgs> action, TArgs args)
        {
            Show(owner, title, subtitle, new Callable<NA, TArgs> { ArgsWithoutResult = action, _args = args });
        }

        internal static void Show<TArgs>(Form owner, string title, string subtitle, Action<ProgressReporter, TArgs> action, TArgs args)
        {
            Show(owner, title, subtitle, new Callable<NA, TArgs> { InfoArgsWithoutResult = action, _args = args });
        }

        internal static TResult Show<TResult>(Form owner, string title, string subtitle, Func<TResult> action)
        {
            return (TResult)Show(owner, title, subtitle, new Callable<TResult, NA> { WithResult = action });
        }

        internal static TResult Show<TResult>(Form owner, string title, string subtitle, Func<ProgressReporter, TResult> action)
        {
            return (TResult)Show(owner, title, subtitle, new Callable<TResult, NA> { InfoWithResult = action });
        }

        internal static TResult Show<TResult, TArgs>(Form owner, string title, string subtitle, Func<TArgs, TResult> action, TArgs args)
        {
            return (TResult)Show(owner, title, subtitle, new Callable<TResult, TArgs> { ArgsWithResult = action, _args = args });
        }

        internal static TResult Show<TResult, TArgs>(Form owner, string title, string subtitle, Func<ProgressReporter, TArgs, TResult> action, TArgs args)
        {
            return (TResult)Show(owner, title, subtitle, new Callable<TResult, TArgs> { InfoArgsWithResult = action, _args = args });
        }

        private static object Show(Form owner, string title, string subtitle, Callable callable)
        {
            using (FrmWait frm = new FrmWait(title, subtitle, callable))
            {
                UiControls.ShowWithDim(owner, frm);
                  
                if (callable.Error != null)
                {
                    if (callable.Error is TaskCanceledException)
                    {
                        throw new TaskCanceledException( "The task was cancelled.", callable.Error );
                    }
                    else
                    {
                        throw new InvalidOperationException( "The background process encountered an error: " + callable.Error.Message, callable.Error );
                    }
                }

                ShowLogMessage( owner, frm._prog.Logs );

                return callable.Result;
            }
        }

        /// <summary>
        /// Shows a message describing the logs (if any)
        /// </summary>                                  
        private static void ShowLogMessage( Form owner, IReadOnlyList<ProgressReporter.LogRecord> logs )
        {
            if (logs.Count == 1)
            {
                // One line messagebox if one log
                var l = logs[0];
                FrmMsgBox.Show( owner, l.Level, l.Message );
            }
            else if (logs.Count != 0)
            {
                // Messagebox with "details" buttons for multiple logs
                // (Messagebox icon reflects the highest log level)
                StringBuilder sb = new StringBuilder();

                DataSet<ProgressReporter.LogRecord> ds = new DataSet<ProgressReporter.LogRecord>()
                {
                    ListSource = logs,
                    ListTitle = "Logs",
                    HandleEdit = DisplayLog,
                };

                ELogLevel max = logs.Max( z => z.Level );
                string msg;
                MsgBoxButton[] bts;

                if (max == ELogLevel.Information)
                {
                    msg = "Update complete.";
                    bts = new MsgBoxButton[] { new MsgBoxButton( DialogResult.OK ),
                                                new MsgBoxButton( "Details", Resources.MnuNext, DialogResult.Cancel ) };
                }
                else
                {
                    msg = $"One or more {max.ToUiString().ToLower()}s were reported.";
                    bts = new MsgBoxButton[] { new MsgBoxButton( "Details", Resources.MnuNext, DialogResult.Cancel ),
                                                new MsgBoxButton( "Ignore", Resources.MnuCancel, DialogResult.OK) };
                }

                if (FrmMsgBox.Show( owner, max.ToUiString(), null, msg, FrmMsgBox.GetIcon( max ), bts ) == DialogResult.Cancel)
                {
                    ds.ShowListEditor( owner, FrmBigList.EShow.ReadOnly, null );
                }
            }
        }

        private static ProgressReporter.LogRecord DisplayLog( DataSet<ProgressReporter.LogRecord>.EditItemArgs input )
        {
            if (input.DefaultValue != null)
            {
                FrmMsgBox.Show( input.Owner, input.DefaultValue.Level, input.DefaultValue.Message );
            }

            return null;
        }

        public FrmWait()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmWait(string title, string subtitle, Callable callable)
            : this()
        {
            this.ctlTitleBar1.Text = title;
            this.ctlTitleBar1.SubText = subtitle;
            this._function = callable;                           
            // UiControls.CompensateForVisualStyles(this);
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this._info = new Info(this);
            this._thread = Thread.CurrentThread;
            this._prog = new ProgressReporter(this._info);
            this._function.Invoke(this._prog);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!this._allowClose)
            {
                MessageBox.Show( this, "Cannot close the window while the task is still running." );                        
                e.Cancel = true;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressReporter.ProgInfo info = (ProgressReporter.ProgInfo)e.UserState;

            this.ctlTitleBar1.SubText = info.Text;

            if (info.Percent >= 0)
            {
                this.progressBar1.Style = ProgressBarStyle.Continuous;
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = info.Percent;
            }
            else
            {
                this.progressBar1.Style = ProgressBarStyle.Marquee;
            }

            this.label1.Text = StringHelper.TimeAsString(this._operationTimer.Elapsed);

            if (info.CText != null)
            {
                this.label2.Text = info.CText;
                this.label2.Visible = true;
            }
            else
            {
                this.label2.Visible = false;
            }  
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this._allowClose = true;

            if (e.Error != null)
            {
                this._function.Error = e.Error;
            }

            this.DialogResult = DialogResult.OK;
        }      

        private void _chkSuspend_CheckedChanged(object sender, EventArgs e)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            if (this._chkSuspend.Checked)
            {
                this._thread.Suspend();
            }
            else
            {
                this._thread.Resume();
            }
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private void _chkDeprioritise_CheckedChanged(object sender, EventArgs e)
        {
            this._chkPrioritise.Enabled = !this._chkDeprioritise.Checked;

            if (this._chkDeprioritise.Checked)
            {
                this._thread.Priority = ThreadPriority.Lowest;
            }
            else
            {
                this._thread.Priority = ThreadPriority.Normal;
            }
        }

        private void _chkPrioritise_CheckedChanged(object sender, EventArgs e)
        {
            this._chkDeprioritise.Enabled = !this._chkPrioritise.Checked;

            if (this._chkPrioritise.Checked)
            {
                this._thread.Priority = ThreadPriority.Highest;
            }
            else
            {
                this._thread.Priority = ThreadPriority.Normal;
            }

        }    

        private void ctlTitleBar1_HelpClicked( object sender, CancelEventArgs e )
        {
            if (FrmMsgBox.ShowYesNo( this, "Cancel", "Are you sure you wish to cancel the task?" ))
            {
                this._prog.SetCancelAsync( true );
            }
        }

        private void label1_Click( object sender, EventArgs e )
        {

        }

        private void label1_DoubleClick( object sender, EventArgs e )
        {
            this.flowLayoutPanel1.Visible = true;
        }
    }
}
