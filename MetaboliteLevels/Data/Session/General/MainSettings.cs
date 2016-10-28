using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Definition;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.General
{
    /// <summary>
    /// Main user settings.
    /// For session-specific settings see <see cref="CoreOptions"/>.
    /// </summary>
    [Serializable]
    class MainSettings
    {
        private const int MAX_RECENT_WORKSPACES = 20;
        private const int MAX_RECENT_SESSIONS = 10;

        public Dictionary<string, int> DoNotShowAgain = new Dictionary<string, int>();
        public List<RecentSession> RecentSessions = new List<RecentSession>();
        public List<DataFileNames> RecentWorkspaces = new List<DataFileNames>();
        public GeneralSettings General = new GeneralSettings();
        public FileLoadInfo FileLoadInfo = new FileLoadInfo();

        public static MainSettings Instance;

        [Serializable]
        public class GeneralSettings
        {
            public string RBinPath;
            public string PathwayToolsDatabasesPath;
            public bool AutoBackup = false;
        }

        public static void Initialise()
        {
            // load objects individually so everything doesn't break on new version
            //Instance = XmlSettings.Load("MainSettings.dat", SerialisationFormat.Binary, new MainSettings());
            Instance = new MainSettings();
        }

        public MainSettings()
        {
            this.SaveLoad(false, EFlags.All);
        }

        public void Reset()
        {
            this.DoNotShowAgain = new Dictionary<string, int>();
            this.General = new GeneralSettings();
            Save( EFlags.DoNotShowAgain | EFlags.General );
        }

        [Flags]
        public enum EFlags
        {
            None=0,
            DoNotShowAgain=1,
            RecentSessions=2,
            RecentWorkspaces=4,
            General=8,
            FileLoadInfo = 16,
            All = 0xFFFF,
        }

        private void SaveLoad( bool save, EFlags flags )
        {
            ProgressReporter prog = ProgressReporter.GetEmpty(); // too fast to warrent dialogue
            if (flags.Has( EFlags.DoNotShowAgain ))   XmlSettings.SaveLoad( save, FileId.DoNotShowAgain, ref this.DoNotShowAgain, null, prog );
            if (flags.Has( EFlags.RecentSessions ))   XmlSettings.SaveLoad( save, FileId.RecentSessions, ref this.RecentSessions, null, prog );
            if (flags.Has( EFlags.RecentWorkspaces )) XmlSettings.SaveLoad( save, FileId.RecentWorkspaces, ref this.RecentWorkspaces, null, prog );
            if (flags.Has( EFlags.General ))          XmlSettings.SaveLoad( save, FileId.General, ref this.General, null, prog );
            if (flags.Has( EFlags.FileLoadInfo ))     XmlSettings.SaveLoad( save, FileId.FileLoadInfo, ref this.FileLoadInfo, null, prog );
        }

        public void Save(EFlags flags)
        {
            this.SaveLoad(true, flags);
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            UiControls.InvokeConstructor(this);
        }

        public void AddRecentSession(Core core)
        {
            string fnu = core.FileNames.Session.ToUpper();
            this.RecentSessions.RemoveAll(λ => λ.FileName.ToUpper() == fnu);
            RecentSession e = new RecentSession(core);
            this.RecentSessions.Insert(0, e);
            ArrayHelper.TrimList(this.RecentSessions, MAX_RECENT_SESSIONS);
        }

        internal void AddRecentWorkspace(DataFileNames fileNames)
        {
            this.RecentWorkspaces.RemoveAll(λ => λ.Title == fileNames.Title);
            this.RecentWorkspaces.Add(fileNames);
            ArrayHelper.TrimList(this.RecentWorkspaces, MAX_RECENT_WORKSPACES);
        }

        [Serializable]
        public class RecentSession
        {
            public string FileName;
            public string Title;
            public Guid Guid;

            public RecentSession(Core core)
            {
                this.FileName = core.FileNames.Session;
                this.Title = core.FileNames.Title;
                this.Guid = core.CoreGuid;
            }

            public override string ToString()
            {
                return this.Title + " - " + this.FileName;
            }
        }
    }
}
