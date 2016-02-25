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
    /// Observation information.
    /// 
    /// aka. Raw independent variables, Raw X variables
    /// 
    /// See ConditionInfo for the observations after averaging out the replicates
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class ObservationInfo : IVisualisable
    {
        public const string ID_COLNAME_GROUP = "Group";
        public readonly ConditionInfo _conditions;
        public readonly int Rep;
        public readonly BatchInfo Batch;
        public readonly int Acquisition;

        public ObservationInfo(ConditionInfo conditions, int rep, BatchInfo batch, int acquisition)
        {
            this._conditions = conditions;
            this.Rep = rep;
            this.Batch = batch;
            this.Acquisition = acquisition;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public int Time
        {
            get { return _conditions.Time; }
        }

        public GroupInfo Group
        {
            get { return _conditions.Group; }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public VisualClass VisualClass
        {
            get
            {
                return VisualClass.None;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.FormatDisplayName(this);
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public string DefaultDisplayName
        {
            get
            {
                return _conditions.DisplayName + "r" + Rep;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public bool Enabled { get; set; } 

        public static int GroupTimeReplicateOrder(ObservationInfo a, ObservationInfo b)
        {
            int i = a.Group.Id.CompareTo(b.Group.Id);

            if (i != 0)
            {
                return i;
            }

            i = a.Time.CompareTo(b.Time);

            if (i != 0)
            {
                return i;
            }

            return a.Rep.CompareTo(b.Rep);
        }

        public static int BatchAcquisitionOrder(ObservationInfo a, ObservationInfo b)
        {
            int i = a.Batch.Id.CompareTo(b.Batch.Id);

            if (i != 0)
            {
                return i;
            }

            return a.Acquisition.CompareTo(b.Acquisition);
        }

        public static int GroupTimeOrder(ObservationInfo a, ObservationInfo b)
        {
            int i = a.Group.Id.CompareTo(b.Group.Id);

            if (i != 0)
            {
                return i;
            }

            i = a.Time.CompareTo(b.Time);

            if (i != 0)
            {
                return i;
            }

            return a.Rep.CompareTo(b.Rep);
        }

        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            return UiControls.ImageListOrder.Point;
        }     

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<ObservationInfo>> columns = new List<Column<ObservationInfo>>();

            columns.Add("Name", EColumn.Visible, z => z.DisplayName);
            columns.Add(ID_COLNAME_GROUP, EColumn.None, z => z.Group.DisplayName, z => z.Group.Colour);
            columns.Add("Replicate", EColumn.None, z => z.Rep);
            columns.Add("Time", EColumn.None, z => z.Time);
            columns.Add("Acquisition", EColumn.None, z => z.Acquisition);
            columns.Add("Batch", EColumn.None, z => z.Batch.DisplayName, z => z.Batch.Colour);
            columns.Add("Comment", EColumn.None, z => z.Comment);

            return columns;
        }

        void IVisualisable.RequestContents(ContentsRequest list)
        {
            // NA
        }
    }
}
