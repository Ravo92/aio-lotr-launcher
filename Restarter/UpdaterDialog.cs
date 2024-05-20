using Helper;
using AutoUpdaterDotNET;
using System.Net;
using System;

namespace Restarter
{
    public partial class UpdaterDialog : Form
    {
        public UpdaterDialog()
        {
            InitializeComponent();
            BackgroundImage = Helper.Properties.Resources.UpdaterSplash;
        }

        private void UpdaterDialog_Shown(object sender, EventArgs e)
        {
            try
            {
                AutoUpdater.Start("https://ravo92.github.io/LauncherUpdater.xml");
                AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
                AutoUpdater.Synchronous = true;
                AutoUpdater.Mandatory = true;
                AutoUpdater.UpdateMode = Mode.ForcedDownload;
                AutoUpdater.ShowSkipButton = false;
                AutoUpdater.ShowRemindLaterButton = false;
                AutoUpdater.LetUserSelectRemindLater = false;
                AutoUpdater.HttpUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                AutoUpdater.AppTitle = "Patch 2.22 Launcher";
                AutoUpdater.RunUpdateAsAdmin = true;
                AutoUpdater.DownloadPath = Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_LAUNCHER);
                AutoUpdater.ClearAppDirectory = false;
                AutoUpdater.ReportErrors = false;

                string jsonPath = Path.Combine(Environment.CurrentDirectory, "AutoUpdaterSettings.json");
                AutoUpdater.PersistenceProvider = new JsonFilePersistenceProvider(jsonPath);
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.IsUpdateAvailable)
                {
                    try
                    {
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            UpdateIsDownloaded.LauncherUpdateIsDownloaded = true;
                            Close();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        LogHelper.LoggerRestarter.Error(exception, "AutoUpdaterOnCheckForUpdateEvent Error: ");
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    Close();
                }
            }
            else
            {
                if (args.Error is WebException)
                {
                    MessageBox.Show(
                        @"There is a problem reaching update server. Please check your internet connection and try again later.",
                        @"Update Check Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(args.Error.Message,
                        args.Error.GetType().ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    LogHelper.LoggerRestarter.Error(args.Error.GetType().ToString(), "args.Error is WebException: ");
                }
            }
        }
    }
}