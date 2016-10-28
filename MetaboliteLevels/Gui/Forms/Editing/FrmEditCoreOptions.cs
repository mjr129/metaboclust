using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Controls;
using MGui.Datatypes;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    internal partial class FrmEditCoreOptions : Form
    {
        private CoreOptions _target;
        private readonly Core _core;
        private readonly bool _readOnly;
        private readonly CtlBinder<CoreOptions> _binder1 = new CtlBinder<CoreOptions>();

        private CtlContextHelp _help;

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
            this.InitializeComponent();
            UiControls.SetIcon(this);                           

            this._core = core;
            this._readOnly = readOnly;
            this._target = core.Options;
            this._help = new CtlContextHelp();
            this._help.Bind( this, this.ctlTitleBar1, this._binder1.ToolTipControl, CtlContextHelp.EFlags.OnFocus | CtlContextHelp.EFlags.ClickLabels );


            this._binder1.Bind(this._txtEvalFilename, λ => λ.ClusteringEvaluationResultsFileName);

            this._binder1.Bind(this._btnColourAxisTitle, λ => λ.Colours.AxisTitle);
            this._binder1.Bind(this._btnColourCentre, λ => λ.Colours.ClusterCentre);
            this._binder1.Bind(this._btnColourUntypedElements, λ => λ.Colours.InputVectorJoiners);
            this._binder1.Bind(this._btnColourMajorGrid, λ => λ.Colours.MajorGrid);
            this._binder1.Bind(this._btnColourMinorGrid, λ => λ.Colours.MinorGrid);
            this._binder1.Bind(this._btnColourHighlight, λ => λ.Colours.NotableHighlight);
            this._binder1.Bind(this._btnColourBackground, λ => λ.Colours.PlotBackground);
            this._binder1.Bind(this._btnColourPreviewBackground, λ => λ.Colours.PreviewBackground);
            this._binder1.Bind(this._btnColourSeries, λ => λ.Colours.SelectedSeries);


            this._binder1.Bind(this._lstPeakPlotting, λ => λ.ConditionsSideBySide);
            this._binder1.Bind(this._chkPeakFlag, λ => λ.EnablePeakFlagging);
            this._binder1.Bind(this._numClusterMaxPlot, λ => λ.MaxPlotVariables);
            this._binder1.Bind(this._numSizeLimit, λ => λ.ObjectSizeLimit);

            this._binder1.Bind(this.numericUpDown2, λ => λ.PopoutThumbnailSize);
            this._binder1.Bind(this._chkClusterCentres, λ => λ.ShowCentres);
            this._binder1.Bind(this._chkPeakData, λ => λ.ShowPoints);
            this._binder1.Bind(this._chkPeakTrend, λ => λ.ShowTrend);
            this._binder1.Bind(this._chkPeakMean, λ => λ.ShowVariableMean);
            this._binder1.Bind(this._chkPeakRanges, λ => λ.ShowVariableRanges);
            this._binder1.Bind( this._chkPeakMinMax, λ => λ.ShowVariableMinMax );

            this._binder1.Bind(this._numThumbnail, λ => λ.ThumbnailSize);

            this._binder1.Bind(this._lstPeakOrder, λ => λ.ViewAcquisition);           

            this._binder1.Bind( this._chkGroupNames, λ => λ.DrawExperimentalGroupAxisLabels );


            this._binder1.Bind( this._btnHhMax, λ => λ.HeatMapMaxColour );
            this._binder1.Bind( this._btnHhMin, λ => λ.HeatMapMinColour );
            this._binder1.Bind( this._btnHhNan, λ => λ.HeatMapNanColour );
            this._binder1.Bind( this._btnHhOor, λ => λ.HeatMapOorColour );

            this._poCluster.BindAll  ( this._binder1, new PropertyPath<CoreOptions, PlotSetup>( z => z.ClusterDisplay  ), _core, typeof(Cluster) );
            this._poCompounds.BindAll( this._binder1, new PropertyPath<CoreOptions, PlotSetup>( z => z.CompoundDisplay ), _core, typeof(Compound) );
            this._poPathways.BindAll ( this._binder1, new PropertyPath<CoreOptions, PlotSetup>( z => z.PathwayDisplay  ), _core, typeof( Pathway ) );
            this._poPeaks.BindAll    ( this._binder1, new PropertyPath<CoreOptions, PlotSetup>( z => z.PeakDisplay     ), _core, typeof( Peak ) );

            this._binder1.Read( this._target );

            if (readOnly)
            {
                UiControls.MakeReadOnly(this);
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            this.SaveAndClose(DialogResult.OK);
        }

        private void SaveAndClose(DialogResult result)
        {
            this._binder1.Commit();

            this.DialogResult = result;
        }   

        private void _btnEditFlags_Click(object sender, EventArgs e)
        {
            DataSet.ForPeakFlags(this._core).ShowListEditor(this, this._readOnly ? FrmBigList.EShow.ReadOnly : FrmBigList.EShow.Default, null);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SaveAndClose(DialogResult.Yes);
        }

        private void resetAllToolStripMenuItem_Click( object sender, EventArgs e )
        {
            
        }

        private void _btnEditColumns_Click( object sender, EventArgs e )
        {                     
            DataSet<_btnEditColumns_Click__ColumnDisplay> dataSet = new DataSet<_btnEditColumns_Click__ColumnDisplay>()
            {
                Core                 = this._core,
                ListTitle                = "Columns",
                ListSource                 = this._target._columnDisplayStatuses.Select( _btnEditColumns_Click__ColumnDisplay.New),
                HandleCommit = z =>
                                       {
                                           this._target._columnDisplayStatuses.Clear();
                                       
                                           foreach (var v in z.List)
                                           {
                                               this._target._columnDisplayStatuses.Add( v.Key, v._col );
                                           }
                                       }
            };

            dataSet.ShowListEditor( this );
        }

        private void _btnEditDefaults_Click( object sender, EventArgs e )
        {
            DataSet<_btnEditDefaults_Click__Kvp> dataSet = new DataSet<_btnEditDefaults_Click__Kvp>()
            {
                Core = this._core,
                ListTitle = "Default selections",
                ListSource = this._target._defaultValues.Select( _btnEditDefaults_Click__Kvp.New ),
                HandleCommit = z =>
                {
                    this._target._defaultValues.Clear();

                    foreach (var v in z.List)
                    {
                        this._target._defaultValues.Add( v._arg.Key, v._arg.Value );
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

            public override string DefaultDisplayName => this._arg.Key;   

            public override EPrevent SupportsHide => EPrevent.Hide | EPrevent.Name | EPrevent.Comment;

            internal static _btnEditDefaults_Click__Kvp New( KeyValuePair<string, object> arg )
            {
                return new _btnEditDefaults_Click__Kvp( arg );
            }

            public override void GetXColumns( CustomColumnRequest request )
            {
                var result = request.Results.Cast<_btnEditDefaults_Click__Kvp>();

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

            public override string DefaultDisplayName => this.Id;

            public override bool Hidden
            {
                get { return !this._col.Visible; }   
                set { this._col.Visible = !value; }
            }

            public override string OverrideDisplayName
            {
                get {return this._col.DisplayName; }      
                set { this._col.DisplayName = value; }
            }                                                               

            internal static _btnEditColumns_Click__ColumnDisplay New( KeyValuePair<string,  CoreOptions.ColumnDetails> arg )
            {
                return new _btnEditColumns_Click__ColumnDisplay( arg );
            }

            public override void GetXColumns( CustomColumnRequest request )
            {
                var list = request.Results.Cast< _btnEditColumns_Click__ColumnDisplay>();

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
