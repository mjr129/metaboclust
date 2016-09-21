using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Utilities;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.General
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
    class ObservationInfo : Visualisable
    {                                                  
        [XColumn( "Acquisition\\", EColumn.Decompose)]
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
        
        public string Id => Acquisition?.Id;         
        public BatchInfo Batch => Acquisition?.Batch;
        public int Order => Acquisition?.Order ?? 0;
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

            i = a.Time.CompareTo( b.Time );

            if (i != 0)
            {
                return i;
            }

            return a.Rep.CompareTo( b.Rep );
        }

        public override UiControls.ImageListOrder Icon => UiControls.ImageListOrder.Point;
    }
}
