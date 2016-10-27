using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls
{
    /// <summary>
    /// Represents the a caption under a graph, list, etc.
    /// 
    /// Only for use with FrmMain.
    /// </summary>
    internal class CaptionBar
    {
        private readonly FlowLayoutPanel _flp;
        private readonly LinkLabel _label;
        private readonly ISelectionHolder _selector;

        /// <summary>
        /// Creates the captionbar at the bottom of the specified control.
        /// </summary>
        public CaptionBar(Control parent, ISelectionHolder selector)
        {
            this._selector = selector;

            this._flp = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Bottom,
                Visible = true
            };

            this._label = new LinkLabel
            {
                AutoSize = true,
                Text = "",
                Visible = true,
                LinkColor = Color.DarkGray
            };

            this._label.LinkClicked += this._label_LinkClicked;
            this._label.LinkBehavior = LinkBehavior.NeverUnderline;

            parent.Controls.Add(this._flp);
            this._flp.Controls.Add(this._label);

            this._label.SendToBack();
        }

        void _label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this._selector == null)
            {
                return;
            }

            this._selector.Selection = new VisualisableSelection((Visualisable)e.Link.LinkData);
        }

        public void SetText(string format, params object[] items)
        {
            this._label.LinkColor = Color.CornflowerBlue;

            StringBuilder r = new StringBuilder();
            StringBuilder t = new StringBuilder();
            bool inb = false;
            this._label.Links.Clear();

            if (format != null)
            {
                foreach (char f in format)
                {
                    if (f == '{')
                    {
                        inb = true;
                    }
                    else if (f == '}')
                    {
                        inb = false;

                        int i = int.Parse(t.ToString());
                        t.Clear();

                        object it = (items != null && items.Length > i) ? items[i] : null;

                        if (it != null)
                        {
                            var asAssociational = it as Associational;
                            string linkText;

                            if (asAssociational != null)
                            {
                                linkText = asAssociational.DisplayName;

                                r.Append( asAssociational.AssociationalClass.ToUiString().ToLower().ToSmallCaps() + " " );
                            }
                            else
                            {
                                linkText = it.ToStringSafe();
                            }

                            this._label.Links.Add(r.Length, linkText.Length, it);
                            r.Append(linkText);
                        }
                        else
                        {
                            r.Append("nothing".ToSmallCaps());
                        }
                    }
                    else if (inb)
                    {
                        t.Append(f);
                    }
                    else
                    {
                        r.Append(f);
                    }
                }
            }

            this._label.Text = r.ToString();
        }

        public bool Visible
        {
            get { return this._label.Visible; }
            set { this._label.Visible = value; }
        }

        public string Text
        {
            get
            {
                return this._label.Text;
            }
        }
    }
}
