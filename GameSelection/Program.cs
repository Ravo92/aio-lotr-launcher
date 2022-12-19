using PatchLauncher;
using System.Diagnostics;

namespace GameSelection
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

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
                Application.Run(new GameSelect());
            }
        }
    }
}