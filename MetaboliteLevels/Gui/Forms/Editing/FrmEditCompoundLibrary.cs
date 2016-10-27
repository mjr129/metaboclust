using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Definition;
using MetaboliteLevels.Gui.Forms.Wizards;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    public partial class FrmEditCompoundLibrary : Form
    {
        internal static CompoundLibrary Show(Form owner)
        {
            using (FrmEditCompoundLibrary frm = new FrmEditCompoundLibrary())
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm.GetSelection();
                }

                return null;
            }
        }

        private FrmEditCompoundLibrary()
        {
            this.InitializeComponent();
        }

        private void anything_Changed(object sender, EventArgs e)
        {
            this._lblCompounds.Visible = this._radCsvFile.Checked;
            this._lblPathways.Visible = this._radCsvFile.Checked;
            this._txtCompounds.Visible = this._radCsvFile.Checked;
            this._txtPathways.Visible = this._radCsvFile.Checked;
            this._btnCompounds.Visible = this._radCsvFile.Checked;
            this._btnPathways.Visible = this._radCsvFile.Checked;
            this._lblPathwayTools.Visible = this._radPathwayTools.Checked;
            this._txtPathwayTools.Visible = this._radPathwayTools.Checked;
            this._btnPathwayTools.Visible = this._radPathwayTools.Checked;
            this._btnOk.Enabled = this.GetSelection() != null;
        }

        private CompoundLibrary GetSelection()
        {
            if (this._radCsvFile.Checked)
            {
                if (string.IsNullOrWhiteSpace(this._txtCompounds.Text)
                    || string.IsNullOrWhiteSpace(this._txtCompounds.Text)
                  || string.IsNullOrWhiteSpace(this._txtPathways.Text))
                {
                    return null;
                }

                return new CompoundLibrary(this._txtCompounds.Text, this._txtCompounds.Text, this._txtPathways.Text);
            }
            else if (this._radPathwayTools.Checked)
            {
                if (string.IsNullOrWhiteSpace(this._txtPathwayTools.Text))
                {
                    return null;
                }

                string dir = Path.GetDirectoryName(this._txtPathwayTools.Text);
                return new CompoundLibrary(dir, dir);
            }

            return null;
        }

        private void _btnCompounds_Click_1(object sender, EventArgs e)
        {
            FrmEditDataFileNames.Browse(this._txtCompounds);
        }

        private void _btnPathways_Click_1(object sender, EventArgs e)
        {
            FrmEditDataFileNames.Browse(this._txtPathways);
        }

        private void _btnPathwayTools_Click(object sender, EventArgs e)
        {
            FrmEditDataFileNames.Browse(this._txtPathwayTools, "Pathway tools databases (*.dat)|*.dat|All files (*.*)|*.*");
        }
    }
}
