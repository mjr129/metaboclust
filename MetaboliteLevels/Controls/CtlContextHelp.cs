using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Forms.Text;

namespace MetaboliteLevels.Controls
{
    public partial class CtlContextHelp : Component
    {
        private CtlTitleBar _titleBar;
        private ToolTip _toolTip;
        private CtlContextHelpInner _panel1;        

        public CtlContextHelp()
        {
            InitializeComponent();
        }

        public CtlContextHelp( IContainer container )
        {
            container.Add( this );

            InitializeComponent();
        }

        public void Bind( CtlTitleBar titleBar, ToolTip toolTip )
        {
            _titleBar = titleBar;
            _toolTip = toolTip;
            toolTip.Active = true;
            toolTip.InitialDelay = 1;
            toolTip.Popup += ToolTip_Popup;

            _panel1 = new CtlContextHelpInner();
            _panel1.Dock = DockStyle.Right;
            _panel1.Visible = false;
            _panel1.VisibleChanged += _panel1_VisibleChanged;
            _panel1.Size = new System.Drawing.Size( 256, 0 );
            titleBar.FindForm().Controls.Add( _panel1 );

            titleBar.HelpIcon = CtlTitleBar.EHelpIcon.ShowBar;
            titleBar.HelpClicked += TitleBar_HelpClicked;
        }

        private void _panel1_VisibleChanged( object sender, EventArgs e )
        {
            _titleBar.HelpIcon = _panel1.Visible ? CtlTitleBar.EHelpIcon.Off : CtlTitleBar.EHelpIcon.ShowBar;
        }

        private void TitleBar_HelpClicked( object sender, CancelEventArgs e )
        {
            _panel1.Visible = !_panel1.Visible; 
        }

        private void ToolTip_Popup( object sender, PopupEventArgs e )
        {
            if (_panel1.Visible)
            {
                string text = _toolTip.GetToolTip( e.AssociatedControl );
                _panel1.Text = text;
            }

            e.Cancel = true;
        }
    }
}
