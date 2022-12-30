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
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

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

            if (args[0].ToString() != "--official")
            {
                Process _process = new();
                _process.StartInfo.FileName = "Updater.exe";
                _process.StartInfo.WorkingDirectory = Application.StartupPath;
                _process.Start();

                Application.Exit();
            }
            else
            {
                if (RegistryService.ReadRegKey("path") == "ValueNotFound" || !Directory.Exists(RegistryService.ReadRegKey("path")))
                {
                    Properties.Settings.Default.IsGameInstalled = false;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.IsGameInstalled = true;
                    Properties.Settings.Default.GameInstallPath = RegistryService.ReadRegKey("path");
                    Properties.Settings.Default.Save();
                }
                Application.Run(new BFME1());
            }
        }
    }
}