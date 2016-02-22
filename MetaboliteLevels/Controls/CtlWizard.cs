using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;
using System.Text;

namespace MetaboliteLevels.Controls
{
    /// <summary>
    /// Converts a tab control into a wizard.
    /// </summary>
    public partial class CtlWizard : UserControl
    {
        //public event CancelEventHandler ValidatePage;
        private event Converter<int, bool> _permitAdvance;
        public event CancelEventHandler OkClicked;
        public event CancelEventHandler CancelClicked;
        public event EventHandler HelpClicked;

        public CtlWizard()
        {
            if (!UiControls.IsDesigning)
            {
                InitializeComponent();

                _lblOrder.BackColor = UiControls.BackColour;
                _lblOrder.ForeColor = UiControls.ForeColour;      
            }
        }

        public event Converter<int, bool> PermitAdvance
        {
            add
            {
                _permitAdvance += value;
                Revalidate();
            }
            remove
            {
                _permitAdvance -= value;
            }
        }

        public static CtlWizard BindNew(Control form, TabControl tctrl, CtlWizardOptions opts)
        {
            CtlWizard ctl = new CtlWizard();

            form.Controls.Add(ctl);

            ctl.Margin = Padding.Empty;
            ctl.Dock = DockStyle.Fill;
            ctl.Visible = true;

            ctl.Bind(tctrl, opts);

            return ctl;
        }

        private HashSet<Control> _alreadyBound = new HashSet<Control>();

        private void BindToEvents(Control ctrl, bool bind)
        {
            foreach (Control ctl in ctrl.Controls)
            {
                BindToEvents(ctl, bind);
            }

            if (_alreadyBound.Contains(ctrl))
            {
                return;
            }

            _alreadyBound.Add(ctrl);

            if (bind) ctrl.TextChanged += ctrl_Changed; else ctrl.TextChanged -= ctrl_Changed;

            if (ctrl is CheckBox)
            {
                var x = (CheckBox)ctrl;
                if (bind) x.CheckedChanged += ctrl_Changed; else x.CheckedChanged -= ctrl_Changed;
            }
            else if (ctrl is RadioButton)
            {
                var x = (RadioButton)ctrl;
                if (bind) x.CheckedChanged += ctrl_Changed; else x.CheckedChanged -= ctrl_Changed;
            }
            else if (ctrl is ComboBox)
            {
                var x = (ComboBox)ctrl;
                if (bind) x.SelectedIndexChanged += ctrl_Changed; else x.SelectedIndexChanged -= ctrl_Changed;
            }
            else if (ctrl is ListView)
            {
                var x = (ListView)ctrl;
                if (bind) x.SelectedIndexChanged += ctrl_Changed; else x.SelectedIndexChanged -= ctrl_Changed;
            }
            else if (ctrl is ListBox)
            {
                var x = (ListBox)ctrl;
                if (bind) x.SelectedIndexChanged += ctrl_Changed; else x.SelectedIndexChanged -= ctrl_Changed;
            }
        }

        void ctrl_Changed(object sender, EventArgs e)
        {
            Revalidate();
        }

        public void Bind(TabControl tctrl, CtlWizardOptions opts)
        {
            Pager = new PagerControl(ref tctrl, panel1);
            Pager.PageChanged += pc_PageChanged;
            pc_PageChanged(Pager, EventArgs.Empty);
            Options = opts;
        }

        void pc_PageChanged(object sender, EventArgs e)
        {
            SetButtonVisibilities();
            ctlTitleBar1.Text = Pager.PageTitle;
            ctlTitleBar1.SubText = Pager.PageSubTitle;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Pager.PageTitles.Count; i++ )
            {
                if (sb.Length != 0)
                {
                    sb.Append(" → ");
                }

                if (i != Pager.Page)
                {
                    sb.Append(Pager.PageTitles[i].ToSans());
                }
                else
                {
                    sb.Append(Pager.PageTitles[i].ToSansBold());
                }  
            }

