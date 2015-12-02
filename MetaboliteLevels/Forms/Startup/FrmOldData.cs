using System;
using System.Text;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Startup
{
    /// <summary>
    /// Warns user about using data saved using a previous version of the software.
    /// </summary>
    public partial class FrmOldData : Form
    {
        bool allowSaveSetting;
        string _breakingChanges;
        DataFileNames dfn;

        public FrmOldData()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        internal FrmOldData(DataFileNames dfn, StringBuilder breakingChanges)
            : this()
        {
            bool bc = breakingChanges.Length != 0;

            Version file = dfn.AppVersion;
            Version current = UiControls.Version;
            string older = (file < current) ? "older" : "newer";

            ctlTitleBar1.SubText = ctlTitleBar1.SubText.Replace("{older}", older).Replace("{product}", UiControls.Title);
            _btnBigChange.Visible = bc;
            _lblSmallChange.Visible = !bc;
            _breakingChanges = breakingChanges.ToString();
            Text = dfn.Title;
            _chkNotAgain.Visible = !bc;
            _chkNotAgain.Checked = false;
            allowSaveSetting = !bc;
            this.dfn = dfn;

            UiControls.CompensateForVisualStyles(this);
        }

        internal static bool Show(Form owner, DataFileNames dfn)
        {
            StringBuilder breakingChanges = new StringBuilder();

            foreach (var v in UiControls.BreakingVersions)
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

            using (FrmOldData frm = new FrmOldData(dfn, breakingChanges))
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
                return CtlHelpBar.GetHidden("FrmOldData");
            }
            set
            {
                CtlHelpBar.SetHidden("FrmOldData", value);
            }
        }

        private void _btnIncompat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmInputLarge.ShowFixed(this, Text, "Breaking changes", "Breaking changes are changes that have been made to newer versions of the software which may prevent data saved in older versions from loading correctly", _breakingChanges);
        }

        private void _chkNotAgain_CheckedChanged(object sender, EventArgs e)
        {
            if (allowSaveSetting)
            {
                DoNotShowAgain = _chkNotAgain.Checked;
            }
        }

        private void _btnDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmInputLarge.ShowFixed(this, Text, "Data file sources", "These were the files used to create the initial dataset. Note that these files may have been moved or renamed since the dataset's creation.", dfn.GetDetails());
        }
    }
}
