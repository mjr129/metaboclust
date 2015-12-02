using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Settings
{
    [Serializable]
    class MainSettings
    {
        private const int MAX_RECENT_WORKSPACES = 20;
        private const int MAX_RECENT_SESSIONS = 10;

        public HashSet<string> DoNotShowAgain = new HashSet<string>();
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

        /// <summary>
        /// Resets the main settings to their defaults.
        /// </summary>
        public static void Reset()
        {
            Instance = new MainSettings();
            Instance.Save();
        }

        private void SaveLoad(bool save)
        {
            XmlSettings.SaveLoad(save, "DoNotShowAgain.dat", ref DoNotShowAgain);
            XmlSettings.SaveLoad(save, "RecentSessions.dat", ref RecentSessions);
            XmlSettings.SaveLoad(save, "RecentWorkspaces.dat", ref RecentWorkspaces);
            XmlSettings.SaveLoad(save, "General.dat", ref General);
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

        public void AddRecentSession(string fileName, string title)
        {
            string fnu = fileName.ToUpper();
            RecentSessions.RemoveAll(λ => λ.FileName.ToUpper() == fnu);
            RecentSession e = new RecentSession(fileName, title);
            RecentSessions.Add(e);
            UiControls.TrimList(RecentSessions, MAX_RECENT_SESSIONS);
        }

        internal void AddRecentWorkspace(DataFileNames fileNames)
        {
            RecentWorkspaces.RemoveAll(λ => λ.Title == fileNames.Title);
            RecentWorkspaces.Add(fileNames);
            UiControls.TrimList(RecentWorkspaces, MAX_RECENT_WORKSPACES);
        }

        [Serializable]
        public class RecentSession
        {
            public string FileName;
            public string Title;

            public RecentSession()
            {
                this.FileName = "";
                this.Title = "";
            }

            public RecentSession(string fn, string title)
            {
                this.FileName = fn;
                this.Title = title;
            }
        }
    }
}
