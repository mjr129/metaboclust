using System;
using System.IO;
using System.Windows.Forms;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System.Diagnostics;
using MetaboliteLevels.Forms.Generic;
using System.Runtime.InteropServices;
using MGui.Helpers;
using MGui.Controls;

namespace MetaboliteLevels.Forms.Startup
{
    /// <summary>
    /// Handles initial setup (currently just the R path)
    /// </summary>
    public partial class FrmInitialSetup : Form
    {
        private bool _potentials;

        /// <summary>
        /// Show method.
        /// </summary>
        /// <param name="owner">Owner form</param>
        /// <param name="forceShow">Always show, even if a configuration is already present</param>
        /// <returns>Whether a new configuration was provided</returns>
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

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public FrmInitialSetup()
        {
            InitializeComponent();
            UiControls.SetIcon(this);

            this._txtWorkingDirectory.Text = UiControls.StartupPath;
            this._txtDataSetData.Text = MainSettings.Instance.General.RBinPath;
            this._txtPathwayTools.Text = MainSettings.Instance.General.PathwayToolsDatabasesPath;

            FormHelper.EnumerateControls<Label>(this, z => z.Text = z.Text.Replace("{ProductName}", UiControls.Title));
            this.defaultToolStripMenuItem.Text = UiControls.GetOrCreateFixedFolder(UiControls.EInitialFolder.CompondDatabases);

            TestFolder(@"C:\Program Files\R");
            TestFolder(@"C:\Program Files (x86)\R");

            // UiControls.CompensateForVisualStyles(this);
        }

        /// <summary>
        /// Checks to see if a folder contains the R DLL.
        /// </summary>                                   
        private void TestFolder(string path)
        {
            bool isNot64 = Marshal.SizeOf(typeof(IntPtr)) != 8;
            bool isNot32 = Marshal.SizeOf(typeof(IntPtr)) != 4;

            if (Directory.Exists(path))
            {
                foreach (string fn in Directory.GetDirectories(path))
                {
                    if (isNot64)
                    {
                        TestFile(Path.Combine(fn, @"bin\i386"));
                    }

                    if (isNot32)
                    {
                        TestFile(Path.Combine(fn, @"bin\x64"));
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a file contains the R DLL, if it does adds it to the list.
        /// </summary>                                                          
        private void TestFile(string path)
        {
            string dll = Path.Combine(path, @"R.dll");

            if (File.Exists(dll))
            {
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(dll);

                ToolStripMenuItem tsmi = new ToolStripMenuItem("Version " + version.FileVersion);
                tsmi.Tag = path;
                tsmi.Click += mnuRDirectoryName_Click;
                _cmsR.Items.Add(tsmi);

                _potentials = true;
            }
        }

        /// <summary>
        /// R path selected.
        /// </summary>      
        void mnuRDirectoryName_Click(object sender, EventArgs e)
        {
            _txtDataSetData.Text = (string)((ToolStripMenuItem)sender).Tag;
        }

        /// <summary>
        /// R browse clicked.
        /// </summary>       
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

        /// <summary>
        /// OK clicked
        /// </summary>
        private void _btnOk_Click(object sender, EventArgs e)
        {
            MainSettings.Instance.General.RBinPath = _txtDataSetData.Text;
            MainSettings.Instance.General.PathwayToolsDatabasesPath = _txtPathwayTools.Text;
            MainSettings.Instance.Save();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Various changes handler.
        /// </summary>              
        private void something_Changed(object sender, EventArgs e)
        {
            bool validRPath = File.Exists(Path.Combine(_txtDataSetData.Text, "R.dll"));
            bool validPTPath = Directory.Exists(_txtPathwayTools.Text);

            errorProvider1.Check(_txtDataSetData, validRPath, "A valid path to R is mandatory.");
            errorProvider1.Check( _txtPathwayTools, validPTPath, "The folder must exist.");

            _btnOk.Enabled = !errorProvider1.HasErrors;
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
                UiControls.StartProcess(this,ll.Text);
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

        private void _btnSetWorkingDirectory_Click(object sender, EventArgs e)
        {
            FrmSetupWorkspace.Show(this);
        }

        private void ctlButton5_Click(object sender, EventArgs e)
        {
            label10.Visible = !label10.Visible;
        }
    }
}
