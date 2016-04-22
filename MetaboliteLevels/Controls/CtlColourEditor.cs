using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Controls
{
    class CtlColourEditor : Button
    {
        public CtlColourEditor()
        {
            UpdateAppearance();
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            } 
            set
            {
                base.BackColor = value;
                UpdateAppearance();
            }
        }

        [EditorBrowsable( EditorBrowsableState.Never ), Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public override string Text
        {
            get { return base.Text; }
            set { /* N/A */ }
        }

        [EditorBrowsable( EditorBrowsableState.Never ), Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public override Color ForeColor
        {
            get { return base.ForeColor; }     
            set { /* N/A */ }
        }

        [EditorBrowsable( EditorBrowsableState.Never ), Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public new Size Size
        {
            get { return base.Size; }
            set { /* N/A */ }
        }

        private void UpdateAppearance()
        {
            base.ForeColor = UiControls.ComplementaryColour( base.BackColor );
            base.Text = UiControls.ColourToName( base.BackColor );
            base.Size = new Size( 128, 29 );
        }

        protected override void OnClick( EventArgs e )
        {                               
            Color colour = BackColor;

            if (UiControls.EditColor( ref colour ))
            {
                BackColor = colour;
            }                                                 
        }
    }
}
