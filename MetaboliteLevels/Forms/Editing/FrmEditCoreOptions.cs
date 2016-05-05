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
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Viewers.Lists;
using System.IO;
using MGui;
using MGui.Controls;

namespace MetaboliteLevels.Forms.Editing
{
    internal partial class FrmEditCoreOptions : Form
    {
        private CoreOptions _target;
        private readonly Core _core;
        private readonly bool _readOnly;
        private readonly CtlBinder<CoreOptions> binder1 = new CtlBinder<CoreOptions>();

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

            binder1.Bind(_txtClusterXAxis, λ => λ.ClusterDisplay.AxisX);
            binder1.Bind(_txtClusterYAxis, λ => λ.ClusterDisplay.AxisY);
            binder1.Bind(_txtClusterInfo, λ => λ.ClusterDisplay.Information);
            binder1.Bind(_txtClusterSubtitle, λ => λ.ClusterDisplay.SubTitle);
            binder1.Bind(_txtClusterTitle, λ => λ.ClusterDisplay.Title);

            binder1.Bind(_txtEvalFilename, λ => λ.ClusteringEvaluationResultsFileName);

            binder1.Bind(_btnColourAxisTitle, λ => λ.Colours.AxisTitle);
            binder1.Bind(_btnColourCentre, λ => λ.Colours.ClusterCentre);
            binder1.Bind(_btnColourUntypedElements, λ => λ.Colours.InputVectorJoiners);
            binder1.Bind(_btnColourMajorGrid, λ => λ.Colours.MajorGrid);
            binder1.Bind(_btnColourMinorGrid, λ => λ.Colours.MinorGrid);
            binder1.Bind(_btnColourHighlight, λ => λ.Colours.NotableHighlight);
            binder1.Bind(_btnColourBackground, λ => λ.Colours.PlotBackground);
            binder1.Bind(_btnColourPreviewBackground, λ => λ.Colours.PreviewBackground);
            binder1.Bind(_btnColourSeries, λ => λ.Colours.SelectedSeries);

            binder1.Bind(_txtCompXAxis, λ => λ.CompoundDisplay.AxisX);
            binder1.Bind(_txtCompYAxis, λ => λ.CompoundDisplay.AxisY);
            binder1.Bind(_txtCompInfo, λ => λ.CompoundDisplay.Information);
            binder1.Bind(_txtCompSubtitle, λ => λ.CompoundDisplay.SubTitle);
            binder1.Bind(_txtCompTitle, λ => λ.CompoundDisplay.Title);

            binder1.Bind(_lstPeakPlotting, λ => λ.ConditionsSideBySide);
            binder1.Bind(_chkPeakFlag, λ => λ.EnablePeakFlagging);
            binder1.Bind(_numClusterMaxPlot, λ => λ.MaxPlotVariables);
            binder1.Bind(_numSizeLimit, λ => λ.ObjectSizeLimit);

            binder1.Bind(_txtPathXAxis, λ => λ.PathwayDisplay.AxisX);
            binder1.Bind(_txtPathYAxis, λ => λ.PathwayDisplay.AxisY);
            binder1.Bind(_txtPathInfo, λ => λ.PathwayDisplay.Information);
            binder1.Bind(_txtPathSubtitle, λ => λ.PathwayDisplay.SubTitle);
            binder1.Bind(_txtPathTitle, λ => λ.PathwayDisplay.Title);

            binder1.Bind(_txtPeakXAxis, λ => λ.PeakDisplay.AxisX);
            binder1.Bind(_txtPeakYAxis, λ => λ.PeakDisplay.AxisY);
            binder1.Bind(_txtPeakInfo, λ => λ.PeakDisplay.Information);
            binder1.Bind(_txtPeakSubtitle, λ => λ.PeakDisplay.SubTitle);
            binder1.Bind(_txtPeakTitle, λ => λ.PeakDisplay.Title);

            binder1.Bind(numericUpDown2, λ => λ.PopoutThumbnailSize);
            binder1.Bind(_chkClusterCentres, λ => λ.ShowCentres);
            binder1.Bind(_chkPeakData, λ => λ.ShowPoints);
            binder1.Bind(_chkPeakTrend, λ => λ.ShowTrend);
            binder1.Bind(_chkPeakMean, λ => λ.ShowVariableMean);
            binder1.Bind(_chkPeakRanges, λ => λ.ShowVariableRanges);

