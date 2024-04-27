using Downloader;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Net.Http;
using System.Reflection;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using static LauncherGUI.Helpers.GameSelectorHelper;
using Serilog;

namespace LauncherGUI.Helpers
{
    class GameFileToolsHelper(ILogger logger)
    {
        private static readonly string startupPath = AppDomain.CurrentDomain.BaseDirectory;

        private int _DownloadProgressChangedLimiter = 0;
        private IProgress<ProgressHelper>? OverallProgress;

        private readonly ILogger _logger = logger;

        private async Task<GameFileDictionary> LoadGameFileDictionary()
        {
            string json = "noInternet";

            try
            {
                // Try deserializing the JSON file from remote into local GameFileDictionary class objects
                json = await DownloadJSONFile();

                // Check if JSON file exists and if the values are identical with remote file, if not, save new remote file as local json file
                if (json == "noInternet")
                {
                    if (File.Exists(Path.Combine(startupPath, ConstStringsHelper.C_JSON_GAMEDICTIONARY_MAIN_FILE)))
                    {
                        json = File.ReadAllText(Path.Combine(startupPath, ConstStringsHelper.C_JSON_GAMEDICTIONARY_MAIN_FILE));
                        return JsonConvert.DeserializeObject<GameFileDictionary>(json)!;
                    }
                    else
                    {
                        MessageBox.Show("Can not download game info. Please establish an internet connection and restart the launcher!");
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "");
            }

            try
            {
                // write JSON directly to a file
                using StreamWriter file = File.CreateText(Path.Combine(startupPath, ConstStringsHelper.C_JSON_GAMEDICTIONARY_MAIN_FILE));
                await file.WriteAsync(json);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Fatal(ex, "Cant Access local json file! Data may not be accurate!");
                json = File.ReadAllText(Path.Combine(startupPath, ConstStringsHelper.C_JSON_GAMEDICTIONARY_MAIN_FILE));
                return JsonConvert.DeserializeObject<GameFileDictionary>(json)!;
            }

            return JsonConvert.DeserializeObject<GameFileDictionary>(json)!;
        }

        private async Task<string> DownloadJSONFile()
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(3);
                string json = await httpClient.GetStringAsync($"https://ravo92.github.io/{ConstStringsHelper.C_JSON_GAMEDICTIONARY_MAIN_FILE}");
                return json;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.Error(ex, "Timeout while downloading JSON file");
                return "noInternet";
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while downloading JSON file");
                return "noInternet";
            }
        }

        private async Task DownloadFile(string pathToZIPFile, string ZIPFileName, List<string> DownloadURLs, int downloadUrlCount, IProgress<ProgressHelper> downloadProgress, string assemblyName)
        {
            try
            {
                string DownloadUrl = DownloadURLs[downloadUrlCount];

                if (DownloadURLs.Count >= downloadUrlCount)
                    DownloadUrl = DownloadURLs[downloadUrlCount];
                else
                    DownloadUrl = DownloadURLs[0];

                _logger.Information("Downloading from URI: < {0} >", DownloadUrl);

                string fullPathWithFileName = Path.Combine(pathToZIPFile, ZIPFileName);
                _logger.Information("Downloading into file: < {0} >", fullPathWithFileName);

                var downloadOpt = new DownloadConfiguration()
                {
                    ParallelDownload = true,
                    ReserveStorageSpaceBeforeStartingDownload = true,
                    ClearPackageOnCompletionWithFailure = true,
                    MaxTryAgainOnFailover = 2,
                    Timeout = 10000,
                    ChunkCount = 4,
                    RequestConfiguration =
                    {
                        KeepAlive = true,
                        ProtocolVersion = System.Net.HttpVersion.Version11,
                        UserAgent = assemblyName + " on Version: " + Assembly.GetEntryAssembly()!.GetName().Version
                    }
                };

                OverallProgress = downloadProgress;
                var downloader = new DownloadService(downloadOpt);
                _logger.Debug(downloader.Status.ToString());

                downloader.DownloadStarted += OnDownloadStarted;
                downloader.DownloadProgressChanged += OnDownloadProgressChanged;
                downloader.DownloadFileCompleted += OnDownloadFileCompleted;

                _logger.Debug(downloader.Status.ToString());

                if (!File.Exists(fullPathWithFileName))
                {
                    await downloader.DownloadFileTaskAsync(DownloadUrl, fullPathWithFileName);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "");
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
                _logger.Error(ex, "");
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
                _logger.Error(ex, "");
            }
        }

        private void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    OverallProgress!.Report(new ProgressHelper() { CurrentFileName = e.Error.Message });
                    _logger.Error(e.Error.Message);
                }
            }

            catch (Exception ex)
            {
                _logger.Error(ex, "");
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
               // _logger.Error(ex, "");
            }
        }

        internal string CheckIfJSONLanguageExists(string jsonLocaleISOCode, AvailableBFMEGames gameName)
        {
            if (JSONDataListHelper._DictionarylanguageSettings.ContainsKey(jsonLocaleISOCode))
            {
                _logger.Information("Language key {0} exists in json, continue", jsonLocaleISOCode);
                return jsonLocaleISOCode;
            }
            else
            {
                _logger.Warning("Language key {0} DOES NOT exist in json, continue with locale > en_uk <", jsonLocaleISOCode);

                try
                {
                   // BFMERegistryHelper.WriteRegKeyForBFMEGames("Locale", "en_uk", gameName);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error writing into key > Locale < with value > en_uk <");
                }

                return "en_uk";
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
            if (BFMERegistryHelper.ReadRegKeyBFME1("path") == ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND || !File.Exists(Path.Combine(BFMERegistryHelper.ReadRegKeyBFME1("path"), ConstStringsHelper.C_BFME1_MAIN_GAME_FILE)))
                Properties.Settings.Default.BFME1GameInstalled = false;
            else
                Properties.Settings.Default.BFME1GameInstalled = true;

            if (BFMERegistryHelper.ReadRegKeyBFME2("path") == ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND || !File.Exists(Path.Combine(BFMERegistryHelper.ReadRegKeyBFME2("path"), ConstStringsHelper.C_BFME2_MAIN_GAME_FILE)))
                Properties.Settings.Default.BFME2GameInstalled = false;
            else
                Properties.Settings.Default.BFME2GameInstalled = true;

            if (BFMERegistryHelper.ReadRegKeyROTWK("path") == ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND || !File.Exists(Path.Combine(BFMERegistryHelper.ReadRegKeyROTWK("path"), ConstStringsHelper.C_ROTWK_MAIN_GAME_FILE)))
                Properties.Settings.Default.ROTWKGameInstalled = false;
            else
                Properties.Settings.Default.ROTWKGameInstalled = true;

            Properties.Settings.Default.Save();
        }
    }
}