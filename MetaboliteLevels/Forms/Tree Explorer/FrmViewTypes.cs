using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Tree_Explorer
{
    public partial class FrmViewTypes : Form
    {
        private Core core;
        private List<Tuple<CheckBox, GroupInfo>> list = new List<Tuple<CheckBox, GroupInfo>>();

        internal static bool Show(Form owner, Core core)
        {
            using (FrmViewTypes frm = new FrmViewTypes(core))
            {
                return UiControls.ShowWithDim(owner, frm) == DialogResult.OK;
            }
        }

        public FrmViewTypes()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmViewTypes(Core core)
            : this()
        {
            this.core = core;
            Text = "Legend - " + core.FileNames.Title;

            foreach (GroupInfo ti in core.Groups)
            {
                CheckBox cb = new CheckBox();
                cb.Text = ti.Name;
                cb.BackColor = ti.ColourLight;
                cb.ForeColor = Color.White;
                cb.AutoSize = true;

                cb.Checked = core.Options.ViewTypes.Contains(ti);

                flowLayoutPanel1.Controls.Add(cb);

                list.Add(new Tuple<CheckBox, GroupInfo>(cb, ti));
            }

            UiControls.CompensateForVisualStyles(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var t in list)
            {
                if (core.Options.ViewTypes.Contains(t.Item2))
                {
                    if (!t.Item1.Checked)
                    {
                        core.Options.ViewTypes.Remove(t.Item2);
                    }
                }
                else
                {
                    if (t.Item1.Checked)
                    {
                        core.Options.ViewTypes.Add(t.Item2);
                        core.Options.ViewTypes.Sort();
                    }
                }
            }
        }
    }
}
