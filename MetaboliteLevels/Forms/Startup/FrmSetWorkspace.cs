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
        public static void Show(Form owner)
        {
            using (FrmSetWorkspace frm = new FrmSetWorkspace())
            {
                frm._txtDataFolder.Text = UiControls.StartupPath;
                frm.ShowDialog();
            }
        }

        private FrmSetWorkspace()
        {
            InitializeComponent();
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
                _txtDataFolder.Text = dir;
            }
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            UiControls.SetStartupPath(_txtDataFolder.Text);

            _txtDataFolder.Enabled = false;
            _btnSetDataFolder.Enabled = false;
            _btnOk.Enabled = false;
            _btnCancel.Enabled = false;
        }

        private void _btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _btnRestart_Click(object sender, EventArgs e)
        {
            UiControls.RestartProgram();
        }
    }
}
