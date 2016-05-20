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
    public partial class CtlHelpfulLabel : UserControl
    {
        public CtlHelpfulLabel()
        {
            InitializeComponent();
        }

        [EditorBrowsable( EditorBrowsableState.Always ), Browsable( true ), DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
        public override string Text
        {
            get
            {
                return label1.Text;
            }      
            set
            {
                label1.Text = value;
            }
        }

        private void button1_Click( object sender, EventArgs e )
        {
            this.OnClick( e );
        }
    }
}
