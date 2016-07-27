using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MGui;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Types.UI;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmEditGroupBase : Form
    {
        private readonly GroupInfoBase _group;
        private Color _colour;
        private readonly EnumComboBox<EGraphIcon> _ecbIcon;
        private readonly EnumComboBox<EHatchStyle> _ecbFill;

        internal static bool Show(Form owner, GroupInfoBase group, bool readOnly)
        {
            using (FrmEditGroupBase frm = new FrmEditGroupBase(group, readOnly))
            {
                return UiControls.ShowWithDim(owner, frm) == DialogResult.OK;
            }
        }     

        private FrmEditGroupBase()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmEditGroupBase( GroupInfoBase group, bool readOnly)
            : this()
        {
            this._group = group;    

            this._txtTitle.Text = group.OverrideDisplayName;
            this._txtTitle.Watermark = group.DefaultDisplayName;
            this._txtAbvTitle.Text = group.OverrideShortName;
            this._txtAbvTitle.Watermark = group.DefaultShortName;
            this._txtComments.Text = group.Comment;
            this._txtId.Text = group.StringId.ToString();
            this._txtDisplayOrder.Value = group.DisplayPriority;
            this._txtTimeRange.Text = group.Range.ToString();

            _ecbIcon= EnumComboBox.Create( _lstIcon, group.GraphIcon );
            _ecbFill = EnumComboBox.Create( _lstStyle, group.HatchStyle );

            _colour = group.Colour;

            UpdateButtonImage();

            bool exp = group is GroupInfo;
            string txtGroup = exp ? "group" : "batch";
            string txtGroupLong = exp ? "experimental group" : "batch";
            string txtContext = readOnly ? "View" : "Edit";
            _lblTimeRange.Text = exp ? "Time range" : "Acquisition range";
            ctlTitleBar1.Text = $"{txtContext} {txtGroup}";
            ctlTitleBar1.SubText = $"{txtContext} the details for the {txtGroupLong}";

            if (readOnly)
            {
                UiControls.MakeReadOnly( this );
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void UpdateButtonImage()
        {
            this._btnColour.Image = UiControls.CreateSolidColourImage(this._txtAbvTitle.Text, GroupInfoBase.GetLightVersionOfColour(_colour), _colour);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ColourHelper.EditColor(ref _colour ))
            {
                UpdateButtonImage();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _group.OverrideDisplayName = this._txtTitle.Text;
            _group.OverrideShortName = this._txtAbvTitle.Text;
            _group.Comment = this._txtComments.Text;
            _group.SetColour(_colour);
            _group.StringId = _txtId.Text;
            _group.GraphIcon = _ecbIcon.SelectedItemOrDefault;
            _group.HatchStyle = _ecbFill.SelectedItemOrDefault;
            _group.DisplayPriority = (int)_txtDisplayOrder.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void _btnEditId_Click( object sender, EventArgs e )
        {
            FrmMsgBox.ShowWarning( this, "Edit ID", "The ID represents the identifier used when the data was first loaded. It must be unique. Changing the ID may have unintended consequences.", FrmMsgBox.EDontShowAgainId.CHANGE_EXPERIMENTAL_GROUP_ID );

            string newId = FrmInputSingleLine.Show( this, Text, "Edit ID", _group.DisplayName, _txtId.Text );

            if (newId != null)
            {
                _txtId.Text = newId;
            }
        }
    }
}
