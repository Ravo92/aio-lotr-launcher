using AllInOneLauncher.Elements;
using AllInOneLauncher.Popups;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace AllInOneLauncher.Logic
{
    public static class LauncherUpdateManager
    {
        private static readonly HttpClient HttpClient = new() { Timeout = TimeSpan.FromSeconds(20) };

        public static async void CheckForUpdates()
        {
            try
            {
                string latestVersionDataRaw = await HttpClient.GetStringAsync("https://ravo92.github.io/LauncherUpdater.xml");
                XmlDocument latestVersionDataXml = new();
                latestVersionDataXml.LoadXml(latestVersionDataRaw);

                Version latestVersion = new(latestVersionDataXml.GetElementsByTagName("version")[0]!.InnerText);
                Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version!;

                if (currentVersion < latestVersion)
                {
                    string url = latestVersionDataXml.GetElementsByTagName("url")[0]!.InnerText;
                    string args = latestVersionDataXml.GetElementsByTagName("args")[0]!.InnerText;

                    try
                    {
                        LauncherUpdatePopup updatePopup = new();
                        PopupVisualizer.ShowPopup(updatePopup);
                        await DownloadUpdate(url, (progress) =>
                        {
                            updatePopup.LoadProgress = progress;
                            return false;
                        });
                        Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updates", Path.GetFileName(url)), args);
                        App.ExitImmediately();
                    }
                    catch (Exception ex)
                    {
# warning PopupVisualizer.ShowPopup(new MessagePopup("ERROR", $"An unexpected error had occurred while updating.\n{ex}"));
                    }
                }
            }
            catch { }
        }

        private static async Task DownloadUpdate(string url, Func<int, bool> OnProgressUpdate)
        {
            using var response = await HttpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength ?? -1L;
            var totalBytesRead = 0L;
            var buffer = new byte[4096];
            var isMoreToRead = true;
            int progressInPercent = 0;

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updates")))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updates"));

            using var fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updates", Path.GetFileName(url)), FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
            using var stream = await response.Content.ReadAsStreamAsync();
            do
            {
                var bytesRead = await stream.ReadAsync(buffer);
                if (bytesRead == 0)
                {
                    isMoreToRead = false;
                    continue;
                }

                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));

                totalBytesRead += bytesRead;
                int newProgressInPercent = (int)(totalBytesRead * 100 / totalBytes);

                if (progressInPercent != newProgressInPercent)
                {
                    progressInPercent = newProgressInPercent;
                    if (OnProgressUpdate.Invoke(newProgressInPercent))
                        return;
                }
            }
            while (isMoreToRead);
        }
    }
}