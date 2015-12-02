using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Generic
{
    internal partial class FrmRScript : Form
    {
        private bool _dirty;
        private UiControls.EInitialFolder _saveFolder;
        private string _manName;
        private string _fileName;
        private SaveMode _mode;

        [Flags]
        public enum SaveMode
        {
            None = 0,
            ReturnFileName = 1,
            SaveToFolderMandatory = 2,
        }

        internal static string Show(Form owner, string caption, string title, string inputs, UiControls.EInitialFolder saveFolder, string manName, SaveMode mode)
        {
            using (FrmRScript frm = new FrmRScript(caption, title, inputs, saveFolder, manName, mode))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    if (mode.HasFlag(SaveMode.ReturnFileName))
                    {
                        return frm._fileName;
                    }
                    else
                    {
                        return frm.Text;
                    }
                }

                return null;
            }
        }

        public FrmRScript()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        public FrmRScript(string caption, string title, string inputs, UiControls.EInitialFolder saveFolder, string manName, SaveMode mode)
            : this()
        {
            this.Text = caption;
            ctlTitleBar1.Text = title;
            ctlTitleBar1.HelpText = "@" + manName;
            this._saveFolder = saveFolder;
            this._manName = manName;
            this._mode = mode;

            string[] e = inputs.Split(",".ToCharArray());
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            foreach (string ee in e)
            {
                string[] eee = ee.Split("=".ToCharArray());

                if (sb.Length != 0)
                {
                    sb.AppendLine();
                    sb2.Append(", ");
                }

                sb.Append("## " + eee[1] + " = " + eee[0]);
                sb2.Append(eee[0] + " (" + (eee[1] != "-" ? eee[1] : "optional") + ")");
            }

            sb.AppendLine();
            sb.AppendLine();

            this.ctlTitleBar1.SubText = "The following inputs are specified: " + sb2.ToString();
            this.textBox1.Text = sb.ToString();

            UpdateFileName();

            UiControls.CompensateForVisualStyles(this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!_dirty)
            {
                _dirty = true;
                UpdateFileName();
            }
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            if (_mode.HasFlag(SaveMode.SaveToFolderMandatory))
            {
                string folder = UiControls.GetOrCreateFixedFolder(_saveFolder);

                if (string.IsNullOrEmpty(_fileName) || _dirty || !_fileName.StartsWith(folder))
                {
                    FrmMsgBox.ShowWarning(this, "R-Script", "You must save the file into \"" + folder + "\" before continuing.");
                    return;
                }
            }
            else if (_mode.HasFlag(SaveMode.ReturnFileName))
            {
                if (string.IsNullOrEmpty(_fileName) || _dirty)
                {
                    FrmMsgBox.ShowWarning(this, "R-Script", "You must save the file before continuing.");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = this.BrowseForFile(_fileName, UiControls.EFileExtension.RScript, FileDialogMode.Open, _saveFolder);

            if (file != null)
            {
                _fileName = file;
                textBox1.Text = File.ReadAllText(file);
                _dirty = false;
                UpdateFileName();
            }
        }

        private void UpdateFileName()
        {
            if (_dirty)
            {
                if (_fileName == null)
                {
                    this.fileToolStripMenuItem.Text = "&FILE";
                }
                else
                {
                    this.fileToolStripMenuItem.Text = Path.GetFileNameWithoutExtension(_fileName).ToUpper() + " (unsaved)";
                }
            }
            else
            {
                if (_fileName == null)
                {
                    this.fileToolStripMenuItem.Text = "&FILE";
                }
                else
                {
                    this.fileToolStripMenuItem.Text = Path.GetFileNameWithoutExtension(_fileName).ToUpper();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void Save(bool saveAs)
        {
            string file = this.BrowseForFile(_fileName, UiControls.EFileExtension.RScript, saveAs ? FileDialogMode.SaveAs : FileDialogMode.Save, _saveFolder);

            if (file != null)
            {
                _fileName = file;
                File.WriteAllText(file, textBox1.Text);
                _dirty = false;
                UpdateFileName();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(true);
        }
    }
}
