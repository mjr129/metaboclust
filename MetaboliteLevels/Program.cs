using System;
using System.Drawing;
using System.Windows.Forms;  
using System.Threading;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Controls;

namespace MetaboliteLevels
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new Mutex(false, "{46A11243-B4F7-43EA-BC19-D3C27BF2218C}");

            try
            {
                // Wait a little while in case we are restarting
                if (!mutex.WaitOne(2000, true))
                {
                    MessageBox.Show("Application already running.");
                    return;
                }
            }
            catch
            {
                // Ignore
            }

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            }

            UiControls.Initialise(new System.Drawing.Font("Segoe UI", 12));
            MainSettings.Initialise();
            CtlError.DefaultIcon = Icon.FromHandle( Resources.IconInputError.GetHicon() );
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        /// <summary>
        /// Handles fatal errors.
        /// </summary>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex1 = e.ExceptionObject as Exception;
            Exception ex = ex1;
            const int level = 0;
            string fn = System.IO.Path.Combine(UiControls.StartupPath, "ErrorLog.txt");

            while (ex != null)
            {
                System.IO.File.AppendAllText(fn, DateTime.Now.ToString() + " ERROR " + level + "\r\n" + ex.ToString());
                ex = ex.InnerException;
            }

            MessageBox.Show("Program fatal error. Sorry about that.\r\n\r\nThe error has been logged to \"" + fn + "\".\r\n\r\nError description: " + ex1.Message, UiControls.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
