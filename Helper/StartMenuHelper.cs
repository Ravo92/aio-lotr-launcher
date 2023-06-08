﻿namespace Helper
{
    public class StartMenuHelper
    {
        public static void CreateGameShortcutToDesktop(string linkName)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using StreamWriter writer = new(deskDir + "\\" + linkName + ".url");
            string app = Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_GAME_FILE);
            writer.WriteLine("[InternetShortcut]");
            writer.WriteLine("URL=file:///" + app);
            writer.WriteLine("IconIndex=0");
            string icon = app.Replace('\\', '/');
            writer.WriteLine("IconFile=" + icon);
        }

        public static void CreateGameShortcutToStartMenu(string linkName, string exeFile)
        {
            string startMenuFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string startMenuFolderPathGameDirectory = "Programs\\Electronic Arts";
            string gameFolderName = ConstStrings.C_GAMETITLE_NAME_EN;
            string combinedPath = Path.Combine(startMenuFolderPath, startMenuFolderPathGameDirectory, gameFolderName);

            Directory.CreateDirectory(combinedPath);

            using StreamWriter writer = new(combinedPath + "\\" + linkName + ".url");
            string app = Path.Combine(ConstStrings.GameInstallPath(), exeFile);
            writer.WriteLine("[InternetShortcut]");
            writer.WriteLine("URL=file:///" + app);
            writer.WriteLine("IconIndex=0");
            string icon = app.Replace('\\', '/');
            writer.WriteLine("IconFile=" + icon);
        }
    }
}