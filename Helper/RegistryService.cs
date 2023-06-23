using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;

namespace Helper
{
    public class RegistryService
    {
        public static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        public static string ReadRegKey(string kindOf)
        {
            if (IsNotNull(kindOf))
            {
                RegistryKey? mainPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth\");
                RegistryKey? secondPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\");

                if (IsNotNull(secondPath))
                {
                    switch (kindOf)
                    {
                        case "locale":
                            if (IsNotNull(mainPath?.GetValue("Locale")))
                            {
                                string? lang = mainPath.GetValue("Locale")!.ToString()!;
                                return lang;
                            }
                            else
                            {
                                return ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND;
                            }

                        case "appData":
                            if (IsNotNull(secondPath.GetValue("UserDataLeafName")))
                            {
                                string? appData = secondPath.GetValue("UserDataLeafName")!.ToString()!;
                                return appData;
                            }
                            else
                            {
                                return ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND;
                            }

                        case "path":
                            if (IsNotNull(secondPath.GetValue("InstallPath")))
                            {
                                string? path = secondPath.GetValue("InstallPath")!.ToString()!;
                                return path;
                            }
                            else
                            {
                                return ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND;
                            }

                        default:
                            return ConstStrings.C_REGISTRY_SERVICE_WRONG_PARAMETER;
                    }
                }
                else
                {
                    return ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND;
                }
            }
            else
            {
                return ConstStrings.C_REGISTRY_SERVICE_WRONG_PARAMETER;
            }
        }

        public static string RandomCDKeyGenerator(int length)
        {
            Random _random = new(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static void WriteRegKeysInstallation(string installpath, string locale, string strLanguageName, string strLanguage)
        {
            RegistryKey keyFolder1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games");

            RegistryKey keyFolder2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\EA Games\The Battle for Middle-earth");
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

            RegistryKey keyFolder4 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts");
            RegistryKey keyFolder5 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games");

            RegistryKey keyFolder6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth");
            keyFolder6.SetValue("InstallPath", installpath);
            keyFolder6.SetValue("Language", strLanguage);
            keyFolder6.SetValue("MapPackVersion", "65536", RegistryValueKind.DWord);
            keyFolder6.SetValue("UseLocalUserMaps", "0", RegistryValueKind.DWord);
            keyFolder6.SetValue("UserDataLeafName", "My Battle for Middle-earth Files");
            keyFolder6.SetValue("Version", "65539", RegistryValueKind.DWord);

            RegistryKey keyFolder7 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\ergc");
            keyFolder7.SetValue("", RandomCDKeyGenerator(20));

            keyFolder1.Close();
            keyFolder2.Close();

            keyFolder4.Close();
            keyFolder5.Close();
            keyFolder6.Close();
            keyFolder7.Close();
        }
    }
}
