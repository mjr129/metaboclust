using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Selection
{
    public partial class FrmSelectClosure : Form
    {
        const string SAVE_ON_CLOSE = "FrmSelectClosure";

        internal static bool? Show(Form owner, Core core)
        {
            if (MainSettings.Instance.DoNotShowAgain.ContainsKey( SAVE_ON_CLOSE ))
            {
                return MainSettings.Instance.DoNotShowAgain[SAVE_ON_CLOSE] == 1;
            }

            if (core.FileNames.SaveOnClose.HasValue)
            {
                return core.FileNames.SaveOnClose;
            }

            using (FrmSelectClosure frm = new FrmSelectClosure(core))
            {
                bool? newValue = UiControls.ShowWithDim(owner, frm).ToBoolean();

                switch (frm.comboBox1.SelectedIndex)
                {
                    case 0: // ignore
                        break;

                    case 1: // remember this session
                        Debug.Assert( newValue.HasValue );
                        core.FileNames.SaveOnClose = newValue;
                        break;

                    case 2: // remember always
                        Debug.Assert( newValue.HasValue );
                        MainSettings.Instance.DoNotShowAgain[SAVE_ON_CLOSE] = newValue.Value ? 1 : 0;
                        MainSettings.Instance.Save(MainSettings.EFlags.General);
                        break;
                }

                return newValue;
            }
        }

        public FrmSelectClosure()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        internal FrmSelectClosure(Core core)
            : this()
        {
            //this.Text = "Close " + core.FileNames.Title;
            this.textBox1.Text = core.FileNames.Session;
            this.comboBox1.SelectedIndex = 0;

            // UiControls.CompensateForVisualStyles(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {                                                           
            this._btnCancel.Enabled = this.comboBox1.SelectedIndex == 0;
        }
    }
}
