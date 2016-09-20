using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Editing
{
    internal partial class FrmEditColumns : Form
    {
        private readonly List<Column> Available;
        private readonly HashSet<Column> Selected;

        public static HashSet<Column> Show( IWin32Window owner, IEnumerable<Column> available, IEnumerable<Column> selected )
        {
            using (FrmEditColumns frm = new FrmEditColumns( available, selected ))
            {
                if (frm.ShowDialog( owner ) == DialogResult.OK)
                {
                    return frm.Selected;
                }

                return null;
            }
        }

        internal FrmEditColumns(IEnumerable<Column> available, IEnumerable<Column> selected )
        {
            InitializeComponent();
            UiControls.SetIcon( this );

            this.Available = available.ToList();
            this.Selected = selected.Unique();

            RefreshView();

            treeView1.ExpandAll();
        }

        private void RefreshView(  )
        {
            treeView1.Nodes.Clear();

            foreach (Column col in Available)
            {
                bool isSelected = Selected.Contains( col );

                if (!isSelected && !_chkShowAdvanced.Checked && col.Special == EColumn.Advanced)
                {
                    continue;
                }

                string[] elements = col.Id.Split( '\\' );

                TreeNodeCollection parent = treeView1.Nodes;

                for (int n = 0; n < elements.Length - 1; n++)
                {
                    bool found = false;

                    foreach (TreeNode child in parent)
                    {
                        if (child.Text == elements[n])
                        {
                            parent = child.Nodes;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        TreeNode newNode = new TreeNode( elements[n] );
                        parent.Add( newNode );
                        parent = newNode.Nodes;
                    }
                }

                TreeNode node = new TreeNode( elements[elements.Length - 1] );
                node.Tag = col;
                node.Checked = isSelected;

                if (col.Special.Has( EColumn.Visible ))
                {
                    node.NodeFont = FontHelper.BoldFont;
                }
                else if (col.Special.Has( EColumn.Advanced ))
                {
                    node.ForeColor = Color.Olive;
                }

                parent.Add( node );
            }
        }

        private void _btnDefaults_Click( object sender, EventArgs e )
        {
            Selected.Clear();

            foreach (Column col in Available)
            {
                if (col.Special == EColumn.Visible)
                {
                    Selected.Add( col );
                }
            }

            RefreshView();
        }

        public List<TreeNode> AllNodes()
        {
            List<TreeNode> all = new List<TreeNode>();
            AllNodes( all, treeView1.Nodes );
            return all;
        }

        public void AllNodes( List<TreeNode> all, TreeNodeCollection col )
        {
            foreach (TreeNode node in col)
            {
                all.Add( node );
                AllNodes( all, node.Nodes );
            }
        }

        private void _chkShowAdvanced_CheckedChanged( object sender, EventArgs e )
        {
            RefreshView();
        }

        bool _ignoreCheck = false;

        private void treeView1_AfterCheck( object sender, TreeViewEventArgs e )
        {
            if (_ignoreCheck)
            {
                return;
            }

            Column col = e.Node.Tag as Column;

            if (col != null)
            {
                if (e.Node.Checked)
                {
                    Selected.Add( col );
                }
                else
                {
                    Selected.Remove( col );
                }

                UpdateParent( e.Node.Parent );
            }
            else
            {
                bool newState = e.Node.Checked;

                foreach (TreeNode n in e.Node.Nodes)
                {
                    n.Checked = newState;
                }
            }
        }

        private void UpdateParent( TreeNode parent )
        {
            if (parent == null)
            {
                return;
            }

            bool allChecked = true;

            foreach (TreeNode child in parent.Nodes)
            {
                if (!child.Checked)
                {
                    allChecked = false;
                    break;
                }
            }

            _ignoreCheck = true;
            parent.Checked = allChecked;
            _ignoreCheck = false;
        }
    }
}
