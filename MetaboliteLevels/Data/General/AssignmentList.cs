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

        internal Assignment Get(Cluster k)
        {
            return List.Find(z => z.Cluster == k);
        }        

        internal void ClearAll()
        {
            List.ForEach(z => z.Discard());
            List.Clear();
        }
    }
}
