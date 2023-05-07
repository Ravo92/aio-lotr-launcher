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


            if (!ProgramState.IsArrayNullOrEmpty(args))
            {
                if (args[0] == "--install")
                {
                    ProgramState.CurrentProgramState = ProgramState.ProgramStates.install;
                }

                if (args[0] == "--repair")
                {
                    ProgramState.CurrentProgramState = ProgramState.ProgramStates.repair;
                }
            }

            Application.Run(new BFME1());
        }
    }
}