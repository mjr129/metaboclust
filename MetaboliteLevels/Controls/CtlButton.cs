using MetaboliteLevels.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MetaboliteLevels.Controls
{
    class CtlButton : Button
    {
        private string _text;
        private bool _fixedSize;

        public CtlButton()
        {
            Image = Resources.MnuAccept;
            FixAppearence();
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool UseDefaultSize
        {
            get
            {
                return _fixedSize;
            }
            set
            {
                _fixedSize = value;
                FixAppearence();
            }
        }

        private void FixAppearence()
        {
            bool noText = string.IsNullOrEmpty(Text);

            if (_fixedSize)
            {
                if (noText)
                {
                    base.Size = new Size(29, 29);
                }
                else
                {
                    base.Size = new Size(128, 40);
                }
            }

            if (noText)
            {
                base.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                base.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                base.TextImageRelation = TextImageRelation.Overlay;
                base.Padding = Padding.Empty;
            }
            else
            {
                base.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                base.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                base.TextImageRelation = TextImageRelation.ImageBeforeText;
                base.Padding = new Padding(4, 4, 4, 4);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                base.Text = "  " + _text;
                FixAppearence();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override System.Drawing.ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
            set { /* Ignore */ }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Drawing.ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
            set { /* Ignore */ }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new TextImageRelation TextImageRelation
        {
            get { return base.TextImageRelation; }
            set { /* Ignore */ }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { /* Ignore */ }
        }

        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (!_fixedSize)
                {
                    base.Size = value;
                }
            }
        }
    }
}
