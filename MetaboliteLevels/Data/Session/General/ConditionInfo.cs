﻿using System;
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
        public const string ID_COLNAME_GROUP = "Group";
        public readonly int Time;
        public readonly GroupInfo Group; 

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
            this.Enabled = true;
        }

        public override string ToString()
        {
            return DisplayName;
        }    

        public static int GroupTimeDisplayOrder(ConditionInfo a, ConditionInfo b)
        {
            int i = a.Group.DisplayPriority.CompareTo(b.Group.DisplayPriority);

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
            columns.Add(ID_COLNAME_GROUP, EColumn.None, z => z.Group.DisplayName, z => z.Group.Colour);
            columns.Add("Time", EColumn.None, z => z.Time);
            columns.Add("Comment", EColumn.None, z => z.Comment);
            columns.Add("Enabled", EColumn.None, z => z.Enabled);

            return columns;
        }      
    }
}