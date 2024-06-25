using System;
using System.Diagnostics;
using System.IO;
using AllInOneLauncher.Data;

namespace AllInOneLauncher.Logic
{
    internal static class BfmeLaunchManager
    {
        internal static void LaunchGame(BfmeGame game, bool windowed)
        {
            BfmeSettingsManager.EnsureOptionsFile(game);

            using Process? gameProcess = Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = BfmeRegistryManager.GetBfmeInstallPath(game),
                FileName = Path.Combine(BfmeRegistryManager.GetBfmeInstallPath(game), GetBfmeExecutableName(game)),
                Arguments = windowed ? $"-win -xres {SystemDisplayManager.GetPrimaryScreenResolution().Width - 100} -yres {SystemDisplayManager.GetPrimaryScreenResolution().Height - 100}" : "",
            });

            if (gameProcess == null)
                return;

            SystemWindowManager.UpdateWindow(gameProcess.MainWindowHandle, 0, 0, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height, windowed);
            SystemInputManager.ConfineCursor(0, 0, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height, windowed);

            gameProcess.WaitForExit();

            SystemInputManager.ReleaseCursor();
        }

        private static string GetBfmeExecutableName(BfmeGames game)
        {
            return game switch
            {
                BfmeGames.BFME1 => Constants.C_BFME1_EXECUTABLE,
                BfmeGames.BFME2 => Constants.C_BFME2_EXECUTABLE,
                BfmeGames.ROTWK => Constants.C_ROTWK_EXECUTABLE,
                _ => throw new ArgumentOutOfRangeException(nameof(game), game, null)
            };
        }
    }
}