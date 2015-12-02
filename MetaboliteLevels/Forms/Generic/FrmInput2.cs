using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Generic
{
    public partial class FrmInput2 : Form
    {
        public FrmInput2()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            UiControls.CompensateForVisualStyles(this);
        }

        public static bool Show(Form owner, string windowText, string mainTitle, string subTitle, ref string name, ref string comments, bool readOnly)
        {
            using (FrmInput2 frm = new FrmInput2())
            {
                frm.Text = windowText;

                frm.textBox1.Text = name;
                frm._txtInput.Text = comments;
                frm.ctlTitleBar1.Text = mainTitle;
                frm.ctlTitleBar1.SubText = subTitle;

                frm.AcceptButton = null;
                frm.CancelButton = frm._btnCancel;

                if (readOnly)
                {
                    frm._btnOk.Visible = false;
                    frm._btnCancel.Text = "  Close";
                    frm.AcceptButton = frm._btnCancel;
                    frm.textBox1.ReadOnly = true;
                    frm._txtInput.ReadOnly = true;
                }

                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    name = frm.textBox1.Text;
                    comments = frm._txtInput.Text;
                    return true;
                }

                return false;
            }
        }
    }
}
