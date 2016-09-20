using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    class CtlLabel : Label
    {
        private ELabelStyle _labelStyle;

        public CtlLabel()
        {
            ApplyStyle();
        }

        [DefaultValue( ELabelStyle.Normal)]
        public ELabelStyle LabelStyle
        {
            get
            {
                return _labelStyle;
            }
            set
            {
                _labelStyle = value;
                ApplyStyle();
            }
        }

        private void ApplyStyle()
        {
            switch (_labelStyle)
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
