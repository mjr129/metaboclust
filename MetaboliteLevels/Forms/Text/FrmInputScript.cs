using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Generic
{
    internal partial class FrmInputScript : Form
    {
        private UiControls.EInitialFolder _saveFolder;
        private string _fileName;

        internal static string Show(Form owner, string title, string inputs, UiControls.EInitialFolder folder, string fileName, bool workOnCopy, string defaultContent, bool readOnly)
        {
            using (FrmInputScript frm = new FrmInputScript(title, inputs, folder, fileName, workOnCopy, defaultContent, readOnly))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm._fileName;
                }

                return null;
            }
        }

        public FrmInputScript()
        {
            InitializeComponent();
            _mnuStrip.BackColor = UiControls.BackColour;
            this._mnuFile.ForeColor = UiControls.ForeColour;

            UiControls.SetIcon(this);
        }

        public FrmInputScript(string title, string inputs, UiControls.EInitialFolder saveFolder, string fileName, bool workOnCopy, string defaultContent, bool readOnly)
            : this()
        {
            this.Text = "Script Editor";
            this._titleBar.Text = title;
            this._titleBar.HelpText = Manual.RScript;
            this._saveFolder = saveFolder;

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

            this._titleBar.SubText = "Enter an R script. The following variables are available: " + sb2.ToString();

            if (string.IsNullOrEmpty(defaultContent))
            {
                this.textBox1.Text = sb.ToString();
            }
            else
            {
                this.textBox1.Text = defaultContent;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                toolStripStatusLabel1.Text = "NEW FILE";
            }
            else if (workOnCopy)
            {
                toolStripStatusLabel1.Text = "COPY OF: " + fileName;
            }
            else
            {
                _fileName = fileName;
                toolStripStatusLabel1.Text = fileName;
            }

            if (readOnly)
            {
                _btnOk.Visible = false;
                btnCancel.Text = "Close";
                textBox1.ReadOnly = true;
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            string fileName;

            if (string.IsNullOrEmpty(_fileName))
            {
                string text = FrmInputSingleLine.Show(this, Text, "Save changes", "Please enter a name for your script", "");

                if (string.IsNullOrEmpty(text))
                {
                    return;
                }

                fileName = Path.Combine(UiControls.GetOrCreateFixedFolder(_saveFolder), text + ".r");
            }
            else if (FrmMsgBox.ShowYesNo(this, Text, "Save changes?\r\n" + _fileName, Resources.MsgInfo))
            {
                fileName = _fileName;
            }
            else
            {
                return;
            }

            try
            {
                File.WriteAllText(fileName, textBox1.Text);
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError(this, ex);
                return;
            }

            _fileName = fileName;
            DialogResult = DialogResult.OK;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = this.BrowseForFile(_fileName, UiControls.EFileExtension.RScript, FileDialogMode.Open, _saveFolder);

            if (file != null)
            {
                textBox1.Text = File.ReadAllText(file);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = this.BrowseForFile(_fileName, UiControls.EFileExtension.RScript, FileDialogMode.SaveAs, _saveFolder);

            if (file != null)
            {
                File.WriteAllText(file, textBox1.Text);
            }
        }
    }
}
