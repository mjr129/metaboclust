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
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Utilities;
using MGui.Controls;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    public partial class FrmEditFileLoadInfo : Form
    {
        private CtlBinder<FileLoadInfo> _binder;
        private TableLayoutPanel _binderTlp;

        public static new void Show( IWin32Window owner )
        {
            using (FrmEditFileLoadInfo frm = new FrmEditFileLoadInfo())
            {
                UiControls.ShowWithDim( owner, frm );
            }
        }

        public FrmEditFileLoadInfo()
        {
            InitializeComponent();
            UiControls.SetIcon( this );

            _binder = new CtlBinder<FileLoadInfo>();

            _binderTlp = _binder.AutoBind();
            _binderTlp.Dock = DockStyle.Top;
            _binderTlp.AutoSize = true;
            _binderTlp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add( _binderTlp );

            _binder.Read( MainSettings.Instance.FileLoadInfo );
        }        

        private void ctlButton1_Click( object sender, EventArgs e )
        {
            _binder.Commit();
            MainSettings.Instance.Save( MainSettings.EFlags.FileLoadInfo );
        }                      
    }
}
