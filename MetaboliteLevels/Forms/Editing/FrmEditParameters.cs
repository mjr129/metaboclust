using MetaboliteLevels.Algorithms.Statistics;
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
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmEditParameters : Form
    {
        private AlgoParameters _algo;
        private List<TextBox> _textBoxes = new List<TextBox>();
        private Core _core;
        private string _result;

        internal static void Show(AlgoBase algo, TextBox paramBox, Core core)
        {
            string newText = FrmEditParameters.Show(paramBox.FindForm(), core, algo.GetParams(), paramBox.Text);

            if (newText != null)
            {
                paramBox.Text = newText;
            }
        }

        internal static string Show(Form owner, Core core, AlgoParameters algo, string defaults)
        {
            using (FrmEditParameters frm = new FrmEditParameters(core, algo, defaults))
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

        private FrmEditParameters(Core core, AlgoParameters algo, string defaults)
            : this()
        {
            this._core = core;
            this._algo = algo;

            List<string> elements = StringHelper.SplitGroups(defaults);

            if (algo.Parameters != null)
            {
                for (int index = 0; index < algo.Parameters.Length; index++)
                {
                    var param = algo.Parameters[index];
                    int row = index + 1;

                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0f));

                    Label label = new Label();
                    label.Text = param.Name;
                    label.AutoSize = true;
                    label.Visible = true;
                    label.Margin = new Padding(8, 8, 8, 8);
                    tableLayoutPanel1.Controls.Add(label, 0, row);

                    Label label2 = new Label();
                    label2.Text = param.Type.ToUiString();
                    label2.AutoSize = true;
                    label2.Visible = true;
                    label2.Margin = new Padding(8, 8, 8, 8);
                    tableLayoutPanel1.Controls.Add(label2, 1, row);

                    TextBox textBox = new TextBox();
                    textBox.Dock = DockStyle.Top;
                    textBox.Visible = true;
                    textBox.Margin = new Padding(8, 8, 8, 8);
                    textBox.Text = elements.Count > index ? elements[index] : "";
                    tableLayoutPanel1.Controls.Add(textBox, 2, row);
                    _textBoxes.Add(textBox);

                    CtlButton button = new CtlButton();
                    button.Image = Resources.MnuEdit;
                    button.Visible = true;
                    button.UseDefaultSize = true;
                    button.Margin = new Padding(8, 8, 8, 8);
                    button.Tag = index;
                    button.Click += button_Click;
                    tableLayoutPanel1.Controls.Add(button, 3, row);
                }
            }

            UiControls.CompensateForVisualStyles(this);
        }

        void button_Click(object sender, EventArgs e)
        {
            int index = (int)((Button)sender).Tag;
            TextBox textBox = _textBoxes[index];
            var param = this._algo.Parameters[index];

            object value = AlgoParameters.TryReadParameter(_core, textBox.Text, param.Type);

            switch (param.Type)
            {
                case AlgoParameters.EType.WeakRefConfigurationClusterer:
                    {
                        ConfigurationClusterer def = ((WeakReference<ConfigurationClusterer>)value).GetTarget();
                        var sel = ListValueSet.ForClusterers(_core).Select(def).ShowList(this);

                        if (sel == null)
                        {
                            return;
                        }

                        value = new WeakReference<ConfigurationClusterer>(sel);
                    }
                    break;

                case AlgoParameters.EType.Group:
                    value = ListValueSet.ForGroups(_core).Select((GroupInfo)value).ShowList(this);
                    break;

                case AlgoParameters.EType.WeakRefPeak:
                    {
                        var sel = ListValueSet.ForPeaks(_core).Select(((WeakReference<Peak>)value).GetTarget()).ShowList(this);

                        if (sel == null)
                        {
                            return;
                        }

                        value = new WeakReference<Peak>(sel);
                    }
                    break;

                case AlgoParameters.EType.WeakRefStatisticArray:
                    {
                        var tvalue = (WeakReference<ConfigurationStatistic>[])value;
                        IEnumerable<ConfigurationStatistic> def = tvalue.Select(z => z.GetTarget()).Where(z => z != null);
                        IEnumerable<ConfigurationStatistic> sel = ListValueSet.ForStatistics(_core).Select(def).ShowCheckList(this);

                        if (sel == null)
                        {
                            return;
                        }

                        value = sel.Select(z => new WeakReference<ConfigurationStatistic>(z)).ToArray();
                    }
                    break;

                case AlgoParameters.EType.Integer:
                case AlgoParameters.EType.Double:
                    {
                        FrmMsgBox.ButtonSet[] btns =
                    {
                        new FrmMsgBox.ButtonSet("MAX", Resources.MnuUp, DialogResult.Yes),
                        new FrmMsgBox.ButtonSet("MIN", Resources.MnuDown, DialogResult.No),
                        new FrmMsgBox.ButtonSet("Cancel", Resources.MnuCancel, DialogResult.Cancel)
                    };

                        bool isInt = param.Type == AlgoParameters.EType.Integer;

                        switch (FrmMsgBox.Show(this, "Select Integer", null, "Select a value or enter a custom value into the textbox", Resources.MsgHelp, btns, 2, 2))
                        {
                            case DialogResult.Yes:
                                value = isInt ? (object)int.MaxValue : (object)double.MaxValue;
                                break;

                            case DialogResult.No:
                                value = isInt ? (object)int.MinValue : (object)double.MinValue;
                                break;

                            default:
                                return;
                        }
                    }
                    break;

                default:
                    {
                        FrmMsgBox.ShowInfo(this, param.Type.ToUiString(), "There is no editor associated with this type.\r\nPlease enter a valid value directly into the textbox.");
                    }
                    return;
            }

            if (value == null)
            {
                return;
            }

            textBox.Text = AlgoParameters.ParamsToReversableString(new[] { value }, _core);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            object[] results = new object[_algo.Parameters.Length];

            for (int index = 0; index < _algo.Parameters.Length; index++)
            {
                var param = _algo.Parameters[index];

                results[index] = AlgoParameters.TryReadParameter(_core, _textBoxes[index].Text, param.Type);

                if (results[index] == null)
                {
                    FrmMsgBox.ShowError(this, "Error", "Enter a valid value for " + param.Name);
                    return;
                }
            }

            _result = AlgoParameters.ParamsToReversableString(results, _core);
            DialogResult = DialogResult.OK;
        }
    }
}
