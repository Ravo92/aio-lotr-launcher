using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LauncherGUI.Helpers
{
    internal class GameInstallerHelper
    {
        internal event EventHandler<double>? ProgressChanged;
        internal event EventHandler<string>? StatusChanged;

        internal async Task InstallGame(GameSelectorHelper.AvailableBFMEGames availableBFMEGames)
        {
            string jsonUrl = GetJsonUrlForGame(availableBFMEGames);
            List<GameFileDictionary> gameFiles = await DownloadGameFilesList(jsonUrl);

            if (gameFiles == null)
                return;

            int totalCount = gameFiles.Count;
            int currentCount = 0;

            foreach (GameFileDictionary gameFile in gameFiles)
            {
                double progress = Math.Round((double)currentCount / totalCount * 100, 0);
                string status = System.IO.Path.GetFileName(gameFile.FileName);

                ProgressChanged?.Invoke(this, progress);
                StatusChanged?.Invoke(this, status);

                await DownloadGameFile(availableBFMEGames, gameFile);

                currentCount++;
            }
        }

        private static async Task<List<GameFileDictionary>> DownloadGameFilesList(string url)
        {
            try
            {
                string json = await GameFileToolsHelper.DownloadJSONFile(url) ?? throw new Exception("JSON string is null.");
                return JsonConvert.DeserializeObject<List<GameFileDictionary>>(json) ?? [];
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task DownloadGameFile(GameSelectorHelper.AvailableBFMEGames game, GameFileDictionary gameFile)
        {
            await GameFileToolsHelper.DownloadFile(GetInstallPath(game), gameFile.FileName, gameFile.FileURL);
        }

        private static string GetJsonUrlForGame(GameSelectorHelper.AvailableBFMEGames game)
        {
            return game switch
            {
                GameSelectorHelper.AvailableBFMEGames.BFME1 => "https://bfmelauncherfiles.ravonator.at/LauncherJson/BFME1BaseGameFiles.json",
                GameSelectorHelper.AvailableBFMEGames.BFME2 => "https://bfmelauncherfiles.ravonator.at/LauncherJson/BFME2BaseGameFiles.json",
                GameSelectorHelper.AvailableBFMEGames.ROTWK => "https://bfmelauncherfiles.ravonator.at/LauncherJson/RotwkBaseGameFiles.json",
                _ => throw new ArgumentOutOfRangeException(nameof(game), game, null)
            };
        }

        private static string GetInstallPath(GameSelectorHelper.AvailableBFMEGames game)
        {
            return game switch
            {
                GameSelectorHelper.AvailableBFMEGames.BFME1 => BFMERegistryHelper.ReadRegKeyBFME1("path"),
                GameSelectorHelper.AvailableBFMEGames.BFME2 => BFMERegistryHelper.ReadRegKeyBFME2("path"),
                GameSelectorHelper.AvailableBFMEGames.ROTWK => BFMERegistryHelper.ReadRegKeyROTWK("path"),
                _ => throw new ArgumentOutOfRangeException(nameof(game), game, null)
            };
        }
    }
}