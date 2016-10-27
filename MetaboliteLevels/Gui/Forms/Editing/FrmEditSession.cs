using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    public partial class FrmSession : Form
    {
        private Core _core;

        public FrmSession()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        internal FrmSession(Core session)
            : this()
        {
            this._core = session;

            this.textBox1.Text = this._core.FileNames.Title;
            this.textBox2.Text = this._core.FileNames.GetShortTitle();
            this.textBox3.Text = this._core.FileNames.Comments;

            // UiControls.CompensateForVisualStyles(this);
        }

        internal static void Show(Form owner, Core session)
        {
            using (FrmSession frm = new FrmSession(session))
            {
                UiControls.ShowWithDim(owner, frm);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._core.FileNames.Title = this.textBox1.Text;
            this._core.FileNames.ShortTitle = this.textBox2.Text;
            this._core.FileNames.Comments = this.textBox3.Text;
        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {
            this._checker.Check( this.textBox1, this.textBox1.TextLength != 0, "A title for the session is required." );
            this.button1.Enabled = this._checker.NoErrors;
        }
    }
}
