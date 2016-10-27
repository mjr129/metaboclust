using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Gui.Controls
{
    public partial class CtlContextHelpInner : UserControl
    {                            
        public CtlContextHelpInner()
        {
            this.InitializeComponent();
        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {

        }

        public string Text
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }

        private void ctlTitleBar1_HelpClicked( object sender, CancelEventArgs e )
        {
            this.Visible = false;
        }
    }
}
