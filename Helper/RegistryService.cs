using Microsoft.Win32;

namespace Helper
{
    public class RegistryService
    {
        private static readonly string AppDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string ReadRegKeyBFME1(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\");

            return kindOf switch
            {
                "Language" => mainPath?.GetValue("Language")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "InstallPath" => mainPath?.GetValue("InstallPath")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "UserDataLeafName" => mainPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                _ => ConstStrings.C_REGISTRY_SERVICE_WRONG_PARAMETER,
            };
        }

        public static string ReadRegKeyBFME2(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\");

            return kindOf switch
            {
                "Language" => mainPath?.GetValue("Language")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "InstallPath" => mainPath?.GetValue("InstallPath")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "UserDataLeafName" => mainPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                _ => ConstStrings.C_REGISTRY_SERVICE_WRONG_PARAMETER,
            };
        }

        public static string ReadRegKeyBFME25(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\");

            return kindOf switch
            {
                "Language" => mainPath?.GetValue("Language")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "InstallPath" => mainPath?.GetValue("InstallPath")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "UserDataLeafName" => mainPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
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

        public static void WriteRegKeyForBFMEGames(string key, string value, string gameName)
        {
            RegistryKey mainPath;

            switch (gameName)
            {
                case "BFME1":
                    mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\", true)!;
                    mainPath.SetValue(key, value);
                    break;
                case "BFME2":
                    mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\", true)!;
                    mainPath.SetValue(key, value);
                    break;
                case "BFME25":
                    mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\", true)!;
                    mainPath.SetValue(key, value);
                    break;
            }
        }

        public static string GameLanguage(string BFMEGameVersion)
        {
            return BFMEGameVersion switch
            {
                "BFME1" => ReadRegKeyBFME1("Language"),
                "BFME2" => ReadRegKeyBFME2("Language"),
                "BFME25" => ReadRegKeyBFME25("Language"),
                _ => ReadRegKeyBFME1("Language"),
            };
        }

        public static string GameInstallPath(string BFMEGameVersion)
        {
            return BFMEGameVersion switch
            {
                "BFME1" => ReadRegKeyBFME1("InstallPath"),
                "BFME2" => ReadRegKeyBFME2("InstallPath"),
                "BFME25" => ReadRegKeyBFME25("InstallPath"),
                _ => ReadRegKeyBFME1("InstallPath"),
            };
        }

        public static string GameAppDataFolderPath(string BFMEGameVersion)
        {
            if (BFMEGameVersion == "BFME1")
            {
                if (ReadRegKeyBFME1("UserDataLeafName") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                    return Path.Combine(AppDataFolderPath, ReadRegKeyBFME1("UserDataLeafName"));
                else
                    return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_BFME1_NAME_EN);
            }

            else if (BFMEGameVersion == "BFME2")
            {
                if (ReadRegKeyBFME2("UserDataLeafName") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                    return Path.Combine(AppDataFolderPath, ReadRegKeyBFME2("UserDataLeafName"));
                else
                    return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_BFME2_NAME_EN);
            }

            else if (BFMEGameVersion == "BFME25")
            {
                if (ReadRegKeyBFME25("UserDataLeafName") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                    return Path.Combine(AppDataFolderPath, ReadRegKeyBFME25("UserDataLeafName"));
                else
                    return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_BFME25_NAME_EN);
            }
            else
            {
                if (ReadRegKeyBFME1("UserDataLeafName") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                    return Path.Combine(AppDataFolderPath, ReadRegKeyBFME1("UserDataLeafName"));
                else
                    return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_BFME1_NAME_EN);
            }
        }

        public static void WriteRegKeysInstallationBFME1(string installPath, string languageName)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
            {
                installPath += Path.DirectorySeparatorChar;
            }

            using RegistryKey gameRegistryEntries = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth");
            gameRegistryEntries.SetValue("InstallPath", installPath);
            gameRegistryEntries.SetValue("Language", languageName);
            gameRegistryEntries.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            gameRegistryEntries.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            gameRegistryEntries.SetValue("UserDataLeafName", ConstStrings.C_APPDATAFOLDER_BFME1_NAME_EN);
            gameRegistryEntries.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolderERGC = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\ergc");
            keyFolderERGC.SetValue("", RandomCDKeyGenerator(20));
        }

        public static void WriteRegKeysInstallationBFME2(string installPath, string languageName)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
            {
                installPath += Path.DirectorySeparatorChar;
            }

            using RegistryKey gameRegistryEntries = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II");
            gameRegistryEntries.SetValue("InstallPath", installPath);
            gameRegistryEntries.SetValue("Language", languageName);
            gameRegistryEntries.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            gameRegistryEntries.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            gameRegistryEntries.SetValue("UserDataLeafName", ConstStrings.C_APPDATAFOLDER_BFME2_NAME_EN);
            gameRegistryEntries.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolderERGC = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc");
            keyFolderERGC.SetValue("", RandomCDKeyGenerator(20));

            using RegistryKey keyFolderAppPathSettings = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\lotrbfme2.exe");
            keyFolderAppPathSettings.SetValue("", Path.Combine(installPath, ConstStrings.C_BFME2_MAIN_GAME_FILE));
            keyFolderAppPathSettings.SetValue("Game Registry", @"SOFTWARE\Electronic Arts\The Battle for Middle-earth II");
            keyFolderAppPathSettings.SetValue("Restart", "00000000", RegistryValueKind.DWord);
            keyFolderAppPathSettings.SetValue("DirectX Installed", "00000000", RegistryValueKind.DWord);
            keyFolderAppPathSettings.SetValue("Installed", "00000001", RegistryValueKind.DWord);
            keyFolderAppPathSettings.SetValue("Path", installPath);
        }

        public static void WriteRegKeysInstallationBFME25(string installPath, string languageName)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
            {
                installPath += Path.DirectorySeparatorChar;
            }

            using RegistryKey gameRegistryEntries = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king");
            gameRegistryEntries.SetValue("InstallPath", installPath);
            gameRegistryEntries.SetValue("Language", languageName);
            gameRegistryEntries.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            gameRegistryEntries.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            gameRegistryEntries.SetValue("UserDataLeafName", ConstStrings.C_APPDATAFOLDER_BFME25_NAME_EN);
            gameRegistryEntries.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolderERGC = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc");
            keyFolderERGC.SetValue("", RandomCDKeyGenerator(20));
        }

        public static string RandomCDKeyGenerator(int length)
        {
            Random _random = new(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}