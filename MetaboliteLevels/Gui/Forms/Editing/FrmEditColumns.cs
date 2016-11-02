using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    internal partial class FrmEditColumns : Form
    {
        private readonly List<Column> _available;
        private readonly HashSet<Column> _selected;
        private readonly HashSet<object> _view = new HashSet<object>();

        public static Column Show( IWin32Window owner, IEnumerable<Column> available, Column selected, string title )
        {
            using (FrmEditColumns frm = new FrmEditColumns( available, new[] { selected }, false,title ))
            {
                if (frm.ShowDialog( owner ) == DialogResult.OK)
                {
                    return frm._selected.FirstOrDefault();
                }

                return null;
            }
        }

        public static HashSet<Column> Show( IWin32Window owner, IEnumerable<Column> available, IEnumerable<Column> selected, string title )
        {
            using (FrmEditColumns frm = new FrmEditColumns( available, selected, true,title ))
            {
                if (frm.ShowDialog( owner ) == DialogResult.OK)
                {
                    return frm._selected;
                }

                return null;
            }
        }

        internal FrmEditColumns(IEnumerable<Column> available, IEnumerable<Column> selected, bool multiSelect, string title )
        {
            this.InitializeComponent();
            UiControls.SetIcon( this );

            this._available = available.ToList();
            this._selected = selected.Unique();
            this.ctlTitleBar1.Text = title; 

            this._chkAdvanced.ForeColor = ColumnManager.COLCOL_ADVANCED;
            this._chkMetaFields.ForeColor = ColumnManager.COLCOL_META;
            this._chkNormal.ForeColor = ColumnManager.COLCOL_NORMAL;
            this._chkProperties.ForeColor = ColumnManager.COLCOL_PROPERTY;
            this._chkStatistics.ForeColor = ColumnManager.COLCOL_STATISTIC;
            this._chkDefault.ForeColor = ColumnManager.COLCOL_VISIBLE;
            this._chkFolders.ForeColor = ColumnManager.COLCOL_FOLDER;

            this._multiSelect = multiSelect;              

            _view.AddRange( new[] { _chkMetaFields , _chkNormal , _chkStatistics, _chkDefault, _chkFolders } );

            ctlContextHelp1.Bind( this, ctlTitleBar1, null, CtlContextHelp.EFlags.None );

            this.RefreshView();
        }

        private void RefreshView(  )
        {
            this.treeView1.Nodes.Clear();

            bool hideAdva = IsHidden( _chkAdvanced );
            bool hideMeta = IsHidden( _chkMetaFields );
            bool hideProp = IsHidden( _chkProperties );
            bool hideStat = IsHidden( _chkStatistics );
            bool hideDefa = IsHidden( _chkDefault );
            bool hideNorm = IsHidden( _chkNormal );
            bool hideFold = IsHidden( _chkFolders );

            foreach (Column col in this._available)
            {
                bool isSelected = this._selected.Contains( col );

                if (!isSelected)
                {   
                    if (col.Special.Has( EColumn.IsMeta ))
                    {
                        if (hideMeta)
                            continue;
                    }
                    else if (col.Special.Has( EColumn.IsProperty ))
                    {
                        if (hideProp)
                            continue;
                    }
                    else if (col.Special.Has( EColumn.IsStatistic ))
                    {
                        if (hideStat)
                            continue;
                    }
                    else if (col.Special.Has( EColumn.Visible ))
                    {
                        if (hideDefa)
                            continue;
                    }
                    else if (col.Special.Has( EColumn.Advanced ))
                    {
                        if (hideAdva)
                            continue;
                    }
                    else if (hideNorm)
                    {
                        continue;
                    }              
                }
                                
                TreeNodeCollection parent = this.treeView1.Nodes;
                string text;

                if (hideFold)
                {
                    string dir = UiControls.GetDirectory( col.Id );

                    if (!string.IsNullOrEmpty( dir ))
                    {
                        dir += "\\";
                    }

                    text = dir + col.DisplayName;
                }
                else
                {   
                    string[] elements = col.Id.Split( '\\' );

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
                            newNode.ForeColor = ColumnManager.COLCOL_FOLDER;
                            parent.Add( newNode );
                            parent = newNode.Nodes;
                        }
                    }

                    text = col.DisplayName;
                }

                TreeNode node = new TreeNode( text );
                node.Tag = col;
                node.Checked = isSelected;
                node.ForeColor = ColumnManager.GetColumnColour( col );  

                parent.Add( node );
            }

            this.treeView1.ExpandAll();
        }

        private bool IsHidden( ToolStripButton button )
        {
            bool isChecked = _view.Contains( button );
            button.Image = UiControls.RecolourImage( isChecked ? Resources.MnuChecked : Resources.MnuUnchecked, button.ForeColor );
            return !isChecked;
        }

        private void _btnDefaults_Click( object sender, EventArgs e )
        {
            this._selected.Clear();

            foreach (Column col in this._available)
            {
                if (col.Special == EColumn.Visible)
                {
                    this._selected.Add( col );
                }
            }

            this.RefreshView();
        }

        public List<TreeNode> AllNodes()
        {
            List<TreeNode> all = new List<TreeNode>();
            this.AllNodes( all, this.treeView1.Nodes );
            return all;
        }

        public void AllNodes( List<TreeNode> all, TreeNodeCollection col )
        {
            foreach (TreeNode node in col)
            {
                all.Add( node );
                this.AllNodes( all, node.Nodes );
            }
        }   

        bool _ignoreCheck = false;
        private readonly bool _multiSelect;

        private void treeView1_AfterCheck( object sender, TreeViewEventArgs e )
        {
            if (this._ignoreCheck)
            {
                return;
            }
                   
            Column col = e.Node.Tag as Column;

            if (!_multiSelect)
            {
                // No multiselect - deselect all other nodes
                if (col == null)
                {
                    // Can't select group in single-select
                    this._ignoreCheck = true;
                    e.Node.Checked = false;
                    this._ignoreCheck = false;
                    return;
                }

                this._ignoreCheck = true;

                foreach (TreeNode node in treeView1.GetAllNodes())
                {
                    node.Checked = node == e.Node;
                }

                this._ignoreCheck = false;

                _selected.Clear();
                _selected.Add( col );

                this.UpdateParent( e.Node.Parent );
            }
            else
            {
                if (col == null)
                {
                    // Group selected - toggle all descendents
                    bool newState = e.Node.Checked;

                    foreach (TreeNode n in e.Node.Nodes)
                    {
                        n.Checked = newState;
                    }
                }
                else
                {
                    if (e.Node.Checked)
                    {
                        this._selected.Add( col );
                    }
                    else
                    {
                        this._selected.Remove( col );
                    }

                    this.UpdateParent( e.Node.Parent );
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

            this._ignoreCheck = true;
            parent.Checked = allChecked;
            this._ignoreCheck = false;
        }        

        private void treeView1_AfterSelect( object sender, TreeViewEventArgs e )
        {
            Column col = (Column)e.Node.Tag;

            if (col == null)
            {
                ctlContextHelp1.ShowHelp( "Select a column to display more information about it." );
                return;
            }

            StringBuilder sb = new StringBuilder();

            string fName = col.DefaultDisplayName;

            if (string.IsNullOrEmpty( col.Comment ))
            {
                if (col.Special.Has( EColumn.IsMeta ))
                {
                    sb.AppendLine( $"This field shows the meta-data {{{fName}}} provided in the information matrix when the file was loaded." );
                }
                else if (col.Special.Has( EColumn.IsStatistic ))
                {
                    sb.AppendLine( $"This column shows the value of the statistic {{{fName}}}." );
                }
                else if (col.Special.Has( EColumn.IsProperty ))
                {
                    sb.AppendLine( $"This column shows an internal value {{{fName}}} reflected by the software." );
                }                                                                                                 
                else
                {
                    sb.AppendLine( $"This column shows the {{{fName}}}." );
                }
            }
            else
            {                                    
                sb.AppendLine( col.Comment );
            }

            if (_view.Contains( _chkAdvanced ))
            {
                sb.AppendLine();
                sb.AppendLine( "ID = " + col.Id );
                sb.AppendLine( "Date type = " + col.FunctionDescription );
                sb.AppendLine( "Flags = " + string.Join( ", ", EnumHelper.SplitEnum( col.Special ).Select( z => z.ToUiString() ) ) );
            }

            ctlContextHelp1.ShowHelp( sb.ToString() );
        }

        private void _chkFolders_Click( object sender, EventArgs e )
        {
            _view.Toggle( sender );
            this.RefreshView();
        }       
    }
}
