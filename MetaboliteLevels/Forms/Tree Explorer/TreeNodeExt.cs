using System.Windows.Forms;

namespace MetaboliteLevels.Forms.Tree_Explorer
{
    static class TreeNodeExt
    {
        public static TreeNode AddSubNode(this TreeNodeCollection x, string title, string image = "INFO", object tag = null)
        {
            TreeNode y = CreateNode(title, image, tag);
            x.Add(y);
            return y;
        }

        public static TreeNode AddSubNode(this TreeNode x, string title, string image = "INFO", object tag = null)
        {
            TreeNode y = CreateNode(title, image, tag);
            x.Nodes.Add(y);
            return y;
        }

        public static TreeNode CreateNode(string title, string image, object tag = null)
        {
            TreeNode y = new TreeNode(title);
            y.Tag = tag;
            y.ImageKey = image;
            y.SelectedImageKey = image;
            return y;
        }
    }
}
