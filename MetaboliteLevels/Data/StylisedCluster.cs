using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Viewers;
using MetaboliteLevels.Viewers.Charts;

namespace MetaboliteLevels.Data
{
    /// <summary>
    /// A cluster with extra information for plotting graphs.
    /// </summary>
    class StylisedCluster
    {
        public class HighlightElement
        {
            public readonly IVisualisable Peak;
            public readonly GroupInfo Group;

            public HighlightElement(IVisualisable visualisable, GroupInfo group)
            {
                Peak = visualisable;
                Group = group;
            }

            public static HighlightElement FromAnnotation(Annotation arg)
            {
                return new HighlightElement(arg.Peak, null);
            }

            public static HighlightElement FromPeak(Peak arg)
            {
                return new HighlightElement(arg, null);
            }

            public static HighlightElement FromVector(Vector arg)
            {
                return new HighlightElement(arg.Peak, arg.Group);
            }
        };

        /// <summary>
        /// The cluster
        /// </summary>
        public Cluster Cluster;

        /// <summary>
        /// Actual element the cluster represents (cluster, pathway, metabolite, etc.)
        /// </summary>
        public IVisualisable ActualElement;

        /// <summary>
        /// Colours for the representative peaks.
        /// </summary>
        public Dictionary<Peak, LineInfo> Colours;

        /// <summary>
        /// Is a fake cluster (e.g. pathway or metabolite)
        /// Tells UI not to bother finding the cluster in the list.
        /// </summary>
        public bool IsFake;

        /// <summary>
        /// Is image for preview? (i.e. draw minimal data)
        /// </summary>
        public bool IsPreview;

        /// <summary>
        /// If any of the peak within should be highlighted in red
        /// </summary>
        public IEnumerable<HighlightElement> Highlight;

        /// <summary>
        /// Caption
        /// </summary>
        public string CaptionFormat;

        /// <summary>
        /// Source (for caption)
        /// </summary>
        public IVisualisable Source;

        /// <summary>
        /// Ctor.
        /// </summary>
        public StylisedCluster(Cluster cluster)
        {
            this.Cluster = cluster;
            this.ActualElement = cluster;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public StylisedCluster(Cluster cluster, IVisualisable actualElement, Dictionary<Peak, LineInfo> colours)
        {
            this.Cluster = cluster;
            this.ActualElement = actualElement;
            this.Colours = colours;
        }

        /// <summary>
        /// Debugging
        /// </summary>           
        public override string ToString()
        {
            return Cluster.ToString() + " - " + ActualElement.ToString();
        }
    }
}
