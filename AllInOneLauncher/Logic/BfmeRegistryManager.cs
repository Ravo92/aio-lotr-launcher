using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using AllInOneLauncher.Data;

namespace AllInOneLauncher.Logic
{
    internal static class BfmeRegistryManager
    {
        internal static bool IsBfmeInstalled(BfmeGame game) => GetBfmeInstallPath(game) != "";
        internal static string GetBfmeLanguage(BfmeGame game) => GetBfmeRegistryKeyValue(game, "Language");
        internal static string GetBfmeInstallPath(BfmeGame game) => GetBfmeRegistryKeyValue(game, "Install Dir");
        internal static string GetBfmeDataPath(BfmeGame game) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetBfmeRegistryKeyValue(game, "UserDataLeafName", true));
        internal static string GetBfmeSerialKey(BfmeGame game) => GetBfmeRegistryKeyValue(game, "", true, @"\ergc");

        internal static void SetBfmeLanguage(BfmeGame game, string language) => SetBfmeRegistryKeyValue(game, "Language", language);
        internal static void SetBfmeInstallPath(BfmeGame game, string path) => SetBfmeRegistryKeyValue(game, "Install Dir", path);
        internal static void CreateBfmeInstallRegistry(BfmeGame game, string installPath, string language)
        {
            if (game == BfmeGame.BFME1)
                CreateBfme1InstallRegistry(installPath, language);
            else if (game == BfmeGame.BFME2)
                CreateBfme2InstallRegistry(installPath, language);
            else if (game == BfmeGame.ROTWK)
                CreateRotwkInstallRegistry(installPath, language);

            EnsureBfmeAppRegistry(game);
        }
        internal static void EnsureBfmeAppRegistry(BfmeGame game)
        {
            try
            {
                if (game == BfmeGame.BFME1)
                    EnsureBfme1AppRegistry();
                else if (game == BfmeGame.BFME2)
                    EnsureBfme2AppRegistry();
                else if (game == BfmeGame.ROTWK)
                    EnsureRotwkAppRegistry();
            }
            catch
            {

            }
        }

