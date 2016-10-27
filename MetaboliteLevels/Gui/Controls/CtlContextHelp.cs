using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Gui.Controls
{
    public partial class CtlContextHelp : Component
    {
        private CtlTitleBar _titleBar;
        private ToolTip _toolTip;
        private CtlContextHelpInner _panel1;        

        public CtlContextHelp()
        {
            this.InitializeComponent();
        }

        public CtlContextHelp( IContainer container )
        {
            container.Add( this );

            this.InitializeComponent();
        }

        public void Bind( CtlTitleBar titleBar, ToolTip toolTip )
        {
            this._titleBar = titleBar;
            this._toolTip = toolTip;
            toolTip.Active = true;
            toolTip.InitialDelay = 1;
            toolTip.Popup += this.ToolTip_Popup;

            this._panel1 = new CtlContextHelpInner();
            this._panel1.Dock = DockStyle.Right;
            this._panel1.Visible = false;
            this._panel1.VisibleChanged += this._panel1_VisibleChanged;
            this._panel1.Size = new System.Drawing.Size( 256, 0 );
            titleBar.FindForm().Controls.Add( this._panel1 );

            titleBar.HelpIcon = CtlTitleBar.EHelpIcon.ShowBar;
            titleBar.HelpClicked += this.TitleBar_HelpClicked;
        }

        private void _panel1_VisibleChanged( object sender, EventArgs e )
        {
            this._titleBar.HelpIcon = this._panel1.Visible ? CtlTitleBar.EHelpIcon.Off : CtlTitleBar.EHelpIcon.ShowBar;
        }

        private void TitleBar_HelpClicked( object sender, CancelEventArgs e )
        {
            this._panel1.Visible = !this._panel1.Visible; 
        }

        private void ToolTip_Popup( object sender, PopupEventArgs e )
        {
            if (this._panel1.Visible)
            {
                string text = this._toolTip.GetToolTip( e.AssociatedControl );
                this._panel1.Text = text;
            }

            e.Cancel = true;
        }
    }
}
