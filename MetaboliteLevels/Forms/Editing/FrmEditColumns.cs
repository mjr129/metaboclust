using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Editing
{
    internal partial class FrmEditColumns : Form
    {
        private readonly List<Column> _available;
        private readonly HashSet<Column> _selected;

        public static HashSet<Column> Show( IWin32Window owner, IEnumerable<Column> available, IEnumerable<Column> selected )
        {
            using (FrmEditColumns frm = new FrmEditColumns( available, selected ))
            {
                if (frm.ShowDialog( owner ) == DialogResult.OK)
                {
                    return frm._selected;
                }

                return null;
            }
        }

        internal FrmEditColumns(IEnumerable<Column> available, IEnumerable<Column> selected )
        {
            InitializeComponent();
            UiControls.SetIcon( this );

            this._available = available.ToList();
            this._selected = selected.Unique();

            RefreshView();
        }

        private void RefreshView(  )
        {
            treeView1.Nodes.Clear();

            foreach (Column col in _available)
            {
                bool isSelected = _selected.Contains( col );

                if (!isSelected && !_chkShowAdvanced.Checked && col.Special.Has( EColumn.Advanced ))
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
                        newNode.NodeFont = FontHelper.ItalicFont;
                        newNode.ForeColor = Color.Orange;
                        parent.Add( newNode );
                        parent = newNode.Nodes;
                    }
                }

                TreeNode node = new TreeNode( elements[elements.Length - 1] );
                node.Tag = col;
                node.Checked = isSelected;
                node.ForeColor = ColumnManager.GetColumnColour( col );

                parent.Add( node );
            }

            treeView1.ExpandAll();
        }

        private void _btnDefaults_Click( object sender, EventArgs e )
        {
            _selected.Clear();

            foreach (Column col in _available)
            {
                if (col.Special == EColumn.Visible)
                {
                    _selected.Add( col );
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
                    _selected.Add( col );
                }
                else
                {
                    _selected.Remove( col );
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
