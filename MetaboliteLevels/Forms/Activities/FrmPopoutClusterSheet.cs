using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls.Charts;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Activities
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
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmPopoutClusterSheet(Size size, List<Visualisable> items, IPreviewProvider previewProvider)
            : this()
        {
            this._imageSize = size;
            this._items = items;
            this._previewProvider = previewProvider;  

            imageList1.ImageSize = this._imageSize;
            listView1.TileSize = this._imageSize;
            listView1.VirtualListSize = items.Count;

            // UiControls.CompensateForVisualStyles(this);
        }

        private int GeneratePreviewImage(Visualisable item)
        {
            int index;

            if (!_imgListPreviewIndexes.TryGetValue(item, out index))
            {
                Image img = _previewProvider.ProvidePreview(_imageSize, item) ??
                            ChartHelperForClusters.CreatePlaceholderBitmap(item, _imageSize);

                index = imageList1.Images.Count;

                imageList1.Images.Add(item.DisplayName, img);
                _imgListPreviewIndexes.Add(item, index);
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
            Visualisable item = _items[e.ItemIndex];
            e.Item = new ListViewItem(item.DisplayName);
            e.Item.ImageIndex = GeneratePreviewImage(item);
        }

        private void listView1_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 0)
            {
                int index = listView1.SelectedIndices[0];
                _result = index;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
