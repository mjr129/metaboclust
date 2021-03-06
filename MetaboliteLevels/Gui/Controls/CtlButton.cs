﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Controls
{
    /// <summary>
    /// A simple subclass of Button that defaults some values more suited to an image-text
    /// appearance.
    /// </summary>
    class CtlButton : Button
    {
        private string _text;
        private bool _fixedSize;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public CtlButton()
        {                               
            this.FixAppearence();
        }

        /// <summary>
        /// Gets or sets whether to use the fixed standard size.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool UseDefaultSize
        {
            get
            {
                return this._fixedSize;
            }
            set
            {
                this._fixedSize = value;
                this.FixAppearence();
            }
        }

        /// <summary>
        /// Sets the appearance of the button to the standard layout.
        /// </summary>
        private void FixAppearence()
        {
            bool noText = string.IsNullOrEmpty(this.Text);

            if (this._fixedSize)
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

            // If visual styles are off then colour our button (unless it has a specific colour set)
            if (!Application.RenderWithVisualStyles
                && !UiControls.IsDesigning 
                && (this.FlatStyle == FlatStyle.Standard
                    || this.FlatStyle == FlatStyle.System))
            {
                this.UseVisualStyleBackColor = false;
                this.BackColor = Color.CornflowerBlue;
                this.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Override property to insert spaces before intended text (to separate text and image a bit more).
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                base.Text = "  " + this._text;
                this.FixAppearence();
            }
        }

        /// <summary>
        /// Override property to disallow external changes and stop the VS editor trying to serialise them.
        /// (We always left-align the text)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override System.Drawing.ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
            set { /* Ignore */ }
        }

        /// <summary>
        /// Override property to disallow external changes and stop the VS editor trying to serialise them.
        /// (We always left-align the image)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Drawing.ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
            set { /* Ignore */ }
        }

        /// <summary>
        /// Override property to disallow external changes and stop the VS editor trying to serialise them.
        /// (We always display image-text)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new TextImageRelation TextImageRelation
        {
            get { return base.TextImageRelation; }
            set { /* Ignore */ }
        }

        /// <summary>
        /// Override property to disallow external changes and stop the VS editor trying to serialise them.
        /// (We always use a fixed padding)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { /* Ignore */ }
        }

        /// <summary>
        /// Override property to disallow external changes when _fixedSize is set.
        /// </summary>
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (!this._fixedSize)
                {
                    base.Size = value;
                }
            }
        }
    }
}
