using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.Main
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
        
        public string Id => this.Acquisition?.Id;         
        public BatchInfo Batch => this.Acquisition?.Batch;
        public int Order => this.Acquisition?.Order ?? 0;
        public int Rep => this.Acquisition?.Replicate ?? 0;          

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public override string DefaultDisplayName
        {
            get
            {
                return this.Acquisition == null ? (this.Group.ToString() + this.Time) : this.Id;
            }
        }                    

        public static int GroupTimeReplicateDisplayOrder(ObservationInfo a, ObservationInfo b)
        {
            int aDp = a.Group?.DisplayPriority ?? -1;
            int bDp = b.Group?.DisplayPriority ?? -1;

            int i = aDp.CompareTo(bDp);

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

        public override Image Icon => Resources.IconPoint;

        public bool IsReplicateOf( ObservationInfo b )
        {
            return Group == b.Group && Time == b.Time;
        }
    }
}
