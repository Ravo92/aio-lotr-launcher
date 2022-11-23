using PatchLauncher;
using System.Diagnostics;

namespace Updater
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new UpdaterWindow());

            Process _process = new();
            _process.StartInfo.FileName = "GameSelection.exe";
            _process.StartInfo.Arguments = "-official";
            _process.StartInfo.WorkingDirectory = Application.StartupPath;
            _process.Start();

            Application.Exit();
        }
    }
}