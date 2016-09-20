using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Utilities;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.General
{
    [Serializable]
    class Acquisition
    {
        public readonly int Replicate;
        public readonly BatchInfo Batch;
        public readonly int Order;
        public readonly string Id;   

        public Acquisition( string v, int repId, BatchInfo batchInfo, int acquisition )
        {
            this.Id = v;
            this.Replicate = repId;
            this.Batch = batchInfo;
            this.Order = acquisition;
        }
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
    class ObservationInfo : Visualisable
    {                                                  
        [XColumn]
        public readonly Acquisition Acquisition;
        [XColumn]
        public readonly GroupInfo Group;
        [XColumn]
        public readonly int Time;

        public ObservationInfo( Acquisition acquisition, GroupInfo group, int time)
        {
            this.Acquisition = acquisition;
            this.Group = group;
            this.Time = time;
        }

        [XColumn]
        public string Id => Acquisition?.Id;
        [XColumn]
        public BatchInfo Batch => Acquisition?.Batch;
        [XColumn]
        public int Order => Acquisition?.Order ?? 0;
        [XColumn]
        public int Rep => Acquisition?.Replicate ?? 0;          

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public override string DefaultDisplayName
        {
            get
            {
                return Acquisition == null ? (Group.ToString() + Time) : Id;
            }
        }                    

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

        public override UiControls.ImageListOrder Icon=> UiControls.ImageListOrder.Point;       
    }
}
