using System.Windows.Forms;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Generic
{
    internal partial class FrmInput2 : Form
    {
        public FrmInput2()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            UiControls.CompensateForVisualStyles(this);
        }

        public static bool Show(Form owner, IVisualisable v, bool readOnly)
        {
            string name = v.OverrideDisplayName;
            string comments = v.Comment;
            bool enabled = v.Enabled;

            if (Show(owner, v.DefaultDisplayName, "Rename", v.ToString(), v.DefaultDisplayName, ref name, ref comments, ref enabled, readOnly))
            {
                v.OverrideDisplayName = name;
                v.Comment = comments;
                v.Enabled = enabled;
                return true;
            }

            return false;
        }

        public static bool Show(Form owner, string windowText, string mainTitle, string subTitle, string defaultName, ref string name, ref string comments, ref bool enabled, bool readOnly)
        {
            using (FrmInput2 frm = new FrmInput2())
            {
                frm.Text = windowText;

                frm.textBox1.Watermark = defaultName;
                frm.textBox1.Text = name;
                frm._txtInput.Text = comments;
                frm.ctlTitleBar1.Text = mainTitle;
                frm.ctlTitleBar1.SubText = subTitle;
                frm.checkBox1.Checked = !enabled;

                frm.AcceptButton = null;
                frm.CancelButton = frm._btnCancel;

                if (readOnly)
                {
                    frm._btnOk.Visible = false;
                    frm._btnCancel.Text = "  Close";
                    frm.AcceptButton = frm._btnCancel;
                    frm.textBox1.ReadOnly = true;
                    frm._txtInput.ReadOnly = true;
                    frm.checkBox1.AutoCheck = false;
                }

                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    name = frm.textBox1.Text;
                    comments = frm._txtInput.Text;
                    enabled = !frm.checkBox1.Checked;
                    return true;
                }

                return false;
            }
        }
    }
}