            binder1.Bind(_numThumbnail, λ => λ.ThumbnailSize);

            binder1.Bind(_lstPeakOrder, λ => λ.ViewAcquisition);

            binder1.Bind(_lstPeakData, λ => λ.ViewAlternativeObservations);

            binder1.Read(_target);

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
            binder1.Commit();

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
                Title                = "Columns",
                Source                 = _target._columnDisplayStatuses.Select( _btnEditColumns_Click__ColumnDisplay.New),
                ListChangeApplicator = z =>
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
                Title = "Default selections",
                Source = _target._defaultValues.Select( _btnEditDefaults_Click__Kvp.New ),
                ListChangeApplicator = z =>
                {
                    _target._defaultValues.Clear();

                    foreach (var v in z.List)
                    {
                        _target._defaultValues.Add( v.arg.Key, v.arg.Value );
                    }
                }
            };

            dataSet.ShowListEditor( this );
        }

        private class _btnEditDefaults_Click__Kvp : IVisualisable
        {
            public KeyValuePair<string, object> arg;

            public _btnEditDefaults_Click__Kvp( KeyValuePair<string, object> arg )
            {
                this.arg = arg;
            }

            public string Comment
            {
                get { return null; }
                set { /* ro */ }
            }

            public string DefaultDisplayName => arg.Key;

            public string DisplayName => arg.Key;

            public bool Enabled
            {
                get { return true; }
                set { /* ro */ }
            }

            public string OverrideDisplayName
            {
                get { return null; }
                set { /* ro */ }
            }                                                       

            internal static _btnEditDefaults_Click__Kvp New( KeyValuePair<string, object> arg )
            {
                return new _btnEditDefaults_Click__Kvp( arg );
            }

            public IEnumerable<Column> GetColumns( Core core )
            {
                List<Column<_btnEditDefaults_Click__Kvp>> result = new List<Column<_btnEditDefaults_Click__Kvp>>();

                result.Add( "Key"  , z => this.arg.Key );
                result.Add( "Value", z => Column.AsString( this.arg.Value, EListDisplayMode.CountAndContent ) );

                return result;
            }

            public UiControls.ImageListOrder GetIcon() => UiControls.ImageListOrder.Point;     
        }

        private class _btnEditColumns_Click__ColumnDisplay : IVisualisable
        {
          public string Key;
          public string Id;
          public CoreOptions.ColumnDetails _col;

            public _btnEditColumns_Click__ColumnDisplay( KeyValuePair<string, CoreOptions.ColumnDetails> arg )
            {
                this.Key  = arg.Key;
                this._col = arg.Value;
                this.Id   = UiControls.GetFileName( arg.Key );
            }

            public string Comment
            {
                get { return string.Empty; }  
                set { /* readonly */ }
            }

            public string DefaultDisplayName => Id;

            public string DisplayName => IVisualisableExtensions.FormatDisplayName( this );

            public bool Enabled
            {
                get { return _col.Visible; }   
                set { _col.Visible = value; }
            }

            public string OverrideDisplayName
            {
                get {return _col.DisplayName; }      
                set { _col.DisplayName = value; }
            }

            public VisualClass VisualClass => VisualClass.None;

            internal static _btnEditColumns_Click__ColumnDisplay New( KeyValuePair<string,  CoreOptions.ColumnDetails> arg )
            {
                return new _btnEditColumns_Click__ColumnDisplay( arg );
            }

            public IEnumerable<Column> GetColumns( Core core )
            {
                List<Column<_btnEditColumns_Click__ColumnDisplay>> list = new List<Column<_btnEditColumns_Click__ColumnDisplay>>();

                list.Add( "ID", EColumn.Visible, z => z.Id );
                list.Add( "Display index", EColumn.Visible, z => z._col.DisplayIndex );
                list.Add( "Display name", EColumn.Visible, z => z._col.DisplayName );
                list.Add( "Visible", EColumn.Visible, z => z._col.Visible );
                list.Add( "Width", EColumn.Visible, z => z._col.Width );
                list.Add( "Key", EColumn.Visible, z => z.Key );

                return list;
            }

            public UiControls.ImageListOrder GetIcon() => UiControls.ImageListOrder.Point;

            public void RequestContents( ContentsRequest list )
            {
                // NA
            }
        }

     
    }
}
