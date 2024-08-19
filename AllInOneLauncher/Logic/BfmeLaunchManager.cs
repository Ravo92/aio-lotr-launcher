using System.Diagnostics;
using System.IO;
using AllInOneLauncher.Data;
using BfmeFoundationProject.BfmeRegistryManagement;
using BfmeFoundationProject.BfmeRegistryManagement.Data;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace AllInOneLauncher.Logic
{
    internal static class BfmeLaunchManager
    {
        internal static void LaunchGame(BfmeGame game, bool windowed)
        {
            ProcessStartInfo startInfo = new()
            {
                WorkingDirectory = BfmeRegistryManager.GetKeyValue((int)game, BfmeRegistryKey.InstallPath),
                FileName = Path.Combine(BfmeRegistryManager.GetKeyValue((int)game, BfmeRegistryKey.InstallPath), BfmeDefaults.DefaultGameExecutableNames[(int)game])
            };
            if (windowed) startInfo.ArgumentList.Add($"-win -xres {SystemDisplayManager.GetPrimaryScreenResolution().Width - 100} -yres {SystemDisplayManager.GetPrimaryScreenResolution().Height - 100}");
            if (BfmeWorkshopSyncManager.GetActiveModPath((int)game) != null) startInfo.ArgumentList.Add($"-mod {BfmeWorkshopSyncManager.GetActiveModPath((int)game)}");

            using Process? gameProcess = Process.Start(startInfo);
            if (gameProcess == null) return;

            SystemWindowManager.UpdateWindow(gameProcess.MainWindowHandle, 0, 0, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height, windowed);
            SystemInputManager.ConfineCursor(0, 0, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height, windowed);

            gameProcess.WaitForExit();

            SystemInputManager.ReleaseCursor();
        }
    }
}