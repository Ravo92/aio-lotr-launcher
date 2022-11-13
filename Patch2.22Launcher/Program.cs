using System;
using System.Diagnostics;
using System.Security.Principal;
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
            WindowsPrincipal pricipal = new(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!hasAdministrativeRight)
            {
                ProcessStartInfo startInfo = new()
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas"
                };
                try
                {
                    Process p = Process.Start(startInfo);
                    UpdaterWindow _updater = new();
                    _updater.Show();
                    _updater.TopMost = true;
                    //Thread.Sleep(2000);
                    _updater.Hide();

                    Application.Run(new GameSelect());
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    return;
                }
            }
        }
    }
}