using System.Windows;
using System.Configuration;

namespace LauncherGUI.Helpers
{
    internal class LauncherConfigHelper
    {
        internal static void MigrateLauncherSettings()
        {
            if (!System.IO.File.Exists(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath))
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.Save();
            }
        }

        internal static void SetWindowVisible()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                Application.Current.MainWindow.ShowInTaskbar = true;
                Application.Current.MainWindow.Activate();
            }
        }

        internal static void SetWindowInvisible()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal || Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
                Application.Current.MainWindow.ShowInTaskbar = false;
            }
        }
    }
}