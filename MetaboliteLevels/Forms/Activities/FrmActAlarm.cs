using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Generic
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
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.BackColor = button1.BackColor == Color.White ? Color.Red : Color.White;

            NativeMethods.Beep(3000, 250);

            _count++;

            if (_count >= 6)
            {
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
