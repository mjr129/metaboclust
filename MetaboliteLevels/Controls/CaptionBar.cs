using MetaboliteLevels.Data;
using MetaboliteLevels.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Controls
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
            _selector = selector;

            _flp = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Bottom,
                Visible = true
            };

            _label = new LinkLabel
            {
                AutoSize = true,
                Text = "",
                Visible = true,
                LinkColor = Color.DarkGray
            };

            _label.LinkClicked += _label_LinkClicked;
            _label.LinkBehavior = LinkBehavior.NeverUnderline;

            parent.Controls.Add(_flp);
            _flp.Controls.Add(_label);

            _label.SendToBack();
        }

        void _label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selector == null)
            {
                return;
            }

            _selector.Selection = new VisualisableSelection((IVisualisable)e.Link.LinkData);
        }

        public void SetText(string format, params IVisualisable[] nameableItems)
        {
            _label.LinkColor = Color.CornflowerBlue;

            StringBuilder r = new StringBuilder();
            StringBuilder t = new StringBuilder();
            bool inb = false;
            _label.Links.Clear();

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

                        IVisualisable it = (nameableItems != null && nameableItems.Length > i) ? nameableItems[i] : null;

                        if (it != null)
                        {
                            string txt = it.DisplayName;

                            r.Append(it.VisualClass.ToUiString().ToLower().ToSmallCaps() + " ");

                            _label.Links.Add(r.Length, txt.Length, it);
                            r.Append(txt);
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

            _label.Text = r.ToString();
        }

        public bool Visible
        {
            get { return _label.Visible; }
            set { _label.Visible = value; }
        }

        public string Text
        {
            get
            {
                return _label.Text;
            }
        }
    }
}
