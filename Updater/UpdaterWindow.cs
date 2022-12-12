using AutoUpdaterDotNET;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Net;

namespace PatchLauncher
{
    public partial class UpdaterWindow : Form
    {
        public UpdaterWindow()
        {
            InitializeComponent();
            InitializeWebView2Settings();
        }

        private async void InitializeWebView2Settings()
        {
            try
            {
                var version = CoreWebView2Environment.GetAvailableBrowserVersionString();

                File.WriteAllText("webView2_Version.log", version);
                TmrCowndown.Enabled = true;
                TmrCowndown.Start();
            }
            catch (WebView2RuntimeNotFoundException)
            {
                string fileName = Path.Combine(Application.StartupPath, "Tools", "MicrosoftEdgeWebview2Setup.exe");
                await RunWebViewSilentSetupAsync(fileName);
                TmrCowndown.Enabled = true;
                TmrCowndown.Start();
            }
        }

        public static async Task RunWebViewSilentSetupAsync(string fileName)
        {
            var p = Process.Start(fileName, new[] { "/SILENT", "/install" });
            await p.WaitForExitAsync().ConfigureAwait(false);
        }

        private void TmrCowndown_Tick(object sender, EventArgs e)
        {
            PBarLoading.Increment(30);
            if (PBarLoading.Value == 100) TmrCowndown.Stop();
            CheckForUpdates();

            if (!TmrCowndown.Enabled)
                Close();
        }

        public static void CheckForUpdates()
        {
            AutoUpdater.Start("https://ravo92.github.io/LauncherUpdater.xml");
            AutoUpdater.InstalledVersion = new Version("1.0.2");
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.ShowRemindLaterButton = false;
            AutoUpdater.HttpUserAgent = "BFME Launcher Update";
            AutoUpdater.ReportErrors = true;
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.DownloadPath = Application.StartupPath + "Downloads\\";
            AutoUpdater.ClearAppDirectory = false;
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
        }

        private static void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.IsUpdateAvailable)
                {
                    try
                    {
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            Application.Exit();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
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
                }
            }
        }
    }
}