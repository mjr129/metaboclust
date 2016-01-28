using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Settings
{
    /// <summary>
    /// User customisable options.
    /// </summary>
    [Serializable]
    class CoreOptions
    {
        [DisplayName("Object size limit (Mb)")]
        [Category("Miscellaneous")]
        [Description("Maximum size of objects used in calculations before the user is told to rethink their parameters. Used to avoid disturbingly long tea breaks.")]
        [DefaultValue(500)]
        public int ObjectSizeLimit { get; set; }

        [DisplayName("Show cluster centres")]
        [Category("Cluster plot")]
        [DefaultValue(true)]
        public bool ShowCentres { get; set; }

        [DisplayName("Maximum variables")]
        [Description("To speed up plotting only plot this number of variables at maximum")]
        [Category("Cluster plot")]
        [DefaultValue(200)]
        public int MaxPlotVariables { get; set; }

        [DisplayName("View alternative dataset")]
        [Category("Peak plot")]
        [Description("Plot the \"alternate data set\" loaded in at program startup.")]
        [DefaultValue(false)]
        public bool ViewAlternativeObservations { get; set; }

        [DisplayName("Explore acquisition")]
        [Category("Peak plot")]
        [Description("Plot peaks batch/acquisition-wise rather than group/time-wise.")]
        [DefaultValue(false)]
        public bool ViewAcquisition { get; set; }

        [DisplayName("Plot experimental conditions side-by-side")]
        [Category("All plots")]
        [DefaultValue(true)]
        public bool ConditionsSideBySide { get; set; }

        [DisplayName("Shade min/max area")]
        [Category("Peak plot")]
        [DefaultValue(true)]
        public bool ShowVariableRanges { get; set; }

        [DisplayName("Show mean and std. dev.")]
        [Category("Peak plot")]
        [DefaultValue(true)]
        public bool ShowVariableMean { get; set; }

        [DisplayName("Show individual data points")]
        [Category("Peak plot")]
        [DefaultValue(true)]
        public bool ShowPoints { get; set; }

        [DisplayName("Show trend")]
        [Description("Plot the trend on the graphs.")]
        [Category("Peak plot")]
        [DefaultValue(true)]
        public bool ShowTrend { get; set; }

        [DisplayName("Cluster text")]
        [Category("Cluster plot")]
        [Description("The text to display above the cluster plot. See \"Peak text\" for details on formatting.")]
        [DefaultValue("m/z = {mz}, rt = {rt}")]
        public ParseElementCollection ClusterDisplay { get; set; }

        [DisplayName("Peak text")]
        [Category("Peak plot")]
        [Description("The text to display above the peak plot. You can use {braces} to specify special values by ID. See the peak information and statistics panels at the bottom left of the main screen for the available IDs. {$id} = statistic (select the view statistics menu option and view the IDs), {*id} = meta field (see information panel or open your peak information file), {+id} = information determined by this program (see information panel), {!id} = special properties and fields (e.g. Name).")]
        [DefaultValue("m/z = {mz}, rt = {rt}")]
        public ParseElementCollection VariableDisplay { get; set; }

        [DisplayName("Enable peak flagging")]
        [Category("Peak list")]
        [Description("When set you can toggle peak flags by pressing the corresponding key on your keyboard with the peak list selected. See the \"Peak flags\" field to edit the list of available flags.")]
        [DefaultValue(false)]
        public bool EnablePeakFlagging { get; set; }

        [DisplayName("Peak flags")]
        [Category("Peak list")]
        public List<PeakFlag> PeakFlags { get; set; }

        [DisplayName("Colours")]
        [Category("All plots")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CoreColourSettings Colours { get; set; }

        [DisplayName("Plot title")]
        [Description("Plot title. You can leave this empty to hide the title and conserve screen space. See \"Peak text\" for details on formatting.")]
        [Category("All plots")]
        [DefaultValue("")]
        public ParseElementCollection PlotTitle { get; set; }

        [DisplayName("Plot sub-title")]
        [Description("Plot sub-title. You can leave this empty to hide the sub-title and conserve screen space. See \"Peak text\" for details on formatting.")]
        [Category("All plots")]
        [DefaultValue("")]
        public ParseElementCollection PlotSubTitle { get; set; }

        [DisplayName("X Axis Label.")]
        [Description("Label for the X axis, e.g. \"day\" or \"hour\". You can leave this empty to hide the axis title and conserve screen space.")]
        [Category("All plots")]
        [DefaultValue("")]
        public ParseElementCollection AxisLabelX { get; set; }

        [DisplayName("Clustering results filename")]
        [Description("How to name the cluster evaluation results. Use {SESSION} for the session filename and {RESULTS} for the results folder. The extension of the file will also determine the filetype. Files will be automatically numbered.")]
        [Category("Clustering evaluation")]
        [DefaultValue("{RESULTS}{SESSION}.mres")]
        public string ClusteringEvaluationResultsFileName { get; set; }

        [DisplayName("Y Axis Label")]
        [Description("Label for the Y axis, e.g. \"intensity\". You can leave this empty to hide the axis title and conserve screen space. See \"Peak text\" for details on formatting.")]
        [Category("All plots")]
        [DefaultValue("")]
        public ParseElementCollection AxisLabelY { get; set; }

        private readonly Dictionary<string, ColumnDetails> _columnDisplayStatuses = new Dictionary<string, ColumnDetails>();
        private readonly Dictionary<string, object> _defaultValues = new Dictionary<string, object>();

        [DisplayName("List display mode")]
        [Category("Miscellaneous")]
        [Description("How lists are displayed: Count = Display number of items, Content = Display comma delimited contents, CountAndContent = Display count followed by comma delimited contents, Smart = If one item then display content, if more than one item then display count, if no items display nothing. Note that displaying contents for large lists may yield poor performance.")]
        [DefaultValue(EListDisplayMode.Smart)]
        public EListDisplayMode ListDisplayMode { get; set; }

        [Browsable(false)]
        [Description("The visible experimental groups.")]
        public List<GroupInfo> ViewTypes { get; set; }

        public CoreOptions()
        {
            UiControls.ApplyDefaultsFromAttributes(this);
            ViewTypes = new List<GroupInfo>();
            Colours = new CoreColourSettings();
            PeakFlags = new List<PeakFlag>();
            PeakFlags.Add(new PeakFlag("INT", '1', "Interesting", Color.Green));
            PeakFlags.Add(new PeakFlag("CHK", '2', "Checked", Color.Gray));
            PeakFlags.Add(new PeakFlag("BOR", '3', "Boring", Color.DarkRed));
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            UiControls.InvokeConstructor(this);
        }

        public DefaultValueBag OpenForm(Form form)
        {
            return new DefaultValueBag(_defaultValues, form);
        }

        /// <summary>
        /// Saves or loads a listview column.
        /// </summary>
        internal void OpenColumn(bool save, string listId, string columnId, ref string displayName, ref bool visible, ref int width, ref int displayIndex)
        {
            string key = listId + "\\" + columnId;
            ColumnDetails r;

            if (save)
            {
                // Save
                if (!_columnDisplayStatuses.TryGetValue(key, out r))
                {
                    r = new ColumnDetails();
                    _columnDisplayStatuses.Add(key, r);
                }

                r.DisplayIndex = displayIndex;
                r.Width = width;
                r.Visible = visible;
                r.DisplayName = displayName;
            }
            else
            {
                // Load
                if (_columnDisplayStatuses.TryGetValue(key, out r))
                {
                    visible = r.Visible;
                    width = r.Width;
                    displayIndex = r.DisplayIndex;
                    displayName = r.DisplayName;
                }
            }
        }

        [Serializable]
        private class ColumnDetails
        {
            public bool Visible;
            public int Width;
            public int DisplayIndex;
            public string DisplayName;
        }

        internal string GetUserComment(Core core, IVisualisable visualisable)
        {
            switch (visualisable.VisualClass)
            {
                case VisualClass.Cluster:
                    return ClusterDisplay.ConvertToString(visualisable, core);

                case VisualClass.Peak:
                    return VariableDisplay.ConvertToString(visualisable, core);

                default:
                    return null;

            }
        }
    }
}
