using System;
using MetaboliteLevels.Utilities;
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
    class ConditionInfo
    {
        public readonly int Time;
        public readonly GroupInfo Group;           

        public ConditionInfo(int time, GroupInfo groupInfo)
        {
            this.Time = time;
            this.Group = groupInfo;
        }

        public override string ToString()
        {
            return Group.ShortName + Time;
        }

        public static int TimeOrder(ConditionInfo x, ConditionInfo y)
        {
            return x.Time.CompareTo(y.Time);
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
    }
}
