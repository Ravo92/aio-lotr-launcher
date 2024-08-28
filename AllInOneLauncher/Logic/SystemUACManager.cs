using AllInOneLauncher.Elements;
using AllInOneLauncher.Popups;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;

namespace AllInOneLauncher.Logic
{
    internal class SystemUACManager
    {
        public static bool IsRunAsAdmin()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RunAsAdmin(string fileName, string arguments = "")
        {
            var startInfo = new ProcessStartInfo(fileName)
            {
                UseShellExecute = true,
                Verb = "runas",
                Arguments = arguments
            };

            try
            {
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            }
        }

        public static void EnsureRunAsAdmin()
        {
            if (!IsRunAsAdmin())
            {
                RunAsAdmin(System.Environment.ProcessPath!);
                Application.Current.Shutdown();
            }
        }
    }
}