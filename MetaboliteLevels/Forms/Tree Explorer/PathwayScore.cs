using System;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Forms.Tree_Explorer
{
    class PathwayScore : IComparable<PathwayScore>
    {
        public readonly Pathway pathway;
        public readonly int compoundScore;
        public readonly int variableScore;
        private int scoreForSorting;

        public PathwayScore(Pathway pathway, int compoundScore, int variableScore, int scoreForSorting)
        {
            this.pathway = pathway;
            this.compoundScore = compoundScore;
            this.variableScore = variableScore;
            this.scoreForSorting = scoreForSorting;
        }

        int IComparable<PathwayScore>.CompareTo(PathwayScore other)
        {
            return other.scoreForSorting.CompareTo(this.scoreForSorting);
        }
    }
}
