using Microsoft.Win32;

namespace Helper
{
    public class RegistryService
    {
        private static readonly string AppDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string ReadRegKey(string kindOf)
        {
            using RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth\");
            using RegistryKey? secondPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\");

            return kindOf switch
            {
                "locale" => mainPath?.GetValue("Locale")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "appData" => secondPath?.GetValue("UserDataLeafName")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                "path" => secondPath?.GetValue("InstallPath")?.ToString() ?? ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND,
                _ => ConstStrings.C_REGISTRY_SERVICE_WRONG_PARAMETER,
            };
        }

        public static string RandomCDKeyGenerator(int length)
        {
            Random _random = new(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static void WriteRegKeysInstallation(string installpath, string locale, string strLanguageName, string strLanguage)
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
            keyFolder5.SetValue("UserDataLeafName", "My Battle for Middle-earth Files");
            keyFolder5.SetValue("Version", "65539", RegistryValueKind.DWord);

            using RegistryKey keyFolder6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\ergc");
            keyFolder6.SetValue("", RandomCDKeyGenerator(20));
        }

        public static string GameLanguage()
        {
            return ReadRegKey("locale");
        }

        public static string GameInstallPath()
        {
            return ReadRegKey("path");
        }

        public static string GameAppdataFolderPath()
        {
            if (ReadRegKey("appData") != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                return Path.Combine(AppDataFolderPath, ReadRegKey("appData"));
            else
                return Path.Combine(AppDataFolderPath, ConstStrings.C_APPDATAFOLDER_NAME_EN);
        }
    }
}