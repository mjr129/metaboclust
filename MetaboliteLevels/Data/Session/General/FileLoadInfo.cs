using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.Session.General
{
    internal sealed class FileLoadInfo
    {
        public readonly string[] OBSFILE_TIME_HEADER = { "time", "day", "t" };
        public readonly string[] OBSFILE_REPLICATE_HEADER = { "rep", "replicate" };
        public readonly string[] OBSFILE_GROUP_HEADER = { "type", "group", "condition", "conditions", "class" };
        public readonly string[] OBSFILE_BATCH_HEADER = { "batch" };
        public readonly string[] OBSFILE_ACQUISITION_HEADER = { "acquisition", "order", "file", "index", "acquisition order" };
        public readonly string[] IDFILE_PEAK_HEADER = { "name", "peak", "variable" };
        public readonly string[] IDFILE_STATUS_HEADER = { "status", "confirmation" };
        public readonly string[] IDFILE_COMPOUNDS_HEADER = { "id", "annotation", "ids", "annotations", "compounds", "compound" };
        public readonly string[] ADDUCTFILE_NAME_HEADER = { "name" };
        public readonly string[] ADDUCTFILE_CHARGE_HEADER = { "charge", "z" };
        public readonly string[] ADDUCTFILE_MASS_DIFFERENCE_HEADER = { "mass.difference" };
        public readonly string[] PATHWAYFILE_NAME_HEADER = { "name" };
        public readonly string[] PATHWAYFILE_FRAME_ID_HEADER = { "frame.id", "id" };
        public readonly string[] COMPOUNDFILE_NAME_HEADER = { "name" };
        public readonly string[] COMPOUNDFILE_FRAME_ID_HEADER = { "frame.id", "id" };
        public readonly string[] COMPOUNDFILE_MASS_HEADER = { "mass", "m" };
        public readonly string[] COMPOUNDFILE_PATHWAYS_HEADER = { "pathways", "pathway", "pathway.ids", "pathway.id" };
        public readonly string[] CONDITIONFILE_ID_HEADER = { "id", "frame.id" };
        public readonly string[] CONDITIONFILE_NAME_HEADER = { "name" };
        public readonly string[] VARFILE_MZ_HEADER = { "mz", "m/z" };
        public readonly string[] VARFILE_RT_HEADER = { "rt", "r.t.", "r.t", "rt.", "ret. time", "retention", "retention time" };
        public readonly string[] VARFILE_MODE_HEADER = { "mode", "lcmsmode", "lcms", "lcms.mode" };
        public readonly string[] AUTOFILE_OBSERVATIONS = { "Info.csv", "ObsInfo.csv", "ObservationInfo.csv", "Observations.csv", "*.jgf" };
        public readonly string[] AUTOFILE_PEAKS = { "VarInfo.csv", "PeakInfo.csv", "FeatureInfo.csv", "Peaks.csv", "Variables.csv", "Features.csv" };

        public readonly char SpreadsheetDelimiter = ',';
        public readonly char SpreadsheetOpenQuote = '\"';
        public readonly char SpreadsheetCloseQuote = '\"';

        public SpreadsheetReader GetReader()
        {
            return new SpreadsheetReader()
            {
                Delimiter = this.SpreadsheetDelimiter,
                OpenQuote = this.SpreadsheetOpenQuote,
                CloseQuote = this.SpreadsheetCloseQuote,
            };
        }
    }
}
