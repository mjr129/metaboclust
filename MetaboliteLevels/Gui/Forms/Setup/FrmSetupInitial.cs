using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Setup
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
            this.InitializeComponent();
            UiControls.SetIcon(this);

            this._txtWorkingDirectory.Text = UiControls.StartupPath;
            this._txtDataSetData.Text = MainSettings.Instance.General.RBinPath;
            this._txtPathwayTools.Text = MainSettings.Instance.General.PathwayToolsDatabasesPath;

            FormHelper.EnumerateControls<Label>(this, z => z.Text = z.Text.Replace("{ProductName}", UiControls.Title));
            this.defaultToolStripMenuItem.Text = UiControls.GetOrCreateFixedFolder(UiControls.EInitialFolder.CompondDatabases);

            this.TestFolder(@"C:\Program Files\R");
            this.TestFolder(@"C:\Program Files (x86)\R");

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
                        this.TestFile(Path.Combine(fn, @"bin\i386"));
                    }

                    if (isNot32)
                    {
                        this.TestFile(Path.Combine(fn, @"bin\x64"));
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
                tsmi.Click += this.mnuRDirectoryName_Click;
                this._cmsR.Items.Add(tsmi);

                this._potentials = true;
            }
        }

        /// <summary>
        /// R path selected.
        /// </summary>      
        void mnuRDirectoryName_Click(object sender, EventArgs e)
        {
            this._txtDataSetData.Text = (string)((ToolStripMenuItem)sender).Tag;
        }

        /// <summary>
        /// R browse clicked.
        /// </summary>       
        private void _btnDataSetData_Click(object sender, EventArgs e)
        {
            if (this._potentials)
            {
                this._cmsR.Show(this._btnDataSetData, 0, this._btnDataSetData.Height);
            }
            else
            {
                this.DoBrowse();
            }
        }

        /// <summary>
        /// OK clicked
        /// </summary>
        private void _btnOk_Click(object sender, EventArgs e)
        {
            MainSettings.Instance.General.RBinPath = this._txtDataSetData.Text;
            MainSettings.Instance.General.PathwayToolsDatabasesPath = this._txtPathwayTools.Text;
            MainSettings.Instance.Save(MainSettings.EFlags.General);
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Various changes handler.
        /// </summary>              
        private void something_Changed(object sender, EventArgs e)
        {
            bool validRPath = File.Exists(Path.Combine(this._txtDataSetData.Text, "R.dll"));
            bool validPtPath = Directory.Exists(this._txtPathwayTools.Text);

            this.errorProvider1.Check(this._txtDataSetData, validRPath, "A valid path to R is mandatory.");
            this.errorProvider1.Check( this._txtPathwayTools, validPtPath, "The folder must exist.");

            this._btnOk.Enabled = !this.errorProvider1.HasErrors;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoBrowse();
        }

        private void DoBrowse()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                try
                {
                    ofd.InitialDirectory = Path.GetDirectoryName(this._txtDataSetData.Text);
                }
                catch (Exception)
                {
                    // Ignore
                }

                ofd.FileName = Path.Combine(this._txtDataSetData.Text, "R.dll");
                ofd.Filter = "R runtime (R.dll)|R.dll";

                if (UiControls.ShowWithDim(this, ofd) == DialogResult.OK)
                {
                    this._txtDataSetData.Text = Path.GetDirectoryName(ofd.FileName);
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
            this._cmsPathwayTools.Show(this._btnPathwayTools, 0, this._btnPathwayTools.Height);
        }

        private void browseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string newFolder = UiControls.BrowseForFolder(this, this._txtPathwayTools.Text);

            if (newFolder != null)
            {
                this._txtPathwayTools.Text = newFolder;
            }
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._txtPathwayTools.Text = this.defaultToolStripMenuItem.Text;
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            this.label1.Visible = !this.label1.Visible;
        }

        private void ctlButton2_Click(object sender, EventArgs e)
        {
            this.label4.Visible = !this.label4.Visible;
        }

        private void ctlButton3_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel2.Visible = !this.tableLayoutPanel2.Visible;
        }

        private void _btnSetWorkingDirectory_Click(object sender, EventArgs e)
        {
            FrmSetupWorkspace.Show(this);
        }

        private void ctlButton5_Click(object sender, EventArgs e)
        {
            this.label10.Visible = !this.label10.Visible;
        }

        private void ctlButton4_Click( object sender, EventArgs e )
        {
            flowLayoutPanel6.Visible = false;
        }
    }
}
