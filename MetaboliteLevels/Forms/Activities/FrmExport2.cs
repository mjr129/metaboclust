using System;
using System.Collections.Generic;
using System.ComponentModel;    
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Text;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Activities
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
            InitializeComponent();
            UiControls.SetIcon( this );

            _cbDatasets = DataSet.ForDatasetProviders( core ).CreateComboBox( comboBox1, ctlButton2, ENullItemName.NoNullItem );

            _txtDir.Watermark = UiControls.GetOrCreateFixedFolder( UiControls.EInitialFolder.ExportedData );
        }

        private void comboBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            IDataSet item = _cbDatasets.SelectedItem;

            if (item != null)
            {
                DataSet<IExportProvider> ds = new DataSet<IExportProvider>()
                {
                    ListSource = item.UntypedGetList(true).Cast<object>().Where( z => z is IExportProvider ).Cast< IExportProvider>()
                };

                if (_cbItems != null)
                {
                    _cbItems.Dispose();
                }

                _cbItems = ds.CreateComboBox( comboBox2, ctlButton3, ENullItemName.RepresentingAll );
                _cbItems.ClearSelection();
            }
        }

        private void _btnBrowseDir_Click( object sender, EventArgs e )
        {
            FileHelper.BrowseForFolder( _txtDir );
        }

        private void comboBox2_SelectedIndexChanged( object sender, EventArgs e )
        {
            if (!_cbItems.HasSelection)
            {
                return;
            }

            object sel = GetSelectedItem();

            _txtFile.Watermark = sel.ToString();
        }

        private IExportProvider GetSelectedItem()
        {
            if (_cbItems.HasSelection)
            {
                if (_cbItems.SelectedItem == null)
                {
                    return _cbDatasets.SelectedItem;
                }
                else
                {
                    return _cbItems.SelectedItem;
                }
            }    

            return null;
        }

        private void ctlButton1_Click( object sender, EventArgs e )
        {
            string dir = string.IsNullOrWhiteSpace( _txtDir.Text ) ? _txtDir.Watermark : _txtDir.Text;
            string file = string.IsNullOrWhiteSpace( _txtFile.Text ) ? _txtFile.Watermark : _txtFile.Text;

            var item = GetSelectedItem();

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

            listBox1.Items.Add( q );
            SetOkButtonStatus();
        }

        private void SetOkButtonStatus()
        {
            _btnOk.Enabled = listBox1.Items.Count != 0;
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
                return Target.ToString() + " --> " + FileName;
            }
        }

        private void _btnOk_Click( object sender, EventArgs e )
        {
            List<string> results = new List<string>();
            SpreadsheetReader ssr = new SpreadsheetReader();

            foreach (QueuedExport q in listBox1.Items)
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

            FrmInputMultiLine.ShowFixed( this, Text, "Export", null, string.Join( "\r\n\r\n", results ) );
        }

        private void ctlButton6_Click( object sender, EventArgs e )
        {
            ListBoxHelper.RemoveSelectedItem( listBox1 );
            SetOkButtonStatus();
        }
    }
}
