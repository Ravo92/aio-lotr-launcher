using System;
using System.IO;
using Downloader;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using static LauncherGUI.Helpers.GameSelectorHelper;

namespace LauncherGUI.Helpers
{
    class GameFileToolsHelper()
    {
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
            catch (Exception)
            {
                return "noInternet";
            }
        }

        internal static async Task DownloadFile(string pathWithFilenameDestination, string FileName, string DownloadURL)
        {
            try
            {
                string fullPathWithFileName = Path.Combine(pathWithFilenameDestination, FileName.Replace("/", "\\"));

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
                        UserAgent = Assembly.GetEntryAssembly()!.GetName() + " on Version: " + Assembly.GetEntryAssembly()!.GetName().Version
                    }
                };

                var downloader = new DownloadService(downloadOpt);

                if (!File.Exists(fullPathWithFileName))
                {
                    await downloader.DownloadFileTaskAsync(DownloadURL, fullPathWithFileName);
                }
            }
            catch (Exception)
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
            catch (Exception)
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