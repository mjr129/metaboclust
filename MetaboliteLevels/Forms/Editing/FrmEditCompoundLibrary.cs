using MetaboliteLevels.Data.General;
using MetaboliteLevels.Utilities;
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

namespace MetaboliteLevels.Forms.Startup
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
            InitializeComponent();
        }

        private void anything_Changed(object sender, EventArgs e)
        {
            _lblCompounds.Visible = _radCsvFile.Checked;
            _lblPathways.Visible = _radCsvFile.Checked;
            _txtCompounds.Visible = _radCsvFile.Checked;
            _txtPathways.Visible = _radCsvFile.Checked;
            _btnCompounds.Visible = _radCsvFile.Checked;
            _btnPathways.Visible = _radCsvFile.Checked;
            _lblPathwayTools.Visible = _radPathwayTools.Checked;
            _txtPathwayTools.Visible = _radPathwayTools.Checked;
            _btnPathwayTools.Visible = _radPathwayTools.Checked;
            _btnOk.Enabled = GetSelection() != null;
        }

        private CompoundLibrary GetSelection()
        {
            if (_radCsvFile.Checked)
            {
                if (string.IsNullOrWhiteSpace(_txtCompounds.Text)
                    || string.IsNullOrWhiteSpace(_txtCompounds.Text)
                  || string.IsNullOrWhiteSpace(_txtPathways.Text))
                {
                    return null;
                }

                return new CompoundLibrary(_txtCompounds.Text, _txtCompounds.Text, _txtPathways.Text);
            }
            else if (_radPathwayTools.Checked)
            {
                if (string.IsNullOrWhiteSpace(_txtPathwayTools.Text))
                {
                    return null;
                }

                string dir = Path.GetDirectoryName(_txtPathwayTools.Text);
                return new CompoundLibrary(dir, dir);
            }

            return null;
        }

        private void _btnCompounds_Click_1(object sender, EventArgs e)
        {
            FrmEditDataFileNames.Browse(_txtCompounds);
        }

        private void _btnPathways_Click_1(object sender, EventArgs e)
        {
            FrmEditDataFileNames.Browse(_txtPathways);
        }

        private void _btnPathwayTools_Click(object sender, EventArgs e)
        {
            FrmEditDataFileNames.Browse(_txtPathwayTools, "Pathway tools databases (*.dat)|*.dat|All files (*.*)|*.*");
        }
    }
}
