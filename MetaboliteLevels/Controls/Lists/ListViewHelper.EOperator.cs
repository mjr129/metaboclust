using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Viewers.Lists
{
    abstract partial class ListVieweHelper
    {
        public enum EOperator
        {
            [Name("contains")]
            TextContains,

            [Name("does not contain")]
            TextDoesNotContain,

            [Name("regex")]
            Regex,

            [Name("=")]
            EqualTo,

            [Name("≠")]
            NotEqualTo,

            [Name("<")]
            LessThan,

            [Name("≤")]
            LessThanEq,

            [Name(">")]
            MoreThan,

            [Name("≥")]
            MoreThanEq,
        }
    }
}
