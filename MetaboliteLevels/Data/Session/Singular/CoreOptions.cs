using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui.Datatypes;
using MGui.Helpers;
using System.Linq;
using JetBrains.Annotations;
using MetaboliteLevels.Viewers.Charts;

namespace MetaboliteLevels.Settings
{
    /// <summary>
    /// User customisable options.
    /// </summary>
    [Serializable]
    class CoreOptions
    {
        [DisplayName( "Object size limit (Mb)" )]
        [Category( "Miscellaneous" )]
        [Description( "Maximum size of objects used in calculations before the user is told to rethink their parameters. Used to avoid disturbingly long tea breaks." )]
        [DefaultValue( 500 )]
        public int ObjectSizeLimit { get; set; }

        [DisplayName( "Show cluster centres" )]
        [Category( "Clusters" )]
        [DefaultValue( true )]
        public bool ShowCentres { get; set; }

        [DisplayName( "Maximum vectors" )]
        [Description( "Speed up plotting only plot this number of vectors at maximum in a cluster plot" )]
        [Category( "Clusters" )]
        [DefaultValue( 200 )]
        public int MaxPlotVariables { get; set; }

        [DisplayName( "Explore acquisition" )]
        [Category( "Peaks" )]
        [Description( "Plot peaks batch/acquisition-wise rather than group/time-wise." )]
        [DefaultValue( false )]
        public bool ViewAcquisition { get; set; }

        [DisplayName( "Plot experimental conditions side-by-side" )]
        [Category( "All" )]
        [DefaultValue( true )]
        public bool ConditionsSideBySide { get; set; }

        [DisplayName( "Shade min/max area" )]
        [Category( "Peaks" )]
        [DefaultValue( true )]
        public bool ShowVariableRanges { get; set; }

        [DisplayName( "Draw lines around min/max area" )]
        [Category( "Peaks" )]
        [DefaultValue( true )]
        public bool ShowVariableMinMax { get; set; }

        [DisplayName( "Show mean and std. dev." )]
        [Category( "Peaks" )]
        [DefaultValue( true )]
        public bool ShowVariableMean { get; set; }

        [DisplayName( "Show individual data points" )]
        [Category( "Peaks" )]
        [DefaultValue( true )]
        public bool ShowPoints { get; set; }

        [DisplayName( "Show trend" )]
        [Description( "Plot the trend on the graphs." )]
        [Category( "Peaks" )]
        [DefaultValue( true )]
        public bool ShowTrend { get; set; }

        [DisplayName( "Enable peak flagging" )]
        [Category( "Peaks" )]
        [Description( "When set you can toggle peak flags by pressing the corresponding key on your keyboard with the peak list selected. See the \"Peak flags\" field to edit the list of available flags." )]
        [DefaultValue( false )]
        public bool EnablePeakFlagging { get; set; }

        [DisplayName( "Peak flags" )]
        [Category( "Peaks" )]
        public List<PeakFlag> PeakFlags { get; set; }

        [DisplayName( "Colours" )]
        [Category( "All" )]
        [TypeConverter( typeof( ExpandableObjectConverter ) )]
        public CoreColourSettings Colours { get; set; }

        [DisplayName( "Display" )]
        [Category( "Clusters" )]
        [TypeConverter( typeof( ExpandableObjectConverter ) )]
        public PlotSetup ClusterDisplay { get; set; }

        [DisplayName( "No axes" )]
        [Description( "Treats all plots as a preview for drawing purposes, suppressing the drawing of the axes. This is useful if you have a very small plot window or for exporting data to small images." )]
        [Category( "Miscellaneous" )]
        [DefaultValue( false )]
        public bool NoAxes { get; set; }

        [DisplayName( "Display" )]
        [Category( "Peaks" )]
        [TypeConverter( typeof( ExpandableObjectConverter ) )]
        public PlotSetup PeakDisplay { get; set; }

        [DisplayName( "Display" )]
        [Category( "Compounds" )]
        [TypeConverter( typeof( ExpandableObjectConverter ) )]
        public PlotSetup CompoundDisplay { get; set; }

        [DisplayName( "Display" )]
        [Category( "Pathways" )]
        [TypeConverter( typeof( ExpandableObjectConverter ) )]
        public PlotSetup PathwayDisplay { get; set; }

        [NonSerialized]
        public Core _core;

        public Core Core { get { return _core; } set { _core = value; } }

