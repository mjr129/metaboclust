using MetaboliteLevels.Data.DataInfo;
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

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmEditExpGroup : Form
    {
        private readonly GroupInfo _group;
        private Color _colour;

        internal static bool Show(Form owner, GroupInfo group, bool readOnly)
        {
            using (FrmEditExpGroup frm = new FrmEditExpGroup(group, readOnly))
            {
                return UiControls.ShowWithDim(owner, frm) == DialogResult.OK;
            }
        }

        private FrmEditExpGroup()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmEditExpGroup(GroupInfo group, bool readOnly)
            : this()
        {
            this._group = group;

            this._txtTitle.Text = group.OverrideDisplayName;
            this._txtTitle.Watermark = group.DefaultDisplayName;
            this._txtAbvTitle.Text = group.OverrideShortName;
            this._txtAbvTitle.Watermark = group.DefaultShortName;
            this._txtComments.Text = group.Comment;
            this._txtId.Text = group.Id.ToString();
            this._txtTimeRange.Text = group.Range.ToString();
            _colour = group.Colour;

            UpdateButtonImage();

            if (readOnly)
            {
                ctlTitleBar1.Text = "View group";
                ctlTitleBar1.SubText = "View the details for the experimental group";
                UiControls.MakeReadOnly(this);
            }
            else
            {
                ctlTitleBar1.Text = "Edit Group";
                ctlTitleBar1.SubText = "Enter the new details for the experimental group";
            }

            UiControls.CompensateForVisualStyles(this);
        }

        private void UpdateButtonImage()
        {
            this._btnColour.Image = UiControls.CreateSolidColourImage(this._txtAbvTitle.Text, GroupInfoBase.GetLightVersionOfColour(_colour), _colour);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = _colour;

                if (UiControls.ShowWithDim(this, cd) == System.Windows.Forms.DialogResult.OK)
                {
                    _colour = cd.Color;
                    UpdateButtonImage();
                }
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
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
