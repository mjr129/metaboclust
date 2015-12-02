using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmOptions : Form
    {
        public static void Show(Form owner, string title, object obj)
        {
            using (FrmOptions frm = new FrmOptions(obj))
            {
                frm.Text = title;
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
