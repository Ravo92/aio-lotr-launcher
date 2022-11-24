using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;

namespace PatchLauncher.Classes
{
    internal class RegistryFunctions
    {
        private const string notInstalled = "GameNotInstalled";
        private const string wrongParameter = "WrongParameter";
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        public static string ReadRegKey(string kindOf)
        {
            if (kindOf != null)
            {
                RegistryKey? localKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\");
                if (IsNotNull(localKey))
                {
                    switch (kindOf)
                    {
                        case "lang":
                            string? lang = localKey.GetValue("Language")!.ToString();

                            if (IsNotNull(lang))
                            {
                                return lang;
                            }
                            else
                            {
                                return notInstalled;
                            }

                        case "appData":
                            string? appData = localKey.GetValue("UserDataLeafName")!.ToString();

                            if (IsNotNull(appData))
                            {
                                return appData;
                            }
                            else
                            {
                                return notInstalled;
                            }

                        case "path":
                            string? path = localKey.GetValue("InstallPath")!.ToString();

                            if (IsNotNull(path))
                            {
                                return path;
                            }
                            else
                            {
                                return notInstalled;
                            }

                        default:
                            return wrongParameter;
                    }
                }
                else
                {
                    return notInstalled;
                }
            }
            else
            {
                return wrongParameter;
            }
        }
    }
}
