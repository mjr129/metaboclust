using System;
using System.Collections.Generic;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MSerialisers;

namespace MetaboliteLevels.Data.DataInfo
{
    public class Acquisition
    {
        public readonly int Replicate;
        public readonly BatchInfo Batch;
        public readonly int Order;
        public readonly string Id;
    }

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
        public readonly Acquisition Acquisition;
        public readonly GroupInfo Group;
        private readonly int Time;

        public ObservationInfo( Acquisition acquisition, GroupInfo group, int time)
        {
            this.Acquisition = acquisition;
            this.Group = group;
            this.Time = time;
        }

        public string Id => Acquisition?.Id;
        public BatchInfo Batch => Acquisition?.BatchInfo;
        public int Order => Acquisition?.Order ?? 0;
        public int Rep => Acquisition?.Replicate ?? 0;

        public override string ToString()
        {
            return DisplayName;
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
                return Acquisition == null ? (Group.ToString() + Time) : Id;
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

        public static int GroupTimeReplicateDisplayOrder(ObservationInfo a, ObservationInfo b)
        {
            int i = a.Group.DisplayPriority.CompareTo(b.Group.DisplayPriority);

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

        public static int BatchAcquisitionDisplayOrder(ObservationInfo a, ObservationInfo b)
        {
            int i = a.Batch.DisplayPriority.CompareTo(b.Batch.DisplayPriority);

            if (i != 0)
            {
                return i;
            }

            return a.Order.CompareTo(b.Order);
        }

        public static int GroupTimeDisplayOrder(ObservationInfo a, ObservationInfo b)
        {
            int i = a.Group.DisplayPriority.CompareTo(b.Group.DisplayPriority);

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
            columns.Add( "ID", EColumn.None, z => z.Id );
            columns.Add(ID_COLNAME_GROUP, EColumn.None, z => z.Group, z => z.Group.Colour);
            columns.Add("Replicate", EColumn.None, z => z.Rep);
            columns.Add("Time", EColumn.None, z => z.Time);
            columns.Add("Acquisition", EColumn.None, z => z.Acquisition);
            columns.Add("Batch", EColumn.None, z => z.Batch, z => z.Batch.Colour);
            columns.Add("Comment", EColumn.None, z => z.Comment);
            columns.Add("Enabled", EColumn.None, z => z.Enabled);

            return columns;
        }     
    }
}
