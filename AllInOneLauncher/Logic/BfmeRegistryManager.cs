using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace AllInOneLauncher.Logic
{
    public static class BfmeRegistryManager
    {
        public static bool IsBfmeInstalled(int game) => GetBfmeInstallPath(game) != "";
        public static string GetBfmeLanguage(int game) => GetBfmeRegistryKeyValue(game, "Language");
        public static string GetBfmeInstallPath(int game) => GetBfmeRegistryKeyValue(game, "Install Dir");
        public static string GetBfmeDataPath(int game) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetBfmeRegistryKeyValue(game, "UserDataLeafName"));
        public static string GetBfmeExecutableName(int game) => $"lotrbfme{(game > 0 ? "2" : "")}{(game > 1 ? "ep1" : "")}.exe";
        public static string GetBfmeSerialKey(int game) => GetBfmeRegistryKeyValue(game, "", true, @"\ergc");

        public static void SetBfmeLanguage(int game, string language) => SetBfmeRegistryKeyValue(game, "Language", language);
        public static void SetBfmeInstallPath(int game, string path) => SetBfmeRegistryKeyValue(game, "Install Dir", path);
        public static void CreateBfmeInstallRegistry(int game, string installPath, string language)
        {
            if (game == 0)
                CreateBfme1InstallRegistry(installPath, language);
            else if (game == 1)
                CreateBfme2InstallRegistry(installPath, language);
            else if (game == 2)
                CreateRotwkInstallRegistry(installPath, language);

            EnsureBfmeAppRegistry(game);
        }
        public static void EnsureBfmeAppRegistry(int game)
        {
            try
            {
                if (game == 0)
                    EnsureBfme1AppRegistry();
                else if (game == 1)
                    EnsureBfme2AppRegistry();
                else if (game == 2)
                    EnsureRotwkAppRegistry();
            }
            catch { }
        }

        private static void CreateBfme1InstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\EA GAMES\The Battle for Middle-earth", false);
            using RegistryKey? keyGameMain = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\EA GAMES\The Battle for Middle-earth", true);
            keyGameMain?.SetValue("CacheSize", "3351006208");
            keyGameMain?.SetValue("DisplayName", "The Battle for Middle-earth");
            keyGameMain?.SetValue("Install Dir", installPath);
            keyGameMain?.SetValue("Language", language);
            keyGameMain?.SetValue("Locale", "en");
            keyGameMain?.SetValue("Product GUID", "{3F290582-3F4E-4B96-009C-E0BABAA40C42}");
            keyGameMain?.SetValue("Region", "EUROPE");
            keyGameMain?.SetValue("Registration", @"SOFTWARE\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc");
            keyGameMain?.SetValue("Suppression Exe", "rtsi.exe");
            keyGameMain?.SetValue("SwapSize", "0");

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA Games\The Battle for Middle-earth", false);
            using RegistryKey? keyGameAlt = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA Games\The Battle for Middle-earth", true);
            keyGameAlt?.SetValue("InstallPath", installPath);
            keyGameAlt?.SetValue("Language", language);
            keyGameAlt?.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UserDataLeafName", "My Battle for Middle-earth Files");
            keyGameAlt?.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files"));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files", "Options.ini"), BfmeSettingsManager.DefaultOptions);
        }

        private static void EnsureBfme1AppRegistry()
        {
            string installPath = GetBfmeRegistryKeyValue(0, "Install Dir");

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, "lotrbfme.exe"));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\EA GAMES\The Battle for Middle-earth");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files"));
        }

        private static void CreateBfme2InstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Battle for Middle-earth II", false);
            using RegistryKey? keyGameMain = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Battle for Middle-earth II", true);
            keyGameMain?.SetValue("CacheSize", "3351006208");
            keyGameMain?.SetValue("DisplayName", "The Battle for Middle-earth II");
            keyGameMain?.SetValue("Install Dir", installPath);
            keyGameMain?.SetValue("Language", language);
            keyGameMain?.SetValue("Locale", "en");
            keyGameMain?.SetValue("Product GUID", "{2A9F95AB-65A3-432c-8631-B8BC5BF7477A}");
            keyGameMain?.SetValue("Region", "EUROPE");
            keyGameMain?.SetValue("Registration", @"SOFTWARE\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc");
            keyGameMain?.SetValue("Suppression Exe", "rtsi.exe");
            keyGameMain?.SetValue("SwapSize", "0");

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II", false);
            using RegistryKey? keyGameAlt = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II", true);
            keyGameAlt?.SetValue("InstallPath", installPath);
            keyGameAlt?.SetValue("Language", language);
            keyGameAlt?.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UserDataLeafName", "My Battle for Middle-earth II Files");
            keyGameAlt?.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files"));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files", "Options.ini"), BfmeSettingsManager.DefaultOptions);
        }

        private static void EnsureBfme2AppRegistry()
        {
            string installPath = GetBfmeRegistryKeyValue(1, "Install Dir");

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, "lotrbfme2.exe"));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Battle for Middle-earth II");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files"));
        }

        private static void CreateRotwkInstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", false);
            using RegistryKey? keyGameMain = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", true);
            keyGameMain?.SetValue("CacheSize", "3351006208");
            keyGameMain?.SetValue("DisplayName", "The Rise of the Witch-king");
            keyGameMain?.SetValue("Install Dir", installPath);
            keyGameMain?.SetValue("Language", language);
            keyGameMain?.SetValue("Locale", "en");
            keyGameMain?.SetValue("Product GUID", "{B931FB80-537A-4600-00AD-AC5DEDB6C25B}");
            keyGameMain?.SetValue("Region", "EUROPE");
            keyGameMain?.SetValue("Registration", @"SOFTWARE\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc");
            keyGameMain?.SetValue("Suppression Exe", "rtsi.exe");
            keyGameMain?.SetValue("SwapSize", "0");

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", false);
            using RegistryKey? keyGameAlt = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", true);
            keyGameAlt?.SetValue("InstallPath", installPath);
            keyGameAlt?.SetValue("Language", language);
            keyGameAlt?.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UserDataLeafName", "My Rise of the Witch-king Files");
            keyGameAlt?.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files"));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files", "Options.ini"), BfmeSettingsManager.DefaultOptions);
        }

        private static void EnsureRotwkAppRegistry()
        {
            string installPath = GetBfmeRegistryKeyValue(2, "Install Dir");

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2ep1.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2ep1.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, "lotrbfme2ep1.exe"));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files"));
        }

        private static string GetBfmeRegistryKeyValue(int game, string key, bool useAltKey = false, string keySufix = "")
        {
            string gameRegistry = "";
            if (game == 0)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\EA Games\The Battle for Middle-earth" : @"EA Games\The Battle for Middle-earth")}{keySufix}";
            else if (game == 1)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Battle for Middle-earth II" : @"Electronic Arts\The Battle for Middle-earth II")}{keySufix}";
            else if (game == 2)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king" : @"Electronic Arts\The Lord of the Rings, The Rise of the Witch-king")}{keySufix}";

            using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(gameRegistry, false);
            string result = registryKey?.GetValue(key) as string ?? "";
            return result;
        }

        private static void SetBfmeRegistryKeyValue(int game, string key, string value, bool useAltKey = false, string keySufix = "")
        {
            string gameRegistry = "";
            if (game == 0)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\EA Games\The Battle for Middle-earth" : @"EA Games\The Battle for Middle-earth")}{keySufix}";
            else if (game == 1)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Battle for Middle-earth II" : @"Electronic Arts\The Battle for Middle-earth II")}{keySufix}";
            else if (game == 2)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king" : @"Electronic Arts\The Lord of the Rings, The Rise of the Witch-king")}{keySufix}";

            using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(gameRegistry, true);
            registryKey?.SetValue(key, value);
        }

        private static string RandomString(int length) => new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }
}
