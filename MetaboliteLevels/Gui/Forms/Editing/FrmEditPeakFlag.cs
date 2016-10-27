using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Utilities;
using MGui.Controls;

namespace MetaboliteLevels.Gui.Forms.Editing
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
            this.InitializeComponent();      

            this._readOnly = readOnly;
                                                       
            this._binder1.Bind(this._txtName, z=> z.OverrideDisplayName);
            this._binder1.Bind(this._txtKey, z => z.Key);
            this._binder1.Bind(this._numDuration, z => z.BeepDuration);
            this._binder1.Bind(this._numFrequency, z => z.BeepFrequency);
            this._binder1.Bind(this._btnColour, z => z.Colour);
            this._binder1.Read(flag);

            this._comment = flag.Comment;

            if (readOnly)
            {
                UiControls.MakeReadOnly(this, this._btnComment);
            }
        }    
        
        private void _btnComment_Click(object sender, EventArgs e)
        {
            FrmEditINameable.Show(this, this._txtName, ref this._comment, this._readOnly);
        }
    }
}
