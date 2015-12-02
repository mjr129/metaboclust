using System;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms;

namespace MetaboliteLevels.Data.General
{
    [Serializable]
    class AssignmentList
    {
        public readonly List<Assignment> List = new List<Assignment>();

        internal IEnumerable<Cluster> Clusters
        {
            get { return this.List.Select(z => z.Cluster); }
        }

        internal IEnumerable<Peak> Peaks
        {
            get { return this.List.Select(z => z.Peak); }
        }

        internal IEnumerable<Vector> Vectors
        {
            get { return this.List.Select(z => z.Vector); }
        }

        internal IEnumerable<double> Scores
        {
            get { return this.List.Select(z => z.Score); }
        }

        internal bool IsInCluster(Cluster x)
        {
            return Clusters.Contains(x);
        }

        public int Count { get { return List.Count; } }

        internal void Add(Assignment item)
        {
            List.Add(item);
        }

        internal void Clear(IEnumerable<Cluster> involvedIn)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (involvedIn.Contains(List[i].Cluster))
                {
                    List[i].Discard();
                    List.RemoveAt(i);
                    i--;
                }
            }
        }

        internal void Clear(IEnumerable<Peak> involvedIn)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (involvedIn.Contains(List[i].Peak))
                {
                    List[i].Discard();
                    List.RemoveAt(i);
                    i--;
                }
            }
        }

        internal void Assert()
        {
            throw new NotImplementedException();
        }

        public IList<Peak> PeaksAsList
        {
            get
            {
                return new List<Peak>(Peaks); // todo: could be more efficient by wrapping instead
            }
        }

        internal Assignment Get(Cluster k)
        {
            return List.Find(z => z.Cluster == k);
        }

        internal void Clear(ConfigurationClusterer config)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Cluster.Method == config)
                {
                    List[i].Discard();
                    List.RemoveAt(i);
                    i--;
                }
            }
        }

        internal void ClearAll()
        {
            List.ForEach(z => z.Discard());
            List.Clear();
        }
    }
}
