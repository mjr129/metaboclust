﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Activities
{
    internal partial class FrmActExport : Form
    {
        List<OtherExportInfo> _otherExports = new List<OtherExportInfo>();
        private Core _core;

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

            _core = core;
        }

        public FrmActExport() : base()
        {
        }

        private void _chkData_CheckedChanged( object sender, EventArgs e )
        {
            Set( _txtData, _btnData, _chkData );
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
            Set( _txtTrend , _btnTrend , _chkTrend);
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

        class OtherExportInfo : IVisualisable
        {
            public readonly EDataSet DataSet;
            public readonly string FileName;

            public OtherExportInfo( EDataSet value, string fn )
            {
                this.DataSet = value;
                this.FileName = fn;
            }

            public string Comment { get { return null; } set { } }

            public string DefaultDisplayName => DataSet.ToUiString();

            public string DisplayName
            {
                get
                {
                    return IVisualisableExtensions.FormatDisplayName( this );
                }
            }

            public bool Enabled
            {
                get
                {
                    return true;
                }
                set
                {
                    // NA
                }
            }

            public string OverrideDisplayName
            {
                get
                {
                    return null;
                }        
                set
                {
                    // NA
                }
            }

            public IEnumerable<Column> GetColumns( Core core )
            {
                List<Column<OtherExportInfo>> cols = new List<Column<OtherExportInfo>>();

                cols.Add( "Dataset", EColumn.Visible, z => z.DataSet.ToUiString() );
                cols.Add( "File name", EColumn.Visible, z => z.FileName );

                return cols;
            }

            public UiControls.ImageListOrder GetIcon()
            {
                return UiControls.ImageListOrder.File;
            }
        }

        private void _btnOther_Click( object sender, EventArgs e )
        {
            DataSet<OtherExportInfo> dex = new DataSet<OtherExportInfo>()
            {
                Core = _core,
                Title = "Export dataset",
                Source = _otherExports,
                ItemDescriptionProvider = z => z.FileName,
                ItemNameProvider = z => z.DataSet.ToUiString(),
                ItemEditor = EditOtherExport,
                ListChangeApplicator = z => _otherExports.ReplaceAll(z.List),
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
            EDataSet value = DataSet.ForDiscreteEnum<EDataSet>( "Datasets", EDataSet.Acquisitions ).ShowList( input.Owner, existing );

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
            FrmWait.Show( this, "Exporting data", "Your data is being exported", ExportSelected );
            DialogResult = DialogResult.OK;
        }

        private void ExportSelected(ProgressReporter prog)
        {
            if (_chkData.Checked)
            {
                prog.Enter( "Intensities" );
                ExportData( _txtData.Text );
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
                ExportTrend( _txtTrend.Text );
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
            Spreadsheet<string> ss = new Spreadsheet<string>( _core.Peaks.Count, _core.Clusters.Count );

            for (int nClust = 0; nClust < _core.Clusters.Count; ++nClust)
            {
                ss.ColNames[nClust] = _core.Clusters[nClust].DisplayName;
            }

            for (int nPeak = 0; nPeak < _core.Peaks.Count; ++nPeak)
            {
                Peak peak = _core.Peaks[nPeak];
                ss.RowNames[nPeak] = peak.DisplayName;

                for (int nClust = 0; nClust < _core.Clusters.Count; ++nClust)
                {
                    Cluster cluster = _core.Clusters[nClust];
                    ss[nPeak, nClust] = string.Join( "; ", peak.Assignments.List.Where( z => z.Cluster == cluster ).Select(AssignmentToString) );
                }
            }

            ss.SaveCsv( fileName );
        }

        private object AssignmentToString( Assignment arg )
        {
            if (arg.Vector.Group == null)
            {
                return arg.Cluster.DisplayName;
            }
            else
            {
                return arg.Cluster.DisplayName + ":" + arg.Vector.Group.DisplayShortName;
            }
        }

        private void ExportData( string fileName )
        {
            Spreadsheet<double> ss = new Spreadsheet<double>( _core.Peaks.Count, _core.Observations.Count );

            for (int nObs = 0; nObs < _core.Observations.Count; ++nObs)
            {
                ss.ColNames[nObs] = _core.Observations[nObs].DisplayName;
            }

            for (int nPeak = 0; nPeak < _core.Peaks.Count; ++nPeak)
            {
                Peak peak = _core.Peaks[nPeak];
                ss.RowNames[nPeak] = peak.DisplayName;

                for (int nObs = 0; nObs < _core.Observations.Count; ++nObs)
                {
                    ss[nPeak, nObs] = peak.Observations.Raw[nObs];
                }
            }

            ss.SaveCsv( fileName );
        }

        private void ExportTrend( string fileName )
        {
            Spreadsheet<double> ss = new Spreadsheet<double>( _core.Peaks.Count, _core.Conditions.Count );

            for (int nObs = 0; nObs < _core.Conditions.Count; ++nObs)
            {
                ss.ColNames[nObs] = _core.Conditions[nObs].DisplayName;
            }

            for (int nPeak = 0; nPeak < _core.Peaks.Count; ++nPeak)
            {
                Peak peak = _core.Peaks[nPeak];
                ss.RowNames[nPeak] = peak.DisplayName;

                for (int nObs = 0; nObs < _core.Conditions.Count; ++nObs)
                {
                    ss[nPeak, nObs] = peak.Observations.Trend[nObs];
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
            IVisualisable[] list;

            if (utlist.FirstOrDefault2() is IVisualisable)
            {
                list = utlist.Cast<IVisualisable>().ToArray();
            }
            else
            {
                list = utlist.Cast<object>().Select( z => new VisualisableWrapper( x, z ) ).ToArray();
            }

            Column[] columns = ColumnManager.GetColumns( _core, list[0] ).Where( z => z.Special != EColumn.Advanced ).ToArray();

            Spreadsheet<string> ss = new Spreadsheet<string>( _core.Peaks.Count, columns.Length );

            for (int nObs = 0; nObs < columns.Length; ++nObs)
            {
                ss.ColNames[nObs] = columns[nObs].Id;
            }

            for (int nPeak = 0; nPeak < list.Length; ++nPeak)
            {
                IVisualisable peak = list[nPeak];
                ss.RowNames[nPeak] = peak.DisplayName;

                for (int nObs = 0; nObs < columns.Length; ++nObs)
                {
                    ss[nPeak, nObs] = Column.AsString( columns[nObs].GetRow(peak), EListDisplayMode.Content );
                }
            }

            ss.SaveCsv( fileName );
        }
    }
}