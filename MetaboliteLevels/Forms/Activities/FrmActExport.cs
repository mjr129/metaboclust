using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Forms.Text;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Activities
{
    internal partial class FrmActExport : Form
    {
        List<OtherExportInfo> _otherExports = new List<OtherExportInfo>();
        private Core _core;
        UniqueTable _uniqueTable = new UniqueTable();
        private EditableComboBox<IMatrixProvider> _ecbIntensitySource;
        private EditableComboBox<IMatrixProvider> _ecbTrendSource;

        public static void Show( Form owner, Core core )
        {
            using (FrmActExport frm = new FrmActExport( core ))
            {
                UiControls.ShowWithDim( owner, frm );
            }
        }

        private FrmActExport(Core core)
        {
            InitializeComponent();
            UiControls.SetIcon( this );

            _ecbIntensitySource = DataSet.ForMatrixProviders( core ).CreateComboBox( _lstIntensitySource, _btnIntensitySource, ENullItemName.NoNullItem );
            _ecbTrendSource = DataSet.ForMatrixProviders( core ).CreateComboBox( _lstIntensitySource, _btnIntensitySource, ENullItemName.NoNullItem );

            _core = core;
        }

        public FrmActExport() : base()
        {
        }

        private void _chkData_CheckedChanged( object sender, EventArgs e )
        {
            Set( _txtData, _btnData, _chkData, _lstIntensitySource, _btnIntensitySource );
        }

        private void _chkObs_CheckedChanged( object sender, EventArgs e )
        {
            Set( _txtObservations , _btnObs , _chkObs);
        }

        private void _chkPeaks_CheckedChanged( object sender, EventArgs e )
        {
            Set( _txtPeaks , _btnPeaks , _chkPeaks);
        }

        private void _chkClusters_CheckedChanged( object sender, EventArgs e )
        {
            Set( _txtClusters , _btnClusters , _chkClusters);
        }

        private void _chkTrend_CheckedChanged( object sender, EventArgs e )
        {
            Set( _txtTrend , _btnTrend , _chkTrend, _lstTrendSource, _btnTrendSource);
        }

        private void _chkConds_CheckedChanged( object sender, EventArgs e )
        {
            Set( _txtConds , _btnConds , _chkConds);
        }

        private void _chkOther_CheckedChanged( object sender, EventArgs e )
        {
            Set( _txtOther , _btnOther , _chkOther);
        }

        private void _btnData_Click( object sender, EventArgs e )
        {
            Browse( _txtData );
        }

        private void Browse( TextBox textBox )
        {
            string fileName = UiControls.BrowseForFile( this, textBox.Text, UiControls.EFileExtension.Csv, FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData );

            if (fileName != null)
            {
                textBox.Text = fileName;
            }
        }

        private void Set( TextBox textBox, CtlButton button, CheckBox checkBox, ComboBox comboBox, CtlButton comboButton )
        {
            Set( textBox, button, checkBox );

            if (comboBox != null)
            {
                comboButton.Enabled = comboBox.Enabled = textBox.Enabled;
            }
        }

        private void Set( TextBox textBox, CtlButton button, CheckBox checkBox )
        {
            if (textBox.Enabled = button.Enabled = checkBox.Checked)
            {
                if (textBox.TextLength == 0)
                {
                    button.PerformClick();
                }
            }
        }

        private void _btnObs_Click( object sender, EventArgs e )
        {
            Browse( _txtObservations );
        }

        private void _btnPeaks_Click( object sender, EventArgs e )
        {
            Browse( _txtPeaks );
        }

        private void _btnClusters_Click( object sender, EventArgs e )
        {
            Browse( _txtClusters );
        }

        private void _btnTrend_Click( object sender, EventArgs e )
        {
            Browse( _txtTrend );
        }

        private void _btnConds_Click( object sender, EventArgs e )
        {
            Browse( _txtConds );
        }

        class OtherExportInfo : Visualisable
        {
            [XColumn(EColumn.Visible )]
            public readonly EDataSet DataSet;
            [XColumn( EColumn.Visible )]
            public readonly string FileName;

            public OtherExportInfo( EDataSet value, string fn )
            {
                this.DataSet = value;
                this.FileName = fn;
            }

            public override EPrevent SupportsHide => EPrevent.Hide | EPrevent.Comment | EPrevent.Name;

            public override string DefaultDisplayName => DataSet.ToUiString();        

            public override UiControls.ImageListOrder Icon=> UiControls.ImageListOrder.File;
        }

        private void _btnOther_Click( object sender, EventArgs e )
        {
            DataSet<OtherExportInfo> dex = new DataSet<OtherExportInfo>()
            {
                Core = _core,
                ListTitle = "Export dataset",
                ListSource = _otherExports,
                ItemDescription = z => z.FileName,
                ItemTitle = z => z.DataSet.ToUiString(),
                HandleEdit = EditOtherExport,
                HandleCommit = z => _otherExports.ReplaceAll(z.List),
            };

            if (dex.ShowListEditor( this ) )
            {
                _txtOther.Text = string.Join( ", ", _otherExports.Select( z => z.DataSet ) );
            }                                                                         
        }

        private OtherExportInfo EditOtherExport( DataSet<OtherExportInfo>.EditItemArgs input )
        {
            if (input.ReadOnly)
            {
                FrmMsgBox.ShowInfo( input.Owner, input.DefaultValue.DataSet.ToUiString(), "This dataset will be written to:\r\n" + input.DefaultValue.FileName );
                return null;
            }

            var invalid = (EDataSet)(-1);
            var existing = (input.DefaultValue != null) ? input.DefaultValue.DataSet : invalid;
            var existingFn = (input.DefaultValue != null) ? input.DefaultValue.FileName : null;
            EDataSet value = DataSet.ForDiscreteEnum<EDataSet>( _core, "Datasets", EDataSet.Acquisitions ).ShowList( input.Owner, existing );

            if (value == invalid)
            {
                return null;
            }

            string fn = UiControls.BrowseForFile( input.Owner, existingFn, UiControls.EFileExtension.Csv, FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData );

            if (fn== null)
            {
                return null;
            }

            return new OtherExportInfo( value, fn );
        }

        private void _btnOk_Click( object sender, EventArgs e )
        {
            _uniqueTable.Reset();

            FrmWait.Show( this, "Exporting data", "Your data is being exported", ExportSelected );

            if (_uniqueTable.Renamed.Count != 0)
            {
                string allRenames = string.Join( "\r\n", _uniqueTable.Renamed.Select( z => z.DisplayName + " --> " + _uniqueTable.Name( z ) ).ToArray() );
                FrmInputMultiLine.ShowFixed( this, Text, "Name clashes", null, "There are some objects with the same DisplayName. To avoid conflicts the objects were temporarily renamed. The new names are guaranteed to match for all tables exported in this instance, but may not match exports made at a later time.\r\n\r\n" + allRenames );
            }

            DialogResult = DialogResult.OK;
        }

        private void ExportSelected(ProgressReporter prog)
        {   
            if (_chkData.Checked)
            {
                prog.Enter( "Intensities" );                 
                ExportData( _txtData.Text, _ecbIntensitySource.SelectedItem.Provide );
                prog.Leave();
            }

            if (_chkObs.Checked)
            {
                prog.Enter( "Observations" );
                Export( _txtObservations.Text, DataSet.ForObservations( _core ) );
                prog.Leave();
            }

            if (_chkPeaks.Checked)
            {
                prog.Enter( "Peaks" );
                Export( _txtPeaks.Text, DataSet.ForPeaks( _core ) );
                prog.Leave();
            }

            if (_chkClusters.Checked)
            {
                prog.Enter( "Assignments" );
                ExportAssignments( _txtClusters.Text );
                prog.Leave();
            }

            if (_chkTrend.Checked)
            {
                prog.Enter( "Trend" );                    
                ExportData( _txtTrend.Text, _ecbTrendSource.SelectedItem.Provide );
                prog.Leave();
            }

            if (_chkConds.Checked)
            {
                prog.Enter( "Conditions" );
                Export( _txtConds.Text, DataSet.ForConditions( _core ) );
                prog.Leave();
            }

            if (_chkOther.Checked)
            {
                foreach (var ooi in _otherExports)
                {
                    prog.Enter( ooi.DataSet.ToUiString());
                    Export( ooi );
                    prog.Leave();
                }
            }
        }

        private void ExportAssignments( string fileName )
        {
            Cluster[] clu = _core.Clusters.ToArray();
            Spreadsheet<string> ss = new Spreadsheet<string>( _core.Peaks.Count, clu.Length );

            for (int nClust = 0; nClust < clu.Length; ++nClust)
            {
                ss.ColNames[nClust] = _uniqueTable.Name( clu[nClust]);
            }

            for (int nPeak = 0; nPeak < _core.Peaks.Count; ++nPeak)
            {
                Peak peak = _core.Peaks[nPeak];
                ss.RowNames[nPeak] = _uniqueTable.Name( peak);

                for (int nClust = 0; nClust < clu.Length; ++nClust)
                {
                    Cluster cluster = clu[nClust];
                    ss[nPeak, nClust] = string.Join( "; ", peak.FindAssignments( _core ).Where( z => z.Cluster == cluster ).Select(AssignmentToString) );
                }
            }

            ss.SaveCsv( fileName );
        }

        private object AssignmentToString( Assignment arg )
        {
            if (arg.Vector.Group == null)
            {
                return _uniqueTable.Name( arg.Cluster);
            }
            else
            {
                return _uniqueTable.Name( arg.Cluster) + ":" + arg.Vector.Group.DisplayShortName;
            }
        }

        /// <summary>
        /// Ensures every object of a particular type has a unique name (for export to R)
        /// Names are guarenteed to match up for each export session only
        /// </summary>
        class UniqueTable
        {
            Dictionary<Visualisable, string> _names = new Dictionary<Visualisable, string>();
            public readonly HashSet<Visualisable> Renamed = new HashSet<Visualisable>();
            HashSet<string> _used = new HashSet<string>();

            /// <summary>
            /// Generates a unique name
            /// </summary>             
            public string Name(Visualisable x)
            {
                string name;

                if (_names.TryGetValue( x, out name ))
                {
                    return name;
                }

                name = x.DefaultDisplayName;
                int index = 0;
                string type = x.GetType().Name + ".";
                bool renamed = false;

                while (_used.Contains( type + name ))
                {
                    renamed = true;
                    name = x.DefaultDisplayName + "." + ++index;
                }

                _used.Add( type + name );
                _names.Add( x, name );

                if (renamed)
                {
                    Renamed.Add( x );
                }

                return name;
            }

            /// <summary>
            /// Clears the names for a new export.
            /// </summary>
            internal void Reset()
            {
                _names.Clear();
                Renamed.Clear();
                _used.Clear();
            }
        }

        /// <summary>
        /// INTENSITIES
        /// </summary>                           
        private void ExportData( string fileName, IntensityMatrix source )
        {
            // nPeaks x nObs
            Spreadsheet<double> ss = new Spreadsheet<double>( source.NumRows, source.NumCols );

            for (int nObs = 0; nObs < _core.Observations.Count; ++nObs)
            {
                ss.ColNames[nObs] = _uniqueTable.Name( _core.Observations[nObs]);
            }

            for (int nPeak = 0; nPeak < _core.Peaks.Count; ++nPeak)
            {
                Peak peak = _core.Peaks[nPeak];
                ss.RowNames[nPeak] = _uniqueTable.Name( peak );

                for (int nObs = 0; nObs < _core.Observations.Count; ++nObs)
                {
                    ss[nPeak, nObs] = source.Values[nPeak][nObs];
                }
            }

            ss.SaveCsv( fileName );
        }   

        private void Export( OtherExportInfo ooi )
        {
            Export( ooi.FileName, DataSet.For( ooi.DataSet, _core ) );
        }

        private void Export( string fileName, IDataSet x )
        {
            IEnumerable utlist = x.UntypedGetList(false);  
            Visualisable[] list;

            if (utlist.FirstOrDefault2() is Visualisable)
            {
                list = utlist.Cast<Visualisable>().ToArray();
            }
            else
            {
                list = utlist.Cast<object>().Select( z => new VisualisableWrapper( x, z ) ).ToArray();
            }

            Column[] columns = ColumnManager.GetColumns( _core, list[0] ).Where( z => z.Special != EColumn.Advanced ).ToArray();

            Spreadsheet<string> ss = new Spreadsheet<string>( list.Length, columns.Length );

            for (int nObs = 0; nObs < columns.Length; ++nObs)
            {
                ss.ColNames[nObs] = columns[nObs].Id;
            }

            for (int nPeak = 0; nPeak < list.Length; ++nPeak)
            {
                Visualisable peak = list[nPeak];
                ss.RowNames[nPeak] = _uniqueTable.Name( peak);

                for (int nObs = 0; nObs < columns.Length; ++nObs)
                {
                    ss[nPeak, nObs] = Column.AsString( columns[nObs].GetRow(peak), EListDisplayMode.Content );
                }
            }

            ss.SaveCsv( fileName );
        }
    }
}
