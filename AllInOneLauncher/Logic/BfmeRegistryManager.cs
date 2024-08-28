using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.Security.Principal;
using System.Runtime.Versioning;
using System.Collections.Generic;
using AllInOneLauncher.Data;

namespace AllInOneLauncher.Logic
{
    public class BfmeRegistryManager
    {
        private static readonly bool is64Bit = nint.Size == 8;

        public enum BfmeRegistryKey
        {
            InstallPath,
            Language,
            MapPackVersion,
            UseLocalUserMaps,
            UserDataLeafName,
            Version,
            SerialKey
        }

        private const string SoftwareRegistryPath = "SOFTWARE\\";
        private const string Wow64Node = "WOW6432Node\\";
        private const string EmptyRegistryKeyOrPath = "";

        internal static readonly Dictionary<BfmeGame, GameInfo> GameInfos = new()
        {
            {
                BfmeGame.BFME1,
                new GameInfo(
                    "Electronic Arts\\EA Games\\The Battle for Middle-earth",
                    "EA GAMES\\The Battle for Middle-earth",
                    "My Battle for Middle-earth Files",
                    "lotrbfme.exe")
            },
            {
                BfmeGame.BFME2,
                new GameInfo(
                    "Electronic Arts\\Electronic Arts\\The Battle for Middle-earth II",
                    "Electronic Arts\\The Battle for Middle-earth II",
                    "My Battle for Middle-earth II Files",
                    "lotrbfme2.exe")
            },
            {
                BfmeGame.ROTWK,
                new GameInfo(
                    "Electronic Arts\\Electronic Arts\\The Lord of the Rings, The Rise of the Witch-king",
                    "Electronic Arts\\The Lord of the Rings, The Rise of the Witch-king",
                    "My Rise of the Witch-king Files",
                    "lotrbfme2ep1.exe")
            }
        };

        [SupportedOSPlatform("windows")]
        public static string GetKeyValue(BfmeGame game, BfmeRegistryKey key)
        {
            string registryBasePath = SoftwareRegistryPath + (is64Bit ? Wow64Node : "");

            string name = key switch
            {
                BfmeRegistryKey.InstallPath => "InstallPath",
                BfmeRegistryKey.Language => "Language",
                BfmeRegistryKey.MapPackVersion => "MapPackVersion",
                BfmeRegistryKey.UseLocalUserMaps => "UseLocalUserMaps",
                BfmeRegistryKey.UserDataLeafName => "UserDataLeafName",
                BfmeRegistryKey.Version => "Version",
                _ => EmptyRegistryKeyOrPath,
            };

            string subKeySuffix = key == BfmeRegistryKey.SerialKey ? "\\ergc" : string.Empty;

            if (key == BfmeRegistryKey.InstallPath)
            {
                using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(Path.Combine(registryBasePath, GameInfos[game].DeprecatedRegistryKey), writable: false);
                if (registryKey != null)
                {
                    string? installPath = registryKey.GetValue("Install Dir") as string;
                    if (!string.IsNullOrEmpty(installPath))
                    {
                        return installPath;
                    }
                }
            }

            using RegistryKey? registryKey2 = Registry.CurrentUser.OpenSubKey(Path.Combine(registryBasePath, GameInfos[game].DefaultRegistryKey) + subKeySuffix, writable: false);
            return registryKey2?.GetValue(name) as string ?? EmptyRegistryKeyOrPath;
        }

        [SupportedOSPlatform("windows")]
        public static void SetKeyValue(BfmeGame game, BfmeRegistryKey key, string value, RegistryValueKind valueType = RegistryValueKind.String)
        {
            string name = key switch
            {
                BfmeRegistryKey.InstallPath => "InstallPath",
                BfmeRegistryKey.Language => "Language",
                BfmeRegistryKey.MapPackVersion => "MapPackVersion",
                BfmeRegistryKey.UseLocalUserMaps => "UseLocalUserMaps",
                BfmeRegistryKey.UserDataLeafName => "UserDataLeafName",
                BfmeRegistryKey.Version => "Version",
                BfmeRegistryKey.SerialKey => "",
                _ => "",
            };
            string text = key != BfmeRegistryKey.SerialKey ? "" : "\\ergc";
            string value2 = text;
            using RegistryKey? registryKey = Registry.CurrentUser.CreateSubKey(
                Path.Combine(SoftwareRegistryPath, nint.Size == 8 ? Wow64Node : EmptyRegistryKeyOrPath, GameInfos[game].DefaultRegistryKey + value2),
                writable: true
            );

            registryKey?.SetValue(name, value, valueType);
        }

