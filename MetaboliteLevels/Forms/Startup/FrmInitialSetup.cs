using System;
using System.IO;
using System.Windows.Forms;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System.Diagnostics;
using MetaboliteLevels.Forms.Generic;

namespace MetaboliteLevels.Forms.Startup
{
    /// <summary>
    /// Handles initial setup (currently just the R path)
    /// </summary>
    public partial class FrmInitialSetup : Form
    {
        private bool _potentials;

        public FrmInitialSetup()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
                                                  
            //ctlButton4.Visible = Debugger.IsAttached;

            this._txtDataSetData.Text = MainSettings.Instance.General.RBinPath;
            this._txtPathwayTools.Text = MainSettings.Instance.General.PathwayToolsDatabasesPath;

            UiControls.EnumerateControls<Label>(this, z => z.Text = z.Text.Replace("{ProductName}", UiControls.Title));
            this.defaultToolStripMenuItem.Text = UiControls.GetOrCreateFixedFolder(UiControls.EInitialFolder.CompondDatabases);

            TestFolder(@"C:\Program Files\R");
            TestFolder(@"C:\Program Files (x86)\R");

            UiControls.CompensateForVisualStyles(this);
        }

        private void TestFolder(string p)
        {
            if (Directory.Exists(p))
            {
                foreach (string fn in Directory.GetDirectories(p))
                {
                    TestFile(Path.Combine(fn, @"bin\i386"));
                }
            }
        }

        private void TestFile(string p)
        {
            string dll = Path.Combine(p, @"R.dll");

            if (File.Exists(dll))
            {
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(dll);

                ToolStripMenuItem tsmi = new ToolStripMenuItem("Version " + version.FileVersion);
                tsmi.Tag = p;
                tsmi.Click += contextMenuStrip1_Click;
                _cmsR.Items.Add(tsmi);

                _potentials = true;
            }
        }

        void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            _txtDataSetData.Text = (string)((ToolStripMenuItem)sender).Tag;
        }

        private void _btnDataSetData_Click(object sender, EventArgs e)
        {
            if (_potentials)
            {
                _cmsR.Show(_btnDataSetData, 0, _btnDataSetData.Height);
            }
            else
            {
                DoBrowse();
            }
        }

        internal static bool Show(Form owner, bool forceShow)
        {
            if (!forceShow)
            {
                if (Directory.Exists(MainSettings.Instance.General.RBinPath)
                    && Directory.Exists(MainSettings.Instance.General.PathwayToolsDatabasesPath))
                {
                    return true;
                }
            }

            using (FrmInitialSetup frm = new FrmInitialSetup())
            {
                if (UiControls.ShowWithDim(owner, frm) != DialogResult.OK)
                {
                    return false;
                }
            }

            return true;
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            MainSettings.Instance.General.RBinPath = _txtDataSetData.Text;
            MainSettings.Instance.General.PathwayToolsDatabasesPath = _txtPathwayTools.Text;
            MainSettings.Instance.Save();
            DialogResult = DialogResult.OK;
        }

        private void _txtDataSetData_TextChanged(object sender, EventArgs e)
        {
            _btnOk.Enabled = File.Exists(Path.Combine(_txtDataSetData.Text, "R.dll")) && Directory.Exists(_txtPathwayTools.Text);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoBrowse();
        }

        private void DoBrowse()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                try
                {
                    ofd.InitialDirectory = Path.GetDirectoryName(_txtDataSetData.Text);
                }
                catch (Exception)
                {
                    // Ignore
                }

                ofd.FileName = Path.Combine(_txtDataSetData.Text, "R.dll");
                ofd.Filter = "R runtime (R.dll)|R.dll";

                if (UiControls.ShowWithDim(this, ofd) == DialogResult.OK)
                {
                    _txtDataSetData.Text = Path.GetDirectoryName(ofd.FileName);
                }
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel ll = (LinkLabel)sender;

            try
            {
                Process.Start(ll.Text);
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError(this, ex.Message);
            }
        }

        private void _btnPathwayTools_Click(object sender, EventArgs e)
        {
            _cmsPathwayTools.Show(_btnPathwayTools, 0, _btnPathwayTools.Height);
        }

        private void browseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string newFolder = UiControls.BrowseForFolder(this, _txtPathwayTools.Text);

            if (newFolder != null)
            {
                _txtPathwayTools.Text = newFolder;
            }
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _txtPathwayTools.Text = defaultToolStripMenuItem.Text;
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            label1.Visible = !label1.Visible;
        }

        private void ctlButton2_Click(object sender, EventArgs e)
        {
            label4.Visible = !label4.Visible;
        }

        private void ctlButton3_Click(object sender, EventArgs e)
        {
            tableLayoutPanel2.Visible = !tableLayoutPanel2.Visible;
        }

        private void ctlButton4_Click(object sender, EventArgs e)
        {
            FrmSetWorkspace.Show(this);
        }
    }
}
