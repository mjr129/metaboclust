using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    public partial class FrmEditParameters : Form
    {
        private AlgoParameterCollection _parameters;
        private List<TextBox> _textBoxes = new List<TextBox>();
        private Core _core;
        private string _result;

        internal static void Show(AlgoBase algo, TextBox paramBox, Core core, bool readOnly)
        {
            string newText = Show(paramBox.FindForm(), core, algo.Parameters, paramBox.Text, readOnly);

            if (newText != null)
            {
                paramBox.Text = newText;
            }
        }

        internal static string Show(Form owner, Core core, AlgoParameterCollection algo, string defaults, bool readOnly)
        {
            using (FrmEditParameters frm = new FrmEditParameters(core, algo, defaults, readOnly))
            {
                if (frm.ShowDialog(owner) == DialogResult.OK)
                {
                    return frm._result;
                }
            }

            return null;
        }

        private FrmEditParameters()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmEditParameters(Core core, AlgoParameterCollection algo, string defaults, bool readOnly)
            : this()
        {
            this._core = core;
            this._parameters = algo;
            this.ctlTitleBar1.Text = readOnly ? "View parameters" : "Edit Parameters";

            string[] elements = AlgoParameterTypes.ExternalConvertor.ReadFields(defaults);

            if (algo.HasCustomisableParams)
            {
                for (int index = 0; index < algo.Count; index++)
                {
                    var param = algo[index];
                    int row = index + 1;

                    this.tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.AutoSize, 0f ) );

                    Label label = new Label()
                    {
                        Text = param.Name,
                        AutoSize = true,
                        Visible = true,
                        Margin = new Padding( 8, 8, 8, 8 ),
                    };
                    this.tableLayoutPanel1.Controls.Add( label, 0, row );

                    Label label2 = new Label()
                    {
                        Text = param.Type.ToString(),
                        AutoSize = true,
                        Visible = true,
                        Margin = new Padding( 8, 8, 8, 8 ),
                    };
                    this.tableLayoutPanel1.Controls.Add( label2, 1, row );

                    TextBox textBox = new TextBox()
                    {
                        Dock = DockStyle.Top,
                        Visible = true,
                        Margin = new Padding( 8, 8, 8, 8 ),
                        Text = elements.Length > index ? elements[index] : "",
                        ReadOnly = readOnly,
                        Tag = index,
                    };
                    textBox.TextChanged += this.TextBox_TextChanged;
                    this.tableLayoutPanel1.Controls.Add( textBox, 2, row );
                    this._textBoxes.Add( textBox );

                    CtlButton button = new CtlButton()
                    {
                        Image = Resources.MnuEnlargeList,
                        Visible = true,
                        UseDefaultSize = true,
                        Margin = new Padding( 8, 8, 8, 8 ),
                        Tag = index,
                        Enabled = !readOnly,
                    };
                    button.Click += this.button_Click;
                    this.tableLayoutPanel1.Controls.Add(button, 3, row);

                    this.TextBox_TextChanged( textBox, EventArgs.Empty );

                    this.toolTip1.SetToolTip( label,   param.Description );
                    this.toolTip1.SetToolTip( label2,  param.Description );
                    this.toolTip1.SetToolTip( textBox, param.Description );
                    this.toolTip1.SetToolTip( button,  param.Description );
                }
            }

            if (readOnly)
            {
                this._btnOk.Visible = false;
                this._btnCancel.Text = "Close";
            }

            ctlContextHelp1.Bind( this, ctlTitleBar1, toolTip1, CtlContextHelp.EFlags.HelpOnClick | CtlContextHelp.EFlags.HelpOnFocus );
        }

        private void TextBox_TextChanged( object sender, EventArgs e )
        {
            TextBox textBox = (TextBox)sender;
            int index = (int)textBox.Tag;       
            var param = this._parameters[index];

            var args = new FromStringArgs( this._core, textBox.Text );
            object value = param.Type.FromString( args );

            this.ctlError1.Check( textBox, value != null, args.Error );
            this._btnOk.Enabled = !this.ctlError1.HasErrors;
        }

        void button_Click(object sender, EventArgs e)
        {
            int index = (int)((Button)sender).Tag;
            TextBox textBox = this._textBoxes[index];
            var param = this._parameters[index];                                         

            object value = param.Type.FromString(new FromStringArgs( this._core, textBox.Text) ); // error ignored

            value = param.Type.Browse( this, this._core, value );

            if (value == null)
            {
                return;
            }

            textBox.Text = AlgoParameterCollection.ParamToString(value);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            object[] results = new object[this._parameters.Count];

            for (int index = 0; index < this._parameters.Count; index++)
            {
                var param = this._parameters[index];
                var args = new FromStringArgs( this._core, this._textBoxes[index].Text );

                results[index] = param.Type.FromString( args );

                Debug.Assert( results[index] != null, $"The {{{param.Name}}} parameter is invalid (the OK button should have been disabled).\r\n" + args.Error );
            }

            this._result = AlgoParameterCollection.ParamsToReversableString(results, this._core);
            this.DialogResult = DialogResult.OK;
        }
    }
}
