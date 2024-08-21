using System;
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
            IntPtr windowHandle;
            ProcessStartInfo startInfo = new()
            {
                WorkingDirectory = BfmeRegistryManager.GetKeyValue((int)game, BfmeRegistryKey.InstallPath),
                FileName = Path.Combine(BfmeRegistryManager.GetKeyValue((int)game, BfmeRegistryKey.InstallPath), BfmeDefaults.DefaultGameExecutableNames[(int)game])
            };

            if (windowed)
                startInfo.Arguments = $"-win -xres {SystemDisplayManager.GetPrimaryScreenResolution().Width - 100} -yres {SystemDisplayManager.GetPrimaryScreenResolution().Height - 110}";
            else if (Properties.Settings.Default.IsWindowed)
                startInfo.Arguments = $"-win -xres {SystemDisplayManager.GetPrimaryScreenResolution().Width} -yres {SystemDisplayManager.GetPrimaryScreenResolution().Height}";

            if (BfmeWorkshopSyncManager.GetActiveModPath((int)game) != null) startInfo.ArgumentList.Add($"-mod {BfmeWorkshopSyncManager.GetActiveModPath((int)game)}");

            using Process? gameProcess = Process.Start(startInfo);
            if (gameProcess == null) return;

            windowHandle = SystemGameWindowManager.FindWindowByClassName("E99E8455-CC9B-488a-BA22-0E8A8F74F9FA");

            if (Properties.Settings.Default.IsWindowed && !windowed)
            {
                SystemGameWindowManager.RemoveWindowBorder(windowHandle, SystemDisplayManager.GetPrimaryScreenResolution().Width, SystemDisplayManager.GetPrimaryScreenResolution().Height, 0, 0);
            }
            else
            {
                SystemGameWindowManager.SetWindowPos(windowHandle, IntPtr.Zero, 25, 25, 0, 0, 0x0001 | 0x0020 | 0x0040);
                SystemInputManager.SetTargetHWnd(windowHandle);
            }

            gameProcess.WaitForExit();
        }
    }
}