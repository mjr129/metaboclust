using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Utilities
{
    static class StrRes
    {
        public static string MissingSourceDetails( object sel )
        {
            return $"The algorithm generating the source matrix, {{{sel}}}, has not yet been run. Either preview your algorithm using a different source, or commit the source matrix first.";
        }

        public static string NoPreview = "No preview is available because the source data has not yet been created.";
        public static string PreviewError = "An error occured whilst generating the preview, click here for details.";
    }
}
