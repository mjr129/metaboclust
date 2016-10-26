using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Forms.Text
{
    public partial class FrmFloatingHelp : Form
    {
        public FrmFloatingHelp()
        {
            InitializeComponent();
        }

        internal void SetText( string text )
        {
            textBox1.Text = text;
        }
    }
}
