using Helper;
using PatchLauncher.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PatchLauncher
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using Mutex mutex = new(false, Process.GetCurrentProcess().ProcessName);
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show(Strings.Warning_AlreadyRunning, Strings.Warning_AlreadyRunningTitle);
                return;
            }

            if (!Directory.Exists(ConstStrings.C_LOGFOLDER_NAME))
            {
                try
                {
                    Directory.CreateDirectory(ConstStrings.C_LOGFOLDER_NAME);
                }
                catch (Exception ex)
                {
                    using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                    file.WriteLineAsync(ex.Message);
                }
            }

            if (Directory.Exists(ConstStrings.C_WEBVIEW2CACHEFOLDER_NAME))
            {
                try
                {
                    Directory.Delete(ConstStrings.C_WEBVIEW2CACHEFOLDER_NAME, true);
                }
                catch (Exception ex)
                {
                    File.AppendAllText(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFOLDER_NAME, "webView2_error.log"), ex.Message);
                }
            }

            string configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            if (!File.Exists(configPath))
            {
                Settings.Default.Upgrade();
                Settings.Default.Reload();
                Settings.Default.Save();
            }

            switch (Settings.Default.Language)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                    break;
                default:
                    break;
            }

            Application.Run(new WinFormsMainGUI());
        }
    }
}