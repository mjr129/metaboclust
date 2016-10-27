using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls
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
                this.InitializeComponent();

                this._lblOrder.BackColor = UiControls.TitleBackColour;
                this._lblOrder.ForeColor = UiControls.TitleForeColour;      
            }
        }

        public event Converter<int, bool> PermitAdvance
        {
            add
            {
                this._permitAdvance += value;
                this.Revalidate();
            }
            remove
            {
                this._permitAdvance -= value;
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
                this.BindToEvents(ctl, bind);
            }

            if (this._alreadyBound.Contains(ctrl))
            {
                return;
            }

            this._alreadyBound.Add(ctrl);

            if (bind) ctrl.TextChanged += this.ctrl_Changed; else ctrl.TextChanged -= this.ctrl_Changed;

            if (ctrl is CheckBox)
            {
                var x = (CheckBox)ctrl;
                if (bind) x.CheckedChanged += this.ctrl_Changed; else x.CheckedChanged -= this.ctrl_Changed;
            }
            else if (ctrl is RadioButton)
            {
                var x = (RadioButton)ctrl;
                if (bind) x.CheckedChanged += this.ctrl_Changed; else x.CheckedChanged -= this.ctrl_Changed;
            }
            else if (ctrl is ComboBox)
            {
                var x = (ComboBox)ctrl;
                if (bind) x.SelectedIndexChanged += this.ctrl_Changed; else x.SelectedIndexChanged -= this.ctrl_Changed;
            }
            else if (ctrl is ListView)
            {
                var x = (ListView)ctrl;
                if (bind) x.SelectedIndexChanged += this.ctrl_Changed; else x.SelectedIndexChanged -= this.ctrl_Changed;
            }
            else if (ctrl is ListBox)
            {
                var x = (ListBox)ctrl;
                if (bind) x.SelectedIndexChanged += this.ctrl_Changed; else x.SelectedIndexChanged -= this.ctrl_Changed;
            }
        }

        void ctrl_Changed(object sender, EventArgs e)
        {
            this.Revalidate();
        }

        public void Bind(TabControl tctrl, CtlWizardOptions opts)
        {
            this.Pager = new PagerControl(ref tctrl, this.panel1);
            this.Pager.PageChanged += this.pc_PageChanged;
            this.pc_PageChanged(this.Pager, EventArgs.Empty);
            this.Options = opts;
        }

        void pc_PageChanged(object sender, EventArgs e)
        {
            this.SetButtonVisibilities();
            this.ctlTitleBar1.Text = this.Pager.PageTitle;
            this.ctlTitleBar1.SubText = this.Pager.PageSubTitle;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.Pager.PageTitles.Count; i++ )
            {
                if (sb.Length != 0)
                {
                    sb.Append(" → ");
                }

                if (i != this.Pager.Page)
                {
                    sb.Append(this.Pager.PageTitles[i].ToSans());
                }
                else
                {
                    sb.Append(this.Pager.PageTitles[i].ToSansBold());
                }  
            }

            this._lblOrder.Text = sb.ToString();
            this._lblOrder.Visible = !this.Pager.PageFlag.Contains("[NOBAR]");
            this.ctlTitleBar1.DrawHBar = !this._lblOrder.Visible;

            this.Revalidate();
        }

        public bool Validate(int page)
        {
            CancelEventArgs ev = new CancelEventArgs();

            //if (ValidatePage != null)
            //{
            //    ValidatePage(this, ev);
            //}

            if (this._permitAdvance != null)
            {
                try
                {
                    if (!this._permitAdvance(this.Page))
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
            bool v = this.Validate(this.Page);
            this._btnNext.Enabled = v;
            this._btnOk.Enabled = v;
            return v;
        }

        public bool RevalidateAll()
        {
            for (int n = 0; n < this.Pager.PageCount; n++)
            {
                if (!this.Validate(n))
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
                return this.Pager.Page;
            }
            set
            {
                this.Pager.Page = value;
            }
        }

        internal PagerControl Pager { get; private set; }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            CancelEventArgs ev = new CancelEventArgs();

            if (this.OkClicked != null)
            {
                this._btnOk.Enabled = false;
                this._btnOk.Refresh();
                this.OkClicked(this, ev);
                this._btnOk.Enabled = true;
            }

            if (this._options.HasFlag(CtlWizardOptions.DialogResultOk) && !ev.Cancel)
            {
                this.ParentForm.DialogResult = DialogResult.OK;
            }
        }

        private void _btnNext_Click(object sender, EventArgs e)
        {
            this.Pager.NextPage();
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            this.Pager.PreviousPage();
        }   

        private void _btnCancel_Click_2(object sender, EventArgs e)
        {
            CancelEventArgs ev = new CancelEventArgs();

            if (this.CancelClicked != null)
            {
                this.CancelClicked(this, ev);
            }

            if (this._options.HasFlag(CtlWizardOptions.DialogResultCancel) && !ev.Cancel)
            {
                this.ParentForm.DialogResult = DialogResult.Cancel;
            }
        }

        public string TitleHelpText
        {
            get { return this.ctlTitleBar1.HelpText; }
            set { this.ctlTitleBar1.HelpText = value; }
        }   

        public CtlTitleBar.EHelpIcon TitleHelpIcon
        {
            get
            {
                return this.ctlTitleBar1.HelpIcon;
            }
            set
            {
                this.ctlTitleBar1.HelpIcon = value;
            }
        }

        CtlWizardOptions _options;

        public CtlWizardOptions Options
        {
            get
            {
                return this._options;
            }
            set
            {
                this._options = value;
                this.ApplyOptions();
            }
        }

        bool _handleChanges;

        private void ApplyOptions()
        {
            this.SetButtonVisibilities();

            bool handleChanges = this._options.HasFlag(CtlWizardOptions.HandleBasicChanges);

            if (handleChanges != this._handleChanges)
            {
                this.BindToEvents(this.panel1, handleChanges);
                this._handleChanges = handleChanges;
            }
        }

        private void SetButtonVisibilities()
        {                                                                         
            this._btnNext.Visible = this._options.HasFlag(CtlWizardOptions.ShowNext) && !this.Pager.IsOnLastPage;
            this._btnBack.Visible = this._options.HasFlag(CtlWizardOptions.ShowCancel) && !this.Pager.IsOnFirstPage;
            this._btnCancel.Visible = this._options.HasFlag(CtlWizardOptions.ShowBack) && this.Pager.IsOnFirstPage;
            this._btnOk.Visible = this.Pager.IsOnLastPage;
        }

        private void _lblOrder_Paint( object sender, PaintEventArgs e )
        {
            UiControls.DrawHBar( e.Graphics, this._lblOrder );
        }

        private void ctlTitleBar1_HelpClicked( object sender, CancelEventArgs e )
        {
            this.HelpClicked?.Invoke( this, EventArgs.Empty );
        }
    }

    [Flags]
    public enum CtlWizardOptions
    {
        None = 0,
                     
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
