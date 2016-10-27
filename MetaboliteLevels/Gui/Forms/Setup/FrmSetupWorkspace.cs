using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;

namespace MetaboliteLevels.Gui.Forms.Setup
{
    public partial class FrmSetupWorkspace : Form
    {
        private string _beforeNoneChecked;

        public static void Show(Form owner)
        {
            using (FrmSetupWorkspace frm = new FrmSetupWorkspace())
            {   
                frm.ShowDialog();
            }
        }

        private FrmSetupWorkspace()
        {
            this.InitializeComponent();   
            UiControls.SetIcon(this);

            this._txtDataFolder.Text = UiControls.StartupPath;

            switch (UiControls.StartupPathMode)
            {
                case UiControls.EStartupPath.Local:
                case UiControls.EStartupPath.None:
                    this._radLocal.Checked = true;
                    break;

                case UiControls.EStartupPath.Machine:
                    this._radMachine.Checked = true;
                    break;

                case UiControls.EStartupPath.User:
                    this._radUser.Checked = true;
                    break;   

                default:
                    throw new SwitchException(UiControls.StartupPathMode);
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _txtDataFolder_TextChanged(object sender, EventArgs e)
        {
            this._btnOk.Enabled = Directory.Exists(this._txtDataFolder.Text);
        }

        private void _btnSetDataFolder_Click(object sender, EventArgs e)
        {
            string dir = UiControls.BrowseForFolder(this, this._txtDataFolder.Text);

            if (dir != null)
            {
                if (this._radNone.Checked)
                {
                    this._radLocal.Checked = true;
                }

                this._txtDataFolder.Text = dir;
            }
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {   
            // Get the pointer mode
            UiControls.EStartupPath mode = this._radLocal.Checked ? UiControls.EStartupPath.Local
                : this._radUser.Checked ? UiControls.EStartupPath.User
                : this._radMachine.Checked ? UiControls.EStartupPath.Machine
                : UiControls.EStartupPath.None;

            // The file browser should have already checked for write-access, but check again just to make sure
            string text = "This is a test file and should have been automatically deleted by " + Path.GetFileName( Application.ExecutablePath ) + ". This file is safe to remove.";
            string dir = mode == UiControls.EStartupPath.None ? Application.StartupPath : this._txtDataFolder.Text;
            string testFile = Path.Combine( dir, "delete-me (822FBADE-2869-4D82-8E1D-56D8D2A22D11).txt" );

            try
            {
                File.WriteAllText( testFile, text );
                File.Delete( testFile );
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError( this, ex );
                return;
            }

            // Set the path and restart
            UiControls.SetStartupPath(this._txtDataFolder.Text, mode);
            UiControls.RestartProgram(this);
        }        

        private void ctlButton4_Click(object sender, EventArgs e)
        {
            this.label10.Visible = !this.label10.Visible;
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            this.label2.Visible = !this.label2.Visible;
        }

        private void _radNone_CheckedChanged(object sender, EventArgs e)
        {
            if (this._radNone.Checked)
            {
                this._beforeNoneChecked = this._txtDataFolder.Text;
                this._txtDataFolder.Text = Application.StartupPath;
                this._txtDataFolder.Enabled = false;
            }
            else
            {
                this._txtDataFolder.Text = this._beforeNoneChecked;
                this._txtDataFolder.Enabled = true;   
            }   
        }
    }
}
