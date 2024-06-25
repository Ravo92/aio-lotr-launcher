using System.Diagnostics;
using System.IO;
using static AllInOneLauncher.Logic.LauncherGameSelectionManager;

namespace AllInOneLauncher.Logic
{
    internal static class BFMELaunchManager
    {
        internal static void LaunchGame(AvailableBFMEGames availableBFMEGames, bool windowed)
        {
            BFMESettingsManager.EnsureOptionsFile(availableBFMEGames);

            using Process? gameProcess = Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = BFMERegistryManager.GetBFMEInstallPath(availableBFMEGames),
                FileName = Path.Combine(BFMERegistryManager.GetBFMEInstallPath(availableBFMEGames), BFMERegistryManager.GetBFMEExecutableName(availableBFMEGames)),
                Arguments = windowed ? $"-win -xres {SystemDisplayManager.GetPrimaryScreenResolution().Width - 100} -yres {SystemDisplayManager.GetPrimaryScreenResolution().Height - 100}" : "",
            });

            if (gameProcess == null)
                return;

            SystemWindowManager.UpdateWindow(gameProcess.MainWindowHandle, 0, 0, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height, windowed);
            SystemInputManager.ConfineCursor(0, 0, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height, windowed);

            gameProcess.WaitForExit();

            SystemInputManager.ReleaseCursor();
        }
    }
}