using Microsoft.Win32;

namespace Helper
{
    public class RegistryService
    {
        private static readonly string AppDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string ReadRegKeyBFME1(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth\");
            using RegistryKey? secondPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\");

            return kindOf switch
            {
                "locale" => mainPath?.GetValue("Locale")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "displayName" => mainPath?.GetValue("DisplayName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "path" => mainPath?.GetValue("Install Dir")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "appData" => secondPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                _ => ConstStrings.C_REGISTRY_SERVICE_WRONG_PARAMETER,
            };
        }

        public static string ReadRegKeyBFME2(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\The Battle for Middle-earth II\");
            using RegistryKey? secondPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\");

            return kindOf switch
            {
                "locale" => mainPath?.GetValue("Locale")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "displayName" => mainPath?.GetValue("DisplayName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "path" => mainPath?.GetValue("Install Dir")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "appData" => secondPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                _ => ConstStrings.C_REGISTRY_SERVICE_WRONG_PARAMETER,
            };
        }

        public static string ReadRegKeyBFME25(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\");
            using RegistryKey? secondPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\");

            return kindOf switch
            {
                "locale" => mainPath?.GetValue("Locale")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "displayName" => mainPath?.GetValue("DisplayName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "path" => mainPath?.GetValue("Install Dir")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "appData" => secondPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                _ => ConstStrings.C_REGISTRY_SERVICE_WRONG_PARAMETER,
            };
        }

        public static string ReadRegKey(string key, string value)
        {
            try
            {
                using RegistryKey? registryPath = Registry.LocalMachine.OpenSubKey(key);
                return registryPath?.GetValue(value)?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND;
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRegistryTools.Error(ex, "");
                return ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND;
            }
        }

        public static string GameLanguage(string BFMEGameVersion)
        {
            return BFMEGameVersion switch
            {
                "BFME1" => ReadRegKeyBFME1("locale"),
                "BFME2" => ReadRegKeyBFME2("locale"),
                "BFME25" => ReadRegKeyBFME25("locale"),
                _ => ReadRegKeyBFME1("locale"),
            };
        }

        public static string GameInstallPath(string BFMEGameVersion)
        {
            return BFMEGameVersion switch
            {
                "BFME1" => ReadRegKeyBFME1("path"),
                "BFME2" => ReadRegKeyBFME2("path"),
                "BFME25" => ReadRegKeyBFME25("path"),
                _ => ReadRegKeyBFME1("path"),
            };
        }

        public static string GameAppdataFolderPath(string BFMEGameVersion)
        {
            if (BFMEGameVersion == "BFME1")
            {
                if (ReadRegKeyBFME1("appData") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                    return Path.Combine(AppDataFolderPath, ReadRegKeyBFME1("appData"));
                else
                    return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_BFME1_NAME_EN);
            }

            else if (BFMEGameVersion == "BFME2")
            {
                if (ReadRegKeyBFME2("appData") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                    return Path.Combine(AppDataFolderPath, ReadRegKeyBFME2("appData"));
                else
                    return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_BFME2_NAME_EN);
            }

            else if (BFMEGameVersion == "BFME25")
            {
                if (ReadRegKeyBFME25("appData") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                    return Path.Combine(AppDataFolderPath, ReadRegKeyBFME25("appData"));
                else
                    return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_BFME25_NAME_EN);
            }
            else
            {
                if (ReadRegKeyBFME1("appData") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                    return Path.Combine(AppDataFolderPath, ReadRegKeyBFME1("appData"));
                else
                    return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_BFME1_NAME_EN);
            }
        }

        public static void WriteRegKeysInstallationBFME1(string installpath, string locale, string strLanguageName, string strLanguage)
        {
            using RegistryKey keyFolder1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games");

            using RegistryKey keyFolder2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth");
            keyFolder2.SetValue("CacheSize", "3351006208");
            keyFolder2.SetValue("CD Drive", @"I:\");
            keyFolder2.SetValue("DisplayName", "The Battle for Middle-earth");
            keyFolder2.SetValue("Folder", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\EA GAMES\The Battle for Middle-earth\");
            keyFolder2.SetValue("Install Dir", installpath);
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
            keyFolder5.SetValue("InstallPath", installpath);
            keyFolder5.SetValue("Language", strLanguage);
            keyFolder5.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyFolder5.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyFolder5.SetValue("UserDataLeafName", ConstStrings.C_APPDATAFOLDER_BFME1_NAME_EN);
            keyFolder5.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolder6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\ergc");
            keyFolder6.SetValue("", RandomCDKeyGenerator(20));
        }

        public static void WriteRegKeysInstallationBFME2(string installpath, string locale, string strLanguageName, string strLanguage)
        {
            using RegistryKey keyFolder1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");

            using RegistryKey keyFolder2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\The Battle for Middle-earth II");
            keyFolder2.SetValue("CacheSize", "3351006208");
            keyFolder2.SetValue("CD Drive", @"I:\");
            keyFolder2.SetValue("DisplayName", "The Battle for Middle-earth II");
            keyFolder2.SetValue("Folder", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Electronic Arts\The Battle for Middle-earth II\");
            keyFolder2.SetValue("Install Dir", installpath);
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
            keyFolder5.SetValue("InstallPath", installpath);
            keyFolder5.SetValue("Language", strLanguage);
            keyFolder5.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyFolder5.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyFolder5.SetValue("UserDataLeafName", ConstStrings.C_APPDATAFOLDER_BFME2_NAME_EN);
            keyFolder5.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolder6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc");
            keyFolder6.SetValue("", RandomCDKeyGenerator(20));

            using RegistryKey keyFolder7 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe");
            keyFolder7.SetValue("", Path.Combine(installpath, ConstStrings.C_BFME2_MAIN_GAME_FILE));
            keyFolder7.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Battle for Middle-earth II");
            keyFolder7.SetValue("Restart", "00000000", RegistryValueKind.DWord);
            keyFolder7.SetValue("DirectX Installed", "00000000", RegistryValueKind.DWord);
            keyFolder7.SetValue("Installed", "00000001", RegistryValueKind.DWord);
            keyFolder7.SetValue("Path", installpath);
        }

        public static void WriteRegKeysInstallationBFME25(string installpath, string locale, string strLanguageName, string strLanguage)
        {
            using RegistryKey keyFolder1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");

            using RegistryKey keyFolder2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\");
            keyFolder2.SetValue("CacheSize", "3351006208");
            keyFolder2.SetValue("CD Drive", @"I:\");
            keyFolder2.SetValue("DisplayName", "The Rise of the Witch-king");
            keyFolder2.SetValue("Folder", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Electronic Arts\The Rise of the Witch-king\");
            keyFolder2.SetValue("Install Dir", installpath);
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
            keyFolder5.SetValue("InstallPath", installpath);
            keyFolder5.SetValue("Language", strLanguage);
            keyFolder5.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyFolder5.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyFolder5.SetValue("UserDataLeafName", ConstStrings.C_APPDATAFOLDER_BFME25_NAME_EN);
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