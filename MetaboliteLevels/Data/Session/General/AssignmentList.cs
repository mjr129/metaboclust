using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Main;

namespace MetaboliteLevels.Data.Session.General
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
            return this.Clusters.Contains(x);
        }

        public int Count { get { return this.List.Count; } }

        internal void Add(Assignment item)
        {
            this.List.Add(item);
        }    

        internal Assignment Get(Cluster k)
        {
            return this.List.Find(z => z.Cluster == k);
        }        

        internal void ClearAll()
        {
            this.List.ForEach(z => z.Discard());
            this.List.Clear();
        }

        public override string ToString()
        {
            return this.Count.ToString();
        }
    }
}
