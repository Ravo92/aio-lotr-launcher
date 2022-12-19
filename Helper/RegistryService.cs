using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;

namespace Helper
{
    public class RegistryService
    {
        private const string C_WRONG_PARAMETER = "WrongParameter";
        private const string C_NOT_FOUND = "ValueNotFound";

        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        public static string ReadRegKey(string kindOf)
        {
            if (IsNotNull(kindOf))
            {
                RegistryKey ?localKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\");
                if (IsNotNull(localKey))
                {
                    switch (kindOf)
                    {
                        case "lang":
                            if (IsNotNull(localKey.GetValue("Language")))
                            {
                                string? lang = localKey.GetValue("Language")!.ToString()!;
                                return lang;
                            }
                            else
                            {
                                return C_NOT_FOUND;
                            }

                        case "appData":
                            if (IsNotNull(localKey.GetValue("UserDataLeafName")))
                            {
                                string? appData = localKey.GetValue("UserDataLeafName")!.ToString()!;
                                return appData;
                            }
                            else
                            {
                                return C_NOT_FOUND;
                            }

                        case "path":
                            if (IsNotNull(localKey.GetValue("InstallPath")))
                            {
                                string? path = localKey.GetValue("InstallPath")!.ToString()!;
                                return path;
                            }
                            else
                            {
                                return C_NOT_FOUND;
                            }

                        default:
                            return C_WRONG_PARAMETER;
                    }
                }
                else
                {
                    return C_NOT_FOUND;
                }
            }
            else
            {
                return C_WRONG_PARAMETER;
            }
        }

        public static string RandomCDKeyGenerator(int length)
        {
            Random _random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static void WriteRegKeysInstallation(string installpath)
        {
            RegistryKey keyFolder1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games");

            RegistryKey keyFolder2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth");
            keyFolder2.SetValue("CacheSize", "3351006208");
            keyFolder2.SetValue("CD Drive", @"I:\");
            keyFolder2.SetValue("DisplayName", "The Battle for Middle-earth (tm)");
            keyFolder2.SetValue("Folder", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\EA GAMES\The Battle for Middle-earth (tm)\");
            keyFolder2.SetValue("Install Dir", installpath);
            keyFolder2.SetValue("Installed From", @"I:\");
            keyFolder2.SetValue("Language", "English US");
            keyFolder2.SetValue("Locale", "en_us");
            keyFolder2.SetValue("Patch URL", @"http://transtest.ea.com/Electronic Arts/The Battle for Middle-earth/Europe");
            keyFolder2.SetValue("Product GUID", "{3F290582-3F4E-4B96-009C-E0BABAA40C42}");
            keyFolder2.SetValue("Region", "EUROPE");
            keyFolder2.SetValue("Registration", @"SOFTWARE\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc");
            keyFolder2.SetValue("Suppression Exe", "rtsi.exe");
            keyFolder2.SetValue("SwapSize", "0");

            RegistryKey keyFolder3 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth\1.0");
            keyFolder3.SetValue("DisplayName", "The Battle for Middle-earth (tm)");
            keyFolder3.SetValue("Language", "1", RegistryValueKind.DWord);
            keyFolder3.SetValue("LanguageName", "English US");

            RegistryKey keyFolder4 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");
            RegistryKey keyFolder5 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games");

            RegistryKey keyFolder6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth");
            keyFolder6.SetValue("InstallPath", installpath);
            keyFolder6.SetValue("Language", "english");
            keyFolder6.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyFolder6.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyFolder6.SetValue("UserDataLeafName", "My Battle for Middle-earth Files");
            keyFolder6.SetValue("Version", "65539", RegistryValueKind.DWord);

            RegistryKey keyFolder7 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\ergc");
            keyFolder7.SetValue("", RandomCDKeyGenerator(20));

            keyFolder1.Close();
            keyFolder2.Close();
            keyFolder3.Close();

            keyFolder4.Close();
            keyFolder5.Close();
            keyFolder6.Close();
            keyFolder7.Close();
        }
    }
}
