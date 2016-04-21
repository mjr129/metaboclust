using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Startup
{
    public partial class FrmSetWorkspace : Form
    {
        private string _beforeNoneChecked;

        public static void Show(Form owner)
        {
            using (FrmSetWorkspace frm = new FrmSetWorkspace())
            {   
                frm.ShowDialog();
            }
        }

        private FrmSetWorkspace()
        {
            InitializeComponent();   
            UiControls.SetIcon(this);

            _txtDataFolder.Text = UiControls.StartupPath;

            switch (UiControls.StartupPathMode)
            {
                case UiControls.EStartupPath.Local:
                case UiControls.EStartupPath.None:
                    _radLocal.Checked = true;
                    break;

                case UiControls.EStartupPath.Machine:
                    _radMachine.Checked = true;
                    break;

                case UiControls.EStartupPath.User:
                    _radUser.Checked = true;
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
            _btnOk.Enabled = Directory.Exists(_txtDataFolder.Text);
        }

        private void _btnSetDataFolder_Click(object sender, EventArgs e)
        {
            string dir = UiControls.BrowseForFolder(this, _txtDataFolder.Text);

            if (dir != null)
            {
                if (_radNone.Checked)
                {
                    _radLocal.Checked = true;
                }

                _txtDataFolder.Text = dir;
            }
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            UiControls.EStartupPath mode = _radLocal.Checked ? UiControls.EStartupPath.Local
                : _radUser.Checked ? UiControls.EStartupPath.User
                : _radMachine.Checked ? UiControls.EStartupPath.Machine
                : UiControls.EStartupPath.None;

            UiControls.SetStartupPath(_txtDataFolder.Text, mode);
            UiControls.RestartProgram();
        }        

        private void ctlButton4_Click(object sender, EventArgs e)
        {
            label10.Visible = !label10.Visible;
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            label2.Visible = !label2.Visible;
        }

        private void _radNone_CheckedChanged(object sender, EventArgs e)
        {
            if (_radNone.Checked)
            {
                _beforeNoneChecked = _txtDataFolder.Text;
                _txtDataFolder.Text = Application.StartupPath;
                _txtDataFolder.Enabled = false;
            }
            else
            {
                _txtDataFolder.Text = _beforeNoneChecked;
                _txtDataFolder.Enabled = true;   
            }   
        }
    }
}
