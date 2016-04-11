using System;
using System.Collections.Generic;
using System.Drawing;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MSerialisers;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace MetaboliteLevels.Data.DataInfo
{
    /// <summary>
    /// Experimental group / batch information.
    /// </summary>
    [Serializable]
    internal abstract class GroupInfoBase : IVisualisable
    {
        private string _id;
        public string StringId => _id; // ID (as in data file)

        public readonly int Order;          // This program's internal index (Core.Groups[this.Order] / Core.Batches[this.Order]). This is arbitrary but MUST NOT BE CHANGED.
        public readonly Range Range;        // Range covered (days / acquisition-order)
        public Color ColourLight;           // Display colour (light)
        public Color Colour;                // Display colour

        VisualClass IVisualisable.VisualClass => VisualClass.None;

        public string DisplayName => IVisualisableExtensions.FormatDisplayName(this);

        public string DisplayShortName => string.IsNullOrEmpty(OverrideShortName) ? DefaultShortName : OverrideShortName;

        public string DefaultShortName => StringId;

        public abstract string DefaultDisplayName { get; }

        public string OverrideShortName { get; set; }

        public string OverrideDisplayName { get; set; }

        public string Comment { get; set; }

        bool ITitlable.Enabled { get { return true; } set { /* NA*/} }

        public int DisplayPriority;

        protected GroupInfoBase(string groupId, int order, Range xRange, string name, string shortName, Color colorLight, Color color, int displayPriority)
        {
#pragma warning disable CS0618
            this.Id = -1;
#pragma warning restore CS0618
            this._id = groupId;
            this.Order = order;
            this.Range = xRange;
            this.OverrideDisplayName = name;
            this.OverrideShortName = shortName;
            this.ColourLight = colorLight;
            this.Colour = color;
            this.DisplayPriority = displayPriority;
        }

        #region Obsolete

        [Obsolete("For serialization of old files only. This field will now always be -1. Please use 'StringId' or '_id' instead.")]
        public int Id;             // ID (as in data file)

        [OnDeserialized]
        void OnDeserialised(StreamingContext context)
        {
#pragma warning disable CS0618
            if (Id != -1)
            {
                Debug.Write("Obsolete field 'Id' updated.");
                UiControls.Assert(_id == null, "New field '_id' expected to be null when obsolete field 'ID' is present.");

                _id = Id.ToString();
                DisplayPriority = Id;
                Id = -1;
            }
#pragma warning restore CS0618
        }

        #endregion

        internal void SetColour(Color color)
        {
            Colour = color;
            ColourLight = GetLightVersionOfColour(color);
        }

        public static Color GetLightVersionOfColour(Color color)
        {
            return UiControls.Blend(Color.White, color, 0.5);
        }

        public static int GroupOrderBy(GroupInfoBase a)
        {
            return a.DisplayPriority;
        }

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<GroupInfoBase>> columns = new List<Column<GroupInfoBase>>();

            columns.Add("ID", z => z.StringId);
            columns.Add("Range", EColumn.Visible, z => z.Range);
            columns.Add("Name", EColumn.Visible, z => z.DisplayName);
            columns.Add("Short name", z => z.DisplayShortName);
            columns.Add("Default name", z => z.DefaultDisplayName);
            columns.Add("Default short name", z => z.DefaultShortName);
            columns.Add("User provided name", z => z.OverrideDisplayName);
            columns.Add("User provided short name", z => z.OverrideShortName);
            columns.Add("Colour", z => UiControls.ColourToName(z.Colour), z => z.Colour);
            columns.Add("Light colour", z => UiControls.ColourToName(z.ColourLight), z => z.ColourLight);
            columns.Add("Comment", z => z.Comment);
            columns.Add("Display priority", z => z.DisplayPriority);

            return columns;
        }

        UiControls.ImageListOrder IVisualisable.GetIcon() => UiControls.ImageListOrder.Point;

        void IVisualisable.RequestContents(ContentsRequest list)
        {
            // NA
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    /// <summary>
    /// Experimental group information.
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    internal sealed class GroupInfo : GroupInfoBase
    {
        public GroupInfo(string groupId, int order, Range timeRange, string name, string shortName, Color colorLight, Color color, int displayPriority)
            : base(groupId, order, timeRange, name, shortName, colorLight, color, displayPriority)
        {
            // NA
        }

        public override string DefaultDisplayName
        {
            get
            {
                return StringId;
            }
        }
    }

    /// <summary>
    /// Batch information
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    internal sealed class BatchInfo : GroupInfoBase
    {
        public BatchInfo(string batchId, int order, Range acqusitionRange, string name, string shortName, int displayPriority)
            : base(batchId, order, acqusitionRange, name, shortName, Color.DarkGray, Color.Black, displayPriority)
        {
            // NA
        }

        public override string DefaultDisplayName
        {
            get
            {
                return StringId;
            }
        }
    }
}
