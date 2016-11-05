using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Definition;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    /// <summary>
    /// Warns user about using data saved using a previous version of the software.
    /// </summary>
    public sealed partial class FrmActOldData : Form
    {
        DataFileNames _dfn;

        public FrmActOldData()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        internal FrmActOldData(DataFileNames dfn)
            : this()
        {                                         
            Version file = dfn.AppVersion;
            Version current = UiControls.Version;
            string older = (file < current) ? "older" : "newer";

            this.ctlTitleBar1.SubText = this.ctlTitleBar1.SubText.Replace("{older}", older).Replace("{product}", UiControls.Title);
            this.Text = dfn.Title;
            this._dfn = dfn;

            // UiControls.CompensateForVisualStyles(this);
        }

        internal static bool Show(Form owner, DataFileNames dfn)
        {
            // Clear session filename to avoid accidentally saving corrupt information
            dfn.ForceSaveAs = true;

            if (DoNotShowAgain)
            {
                return true;
            }

            using (FrmActOldData frm = new FrmActOldData(dfn))
            {
                return UiControls.ShowWithDim(owner, frm) == DialogResult.OK;
            }
        }

        private static bool DoNotShowAgain
        {
            get
            {
                return MainSettings.Instance.DoNotShowAgain.ContainsKey("FrmOldData");
            }
            set
            {
                if (value)
                {
                    MainSettings.Instance.DoNotShowAgain["FrmOldData"] = 1;
                }
                else
                {
                    MainSettings.Instance.DoNotShowAgain.Remove("FrmOldData");
                }

                MainSettings.Instance.Save( MainSettings.EFlags.DoNotShowAgain);
            }
        }        

        private void _chkNotAgain_CheckedChanged(object sender, EventArgs e)
        {
            DoNotShowAgain = this._chkNotAgain.Checked;
        }

        private void _btnDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmInputMultiLine.ShowFixed(this, this.Text, "Data file sources", "These were the files used to create the initial dataset. Note that these files may have been moved or renamed since the dataset's creation.", this._dfn.GetDetails());
        }
    }
}
