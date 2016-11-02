using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Properties;
using MGui.Datatypes;
using MGui.Helpers;
using RDotNet;

namespace MetaboliteLevels.Data.Algorithms.General
{
    class FromStringArgs
    {
        public readonly string Text;
        public readonly Core Core;
        public string Error;

        public FromStringArgs( Core core, string text )
        {
            this.Core = core;
            this.Text = text;
        }
    }  
}
