using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Text
{
    internal partial class FrmInputScript : Form
    {
        private UiControls.EInitialFolder _saveFolder;
        private string _fileName;

        internal static string Show(Form owner, string title, string inputTable, UiControls.EInitialFolder folder, string fileName, bool workOnCopy, string defaultContent, bool readOnly)
        {
            using (FrmInputScript frm = new FrmInputScript(title, inputTable, folder, fileName, workOnCopy, defaultContent, readOnly))
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
            this.InitializeComponent();
            this._mnuStrip.BackColor = UiControls.TitleBackColour;
            this._mnuFile.ForeColor = UiControls.TitleForeColour;

            UiControls.SetIcon(this);
        }

        public FrmInputScript(string title, string inputTable, UiControls.EInitialFolder saveFolder, string fileName, bool workOnCopy, string defaultContent, bool readOnly)
            : this()
        {
            this.Text = "Script Editor";
            this._titleBar.Text = title;
            this._saveFolder = saveFolder;

            RScript.RScriptMarkup markup = new RScript.RScriptMarkup( inputTable );

            StringBuilder defaultCode = new StringBuilder();
            StringBuilder helpText = new StringBuilder();

            helpText.AppendLine( title.ToUpper() + " OUTLINE" );
            helpText.AppendLine( markup.Summary );
            helpText.AppendLine();

            foreach (RScript.RScriptMarkupElement element in markup.Inputs)
            {                
                defaultCode.AppendLine("## " + element.Name + " = " + element.Key);

                helpText.AppendLine( element.Key.PadRight( 16 ) + ": " + element.Comment );

                if (element.Name == "-")
                {
                    helpText.AppendLine( new string( ' ', 16 ) + "  Not available by default." );
                }
                else
                {
                    helpText.AppendLine( new string( ' ', 16 ) + "  Default name: " + element.Name );
                }

                helpText.AppendLine();
            }

            helpText.AppendLine( "EXPECTED RESULT".PadRight( 16 ) + ": " + markup.ReturnValue );
            helpText.AppendLine();
            helpText.AppendLine(Resx.Manual.RScript);
            this._titleBar.HelpText = helpText.ToString();

            defaultCode.AppendLine(); 

            this._titleBar.SubText = "Enter an R script. Click the help button to see the available inputs.";

            if (string.IsNullOrEmpty(defaultContent))
            {
                this.textBox1.Text = defaultCode.ToString();
            }
            else
            {
                this.textBox1.Text = defaultContent;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                this.toolStripStatusLabel1.Text = "NEW FILE";
            }
            else if (workOnCopy)
            {
                this.toolStripStatusLabel1.Text = "COPY OF: " + fileName;
            }
            else
            {
                this._fileName = fileName;
                this.toolStripStatusLabel1.Text = fileName;
            }

            if (readOnly)
            {
                this._btnOk.Visible = false;
                this.btnCancel.Text = "Close";
                this.textBox1.ReadOnly = true;
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            string fileName;

            if (string.IsNullOrEmpty(this._fileName))
            {
                string text = FrmInputSingleLine.Show(this, this.Text, "Save changes", "Please enter a name for your script", "");

                if (string.IsNullOrEmpty(text))
                {
                    return;
                }

                fileName = Path.Combine(UiControls.GetOrCreateFixedFolder(this._saveFolder), text + ".r");
            }
            else if (FrmMsgBox.ShowYesNo(this, this.Text, "Save changes?\r\n" + this._fileName, Resources.MsgInfo))
            {
                fileName = this._fileName;
            }
            else
            {
                return;
            }

            try
            {
                File.WriteAllText(fileName, this.textBox1.Text);
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError(this, ex);
                return;
            }

            this._fileName = fileName;
            this.DialogResult = DialogResult.OK;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = this.BrowseForFile(this._fileName, UiControls.EFileExtension.RScript, FileDialogMode.Open, this._saveFolder);

            if (file != null)
            {
                this.textBox1.Text = File.ReadAllText(file);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = this.BrowseForFile(this._fileName, UiControls.EFileExtension.RScript, FileDialogMode.SaveAs, this._saveFolder);

            if (file != null)
            {
                File.WriteAllText(file, this.textBox1.Text);
            }
        }
    }
}
