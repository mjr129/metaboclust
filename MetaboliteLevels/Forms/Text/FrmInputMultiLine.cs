using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Text
{
    /// <summary>
    /// Generic multi-line text input/display form.
    /// </summary>
    public partial class FrmInputMultiLine : Form
    {
        public FrmInputMultiLine()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            // UiControls.CompensateForVisualStyles(this);
        }

        public static void ShowFixed(Form owner, string windowText, string mainTitle, string subTitle, string content)
        {
            using (FrmInputMultiLine frm = new FrmInputMultiLine())
            {
                frm.Text = windowText;

                frm._txtInput.Text = content;
                frm._txtInput.SelectionStart = 0;
                frm._txtInput.SelectionLength = 0;
                frm._txtInput.ReadOnly = true;
                frm._txtInput.ForeColor = Color.Black;
                frm._txtInput.BackColor = Color.White;

                frm._btnCancel.Visible = false;
                frm.ctlTitleBar1.Text = mainTitle;
                frm.ctlTitleBar1.SubText = subTitle;

                frm.AcceptButton = frm._btnOk;
                frm.CancelButton = frm._btnOk;

                UiControls.ShowWithDim(owner, frm);
            }
        }

        public static string Show(Form owner, string windowText, string mainTitle, string subTitle, string @default)
        {
            using (FrmInputMultiLine frm = new FrmInputMultiLine())
            {
                frm.Text = windowText;

                frm._txtInput.Text = @default;
                frm.ctlTitleBar1.Text = mainTitle;
                frm.ctlTitleBar1.SubText = subTitle;

                frm.AcceptButton = null;
                frm.CancelButton = frm._btnCancel;

                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm._txtInput.Text;
                }

                return null;
            }
        }
    }
}
