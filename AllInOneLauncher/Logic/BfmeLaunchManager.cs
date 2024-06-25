using System.Diagnostics;
using System.IO;
using static AllInOneLauncher.Logic.LauncherGameSelectionManager;

namespace AllInOneLauncher.Logic
{
    internal static class BfmeLaunchManager
    {
        internal static void LaunchGame(BfmeGame game, bool windowed)
        {
            BfmeSettingsManager.EnsureOptionsFile(game);

            using Process? gameProcess = Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = BfmeRegistryManager.GetBFMEInstallPath(game),
                FileName = Path.Combine(BfmeRegistryManager.GetBFMEInstallPath(game), BfmeRegistryManager.GetBFMEExecutableName(game)),
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