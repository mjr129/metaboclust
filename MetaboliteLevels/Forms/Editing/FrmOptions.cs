using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Editing
{
    internal partial class FrmOptions : Form
    {
        internal static void Show(Form owner, Core core)
        {
            using (FrmOptions2 frm = new FrmOptions2(core.Options))
            {
                frm.Text = "Visual options - " + core.FileNames.Title;
                UiControls.ShowWithDim(owner, frm);
            }
        }

        internal static void Show(Form owner, string title, object target)
        {
            using (FrmOptions frm = new FrmOptions(target))
            {
                frm.Text = title;
                UiControls.ShowWithDim(owner, frm);
            }
        }

        private FrmOptions()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmOptions(object obj)
            : this()
        {
            this.propertyGrid1.SelectedObject = obj;
            UiControls.CompensateForVisualStyles(this);
        }
    }
}
