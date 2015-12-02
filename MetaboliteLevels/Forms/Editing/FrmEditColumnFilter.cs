using System;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    /// Edits: A filter (EOperator and text)
    /// </summary>
    public partial class FrmFilter : Form
    {
        internal static bool Show(Form owner, string colName, ref ListVieweHelper.EOperator op, ref string text)
        {
            using (FrmFilter frm = new FrmFilter())
            {
                frm.ctlTitleBar1.SubText += " for " + colName;

                EnumComboBox.Populate(frm._lstNumComp, op, true);
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
            return EnumComboBox.Get(_lstNumComp, (ListVieweHelper.EOperator)(-1));
        }

        public FrmFilter()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            UiControls.CompensateForVisualStyles(this);
        }

        private void something_Changed(object sender, EventArgs e)
        {
            bool ok;
            string txt = _txtNumComp.Text;
            double tmp;

            switch (GetSelectedOperator())
            {
                case ListVieweHelper.EOperator.EqualTo:
                case ListVieweHelper.EOperator.LessThan:
                case ListVieweHelper.EOperator.LessThanEq:
                case ListVieweHelper.EOperator.MoreThan:
                case ListVieweHelper.EOperator.MoreThanEq:
                case ListVieweHelper.EOperator.NotEqualTo:
                    ok = txt.Length != 0 && double.TryParse(txt, out tmp);
                    break;

                case ListVieweHelper.EOperator.Regex:
                case ListVieweHelper.EOperator.TextContains:
                case ListVieweHelper.EOperator.TextDoesNotContain:
                    ok = txt.Length != 0;
                    break;

                default:
                    ok = false;
                    break;
            }

            button1.Enabled = ok;
        }
    }
}
