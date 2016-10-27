using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    /// <summary>
    /// Edits: A filter (EOperator and text)
    /// </summary>
    public partial class FrmEditColumnFilter : Form
    {
        internal static bool Show(Form owner, string colName, ref ListVieweHelper.EOperator op, ref string text)
        {
            using (FrmEditColumnFilter frm = new FrmEditColumnFilter())
            {
                frm.ctlTitleBar1.SubText += " for " + colName;

                EnumComboBox.Populate(frm._lstNumComp, op);
                frm._txtNumComp.Text = text;

                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    op = frm.GetSelectedOperator();
                    text = frm._txtNumComp.Text;
                    return true;
                }

                return false;
            }
        }

        private ListVieweHelper.EOperator GetSelectedOperator()
        {
            return EnumComboBox.Get(this._lstNumComp, (ListVieweHelper.EOperator)(-1));
        }

        public FrmEditColumnFilter()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
            // UiControls.CompensateForVisualStyles(this);
        }

        private void something_Changed(object sender, EventArgs e)
        {
            this._checker.Clear();
                     
            string txt = this._txtNumComp.Text;
            double tmp;

            switch (this.GetSelectedOperator())
            {
                case ListVieweHelper.EOperator.EqualTo:
                case ListVieweHelper.EOperator.LessThan:
                case ListVieweHelper.EOperator.LessThanEq:
                case ListVieweHelper.EOperator.MoreThan:
                case ListVieweHelper.EOperator.MoreThanEq:
                case ListVieweHelper.EOperator.NotEqualTo:
                    this._txtNumComp.Enabled = true;
                    this._checker.Check( this._txtNumComp, txt.Length != 0 && double.TryParse( txt, out tmp ), "Please enter a valid number." );
                    break;

                case ListVieweHelper.EOperator.Regex:
                case ListVieweHelper.EOperator.TextContains:
                case ListVieweHelper.EOperator.TextDoesNotContain:
                    this._txtNumComp.Enabled = true;
                    this._checker.Check( this._txtNumComp, txt.Length != 0, "The field cannot be empty." );
                    break;

                default:
                    this._txtNumComp.Enabled = false;
                    this._checker.Check( this._lstNumComp, false, "Select an operator" );
                    break;
            }

            this.button1.Enabled = this._checker.NoErrors;
        }
    }
}
