using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.DataLoader;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System.IO;
using MetaboliteLevels.Controls;
using System.Threading;

namespace MetaboliteLevels.Forms.Startup
{
    /// <summary>
    /// Performs loading of the dataset / session.
    /// </summary>
    public partial class FrmDataLoad : Form, IProgressReceiver
    {
        // column headers
        private const string OBSFILE_TIME_HEADER = "time,day,t";
        private const string OBSFILE_REPLICATE_HEADER = "rep,replicate";
        private const string OBSFILE_GROUP_HEADER = "type,group,condition,conditions";
        private const string OBSFILE_BATCH_HEADER = "batch";
        private const string OBSFILE_ACQUISITION_HEADER = "acquisition,order,file,index";
        private const string IDFILE_PEAK_HEADER = "name,peak,variable";
        private const string IDFILE_COMPOUNDS_HEADER = "id,annotation,ids,annotations,compounds,compound";
        private const string ADDUCTFILE_NAME_HEADER = "name";
        private const string ADDUCTFILE_CHARGE_HEADER = "charge,z";
        private const string ADDUCTFILE_MASS_DIFFERENCE_HEADER = "mass.difference";
        private const string PATHWAYFILE_NAME_HEADER = "name";
        private const string PATHWAYFILE_FRAME_ID_HEADER = "frame.id,id";
        private const string COMPOUNDFILE_NAME_HEADER = "name";
        private const string COMPOUNDFILE_FRAME_ID_HEADER = "frame.id,id";
        private const string COMPOUNDFILE_MASS_HEADER = "mass,m";
        private const string COMPOUNDFILE_PATHWAYS_HEADER = "pathways,pathway,pathway.ids,pathway.id";
        private const string CONDITIONFILE_ID_HEADER = "id,frame.id";
        private const string CONDITIONFILE_NAME_HEADER = "name";
        private const string VARFILE_MZ_HEADER = "mz";
        private const string VARFILE_MODE_HEADER = "mode,lcmsmode,lcms,lcms.mode";

        private readonly DataFileNames _fileNames; // files to load...
        private readonly string _sessionFileName; // ... OR session to load

        private Core _result; // the result

        private const string ALT_NAMES_KEY = "Alt. Names";
        private ProgressReporter _prog;

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmDataLoad()
        {
            InitializeComponent();
            UiControls.SetIcon(this);

            label1.Text = UiControls.Title;
            label2.Text = UiControls.Description;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private FrmDataLoad(DataFileNames fileNames, string sessionFileName)
            : this()
        {
            this._fileNames = fileNames;
            this._sessionFileName = sessionFileName;

            UiControls.CompensateForVisualStyles(this);
        }

        /// <summary>
        /// Loads data from file
        /// </summary>
        internal static Core Show(Form owner, DataFileNames fileNames)
        {
            return Show(owner, fileNames, null);
        }

        /// <summary>
        /// Loads previous session
        /// </summary>
        internal static Core Show(Form owner, string sessionFileName)
        {
            return Show(owner, null, sessionFileName);
        }

        /// <summary>
        /// Loads data from file or session
        /// </summary>
        private static Core Show(Form owner, DataFileNames fileNames, string sessionFileName)
        {
            UiControls.Assert((fileNames == null) ^ (sessionFileName == null), "Specify filenames to load or session to load.");

            owner.Hide();

            using (FrmDataLoad frm = new FrmDataLoad(fileNames, sessionFileName))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    owner.Show();
                    return frm._result;
                }

                owner.Show();
                return null;
            }
        }

        /// <summary>
        /// OVERRIDE
        /// Start loading on form load.
        /// </summary>            
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// Meat of the loading (async).
        /// </summary>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _prog = new ProgressReporter(this);

            // LOAD FROM SESSION?
            if (_sessionFileName != null)
            {
                _prog.Enter("Loading session");
                _result = Core.Load(_sessionFileName, _prog);
                _prog.Leave();
                return;
            }

            // CREATE A NEW CORE
            List<Compound> compounds = new List<Compound>();
            List<Pathway> pathways = new List<Pathway>();
            List<Adduct> adducts = new List<Adduct>();
            MetaInfoHeader pathwayHeader = new MetaInfoHeader();
            MetaInfoHeader compoundHeader = new MetaInfoHeader();
            MetaInfoHeader adductsHeader = new MetaInfoHeader();
            MetaInfoHeader annotationsHeader = new MetaInfoHeader();

            // DATA SETS
            DataSet data = Load_1_DataSets(_fileNames, _prog);

            // COMPOUNDS & PATHWAYS
            Load_2_Compounds(compounds, pathways, pathwayHeader, compoundHeader, _fileNames.CompoundLibraies, _prog);

            // ADDUCTS
            Load_3_Adducts(adducts, _fileNames.AdductLibraries, adductsHeader, _prog);

            // M/Zs
            Load_4_MatchMzs(data.Peaks, adducts, compounds, _prog);

            // IDENTIFICATIONS
            if (!string.IsNullOrEmpty(_fileNames.Identifications))
            {
                Load_5_UserIdentifications(annotationsHeader, data.Peaks, compounds, adducts, _fileNames.Identifications, _prog);
            }

            // Set result
            _result = new Core(_fileNames, data, compounds, pathways, compoundHeader, pathwayHeader, adducts, adductsHeader, annotationsHeader);

