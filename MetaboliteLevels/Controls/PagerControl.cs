using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Controls
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
                _mainPanel = panel;
            }
            else if (tc.Parent is Panel)
            {
                _mainPanel = (Panel)tc.Parent;
            }
            else
            {
                throw new InvalidOperationException("PagerControl needs Panel parameter or the TabControl must reside in a panel.");
            }

            foreach (TabPage tabPage in tc.TabPages)
            {
                PageTitles.Add(tabPage.Text);

                Panel p = new Panel
                {
                    Margin = new Padding(0, 0, 0, 0),
                    Padding = new Padding(0, 0, 0, 0),
                    Dock = DockStyle.Fill,
                    Visible = false,
                    AutoScroll = true,
                };

                _mainPanel.Controls.Add(p);
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
                        PageFlags.Add(lab.Text.Substring(3));
                        flags = true;
                        lab.Visible = false;
                    }
                    else if (lab.Text.StartsWith("^^"))
                    {
                        PageSubTitles.Add(lab.Text.Substring(2));
                        subTitle = true;
                        lab.Visible = false;
                    }
                }

                if (!subTitle)
                {
                    PageSubTitles.Add(null);
                }

                if (!flags)
                {
                    PageFlags.Add("");
                }

                tabPage.Controls.Clear();
                Pages.Add(p);
            }

            tc.Parent.Controls.Remove(tc);
            tc.Dispose();
            tc = null;

            GotoPage(0);
        }

        public bool NextPage()
        {
            return GotoPage(Page + 1);
        }

        public bool PreviousPage()
        {
            return GotoPage(Page - 1);
        }

        public int PageCount
        {
            get
            {
                return Pages.Count;
            }
        }

        public string PageFlag
        {
            get
            {
                return PageFlags[_pageno];
            }
        }

        public string PageTitle
        {
            get
            {
                return PageTitles[_pageno];
            }
        }

        public string PageSubTitle
        {
            get
            {
                return PageSubTitles[_pageno];
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
                return PageCount - 1;
            }
        }

        public bool IsOnFirstPage
        {
            get
            {
                return Page == FirstPage;
            }
        }

        public bool IsOnLastPage
        {
            get
            {
                return Page == LastPage;
            }
        }

        public int Page
        {
            get
            {
                return _pageno;
            }
            set
            {
                GotoPage(value);
            }
        }  

        public bool GotoPage(int p)
        {
            if (p >= 0 && p < Pages.Count)
            {
                if (p == _pageno)
                {
                    return true;
                }

                OnPageChanging();

                _mainPanel.SuspendDrawingAndLayout();

                if (_current != null)
                {
                    _current.Visible = false;
                }

                _pageno = p;
                _current = Pages[p];
                _current.Visible = true;
                OnPageChanged(EventArgs.Empty);

                _mainPanel.ResumeDrawingAndLayout();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnPageChanging()
        {
            _mainPanel.SuspendLayout();

            if (_buttons != null)
            {
                _buttons[Page].BackgroundImage = Resources.TabUnsel;
            }
        }

        /// <summary>
        /// Raises the PageChanged event.
        /// </summary>
        private void OnPageChanged(EventArgs e)
        {
            _mainPanel.ResumeLayout();

            if (_buttons != null)
            {
                _buttons[Page].BackgroundImage = Resources.TabSel;
            }

            if (PageChanged != null)
            {
                PageChanged(this, e);
            }
        }

        internal void BindToButtons(params ToolStripButton[] buttons)
        {
            this._buttons = buttons;

            for (int n = 0; n < buttons.Length; n++)
            {
                var btn = buttons[n];
                btn.Tag = n;
                btn.Click += btn_Click;
                btn.BackgroundImage = Resources.TabUnsel;
            }

            OnPageChanged(EventArgs.Empty);
        }

        void btn_Click(object sender, EventArgs e)
        {
            var control = (ToolStripButton)sender;
            int tag = (int)control.Tag;
            Page = tag;
        }
    }
}
