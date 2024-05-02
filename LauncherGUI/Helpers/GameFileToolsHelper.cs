using System;
using System.IO;
using Downloader;
using System.Net.Http;
using System.Reflection;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using static LauncherGUI.Helpers.GameSelectorHelper;

namespace LauncherGUI.Helpers
{
    class GameFileToolsHelper()
    {
        private static readonly string startupPath = AppDomain.CurrentDomain.BaseDirectory;

        private int _DownloadProgressChangedLimiter = 0;
        private IProgress<ProgressHelper>? OverallProgress;

        internal static async Task<string> DownloadJSONFile(string url)
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                string json = await httpClient.GetStringAsync(url);
                return json;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                return "noInternet";
            }
            catch (Exception ex)
            {
                return "noInternet";
            }
        }

        private async Task DownloadFile(string pathWithFilenameDestination, string FileName, List<string> DownloadURLs, int downloadUrlCount, IProgress<ProgressHelper> downloadProgress, string assemblyName)
        {
            try
            {
                string DownloadUrl = DownloadURLs[downloadUrlCount];

                if (DownloadURLs.Count >= downloadUrlCount)
                    DownloadUrl = DownloadURLs[downloadUrlCount];
                else
                    DownloadUrl = DownloadURLs[0];

                string fullPathWithFileName = Path.Combine(pathWithFilenameDestination, FileName);

                var downloadOpt = new DownloadConfiguration()
                {
                    ParallelDownload = false,
                    ReserveStorageSpaceBeforeStartingDownload = true,
                    ClearPackageOnCompletionWithFailure = true,
                    MaxTryAgainOnFailover = 2,
                    Timeout = 20000,
                    ChunkCount = 1,
                    RequestConfiguration =
                    {
                        KeepAlive = true,
                        ProtocolVersion = System.Net.HttpVersion.Version11,
                        UserAgent = assemblyName + " on Version: " + Assembly.GetEntryAssembly()!.GetName().Version
                    }
                };

                OverallProgress = downloadProgress;
                var downloader = new DownloadService(downloadOpt);

                downloader.DownloadStarted += OnDownloadStarted;
                downloader.DownloadProgressChanged += OnDownloadProgressChanged;
                downloader.DownloadFileCompleted += OnDownloadFileCompleted;

                if (!File.Exists(fullPathWithFileName))
                {
                    await downloader.DownloadFileTaskAsync(DownloadUrl, fullPathWithFileName);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void OnDownloadStarted(object? sender, DownloadStartedEventArgs e)
        {
            try
            {
                OverallProgress!.Report(new ProgressHelper() { CurrentFileName = e.FileName, TotalDownloadSizeInBytes = e.TotalBytesToReceive });
            }
            catch (Exception ex)
            {

            }
        }

        private void OnDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                if (_DownloadProgressChangedLimiter < 2048)
                {
                    _DownloadProgressChangedLimiter++;
                }
                else
                {
                    OverallProgress!.Report(new ProgressHelper()
                    {
                        PercentageValue = Math.Min(e.ProgressPercentage, 100),
                        DownloadSpeedSizeInBytes = e.BytesPerSecondSpeed,
                        TotalDownloadSizeInBytes = e.TotalBytesToReceive,
                        ProgressedDownloadSizeInBytes = e.ReceivedBytesSize
                    });

                    _DownloadProgressChangedLimiter = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    OverallProgress!.Report(new ProgressHelper() { CurrentFileName = e.Error.Message });
                }
            }

            catch (Exception ex)
            {

            }
        }

        internal static bool BFMEAppDataFolderExists(AvailableBFMEGames assemblyName)
        {
            return Directory.Exists(BFMERegistryHelper.GameAppDataFolderPath(assemblyName));
        }

        internal static void CreateBFMEAppDataFolder(AvailableBFMEGames assemblyName)
        {
            Directory.CreateDirectory(BFMERegistryHelper.GameAppDataFolderPath(assemblyName));
        }

        internal static bool BFMEOptionsIniFileExists(AvailableBFMEGames assemblyName)
        {
            return File.Exists(Path.Combine(BFMERegistryHelper.GameAppDataFolderPath(assemblyName), ConstStringsHelper.C_OPTIONSINI_FILENAME));
        }

        internal static void CreateBFMEOptionsIniFile(AvailableBFMEGames assemblyName)
        {
            try
            {
                File.Create(Path.Combine(BFMERegistryHelper.GameAppDataFolderPath(assemblyName), ConstStringsHelper.C_OPTIONSINI_FILENAME), 0, FileOptions.WriteThrough).Close();
            }
            catch (Exception ex)
            {

            }
        }

        internal static (string driveLetter, int totalDiskSpace, int totalFreeDiskSpace) CheckIfThereIsEnoughDiskSpace(string path)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            int totalSize = 0;
            int totalFreeSize = 0;
            string driveLetter = "C:\\";

            foreach (DriveInfo driveInfo in allDrives)
            {
                if (driveInfo.IsReady == true && path[..3] == driveInfo.Name)
                {
                    totalFreeSize = Convert.ToInt32(driveInfo.TotalFreeSpace / (1024 * 1024 * 1024));
                    totalSize = Convert.ToInt32(driveInfo.TotalSize / (1024 * 1024 * 1024));
                    driveLetter = driveInfo.Name;
                }
            }

            return (driveLetter, totalSize, totalFreeSize);
        }

        internal static void CheckForInstalledGames()
        {
            if (!File.Exists(Path.Combine(BFMERegistryHelper.ReadRegKeyBFME1("path"), ConstStringsHelper.C_BFME1_MAIN_GAME_FILE)))
                Properties.Settings.Default.BFME1GameInstalled = false;
            else
                Properties.Settings.Default.BFME1GameInstalled = true;


            if (!File.Exists(Path.Combine(BFMERegistryHelper.ReadRegKeyBFME2("path"), ConstStringsHelper.C_BFME2_MAIN_GAME_FILE)))
                Properties.Settings.Default.BFME2GameInstalled = false;
            else
                Properties.Settings.Default.BFME2GameInstalled = true;


            if (!File.Exists(Path.Combine(BFMERegistryHelper.ReadRegKeyROTWK("path"), ConstStringsHelper.C_ROTWK_MAIN_GAME_FILE)))
                Properties.Settings.Default.ROTWKGameInstalled = false;
            else
                Properties.Settings.Default.ROTWKGameInstalled = true;

            Properties.Settings.Default.Save();
        }
    }
}