        [Serializable]
        public class PlotSetup
        {
            [DisplayName( "Information bar" )]
            [Description( "The text to display in the toolbar above the plot. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
            [DefaultValue( "" )]
            public ParseElementCollection Information { get; set; }

            [DisplayName( "Plot title" )]
            [Description( "Plot title. You can leave this empty to hide the title and conserve screen space. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
            [DefaultValue( "" )]
            public ParseElementCollection Title { get; set; }

            [DisplayName( "Plot sub-title" )]
            [Description( "Plot sub-title. You can leave this empty to hide the sub-title and conserve screen space. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
            [DefaultValue( "" )]
            public ParseElementCollection SubTitle { get; set; }

            [DisplayName( "X Axis Label." )]
            [Description( "Label for the X axis, e.g. \"day\" or \"hour\". You can leave this empty to hide the axis title and conserve screen space. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
            [DefaultValue( "" )]
            public ParseElementCollection AxisX { get; set; }

            [DisplayName( "Y Axis Label" )]
            [Description( "Label for the Y axis, e.g. \"intensity\". You can leave this empty to hide the axis title and conserve screen space. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
            [Category( "All plots" )]
            [DefaultValue( "" )]
            public ParseElementCollection AxisY { get; set; }

            public override string ToString()
            {
                return (ParseElementCollection.IsNullOrEmpty( Information )
                    && ParseElementCollection.IsNullOrEmpty( Title )
                    && ParseElementCollection.IsNullOrEmpty( SubTitle )
                    && ParseElementCollection.IsNullOrEmpty( AxisX )
                    && ParseElementCollection.IsNullOrEmpty( AxisY ))
                    ? "(None)"
                    : "(Custom text)";
            }
        }

        [DisplayName( "Clustering results filename" )]
        [Description( "How to name the cluster evaluation results. Use {SESSION} for the session filename and {RESULTS} for the results folder. The extension of the file will also determine the filetype. Files will be automatically numbered." )]
        [Category( "Clustering evaluation" )]
        [DefaultValue( "{RESULTS}{SESSION}.mres" )]
        public string ClusteringEvaluationResultsFileName { get; set; }

        [Browsable( false )]
        [Description( "The visible experimental groups." )]
        public List<GroupInfo> ViewTypes { get; set; }

        [Description( "Size of thumbnails on lists" )]
        [DefaultValue( 96 )]
        public int ThumbnailSize { get; set; }

        [Description( "Size of thumbnails on popout lists" )]
        [DefaultValue( 128 )]
        public int PopoutThumbnailSize { get; set; }

        [DisplayName( "Margin" )]
        [Description( "Plot margin width" )]
        [Category( "Miscellaneous" )]
        [DefaultValue( 32 )]
        public int Margin { get; set; }

        [DisplayName( "Line width" )]
        [Description( "Multiplier applied to the width of the lines in the plots" )]
        [Category( "Miscellaneous" )]
        [DefaultValue( 1.0f )]
        public float LineWidth { get; set; }

        [DisplayName( "Display groups" )]
        [Description( "When set all groups will be displayed in cluster plots. This is the default behaviour since these plots represent the input vectors rather than the peaks directly. If unchecked only the visible groups will be displayed. Only the visible groups will ever be shown in the peak plots." )]
        [Category( "Cluster" )]
        [DefaultValue( true )]
        public bool DisplayAllGroupsInClusterPlot { get; set; }

        [DisplayName( "Experimental group axis labels" )]
        [Description( "When set labels will be drawn on the X axis indicating the experimental groups." )]
        [Category( "Peaks" )]
        [DefaultValue( true )]
        public bool DrawExperimentalGroupAxisLabels { get; set; }

        public readonly Dictionary<string, ColumnDetails> _columnDisplayStatuses = new Dictionary<string, ColumnDetails>();
        public readonly Dictionary<string, object> _defaultValues = new Dictionary<string, object>();

        [DisplayName( "Maximum values" )]
        [Category( "Heat map" )]
        [DefaultValue( typeof( Color ), "0xFFFF00" )]
        public Color HeatMapMaxColour { get; set; }

        [DisplayName( "Minimum values" )]
        [Category( "Heat map" )]
        [DefaultValue( typeof( Color ), "0x000000" )]
        public Color HeatMapMinColour { get; set; }

        [DisplayName( "Not-a-number" )]
        [Category( "Heat map" )]
        [DefaultValue( typeof( Color ), " 0xFF00FF" )]
        public Color HeatMapNanColour { get; set; }

        [DisplayName( "Out of range" )]
        [Category( "Heat map" )]
        [DefaultValue( typeof( Color ), "0xC0C0C0" )]
        public Color HeatMapOorColour { get; set; }


        private WeakReference<IMatrixProvider> _selectedMatrixProvider;
        private WeakReference<ConfigurationTrend> _selectedTrendProvider;

        /// <summary>
        /// The user-selected default viewing matrix provider
        /// If there isn't a selection this falls back to a default matrix
        /// If the selection has expired however, we still return null, otherwise it would give the impression that the selected
        /// matrix contains something else.
        /// </summary>
        [Name( "Default matrix to view using" )]
        [Category( "Peaks" )]
        [CanBeNull]
        public IMatrixProvider SelectedMatrixProvider
        {
            get
            {
                if (_selectedMatrixProvider != null)
                {
                    return _selectedMatrixProvider.GetTarget();
                }

                return this._core.Matrices.First();
            }
            set { _selectedMatrixProvider = new WeakReference<IMatrixProvider>( value ); }
        }

        /// <summary>
        /// The matrix of <see cref="SelectedMatrixProvider"/>.
        /// </summary>
        [CanBeNull]
        public IntensityMatrix SelectedMatrix => SelectedMatrixProvider?.Provide;          

        /// <summary>
        /// The selected trend
        /// </summary>
        [Name( "Default trend to view using" )]
        [Category( "Peaks" )]
        [CanBeNull]
        public ConfigurationTrend SelectedTrend
        {
            get
            {
                if (_selectedTrendProvider != null)
                {
                    return _selectedTrendProvider.GetTarget();
                }

                // Fallback    
                if (_core.AllTrends.Count != 0)
                {
                    return _core.AllTrends[0];
                }

                return ChartHelperForPeaks.FallbackSmoother;
            }
            set { _selectedTrendProvider = new WeakReference<ConfigurationTrend>( value ); }
        }   

        public CoreOptions() 
        {
            UiControls.ApplyDefaultsFromAttributes( this );
            ViewTypes = new List<GroupInfo>();
            Colours = new CoreColourSettings();
            PeakFlags = new List<PeakFlag>();
            PeakFlags.Add( new PeakFlag( "INT", '1', "Interesting", Color.Green ) );
            PeakFlags.Add( new PeakFlag( "CHK", '2', "Checked", Color.Gray ) );
            PeakFlags.Add( new PeakFlag( "BOR", '3', "Boring", Color.DarkRed ) );

            PeakDisplay = new PlotSetup();
            ClusterDisplay = new PlotSetup();
            CompoundDisplay = new PlotSetup();
            PathwayDisplay = new PlotSetup();

            PeakDisplay.Information = new ParseElementCollection( "m/z = {m/z}, rt = {meta\\rt}" );
        }

        [OnDeserializing]
        private void OnDeserializing( StreamingContext context )
        {
            UiControls.InvokeConstructor( this );
        }                 

        /// <summary>
        /// Saves or loads a listview column.
        /// </summary>
        internal void OpenColumn( bool save, string listId, Column column )
        {
            string key = listId + "\\" + column.Id;
            ColumnDetails savedData;

            if (save)
            {
                // Save
                if (!_columnDisplayStatuses.TryGetValue( key, out savedData ))
                {
                    savedData = new ColumnDetails();
                    _columnDisplayStatuses.Add( key, savedData );
                }

                savedData.DisplayIndex = column.DisplayIndex;
                savedData.Width = column.Width;
                savedData.Visible = column.Visible;
                savedData.DisplayName = column.OverrideDisplayName;
            }
            else
            {
                // Load
                if (_columnDisplayStatuses.TryGetValue( key, out savedData ))
                {
                    column.Visible = savedData.Visible;
                    column.Width = savedData.Width;
                    column.DisplayIndex = savedData.DisplayIndex;
                    column.OverrideDisplayName = savedData.DisplayName;
                }
            }
        }

        [Serializable]
        public class ColumnDetails
        {
            public bool Visible;
            public int Width;
            public int DisplayIndex;
            public string DisplayName;
        }

        internal PlotSetup GetUserText( Core core, Associational visualisable )
        {
            if (visualisable == null)
            {
                return new PlotSetup();
            }

            switch (visualisable.AssociationalClass)
            {
                case EVisualClass.Cluster:
                    return ClusterDisplay;

                case EVisualClass.Peak:
                    return PeakDisplay;

                case EVisualClass.Compound:
                    return CompoundDisplay;

                case EVisualClass.Pathway:
                    return PathwayDisplay;

                case EVisualClass.Adduct:
                case EVisualClass.Annotation:
                case EVisualClass.Assignment:
                case EVisualClass.None:
                default:
                    return new PlotSetup();
            }
        }
    }
}
