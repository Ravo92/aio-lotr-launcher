using Helper;
using PatchLauncher.Properties;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PatchLauncher
{
    internal static class Program
    {
        static readonly Mutex _mutex = new(true, ConstStrings.C_MUTEX_NAME);
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    return;
                }
                else if (args[0] != "--official")
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ConstStrings.LogTime + ex.ToString());
            }

            ApplicationConfiguration.Initialize();

            try
            {
                if (!Directory.Exists(ConstStrings.C_LOGFOLDER_NAME))
                {
                    Directory.CreateDirectory(ConstStrings.C_LOGFOLDER_NAME);
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
                        if (Settings.Default.InstalledLanguageISOCode == "" || Settings.Default.InstalledLanguageISOCode is null)
                            Settings.Default.InstalledLanguageISOCode = "en_us";
                        break;

                    case 1:

                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                        if (Settings.Default.InstalledLanguageISOCode == "" || Settings.Default.InstalledLanguageISOCode is null)
                            Settings.Default.InstalledLanguageISOCode = "de";
                        break;

                    default:
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                        if (Settings.Default.InstalledLanguageISOCode == "" || Settings.Default.InstalledLanguageISOCode is null)
                            Settings.Default.InstalledLanguageISOCode = "en_us";
                        break;
                }

                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ConstStrings.LogTime + ex.ToString());
            }

            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.Run(new WinFormsMainGUI());
                _mutex.ReleaseMutex();
                _mutex.Dispose();
            }
            else
            {
                MessageBox.Show(Strings.Warning_AlreadyRunning, Strings.Warning_AlreadyRunningTitle);
                Application.Exit();
            }
        }
    }
}