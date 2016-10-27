using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Editing
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
            this.InitializeComponent();
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
            this._txtId.Text = group.Id.ToString();
            this._txtDisplayOrder.Value = group.DisplayPriority;
            this._txtTimeRange.Text = group.Range.ToString();

            this._ecbIcon= EnumComboBox.Create( this._lstIcon, group.GraphIcon );
            this._ecbFill = EnumComboBox.Create( this._lstStyle, group.HatchStyle );

            this._colour = group.Colour;

            this.UpdateButtonImage();

            bool exp = group is GroupInfo;
            string txtGroup = exp ? "group" : "batch";
            string txtGroupLong = exp ? "experimental group" : "batch";
            string txtContext = readOnly ? "View" : "Edit";
            this._lblTimeRange.Text = exp ? "Time range" : "Acquisition range";
            this.ctlTitleBar1.Text = $"{txtContext} {txtGroup}";
            this.ctlTitleBar1.SubText = $"{txtContext} the details for the {txtGroupLong}";

            if (readOnly)
            {
                UiControls.MakeReadOnly( this );
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void UpdateButtonImage()
        {
            this._btnColour.Image = UiControls.CreateSolidColourImage(this._txtAbvTitle.Text, GroupInfoBase.GetLightVersionOfColour(this._colour), this._colour);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ColourHelper.EditColor(ref this._colour ))
            {
                this.UpdateButtonImage();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.UpdateButtonImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._group.OverrideDisplayName = this._txtTitle.Text;
            this._group.OverrideShortName = this._txtAbvTitle.Text;
            this._group.Comment = this._txtComments.Text;
            this._group.SetColour(this._colour);
            this._group.Id = this._txtId.Text;
            this._group.GraphIcon = this._ecbIcon.SelectedItemOrDefault;
            this._group.HatchStyle = this._ecbFill.SelectedItemOrDefault;
            this._group.DisplayPriority = (int)this._txtDisplayOrder.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void _btnEditId_Click( object sender, EventArgs e )
        {
            FrmMsgBox.ShowWarning( this, "Edit ID", "The ID represents the identifier used when the data was first loaded. It must be unique. Changing the ID may have unintended consequences.", FrmMsgBox.EDontShowAgainId.ChangeExperimentalGroupsId );

            string newId = FrmInputSingleLine.Show( this, this.Text, "Edit ID", this._group.DisplayName, this._txtId.Text );

            if (newId != null)
            {
                this._txtId.Text = newId;
            }
        }
    }
}
