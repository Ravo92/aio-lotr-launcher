using Microsoft.Win32;
using System.Linq;
using System;
using System.IO;
using static LauncherGUI.Helpers.GameSelectorHelper;

namespace LauncherGUI.Helpers
{
    internal class BFMERegistryHelper
    {
        private enum RegServiceStates
        {
            ReturnedValue = 0,
            NotFound = 1,
            WrongParameter = 2
        }

        public static string ReadRegKeyBFME1(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth\");
            using RegistryKey? secondPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\");
            using RegistryKey? ergcPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\ergc");

            if (mainPath is not null && secondPath is not null)
            {
                return kindOf switch
                {
                    "locale" => mainPath?.GetValue("Locale")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                    "displayName" => mainPath?.GetValue("DisplayName")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                    "path" => mainPath?.GetValue("Install Dir")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                    "appData" => secondPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                    "cdKey" => ergcPath?.GetValue("")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                    _ => ConstStringsHelper.C_REGISTRY_SERVICE_WRONG_PARAMETER,
                };
            }
            else
            {
                LogHelper.LoggerRegistryTools.Error(string.Concat("Error Reading Registry: ", kindOf, " was not found in Registry!"));
                return ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND;
            }
        }

        public static string ReadRegKeyBFME2(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\The Battle for Middle-earth II\");
            using RegistryKey? secondPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\");
            using RegistryKey? ergcPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc");

            return kindOf switch
            {
                "locale" => mainPath?.GetValue("Locale")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                "displayName" => mainPath?.GetValue("DisplayName")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                "path" => mainPath?.GetValue("Install Dir")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                "appData" => secondPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                "cdKey" => ergcPath?.GetValue("")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                _ => ConstStringsHelper.C_REGISTRY_SERVICE_WRONG_PARAMETER,
            };
        }

        public static string ReadRegKeyROTWK(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\");
            using RegistryKey? secondPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\");
            using RegistryKey? ergcPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc");

            return kindOf switch
            {
                "locale" => mainPath?.GetValue("Locale")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                "displayName" => mainPath?.GetValue("DisplayName")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                "path" => mainPath?.GetValue("Install Dir")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                "appData" => secondPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                "cdKey" => ergcPath?.GetValue("")?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND,
                _ => ConstStringsHelper.C_REGISTRY_SERVICE_WRONG_PARAMETER,
            };
        }

        public static string ReadRegKey(string key, string value)
        {
            try
            {
                using RegistryKey? registryPath = Registry.LocalMachine.OpenSubKey(key);
                return registryPath?.GetValue(value)?.ToString() ?? ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND;
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRegistryTools.Error(ex, "");
                return ConstStringsHelper.C_REGISTRY_SERVICE_NOT_FOUND;
            }
        }

        public static void SetRegKey(string path, string key, string value)
        {
            RegistryKey mainPath;
            mainPath = Registry.LocalMachine.OpenSubKey(path, true)!;
            mainPath.SetValue(key, value);
        }

        public static string GameLanguage(AvailableBFMEGames BFMEGameVersion)
        {
            return BFMEGameVersion switch
            {
                AvailableBFMEGames.BFME1 => ReadRegKeyBFME1("locale"),
                AvailableBFMEGames.BFME2 => ReadRegKeyBFME2("locale"),
                AvailableBFMEGames.ROTWK => ReadRegKeyROTWK("locale"),
                _ => ReadRegKeyBFME1("locale"),
            };
        }

        public static string GameInstallPath(AvailableBFMEGames BFMEGameVersion)
        {
            return BFMEGameVersion switch
            {
                AvailableBFMEGames.BFME1 => ReadRegKeyBFME1("path"),
                AvailableBFMEGames.BFME2 => ReadRegKeyBFME2("path"),
                AvailableBFMEGames.ROTWK => ReadRegKeyROTWK("path"),
                _ => ReadRegKeyBFME1("path"),
            };
        }

