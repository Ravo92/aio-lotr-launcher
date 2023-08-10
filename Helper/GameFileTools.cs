using Downloader;
using Newtonsoft.Json;
using System.ComponentModel;
using SevenZipExtractor;

namespace Helper
{
    public class GameFileTools
    {
        public ulong EntrySize = 0;
        public string EntryFilename = "";
        public int counter = 0;
        public int Percentage = 0;

        private int _DownloadProgressChangedLimiter = 0;
        private IProgress<ProgressHelper>? OverallProgress;

        public static async Task<GameFileDictionary> LoadGameFileDictionary()
        {
            GameFileDictionary _gameFileDictionary = new();
            string json = "noInternet";

            try
            {
                // Try deserializing the JSON file from remote into local GameFileDictionary class objects
                json = await DownloadJSONFile();
                _gameFileDictionary = JsonConvert.DeserializeObject<GameFileDictionary>(json)!;
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
            }

            // Check if JSON file exists and if the values are identical with remote file, if not, save new remote file as local json file
            if (json == "noInternet")
            {
                _gameFileDictionary = JsonConvert.DeserializeObject<GameFileDictionary>(File.ReadAllText(Path.Combine(Application.StartupPath, "GameFileDictionary.json")))!;
                return _gameFileDictionary;
            }
            else
            {
                try
                {
                    // write JSON directly to a file
                    using StreamWriter file = File.CreateText(Path.Combine(Application.StartupPath, "GameFileDictionary.json"));
                    await file.WriteAsync(json);
                }
                catch (UnauthorizedAccessException ex)
                {
                    LogHelper.LoggerGameFileTools.Warning(ex, "Cant Access local json file! Data may not be accurate!");
                    return _gameFileDictionary;
                }
            }
            return _gameFileDictionary;
        }

        public static async Task<string> DownloadJSONFile()
        {
            try
            {
                // TODO: Write better network connection detection -> Ping 1.1.1.1 or check network adapter state.
                using var _httpClient = new HttpClient();
                _httpClient.Timeout = TimeSpan.FromSeconds(3);
                string json = await _httpClient.GetStringAsync("https://ravo92.github.io/GameFileDictionary.json");
                return json;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
                return "noInternet";
            }
        }

        public async Task DownloadFile(string pathtoZIPFile, string ZIPFileName, string DownloadUrl, IProgress<ProgressHelper> downloadProgress)
        {
            try
            {
                string fullPathwithFileName = Path.Combine(pathtoZIPFile, ZIPFileName);

                var downloadOpt = new DownloadConfiguration()
                {
                    ChunkCount = 1,
                    ParallelDownload = false,
                    ReserveStorageSpaceBeforeStartingDownload = true,
                    BufferBlockSize = 8000,
                    MaximumBytesPerSecond = 131072000,
                    ClearPackageOnCompletionWithFailure = true
                };

                OverallProgress = downloadProgress;

                var downloader = new DownloadService(downloadOpt);

                downloader.DownloadStarted += OnDownloadStarted;
                downloader.DownloadProgressChanged += OnDownloadProgressChanged;
                downloader.DownloadFileCompleted += OnDownloadFileCompleted;

                if (!File.Exists(fullPathwithFileName))
                {
                    await downloader.DownloadFileTaskAsync(DownloadUrl, fullPathwithFileName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
            }
        }

        public Task ExtractFile(string pathtoZIPFile, string ZIPFileName, string gameInstallPath, IProgress<ProgressHelper> extractProgress, bool hasExternalInstaller = false)
        {
            try
            {
                string fullPathwithFileName = Path.Combine(pathtoZIPFile, ZIPFileName);

                if (Path.GetExtension(ZIPFileName) != ".7z" && Path.GetExtension(ZIPFileName) != ".rar")
                {
                    File.Copy(fullPathwithFileName, Path.Combine(gameInstallPath, ZIPFileName), true);
                    return Task.CompletedTask;
                }
                else
                {
                    return Task.Run(() =>
                    {
                        using ArchiveFile archiveFile = new(fullPathwithFileName);

                        foreach (Entry entry in archiveFile.Entries)
                        {
                            counter++;
                            EntrySize = entry.Size;
                            EntryFilename = entry.FileName;
                            extractProgress.Report(new ProgressHelper() { CurrentFileName = EntryFilename, CurrentlyExtractedFileCount = counter, TotalArchiveFileCount = archiveFile.Entries.Count });

                            if (hasExternalInstaller)
                            {
                                entry.Extract(Path.Combine(ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2, Path.GetFileNameWithoutExtension(ZIPFileName), entry.FileName));
                            }
                            else
                            {
                                entry.Extract(Path.Combine(gameInstallPath, entry.FileName));
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
                return Task.FromException(ex);
            }
        }

        private void OnDownloadStarted(object? sender, DownloadStartedEventArgs e)
        {
            OverallProgress!.Report(new ProgressHelper() { CurrentFileName = EntryFilename, TotalDownloadSizeInBytes = e.TotalBytesToReceive });
        }

        private void OnDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            if (_DownloadProgressChangedLimiter < 1024)
            {
                _DownloadProgressChangedLimiter++;
            }
            else
            {
                OverallProgress!.Report(new ProgressHelper()
                {
                    PercentageValue = e.ProgressPercentage,
                    DownloadSpeedSizeInBytes = e.BytesPerSecondSpeed,
                    TotalDownloadSizeInBytes = e.TotalBytesToReceive,
                    ProgressedDownloadSizeInBytes = e.ReceivedBytesSize
                });

                _DownloadProgressChangedLimiter = 0;
            }
        }

        private void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OverallProgress!.Report(new ProgressHelper() { CurrentFileName = e.Error.Message });
                LogHelper.LoggerGameFileTools.Error(e.Error.Message);
            }
        }

        public static bool EnsureBFMEAppdataFolderExists(string assemblyName)
        {
            try
            {
                if (!Directory.Exists(RegistryService.GameAppdataFolderPath(assemblyName)))
                {
                    CreateBFMEAppdataFolder(assemblyName);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
                return false;
            }
        }

        private static void CreateBFMEAppdataFolder(string assemblyName)
        {
            try
            {
                Directory.CreateDirectory(RegistryService.GameAppdataFolderPath(assemblyName));
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
            }
        }

        public static void EnsureBFMEOptionsIniFileExists(string assemblyName)
        {
            try
            {
                if (!File.Exists(Path.Combine(RegistryService.GameAppdataFolderPath(assemblyName), ConstStrings.C_OPTIONSINI_FILENAME)))
                    File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_OPTIONSINI_FILENAME), Path.Combine(RegistryService.GameAppdataFolderPath(assemblyName), ConstStrings.C_OPTIONSINI_FILENAME));
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
            }
        }
    }
}