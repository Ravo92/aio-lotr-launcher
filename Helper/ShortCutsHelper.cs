using Serilog.Core;
using Serilog;
using WindowsShortcutFactory;

namespace Helper
{
    public class ShortCutsHelper
    {
        readonly Logger _log = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_SHORTCUTS_NAME)).CreateLogger();

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

        public void CreateShortcutToDesktop(string path, string linkName, string arguments = "", string description = "")
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
                _log.Error(ex.ToString());
            }
        }

        public void CreateShortcutToStartMenu(string path, string linkName, string optionalSubPath = "", string arguments = "", string description = "")
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
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), optionalSubPath)))
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), optionalSubPath));

                _windowsShortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), optionalSubPath, linkName + ".lnk"));
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        public void DeleteGameShortcutFromDesktop()
        {
            try
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_GAMETITLE_NAME_EN + ".lnk"));
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        public void DeleteGameShortcutsFromStartMenu()
        {
            try
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN, ConstStrings.C_GAMETITLE_NAME_EN + ".lnk"));
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN, "Worldbuilder" + ".lnk"));
                Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN));
            }
            catch (Exception ex)
            {

                _log.Error(ex.ToString());
            }
        }

        public void DeleteLauncherShortcutFromDesktop()
        {
            try
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME + ".lnk"));
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        public void DeleteLauncherShortcutsFromStartMenu()
        {
            try
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Patch 2.22 Launcher", ConstStrings.C_LAUNCHER_SHORTCUT_NAME + ".lnk"));
                Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Path.Combine("Programs", "Patch 2.22 Launcher")));
            }
            catch (Exception ex)
            {

                _log.Error(ex.ToString());
            }
        }
    }
}