            // STATISTICS
            Load_6_CalculateDefaultStatistics(_result, _fileNames.StandardStatisticalMethods.HasFlag(EStatisticalMethods.TTest), _fileNames.StandardStatisticalMethods.HasFlag(EStatisticalMethods.Pearson), _prog);
        }

        /// <summary>
        /// Loads identifications.
        /// </summary>
        private static void Load_5_UserIdentifications(MetaInfoHeader annotationMeta, IEnumerable<Peak> peaks, List<Compound> ccompounds, List<Adduct> adducts, string fileName, ProgressReporter prog)
        {
            prog.Enter("Loading identifications");
            Matrix<string> mat = new Matrix<string>(fileName, true, true, prog);
            prog.Leave();

            prog.Enter("Interpreting identifications");
            int peakCol = mat.OptionalColIndex(IDFILE_PEAK_HEADER);
            int mzCol = mat.OptionalColIndex(VARFILE_MZ_HEADER);
            int compoundsCol = mat.ColIndex(IDFILE_COMPOUNDS_HEADER);

            for (int row = 0; row < mat.NumRows; row++)
            {
                prog.SetProgress(row, mat.NumRows);

                string peakName;
                decimal mz = decimal.Zero;

                if (peakCol != -1)
                {
                    peakName = mat[row, peakCol];
                }
                else
                {
                    peakName = mat.RowNames[row];
                }

                if (mzCol != -1)
                {
                    mz = decimal.Parse(mat[row, mzCol]);
                }

                Peak peak = peaks.First(z => z.DefaultDisplayName == peakName);

                string[] compounds = mat[row, compoundsCol].Split(",".ToCharArray());

                foreach (string compoundName in compounds)
                {
                    Annotation annotation = null;

                    foreach (Compound compound in ccompounds)
                    {
                        if (compound.Id == compoundName || compound.DefaultDisplayName == compoundName)
                        {
                            annotation = new Annotation(peak, compound, CreateOrGetEmpty(adducts));
                            break;
                        }
                    }

                    if (annotation == null)
                    {
                        Compound newCompound = new Compound(null, compoundName, compoundName, mz);
                        ccompounds.Add(newCompound);
                        annotation = new Annotation(peak, newCompound, CreateOrGetEmpty(adducts));
                    }

                    mat.WriteMeta(row, annotation.Meta, annotationMeta);

                    annotation.Peak.Annotations.Add(annotation);
                    annotation.Compound.Annotations.Add(annotation);
                    annotation.Adduct.Annotations.Add(annotation);
                }
            }

            prog.Leave();
        }

        private static Adduct CreateOrGetEmpty(List<Adduct> adducts)
        {
            Adduct result = adducts.FirstOrDefault(z => z.IsEmpty);

            if (result == null)
            {
                result = Adduct.CreateEmpty();
                adducts.Add(result);
            }

            return result;
        }

        private static void Load_3_Adducts(List<Adduct> adducts, List<string> fileNames, MetaInfoHeader header, ProgressReporter prog)
        {
            for (int index = 0; index < fileNames.Count; index++)
            {
                string fileName = fileNames[index];
                prog.Enter("Loading adducts (" + index + " of " + fileNames.Count + ")");
                Matrix<string> mat = new Matrix<string>(fileName, true, true, prog);
                prog.Leave();

                prog.Enter("Interpreting adducts (" + index + " of " + fileNames.Count + ")");

                int nameCol = mat.ColIndex(ADDUCTFILE_NAME_HEADER);
                int chargeCol = mat.ColIndex(ADDUCTFILE_CHARGE_HEADER);
                int mzCol = mat.ColIndex(ADDUCTFILE_MASS_DIFFERENCE_HEADER);

                for (int row = 0; row < mat.NumRows; row++)
                {
                    prog.SetProgress(row, mat.NumRows);

                    Adduct a = new Adduct(mat[row, nameCol], int.Parse(mat[row, chargeCol]), decimal.Parse(mat[row, mzCol]));

                    mat.WriteMeta(row, a.MetaInfo, header);

                    adducts.Add(a);
                }

                prog.Leave();
            }
        }

        private static void Load_4_MatchMzs(List<Peak> peaks, List<Adduct> adducts, List<Compound> compounds, ProgressReporter prog)
        {
            prog.Enter("Matching peaks to compounds");

            for (int pi = 0; pi < peaks.Count; pi++)
            {
                prog.SetProgress(pi, peaks.Count);

                Peak p = peaks[pi];

                foreach (Adduct a in adducts)
                {
                    if ((int)p.LcmsMode == Math.Sign(a.Charge))
                    {
                        decimal tol = (p.Mz / 1000000) * 5;
                        decimal tmz = p.Mz - (a.Mz * Math.Abs(a.Charge));

                        foreach (Compound c in compounds)
                        {
                            if (c.Mass != 0)
                            {
                                if (c.Mass >= tmz - tol && c.Mass <= tmz + tol)
                                {
                                    Annotation annotation = new Annotation(p, c, a);
                                    p.Annotations.Add(annotation);
                                    c.Annotations.Add(annotation);
                                    a.Annotations.Add(annotation);
                                }
                            }
                        }
                    }
                }
            }

            prog.Leave();

            prog.Enter("Matching peaks to peaks");

            for (int pi = 0; pi < peaks.Count; pi++)
            {
                prog.SetProgress(pi, peaks.Count);

                Peak p = peaks[pi];

                if (p.Annotations.Count == 0)
                {
                    foreach (Peak p2 in peaks)
                    {
                        if (p2 != p)
                        {
                            decimal tol = (p2.Mz / 1000000) * 5;

                            if (p.Mz >= p2.Mz - tol && p.Mz <= p2.Mz + tol)
                            {
                                p.SimilarPeaks.Add(p2);
                            }
                        }
                    }
                }
            }

            prog.Leave();
        }

        internal static void GetCompoundLibraries(out List<CompoundLibrary> compounds, out List<NamedItem<string>> adducts)
        {
            compounds = new List<CompoundLibrary>();
            adducts = new List<NamedItem<string>>();

            string[] pots = Directory.GetDirectories(MainSettings.Instance.General.PathwayToolsDatabasesPath, "*", SearchOption.AllDirectories);

            foreach (string pot in pots)
            {
                string speciesFile = Path.Combine(pot, "species.dat");
                string compoundsFile = Path.Combine(pot, "Compounds.csv");
                string pathwaysFile = Path.Combine(pot, "Pathways.csv");
                string adductsFile = Path.Combine(pot, "Adducts.csv");
                string name;

                if (File.Exists(speciesFile))
                {
                    using (PathwayToolsReader sr = new PathwayToolsReader(speciesFile, null))
                    {
                        var ptSpecies = sr.ReadNext();
                        name = ptSpecies.GetFirst("COMMON-NAME");
                    }

                    CompoundLibrary cl = new CompoundLibrary(name, pot);
                    compounds.Add(cl);
                }

                if (File.Exists(compoundsFile))
                {
                    name = Path.GetFileName(Path.GetDirectoryName(compoundsFile));
                    CompoundLibrary cl = new CompoundLibrary(name, compoundsFile, pathwaysFile);
                    compounds.Add(cl);
                }

                if (File.Exists(adductsFile))
                {
                    name = Path.GetFileName(Path.GetDirectoryName(adductsFile));
                    NamedItem<string> cl = new NamedItem<string>(adductsFile, name);
                    adducts.Add(cl);
                }
            }
        }

        /// <summary>
        /// Loads a pathway tools database into the specified lists.
        /// </summary>
        private static void LoadPathwayToolsDatabase(List<Pathway> pathwaysOut, List<Compound> compoundsOut, MetaInfoHeader pathwayMeta, MetaInfoHeader compoundMeta, string databaseDirectory, CompoundLibrary tag, ProgressReporter prog)
        {
            string pathwayFile = Path.Combine(databaseDirectory, "pathways.dat");
            string compoundsFile = Path.Combine(databaseDirectory, "compounds.dat");
            string reactionsFile = Path.Combine(databaseDirectory, "reactions.dat");

            Dictionary<string, List<string>> reactions = new Dictionary<string, List<string>>();
            Dictionary<string, Compound> compounds = new Dictionary<string, Compound>();
            Dictionary<string, List<Compound>> classes = new Dictionary<string, List<Compound>>();
            Dictionary<string, Pathway> pathways = new Dictionary<string, Pathway>();
            List<Tuple<Pathway, string>> links = new List<Tuple<Pathway, string>>();

            // REACTIONS
            using (PathwayToolsReader sr = new PathwayToolsReader(reactionsFile, prog))
            {
                while (!sr.EndOfStream)
                {
                    var ptReaction = sr.ReadNext();
                    string id = ptReaction.GetFirst("UNIQUE-ID");
                    List<string> ptReactionCompounds = ptReaction.TryGetAll("LEFT", "RIGHT").ToList();

                    reactions.Add(id, ptReactionCompounds);
                }
            }

            // COMPOUNDS
            compounds.AddRange(compoundsOut, z => z.Id);

            using (PathwayToolsReader sr = new PathwayToolsReader(compoundsFile, prog))
            {
                while (!sr.EndOfStream)
                {
                    var ptCompound = sr.ReadNext();
                    string id = ptCompound.GetFirst("UNIQUE-ID");
                    string name = ptCompound.GetFirst("COMMON-NAME", id);
                    string masss = ptCompound.GetFirst("MONOISOTOPIC-MW", "0");
                    decimal mass = decimal.Parse(masss);
                    string type = ptCompound.GetFirst("TYPES");
                    Compound compound;

                    compound = AddWithoutConflict(compoundMeta, tag, compounds, id, name, mass);

                    ptCompound.WriteToMeta(compound.MetaInfo, compoundMeta);

                    classes.GetOrNew(type).Add(compound);
                }
            }

            // PATHWAYS
            pathways.AddRange(pathwaysOut, z => z.Id);

            using (PathwayToolsReader sr = new PathwayToolsReader(pathwayFile, prog))
            {
                while (!sr.EndOfStream)
                {
                    var ptPathway = sr.ReadNext();
                    string name = ptPathway.GetFirst("COMMON-NAME");
                    string id = ptPathway.GetFirst("UNIQUE-ID");
                    List<string> ptPathwayReactions = ptPathway.GetAll("REACTION-LIST");
                    List<Compound> ptPathwayCompounds = new List<Compound>();
                    Pathway pathway;

                    pathway = AddWithoutConflict(pathwayMeta, tag, pathways, id, name);

                    ptPathway.WriteToMeta(pathway.MetaInfo, pathwayMeta);

                    foreach (string ptPathwayReaction in ptPathwayReactions)
                    {
                        List<string> reactionCompounds;

                        if (reactions.TryGetValue(ptPathwayReaction, out reactionCompounds))
                        {
                            foreach (string reactionCompound in reactionCompounds)
                            {
                                string failureCompound = null;

                                if (reactionCompound.StartsWith("|") && reactionCompound.EndsWith("|"))
                                {
                                    // Pathway contains CLASS
                                    string classid = reactionCompound.Substring(1, reactionCompound.Length - 2);
                                    List<Compound> classCompounds;

                                    if (classes.TryGetValue(classid, out classCompounds))
                                    {
                                        ptPathwayCompounds.AddRange(classCompounds);
                                    }
                                    else
                                    {
                                        failureCompound = "class ID: " + classid;
                                    }
                                }
                                else if (compounds.ContainsKey(reactionCompound))
                                {
                                    // Pathway contains COMPOUND
                                    Compound c;

                                    if (compounds.TryGetValue(reactionCompound, out c))
                                    {
                                        ptPathwayCompounds.Add(c);
                                    }
                                    else
                                    {
                                        failureCompound = "compound ID: " + reactionCompound;
                                    }
                                }

                                // Pathway contains something MISSING
                                if (failureCompound != null)
                                {
                                    Compound c;
                                    failureCompound = "Missing " + failureCompound;

                                    if (compounds.TryGetValue(reactionCompound, out c))
                                    {
                                        ptPathwayCompounds.Add(c);
                                    }
                                    else
                                    {
                                        c = new Compound(tag, failureCompound, reactionCompound, decimal.Zero);
                                        compounds.Add(c.Id, c);
                                        ptPathwayCompounds.Add(c);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Another pathway
                            links.Add(new Tuple<Pathway, string>(pathway, ptPathwayReaction));
                        }
                    }

                    foreach (Compound comp in ptPathwayCompounds)
                    {
                        if (!pathway.Compounds.Contains(comp))
                        {
                            pathway.Compounds.Add(comp);
                        }

                        if (!comp.Pathways.Contains(pathway))
                        {
                            comp.Pathways.Add(pathway);
                        }
                    }
                }
            }

            // Pathway -> pathway mappings
            foreach (var t in links)
            {
                Pathway p2 = pathways[t.Item2];
                t.Item1.Compounds.AddRange(p2.Compounds);
                t.Item1.RelatedPathways.Add(p2);
            }

            compoundsOut.ReplaceAll(compounds.Values);
            pathwaysOut.ReplaceAll(pathways.Values);
        }

        private static Compound AddWithoutConflict(MetaInfoHeader header, CompoundLibrary tag, Dictionary<string, Compound> compounds, string id, string name, decimal mass)
        {
            Compound compound;

            if (compounds.TryGetValue(id, out compound))
            {
                if (compound.DefaultDisplayName != name)
                {
                    Debug.WriteLine("Warning: Compound with ID \"" + id + "\" has conflicting name \"" + compound.DefaultDisplayName + "\" vs \"" + name + "\" in another database.");

                    if (header.GetValue(compound.MetaInfo, ALT_NAMES_KEY).IsEmpty())
                    {
                        compound.MetaInfo.Write(header, ALT_NAMES_KEY, compound.DefaultDisplayName + ((compound.Libraries.Count != 0) ? (" [from: " + compound.Libraries[0].ToString() + "]") : ""));
                    }

                    compound.MetaInfo.Write(header, ALT_NAMES_KEY, name + ((tag != null) ? (" [from: " + tag + "]") : ""));
                }

                if (compound.Mass != mass && mass != decimal.Zero)
                {
                    if (compound.Mass == decimal.Zero)
                    {
                        compound.Mass = mass;
                    }
                    else
                    {
                        decimal diff = Math.Abs(compound.Mass - mass);

                        const decimal MASS_TOL = 0.00000000000020m;

                        if (diff > MASS_TOL)
                        {
                            Debug.WriteLine("CRITICAL WARNING: Compound with ID \"" + id + "\" has conflicting mass " + compound.Mass + " vs "
                                                                + mass + " (difference of " + diff + ") in another database.");
                        }
                        else
                        {
                            Debug.WriteLine("Warning: Compound with ID \"" + id + "\" has conflicting mass " + compound.Mass + " vs " + mass
                                            + " (difference of " + diff + ") in another database.");
                        }

                        const string ALT_MASSES_KEY = "Alt. Masses";

                        if (header.GetValue(compound.MetaInfo, ALT_MASSES_KEY).IsEmpty())
                        {
                            compound.MetaInfo.Write(header, ALT_MASSES_KEY,
                                                    compound.Mass.ToString()
                                                    + ((compound.Libraries.Count != 0) ? (" [from: " + compound.Libraries[0].ToString() + "]") : ""));
                        }

                        compound.MetaInfo.Write(header, ALT_MASSES_KEY, compound.Mass.ToString() + ((tag != null) ? (" [from: " + tag + "]") : ""));
                    }
                }

                compound.Libraries.Add(tag);
            }
            else
            {
                compound = new Compound(tag, name, id, mass);
                compounds.Add(id, compound);
            }

            return compound;
        }

        private static Pathway AddWithoutConflict(MetaInfoHeader header, CompoundLibrary tag, Dictionary<string, Pathway> pathways, string id, string name)
        {
            Pathway pathway;

            if (pathways.TryGetValue(id, out pathway))
            {
                if (pathway.DefaultDisplayName != name)
                {
                    Debug.WriteLine("Warning: Pathway with ID \"" + id + "\" has conflicting name \"" + pathway.DefaultDisplayName + "\" vs \"" + name + "\" in another database.");

                    if (header.GetValue(pathway.MetaInfo, ALT_NAMES_KEY).IsEmpty())
                    {
                        pathway.MetaInfo.Write(header, ALT_NAMES_KEY, pathway.DefaultDisplayName + ((pathway.Libraries.Count != 0) ? (" [from: " + pathway.Libraries[0].ToString() + "]") : ""));
                    }

                    pathway.MetaInfo.Write(header, ALT_NAMES_KEY, name + ((tag != null) ? (" [from: " + tag + "]") : ""));
                }

                pathway.Libraries.Add(tag);
            }
            else
            {
                pathway = new Pathway(tag, name, id);
                pathways.Add(id, pathway);
            }

            return pathway;
        }

        private static void Load_2_Compounds(List<Compound> compounds, List<Pathway> pathwaysList, MetaInfoHeader pathwayMeta, MetaInfoHeader compoundMeta, List<CompoundLibrary> toLoad, ProgressReporter prog)
        {
            for (int index = 0; index < toLoad.Count; index++)
            {
                prog.Enter("Loading compound library (" + index + " of " + toLoad.Count + ")...");

                var cl = toLoad[index];

                if (cl.PathwayToolsFolder != null)
                {
                    LoadPathwayToolsDatabase(pathwaysList, compounds, pathwayMeta, compoundMeta, cl.PathwayToolsFolder, cl, prog);
                }
                else
                {
                    LoadCsvDatabase(compounds, pathwaysList, pathwayMeta, compoundMeta, cl.CompoundFile, cl.PathwayFile, cl, prog);
                }

                prog.Leave();
            }
        }

        private static void LoadCsvDatabase(List<Compound> compoundsOut, List<Pathway> pathwaysList, MetaInfoHeader pathwayMeta, MetaInfoHeader compoundMeta, string compFile, string patFile, CompoundLibrary tag, ProgressReporter prog)
        {
            Dictionary<string, Pathway> pathways = new Dictionary<string, Pathway>();

            pathways.AddRange(pathwaysList, z => z.Id);

            // Pathways
            {
                Matrix<string> pathwayMatrix = new Matrix<string>(patFile, true, true, prog);

                int nameCol = pathwayMatrix.ColIndex(PATHWAYFILE_NAME_HEADER);
                int idCol = pathwayMatrix.ColIndex(PATHWAYFILE_FRAME_ID_HEADER);

                for (int row = 0; row < pathwayMatrix.NumRows; row++)
                {
                    prog.SetProgress(row, pathwayMatrix.NumRows);

                    string name = pathwayMatrix[row, nameCol];
                    string id = pathwayMatrix[row, idCol];

                    Pathway p = AddWithoutConflict(pathwayMeta, tag, pathways, id, name);

                    pathwayMatrix.WriteMeta(row, p.MetaInfo, pathwayMeta);
                }
            }

            Dictionary<string, Compound> compounds = new Dictionary<string, Compound>();

            compounds.AddRange(compoundsOut, z => z.Id);

            // Compounds
            {
                Matrix<string> mat = new Matrix<string>(compFile, true, true, prog);

                int nameCol = mat.ColIndex(COMPOUNDFILE_NAME_HEADER);
                int idCol = mat.ColIndex(COMPOUNDFILE_FRAME_ID_HEADER);
                int mzCol = mat.ColIndex(COMPOUNDFILE_MASS_HEADER);
                int pathwayCol = mat.ColIndex(COMPOUNDFILE_PATHWAYS_HEADER);

                for (int row = 0; row < mat.NumRows; row++)
                {
                    prog.SetProgress(row, mat.NumRows);

                    string id = mat[row, idCol];
                    string name = mat[row, nameCol];
                    decimal mass = mat[row, mzCol] != "" ? decimal.Parse(mat[row, mzCol]) : 0m;

                    Compound compound = AddWithoutConflict(compoundMeta, tag, compounds, id, name, mass);

                    mat.WriteMeta(row, compound.MetaInfo, compoundMeta);

                    string[] pathwaysForCompound = mat[row, pathwayCol].Split(",".ToCharArray());

                    foreach (string pathwayName in pathwaysForCompound) // note: this can be blank if the string is empty
                    {
                        Pathway pathway;

                        if (!pathways.TryGetValue(pathwayName, out pathway))
                        {
                            pathway = new Pathway(tag, pathwayName, pathwayName);
                            pathways.Add(pathwayName, pathway);
                        }

                        compound.Pathways.Add(pathway);
                        pathway.Compounds.Add(compound);
                    }
                }
            }

            compoundsOut.ReplaceAll(compounds.Values);
            pathwaysList.ReplaceAll(pathways.Values);
        }

        public static Dictionary<int, string> LoadConditionInfo(string fileName)
        {
            return LoadConditionInfo(fileName, null);
        }

        private static Dictionary<int, string> LoadConditionInfo(string fileName, ProgressReporter reportProgress)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();

            Matrix<string> mat = new Matrix<string>(fileName, true, true, reportProgress);

            output.Clear();

            int idCol = mat.ColIndex(CONDITIONFILE_ID_HEADER);
            int nameCol = mat.ColIndex(CONDITIONFILE_NAME_HEADER);

            for (int row = 0; row < mat.NumRows; row++)
            {
                output.Add(int.Parse(mat[row, idCol]), mat[row, nameCol]);
            }

            return output;
        }

        internal class DataSet
        {
            public List<ObservationInfo> Observations;
            public List<ConditionInfo> Conditions;
            public List<GroupInfo> Types;
            public List<BatchInfo> Batches;
            public ConfigurationTrend AvgSmoother;
            public ConfigurationTrend MinSmoother;
            public ConfigurationTrend MaxSmoother;
            public List<Peak> Peaks;
            public MetaInfoHeader PeakMetaHeader;
        }

        private static DataSet Load_1_DataSets(DataFileNames dfn, ProgressReporter prog)
        {
            DataSet result = new DataSet();
            result.PeakMetaHeader = new MetaInfoHeader();

            // Assertions
            UiControls.Assert(!string.IsNullOrEmpty(dfn.Data), "Missing data file.");
            UiControls.Assert(!string.IsNullOrEmpty(dfn.ObservationInfo), "Missing data file.");
            UiControls.Assert(!string.IsNullOrEmpty(dfn.PeakInfo), "Missing data file.");
            UiControls.Assert(dfn.ConditionsOfInterest.Count != 0, "Missing experimental conditions.");

            // Condition names
            Dictionary<int, string> conditionNames;

            if (!string.IsNullOrEmpty(dfn.ConditionInfo))
            {
                prog.Enter("Loading conditions");
                conditionNames = LoadConditionInfo(dfn.ConditionInfo, prog);
                prog.Leave();
            }
            else
            {
                conditionNames = null;
            }

            // Load data
            prog.Enter("Loading intensities");
            Matrix<double> data = new Matrix<double>(dfn.Data, true, true, prog);
            prog.Leave();

            prog.Enter("Loading alt. intensities");
            Matrix<double> altData = !string.IsNullOrWhiteSpace(dfn.AltData) ? new Matrix<double>(dfn.AltData, true, true, prog) : null;
            prog.Leave();

            prog.Enter("Loading observations");
            Matrix<int> info = new Matrix<int>(dfn.ObservationInfo, true, true, prog);
            prog.Leave();

            prog.Enter("Loading peaks");
            Matrix<string> varInfo = new Matrix<string>(dfn.PeakInfo, true, true, prog);
            prog.Leave();

            prog.Enter("Formatting data");

            // Get "obsinfo" columns
            int dayCol = info.OptionalColIndex(OBSFILE_TIME_HEADER);
            int repCol = info.OptionalColIndex(OBSFILE_REPLICATE_HEADER);
            int typeCol = info.OptionalColIndex(OBSFILE_GROUP_HEADER);
            int batchCol = info.OptionalColIndex(OBSFILE_BATCH_HEADER);
            int acquisitionCol = info.OptionalColIndex(OBSFILE_ACQUISITION_HEADER);

            // Get "peakinfo" columns specific to LCMS mode
            int mzCol;
            int lcmsModeCol;
            switch (dfn.LcmsMode)
            {
                case ELcmsMode.None:
                    mzCol = -1;
                    lcmsModeCol = -1;
                    break;

                case ELcmsMode.Positive:
                case ELcmsMode.Negative:
                    mzCol = varInfo.ColIndex(VARFILE_MZ_HEADER);
                    lcmsModeCol = -1;
                    break;

                case ELcmsMode.Mixed:
                    mzCol = varInfo.ColIndex(VARFILE_MZ_HEADER);
                    lcmsModeCol = varInfo.ColIndex(VARFILE_MODE_HEADER);
                    break;

                default:
                    throw new InvalidOperationException("LC-MS mode hasn't been specified.");
            }

            // Create our TYPE array
            var types = new List<GroupInfo>();
            var typesById = new Dictionary<int, GroupInfo>();
            var batches = new List<BatchInfo>();
            var batchesById = new Dictionary<int, BatchInfo>();

            {
                List<int> typeIds = new List<int>();
                Dictionary<int, Range> typeRanges = new Dictionary<int, Range>();

                List<int> batchIds = new List<int>();
                Dictionary<int, Range> batchRanges = new Dictionary<int, Range>();

                for (int oId = 0; oId < info.NumRows; oId++) // obs info
                {
                    int day = dayCol != -1 ? info[oId, dayCol] : 0;
                    int typeId = typeCol != -1 ? info[oId, typeCol] : 0;
                    int acquis = acquisitionCol != -1 ? info[oId, acquisitionCol] : 0;
                    int batchId = batchCol != -1 ? info[oId, batchCol] : 0;

                    // Add type (if not already)
                    if (!typeIds.Contains(typeId))
                    {
                        typeIds.Add(typeId);
                        typeRanges.Add(typeId, Range.MaxValue);
                    }

                    // Add batch (if not already)
                    if (!batchIds.Contains(batchId))
                    {
                        batchIds.Add(batchId);
                        batchRanges.Add(batchId, Range.MaxValue);
                    }

                    typeRanges[typeId] = typeRanges[typeId].ExpandOrStart(day);
                    batchRanges[batchId] = batchRanges[batchId].ExpandOrStart(acquis);
                }

                for (int n = 0; n < typeIds.Count; n++)
                {
                    int id = typeIds[n];
                    string name;
                    string shortName;

                    if (conditionNames != null && conditionNames.TryGetValue(id, out name))
                    {
                        shortName = name.Substring(0, 1).ToUpper();
                    }
                    else
                    {
                        name = "Type " + id;
                        shortName = "T" + id;
                    }

                    var ti = new GroupInfo(id, n, typeRanges[id], name, shortName, GetTypeColor(n, false), GetTypeColor(n, true));
                    types.Add(ti);
                    typesById.Add(id, ti);
                }

                for (int n = 0; n < batchIds.Count; n++)
                {
                    int id = batchIds[n];
                    var bi = new BatchInfo(id, n, batchRanges[id]);
                    batches.Add(bi);
                    batchesById.Add(id, bi);
                }
            }

            // Create our arrays of { observations, conditions, types }
            List<ObservationInfo> observations = new List<ObservationInfo>();
            List<ConditionInfo> conditions = new List<ConditionInfo>();

            for (int oId = 0; oId < info.NumRows; oId++) // obs info
            {
                int day = dayCol != -1 ? info[oId, dayCol] : 0;
                int repId = repCol != -1 ? info[oId, repCol] : 0;
                int typeId = typeCol != -1 ? info[oId, typeCol] : 0;
                int batchId = batchCol != -1 ? info[oId, batchCol] : 0;
                int acquisition = acquisitionCol != -1 ? info[oId, acquisitionCol] : 0;

                // Add condition (if not already)
                ConditionInfo ci = conditions.FirstOrDefault(λ => λ.Time == day && λ.Group.Id == typeId);

                if (ci == null)
                {
                    ci = new ConditionInfo(day, typesById[typeId]);
                    conditions.Add(ci); // all conditions
                }

                // Add observation
                observations.Add(new ObservationInfo(ci, repId, batchesById[batchId], acquisition));
            }

            result.Observations = observations;
            result.Conditions = conditions;
            result.Types = types;
            result.Batches = batches;

            // Create smoothers
            result.AvgSmoother = new ConfigurationTrend(null, null, Algo.ID_TREND_FLAT_MEAN, new ArgsTrend(new object[0]));
            result.MinSmoother = new ConfigurationTrend(null, null, Algo.ID_TREND_MOVING_MINIMUM, new ArgsTrend(new object[] { 0 }));
            result.MaxSmoother = new ConfigurationTrend(null, null, Algo.ID_TREND_MOVING_MAXIMUM, new ArgsTrend(new object[] { 0 }));

            // Create our array of peaks
            result.Peaks = new List<Peak>();

            for (int peakIndex = 0; peakIndex < data.NumCols; peakIndex++)
            {
                // Feedback
                prog.SetProgress(peakIndex, data.NumCols);
                UiControls.Assert(data.ColNames[peakIndex] == varInfo.RowNames[peakIndex], "Data order mismatch error. The data file contains observation \"" + data.ColNames[peakIndex] + "\" on row " + peakIndex + " but the peak information file has observation \"" + varInfo.RowNames[peakIndex] + "\" on row " + peakIndex + ".");

                // --------------------
                // - PEAK INFO FIELDS -
                // --------------------

                // Field: LC-MS mode
                ELcmsMode lcmsMode;

                if (dfn.LcmsMode == ELcmsMode.Mixed)
                {
                    lcmsMode = (ELcmsMode)int.Parse(varInfo[peakIndex, lcmsModeCol]);
                    UiControls.Assert(lcmsMode == ELcmsMode.Negative || lcmsMode == ELcmsMode.Positive, "LC-MS mode for peak " + peakIndex + " is invalid (" + lcmsMode + ")");
                }
                else
                {
                    lcmsMode = dfn.LcmsMode;
                }

                // Field: m/z
                decimal mz = mzCol != -1 ? decimal.Parse(varInfo[peakIndex, mzCol]) : 0;

                // ---------------
                // - INTENSITIES -
                // ---------------
                double[] intensities = new double[data.NumRows];

                for (int obsIndex = 0; obsIndex < data.NumRows; obsIndex++)
                {
                    intensities[obsIndex] = data[obsIndex, peakIndex];
                }

                PeakValueSet values = new PeakValueSet(result.Observations, result.Conditions, result.Types, intensities, result.AvgSmoother, result.MinSmoother, result.MaxSmoother);

                // Alternative observations
                PeakValueSet altValues = null;

                if (altData != null)
                {
                    // The alt. data might contain more variables or observations than the original data, or be in a
                    // different order - either way the row/column names must be the same and only those in both sets
                    // are used currently.
                    int altV = altData.ColIndex(varInfo.RowNames[peakIndex]);
                    double[] altIntensities = new double[data.NumRows];

                    for (int origRow = 0; origRow < data.NumRows; origRow++)
                    {
                        int altRow = altData.RowIndex(data.RowNames[origRow]);

                        altIntensities[origRow] = altData[altRow, altV];
                    }

                    altValues = new PeakValueSet(result.Observations, result.Conditions, result.Types, altIntensities, result.AvgSmoother, result.MinSmoother, result.MaxSmoother);
                }

                // Computation values
                IEnumerable<double> cvalues = values.ExtractValues(result.Conditions, IdsToTypes(result.Types, dfn.ConditionsOfInterest));

                // Create the variable
                Peak peak = new Peak(peakIndex, data.ColNames[peakIndex], values, altValues, lcmsMode, mz);
                varInfo.WriteMeta(peakIndex, peak.MetaInfo, result.PeakMetaHeader);
                result.Peaks.Add(peak);
            }

            prog.Leave();

            return result;
        }

        private static Color GetTypeColor(int n, bool bold)
        {
            switch (n % 7)
            {
                case 0:
                    return bold ? Color.FromArgb(0, 0, 255) : Color.FromArgb(128, 128, 255); // blue
                case 1:
                    return bold ? Color.FromArgb(255, 0, 0) : Color.FromArgb(255, 128, 128); // red
                case 2:
                    return bold ? Color.FromArgb(0, 128, 0) : Color.FromArgb(64, 128, 64); // green
                case 3:
                    return bold ? Color.FromArgb(128, 128, 0) : Color.FromArgb(128, 128, 6); // yellow
                case 4:
                    return bold ? Color.FromArgb(255, 0, 255) : Color.FromArgb(255, 128, 255); // magenta
                case 5:
                    return bold ? Color.FromArgb(0, 128, 128) : Color.FromArgb(64, 128, 128); // cyan
                case 6:
                    return bold ? Color.FromArgb(128, 128, 128) : Color.FromArgb(192, 192, 192); // gray
                default:
                    throw new SwitchException(n);
            }
        }

        private static List<GroupInfo> IdsToTypes(IEnumerable<GroupInfo> types, IEnumerable<int> list)
        {
            return list.Select(z => types.First(x => x.Id == z)).ToList();
        }

        private static void Load_6_CalculateDefaultStatistics(Core core, bool calcT, bool calcP, ProgressReporter prog)
        {
            // Create filters
            List<ObsFilter> allFilters = new List<ObsFilter>();
            Dictionary<GroupInfo, ObsFilter> singleGroupFilters = new Dictionary<GroupInfo, ObsFilter>();

            // Single group filters
            foreach (GroupInfo group in core.Groups)
            {
                var condition = new ObsFilter.ConditionGroup(Filter.ELogicOperator.And, false, Filter.EElementOperator.Is, new[] { group });
                ObsFilter filter = new ObsFilter(null, null, new[] { condition });
                allFilters.Add(filter);
                singleGroupFilters.Add(group, filter);
            }

            // Control filter
            ObsFilter filterControl;

            switch (core.ControlConditions.Count)
            {
                case 0:
                    filterControl = null;
                    break;

                case 1:
                    filterControl = singleGroupFilters[core.ControlConditions[0]];
                    break;

                default:
                    {
                        var condition = new ObsFilter.ConditionGroup(Filter.ELogicOperator.And, false, Filter.EElementOperator.Is, core.ControlConditions);
                        filterControl = new ObsFilter(null, null, new[] { condition });
                        allFilters.Add(filterControl);
                    }
                    break;
            }

            // Conditions of interest filter
            ObsFilter filterConditionsOfInterest;

            switch (core.ConditionsOfInterest.Count)
            {
                case 0:
                    filterConditionsOfInterest = null;
                    break;

                case 1:
                    filterConditionsOfInterest = singleGroupFilters[core.ConditionsOfInterest[0]];
                    break;

                default:
                    {
                        var condition = new ObsFilter.ConditionGroup(Filter.ELogicOperator.And, false, Filter.EElementOperator.Is, core.ConditionsOfInterest);
                        filterConditionsOfInterest = new ObsFilter(null, null, new[] { condition });
                        allFilters.Add(filterConditionsOfInterest);
                    }
                    break;
            }

            // Decide which tests
            if (core.ConditionsOfInterest.Count == 0)
            {
                calcT = false;
            }

            if (core.ConditionsOfInterest.Count == 0)
            {
                calcT = false;
                calcP = false;
            }

            // Create default tests
            List<ConfigurationStatistic> allTTests = new List<ConfigurationStatistic>();
            List<ConfigurationStatistic> allPearson = new List<ConfigurationStatistic>();

            // Iterate conditions of interest
            foreach (GroupInfo group in core.ConditionsOfInterest)
            {
                ObsFilter filterGroup = singleGroupFilters[group];

                if (calcT)
                {
                    // Create t-test
                    allTTests.Add(CreateTTestStatistic(group.DisplayName, filterGroup, filterControl));
                }

                if (calcP)
                {
                    // Create Pearson
                    allPearson.Add(CreatePearsonStatistic(group.DisplayName, filterGroup));
                }
            }

            // Create summary statistics
            if (calcT)
            {
                allTTests.Add(CreateMinStatistic(allTTests, "MINIMUM: t-tests"));
            }

            if (calcP)
            {
                allPearson.Add(CreateAbsMaxStatistic(allPearson, "MAXIMUM: Abs. correlation"));
            }

            // Set filters
            core.SetObsFilters(allFilters);

            // Calculate values
            ConfigurationStatistic[] allTests = allTTests.Concat(allPearson).ToArray();
            core.SetStatistics(allTests, true, prog);
        }

        /// <summary>
        /// Creates an absolute maximum statistic of other statistics.
        /// </summary>
        private static ConfigurationStatistic CreateAbsMaxStatistic(List<ConfigurationStatistic> pStatOpts, string name)
        {
            object pStatMinParam1 = pStatOpts.Select(z => new WeakReference<ConfigurationStatistic>(z)).ToArray();
            ArgsStatistic pStatMinArgs = new ArgsStatistic(EAlgoSourceMode.Full, null, EAlgoInputBSource.None, null, null, new[] { pStatMinParam1 });
            var pStat = new ConfigurationStatistic(name, null, Algo.ID_STATS_ABSMAX, pStatMinArgs);
            return pStat;
        }

        /// <summary>
        /// Creates an mathematical minimum statistic of other statistics.
        /// </summary>
        private static ConfigurationStatistic CreateMinStatistic(List<ConfigurationStatistic> tStatOpts, string name)
        {
            object tStatMinParam1 = tStatOpts.Select(z => new WeakReference<ConfigurationStatistic>(z)).ToArray();
            ArgsStatistic tStatMinArgs = new ArgsStatistic(EAlgoSourceMode.Full, null, EAlgoInputBSource.None, null, null, new[] { tStatMinParam1 });
            var tStat = new ConfigurationStatistic(name, null, Algo.ID_STATS_MIN, tStatMinArgs);
            return tStat;
        }

        /// <summary>
        /// Calculates a default T-test statistic.
        /// </summary>
        private static ConfigurationStatistic CreateTTestStatistic(string name, ObsFilter typeOfInterest, ObsFilter controlConditions)
        {
            ArgsStatistic args = new ArgsStatistic(EAlgoSourceMode.Full, typeOfInterest, EAlgoInputBSource.SamePeak, controlConditions, null, null);
            var stat = new ConfigurationStatistic("T-TEST: " + name, null, Algo.ID_METRIC_TTEST, args);
            return stat;
        }

        /// <summary>
        /// Calculates a default pearson correlation statistic.
        /// </summary>
        private static ConfigurationStatistic CreatePearsonStatistic(string name, ObsFilter typeOfInterest)
        {
            ArgsStatistic argsPearson = new ArgsStatistic(EAlgoSourceMode.Trend, typeOfInterest, EAlgoInputBSource.Time, null, null, null);
            var statPearson = new ConfigurationStatistic("PEARSON: " + name, "Pearson correlation of the trend-line for " + name + " against time.", Algo.ID_METRIC_PEARSON, argsPearson);
            return statPearson;
        }

        void IProgressReceiver.ReportProgressDetails(ProgressReporter.ProgInfo info)
        {
            backgroundWorker1.ReportProgress(0, info);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressReporter.ProgInfo info = (ProgressReporter.ProgInfo)e.UserState;

            if (info.Percent >= 0)
            {
                progressBar1.Value = info.Percent;
                progressBar1.Maximum = 100;
                progressBar1.Style = ProgressBarStyle.Continuous;
            }
            else
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
            }

            if (info.CText != null)
            {
                _lblInfo.Text = info.Text + " (" + info.CText + ")";
            }
            else
            {
                _lblInfo.Text = info.Text;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (FrmMsgBox.ShowOkCancel(this, "Error", e.Error.Message + "\nDo you want to view the full error details?"))
                {
                    FrmInputLarge.ShowFixed(this, "View error details", "Error report", e.Error.Message, e.Error.ToString());
                }

                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _prog.SetCancelAsync(true);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
