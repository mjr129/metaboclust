using MetaboliteLevels.Controls;
using MetaboliteLevels.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Utilities;
using MGui;
using MGui.Helpers;
using DataSet = MetaboliteLevels.Types.UI.DataSet;
using System.Diagnostics;

namespace MetaboliteLevels.Forms.Editing
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
            InitializeComponent();
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

                    tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.AutoSize, 0f ) );

                    Label label = new Label()
                    {
                        Text = param.Name,
                        AutoSize = true,
                        Visible = true,
                        Margin = new Padding( 8, 8, 8, 8 ),
                    };
                    tableLayoutPanel1.Controls.Add( label, 0, row );

                    Label label2 = new Label()
                    {
                        Text = param.Type.ToString(),
                        AutoSize = true,
                        Visible = true,
                        Margin = new Padding( 8, 8, 8, 8 ),
                    };
                    tableLayoutPanel1.Controls.Add( label2, 1, row );

                    TextBox textBox = new TextBox()
                    {
                        Dock = DockStyle.Top,
                        Visible = true,
                        Margin = new Padding( 8, 8, 8, 8 ),
                        Text = elements.Length > index ? elements[index] : "",
                        ReadOnly = readOnly,
                        Tag = index,
                    };
                    TextChanged += TextBox_TextChanged;
                    tableLayoutPanel1.Controls.Add( textBox, 2, row );
                    _textBoxes.Add( textBox );

                    CtlButton button = new CtlButton()
                    {
                        Image = Resources.MnuEnlargeList,
                        Visible = true,
                        UseDefaultSize = true,
                        Margin = new Padding( 8, 8, 8, 8 ),
                        Tag = index,
                        Enabled = !readOnly,
                    };
                    button.Click += button_Click;
                    tableLayoutPanel1.Controls.Add(button, 3, row);

                    TextBox_TextChanged( textBox, EventArgs.Empty );

                    toolTip1.SetToolTip( label,   param.Description );
                    toolTip1.SetToolTip( label2,  param.Description );
                    toolTip1.SetToolTip( textBox, param.Description );
                    toolTip1.SetToolTip( button,  param.Description );
                }
            }

            if (readOnly)
            {
                _btnOk.Visible = false;
                _btnCancel.Text = "Close";
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void TextBox_TextChanged( object sender, EventArgs e )
        {
            TextBox textBox = (TextBox)sender;
            int index = (int)textBox.Tag;       
            var param = this._parameters[index];

            var args = new FromStringArgs( _core, textBox.Text );
            object value = param.Type.FromString( args );

            ctlError1.Check( textBox, value != null, args.Error );
            _btnOk.Enabled = !ctlError1.HasErrors;
        }

        void button_Click(object sender, EventArgs e)
        {
            int index = (int)((Button)sender).Tag;
            TextBox textBox = _textBoxes[index];
            var param = this._parameters[index];                                         

            object value = param.Type.FromString(new FromStringArgs( _core, textBox.Text) ); // error ignored

            value = param.Type.Browse( this, _core, value );

            if (value == null)
            {
                return;
            }

            textBox.Text = AlgoParameterCollection.ParamsToReversableString(new[] { value }, _core);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            object[] results = new object[_parameters.Count];

            for (int index = 0; index < _parameters.Count; index++)
            {
                var param = _parameters[index];
                var args = new FromStringArgs( _core, _textBoxes[index].Text );

                results[index] = param.Type.FromString( args );

                Debug.Assert( results[index] != null, $"The {{{param.Name}}} parameter is invalid (the OK button should have been disabled).\r\n" + args.Error );
            }

            _result = AlgoParameterCollection.ParamsToReversableString(results, _core);
            DialogResult = DialogResult.OK;
        }
    }
}