        public static string GameAppDataFolderPath(AvailableBFMEGames BFMEGameVersion)
        {
            return BFMEGameVersion switch
            {
                AvailableBFMEGames.BFME1 => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ReadRegKeyBFME1("appData")),
                AvailableBFMEGames.BFME2 => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ReadRegKeyBFME2("appData")),
                AvailableBFMEGames.ROTWK => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ReadRegKeyROTWK("appData")),
                _ => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ReadRegKeyBFME1("appData")),
            };
        }

        public static void WriteRegKeysInstallationBFME1(string installPath, string locale, string strLanguageName, string strLanguage)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
            {
                installPath += Path.DirectorySeparatorChar;
            }

            using RegistryKey keyFolder1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games");

            using RegistryKey keyFolder2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth");
            keyFolder2.SetValue("CacheSize", "3351006208");
            keyFolder2.SetValue("CD Drive", @"I:\");
            keyFolder2.SetValue("DisplayName", "The Battle for Middle-earth");
            keyFolder2.SetValue("Folder", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\EA GAMES\The Battle for Middle-earth\");
            keyFolder2.SetValue("Install Dir", installPath);
            keyFolder2.SetValue("Installed From", @"I:\");
            keyFolder2.SetValue("Language", strLanguageName);
            keyFolder2.SetValue("Locale", locale);
            keyFolder2.SetValue("Patch URL", @"http://transtest.ea.com/Electronic Arts/The Battle for Middle-earth/Europe");
            keyFolder2.SetValue("Product GUID", "{3F290582-3F4E-4B96-009C-E0BABAA40C42}");
            keyFolder2.SetValue("Region", "EUROPE");
            keyFolder2.SetValue("Registration", @"SOFTWARE\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc");
            keyFolder2.SetValue("Suppression Exe", "rtsi.exe");
            keyFolder2.SetValue("SwapSize", "0");

            using RegistryKey keyFolder3 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");
            using RegistryKey keyFolder4 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games");

            using RegistryKey keyFolder5 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth");
            keyFolder5.SetValue("InstallPath", installPath);
            keyFolder5.SetValue("Language", strLanguage);
            keyFolder5.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyFolder5.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyFolder5.SetValue("UserDataLeafName", ConstStringsHelper.C_APPDATAFOLDER_BFME1_NAME_EN);
            keyFolder5.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolder6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\ergc");
            keyFolder6.SetValue("", RandomCDKeyGenerator(20));
        }

        public static void WriteRegKeysInstallationBFME2(string installPath, string locale, string strLanguageName, string strLanguage)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
            {
                installPath += Path.DirectorySeparatorChar;
            }

            using RegistryKey keyFolder1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");

            using RegistryKey keyFolder2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\The Battle for Middle-earth II");
            keyFolder2.SetValue("CacheSize", "3351006208");
            keyFolder2.SetValue("CD Drive", @"I:\");
            keyFolder2.SetValue("DisplayName", "The Battle for Middle-earth II");
            keyFolder2.SetValue("Folder", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Electronic Arts\The Battle for Middle-earth II\");
            keyFolder2.SetValue("Install Dir", installPath);
            keyFolder2.SetValue("Installed From", @"I:\");
            keyFolder2.SetValue("Language", strLanguageName);
            keyFolder2.SetValue("Locale", locale);
            keyFolder2.SetValue("Patch URL", @"http://transtest.ea.com/Electronic Arts/The Battle for Middle-earth II/Europe");
            keyFolder2.SetValue("Product GUID", "{2A9F95AB-65A3-432c-8631-B8BC5BF7477A}");
            keyFolder2.SetValue("Region", "EUROPE");
            keyFolder2.SetValue("Registration", @"SOFTWARE\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc");
            keyFolder2.SetValue("Suppression Exe", "rtsi.exe");
            keyFolder2.SetValue("SwapSize", "0");

            using RegistryKey keyFolder3 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");
            using RegistryKey keyFolder4 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts");

            using RegistryKey keyFolder5 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II");
            keyFolder5.SetValue("InstallPath", installPath);
            keyFolder5.SetValue("Language", strLanguage);
            keyFolder5.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyFolder5.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyFolder5.SetValue("UserDataLeafName", ConstStringsHelper.C_APPDATAFOLDER_BFME2_NAME_EN);
            keyFolder5.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolder6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc");
            keyFolder6.SetValue("", RandomCDKeyGenerator(20));

            using RegistryKey keyFolder7 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe");
            keyFolder7.SetValue("", Path.Combine(installPath, ConstStringsHelper.C_BFME2_MAIN_GAME_FILE));
            keyFolder7.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Battle for Middle-earth II");
            keyFolder7.SetValue("Restart", "00000000", RegistryValueKind.DWord);
            keyFolder7.SetValue("DirectX Installed", "00000000", RegistryValueKind.DWord);
            keyFolder7.SetValue("Installed", "00000001", RegistryValueKind.DWord);
            keyFolder7.SetValue("Path", installPath);
        }

        public static void WriteRegKeysInstallationROTWK(string installPath, string locale, string strLanguageName, string strLanguage)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
            {
                installPath += Path.DirectorySeparatorChar;
            }

            using RegistryKey keyFolder1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");

            using RegistryKey keyFolder2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\");
            keyFolder2.SetValue("CacheSize", "3351006208");
            keyFolder2.SetValue("CD Drive", @"I:\");
            keyFolder2.SetValue("DisplayName", "The Rise of the Witch-king");
            keyFolder2.SetValue("Folder", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Electronic Arts\The Rise of the Witch-king\");
            keyFolder2.SetValue("Install Dir", installPath);
            keyFolder2.SetValue("Installed From", @"I:\");
            keyFolder2.SetValue("Language", strLanguageName);
            keyFolder2.SetValue("Locale", locale);
            keyFolder2.SetValue("Patch URL", @"http://transtest.ea.com/Electronic Arts/The Battle for Middle-earth II/Europe");
            keyFolder2.SetValue("Product GUID", "{B931FB80-537A-4600-00AD-AC5DEDB6C25B}");
            keyFolder2.SetValue("Region", "EUROPE");
            keyFolder2.SetValue("Registration", @"SOFTWARE\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc");
            keyFolder2.SetValue("Suppression Exe", "rtsi.exe");
            keyFolder2.SetValue("SwapSize", "0");

            using RegistryKey keyFolder3 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");
            using RegistryKey keyFolder4 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts");

            using RegistryKey keyFolder5 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king");
            keyFolder5.SetValue("InstallPath", installPath);
            keyFolder5.SetValue("Language", strLanguage);
            keyFolder5.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyFolder5.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyFolder5.SetValue("UserDataLeafName", ConstStringsHelper.C_APPDATAFOLDER_ROTWK_NAME_EN);
            keyFolder5.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolder6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc");
            keyFolder6.SetValue("", RandomCDKeyGenerator(20));
        }

        public static string RandomCDKeyGenerator(int length)
        {
            Random _random = new(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
