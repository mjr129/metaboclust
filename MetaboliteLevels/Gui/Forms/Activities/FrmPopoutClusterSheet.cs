using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Gui.Controls.Charts;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    public partial class FrmPopoutClusterSheet : Form
    {
        private int _result;
        private Size _imageSize;
        private readonly List<Visualisable> _items;
        private readonly IPreviewProvider _previewProvider;
        private readonly Dictionary<object, int> _imgListPreviewIndexes = new Dictionary<object, int>();

        private FrmPopoutClusterSheet()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmPopoutClusterSheet(Size size, List<Visualisable> items, IPreviewProvider previewProvider)
            : this()
        {
            this._imageSize = size;
            this._items = items;
            this._previewProvider = previewProvider;  

            this.imageList1.ImageSize = this._imageSize;
            this.listView1.TileSize = this._imageSize;
            this.listView1.VirtualListSize = items.Count;

            // UiControls.CompensateForVisualStyles(this);
        }

        private int GeneratePreviewImage(Visualisable item)
        {
            int index;

            if (!this._imgListPreviewIndexes.TryGetValue(item, out index))
            {
                Image img = this._previewProvider.ProvidePreview(this._imageSize, item) ??
                            ChartHelperForClusters.CreatePlaceholderBitmap(item, this._imageSize);

                index = this.imageList1.Images.Count;

                this.imageList1.Images.Add(item.DisplayName, img);
                this._imgListPreviewIndexes.Add(item, index);
            }

            return index;
        }

        internal static int Show(Form owner, Size size, IEnumerable items, IPreviewProvider previewProvider)
        {
            FrmPopoutClusterSheet frm = new FrmPopoutClusterSheet(size, new List<Visualisable>(items.Cast<Visualisable>()), previewProvider);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = owner.Location;
            frm.Size = new Size(owner.Width, owner.Height);

            if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
            {
                return frm._result;
            }

            return -1;
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            Visualisable item = this._items[e.ItemIndex];
            e.Item = new ListViewItem(item.DisplayName);
            e.Item.ImageIndex = this.GeneratePreviewImage(item);
        }

        private void listView1_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count != 0)
            {
                int index = this.listView1.SelectedIndices[0];
                this._result = index;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
