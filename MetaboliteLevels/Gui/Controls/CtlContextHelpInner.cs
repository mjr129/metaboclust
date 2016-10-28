using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Gui.Forms.Wizards;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls
{
    public partial class CtlContextHelpInner : UserControl
    {
        private string _fileFormatDetails;     
        public event EventHandler CloseClicked;
        public string DefaultText = "Click or focus on an input for context help";

        public CtlContextHelpInner()
        {
            this.InitializeComponent();
        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {

        }

        public new string Text
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                if (string.IsNullOrEmpty( value ))
                {
                    value = DefaultText;
                }

                if (HandleFileFormats)
                {
                    value = Regex.Replace( value, "(?<!\r)\n", "\r\n" );

                    if (value.StartsWith( "FILEFORMAT" ))
                    {
                        this.textBox1.Text = value.After( "FILEFORMAT\r\n" ).Before( "{" ).Replace( "CSVFILE", "The name of a CSV (spreadsheet) file. Click the button below to shown specific details on the data expected." );
                        this.toolStrip1.Visible = true;
                        _fileFormatDetails = value;
                    }
                    else
                    {
                        this.textBox1.Text = value;
                        this.toolStrip1.Visible = false;
                    }
                }
                else
                {
                    this.textBox1.Text = value;
                }
            }
        }

        public bool HandleFileFormats { get; set; }

        private void ctlTitleBar1_HelpClicked( object sender, CancelEventArgs e )
        {
            this.CloseClicked?.Invoke( this, EventArgs.Empty );
        }

        private void _btnFileFormatDetails_Click( object sender, EventArgs e )
        {
            FrmViewSpreadsheet.Show( this, this._fileFormatDetails );
        }
    }
}
