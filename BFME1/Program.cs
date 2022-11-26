using System;
using System.Diagnostics;
using System.Security.Principal;
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
            if (args[0].ToString() != "-official")
            {
                Application.Exit();
            }
            else
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
                        Process? p = Process.Start(startInfo);

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new BFME1());
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        return;
                    }
                }
            }
        }
    }
}