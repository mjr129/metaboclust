using System;
using System.ComponentModel;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Generic
{
    public partial class FrmWait : Form
    {
        private Callable function;

        private class Info : IProgressReporter
        {
            private FrmWait form;

            public Info(FrmWait frmWait)
            {
                this.form = frmWait;
            }

            public void ReportProgress(string message)
            {
                form.backgroundWorker1.ReportProgress(0, message);
            }

            public void ReportProgress(int percent)
            {
                form.backgroundWorker1.ReportProgress(percent, null);
            }
        }

        private abstract class Callable
        {
            public Exception Error;
            public object Result { get; private set; }

            internal void Invoke(Info info)
            {
                Result = OnInvoke(info);
            }

            protected abstract object OnInvoke(Info info);
        }

        private class Callable<TResult, TArgs> : Callable
        {
            public TArgs args;

            // Info and args
            public Func<Info, TArgs, TResult> InfoArgsWithResult;
            public Action<Info, TArgs> InfoArgsWithoutResult;

            // Info
            public Func<Info, TResult> InfoWithResult;
            public Action<Info> InfoWithoutResult;

            // Args
            public Func<TArgs, TResult> ArgsWithResult;
            public Action<TArgs> ArgsWithoutResult;

            // None
            public Func<TResult> WithResult;
            public Action WithoutResult;

            protected override object OnInvoke(Info info)
            {
                if (InfoArgsWithResult != null)
                {
                    return InfoArgsWithResult(info, args);
                }
                else if (InfoArgsWithoutResult != null)
                {
                    InfoArgsWithoutResult(info, args);
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
                    return ArgsWithResult(args);
                }
                else if (ArgsWithoutResult != null)
                {
                    ArgsWithoutResult(args);
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

        internal static void Show(Form owner, string title, string subtitle, Action<IProgressReporter> action)
        {
            Show(owner, title, subtitle, new Callable<NA, NA> { InfoWithoutResult = action });
        }

        internal static void Show<TArgs>(Form owner, string title, string subtitle, Action<TArgs> action, TArgs args)
        {
            Show(owner, title, subtitle, new Callable<NA, TArgs> { ArgsWithoutResult = action, args = args });
        }

        internal static void Show<TArgs>(Form owner, string title, string subtitle, Action<IProgressReporter, TArgs> action, TArgs args)
        {
            Show(owner, title, subtitle, new Callable<NA, TArgs> { InfoArgsWithoutResult = action, args = args });
        }

        internal static TResult Show<TResult>(Form owner, string title, string subtitle, Func<TResult> action)
        {
            return (TResult)Show(owner, title, subtitle, new Callable<TResult, NA> { WithResult = action });
        }

        internal static TResult Show<TResult>(Form owner, string title, string subtitle, Func<IProgressReporter, TResult> action)
        {
            return (TResult)Show(owner, title, subtitle, new Callable<TResult, NA> { InfoWithResult = action });
        }

        internal static TResult Show<TResult, TArgs>(Form owner, string title, string subtitle, Func<TArgs, TResult> action, TArgs args)
        {
            return (TResult)Show(owner, title, subtitle, new Callable<TResult, TArgs> { ArgsWithResult = action, args = args });
        }

        internal static TResult Show<TResult, TArgs>(Form owner, string title, string subtitle, Func<IProgressReporter, TArgs, TResult> action, TArgs args)
        {
            return (TResult)Show(owner, title, subtitle, new Callable<TResult, TArgs> { InfoArgsWithResult = action, args = args });
        }

        private static object Show(Form owner, string title, string subtitle, Callable callable)
        {
            using (FrmWait frm = new FrmWait(title, subtitle, callable))
            {
                UiControls.ShowWithDim(owner, frm);

                if (callable.Error != null)
                {
                    throw new Exception(callable.Error.Message, callable.Error);
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
            this.function = callable;
            UiControls.CompensateForVisualStyles(this);
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Info info = new Info(this);

            function.Invoke(info);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                ctlTitleBar1.SubText = e.UserState.ToString();
            }
            else if (e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100)
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Maximum = 100;
                progressBar1.Value = e.ProgressPercentage;
            }
            else
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                function.Error = e.Error;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
