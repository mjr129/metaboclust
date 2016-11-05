using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Activities
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
            this.InitializeComponent();
            UiControls.SetIcon( this );

            this._ecbIntensitySource = DataSet.ForMatrixProviders( core ).CreateComboBox( this._lstIntensitySource, this._btnIntensitySource, EditableComboBox.EFlags.None );
            this._ecbTrendSource = DataSet.ForMatrixProviders( core ).CreateComboBox( this._lstIntensitySource, this._btnIntensitySource, EditableComboBox.EFlags.None );

            this._core = core;
        }

        public FrmActExport() : base()
        {
        }

        private void _chkData_CheckedChanged( object sender, EventArgs e )
        {
            this.Set( this._txtData, this._btnData, this._chkData, this._lstIntensitySource, this._btnIntensitySource );
        }

        private void _chkObs_CheckedChanged( object sender, EventArgs e )
        {
            this.Set( this._txtObservations , this._btnObs , this._chkObs);
        }

        private void _chkPeaks_CheckedChanged( object sender, EventArgs e )
        {
            this.Set( this._txtPeaks , this._btnPeaks , this._chkPeaks);
        }

        private void _chkClusters_CheckedChanged( object sender, EventArgs e )
        {
            this.Set( this._txtClusters , this._btnClusters , this._chkClusters);
        }        

        private void _chkOther_CheckedChanged( object sender, EventArgs e )
        {
            this.Set( this._txtOther , this._btnOther , this._chkOther);
        }

        private void _btnData_Click( object sender, EventArgs e )
        {
            this.Browse( this._txtData );
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
            this.Set( textBox, button, checkBox );

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
            this.Browse( this._txtObservations );
        }

        private void _btnPeaks_Click( object sender, EventArgs e )
        {
            this.Browse( this._txtPeaks );
        }

        private void _btnClusters_Click( object sender, EventArgs e )
        {
            this.Browse( this._txtClusters );
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

            public override string DefaultDisplayName => this.DataSet.ToUiString();        

            public override Image Icon=> Resources.MnuFile;
        }

        private void _btnOther_Click( object sender, EventArgs e )
        {
            DataSet<OtherExportInfo> dex = new DataSet<OtherExportInfo>()
            {
                Core = this._core,
                ListTitle = "Export dataset",
                ListSource = this._otherExports,
                ItemDescription = z => z.FileName,
                ItemTitle = z => z.DataSet.ToUiString(),
                HandleEdit = this.EditOtherExport,
                HandleCommit = z => this._otherExports.ReplaceAll(z.List),
            };

            if (dex.ShowListEditor( this ) != null)
            {
                this._txtOther.Text = string.Join( ", ", this._otherExports.Select( z => z.DataSet ) );
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
            EDataSet value = DataSet.ForDiscreteEnum<EDataSet>( this._core, "Datasets", EDataSet.Acquisitions ).ShowList( input.Owner, existing );

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
            this._uniqueTable.Reset();

            FrmWait.Show( this, "Exporting data", "Your data is being exported", this.ExportSelected );

            if (this._uniqueTable.Renamed.Count != 0)
            {
                string allRenames = string.Join( "\r\n", this._uniqueTable.Renamed.Select( z => z.DisplayName + " --> " + this._uniqueTable.Name( z ) ).ToArray() );
                FrmInputMultiLine.ShowFixed( this, this.Text, "Name clashes", null, "There are some objects with the same DisplayName. To avoid conflicts the objects were temporarily renamed. The new names are guaranteed to match for all tables exported in this instance, but may not match exports made at a later time.\r\n\r\n" + allRenames );
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ExportSelected(ProgressReporter prog)
        {   
            if (this._chkData.Checked)
            {
                prog.Enter( "Intensities" );                 
                this.ExportData( this._txtData.Text, this._ecbIntensitySource.SelectedItem.Provide );
                prog.Leave();
            }

            if (this._chkObs.Checked)
            {
                prog.Enter( "Observations" );
                this.Export( this._txtObservations.Text, DataSet.ForObservations( this._core ) );
                prog.Leave();
            }

            if (this._chkPeaks.Checked)
            {
                prog.Enter( "Peaks" );
                this.Export( this._txtPeaks.Text, DataSet.ForPeaks( this._core ) );
                prog.Leave();
            }

            if (this._chkClusters.Checked)
            {
                prog.Enter( "Assignments" );
                this.ExportAssignments( this._txtClusters.Text );
                prog.Leave();
            }    

            if (this._chkOther.Checked)
            {
                foreach (var ooi in this._otherExports)
                {
                    prog.Enter( ooi.DataSet.ToUiString());
                    this.Export( ooi );
                    prog.Leave();
                }
            }
        }

        private void ExportAssignments( string fileName )
        {
            Cluster[] clu = this._core.Clusters.ToArray();
            Spreadsheet<string> ss = new Spreadsheet<string>( this._core.Peaks.Count, clu.Length );

            for (int nClust = 0; nClust < clu.Length; ++nClust)
            {
                ss.ColNames[nClust] = this._uniqueTable.Name( clu[nClust]);
            }

            for (int nPeak = 0; nPeak < this._core.Peaks.Count; ++nPeak)
            {
                Peak peak = this._core.Peaks[nPeak];
                ss.RowNames[nPeak] = this._uniqueTable.Name( peak);

                for (int nClust = 0; nClust < clu.Length; ++nClust)
                {
                    Cluster cluster = clu[nClust];
                    ss[nPeak, nClust] = string.Join( "; ", peak.FindAssignments( this._core ).Where( z => z.Cluster == cluster ).Select(this.AssignmentToString) );
                }
            }

            ss.SaveCsv( fileName );
        }

        private object AssignmentToString( Assignment arg )
        {
            if (arg.Vector.Group == null)
            {
                return this._uniqueTable.Name( arg.Cluster);
            }
            else
            {
                return this._uniqueTable.Name( arg.Cluster) + ":" + arg.Vector.Group.DisplayShortName;
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

                if (this._names.TryGetValue( x, out name ))
                {
                    return name;
                }

                name = x.DefaultDisplayName;
                int index = 0;
                string type = x.GetType().Name + ".";
                bool renamed = false;

                while (this._used.Contains( type + name ))
                {
                    renamed = true;
                    name = x.DefaultDisplayName + "." + ++index;
                }

                this._used.Add( type + name );
                this._names.Add( x, name );

                if (renamed)
                {
                    this.Renamed.Add( x );
                }

                return name;
            }

            /// <summary>
            /// Clears the names for a new export.
            /// </summary>
            internal void Reset()
            {
                this._names.Clear();
                this.Renamed.Clear();
                this._used.Clear();
            }
        }

        /// <summary>
        /// INTENSITIES
        /// </summary>                           
        private void ExportData( string fileName, IntensityMatrix source )
        {
            // nPeaks x nObs
            Spreadsheet<double> ss = new Spreadsheet<double>( source.NumRows, source.NumCols );

            for (int nObs = 0; nObs < this._core.Observations.Count; ++nObs)
            {
                ss.ColNames[nObs] = this._uniqueTable.Name( this._core.Observations[nObs]);
            }

            for (int nPeak = 0; nPeak < this._core.Peaks.Count; ++nPeak)
            {
                Peak peak = this._core.Peaks[nPeak];
                ss.RowNames[nPeak] = this._uniqueTable.Name( peak );

                for (int nObs = 0; nObs < this._core.Observations.Count; ++nObs)
                {
                    ss[nPeak, nObs] = source.Values[nPeak,nObs];
                }
            }

            ss.SaveCsv( fileName );
        }   

        private void Export( OtherExportInfo ooi )
        {
            this.Export( ooi.FileName, DataSet.For( ooi.DataSet, this._core ) );
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

            Column[] columns = ColumnManager.GetColumns( this._core, list[0] ).Where( z => z.Special != EColumn.Advanced ).ToArray();

            Spreadsheet<string> ss = new Spreadsheet<string>( list.Length, columns.Length );

            for (int nObs = 0; nObs < columns.Length; ++nObs)
            {
                ss.ColNames[nObs] = columns[nObs].Id;
            }

            for (int nPeak = 0; nPeak < list.Length; ++nPeak)
            {
                Visualisable peak = list[nPeak];
                ss.RowNames[nPeak] = this._uniqueTable.Name( peak);

                for (int nObs = 0; nObs < columns.Length; ++nObs)
                {
                    ss[nPeak, nObs] = Column.AsString( columns[nObs].GetRow(peak), EListDisplayMode.Content );
                }
            }

            ss.SaveCsv( fileName );
        }
    }
}
