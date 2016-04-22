using MetaboliteLevels.Controls;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmEditPeakFlag : Form
    {
        private string _comment;
        private readonly bool _readOnly;
        private readonly CtlBinder<PeakFlag> binder1 = new CtlBinder<PeakFlag>();

        public static bool Show(Form owner, PeakFlag flag, bool readOnly)
        {
            UiControls.Assert(flag != null, "flag must not be null");

            using (FrmEditPeakFlag frm = new FrmEditPeakFlag(flag, readOnly))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    frm.binder1.Commit();
                    flag.Comment = frm._comment;
                    return true;
                }

                return false;
            }
        }

        public FrmEditPeakFlag(PeakFlag flag, bool readOnly)
        {
            InitializeComponent();      

            _readOnly = readOnly;                           
                                                              
            binder1.Bind(_txtName, z=> z.OverrideDisplayName);
            binder1.Bind(_txtKey, z => z.Key);
            binder1.Bind(_numDuration, z => z.BeepDuration);
            binder1.Bind(_numFrequency, z => z.BeepFrequency);
            binder1.Bind(_btnColour, z => z.Colour);
            binder1.Read(flag);

            _comment = flag.Comment;

            if (readOnly)
            {
                UiControls.MakeReadOnly(this, _btnComment);
            }
        }    

        private void _btnComment_Click(object sender, EventArgs e)
        {
            FrmEditINameable.Show(this, _txtName, ref _comment, _readOnly);
        }
    }
}
