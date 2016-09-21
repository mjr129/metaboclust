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

            List<string> elements = StringHelper.SplitGroups(defaults);

            if (algo.HasCustomisableParams)
            {
                for (int index = 0; index < algo.Count; index++)
                {
                    var param = algo[index];
                    int row = index + 1;

                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0f));

                    Label label = new Label();
                    label.Text = param.Name;
                    label.AutoSize = true;
                    label.Visible = true;
                    label.Margin = new Padding(8, 8, 8, 8);
                    tableLayoutPanel1.Controls.Add(label, 0, row);

                    Label label2 = new Label();
                    label2.Text = param.Type.ToString();
                    label2.AutoSize = true;
                    label2.Visible = true;
                    label2.Margin = new Padding(8, 8, 8, 8);
                    tableLayoutPanel1.Controls.Add(label2, 1, row);

                    TextBox textBox = new TextBox();
                    textBox.Dock = DockStyle.Top;
                    textBox.Visible = true;
                    textBox.Margin = new Padding(8, 8, 8, 8);
                    textBox.Text = elements.Count > index ? elements[index] : "";
                    textBox.ReadOnly = readOnly;
                    tableLayoutPanel1.Controls.Add(textBox, 2, row);
                    _textBoxes.Add(textBox);

                    CtlButton button = new CtlButton();
                    button.Image = Resources.MnuEdit;
                    button.Visible = true;
                    button.UseDefaultSize = true;
                    button.Margin = new Padding(8, 8, 8, 8);
                    button.Tag = index;
                    button.Click += button_Click;
                    button.Enabled = !readOnly;
                    tableLayoutPanel1.Controls.Add(button, 3, row);
                }
            }

            if (readOnly)
            {
                _btnOk.Visible = false;
                _btnCancel.Text = "Close";
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        void button_Click(object sender, EventArgs e)
        {
            int index = (int)((Button)sender).Tag;
            TextBox textBox = _textBoxes[index];
            var param = this._parameters[index];

            object value = param.Type.FromString(_core, textBox.Text );

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

                results[index] = param.Type.FromString( _core, _textBoxes[index].Text );

                if (results[index] == null)
                {
                    FrmMsgBox.ShowError(this, "Error", "Enter a valid value for " + param.Name);
                    return;
                }
            }

            _result = AlgoParameterCollection.ParamsToReversableString(results, _core);
            DialogResult = DialogResult.OK;
        }
    }
}
