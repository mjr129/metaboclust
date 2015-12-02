using System;
using MetaboliteLevels.Utilities;
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
    class ObservationInfo
    {
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
            return _conditions.ToString() + "r" + Rep;
        }

        public int Time
        {
            get { return _conditions.Time; }
        }

        public GroupInfo Group
        {
            get { return _conditions.Group; }
        }       

        public static int TimeRepOrder(ObservationInfo a, ObservationInfo b)
        {
            int i = a.Time.CompareTo(b.Time);

            if (i != 0)
            {
                return i;
            }

            return a.Rep.CompareTo(b.Rep);
        }

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
    }
}
