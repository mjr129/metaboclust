using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    public partial class FrmActAlarm : Form
    {
        private int _count;

        public static void Show(Form owner)
        {
            using (FrmActAlarm frm = new FrmActAlarm())
            {
                UiControls.ShowWithDim(owner, frm);
            }
        }

        private FrmActAlarm()
        {
            this.InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.button1.BackColor = this.button1.BackColor == Color.White ? Color.Red : Color.White;

            NativeMethods.Beep(3000, 250);

            this._count++;

            if (this._count >= 6)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
