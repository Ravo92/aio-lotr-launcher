using Helper;
using PatchLauncher.Properties;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PatchLauncher
{
    internal class Program
    {
        static readonly Mutex _mutex = new(true, ConstStrings.C_MUTEX_NAME);
        static readonly string RegistryDetectedGameLanguage = RegistryService.ReadRegKeyBFME2("Language");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            try
            {
                string configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                if (!File.Exists(configPath))
                {
                    Settings.Default.Upgrade();
                    Settings.Default.Reload();
                    Settings.Default.Save();
                }

                if (args.Length < 1)
                {
                    return;
                }
                else if (args[0] == "--showLauncherUpdateLog")
                {
                    LogHelper.LoggerBFME2GUI.Information(string.Format("Launched after LauncherUpdate now with version: > {0} <", AssemblyNameHelper.BFMELauncherGameVersion));
                    Settings.Default.OpenLauncherChangelogPageAfterUpdate = true;
                    Settings.Default.Save();
                }
                else if (args[0] != "--official")
                {
                    LogHelper.LoggerBFME2GUI.Warning(string.Format("Parameter > {0} < not the expected one at void Main() args in {1}", args[0], AssemblyNameHelper.BFMELauncherGameName));
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex, "");
                return;
            }

            ApplicationConfiguration.Initialize();

            if (Settings.Default.LauncherLanguage == "en")
                Settings.Default.LauncherLanguage = "English";

            if (Settings.Default.LauncherLanguage == "de")
                Settings.Default.LauncherLanguage = "German";

            Settings.Default.Save();

            try
            {
                Thread.CurrentThread.CurrentUICulture = Settings.Default.LauncherLanguage switch
                {
                    "German" => new System.Globalization.CultureInfo("German"),
                    _ => new System.Globalization.CultureInfo("en"),
                };

                if (!Settings.Default.IsGameInstalled)
                {
                    switch (Settings.Default.InstalledLanguageISOCode)
                    {
                        case "German":
                            if (RegistryDetectedGameLanguage == ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                            {
                                Settings.Default.InstalledLanguageISOCode = "German";
                                Settings.Default.Save();
                            }
                            else
                            {
                                Settings.Default.InstalledLanguageISOCode = RegistryDetectedGameLanguage;
                                Settings.Default.Save();
                            }
                            break;

                        default:
                            if (RegistryDetectedGameLanguage == ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                            {
                                Settings.Default.InstalledLanguageISOCode = "English";
                                Settings.Default.Save();
                            }
                            else
                            {
                                Settings.Default.InstalledLanguageISOCode = RegistryDetectedGameLanguage;
                                Settings.Default.Save();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex, "");
            }

            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    GameFileTools _gameFileTools = new();
                    GameFileDictionary gameFileDictionary = GameFileTools.LoadGameFileDictionary().Result;

                    JSONDataListHelper._DictionarylanguageSettings = gameFileDictionary.LanguagePacks[AssemblyNameHelper.BFMELauncherGameName].ToDictionary(x => x.RegistrySelectedLanguage, x => x);
                    JSONDataListHelper._MainPackSettings = gameFileDictionary.MainPacks[AssemblyNameHelper.BFMELauncherGameName];
                    JSONDataListHelper._DictionaryPatchPacksSettings = gameFileDictionary.PatchPacks[AssemblyNameHelper.BFMELauncherGameName].ToDictionary(x => x.Index, x => x);

                    PatchPacks _latestPatchPack = JSONDataListHelper._DictionaryPatchPacksSettings[JSONDataListHelper._DictionaryPatchPacksSettings.Keys.Max()];
                    PatchPacksBeta _betaPatchFiles = JSONDataListHelper._PatchBetaSettings = gameFileDictionary.PatchPacksBeta[AssemblyNameHelper.BFMELauncherGameName];

                    Settings.Default.LatestPatchVersion = _latestPatchPack.MajorVersion;
                    Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME2GUI.Error(ex, "JSON File Error!");
                    Application.Exit();
                }

                try
                {
                    GameFileTools.EnsureBFMEAppDataFolderExists(AssemblyNameHelper.BFMELauncherGameName);
                    GameFileTools.EnsureBFMEOptionsIniFileExists(AssemblyNameHelper.BFMELauncherGameName);

                    Application.Run(new WinFormsMainGUI());
                    _mutex.ReleaseMutex();
                    _mutex.Dispose();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME2GUI.Error(ex, "Window Related Error!");
                }
            }
            else
            {
                MessageBox.Show(Strings.Warning_AlreadyRunning, Strings.Warning_AlreadyRunningTitle);
                Application.Exit();
            }
        }
    }
}