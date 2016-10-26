using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    public partial class CtlContextHelpInner : UserControl
    {                            
        public CtlContextHelpInner()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {

        }

        public string Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        private void ctlTitleBar1_HelpClicked( object sender, CancelEventArgs e )
        {
            Visible = false;
        }
    }
}
