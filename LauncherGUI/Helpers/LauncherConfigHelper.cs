using System.Configuration;
using System.IO;

namespace LauncherGUI.Helpers
{
    internal class LauncherConfigHelper
    {
        internal static void MigrateLauncherSettings()
        {
            if (!File.Exists(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath))
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.Save();
            }
        }
    }
}