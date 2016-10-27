using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Data.Session.Definition
{
    /// <summary>
    /// Reference to a compound library, part of <see cref="DataFileNames"/>.
    /// </summary>
    [Serializable]
    class CompoundLibrary
    {
        public readonly string Name;
        public readonly string PathwayToolsFolder;
        public readonly string CompoundFile;
        public readonly string PathwayFile;

        public CompoundLibrary(string name, string pathwayToolsFolder)
        {
            this.Name = name;
            this.PathwayToolsFolder = pathwayToolsFolder;
        }

        public CompoundLibrary(string name, string compoundCsv, string pathwayCsv)
        {
            this.Name = name;
            this.CompoundFile = compoundCsv;
            this.PathwayFile = pathwayCsv;
        }

        /// <summary>
        /// Used in lists
        /// </summary>
        public override string ToString()
        {
            return this.Name;
        }

        public bool ContentsMatch(CompoundLibrary cl)
        {
            return this.PathwayToolsFolder == cl.PathwayToolsFolder && this.CompoundFile == cl.CompoundFile && this.PathwayFile == cl.PathwayFile;
        }
    }
}
