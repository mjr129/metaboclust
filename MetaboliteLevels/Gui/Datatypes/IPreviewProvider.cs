using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Gui.Datatypes
{
    /// <summary>
    /// Classes able to provide previews for IVisualisable derivatives.
    /// </summary>
    interface IPreviewProvider
    {
        /// <summary>
        /// Provides a preview for the specified IVisualisable.
        /// </summary>
        Image ProvidePreview( Size size, object target );
    }
}
