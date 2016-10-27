using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Definition
{
    [Serializable]
    class DataFileNames
    {
        // files
        public string Data;                             // data file
        public string ObservationInfo;                  // obs info
        public string PeakInfo;                         // peak info
        public List<CompoundLibrary> CompoundLibraies;  // pathways file
        public List<string> AdductLibraries;            // adducts file
        public string Identifications;                  // annotations file
        public string AltData;                          // alt data file
        public string ConditionInfo;                    // condition info file

        // modes
        public ELcmsMode LcmsMode;               // lc-ms mode
        public bool AutomaticIdentifications;   // for annotations - whether to auto-id
        public EAnnotation AutomaticIdentificationsStatus;   // for annotations - initial status
        public EAnnotation ManualIdentificationsStatus;   // for annotations - default status
        public bool PeakPeakMatching;   // for annotations - whether to compare peaks to peaks
        public decimal AutomaticIdentificationsToleranceValue; // for annotations - m/z tolerance
        public ETolerance AutomaticIdentificationsToleranceUnit; // for annotations - m/z tolerance unit

        // stats
        public List<string> ConditionsOfInterestString;  // for stats - conditions of interest
        public List<string> ControlConditionsString;  // for stats - conditions of interest
        public EAutoCreateOptions _standardAutoCreateOptions;  // for stats - what methods to apply

        // session info
        public string Title;                    // session title
        public string ShortTitle;               // session short title (optional)
        public string Comments;                 // session comments

        // session file
        public string Session;                  // session (saved data)
        public bool ForceSaveAs;                // only allow "save as" not "save" (set if saving might lose data)
        public bool? SaveOnClose;               // save automatically on close
        public Version AppVersion;              // version of app saved using      

        /// <summary>
        /// Gets the title
        /// </summary>
        internal string GetDescription()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
            {
                return "Untitled session";
            }

            return this.Title;
        }

        /// <summary>
        /// String summary.
        /// </summary>
        /// <returns></returns>
        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("--- Application information ---");
            sb.AppendLine("Title: " + UiControls.Title);
            sb.AppendLine("Version: " + UiControls.Version);

            sb.AppendLine("--- Session information ---");
            sb.AppendLine("Title: " + this.Title);
            sb.AppendLine("ShortTitle: " + this.ShortTitle);
            sb.AppendLine("Comments: " + this.ShortTitle);

            sb.AppendLine("--- Session file ---");
            sb.AppendLine("Session: " + this.Session);
            sb.AppendLine("File Version: " + this.AppVersion);
            sb.AppendLine("Version warning: " + this.ForceSaveAs);

            sb.AppendLine("--- Data files ---");
            sb.AppendLine("LcmsMode: " + this.LcmsMode);
            sb.AppendLine("Data: " + this.Data);
            sb.AppendLine("ObservationInfo: " + this.ObservationInfo);
            sb.AppendLine("PeakInfo: " + this.PeakInfo);
            sb.AppendLine("AltData: " + this.AltData);

            sb.AppendLine("--- Compound information ---");
            sb.AppendLine("Compounds: " + StringHelper.ArrayToString(this.CompoundLibraies));
            sb.AppendLine("Adducts: " + StringHelper.ArrayToString(this.AdductLibraries));
            sb.AppendLine("Identifications: " + this.Identifications);
            sb.AppendLine("AutomaticIdentifications: " + this.AutomaticIdentifications);

            sb.AppendLine("--- Experimental information ---");
            sb.AppendLine("ConditionInfo: " + this.ConditionInfo);
            sb.AppendLine("ConditionsOfInterest: " + StringHelper.ArrayToString(this.ConditionsOfInterestString));
            sb.AppendLine("ControlConditions: " + StringHelper.ArrayToString(this.ControlConditionsString));
            sb.AppendLine("_standardAutoCreateOptions: " + this._standardAutoCreateOptions.ToString());

            return sb.ToString();
        }

        internal string GetShortTitle()
        {
            if (!string.IsNullOrWhiteSpace(this.ShortTitle))
            {
                return this.ShortTitle;
            }
            else if (this.Title.Length <= 24)
            {
                return this.Title;
            }
            else if (this.Title.Contains(' '))
            {
                return this.Title.Substring(0, this.Title.LastIndexOf(' ', 24));
            }
            else
            {
                return this.Title.Substring(0, 24);
            }
        }
    }

    enum EDefaultTrendGenerator
    {
        [Name("None (recommended)")]
        None,

        [Name( "Median of replicates" )]
        MedianOfReplicates,

        [Name( "Mean of replicates" )]
        MeanOfReplicates,
    }
}
