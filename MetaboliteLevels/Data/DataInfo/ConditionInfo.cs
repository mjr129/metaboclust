using System;
using System.Collections.Generic;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MSerialisers;

namespace MetaboliteLevels.Data.DataInfo
{
    /// <summary>
    /// Experimental conditions.
    /// aka. Observation for the trend-line (after averaging out replicates), Averaged independent variables, Averaged X variables
    /// 
    /// See ObservationInfo for the raw observation
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class ConditionInfo : IVisualisable
    {
        public readonly int Time;
        public readonly GroupInfo Group;

        VisualClass IVisualisable.VisualClass
        {
            get
            {
                return VisualClass.None;
            }
        }

        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.FormatDisplayName(this);
            }
        }

        public string DefaultDisplayName
        {
            get
            {
                return Group.DisplayName + Time;
            }
        }

        public string OverrideDisplayName { get; set; }

        public string Comment { get; set; }

        public bool Enabled { get; set; }

        public ConditionInfo(int time, GroupInfo groupInfo)
        {
            this.Time = time;
            this.Group = groupInfo;
        }

        public override string ToString()
        {
            return DisplayName;
        }    

        public static int GroupTimeOrder(ConditionInfo a, ConditionInfo b)
        {
            int i = a.Group.Id.CompareTo(b.Group.Id);

            if (i != 0)
            {
                return i;
            }

            return a.Time.CompareTo(b.Time);
        }

        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            return UiControls.ImageListOrder.Point;
        }       

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<ConditionInfo>> columns = new List<Column<ConditionInfo>>();

            columns.Add("Name", EColumn.Visible, z => z.DisplayName);
            columns.Add("Group", EColumn.None, z => z.Group.DisplayName, z => z.Group.Colour);
            columns.Add("Time", EColumn.None, z => z.Time);
            columns.Add("Comment", EColumn.None, z => z.Comment);

            return columns;
        }

        public void RequestContents(ContentsRequest list)
        {
            // NA
        }
    }
}
