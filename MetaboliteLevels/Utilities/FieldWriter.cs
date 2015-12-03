/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Utilities
{
    class FieldWriter
    {
        public void Write(string field, object value)
        {
        }

        public void WriteDictionary<T, U>(string field, Dictionary<T, U> value)
        {
            Write(field, value.Count);

            foreach (var kvp in value)
            {
                Write("Key", kvp.Key);
                Write("Value", kvp.Value);
            }
        }
    }

    class FieldReader
    {
        public string Read(string field)
        {
            // 
        }

        public double ReadDouble(string field)
        {
            return double.Parse(Read(field));
        }

        public int ReadInt(string field)
        {
            return int.Parse(Read(field));
        }

        internal Dictionary<T1, T2> ReadDictionary<T1, T2>(string field)
        {
            Dictionary<T1, T2> dict = new Dictionary<T1, T2>();

            int count = ReadInt(field);

            for (int n = 0; n < count; n++)
            {
                string key = Read("Key");
                string value = Read("Value");

                dict.Add(key, value);
            }
        }
    }
}



        public void Write(FieldWriter fw, Dictionary<Cluster, int> clusterIds)
        {
            Vector.Write(fw);
            fw.Write("Cluster", clusterIds.GetOrDefault(Cluster, -1));
            fw.Write("Score", Score);
            fw.Write("NextNearestCluster", clusterIds.GetOrDefault(NextNearestCluster, -1));
            fw.WriteDictionary("AssignmentStatistics", AssignmentStatistics);
        }

        public static Assignment Read(FieldReader fr, Core core, ConditionInfo[] conditions, ObservationInfo[] observations, Dictionary<int, Cluster> clusterIds)
        {
            Vector vector = Vector.Read(fr, core, conditions, observations);
            Cluster cluster = clusterIds.GetOrDefault(fr.ReadInt("Cluster"), null);
            double score = fr.ReadDouble("Score");

            var res = new Assignment(vector, cluster, score);

            res.NextNearestCluster = clusterIds.GetOrDefault(fr.ReadInt("NextNearestCluster"), null);
            res.AssignmentStatistics = fr.ReadDictionary<string, double>("AssignmentStatistics");
        }



   public void Write(FieldWriter w)
        {
            w.Write("Peak", Peak.Name);
            w.Write("Group", Group != null ? Group.Id.ToString() : "");
            w.Write("Values", StringHelper.ArrayToString(Values));
        }

        public static Vector Read(FieldReader r, Core core, ConditionInfo[] conditions, ObservationInfo[] observations)
        {
            string peakName = r.Read("Peak");
            string groupName = r.Read("Group");
            string values = r.Read("Values");
            int groupId;

            int.TryParse(groupName, out groupId);

            return new Vector(
                core.Peaks.First(z => z.Name == peakName),
                groupName.Length == 0 ? null : core.Groups.First(z => z.Id == groupId),
                conditions,
                observations,
                StringHelper.StringToArray(values, double.Parse).ToArray());
        }*/