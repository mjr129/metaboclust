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

        internal static void Show(Form owner, GroupInfo group)
        {
            using (FrmEditExpGroup frm = new FrmEditExpGroup(group))
            {
                UiControls.ShowWithDim(owner, frm);
            }
        }

        private FrmEditExpGroup()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmEditExpGroup(GroupInfo group)
            : this()
        {
            this._group = group;

            ctlTitleBar1.Text = "Edit Group";
            ctlTitleBar1.SubText = "Enter the new details for the experimental group";

            this._txtTitle.Text = group.Name;
            this._txtAbvTitle.Text = group.ShortName;
            this._txtComments.Text = group.Comment;
            this._txtId.Text = group.Id.ToString();
            this._txtTimeRange.Text = group.Range.ToString();
            _colour = group.Colour;

            UpdateButtonImage();

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
            _group.Name = this._txtTitle.Text;
            _group.ShortName = this._txtAbvTitle.Text;
            _group.Comment = this._txtComments.Text;
            _group.SetColour(_colour);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
