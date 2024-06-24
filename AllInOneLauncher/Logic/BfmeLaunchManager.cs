using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AllInOneLauncher.Logic
{
    public static class BfmeLaunchManager
    {
        public static void LaunchGame(int game, bool windowed)
        {
            using Process? gameProcess = Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = BfmeRegistryManager.GetBfmeInstallPath(game),
                FileName = Path.Combine(BfmeRegistryManager.GetBfmeInstallPath(game), BfmeRegistryManager.GetBfmeExecutableName(game)),
                Arguments = windowed ? $"-win -xres {SystemDisplayManager.GetPrimaryScreenResolution().Width} -yres {SystemDisplayManager.GetPrimaryScreenResolution().Height}" : "-win",
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
