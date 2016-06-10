using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Settings
{
    [Serializable]
    class MainSettings
    {
        private const int MAX_RECENT_WORKSPACES = 20;
        private const int MAX_RECENT_SESSIONS = 10;

        public Dictionary<string, int> DoNotShowAgain = new Dictionary<string, int>();
        public List<RecentSession> RecentSessions = new List<RecentSession>();
        public List<DataFileNames> RecentWorkspaces = new List<DataFileNames>();
        public GeneralSettings General = new GeneralSettings();

        public static MainSettings Instance;

        [Serializable]
        public class GeneralSettings
        {
            public string RBinPath;
            public string PathwayToolsDatabasesPath;
            public bool? SaveOnClose;
            public bool AutoBackup = true;
        }

        public static void Initialise()
        {
            // load objects individually so everything doesn't break on new version
            //Instance = XmlSettings.Load("MainSettings.dat", SerialisationFormat.Binary, new MainSettings());
            Instance = new MainSettings();
        }

        public MainSettings()
        {
            SaveLoad(false);
        }                 

        private void SaveLoad(bool save)
        {
            ProgressReporter prog = ProgressReporter.GetEmpty(); // too fast to warrent dialogue
            XmlSettings.SaveLoad(save, FileId.DoNotShowAgain, ref DoNotShowAgain, null, prog);
            XmlSettings.SaveLoad(save, FileId.RecentSessions, ref RecentSessions, null, prog);
            XmlSettings.SaveLoad(save, FileId.RecentWorkspaces, ref RecentWorkspaces, null, prog);
            XmlSettings.SaveLoad(save, FileId.General, ref General, null, prog);
        }

        public void Save()
        {
            SaveLoad(true);
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            UiControls.InvokeConstructor(this);
        }

        public void AddRecentSession(Core core)
        {
            string fnu = core.FileNames.Session.ToUpper();
            RecentSessions.RemoveAll(λ => λ.FileName.ToUpper() == fnu);
            RecentSession e = new RecentSession(core);
            RecentSessions.Insert(0, e);
            ArrayHelper.TrimList(RecentSessions, MAX_RECENT_SESSIONS);
        }

        internal void AddRecentWorkspace(DataFileNames fileNames)
        {
            RecentWorkspaces.RemoveAll(λ => λ.Title == fileNames.Title);
            RecentWorkspaces.Add(fileNames);
            ArrayHelper.TrimList(RecentWorkspaces, MAX_RECENT_WORKSPACES);
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
                return Title + " - " + FileName;
            }
        }
    }
}
