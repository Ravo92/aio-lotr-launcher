using WindowsShortcutFactory;

namespace Helper
{
    public class StartMenuHelper
    {
        public static void CreateShortcutToDesktop(string path, string linkName, string arguments = "", string description = "")
        {
            using WindowsShortcut _windowsShortcut = new()
            {
                Path = path,
                Arguments = arguments,
                WorkingDirectory = Path.GetDirectoryName(path),
                IconLocation = path,
                Description = description
            };

            _windowsShortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), linkName + ".lnk"));
        }

        public static bool DoesTheShortCutExist(string path, string shortcutName)
        {
            if (File.Exists(Path.Combine(path, shortcutName + ".lnk")))
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
            using WindowsShortcut _windowsShortcut = new()
            {
                Path = path,
                Arguments = arguments,
                WorkingDirectory = Path.GetDirectoryName(path),
                IconLocation = path,
                Description = description
            };

            _windowsShortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), optionalSubPath, linkName + ".lnk"));
        }
    }
}