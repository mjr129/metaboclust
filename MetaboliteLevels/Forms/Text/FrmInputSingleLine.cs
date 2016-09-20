using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Text
{
    public partial class FrmInputSingleLine : Form
    {
        public FrmInputSingleLine()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            // UiControls.CompensateForVisualStyles(this);
        }

        public static string Show(Form owner, string formText, string title, string subTitle, string @default)
        {
            using (FrmInputSingleLine find = new FrmInputSingleLine())
            {
                find.Text = formText;
                find.ctlTitleBar1.Text = title;
                find.ctlTitleBar1.SubText = subTitle;
                find._txtInput.Text = @default;

                if (UiControls.ShowWithDim(owner, find) == DialogResult.OK)
                {
                    return find._txtInput.Text;
                }

                return null;
            }
        }
    }
}
