using Helper;

namespace Restarter
{
    internal class LauncherUpdater
    {
        internal static void CheckForUpdates()
        {
#warning A) REMOVE "TEST" IN XML IF YOU WANT TO SHIP THIS STUFF!!
#warning B) ALSO: TEST THIS UPDATE FUNCTION IF IT WORKS HERE!! IF NOT, WE NEED A NEW RESTARTER PROJECT AS AN WPF APPLICATION!!!
            try
            {
                AutoUpdaterDotNET.AutoUpdater.Start("https://ravo92.github.io/LauncherUpdaterTest.xml");
                AutoUpdaterDotNET.AutoUpdater.InstalledVersion = System.Reflection.Assembly.GetEntryAssembly()!.GetName().Version;
                AutoUpdaterDotNET.AutoUpdater.Synchronous = true;
                AutoUpdaterDotNET.AutoUpdater.Mandatory = true;
                AutoUpdaterDotNET.AutoUpdater.UpdateMode = AutoUpdaterDotNET.Mode.Forced;
                AutoUpdaterDotNET.AutoUpdater.ShowSkipButton = false;
                AutoUpdaterDotNET.AutoUpdater.ShowRemindLaterButton = false;
                AutoUpdaterDotNET.AutoUpdater.LetUserSelectRemindLater = false;
                AutoUpdaterDotNET.AutoUpdater.RemindLaterTimeSpan = AutoUpdaterDotNET.RemindLaterFormat.Minutes;
                AutoUpdaterDotNET.AutoUpdater.RemindLaterAt = 5;
                AutoUpdaterDotNET.AutoUpdater.UpdateFormSize = new System.Drawing.Size(1296, 759);
                AutoUpdaterDotNET.AutoUpdater.HttpUserAgent = "BFME Launcher Update";
                AutoUpdaterDotNET.AutoUpdater.AppTitle = System.Windows.Forms.Application.ProductName;
                AutoUpdaterDotNET.AutoUpdater.RunUpdateAsAdmin = true;
                AutoUpdaterDotNET.AutoUpdater.DownloadPath = Path.Combine(System.Windows.Forms.Application.StartupPath, Helper.ConstStrings.C_DOWNLOADFOLDER_NAME);
                AutoUpdaterDotNET.AutoUpdater.ClearAppDirectory = false;
                AutoUpdaterDotNET.AutoUpdater.ReportErrors = false;

                string jsonPath = Path.Combine(Environment.CurrentDirectory, "AutoUpdaterSettings.json");
                AutoUpdaterDotNET.AutoUpdater.PersistenceProvider = new AutoUpdaterDotNET.JsonFilePersistenceProvider(jsonPath);
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }
        }
    }
}