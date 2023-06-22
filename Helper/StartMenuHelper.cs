using IWshRuntimeLibrary;

namespace Helper
{
    public class StartMenuHelper
    {
        public static void CreateShortcutToDesktop(string path, string linkName, string arguments = "", string description = "")
        {
            object shDesktop = "Desktop";
            WshShell shell = new();
            string shortcutAddress = Path.Combine((string)shell.SpecialFolders.Item(ref shDesktop), linkName + ".lnk");

            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = description;
            shortcut.TargetPath = path;
            shortcut.Arguments = arguments;
            shortcut.WorkingDirectory = Path.GetDirectoryName(path);
            shortcut.Save();
        }

        public static bool DoesTheShortCutExist(string path, string shortcutName)
        {
            if (System.IO.File.Exists(Path.Combine(path, shortcutName + ".lnk")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void CreateShortcutToStartMenu(string path, string linkName, string optionalSubPath = "", string arguments = "", string description = "")
        {
            WshShell shell = new();
            string shortcutAddress = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), optionalSubPath, linkName + ".lnk");

            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = description;
            shortcut.TargetPath = path;
            shortcut.Arguments = arguments;
            shortcut.WorkingDirectory = Path.GetDirectoryName(path);
            shortcut.Save();
        }
    }
}