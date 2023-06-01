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

            if (!Directory.Exists(ConstStrings.C_LOGFOLDER_NAME))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(Application.StartupPath, ConstStrings.C_LOGFOLDER_NAME));
                }
                catch (Exception ex)
                {
                    using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                    file.WriteLineAsync(ex.Message);
                }
            }

            string configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            if (!File.Exists(configPath))
            {
                Settings.Default.Upgrade();
                Settings.Default.Reload();
                Settings.Default.Save();
            }

            using Mutex mutex = new(false, Process.GetCurrentProcess().ProcessName);
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("Launcher is already running! Please Check TrayIcon or Taskmanager first.", "Launcher already running!");
                return;
            }

            if (RegistryService.ReadRegKey("path") == "ValueNotFound" || !Directory.Exists(RegistryService.ReadRegKey("path")))
            {
                Settings.Default.IsGameInstalled = false;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.IsGameInstalled = true;
                Settings.Default.GameInstallPath = RegistryService.ReadRegKey("path");
                Settings.Default.Save();
            }

            Application.Run(new WinFormsMainGUI());
        }
    }
}