        [SupportedOSPlatform("windows")]
        public static void CreateNewInstallRegistry(BfmeGame game, string installPath, string language)
        {
            if (!Path.EndsInDirectorySeparator(installPath))
            {
                ReadOnlySpan<char> readOnlySpan = installPath;
                char reference = Path.DirectorySeparatorChar;
                installPath = string.Concat(readOnlySpan, new ReadOnlySpan<char>(ref reference));
            }
            if (!Directory.Exists(installPath))
            {
                Directory.CreateDirectory(installPath);
            }
            EnsureFixedRegistry(game);
            SetKeyValue(game, BfmeRegistryKey.InstallPath, installPath);
            SetKeyValue(game, BfmeRegistryKey.Language, language);
            SetKeyValue(game, BfmeRegistryKey.MapPackVersion, "65536", RegistryValueKind.DWord);
            SetKeyValue(game, BfmeRegistryKey.UseLocalUserMaps, "0", RegistryValueKind.DWord);
            SetKeyValue(game, BfmeRegistryKey.UserDataLeafName, GameInfos[game].UserDataLeafName);
            SetKeyValue(game, BfmeRegistryKey.Version, "65539", RegistryValueKind.DWord);
            SetKeyValue(game, BfmeRegistryKey.SerialKey, new string((from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 20) select s[Random.Shared.Next(s.Length)]).ToArray()));
        }

        [SupportedOSPlatform("windows")]
        internal static void CreateAppPathRegistryKey(BfmeGame game)
        {
            string appPathRegistryKey = Path.Combine(SoftwareRegistryPath, is64Bit ? Wow64Node : EmptyRegistryKeyOrPath, "Microsoft\\Windows\\CurrentVersion\\App Paths", GameInfos[game].ExecutableName);

            using RegistryKey? registryKey = Registry.CurrentUser.CreateSubKey(appPathRegistryKey, writable: true);
            if (registryKey == null) return;

            registryKey.SetValue("", Path.Combine(GetKeyValue(game, BfmeRegistryKey.InstallPath), GameInfos[game].ExecutableName));
            registryKey.SetValue("Game Registry", Path.Combine(SoftwareRegistryPath, GameInfos[game].DefaultRegistryKey));
            registryKey.SetValue("Installed", "1", RegistryValueKind.DWord);
            registryKey.SetValue("Path", GetKeyValue(game, BfmeRegistryKey.InstallPath));
            registryKey.SetValue("Restart", "0", RegistryValueKind.DWord);
        }


        internal static void DeleteAppPathRegistryKey(BfmeGame game)
        {
            string appPathRegistryKey = Path.Combine(SoftwareRegistryPath, is64Bit ? Wow64Node : EmptyRegistryKeyOrPath, "Microsoft\\Windows\\CurrentVersion\\App Paths", GameInfos[game].ExecutableName);
            Registry.CurrentUser.DeleteSubKeyTree(appPathRegistryKey, throwOnMissingSubKey: false);
        }

        [SupportedOSPlatform("windows")]
        internal static void EnsureFixedRegistry(BfmeGame game)
        {
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                return;

            using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(SoftwareRegistryPath + (is64Bit ? Wow64Node : EmptyRegistryKeyOrPath) + "\\" + GameInfos[game].DeprecatedRegistryKey, writable: false);
            if (registryKey != null)
            {
                SetKeyValue(game, BfmeRegistryKey.InstallPath, registryKey.GetValue("Install Dir")?.ToString() ?? string.Empty);
                Registry.CurrentUser.DeleteSubKeyTree(SoftwareRegistryPath + (is64Bit ? Wow64Node : EmptyRegistryKeyOrPath) + "\\" + GameInfos[game].DeprecatedRegistryKey, throwOnMissingSubKey: false);
            }
        }

        [SupportedOSPlatform("windows")]
        internal static bool IsInstalled(BfmeGame game)
        {
            if (GetKeyValue(game, BfmeRegistryKey.InstallPath) != "")
            {
                return Directory.Exists(GetKeyValue(game, BfmeRegistryKey.InstallPath));
            }
            return false;
        }
    }

    internal class GameInfo(string defaultRegistryKey, string deprecatedRegistryKey, string userDataLeafName, string executableName)
    {
        internal string DefaultRegistryKey { get; } = defaultRegistryKey;
        internal string DeprecatedRegistryKey { get; } = deprecatedRegistryKey;
        internal string UserDataLeafName { get; } = userDataLeafName;
        internal string ExecutableName { get; } = executableName;
    }
}