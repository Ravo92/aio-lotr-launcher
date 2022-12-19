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
            ApplicationConfiguration.Initialize();
            Application.Run(new UpdaterWindow());

            Application.Exit();
        }
    }
}