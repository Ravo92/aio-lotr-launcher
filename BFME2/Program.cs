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
        static readonly string RegistryDetectedGameLocale = RegistryService.ReadRegKeyBFME2("locale");

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
                    LogHelper.LoggerBFME2GUI.Information(string.Format("Launched after LauncherUpdate now with version: > {0} <", AssemblyNameHelper.BFMELauncherGameVerion));
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

            try
            {
                Thread.CurrentThread.CurrentUICulture = Settings.Default.LauncherLanguage switch
                {
                    "de" => new System.Globalization.CultureInfo("de"),
                    _ => new System.Globalization.CultureInfo("en"),
                };

                if (!Settings.Default.IsGameInstalled)
                {
                    switch (Settings.Default.InstalledLanguageISOCode)
                    {
                        case "de":
                            if (RegistryDetectedGameLocale == ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                            {
                                Settings.Default.InstalledLanguageISOCode = "de";
                                Settings.Default.Save();
                            }
                            else
                            {
                                Settings.Default.InstalledLanguageISOCode = RegistryDetectedGameLocale;
                                Settings.Default.Save();
                            }
                            break;

                        default:
                            if (RegistryDetectedGameLocale == ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                            {
                                Settings.Default.InstalledLanguageISOCode = "en_uk";
                                Settings.Default.Save();
                            }
                            else
                            {
                                Settings.Default.InstalledLanguageISOCode = RegistryDetectedGameLocale;
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

                    JSONDataListHelper._DictionarylanguageSettings = gameFileDictionary.LanguagePacks[AssemblyNameHelper.BFMELauncherGameName].ToDictionary(x => x.RegistrySelectedLocale, x => x);
                    JSONDataListHelper._MainPackSettings = gameFileDictionary.MainPacks[AssemblyNameHelper.BFMELauncherGameName];
                    JSONDataListHelper._DictionaryPatchPacksSettings = gameFileDictionary.PatchPacks[AssemblyNameHelper.BFMELauncherGameName].ToDictionary(x => x.Index, x => x);

                    PatchPacks _latestPatchPack = JSONDataListHelper._DictionaryPatchPacksSettings[JSONDataListHelper._DictionaryPatchPacksSettings.Keys.Max()];
                    PatchPacksBeta _betaPatchFiles = JSONDataListHelper._PatchBetaSettings = gameFileDictionary.PatchPacksBeta[AssemblyNameHelper.BFMELauncherGameName];

                    Settings.Default.LatestPatchVersion = _latestPatchPack.MinorVersion * 10 + _latestPatchPack.Revision;
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