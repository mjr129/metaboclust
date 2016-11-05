using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    internal partial class FrmExport2 : Form
    {
        private readonly EditableComboBox<IDataSet> _cbDatasets;
        private EditableComboBox<IExportProvider> _cbItems;

        public static void Show( IWin32Window owner, Core core )
        {
            using (FrmExport2 frm = new FrmExport2( core ))
            {
                UiControls.ShowWithDim( owner, frm );
            }
        }

        public FrmExport2( Core core )
        {
            this.InitializeComponent();
            UiControls.SetIcon( this );

            this._cbDatasets = DataSet.ForDatasetProviders( core ).CreateComboBox( this.comboBox1, this.ctlButton2, EditableComboBox.EFlags.None );

            this._txtDir.Watermark = UiControls.GetOrCreateFixedFolder( UiControls.EInitialFolder.ExportedData );
        }

        private void comboBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            IDataSet item = this._cbDatasets.SelectedItem;

            if (item != null)
            {
                DataSet<IExportProvider> ds = new DataSet<IExportProvider>()
                {
                    ListSource = item.UntypedGetList(true).Cast<object>().Where( z => z is IExportProvider ).Cast< IExportProvider>()
                };

                if (this._cbItems != null)
                {
                    this._cbItems.Dispose();
                }

                this._cbItems = ds.CreateComboBox( this.comboBox2, this.ctlButton3, EditableComboBox.EFlags.IncludeAll );
                this._cbItems.ClearSelection();
            }
        }

        private void _btnBrowseDir_Click( object sender, EventArgs e )
        {
            FileHelper.BrowseForFolder( this._txtDir );
        }

        private void comboBox2_SelectedIndexChanged( object sender, EventArgs e )
        {
            if (!this._cbItems.HasSelection)
            {
                return;
            }

            object sel = this.GetSelectedItem();

            this._txtFile.Watermark = sel.ToString();
        }

        private IExportProvider GetSelectedItem()
        {
            if (this._cbItems.HasSelection)
            {
                if (this._cbItems.SelectedItem == null)
                {
                    return this._cbDatasets.SelectedItem;
                }
                else
                {
                    return this._cbItems.SelectedItem;
                }
            }    

            return null;
        }

        private void ctlButton1_Click( object sender, EventArgs e )
        {
            string dir = string.IsNullOrWhiteSpace( this._txtDir.Text ) ? this._txtDir.Watermark : this._txtDir.Text;
            string file = string.IsNullOrWhiteSpace( this._txtFile.Text ) ? this._txtFile.Watermark : this._txtFile.Text;

            var item = this.GetSelectedItem();

            if (item == null)
            {
                return;
            }

            file = FileHelper.SanitiseFilename( file );

            if (string.IsNullOrWhiteSpace( Path.GetExtension( file ) ))
            {
                file += ".csv";
            }

            QueuedExport q = new QueuedExport( item, Path.Combine( dir, file ) );

            this.listBox1.Items.Add( q );
            this.SetOkButtonStatus();
        }

        private void SetOkButtonStatus()
        {
            this._btnOk.Enabled = this.listBox1.Items.Count != 0;
        }

        class QueuedExport
        {
            public readonly IExportProvider Target;
            public readonly string FileName;

            public QueuedExport( IExportProvider target, string fileName )
            {
                this.Target = target;
                this.FileName = fileName;
            }

            public override string ToString()
            {
                return this.Target.ToString() + " --> " + this.FileName;
            }
        }

        private void _btnOk_Click( object sender, EventArgs e )
        {
            List<string> results = new List<string>();
            SpreadsheetReader ssr = new SpreadsheetReader();

            foreach (QueuedExport q in this.listBox1.Items)
            {
                ISpreadsheet ss = q.Target.ExportData();

                if (ss == null)
                {
                    results.Add( "NO-DATA: " + q.ToString() );
                }
                else
                {
                    try
                    {
                        ssr.Write( ss, q.FileName );
                        results.Add( "SUCCESS: " + q.ToString() );
                    }
                    catch
                    {
                        results.Add( "ERROR: " + q.ToString() );
                    }                      
                }
            }

            FrmInputMultiLine.ShowFixed( this, this.Text, "Export", null, string.Join( "\r\n\r\n", results ) );
        }

        private void ctlButton6_Click( object sender, EventArgs e )
        {
            ListBoxHelper.RemoveSelectedItem( this.listBox1 );
            this.SetOkButtonStatus();
        }
    }
}
