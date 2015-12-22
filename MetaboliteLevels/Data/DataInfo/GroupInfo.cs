using System;
using System.Drawing;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Utilities;
using MSerialisers;

namespace MetaboliteLevels.Data.DataInfo
{
    /// <summary>
    /// Experimental group / batch information.
    /// </summary>
    [Serializable]
    internal abstract class GroupInfoBase
    {
        public readonly int Id;             // ID (as in data file)
        public readonly int Order;          // This program's internal index (Core.Groups[this.Order] / Core.Batches[this.Order])
        public readonly Range Range;        // Range covered (days / acquisition-order)
        public string Name;                 // Name
        public string ShortName;            // Abbreviated name
        public Color ColourLight;           // Display colour (light)
        public Color Colour;                // Display colour
        public string Comment;              // User comments

        protected GroupInfoBase(int groupId, int order, Range xRange, string name, string shortName, Color colorLight, Color color)
        {
            this.Id = groupId;
            this.Order = order;
            this.Range = xRange;
            this.Name = name;
            this.ShortName = shortName;
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

        public static int GroupOrder(GroupInfoBase a, GroupInfoBase b)
        {
            return a.Id.CompareTo(b.Id);
        }

        public static int GroupOrderBy(GroupInfoBase a)
        {
            return a.Id;
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

        public override string ToString()
        {
            return base.Name;
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
            : base(batchId, order, acqusitionRange, "Batch " + batchId, batchId.ToString(), Color.DarkGray, Color.Black)
        {
            // NA
        }

        public override string ToString()
        {
            return "Batch " + Id;
        }
    }
}
