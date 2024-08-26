﻿using System;
using System.Diagnostics;
using System.IO;
using BfmeFoundationProject.WorkshopKit.Logic;
using static AllInOneLauncher.Logic.BfmeRegistryManager;

namespace AllInOneLauncher.Logic
{
    internal static class BfmeLaunchManager
    {
        internal static async void LaunchGame(Data.BfmeGame game, int displayMode)
        {
            ProcessStartInfo startInfo = new()
            {
                WorkingDirectory = GetKeyValue(game, BfmeRegistryKey.InstallPath),
                FileName = Path.Combine(GetKeyValue(game, BfmeRegistryKey.InstallPath), GameInfos[game].DefaultRegistryKey) // BfmeDefaults.DefaultGameExecutableNames[(int)game])
            };

            if (displayMode == 1)
            {
                startInfo.ArgumentList.Add("-win");
                startInfo.ArgumentList.Add("-xres");
                startInfo.ArgumentList.Add(SystemDisplayManager.GetPrimaryScreenResolution().Width.ToString());
                startInfo.ArgumentList.Add("-yres");
                startInfo.ArgumentList.Add((SystemDisplayManager.GetPrimaryScreenResolution().Height - 29).ToString());
            }
            else if (displayMode == 2)
            {
                startInfo.ArgumentList.Add("-win");
            }

            string? activeModPath = await BfmeWorkshopStateManager.GetActiveModPath((int)game);
            if (activeModPath != null)
            {
                startInfo.ArgumentList.Add("-mod");
                startInfo.ArgumentList.Add(activeModPath);
            }

            using Process? gameProcess = Process.Start(startInfo);
            if (gameProcess == null) return;

            if (displayMode == 1)
            {
                IntPtr windowHandle = SystemGameWindowManager.FindWindowByClassName("E99E8455-CC9B-488a-BA22-0E8A8F74F9FA");
                SystemGameWindowManager.RemoveWindowBorder(windowHandle, 0, 0);
            }

            gameProcess.WaitForExit();
        }
    }
}