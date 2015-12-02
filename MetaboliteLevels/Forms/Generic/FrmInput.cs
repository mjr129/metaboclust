using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Generic
{
    public partial class FrmInput : Form
    {
        public FrmInput()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            UiControls.CompensateForVisualStyles(this);
        }

        public static string Show(Form owner, string formText, string title, string subTitle, string @default)
        {
            using (FrmInput find = new FrmInput())
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
