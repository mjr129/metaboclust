using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Activities
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
                _form.backgroundWorker1.ReportProgress(0, info);
            }
        }

        private abstract class Callable
        {
            public Exception Error;
            public object Result { get; private set; }

            internal void Invoke(ProgressReporter info)
            {
                Result = OnInvoke(info);
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
                if (InfoArgsWithResult != null)
                {
                    return InfoArgsWithResult(info, _args);
                }
                else if (InfoArgsWithoutResult != null)
                {
                    InfoArgsWithoutResult(info, _args);
                    return null;
                }
                else if (InfoWithResult != null)
                {
                    return InfoWithResult(info);
                }
                else if (InfoWithoutResult != null)
                {
                    InfoWithoutResult(info);
                    return null;
                }
                else if (ArgsWithResult != null)
                {
                    return ArgsWithResult(_args);
                }
                else if (ArgsWithoutResult != null)
                {
                    ArgsWithoutResult(_args);
                    return null;
                }
                else if (WithResult != null)
                {
                    return WithResult();
                }
                else if (WithoutResult != null)
                {
                    WithoutResult();
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

                if (frm._prog.Logs.Count == 1)
                {
                    var l = frm._prog.Logs[0];
                    FrmMsgBox.Show( owner, l.Level, l.Message );
                }
                else if (frm._prog.Logs.Count != 0)
                {
                    StringBuilder sb = new StringBuilder();

                    DataSet<ProgressReporter.LogRecord> ds = new DataSet<ProgressReporter.LogRecord>()
                    {
                        ListSource = frm._prog.Logs,
                        ListTitle = "Logs",
                    };

                    ds.ShowListEditor( owner, Editing.FrmBigList.EShow.ReadOnly, null );
                }

                return callable.Result;
            }
        }

        public FrmWait()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmWait(string title, string subtitle, Callable callable)
            : this()
        {
            this.ctlTitleBar1.Text = title;
            this.ctlTitleBar1.SubText = subtitle;
            this._function = callable;                           
            // UiControls.CompensateForVisualStyles(this);
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _info = new Info(this);
            _thread = Thread.CurrentThread;
            _prog = new ProgressReporter(_info);
            _function.Invoke(_prog);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!_allowClose)
            {
                MessageBox.Show( this, "Cannot close the window while the task is still running." );                        
                e.Cancel = true;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressReporter.ProgInfo info = (ProgressReporter.ProgInfo)e.UserState;

            ctlTitleBar1.SubText = info.Text;

            if (info.Percent >= 0)
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Maximum = 100;
                progressBar1.Value = info.Percent;
            }
            else
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
            }

            label1.Text = StringHelper.TimeAsString(_operationTimer.Elapsed);

            if (info.CText != null)
            {
                label2.Text = info.CText;
                label2.Visible = true;
            }
            else
            {
                label2.Visible = false;
            }  
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _allowClose = true;

            if (e.Error != null)
            {
                _function.Error = e.Error;
            }

            DialogResult = DialogResult.OK;
        }      

        private void _chkSuspend_CheckedChanged(object sender, EventArgs e)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            if (_chkSuspend.Checked)
            {
                _thread.Suspend();
            }
            else
            {
                _thread.Resume();
            }
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private void _chkDeprioritise_CheckedChanged(object sender, EventArgs e)
        {
            _chkPrioritise.Enabled = !_chkDeprioritise.Checked;

            if (_chkDeprioritise.Checked)
            {
                _thread.Priority = ThreadPriority.Lowest;
            }
            else
            {
                _thread.Priority = ThreadPriority.Normal;
            }
        }

        private void _chkPrioritise_CheckedChanged(object sender, EventArgs e)
        {
            _chkDeprioritise.Enabled = !_chkPrioritise.Checked;

            if (_chkPrioritise.Checked)
            {
                _thread.Priority = ThreadPriority.Highest;
            }
            else
            {
                _thread.Priority = ThreadPriority.Normal;
            }

        }    

        private void ctlTitleBar1_HelpClicked( object sender, CancelEventArgs e )
        {
            if (FrmMsgBox.ShowYesNo( this, "Cancel", "Are you sure you wish to cancel the task?" ))
            {
                _prog.SetCancelAsync( true );
            }
        }

        private void label1_Click( object sender, EventArgs e )
        {

        }

        private void label1_DoubleClick( object sender, EventArgs e )
        {
            flowLayoutPanel1.Visible = true;
        }
    }
}
