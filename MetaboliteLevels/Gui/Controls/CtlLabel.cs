using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Gui.Controls
{
    class CtlLabel : Label
    {
        private ELabelStyle _labelStyle;

        public CtlLabel()
        {
            this.ApplyStyle();
        }

        [DefaultValue( ELabelStyle.Normal)]
        public ELabelStyle LabelStyle
        {
            get
            {
                return this._labelStyle;
            }
            set
            {
                this._labelStyle = value;
                this.ApplyStyle();
            }
        }

        private void ApplyStyle()
        {
            switch (this._labelStyle)
            {
                case ELabelStyle.Normal:
                    base.ForeColor = Color.Black;
                    break;

                case ELabelStyle.Caption:
                    base.ForeColor = Color.Purple;
                    break;

                case ELabelStyle.Highlight:
                    base.ForeColor = Color.Blue;
                    break;
            }
        }

        [EditorBrowsable( EditorBrowsableState.Never ), Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }                         
            set
            {
                base.ForeColor = value;
            }
        }

        [EditorBrowsable( EditorBrowsableState.Never ), Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
    }

    public enum ELabelStyle
    {
        Normal,
        Caption,
        Highlight,
    }
}
