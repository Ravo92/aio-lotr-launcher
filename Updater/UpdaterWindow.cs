using AutoUpdaterDotNET;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace PatchLauncher
{
    public partial class UpdaterWindow : Form
    {
        public UpdaterWindow()
        {
            InitializeComponent();
            InitializeWebView2Settings();
            CheckForUpdates();
        }

        private static async void InitializeWebView2Settings()
        {
            try
            {
                var version = CoreWebView2Environment.GetAvailableBrowserVersionString();

                File.WriteAllText("webView2_Version.log", version);
            }
            catch (WebView2RuntimeNotFoundException)
            {
                string fileName = Path.Combine(Application.StartupPath, "Tools", "MicrosoftEdgeWebview2Setup.exe");
                await RunWebViewSilentSetupAsync(fileName);
            }
        }

        public static async Task RunWebViewSilentSetupAsync(string fileName)
        {
            var p = Process.Start(fileName, new[] { "/silent", "/install" });
            await p.WaitForExitAsync().ConfigureAwait(false);
        }

        public static void CheckForUpdates()
        {
            AutoUpdater.Start("https://ravo92.github.io/LauncherUpdater.xml");
            AutoUpdater.InstalledVersion = Assembly.GetEntryAssembly()!.GetName().Version;
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
                        else
                        {
                            Process _process = new();
                            _process.StartInfo.FileName = "GameSelection.exe";
                            _process.StartInfo.Arguments = "--official";
                            _process.StartInfo.WorkingDirectory = Application.StartupPath;
                            _process.Start();

                            Application.Exit();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        Application.Exit();
                    }
                }
                else
                {
                    Process _process = new();
                    _process.StartInfo.FileName = "GameSelection.exe";
                    _process.StartInfo.Arguments = "--official";
                    _process.StartInfo.WorkingDirectory = Application.StartupPath;
                    _process.Start();

                    Application.Exit();
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