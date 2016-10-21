using MetaboliteLevels.Controls;
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
using MetaboliteLevels.Data.Session.General;
using MGui;
using MGui.Controls;

namespace MetaboliteLevels.Forms.Editing
{
    internal partial class FrmEditPeakFlag : Form
    {
        private string _comment;
        private readonly bool _readOnly;
        private readonly CtlBinder<PeakFlag> _binder1 = new CtlBinder<PeakFlag>();

        /// <summary>
        /// Shows the PeakFlag editor
        /// </summary>               
        public static bool Show(Form owner, PeakFlag flag, bool readOnly)
        {
            UiControls.Assert(flag != null, "flag must not be null");

            using (FrmEditPeakFlag frm = new FrmEditPeakFlag(flag, readOnly))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    frm._binder1.Commit();
                    flag.Comment = frm._comment;
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>                     
        private FrmEditPeakFlag(PeakFlag flag, bool readOnly)
        {
            InitializeComponent();      

            _readOnly = readOnly;
                                                       
            _binder1.Bind(_txtName, z=> z.OverrideDisplayName);
            _binder1.Bind(_txtKey, z => z.Key);
            _binder1.Bind(_numDuration, z => z.BeepDuration);
            _binder1.Bind(_numFrequency, z => z.BeepFrequency);
            _binder1.Bind(_btnColour, z => z.Colour);
            _binder1.Read(flag);

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
