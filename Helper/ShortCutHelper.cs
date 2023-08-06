using WindowsShortcutFactory;

namespace Helper
{
    public class ShortCutHelper
    {
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

            try
            {
                _windowsShortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), linkName + ".lnk"));
            }
            catch (Exception ex)
            {
                LogHelper.LoggerShortcuts.Error(ex, "");
            }
        }

        public static void CreateShortcutToStartMenu(string path, string linkName, string optionalSubPath = "", string arguments = "", string description = "")
        {
            string shortcutTargetDirectory = (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), optionalSubPath));

            using WindowsShortcut _windowsShortcut = new()
            {
                Path = path,
                Arguments = arguments,
                WorkingDirectory = Path.GetDirectoryName(path),
                IconLocation = path,
                Description = description
            };

            try
            {
                if (!Directory.Exists(shortcutTargetDirectory))
                    Directory.CreateDirectory(shortcutTargetDirectory);

                _windowsShortcut.Save(Path.Combine(shortcutTargetDirectory, linkName + ".lnk"));
            }
            catch (Exception ex)
            {
                LogHelper.LoggerShortcuts.Error(ex, "");
            }
        }

        public static void DeleteGameShortcutFromDesktop()
        {
            try
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), RegistryService.ReadRegKeyBFME2("displayName") + ".lnk"));
            }
            catch (Exception ex)
            {
                LogHelper.LoggerShortcuts.Error(ex, "");
            }
        }

        public static void DeleteGameShortcutsFromStartMenu()
        {
            try
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", RegistryService.ReadRegKeyBFME2("displayName"), RegistryService.ReadRegKeyBFME2("displayName") + ".lnk"));
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", RegistryService.ReadRegKeyBFME2("displayName"), "Worldbuilder" + ".lnk"));
                Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", RegistryService.ReadRegKeyBFME2("displayName")));
            }
            catch (Exception ex)
            {

                LogHelper.LoggerShortcuts.Error(ex, "");
            }
        }

        public static void DeleteLauncherShortcutFromDesktop()
        {
            try
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME + ".lnk"));
            }
            catch (Exception ex)
            {
                LogHelper.LoggerShortcuts.Error(ex, "");
            }
        }

        public static void DeleteLauncherShortcutsFromStartMenu()
        {
            try
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Patch 2.22 Launcher", ConstStrings.C_LAUNCHER_SHORTCUT_NAME + ".lnk"));
                Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Path.Combine("Programs", "Patch 2.22 Launcher")));
            }
            catch (Exception ex)
            {

                LogHelper.LoggerShortcuts.Error(ex, "");
            }
        }
    }
}