        private static void CreateBfme1InstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            // Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\EA GAMES\The Battle for Middle-earth", false);
            // using RegistryKey? keyGameMain = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\EA GAMES\The Battle for Middle-earth", true);
            // keyGameMain?.SetValue("CacheSize", "3351006208");
            // keyGameMain?.SetValue("DisplayName", "The Battle for Middle-earth");
            // keyGameMain?.SetValue("Install Dir", installPath);
            // keyGameMain?.SetValue("Language", language);
            // keyGameMain?.SetValue("Locale", "en");
            // keyGameMain?.SetValue("Product GUID", "{3F290582-3F4E-4B96-009C-E0BABAA40C42}");
            // keyGameMain?.SetValue("Region", "EUROPE");
            // keyGameMain?.SetValue("Registration", @"SOFTWARE\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc");
            // keyGameMain?.SetValue("Suppression Exe", "rtsi.exe");
            // keyGameMain?.SetValue("SwapSize", "0");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA Games\The Battle for Middle-earth", false);
            using RegistryKey? keyGame = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA Games\The Battle for Middle-earth", true);
            keyGame?.SetValue("InstallPath", installPath);
            keyGame?.SetValue("Language", language);
            keyGame?.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGame?.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGame?.SetValue("UserDataLeafName", Constants.C_APPDATAFOLDER_BFME1_NAME_EN);
            keyGame?.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME1_NAME_EN)))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME1_NAME_EN));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME1_NAME_EN, Constants.C_OPTIONSINI_FILENAME), BfmeSettingsManager.DefaultOptions);
        }

        private static void EnsureBfme1AppRegistry()
        {
            string installPath = GetBfmeRegistryKeyValue(0, "Install Dir");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, Constants.C_BFME1_EXECUTABLE));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\EA GAMES\The Battle for Middle-earth");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME1_NAME_EN)))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME1_NAME_EN));
        }

        private static void CreateBfme2InstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            // Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Battle for Middle-earth II", false);
            // using RegistryKey? keyGameMain = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Battle for Middle-earth II", true);
            // keyGameMain?.SetValue("CacheSize", "3351006208");
            // keyGameMain?.SetValue("DisplayName", "The Battle for Middle-earth II");
            // keyGameMain?.SetValue("Install Dir", installPath);
            // keyGameMain?.SetValue("Language", language);
            // keyGameMain?.SetValue("Locale", "en");
            // keyGameMain?.SetValue("Product GUID", "{2A9F95AB-65A3-432c-8631-B8BC5BF7477A}");
            // keyGameMain?.SetValue("Region", "EUROPE");
            // keyGameMain?.SetValue("Registration", @"SOFTWARE\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc");
            // keyGameMain?.SetValue("Suppression Exe", "rtsi.exe");
            // keyGameMain?.SetValue("SwapSize", "0");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II", false);
            using RegistryKey? keyGame = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II", true);
            keyGame.SetValue("InstallPath", installPath);
            keyGame.SetValue("Language", language);
            keyGame.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGame.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGame.SetValue("UserDataLeafName", Constants.C_APPDATAFOLDER_BFME2_NAME_EN);
            keyGame.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME2_NAME_EN)))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME2_NAME_EN));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME2_NAME_EN, Constants.C_OPTIONSINI_FILENAME), BfmeSettingsManager.DefaultOptions);
        }

        private static void EnsureBfme2AppRegistry()
        {
            string installPath = GetBfmeRegistryKeyValue(BfmeGame.BFME2, "Install Dir");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, Constants.C_BFME2_EXECUTABLE));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Battle for Middle-earth II");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME2_NAME_EN)))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_BFME2_NAME_EN));
        }

        private static void CreateRotwkInstallRegistry(string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
                installPath += Path.DirectorySeparatorChar;

            // Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", false);
            // using RegistryKey? keyGameMain = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", true);
            // keyGameMain?.SetValue("CacheSize", "3351006208");
            // keyGameMain?.SetValue("DisplayName", "The Rise of the Witch-king");
            // keyGameMain?.SetValue("Install Dir", installPath);
            // keyGameMain?.SetValue("Language", language);
            // keyGameMain?.SetValue("Locale", "en");
            // keyGameMain?.SetValue("Product GUID", "{B931FB80-537A-4600-00AD-AC5DEDB6C25B}");
            // keyGameMain?.SetValue("Region", "EUROPE");
            // keyGameMain?.SetValue("Registration", @"SOFTWARE\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc");
            // keyGameMain?.SetValue("Suppression Exe", "rtsi.exe");
            // keyGameMain?.SetValue("SwapSize", "0");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", false);
            using RegistryKey? keyGame = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king", true);
            keyGame?.SetValue("InstallPath", installPath);
            keyGame?.SetValue("Language", language);
            keyGame?.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyGame?.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyGame?.SetValue("UserDataLeafName", Constants.C_APPDATAFOLDER_Rotwk_NAME_EN);
            keyGame?.SetValue("Version", "65539", RegistryValueKind.DWord);

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_Rotwk_NAME_EN)))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_Rotwk_NAME_EN));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_Rotwk_NAME_EN, Constants.C_OPTIONSINI_FILENAME), BfmeSettingsManager.DefaultOptions);
        }

        private static void EnsureRotwkAppRegistry()
        {
            string installPath = GetBfmeRegistryKeyValue(BfmeGame.ROTWK, "Install Dir");

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2ep1.exe", false);
            using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2ep1.exe", true);
            keyApp?.SetValue("", Path.Combine(installPath, Constants.C_ROTWK_EXECUTABLE));
            keyApp?.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king");
            keyApp?.SetValue("Installed", "1", RegistryValueKind.DWord);
            keyApp?.SetValue("Path", installPath);
            keyApp?.SetValue("Restart", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc", false);
            using RegistryKey? keySerial = Registry.LocalMachine.CreateSubKey(@$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc", true);
            keySerial?.SetValue("", RandomString(20));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_Rotwk_NAME_EN)))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.C_APPDATAFOLDER_Rotwk_NAME_EN));
        }

        private static string GetBfmeRegistryKeyValue(BfmeGame game, string key, bool useAltKey = false, string keySuffix = "")
        {
            string gameRegistry = "";
            if (game == BfmeGame.BFME1)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\EA Games\The Battle for Middle-earth" : @"EA Games\The Battle for Middle-earth")}{keySuffix}";
            else if (game == BfmeGame.BFME2)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Battle for Middle-earth II" : @"Electronic Arts\The Battle for Middle-earth II")}{keySuffix}";
            else if (game == BfmeGame.ROTWK)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king" : @"Electronic Arts\The Lord of the Rings, The Rise of the Witch-king")}{keySuffix}";

            using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(gameRegistry, false);
            string result = registryKey?.GetValue(key) as string ?? "";
            return result;
        }

        private static void SetBfmeRegistryKeyValue(BfmeGame game, string key, string value, bool useAltKey = false, string keySuffix = "")
        {
            string gameRegistry = "";
            if (game == BfmeGame.BFME1)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\EA Games\The Battle for Middle-earth" : @"EA Games\The Battle for Middle-earth")}{keySuffix}";
            else if (game == BfmeGame.BFME2)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Battle for Middle-earth II" : @"Electronic Arts\The Battle for Middle-earth II")}{keySuffix}";
            else if (game == BfmeGame.ROTWK)
                gameRegistry = @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\{(useAltKey ? @"Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king" : @"Electronic Arts\The Lord of the Rings, The Rise of the Witch-king")}{keySuffix}";

            using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(gameRegistry, true);
            registryKey?.SetValue(key, value);
        }

        private static string RandomString(int length) => new(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }
}
