using Downloader;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Helper
{
    public class GameFileTools
    {
        public delegate void SetTextDLSpeedCallback(string text);
        public delegate void SetTextFileNameCallback(string text);

        public delegate void SetPBarFilesCallback(int value);
        public delegate void SetPBarFilesMaxCallback(int value);
        public delegate void SetPBarPercentagesCallback(string value);

        public event SetPBarFilesCallback SetPBarFilesCallbackEvent;
        public event SetPBarFilesMaxCallback SetPBarFilesMaxCallbackEvent;
        public event SetPBarPercentagesCallback SetPBarPercentagesCallbackEvent;

        public event SetTextDLSpeedCallback SetTextDLSpeedCallbackEvent;
        public event SetTextFileNameCallback SetTextFileNameCallbackEvent;

        private int _DownloadProgressChangedLimiter = 0;

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
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                await file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }

            // Check if JSON file exists and if the values are identical with remote file, if not, save new remote file as local json file
            if (json == "noInternet")
            {
                return _gameFileDictionary;
            }
            else
            {
                // write JSON directly to a file
                using StreamWriter file = File.CreateText(Path.Combine(Application.StartupPath, "GameFileDictionary.json"));
                await file.WriteAsync(json);
                return _gameFileDictionary;
            }
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
                return "noInternet";
            }
        }

        public async Task DownloadFile(string pathtoZIPFile, string ZIPFileName, string DownloadUrl)
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

                var downloader = new DownloadService(downloadOpt);

                downloader.DownloadStarted += OnDownloadStarted;
                downloader.DownloadProgressChanged += OnDownloadProgressChanged;
                downloader.DownloadFileCompleted += OnDownloadFileCompleted;

                if (!File.Exists(fullPathwithFileName))
                {
                    await downloader.DownloadFileTaskAsync(DownloadUrl, Path.Combine(fullPathwithFileName, ZIPFileName));
                }
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                await file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }
        }

        public async Task ExtractFile(string pathtoZIPFile, string ZIPFileName, string gameInstallPath)
        {
            try
            {
                string fullPathwithFileName = Path.Combine(pathtoZIPFile, ZIPFileName);

                var progressHandler = new Progress<ProgressHelper>(progress =>
                {
                    SetPBarFilesCallbackEvent(progress.Count);
                    SetPBarFilesMaxCallbackEvent(progress.Max);
                    SetTextFileNameCallbackEvent(string.Concat(progress.Count, "/", progress.Max));
                    SetTextDLSpeedCallbackEvent(progress.Filename!);
                });

                ZIPFileHelper _ZIPFileHelper = new();
                await _ZIPFileHelper.ExtractArchive(fullPathwithFileName, gameInstallPath, progressHandler)!;
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                await file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }
        }

        private void OnDownloadStarted(object? sender, DownloadStartedEventArgs e)
        {
            SetPBarFilesCallbackEvent(0);
            SetTextFileNameCallbackEvent("Downloading: " + Path.GetFileName(e.FileName));
        }

        private void OnDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            if (_DownloadProgressChangedLimiter < 64)
            {
                _DownloadProgressChangedLimiter++;
            }
            else
            {
                SetPBarFilesCallbackEvent((int)e.ProgressPercentage);
                SetTextDLSpeedCallbackEvent("@ " + Math.Round(e.AverageBytesPerSecondSpeed / 1024000).ToString() + " MB/s");
                SetPBarPercentagesCallbackEvent(Math.Round(e.ProgressPercentage).ToString() + " %");

                _DownloadProgressChangedLimiter = 0;
            }
        }

        private void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                SetTextFileNameCallbackEvent(e.Error.Message);
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(e.Error.Message);
            }
            else
            {
                SetPBarFilesCallbackEvent(100);
            }
        }
    }
}