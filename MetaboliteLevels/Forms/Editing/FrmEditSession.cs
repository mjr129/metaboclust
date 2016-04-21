using System;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmSession : Form
    {
        private Core core;

        public FrmSession()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        internal FrmSession(Core session)
            : this()
        {
            this.core = session;

            textBox1.Text = core.FileNames.Title;
            textBox2.Text = core.FileNames.GetShortTitle();
            textBox3.Text = core.FileNames.Comments;

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
            core.FileNames.Title = textBox1.Text;
            core.FileNames.ShortTitle = textBox2.Text;
            core.FileNames.Comments = textBox3.Text;
        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {
            _checker.Check( textBox1, textBox1.TextLength != 0, "A title for the session is required." );
            button1.Enabled = _checker.NoErrors;
        }
    }
}
