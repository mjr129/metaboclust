using System;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Startup
{
    public partial class FrmClosing : Form
    {
        internal static bool? Show(Form owner, Core core)
        {
            if (MainSettings.Instance.General.SaveOnClose.HasValue)
            {
                return MainSettings.Instance.General.SaveOnClose;
            }

            if (core.FileNames.SaveOnClose.HasValue)
            {
                return core.FileNames.SaveOnClose;
            }

            using (FrmClosing frm = new FrmClosing(core))
            {
                bool? newValue = UiControls.ShowWithDim(owner, frm).ToBoolean();

                switch (frm.comboBox1.SelectedIndex)
                {
                    case 0: // ignore
                        break;

                    case 1: // remember this session
                        core.FileNames.SaveOnClose = newValue;
                        break;

                    case 2: // remember always
                        MainSettings.Instance.General.SaveOnClose = newValue;
                        MainSettings.Instance.Save();
                        break;
                }

                return newValue;
            }
        }

        public FrmClosing()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        internal FrmClosing(Core core)
            : this()
        {
            //this.Text = "Close " + core.FileNames.Title;
            this.textBox1.Text = core.FileNames.Session;
            this.comboBox1.SelectedIndex = 0;

            // UiControls.CompensateForVisualStyles(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _btnNo.Enabled = comboBox1.SelectedIndex != 1;
            _btnCancel.Enabled = comboBox1.SelectedIndex == 0;
        }
    }
}
