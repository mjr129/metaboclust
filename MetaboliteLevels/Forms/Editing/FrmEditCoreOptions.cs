using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Controls;
using System.IO;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Types.UI;
using MGui;
using MGui.Controls;
using MGui.Datatypes;

namespace MetaboliteLevels.Forms.Editing
{
    internal partial class FrmEditCoreOptions : Form
    {
        private CoreOptions _target;
        private readonly Core _core;
        private readonly bool _readOnly;
        private readonly CtlBinder<CoreOptions> _binder1 = new CtlBinder<CoreOptions>();

        public static bool Show(Form owner, Core core, bool readOnly)
        {
            using (FrmEditCoreOptions frm = new FrmEditCoreOptions(core, readOnly))
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        return true;

                    case DialogResult.Cancel:
                        return false;       

                    default:
                        throw new SwitchException();
                }
            }
        }

        public FrmEditCoreOptions(Core core, bool readOnly)
        {
            InitializeComponent();
            UiControls.SetIcon(this);                           

            _core = core;
            _readOnly = readOnly;
            _target = core.Options;


            _binder1.Bind(_txtEvalFilename, λ => λ.ClusteringEvaluationResultsFileName);

            _binder1.Bind(_btnColourAxisTitle, λ => λ.Colours.AxisTitle);
            _binder1.Bind(_btnColourCentre, λ => λ.Colours.ClusterCentre);
            _binder1.Bind(_btnColourUntypedElements, λ => λ.Colours.InputVectorJoiners);
            _binder1.Bind(_btnColourMajorGrid, λ => λ.Colours.MajorGrid);
            _binder1.Bind(_btnColourMinorGrid, λ => λ.Colours.MinorGrid);
            _binder1.Bind(_btnColourHighlight, λ => λ.Colours.NotableHighlight);
            _binder1.Bind(_btnColourBackground, λ => λ.Colours.PlotBackground);
            _binder1.Bind(_btnColourPreviewBackground, λ => λ.Colours.PreviewBackground);
            _binder1.Bind(_btnColourSeries, λ => λ.Colours.SelectedSeries);


            _binder1.Bind(_lstPeakPlotting, λ => λ.ConditionsSideBySide);
            _binder1.Bind(_chkPeakFlag, λ => λ.EnablePeakFlagging);
            _binder1.Bind(_numClusterMaxPlot, λ => λ.MaxPlotVariables);
            _binder1.Bind(_numSizeLimit, λ => λ.ObjectSizeLimit);

            _binder1.Bind(numericUpDown2, λ => λ.PopoutThumbnailSize);
            _binder1.Bind(_chkClusterCentres, λ => λ.ShowCentres);
            _binder1.Bind(_chkPeakData, λ => λ.ShowPoints);
            _binder1.Bind(_chkPeakTrend, λ => λ.ShowTrend);
            _binder1.Bind(_chkPeakMean, λ => λ.ShowVariableMean);
            _binder1.Bind(_chkPeakRanges, λ => λ.ShowVariableRanges);
            _binder1.Bind( _chkPeakMinMax, λ => λ.ShowVariableMinMax );

            _binder1.Bind(_numThumbnail, λ => λ.ThumbnailSize);

            _binder1.Bind(_lstPeakOrder, λ => λ.ViewAcquisition);           

            _binder1.Bind( _chkGroupNames, λ => λ.DrawExperimentalGroupAxisLabels );


            _binder1.Bind( _btnHhMax, λ => λ.HeatMapMaxColour );
            _binder1.Bind( _btnHhMin, λ => λ.HeatMapMinColour );
            _binder1.Bind( _btnHhNan, λ => λ.HeatMapNanColour );
            _binder1.Bind( _btnHhOor, λ => λ.HeatMapOorColour );

            _poCluster.BindAll( _binder1, new PropertyPath<CoreOptions, PlotSetup>( z => z.ClusterDisplay ) );
            _poCompounds.BindAll( _binder1, new PropertyPath<CoreOptions, PlotSetup>( z => z.CompoundDisplay ) );
            _poPathways.BindAll( _binder1, new PropertyPath<CoreOptions, PlotSetup>( z => z.PathwayDisplay ) );
            _poPeaks.BindAll( _binder1, new PropertyPath<CoreOptions, PlotSetup>( z => z.PeakDisplay ) );

            _binder1.Read( _target );

            if (readOnly)
            {
                UiControls.MakeReadOnly(this);
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            SaveAndClose(DialogResult.OK);
        }

        private void SaveAndClose(DialogResult result)
        {
            _binder1.Commit();

            DialogResult = result;
        }   

        private void _btnEditFlags_Click(object sender, EventArgs e)
        {
            DataSet.ForPeakFlags(_core).ShowListEditor(this, _readOnly ? FrmBigList.EShow.ReadOnly : FrmBigList.EShow.Default, null);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveAndClose(DialogResult.Yes);
        }

        private void resetAllToolStripMenuItem_Click( object sender, EventArgs e )
        {
            
        }

        private void _btnEditColumns_Click( object sender, EventArgs e )
        {                     
            DataSet<_btnEditColumns_Click__ColumnDisplay> dataSet = new DataSet<_btnEditColumns_Click__ColumnDisplay>()
            {
                Core                 = _core,
                ListTitle                = "Columns",
                ListSource                 = _target._columnDisplayStatuses.Select( _btnEditColumns_Click__ColumnDisplay.New),
                HandleCommit = z =>
                                       {
                                           _target._columnDisplayStatuses.Clear();
                                       
                                           foreach (var v in z.List)
                                           {
                                               _target._columnDisplayStatuses.Add( v.Key, v._col );
                                           }
                                       }
            };

            dataSet.ShowListEditor( this );
        }

        private void _btnEditDefaults_Click( object sender, EventArgs e )
        {
            DataSet<_btnEditDefaults_Click__Kvp> dataSet = new DataSet<_btnEditDefaults_Click__Kvp>()
            {
                Core = _core,
                ListTitle = "Default selections",
                ListSource = _target._defaultValues.Select( _btnEditDefaults_Click__Kvp.New ),
                HandleCommit = z =>
                {
                    _target._defaultValues.Clear();

                    foreach (var v in z.List)
                    {
                        _target._defaultValues.Add( v._arg.Key, v._arg.Value );
                    }
                }
            };

            dataSet.ShowListEditor( this );
        }

        private class _btnEditDefaults_Click__Kvp : Visualisable
        {
            public KeyValuePair<string, object> _arg;

            public _btnEditDefaults_Click__Kvp( KeyValuePair<string, object> arg )
            {
                this._arg = arg;
            }

            public override string DefaultDisplayName => _arg.Key;   

            public override EPrevent SupportsHide => EPrevent.Hide | EPrevent.Name | EPrevent.Comment;

            internal static _btnEditDefaults_Click__Kvp New( KeyValuePair<string, object> arg )
            {
                return new _btnEditDefaults_Click__Kvp( arg );
            }

            public override void GetXColumns(ColumnCollection list, Core core )
            {
                var result = list.Cast<_btnEditDefaults_Click__Kvp>();

                result.Add( "Key"  , z => this._arg.Key );
                result.Add( "Value", z => Column.AsString( this._arg.Value, EListDisplayMode.CountAndContent ) );
            }

            public override Image Icon => Resources.IconPoint;     
        }

        private class _btnEditColumns_Click__ColumnDisplay : Visualisable
        {
            [XColumn]
            public string Key;
            [XColumn]
          public string Id;
          public CoreOptions.ColumnDetails _col;

            public _btnEditColumns_Click__ColumnDisplay( KeyValuePair<string, CoreOptions.ColumnDetails> arg )
            {
                this.Key  = arg.Key;
                this._col = arg.Value;
                this.Id   = UiControls.GetFileName( arg.Key );
            }

            public override EPrevent SupportsHide => EPrevent.Comment;

            public override string DefaultDisplayName => Id;

            public override bool Hidden
            {
                get { return !_col.Visible; }   
                set { _col.Visible = !value; }
            }

            public override string OverrideDisplayName
            {
                get {return _col.DisplayName; }      
                set { _col.DisplayName = value; }
            }                                                               

            internal static _btnEditColumns_Click__ColumnDisplay New( KeyValuePair<string,  CoreOptions.ColumnDetails> arg )
            {
                return new _btnEditColumns_Click__ColumnDisplay( arg );
            }

            public override void GetXColumns( ColumnCollection ulist, Core core )
            {
                var list = ulist.Cast< _btnEditColumns_Click__ColumnDisplay>();

                list.Add( "Display index", EColumn.Visible, z => z._col.DisplayIndex );
                list.Add( "Display name", EColumn.Visible, z => z._col.DisplayName );
                list.Add( "Visible", EColumn.Visible, z => z._col.Visible );
                list.Add( "Width", EColumn.Visible, z => z._col.Width );
            }

            public override Image Icon => Resources.IconPoint;     
        }

        private void label14_Click( object sender, EventArgs e )
        {

        }
    }
}
