﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Corrections;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Definition;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    /// <summary>
    /// Performs loading of the dataset / session.
    /// </summary>
    internal partial class FrmActDataLoad : Form, IProgressReceiver
    {
        // column headers
      

        private readonly DataFileNames _fileNames; // files to load...
        private readonly string _sessionFileName; // ... OR session to load

        private Core _result; // the result

        private const string ALT_NAMES_KEY = "Alt. Names";
        private const string FALLBACK_ALL_TYPE_ID = "A";
        private const string FALLBACK_ALL_BATCH_ID = "B";
        private ProgressReporter _prog;

        FileLoadInfo _dataInfo;
        private List<string> _warnings;

        /// <summary>
        /// Constructor
        /// </summary>
        private FrmActDataLoad(DataFileNames fileNames, string sessionFileName )
        {
            this.InitializeComponent();
            UiControls.SetIcon( this );

            this.label1.Text = UiControls.Title;
            this.label2.Text = UiControls.Description;

            this._fileNames       = fileNames;
            this._sessionFileName = sessionFileName;
            this._dataInfo        = UiControls.GetFileLoadInfo();

            // UiControls.CompensateForVisualStyles(this);
        }

        /// <summary>
        /// Loads data from file
        /// </summary>
        internal static Core Show(Form owner, DataFileNames fileNames )
        {
            return Show(owner, fileNames, null );
        }

        /// <summary>
        /// Loads previous session
        /// </summary>
        internal static Core Show(Form owner, string sessionFileName )
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

            using (FrmActDataLoad frm = new FrmActDataLoad(fileNames, sessionFileName ))
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

            this.backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// Meat of the loading (async).
        /// </summary>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this._prog = new ProgressReporter(this);

            // LOAD FROM SESSION?
            if (this._sessionFileName != null)
            {
                this._prog.Enter("Loading session");
                this._result = Core.Load(this._sessionFileName, this._prog);
                this._prog.Leave();
                return;
            }

            this._warnings = new List<string>();

            // CREATE A NEW CORE
            List <Compound> compounds         = new List<Compound>();
            List<Pathway> pathways           = new List<Pathway>();
            List<Adduct> adducts             = new List<Adduct>();
            MetaInfoHeader pathwayHeader     = new MetaInfoHeader();
            MetaInfoHeader compoundHeader    = new MetaInfoHeader();
            MetaInfoHeader adductsHeader     = new MetaInfoHeader();
            MetaInfoHeader annotationsHeader = new MetaInfoHeader();

            // DATA SETS
            DataSet data = Load_1_DataSets(this._dataInfo, this._fileNames, this._prog, this._warnings );

            // COMPOUNDS & PATHWAYS
            Load_2_Compounds( this._dataInfo, compounds, pathways, pathwayHeader, compoundHeader, this._fileNames.CompoundLibraies, this._prog);

            // ADDUCTS
            Load_3_Adducts( this._dataInfo, adducts, this._fileNames.AdductLibraries, adductsHeader, this._prog);

            // M/Zs
            Load_4_MatchMzs( this._fileNames.AutomaticIdentifications, this._fileNames.PeakPeakMatching, data.Peaks, adducts, compounds, this._fileNames.AutomaticIdentificationsToleranceValue, this._fileNames.AutomaticIdentificationsToleranceUnit, this._fileNames.AutomaticIdentificationsStatus, this._prog );

            // IDENTIFICATIONS
            if (!string.IsNullOrEmpty(this._fileNames.Identifications))
            {
                Load_5_UserIdentifications( this._dataInfo, annotationsHeader, data.Peaks, compounds, adducts, this._fileNames.Identifications, this._fileNames.ManualIdentificationsStatus, this._warnings, this._prog );
            }

            // Set result
            this._result = new Core(this._fileNames, data, compounds, pathways, compoundHeader, pathwayHeader, adducts, adductsHeader, annotationsHeader);

            // STATISTICS
            Load_6_CalculateDefaultStatistics( data.IntensityMatrix, this._result, this._fileNames._standardAutoCreateOptions, this._prog);
        }

        /// <summary>
        /// Loads identifications.
        /// </summary>
        public static void Load_5_UserIdentifications( FileLoadInfo dataInfo, MetaInfoHeader annotationMeta, IEnumerable<Peak> peaks, List<Compound> ccompounds, List<Adduct> adducts, string fileName, EAnnotation status, List<string> warnings, ProgressReporter prog)
        {
            SpreadsheetReader reader = dataInfo.GetReader();
            reader.Progress = prog.SetProgress;

            prog.Enter("Loading identifications");
            Spreadsheet<string> mat = reader.Read<string>(fileName);
            prog.Leave();

            prog.Enter("Interpreting identifications");
            int peakCol      = mat.TryFindColumn( dataInfo.ANNOTATIONFILE_PEAK_HEADER );
            int mzCol        = mat.TryFindColumn( dataInfo.PEAKFILE_MZ_HEADER );
            int rtCol        = mat.TryFindColumn( dataInfo.PEAKFILE_RT_HEADER );
            int statusCol    = mat.TryFindColumn( dataInfo.ANNOTATIONFILE_STATUS_HEADER );
            int compoundsCol = mat.FindColumn( dataInfo.ANNOTATIONFILE_COMPOUNDS_HEADER );

            if (peakCol == -1 && (mzCol == -1 || rtCol == -1))
            {
                throw new InvalidOperationException( $"The manual identifications file \"{fileName}\" contains neither a \"peak\" column nor a combination of \"mz\" and \"rt\" columns. Peak names or a combination of m/z and retention time (LC-MS only) are required to correlate annotations in the file with those in the database. Please fix this in the file and try again." );
            }

            List<string> errors = new List<string>();

            for (int row = 0; row < mat.NumRows; row++)
            {
                prog.SetProgress(row, mat.NumRows);

                string peakName = null;
                string mz = null;
                string rt = null;
                EAnnotation annotationStatus = status;

                if (peakCol != -1)
                {
                    peakName = mat[row, peakCol];
                }      

                if (mzCol != -1)
                {
                    mz = mat[row, mzCol];
                }

                if (rtCol != -1)
                {
                    rt = mat[row, rtCol];
                }

                if (statusCol != -1)
                {
                    annotationStatus = EnumHelper.Parse<EAnnotation>( mat[row, statusCol] );
                }

                Peak peak ;

                if (peakName != null)
                {
                    peak = peaks.FirstOrDefault( z => z.DefaultDisplayName == peakName );

                    if (peak == null)
                    {
                        warnings.Add( $"Could not find the peak named \"{peakName}\" as referenced in \"{fileName}\"." );
                        continue;
                    }
                }
                else
                {
                    decimal dmz = decimal.Parse( mz );
                    decimal drt = decimal.Parse( rt );
                    int pmz = StringHelper.GetDecimalPlaces( mz );
                    int prt = StringHelper.GetDecimalPlaces( rt );
                                                                                           
                    peak = peaks.FirstOrDefault( z => decimal.Round( z.Mz, pmz ) == dmz && decimal.Round( z.Rt, prt ) == drt );

                    if (peak == null)
                    {
                        Peak closestMz = peaks.FindLowest( z => (Math.Abs( z.Mz - dmz ) / dmz) + (Math.Abs( z.Rt - drt ) / drt) );
                        warnings.Add( $"Could not find the peak with m/z = {mz} and r.t. = {rt} as referenced in \"{fileName}\". Check that you have specified the identification values correctly and that your peak table has loaded correctly. The closest match is {closestMz.Id} (m/z = {closestMz.Mz}, r.t = {closestMz.Rt})." );
                        continue;
                    }
                }

                string[] compounds = mat[row, compoundsCol].Split(",".ToCharArray());

                foreach (string compoundName in compounds)
                {
                    Annotation annotation = null;

                    foreach (Compound compound in ccompounds)
                    {
                        if (compound.Id == compoundName || compound.DefaultDisplayName == compoundName)
                        {
                            annotation = new Annotation(peak, compound, CreateOrGetEmpty(adducts), annotationStatus);
                            break;
                        }
                    }

                    if (annotation == null)
                    {
                        Compound newCompound = new Compound(null, compoundName, compoundName, 0);
                        ccompounds.Add(newCompound);
                        annotation = new Annotation(peak, newCompound, CreateOrGetEmpty(adducts), annotationStatus );
                    }

                    WriteMeta( mat, row, annotation.Meta, annotationMeta);

                    annotation.Peak.Annotations.Add(annotation);
                    annotation.Compound.Annotations.Add(annotation);
                    annotation.Adduct.Annotations.Add(annotation);
                }
            }

            prog.Leave();       
        }

        private static void WriteMeta( Spreadsheet<string> data, int row, MetaInfoCollection collection, MetaInfoHeader headers )
        {
            for (int col = 0; col < data.NumCols; col++)
            {
                collection.Write( headers, data.ColNames[col], data.Data[row, col].ToString() );
            }
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

        private static void Load_3_Adducts( FileLoadInfo dataInfo, List<Adduct> adducts, List<string> fileNames, MetaInfoHeader header, ProgressReporter prog)
        {
            SpreadsheetReader reader = dataInfo.GetReader();
            reader.Progress = prog.SetProgress;

            for (int index = 0; index < fileNames.Count; index++)
            {
                string fileName = fileNames[index];
                prog.Enter( "Loading adducts (" + index + " of " + fileNames.Count + ")" );
                Spreadsheet<string> mat;

                if (fileName.StartsWith( "resx:\\\\" ))
                {
                    byte[] text = (byte[])Resx.Scripts.ResourceManager.GetObject( fileName.Substring( 7 ) );

                    using (MemoryStream ms = new MemoryStream( text ))
                    {
                        using (StreamReader sr = new StreamReader( ms ))
                        {
                            mat = reader.Read<string>( sr, fileName );
                        }
                    }
                }
                else
                {
                    mat = reader.Read<string>( fileName );
                }

                prog.Leave();

                prog.Enter( "Interpreting adducts (" + index + " of " + fileNames.Count + ")");

                int nameCol = mat.FindColumn( dataInfo.ADDUCTFILE_NAME_HEADER );
                int chargeCol = mat.FindColumn( dataInfo.ADDUCTFILE_CHARGE_HEADER );
                int mzCol = mat.FindColumn( dataInfo.ADDUCTFILE_MASS_DIFFERENCE_HEADER );

                for (int row = 0; row < mat.NumRows; row++)
                {
                    prog.SetProgress(row, mat.NumRows);

                    Adduct a = new Adduct(mat[row, nameCol], int.Parse(mat[row, chargeCol]), decimal.Parse(mat[row, mzCol]));

                    WriteMeta(mat, row, a.MetaInfo, header);

                    adducts.Add(a);
                }

                prog.Leave();
            }
        }

        private static void Load_4_MatchMzs( bool matchPeaksToCompounds, bool matchPeaksToPeaks, List<Peak> peaks, List<Adduct> adducts, List<Compound> compounds, decimal toleranceValue, ETolerance toleranceUnit, EAnnotation status, ProgressReporter progress )
        {
            if (matchPeaksToCompounds)
            {
                progress.Enter( "Matching peaks to compounds" );

                decimal tolerance;

                switch (toleranceUnit)
                {
                    case ETolerance.Absolute:
                        tolerance = toleranceValue;
                        break;

                    case ETolerance.PartsPerMillion:
                        tolerance = toleranceValue / 1000000m;
                        break;

                    case ETolerance.Percent:
                        tolerance = toleranceValue / 100m;
                        break;

                    default:
                        throw new SwitchException( toleranceUnit );
                }

                UiControls.Assert( tolerance > 0, "m/z matching tolerance cannot be 0 or less." );


                for (int i = 0; i < peaks.Count; i++)
                {
                    progress.SetProgress( i, peaks.Count );

                    Peak pᵢ = peaks[i];


                    foreach (Adduct aᵢ in adducts)
                    {
                        if ((int)pᵢ.LcmsMode == Math.Sign( aᵢ.Charge ))
                        {
                            decimal tol = pᵢ.Mz * tolerance;
                            decimal tmz = pᵢ.Mz - (aᵢ.Mz * Math.Abs( aᵢ.Charge ));

                            foreach (Compound cᵢ in compounds)
                            {
                                if (cᵢ.Mass != 0)
                                {
                                    if (cᵢ.Mass >= tmz - tol && cᵢ.Mass <= tmz + tol)
                                    {
                                        Annotation annotation = new Annotation( pᵢ, cᵢ, aᵢ, status );

                                        pᵢ.Annotations.Add( annotation );
                                        cᵢ.Annotations.Add( annotation );
                                        aᵢ.Annotations.Add( annotation );
                                    }
                                }
                            }
                        }
                    }
                }

                progress.Leave();
            }

            if (matchPeaksToPeaks)
            {

                progress.Enter( "Matching peaks to peaks" );

                for (int pi = 0; pi < peaks.Count; pi++)
                {
                    progress.SetProgress( pi, peaks.Count );

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
                                    p.SimilarPeaks.Add( p2 );
                                }
                            }
                        }
                    }
                }

                progress.Leave();
            }
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

            string resourcePrefix = "adducts~";

            foreach (DictionaryEntry resource in UiControls.GetScriptsResources())
            {
                string key = (string)resource.Key;

                if (key.StartsWith( resourcePrefix ))
                {
                    string title = key.Substring( resourcePrefix.Length );
                    adducts.Add( new NamedItem<string>( "resx:\\\\" + key , title ) );
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

        private static void Load_2_Compounds(FileLoadInfo dataInfo, List<Compound> compounds, List<Pathway> pathwaysList, MetaInfoHeader pathwayMeta, MetaInfoHeader compoundMeta, List<CompoundLibrary> toLoad, ProgressReporter prog)
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
                    LoadCsvDatabase( dataInfo, compounds, pathwaysList, pathwayMeta, compoundMeta, cl.CompoundFile, cl.PathwayFile, cl, prog);
                }

                prog.Leave();
            }
        }

        private static void LoadCsvDatabase( FileLoadInfo dataInfo, List<Compound> compoundsOut, List<Pathway> pathwaysList, MetaInfoHeader pathwayMeta, MetaInfoHeader compoundMeta, string compFile, string patFile, CompoundLibrary tag, ProgressReporter prog)
        {
            SpreadsheetReader reader = dataInfo.GetReader();
            reader.Progress = prog.SetProgress;

            Dictionary<string, Pathway> pathways = new Dictionary<string, Pathway>();

            pathways.AddRange(pathwaysList, z => z.Id);

            // Pathways
            {
                Spreadsheet<string> pathwayMatrix = reader.Read<string>( patFile );

                int nameCol = pathwayMatrix.FindColumn( dataInfo.PATHWAYFILE_NAME_HEADER );
                int idCol = pathwayMatrix.FindColumn( dataInfo.PATHWAYFILE_FRAME_ID_HEADER );

                for (int row = 0; row < pathwayMatrix.NumRows; row++)
                {
                    prog.SetProgress(row, pathwayMatrix.NumRows);

                    string name = pathwayMatrix[row, nameCol];
                    string id = pathwayMatrix[row, idCol];

                    Pathway p = AddWithoutConflict(pathwayMeta, tag, pathways, id, name);

                    WriteMeta( pathwayMatrix, row, p.MetaInfo, pathwayMeta);
                }
            }

            Dictionary<string, Compound> compounds = new Dictionary<string, Compound>();

            compounds.AddRange(compoundsOut, z => z.Id);

            // Compounds
            {
                Spreadsheet<string> mat = reader.Read<string>( compFile );

                int nameCol = mat.FindColumn( dataInfo.COMPOUNDFILE_NAME_HEADER );
                int idCol = mat.FindColumn( dataInfo.COMPOUNDFILE_FRAME_ID_HEADER );
                int mzCol = mat.FindColumn( dataInfo.COMPOUNDFILE_MASS_HEADER );
                int pathwayCol = mat.FindColumn( dataInfo.COMPOUNDFILE_PATHWAYS_HEADER );

                for (int row = 0; row < mat.NumRows; row++)
                {
                    prog.SetProgress(row, mat.NumRows);

                    string id = mat[row, idCol];
                    string name = mat[row, nameCol];
                    decimal mass = mat[row, mzCol] != "" ? decimal.Parse(mat[row, mzCol]) : 0m;

                    Compound compound = AddWithoutConflict(compoundMeta, tag, compounds, id, name, mass);

                    WriteMeta(mat, row, compound.MetaInfo, compoundMeta);

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

        public static Dictionary<string, string> LoadConditionInfo(FileLoadInfo dataInfo, string fileName, ProgressReporter reportProgress)
        {
            SpreadsheetReader reader = dataInfo.GetReader();
            reader.Progress = reportProgress.SetProgress;

            Dictionary<string, string> output = new Dictionary<string, string>();

            Spreadsheet<string> mat = reader.Read<string>( fileName );

            output.Clear();

            int idCol = mat.FindColumn( dataInfo.CONDITIONFILE_ID_HEADER );
            int nameCol = mat.FindColumn( dataInfo.CONDITIONFILE_NAME_HEADER );

            for (int row = 0; row < mat.NumRows; row++)
            {
                output.Add(mat[row, idCol], mat[row, nameCol]);
            }

            return output;
        }

        internal class DataSet
        {
            public List<ObservationInfo> Observations;
            public List<ObservationInfo> Conditions;
            public List<GroupInfo> Types;
            public List<BatchInfo> Batches;
            public List<Peak> Peaks;
            public MetaInfoHeader PeakMetaHeader;
            public MetaInfoHeader ObsMetaHeader;
            internal OriginalData IntensityMatrix;
            internal OriginalData AltIntensityMatrix;
        }

        /// <summary>
        /// Loads the primary datasets into a DataSet object.
        /// * Condition names
        /// * Intensities
        /// * Alt. intensities
        /// * Observations
        /// * Peaks
        /// </summary>
        /// <param name="dataInfo">Loading parameters</param>
        /// <param name="files">Names of files to load</param>
        /// <param name="prog">Progress reporter</param>
        /// <returns>A DataSet object containing the loaded datasets</returns>
        private static DataSet Load_1_DataSets(FileLoadInfo dataInfo, DataFileNames files, ProgressReporter prog, List<string> warnings)
        {
            // Generate a SpreadsheetReader to read the CSV files
            SpreadsheetReader reader = dataInfo.GetReader();
            reader.Progress = prog.SetProgress;

            // Create the object to hold the result
            DataSet result = new DataSet();
            result.PeakMetaHeader = new MetaInfoHeader();
            result.ObsMetaHeader = new MetaInfoHeader();

            // Check for common problems
            UiControls.Assert( !string.IsNullOrEmpty( files.Data ), "The intensities file has not been specified." );
            UiControls.Assert( !string.IsNullOrEmpty( files.ObservationInfo ), "The observation information file has not been specified." );

            /////////////////////////////////////////////////////////////
            // STAGE 1.
            // First we load the information into spreadsheets
            /////////////////////////////////////////////////////////////

            // Load the condition names
            Dictionary<string, string> conditionNames;

            if (!string.IsNullOrEmpty( files.ConditionInfo ))
            {
                prog.Enter( "Loading conditions" );
                conditionNames = LoadConditionInfo( dataInfo, files.ConditionInfo, prog );
                prog.Leave();
            }
            else
            {
                conditionNames = null;
            }

            // Load data
            prog.Enter( "Loading intensities" );
            Spreadsheet<double> ssIntensities = reader.Read<double>( files.Data );
            prog.Leave();

            prog.Enter( "Loading alt. intensities" );
            Spreadsheet<double> ssAltIntensities = !string.IsNullOrWhiteSpace( files.AltData ) ? reader.Read<double>( files.AltData ) : null;
            prog.Leave();

            prog.Enter( "Loading observations" );
            Spreadsheet<string> ssObservations = reader.Read<string>( files.ObservationInfo );
            prog.Leave();

            prog.Enter( "Loading peaks" );
            Spreadsheet<string> ssPeaks = reader.Read<string>( files.PeakInfo );
            prog.Leave();

            /////////////////////////////////////////////////////
            // STAGE 2.
            // We process the spreadsheets into our data format
            /////////////////////////////////////////////////////
            prog.Enter( "Formatting data" );

            // Get the columns
            int ssObsDayCol = TryFindColumn( ssObservations, warnings, dataInfo.OBSERVATIONFILE_TIME_HEADER, "time", "Time information is required for meaningful time-series analysis. Whilst analyses requiring time-series data will function incorrectly you may still be able to perform basic analysis of your data." );
            int ssObsRepCol = TryFindColumn( ssObservations, warnings, dataInfo.OBSERVATIONFILE_REPLICATE_HEADER, "replicate index", "Replicate information is required primarily to distinguish individual datapoints, however most analyses should function without it." );
            int ssObsTypCol = TryFindColumn( ssObservations, warnings, dataInfo.OBSERVATIONFILE_GROUP_HEADER, "experimental group", "Group information is required to compare experimental groups, however if you only have one experimental group this warning can be safely ignored." );
            int ssObsBatCol = TryFindColumn( ssObservations, warnings, dataInfo.OBSERVATIONFILE_BATCH_HEADER, "batch", "Batch information is required in order to apply batch correction. Batch correction options may not function correctly without this information. If your data was not collected batch-wise then this warning can be safely ignored." );
            int ssObsAcqCol = TryFindColumn( ssObservations, warnings, dataInfo.OBSERVATIONFILE_ACQUISITION_HEADER, "acquisition index", "Acquisition information is required in order to apply certain batch correction methods. Some batch correction options may not function correctly without this information. If your data was not collected batch-wise, or if you only intend to perform basic data corrections, then this warning can be safely ignored." );

            int ssPeakMzCol;
            int ssPeakRtCol;
            int ssPeakLcmsCol;

            switch (files.LcmsMode)
            {
                case ELcmsMode.None:
                    ssPeakMzCol = -1;
                    ssPeakRtCol = -1;
                    ssPeakLcmsCol = -1;
                    break;

                case ELcmsMode.Positive:
                case ELcmsMode.Negative:
                    ssPeakMzCol = ssPeaks.FindColumn( dataInfo.PEAKFILE_MZ_HEADER );
                    ssPeakRtCol = ssPeaks.FindColumn( dataInfo.PEAKFILE_RT_HEADER );
                    ssPeakLcmsCol = -1;
                    break;

                case ELcmsMode.Mixed:
                    ssPeakMzCol = ssPeaks.FindColumn( dataInfo.PEAKFILE_MZ_HEADER );
                    ssPeakRtCol = ssPeaks.FindColumn( dataInfo.PEAKFILE_RT_HEADER );
                    ssPeakLcmsCol = ssPeaks.FindColumn( dataInfo.PEAKFILE_LCMSMODE_HEADER );
                    break;

                default:
                    throw new InvalidOperationException( "LC-MS mode hasn't been specified." );
            }

            // Interpret "ssObservations" to create an array of experimental groups 
            //                                                , batches
            var expGroups = new List<GroupInfo>();
            var expGroupsById = new Dictionary<string, GroupInfo>();
            var batches = new List<BatchInfo>();
            var batchesById = new Dictionary<string, BatchInfo>();
            {
                List<string> typeIds = new List<string>();
                Dictionary<string, Range> typeRanges = new Dictionary<string, Range>();

                List<string> batchIds = new List<string>();
                Dictionary<string, Range> batchRanges = new Dictionary<string, Range>();

                for (int row = 0; row < ssObservations.NumRows; row++) // obs info
                {
                    int day = ssObsDayCol != -1 ? ssObservations.AsInteger( row, ssObsDayCol ) : 0;
                    string typeId = ssObsTypCol != -1 ? ssObservations[row, ssObsTypCol] : FALLBACK_ALL_TYPE_ID;
                    int acquis = ssObsAcqCol != -1 ? ssObservations.AsInteger( row, ssObsAcqCol ) : 0;
                    string batchId = ssObsBatCol != -1 ? ssObservations[row, ssObsBatCol] : FALLBACK_ALL_BATCH_ID;

                    // Add type (if not already)
                    if (!typeIds.Contains( typeId ))
                    {
                        typeIds.Add( typeId );
                        typeRanges.Add( typeId, Range.MaxValue );
                    }

                    // Add batch (if not already)
                    if (!batchIds.Contains( batchId ))
                    {
                        batchIds.Add( batchId );
                        batchRanges.Add( batchId, Range.MaxValue );
                    }

                    typeRanges[typeId] = typeRanges[typeId].ExpandOrStart( day );
                    batchRanges[batchId] = batchRanges[batchId].ExpandOrStart( acquis );
                }


                CreateGroupInfo( "Condition", "C", conditionNames, expGroups, expGroupsById, typeIds, ( index, stringId, displayPriority, name, shortName ) => new GroupInfo( stringId, index, typeRanges[stringId], name, shortName, displayPriority ) );
                CreateGroupInfo( "Batch", "B", null, batches, batchesById, batchIds, ( index, stringId, displayPriority, name, shortName ) => new BatchInfo( stringId, index, batchRanges[stringId], name, shortName, displayPriority ) );
            }

            // Interpret "ssObservations" to create our arrays of observations
            //                                                  , conditions
            //                                                  , types
            List<ObservationInfo> observations = new List<ObservationInfo>();
            List<ObservationInfo> conditions = new List<ObservationInfo>();

            for (int row = 0; row < ssObservations.NumRows; row++) // obs info
            {
                int day = ssObsDayCol != -1 ? ssObservations.AsInteger( row, ssObsDayCol ) : 0;
                int repId = ssObsRepCol != -1 ? ssObservations.AsInteger( row, ssObsRepCol ) : 0;
                string typeId = ssObsTypCol != -1 ? ssObservations[row, ssObsTypCol] : FALLBACK_ALL_TYPE_ID;
                string batchId = ssObsBatCol != -1 ? ssObservations[row, ssObsBatCol] : FALLBACK_ALL_BATCH_ID;
                int acquisition = ssObsAcqCol != -1 ? ssObservations.AsInteger( row, ssObsAcqCol ) : 0;

                GroupInfo groupInfo = expGroupsById[typeId];

                // Add observation
                var newObs = new ObservationInfo( new Acquisition( ssObservations.RowNames[row], repId, batchesById[batchId], acquisition ), groupInfo, day );
                observations.Add( newObs );
                WriteMeta( ssObservations, row, newObs.MetaInfo, result.ObsMetaHeader );
            }

            // Generate CONDITIONS from OBSERVATIONS
            foreach (var group in observations.GroupBy( z => z.Group ))
            {
                foreach (var time in group.GroupBy( z => z.Time ))
                {
                    ObservationInfo condition = new ObservationInfo( null, group.Key, time.Key );

                    // Write META where it is the same across everything
                    for (int n = 0; n < result.ObsMetaHeader.Headers.Length; ++n)
                    {
                        var metaValues = time.Unique( z => z.MetaInfo.Read( n ) );
                                              
                        if (metaValues.Count == 1)
                        {
                            condition.MetaInfo.Write( result.ObsMetaHeader, result.ObsMetaHeader.Headers[n], metaValues.First() );
                        }
                    }

                    conditions.Add( condition );
                }
            }

            // Add to result
            result.Observations = observations;
            result.Conditions = conditions;
            result.Types = expGroups;
            result.Batches = batches;                                                                                      

            // Read in "ssPeaks" to an array of peaks
            result.Peaks = new List<Peak>();

            for (int row = 0; row < ssPeaks.NumRows; row++)
            {
                ELcmsMode lcmsMode;

                if (files.LcmsMode == ELcmsMode.Mixed)
                {
                    lcmsMode = (ELcmsMode)int.Parse( ssPeaks[row, ssPeakLcmsCol] );
                    UiControls.Assert( lcmsMode == ELcmsMode.Negative || lcmsMode == ELcmsMode.Positive, "LC-MS mode for peak " + row + " is invalid (" + lcmsMode + ")" );
                }
                else
                {
                    lcmsMode = files.LcmsMode;
                }

                decimal mz = ssPeakMzCol != -1 ? decimal.Parse( ssPeaks[row, ssPeakMzCol] ) : 0;
                decimal rt = ssPeakRtCol != -1 ? decimal.Parse( ssPeaks[row, ssPeakRtCol] ) : 0;


                Peak peak = new Peak( ssPeaks.RowNames[row], lcmsMode, mz, rt );
                WriteMeta( ssPeaks, row, peak.MetaInfo, result.PeakMetaHeader );
                result.Peaks.Add( peak );
            }


            // Read in "ssIntensities" to an intensity matrix
            result.IntensityMatrix = InterpretIntensityMatrix( "Original data", prog, ssIntensities, result.Peaks, result.Observations );

            if (ssAltIntensities != null)
            {
                result.AltIntensityMatrix = InterpretIntensityMatrix( "alternate", prog, ssAltIntensities, result.Peaks, result.Observations );
            }

            prog.Leave();

            return result;
        }

        private static int TryFindColumn( Spreadsheet<string> matrix, List<string> warnings, string[] header, string title, string extraWarning )
        {
            int result= matrix.TryFindColumn( header );

            if (result == -1)
            {
                warnings.Add( $"The file {{{matrix.Title}}} does not contain any of the columns defining {title}, i.e. any of of {{{string.Join( ", ", header )}}} as defined in the program settings. The columns which are present in your data are {{{string.Join( ", ", matrix.ColNames )}}}. This information will be missing or zero in your analysis." + (extraWarning != null ? (" " + extraWarning) : "") );
            }

            return result;
        }

        private static OriginalData InterpretIntensityMatrix( string title, ProgressReporter prog, Spreadsheet<double> ss, List<Peak> availPeaks, List<ObservationInfo> availableObs )
        {
            //       PEAKS →
            // OBS   intensities
            // ↓ 

            Dictionary<string, Peak> peakFinder = availPeaks.ToDictionary( z => z.Id );
            Dictionary<string, ObservationInfo> obsFinder = availableObs.ToDictionary( z => z.Id );

            double[,] matrix = new double[ss.NumCols,ss.NumRows];

            Peak[] peaks = new Peak[ss.NumCols];
            ObservationInfo[] obs = new ObservationInfo[ss.NumRows];

            for (int peakIndex = 0; peakIndex < ss.NumCols; ++peakIndex)
            {
                prog.SetProgress( peakIndex, ss.NumCols );

                for (int obsIndex = 0; obsIndex < ss.NumRows; obsIndex++)
                {
                    matrix[peakIndex,obsIndex] = ss[obsIndex, peakIndex];
                }

                peaks[peakIndex] =  peakFinder[ss.ColNames[peakIndex]] ;
            }

            for (int row = 0; row < ss.NumRows; ++row)
            {
                if (!obsFinder.TryGetValue( ss.RowNames[row], out obs[row] ))
                {
                    throw new FormatException( $"The intensity matrix {{{ss.Title}}} contains a definition for observation {{{ss.RowNames[row]}}} but that observation does not exist." );
                }
            }

            var x =  new IntensityMatrix(peaks, obs, matrix );

            return new OriginalData( title, ss.Title, x );
        }

        private static void CreateGroupInfo<T>(string longNamePrefix, string shortNamePrefix, Dictionary<string, string> conditionNames, List<T> types, Dictionary<string, T> byId, List<string> typeIds, Func<int, string, int, string, string, T> result)
            where T : GroupInfoBase
        {
            for (int index = 0; index < typeIds.Count; index++)
            {
                string stringId = typeIds[index];

                string name;
                string shortName;
                int displayPriority;

                // If we have been provided a name then use it
                if (stringId.Length == 0)
                {
                    shortName = "";
                    name = "";
                    displayPriority = 0;
                }
                else if (conditionNames != null && conditionNames.TryGetValue(stringId, out name))
                {
                    shortName = name.Substring(0, 1).ToUpper();
                    displayPriority = 0;
                }
                else
                {
                    // If we have just got a number then give it a longer name
                    int intId = 0;

                    if (int.TryParse(stringId, out intId))
                    {
                        name = longNamePrefix+" " + stringId;
                        shortName = shortNamePrefix + stringId;
                        displayPriority = intId;
                    }
                    else
                    {
                        name = stringId;
                        shortName = name.Substring(0, 1).ToUpper();
                        displayPriority = 0;
                    }
                }   

                while (types.Any(z => z.DisplayPriority == displayPriority))
                {
                    displayPriority++;
                }

                // (n, intId, name, shortName)
                T r = result(index, stringId, displayPriority, name, shortName);
                types.Add(r);
                byId.Add(stringId, r);
            }
        }

        private static List<GroupInfo> IdsToTypes(IEnumerable<GroupInfo> types, IEnumerable<string> list)
        {
            List<GroupInfo> result = new List<GroupInfo>( );

            foreach (string name in list)
            {
                GroupInfo group = types.FirstOrDefault( z => z.Id == name );

                if (group == null)
                {
                    string expG = StringHelper.ArrayToString( types, z=> z.Id );
                    throw new InvalidOperationException( $"Cannot find the experimental group named '{name}' in the list of experimental groups: {expG}"  );
                }

                result.Add( group );
            }

            return result;                                                        
        }

        private static void Load_6_CalculateDefaultStatistics( IMatrixProvider src, Core core, EAutoCreateOptions opts, ProgressReporter prog)
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
            if (core.ConditionsOfInterest.Count>1)
            {                                    
                var condition = new ObsFilter.ConditionGroup(Filter.ELogicOperator.And, false, Filter.EElementOperator.Is, core.ConditionsOfInterest);
                ObsFilter filterConditionsOfInterest = new ObsFilter(null, null, new[] { condition });
                allFilters.Add(filterConditionsOfInterest);
            }

            // Decide which tests
            bool calcT = opts.Has( EAutoCreateOptions.TTest ) && core.ConditionsOfInterest.Count!=0 && core.ControlConditions.Count!=0;
            bool calcP = opts.Has( EAutoCreateOptions.Pearson ) && core.ConditionsOfInterest.Count != 0;
            bool calcMed = opts.Has( EAutoCreateOptions.MedianTrend );
            bool calcMean = opts.Has( EAutoCreateOptions.MeanTrend );
            bool calcUVSc = opts.Has( EAutoCreateOptions.UvScaleAndCentre );

            List<ConfigurationTrend> trends = new List<ConfigurationTrend>();

            List<ConfigurationCorrection> corrections = new List<ConfigurationCorrection>();

            // UV Scale and Centre
            if (calcUVSc)
            {
                ArgsCorrection args = new ArgsCorrection( Algo.ID_CORRECTION_UV_SCALE_AND_CENTRE, src, new object[0], ECorrectionMode.None,
                                                          ECorrectionMethod.None, null, null );
                corrections.Add( new ConfigurationCorrection() { Args = args } );
            }

            src = core.FindMatrixAlias( EProviderAlias.LastCorrection );

            // MEDIAN
            if (calcMed)
            {   
                ArgsTrend args = new ArgsTrend( Algo.ID_TREND_MOVING_MEDIAN, src, new object[] { 1 } );
                trends.Add( new ConfigurationTrend() { Args = args } );
            }

            if (calcMean)
            {
                ArgsTrend args = new ArgsTrend( Algo.ID_TREND_MOVING_MEAN, src, new object[] { 1 } );
                trends.Add( new ConfigurationTrend() { Args = args } );
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
                    allTTests.Add(CreateTTestStatistic(src, group, filterGroup, core.ControlConditions, filterControl ));
                }

                if (calcP)
                {
                    // Create Pearson
                    allPearson.Add(CreatePearsonStatistic(src, group, filterGroup));
                }
            }

            // Create summary statistics
            if (calcT)
            {
                allTTests.Add(CreateMinStatistic(src, allTTests, "p.min"));
            }

            if (calcP)
            {
                allPearson.Add(CreateAbsMaxStatistic(src, allPearson, "r.max"));
            }

            core.SetCorrections( corrections, prog, false );

            // Set filters
            core.SetObsFilters(allFilters);

            // Calculate values
            ConfigurationStatistic[] allTests = allTTests.Concat(allPearson).ToArray();
            core.SetStatistics(allTests, prog, false);
        }

        /// <summary>
        /// Creates an absolute maximum statistic of other statistics.
        /// </summary>
        private static ConfigurationStatistic CreateAbsMaxStatistic( IMatrixProvider src, List<ConfigurationStatistic> pStatOpts, string name)
        {
            object pStatMinParam1 = pStatOpts.Select(z => new WeakReference<ConfigurationStatistic>(z)).ToArray();
            ArgsStatistic pStatMinArgs = new ArgsStatistic( Algo.ID_STATS_ABSMAX, src, null, EAlgoInputBSource.None, null, null, new[] { pStatMinParam1 });
            pStatMinArgs.OverrideDisplayName = name;
            pStatMinArgs.Comment = $"Autogenerated statistic.\r\nAbsolute maximum of the default pearson statistics:\r\n\r\n * {string.Join( "\r\n * ", pStatOpts.Select( z => z.Args.DisplayName ) )}";
            var pStat = new ConfigurationStatistic();
            pStat.Args = pStatMinArgs;
            return pStat;
        }

        /// <summary>
        /// Creates an mathematical minimum statistic of other statistics.
        /// </summary>
        private static ConfigurationStatistic CreateMinStatistic( IMatrixProvider src, List<ConfigurationStatistic> tStatOpts, string name )
        {
            object param1 = tStatOpts.Select( z => new WeakReference<ConfigurationStatistic>( z ) ).ToArray();
            ArgsStatistic args = new ArgsStatistic( Algo.ID_STATS_MIN, src, null, EAlgoInputBSource.None, null, null, new[] { param1 } );
            args.OverrideDisplayName = name;                             
            args.Comment = $"Autogenerated statistic.\r\nMinimum of the default t-test statistics:\r\n\r\n * {string.Join( "\r\n * ", tStatOpts.Select( z => z.Args.DisplayName ) )}";
            var stat = new ConfigurationStatistic();
            stat.Args = args;
            return stat;
        }

        /// <summary>
        /// Calculates a default T-test statistic.
        /// </summary>
        private static ConfigurationStatistic CreateTTestStatistic( IMatrixProvider src, GroupInfo typeOfInterestName, ObsFilter typeOfInterest, IEnumerable< GroupInfo> controlName, ObsFilter controlConditions)
        {
            ArgsStatistic args = new ArgsStatistic( Algo.ID_METRIC_TTEST, src, typeOfInterest, EAlgoInputBSource.SamePeak, controlConditions, null, null);
            args.OverrideDisplayName = "p." + typeOfInterestName.DisplayShortName;
            args.Comment = $"Autogenerated statistic.\r\nt-test p-value between observations in the {{{typeOfInterestName.DisplayName}}} group against observations in the {{{string.Join( ", ", controlConditions.DisplayName )}}} group(s).";
            var stat = new ConfigurationStatistic();
            stat.Args = args;
            return stat;
        }

        /// <summary>
        /// Calculates a default pearson correlation statistic.
        /// </summary>
        private static ConfigurationStatistic CreatePearsonStatistic( IMatrixProvider src, GroupInfo typeOfInterestName, ObsFilter typeOfInterest)
        {
            ArgsStatistic args = new ArgsStatistic( Algo.ID_METRIC_PEARSON, src, typeOfInterest, EAlgoInputBSource.Time, null, null, null);
            args.OverrideDisplayName = "r."+ typeOfInterestName.DisplayShortName;
            args.Comment = $"Autogenerated statistic.\r\nPearson correlation of the trend-line for {{{typeOfInterestName.DisplayName}}} against time.";
            var stat = new ConfigurationStatistic();
            stat.Args = args;
            return stat;
        }

        void IProgressReceiver.ReportProgressDetails(ProgressReporter.ProgInfo info)
        {
            this.backgroundWorker1.ReportProgress(0, info);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressReporter.ProgInfo info = (ProgressReporter.ProgInfo)e.UserState;

            if (info.Percent >= 0)
            {
                this.label3.Text = info.Percent + "%";
            }
            else
            {
                this.label3.Text = "";
            }

            if (info.CText != null)
            {
                this._lblInfo.Text = info.Text + " (" + info.CText + ")";
            }
            else
            {
                this._lblInfo.Text = info.Text;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.pictureBox1.Visible = false;
                FrmMsgBox.ShowError( this, e.Error.Message );
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                if (this._warnings != null && this._warnings.Count != 0)
                {
                    FrmInputMultiLine.ShowFixed( this, "Load data", "Warnings", "One or more warnings occured whilst loading the data", string.Join( "\r\n\r\n", this._warnings ) );
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this._prog.SetCancelAsync(true);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