            _lblOrder.Text = sb.ToString();
            _lblOrder.Visible = !Pager.PageFlag.Contains("[NOBAR]");

            Revalidate();
        }

        public bool Validate(int page)
        {
            CancelEventArgs ev = new CancelEventArgs();

            //if (ValidatePage != null)
            //{
            //    ValidatePage(this, ev);
            //}

            if (_permitAdvance != null)
            {
                try
                {
                    if (!_permitAdvance(Page))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return !ev.Cancel;
        }

        public bool Revalidate()
        {
            bool v = Validate(Page);
            _btnNext.Enabled = v;
            _btnOk.Enabled = v;
            return v;
        }

        public bool RevalidateAll()
        {
            for (int n = 0; n < Pager.PageCount; n++)
            {
                if (!Validate(n))
                {
                    return false;
                }
            }

            return true;
        }

        public int Page
        {
            get
            {
                return Pager.Page;
            }
            set
            {
                Pager.Page = value;
            }
        }

        internal PagerControl Pager { get; private set; }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            CancelEventArgs ev = new CancelEventArgs();

            if (OkClicked != null)
            {
                _btnOk.Enabled = false;
                _btnOk.Refresh();
                OkClicked(this, ev);
                _btnOk.Enabled = true;
            }

            if (_options.HasFlag(CtlWizardOptions.DialogResultOk) && !ev.Cancel)
            {
                this.ParentForm.DialogResult = DialogResult.OK;
            }
        }

        private void _btnNext_Click(object sender, EventArgs e)
        {
            Pager.NextPage();
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            Pager.PreviousPage();
        }

        private void _btnShowHelp_Click(object sender, EventArgs e)
        {
            if (HelpClicked != null)
            {
                HelpClicked(this, EventArgs.Empty);
            }
        }

        private void _btnCancel_Click_2(object sender, EventArgs e)
        {
            CancelEventArgs ev = new CancelEventArgs();

            if (CancelClicked != null)
            {
                CancelClicked(this, ev);
            }

            if (_options.HasFlag(CtlWizardOptions.DialogResultCancel) && !ev.Cancel)
            {
                this.ParentForm.DialogResult = DialogResult.Cancel;
            }
        }

        public string TitleHelpText
        {
            get { return ctlTitleBar1.HelpText; }
            set { ctlTitleBar1.HelpText = value; }
        }

        public string HelpText
        {
            get
            {
                return _btnShowHelp.Text;
            }
            set
            {
                _btnShowHelp.Text = value;
            }
        }

        CtlWizardOptions _options;

        public CtlWizardOptions Options
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
                ApplyOptions();
            }
        }

        bool _handleChanges;

        private void ApplyOptions()
        {
            SetButtonVisibilities();

            bool handleChanges = _options.HasFlag(CtlWizardOptions.HandleBasicChanges);

            if (handleChanges != _handleChanges)
            {
                BindToEvents(panel1, handleChanges);
                _handleChanges = handleChanges;
            }
        }

        private void SetButtonVisibilities()
        {
            _btnShowHelp.Visible = _options.HasFlag(CtlWizardOptions.ShowHelp);
            _btnNext.Visible = _options.HasFlag(CtlWizardOptions.ShowNext) && !Pager.IsOnLastPage;
            _btnBack.Visible = _options.HasFlag(CtlWizardOptions.ShowCancel) && !Pager.IsOnFirstPage;
            _btnCancel.Visible = _options.HasFlag(CtlWizardOptions.ShowBack) && Pager.IsOnFirstPage;
            _btnOk.Visible = Pager.IsOnLastPage;
        }
    }

    [Flags]
    public enum CtlWizardOptions
    {
        None = 0,

        ShowHelp = 1,
        ShowNext = 2,
        ShowCancel = 4,
        ShowBack = 8,

        DialogResultOk = 16,
        DialogResultCancel = 32,

        HandleBasicChanges = 64,

        /// <summary>ShowNext | ShowCancel | ShowBack</summary>
        DEFAULT = ShowNext | ShowCancel | ShowBack,
    }
}
