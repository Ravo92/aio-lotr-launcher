using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using static AllInOneLauncher.Logic.LauncherGameSelectionManager;

namespace AllInOneLauncher.Logic
{
    internal static class BFMERegistryManager
    {
        internal static bool IsBFMEInstalled(AvailableBFMEGames availableBFMEGames) => GetBFMEInstallPath(availableBFMEGames) != "";
        internal static string GetBFMELanguage(AvailableBFMEGames availableBFMEGames) => GetBFMERegistryKeyValue(availableBFMEGames, "Language");
        internal static string GetBFMEInstallPath(AvailableBFMEGames availableBFMEGames) => GetBFMERegistryKeyValue(availableBFMEGames, "Install Dir");
        internal static string GetBFMEDataPath(AvailableBFMEGames availableBFMEGames) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetBFMERegistryKeyValue(availableBFMEGames, "UserDataLeafName", true));
        internal static string GetBFMEExecutableName(AvailableBFMEGames availableBFMEGames) => GetFileName(availableBFMEGames);
        internal static string GetBFMESerialKey(AvailableBFMEGames availableBFMEGames) => GetBFMERegistryKeyValue(availableBFMEGames, "", true, @"\ergc");

        internal static void SetBFMELanguage(AvailableBFMEGames availableBFMEGames, string language) => SetBFMERegistryKeyValue(availableBFMEGames, "Language", language);
        internal static void SetBFMEInstallPath(AvailableBFMEGames availableBFMEGames, string path) => SetBFMERegistryKeyValue(availableBFMEGames, "Install Dir", path);
        internal static void CreateBFMEInstallRegistry(AvailableBFMEGames availableBFMEGames, string installPath, string language)
        {
            if (availableBFMEGames == AvailableBFMEGames.BFME1)
                CreateBFME1InstallRegistry(installPath, language);
            else if (availableBFMEGames == AvailableBFMEGames.BFME2)
                CreateBFME2InstallRegistry(installPath, language);
            else if (availableBFMEGames == AvailableBFMEGames.ROTWK)
                CreateROTWKInstallRegistry(installPath, language);

            EnsureBFMEAppRegistry(availableBFMEGames);
        }
        internal static void EnsureBFMEAppRegistry(AvailableBFMEGames availableBFMEGames)
        {
            try
            {
                if (availableBFMEGames == AvailableBFMEGames.BFME1)
                    EnsureBFME1AppRegistry();
                else if (availableBFMEGames == AvailableBFMEGames.BFME2)
                    EnsureBFME2AppRegistry();
                else if (availableBFMEGames == AvailableBFMEGames.ROTWK)
                    EnsureROTWKAppRegistry();
            }
            catch
            {

            }
        }

        private static void CreateBFME1InstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\EA GAMES\The Battle for Middle-earth", false);
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

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA Games\The Battle for Middle-earth", false);
            using RegistryKey? keyGameAlt = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA Games\The Battle for Middle-earth", true);
            keyGameAlt?.SetValue("InstallPath", installPath);
            keyGameAlt?.SetValue("Language", language);
            keyGameAlt?.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UserDataLeafName", "My Battle for Middle-earth Files");
            keyGameAlt?.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files"));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files", "Options.ini"), BFMESettingsManager.DefaultOptions);
        }

        private static void EnsureBFME1AppRegistry()
        {
            string installPath = GetBFMERegistryKeyValue(0, "Install Dir");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, "lotrbfme.exe"));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\EA GAMES\The Battle for Middle-earth");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files"));
        }

        private static void CreateBFME2InstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Battle for Middle-earth II", false);
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

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II", false);
            using RegistryKey? keyGameAlt = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II", true);
            keyGameAlt?.SetValue("InstallPath", installPath);
            keyGameAlt?.SetValue("Language", language);
            keyGameAlt?.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UserDataLeafName", "My Battle for Middle-earth II Files");
            keyGameAlt?.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files"));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files", "Options.ini"), BFMESettingsManager.DefaultOptions);
        }

        private static void EnsureBFME2AppRegistry()
        {
            string installPath = GetBFMERegistryKeyValue(AvailableBFMEGames.BFME2, "Install Dir");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, "lotrbfme2.exe"));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Battle for Middle-earth II");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth II Files"));
        }

        private static void CreateROTWKInstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", false);
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

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", false);
            using RegistryKey? keyGameAlt = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", true);
            keyGameAlt?.SetValue("InstallPath", installPath);
            keyGameAlt?.SetValue("Language", language);
            keyGameAlt?.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGameAlt?.SetValue("UserDataLeafName", "My Rise of the Witch-king Files");
            keyGameAlt?.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files"));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files", "Options.ini"), BFMESettingsManager.DefaultOptions);
        }

        private static void EnsureROTWKAppRegistry()
        {
            string installPath = GetBFMERegistryKeyValue(AvailableBFMEGames.ROTWK, "Install Dir");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2ep1.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2ep1.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, "lotrbfme2ep1.exe"));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Rise of the Witch-king Files"));
        }

        private static string GetBFMERegistryKeyValue(AvailableBFMEGames availableBFMEGames, string key, bool useAltKey = false, string keySuffix = "")
        {
            string gameRegistry = "";
            if (availableBFMEGames == AvailableBFMEGames.BFME1)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\EA Games\The Battle for Middle-earth" : @"EA Games\The Battle for Middle-earth")}{keySuffix}";
            else if (availableBFMEGames == AvailableBFMEGames.BFME2)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Battle for Middle-earth II" : @"Electronic Arts\The Battle for Middle-earth II")}{keySuffix}";
            else if (availableBFMEGames == AvailableBFMEGames.ROTWK)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king" : @"Electronic Arts\The Lord of the Rings, The Rise of the Witch-king")}{keySuffix}";

            using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(gameRegistry, false);
            string result = registryKey?.GetValue(key) as string ?? "";
            return result;
        }

        private static void SetBFMERegistryKeyValue(AvailableBFMEGames availableBFMEGames, string key, string value, bool useAltKey = false, string keySuffix = "")
        {
            string gameRegistry = "";
            if (availableBFMEGames == AvailableBFMEGames.BFME1)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\EA Games\The Battle for Middle-earth" : @"EA Games\The Battle for Middle-earth")}{keySuffix}";
            else if (availableBFMEGames == AvailableBFMEGames.BFME2)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Battle for Middle-earth II" : @"Electronic Arts\The Battle for Middle-earth II")}{keySuffix}";
            else if (availableBFMEGames == AvailableBFMEGames.ROTWK)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king" : @"Electronic Arts\The Lord of the Rings, The Rise of the Witch-king")}{keySuffix}";

            using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(gameRegistry, true);
            registryKey?.SetValue(key, value);
        }

        private static string RandomString(int length) => new(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }
}
