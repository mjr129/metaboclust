using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Gui.Controls
{
    public partial class CtlHelpfulLabel : UserControl
    {
        public CtlHelpfulLabel()
        {
            this.InitializeComponent();
        }

        [EditorBrowsable( EditorBrowsableState.Always ), Browsable( true ), DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
        public override string Text
        {
            get
            {
                return this.label1.Text;
            }      
            set
            {
                this.label1.Text = value;
            }
        }

        private void button1_Click( object sender, EventArgs e )
        {
            this.OnClick( e );
        }
    }
}
