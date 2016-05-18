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
        private readonly Panel MainPanel;
        public readonly List<Panel> Pages = new List<Panel>();
        public readonly List<string> PageTitles = new List<string>();
        public readonly List<string> PageSubTitles = new List<string>();
        public readonly List<string> PageFlags = new List<string>();
        int pageno = -1;
        Panel current;
        public event EventHandler PageChanged;
        ToolStripButton[] buttons;

        public PagerControl(ref TabControl tc, Panel panel = null)
        {
            if (panel != null)
            {
                MainPanel = panel;
            }
            else if (tc.Parent is Panel)
            {
                MainPanel = (Panel)tc.Parent;
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

                MainPanel.Controls.Add(p);
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
                return PageFlags[pageno];
            }
        }

        public string PageTitle
        {
            get
            {
                return PageTitles[pageno];
            }
        }

        public string PageSubTitle
        {
            get
            {
                return PageSubTitles[pageno];
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
                return pageno;
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
                if (p == pageno)
                {
                    return true;
                }

                OnPageChanging();

                MainPanel.SuspendDrawingAndLayout();

                if (current != null)
                {
                    current.Visible = false;
                }

                pageno = p;
                current = Pages[p];
                current.Visible = true;
                OnPageChanged(EventArgs.Empty);

                MainPanel.ResumeDrawingAndLayout();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnPageChanging()
        {
            MainPanel.SuspendLayout();

            if (buttons != null)
            {
                buttons[Page].BackgroundImage = Resources.TabUnsel;
            }
        }

        /// <summary>
        /// Raises the PageChanged event.
        /// </summary>
        private void OnPageChanged(EventArgs e)
        {
            MainPanel.ResumeLayout();

            if (buttons != null)
            {
                buttons[Page].BackgroundImage = Resources.TabSel;
            }

            if (PageChanged != null)
            {
                PageChanged(this, e);
            }
        }

        internal void BindToButtons(params ToolStripButton[] buttons)
        {
            this.buttons = buttons;

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
