using Downloader;
using Newtonsoft.Json;
using System.ComponentModel;
using SevenZipExtractor;
using System.Reflection;

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
            string json = "noInternet";

            try
            {
                // Try deserializing the JSON file from remote into local GameFileDictionary class objects
                json = await DownloadJSONFile();

                // Check if JSON file exists and if the values are identical with remote file, if not, save new remote file as local json file
                if (json == "noInternet")
                {
                    if (File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_JSON_GAMEDICTIONARY_MAIN_FILE)))
                    {
                        json = File.ReadAllText(Path.Combine(Application.StartupPath, ConstStrings.C_JSON_GAMEDICTIONARY_MAIN_FILE));
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
                LogHelper.LoggerGameFileTools.Error(ex, "");
            }

            try
            {
                // write JSON directly to a file
                using StreamWriter file = File.CreateText(Path.Combine(Application.StartupPath, ConstStrings.C_JSON_GAMEDICTIONARY_MAIN_FILE));
                await file.WriteAsync(json);
            }
            catch (UnauthorizedAccessException ex)
            {
                LogHelper.LoggerGameFileTools.Fatal(ex, "Cant Access local json file! Data may not be accurate!");
                Environment.Exit(1);
            }

            return JsonConvert.DeserializeObject<GameFileDictionary>(json)!;
        }

        public static async Task<string> DownloadJSONFile()
        {
            try
            {
                // TODO: Write better network connection detection -> Ping 1.1.1.1 or check network adapter state.
                using var _httpClient = new HttpClient();
                _httpClient.Timeout = TimeSpan.FromSeconds(3);
                string json = await _httpClient.GetStringAsync($"https://ravo92.github.io/{ConstStrings.C_JSON_GAMEDICTIONARY_MAIN_FILE}");
                return json;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
                return "noInternet";
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
                return "noInternet";
            }
        }

        public async Task DownloadFile(string pathtoZIPFile, string ZIPFileName, List<string> DownloadUrls, int downloadUrlCount, IProgress<ProgressHelper> downloadProgress, string assemblyName)
        {
            try
            {
                string DownloadUrl = DownloadUrls[downloadUrlCount]; //[new Random().Next(DownloadUrls.Length)];
                LogHelper.LoggerGameFileTools.Information("Downloading from URI: < {0} >", DownloadUrl);

                string fullPathwithFileName = Path.Combine(pathtoZIPFile, ZIPFileName);
                LogHelper.LoggerGameFileTools.Information("Downloading into file: < {0} >", fullPathwithFileName);

                var downloadOpt = new DownloadConfiguration()
                {
                    ParallelDownload = true,
                    ReserveStorageSpaceBeforeStartingDownload = true,
                    ClearPackageOnCompletionWithFailure = true,
                    MaxTryAgainOnFailover = 1,
                    Timeout = 5000,
                    ChunkCount = 8,
                    RequestConfiguration =
                    {
                        KeepAlive = true,
                        ProtocolVersion = System.Net.HttpVersion.Version11,
                        UserAgent = assemblyName + " on Version: " + Assembly.GetEntryAssembly()!.GetName().Version
                    }
                };

                OverallProgress = downloadProgress;
                var downloader = new DownloadService(downloadOpt);
                LogHelper.LoggerGameFileTools.Debug(downloader.Status.ToString());

                downloader.DownloadStarted += OnDownloadStarted;
                downloader.DownloadProgressChanged += OnDownloadProgressChanged;
                downloader.DownloadFileCompleted += OnDownloadFileCompleted;

                LogHelper.LoggerGameFileTools.Debug(downloader.Status.ToString());

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
            try
            {
                OverallProgress!.Report(new ProgressHelper() { CurrentFileName = EntryFilename, TotalDownloadSizeInBytes = e.TotalBytesToReceive });
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
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
                        PercentageValue = e.ProgressPercentage,
                        DownloadSpeedSizeInBytes = e.BytesPerSecondSpeed,
                        TotalDownloadSizeInBytes = e.TotalBytesToReceive,
                        ProgressedDownloadSizeInBytes = e.ReceivedBytesSize
                    });

                    _DownloadProgressChangedLimiter = 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
            }
        }

        private void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    OverallProgress!.Report(new ProgressHelper() { CurrentFileName = e.Error.Message });
                    LogHelper.LoggerGameFileTools.Error(e.Error.Message);
                }
            }

            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
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
                LogHelper.LoggerGameFileTools.Information("check if options.ini file for game > {0} < in path > {1} < exists...", assemblyName, Path.Combine(RegistryService.GameAppdataFolderPath(assemblyName), ConstStrings.C_OPTIONSINI_FILENAME));
                if (!File.Exists(Path.Combine(RegistryService.GameAppdataFolderPath(assemblyName), ConstStrings.C_OPTIONSINI_FILENAME)))
                    LogHelper.LoggerGameFileTools.Information("It does not exist, so we create it now...");
                File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_OPTIONSINI_FILENAME), Path.Combine(RegistryService.GameAppdataFolderPath(assemblyName), ConstStrings.C_OPTIONSINI_FILENAME));
                LogHelper.LoggerGameFileTools.Information("sucessfully created options.ini file in < {0} >", Path.Combine(RegistryService.GameAppdataFolderPath(assemblyName), ConstStrings.C_OPTIONSINI_FILENAME));
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGameFileTools.Error(ex, "");
            }
        }
    }
}