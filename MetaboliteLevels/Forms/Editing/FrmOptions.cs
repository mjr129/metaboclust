using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Editing
{
    internal partial class FrmOptions : Form
    {
        internal static void Show(Form owner, Core core)
        {
            using (FrmOptions frm = new FrmOptions(core.Options))
            {
                frm.Text = "Visual options - " + core.FileNames.Title;
                UiControls.ShowWithDim(owner, frm);
            }
        }

        public FrmOptions()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        public FrmOptions(object obj)
            : this()
        {
            this.propertyGrid1.SelectedObject = obj;
            UiControls.CompensateForVisualStyles(this);
        }
    }
}
