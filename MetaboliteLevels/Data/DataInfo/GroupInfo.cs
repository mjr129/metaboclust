using System;
using System.Collections.Generic;
using System.Drawing;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MSerialisers;

namespace MetaboliteLevels.Data.DataInfo
{
    /// <summary>
    /// Experimental group / batch information.
    /// </summary>
    [Serializable]
    internal abstract class GroupInfoBase : IVisualisable
    {
        public readonly int Id;             // ID (as in data file)
        public readonly int Order;          // This program's internal index (Core.Groups[this.Order] / Core.Batches[this.Order])
        public readonly Range Range;        // Range covered (days / acquisition-order)
        //public string Name;                 // Name
        //public string ShortName;            // Abbreviated name
        public Color ColourLight;           // Display colour (light)
        public Color Colour;                // Display colour
        //public string Comment;              // User comments        

        VisualClass IVisualisable.VisualClass => VisualClass.None;

        public string DisplayName => IVisualisableExtensions.FormatDisplayName(this);

        public string ShortName => string.IsNullOrEmpty(OverrideShortName) ? DefaultShortName : OverrideShortName;

        public string DefaultShortName => this.Id.ToString();

        public abstract string DefaultDisplayName { get; }

        public string OverrideShortName { get; set; }

        public string OverrideDisplayName { get; set; }

        public string Comment { get; set; }

        bool ITitlable.Enabled { get { return true; } set { /* NA*/} }

        protected GroupInfoBase(int groupId, int order, Range xRange, string name, string shortName, Color colorLight, Color color)
        {
            this.Id = groupId;
            this.Order = order;
            this.Range = xRange;
            this.OverrideDisplayName = name;
            this.OverrideShortName = shortName;
            this.ColourLight = colorLight;
            this.Colour = color;
        }

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
            return a.Id;
        }

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<GroupInfoBase>> columns = new List<Column<GroupInfoBase>>();

            columns.Add("ID", z => z.Id);
            columns.Add("Range", EColumn.Visible, z => z.Range);
            columns.Add("Name", EColumn.Visible, z => z.DisplayName);
            columns.Add("Short name", z => z.ShortName);
            columns.Add("Default name", z => z.DefaultDisplayName);
            columns.Add("Default short name", z => z.DefaultShortName);
            columns.Add("Colour", z => z.Colour, z => z.Colour);
            columns.Add("Light colour", z => z.ColourLight, z => z.ColourLight);
            columns.Add("Comment", z => z.Comment);

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
        public GroupInfo(int groupId, int order, Range timeRange, string name, string shortName, Color colorLight, Color color)
            : base(groupId, order, timeRange, name, shortName, colorLight, color)
        {
            // NA
        }

        public override string DefaultDisplayName
        {
            get
            {
                return "Type " + this.Id;
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
        public BatchInfo(int batchId, int order, Range acqusitionRange)
            : base(batchId, order, acqusitionRange, null, batchId.ToString(), Color.DarkGray, Color.Black)
        {
            // NA
        }

        public override string DefaultDisplayName
        {
            get
            {
                return "Batch " + this.Id;
            }
        }
    }
}
