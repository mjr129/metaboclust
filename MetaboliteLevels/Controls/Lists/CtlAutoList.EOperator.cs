using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Controls.Lists
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
