using PatchLauncher.Helper;
using System;
using System.IO;
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
            // ***this line is added***
            // if (Environment.OSVersion.Version.Major >= 6)
            //    SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args[0].ToString() != "-official")
            {
                Application.Exit();
            }
            else
            {
                ApplicationConfiguration.Initialize();

                if (RegistryFunctions.ReadRegKey("path") == null || !Directory.Exists(RegistryFunctions.ReadRegKey("path")))
                {
                    Properties.Settings.Default.IsGameInstalled = false;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.IsGameInstalled = true;
                    Properties.Settings.Default.GameInstallPath = RegistryFunctions.ReadRegKey("path");
                    Properties.Settings.Default.Save();
                }

                Application.Run(new BFME1());
            }
        }
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //private static extern bool SetProcessDPIAware();
    }
}