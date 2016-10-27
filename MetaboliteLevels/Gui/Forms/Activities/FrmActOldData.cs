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
    public partial class FrmActOldData : Form
    {
        bool _allowSaveSetting;
        string _breakingChanges;
        DataFileNames _dfn;

        public FrmActOldData()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        internal FrmActOldData(DataFileNames dfn, StringBuilder breakingChanges)
            : this()
        {
            bool bc = breakingChanges.Length != 0;

            Version file = dfn.AppVersion;
            Version current = UiControls.Version;
            string older = (file < current) ? "older" : "newer";

            this.ctlTitleBar1.SubText = this.ctlTitleBar1.SubText.Replace("{older}", older).Replace("{product}", UiControls.Title);
            this._btnBigChange.Visible = bc;
            this._lblSmallChange.Visible = !bc;
            this._breakingChanges = breakingChanges.ToString();
            this.Text = dfn.Title;
            this._chkNotAgain.Visible = !bc;
            this._chkNotAgain.Checked = false;
            this._allowSaveSetting = !bc;
            this._dfn = dfn;

            // UiControls.CompensateForVisualStyles(this);
        }

        internal static bool Show(Form owner, DataFileNames dfn)
        {
            StringBuilder breakingChanges = new StringBuilder();

            foreach (KeyValuePair<Version, string> v in UiControls.VersionHistory)
            {
                if (dfn.AppVersion <= v.Key)
                {
                    breakingChanges.AppendLine(" * " + v.Value);
                }
            }

            if (breakingChanges.Length == 0 && DoNotShowAgain)
            {
                return true;
            }

            using (FrmActOldData frm = new FrmActOldData(dfn, breakingChanges))
            {
                var result = UiControls.ShowWithDim(owner, frm) == DialogResult.OK;

                if (breakingChanges.Length != 0)
                {
                    // Clear session filename to avoid accidentally saving corrupt information
                    dfn.ForceSaveAs = true;
                }

                return result;
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

                MainSettings.Instance.Save();
            }
        }

        private void _btnIncompat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmInputMultiLine.ShowFixed(this, this.Text, "Breaking changes", "Breaking changes are changes that have been made to newer versions of the software which may prevent data saved in older versions from loading correctly", this._breakingChanges);
        }

        private void _chkNotAgain_CheckedChanged(object sender, EventArgs e)
        {
            if (this._allowSaveSetting)
            {
                DoNotShowAgain = this._chkNotAgain.Checked;
            }
        }

        private void _btnDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmInputMultiLine.ShowFixed(this, this.Text, "Data file sources", "These were the files used to create the initial dataset. Note that these files may have been moved or renamed since the dataset's creation.", this._dfn.GetDetails());
        }
    }
}
