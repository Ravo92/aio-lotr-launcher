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

            startInfo.ArgumentList.Add("-win");

            if (windowed)
                startInfo.ArgumentList.Add($"-xres {SystemDisplayManager.GetPrimaryScreenResolution().Width - 100} -yres {SystemDisplayManager.GetPrimaryScreenResolution().Height - 100}");
            else
                startInfo.ArgumentList.Add($"-xres {SystemDisplayManager.GetPrimaryScreenResolution().Width} -yres {SystemDisplayManager.GetPrimaryScreenResolution().Height}");

            if (BfmeWorkshopSyncManager.GetActiveModPath((int)game) != null) startInfo.ArgumentList.Add($"-mod {BfmeWorkshopSyncManager.GetActiveModPath((int)game)}");

            using Process? gameProcess = Process.Start(startInfo);
            if (gameProcess == null) return;

            if (game == BfmeGame.BFME1)
                SystemGameWindowManager.RemoveWindowBorder("E99E8455-CC9B-488a-BA22-0E8A8F74F9FA", 0, 0, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height);

            SystemInputManager.ConfineCursor(0, 0, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height, false);

            gameProcess.WaitForExit();

            SystemInputManager.ReleaseCursor();
        }
    }
}