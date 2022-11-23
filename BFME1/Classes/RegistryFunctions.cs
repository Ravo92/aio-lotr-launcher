using Microsoft.Win32;
using System;
using System.Windows;

namespace PatchLauncher.Classes
{
    internal class RegistryFunctions
    {
        public static string ReadRegKey(string kindOf)
        {
            RegistryKey localKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\EA Games\The Battle for Middle-earth\");

            switch (kindOf)
            {
                case "lang":
                    string lang = (string)localKey.GetValue("Language");
                    return lang;

                case "appData":
                    string appData = (string)localKey.GetValue("UserDataLeafName");
                    return appData;

                case "path":
                    string path = (string)localKey.GetValue("InstallPath");
                    return path;

                default:
                    return null;
            }
        }
    }
}
