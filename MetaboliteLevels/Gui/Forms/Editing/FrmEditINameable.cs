using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Utilities;
using Visualisable = MetaboliteLevels.Data.Session.Associational.Visualisable;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    internal partial class FrmEditINameable : Form
    {
        public FrmEditINameable()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
            // UiControls.CompensateForVisualStyles(this);
        }

        public static bool Show(Form owner, TextBox name, ref string comments, bool readOnly)
        {
            if (readOnly)
            {
                FrmInputMultiLine.ShowFixed(owner, owner.Text, "View Comments", name.Text, comments);
                return false;
            }
            else
            {
                string names = name.Text;

                if (FrmEditINameable.Show(owner, owner.Text, "Edit name and comments", null, name.Text, ref names, ref comments, readOnly, null))
                {
                    name.Text = names;
                    return true;
                }

                return false;
            }
        }

        public static bool Show(Form owner, Visualisable v, bool readOnly)
        {
            string name = v.OverrideDisplayName;
            string comments = v.Comment;

            if (Show(owner, v.DefaultDisplayName, "Edit name and comments", v.ToString(), v.DefaultDisplayName, ref name, ref comments, readOnly, v))
            {
                v.OverrideDisplayName = name;
                v.Comment = comments;
                return true;
            }

            return false;
        }

        public static bool Show(Form owner, string windowText, string mainTitle, string subTitle, string defaultName, ref string name, ref string comments, bool readOnly, Visualisable supports )
        {
            bool canRename = supports == null || IVisualisableExtensions.SupportsRename(supports);
            bool canComment = supports == null || IVisualisableExtensions.SupportsComment(supports);

            if (!canRename && !canComment)
            {
                FrmMsgBox.ShowInfo(owner, defaultName, "This item cannot be renamed.");
                return false;
            }

            using (FrmEditINameable frm = new FrmEditINameable())
            {
                frm.Text = windowText;

                frm.textBox1.Watermark = defaultName;
                frm.textBox1.Text = name;
                frm._txtInput.Text = comments;
                frm.ctlTitleBar1.Text = mainTitle;
                frm.ctlTitleBar1.SubText = subTitle;

                frm.AcceptButton = null;
                frm.CancelButton = frm._btnCancel;

                frm.textBox1.Visible = canRename;
                frm.label6.Visible = !canRename;
                frm.label6.Text = defaultName;
                frm._txtInput.Visible = canComment;
                frm.label2.Visible = canComment;

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
