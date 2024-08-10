using System;
using System.Diagnostics;
using System.IO;
using AllInOneLauncher.Data;
using BfmeFoundationProject.BfmeRegistryManagement;
using BfmeFoundationProject.BfmeRegistryManagement.Data;

namespace AllInOneLauncher.Logic
{
    internal static class BfmeLaunchManager
    {
        internal static void LaunchGame(BfmeGame game, bool windowed)
        {
            BfmeRegistryManager.EnsureDefaults((int)game);

            using Process? gameProcess = Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = BfmeRegistryManager.GetKeyValue((int)game, BfmeRegistryKey.InstallPath),
                FileName = Path.Combine(BfmeRegistryManager.GetKeyValue((int)game, BfmeRegistryKey.InstallPath), BfmeDefaults.DefaultGameExecutableNames[(int)game]),
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