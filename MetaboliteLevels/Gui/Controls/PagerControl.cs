using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Properties;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls
{
    sealed class PagerControl
    {
        private readonly Panel _mainPanel;
        public readonly List<Panel> Pages = new List<Panel>();
        public readonly List<string> PageTitles = new List<string>();
        public readonly List<string> PageSubTitles = new List<string>();
        public readonly List<string> PageFlags = new List<string>();
        int _pageno = -1;
        Panel _current;
        public event EventHandler PageChanged;
        ToolStripButton[] _buttons;

        public PagerControl(ref TabControl tc, Panel panel = null)
        {
            if (panel != null)
            {
                this._mainPanel = panel;
            }
            else if (tc.Parent is Panel)
            {
                this._mainPanel = (Panel)tc.Parent;
            }
            else
            {
                throw new InvalidOperationException("PagerControl needs Panel parameter or the TabControl must reside in a panel.");
            }

            foreach (TabPage tabPage in tc.TabPages)
            {
                this.PageTitles.Add(tabPage.Text);

                Panel p = new Panel
                {
                    Margin = new Padding(0, 0, 0, 0),
                    Padding = new Padding(0, 0, 0, 0),
                    Dock = DockStyle.Fill,
                    Visible = false,
                    AutoScroll = true,
                };

                this._mainPanel.Controls.Add(p);
                ArrayList controls = new ArrayList(tabPage.Controls);

                foreach (Control c in controls)
                {
                    if (c.Width == 0) // fixed errors with controls autosizing to zero if they have no content or they are docked without a parent
                    {
                        c.Width = 1;
                    }

                    if (c.Height == 0)
                    {
                        c.Height = 1;
                    }

                    p.Controls.Add(c);
                }

                bool subTitle = false;
                bool flags = false;

                foreach (Label lab in FormHelper.EnumerateControls<Label>(p))
                {
                    if (lab.Text.StartsWith("^^^"))
                    {
                        this.PageFlags.Add(lab.Text.Substring(3));
                        flags = true;
                        lab.Visible = false;
                    }
                    else if (lab.Text.StartsWith("^^"))
                    {
                        this.PageSubTitles.Add(lab.Text.Substring(2));
                        subTitle = true;
                        lab.Visible = false;
                    }
                }

                if (!subTitle)
                {
                    this.PageSubTitles.Add(null);
                }

                if (!flags)
                {
                    this.PageFlags.Add("");
                }

                tabPage.Controls.Clear();
                this.Pages.Add(p);
            }

            tc.Parent.Controls.Remove(tc);
            tc.Dispose();
            tc = null;

            this.GotoPage(0);
        }

        public bool NextPage()
        {
            return this.GotoPage(this.Page + 1);
        }

        public bool PreviousPage()
        {
            return this.GotoPage(this.Page - 1);
        }

        public int PageCount
        {
            get
            {
                return this.Pages.Count;
            }
        }

        public string PageFlag
        {
            get
            {
                return this.PageFlags[this._pageno];
            }
        }

        public string PageTitle
        {
            get
            {
                return this.PageTitles[this._pageno];
            }
        }

        public string PageSubTitle
        {
            get
            {
                return this.PageSubTitles[this._pageno];
            }
        }

        public int FirstPage
        {
            get
            {
                return 0;
            }
        }

        public int LastPage
        {
            get
            {
                return this.PageCount - 1;
            }
        }

        public bool IsOnFirstPage
        {
            get
            {
                return this.Page == this.FirstPage;
            }
        }

        public bool IsOnLastPage
        {
            get
            {
                return this.Page == this.LastPage;
            }
        }

        public int Page
        {
            get
            {
                return this._pageno;
            }
            set
            {
                this.GotoPage(value);
            }
        }  

        public bool GotoPage(int p)
        {
            if (p >= 0 && p < this.Pages.Count)
            {
                if (p == this._pageno)
                {
                    return true;
                }

                this.OnPageChanging();

                this._mainPanel.SuspendDrawingAndLayout();

                if (this._current != null)
                {
                    this._current.Visible = false;
                }

                this._pageno = p;
                this._current = this.Pages[p];
                this._current.Visible = true;
                this.OnPageChanged(EventArgs.Empty);

                this._mainPanel.ResumeDrawingAndLayout();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnPageChanging()
        {
            this._mainPanel.SuspendLayout();

            if (this._buttons != null)
            {
                this._buttons[this.Page].BackgroundImage = Resources.TabUnsel;
            }
        }

        /// <summary>
        /// Raises the PageChanged event.
        /// </summary>
        private void OnPageChanged(EventArgs e)
        {
            this._mainPanel.ResumeLayout();

            if (this._buttons != null)
            {
                this._buttons[this.Page].BackgroundImage = Resources.TabSel;
            }

            if (this.PageChanged != null)
            {
                this.PageChanged(this, e);
            }
        }

        internal void BindToButtons(params ToolStripButton[] buttons)
        {
            this._buttons = buttons;

            for (int n = 0; n < buttons.Length; n++)
            {
                var btn = buttons[n];
                btn.Tag = n;
                btn.Click += this.btn_Click;
                btn.BackgroundImage = Resources.TabUnsel;
            }

            this.OnPageChanged(EventArgs.Empty);
        }

        void btn_Click(object sender, EventArgs e)
        {
            var control = (ToolStripButton)sender;
            int tag = (int)control.Tag;
            this.Page = tag;
        }
    }
}
