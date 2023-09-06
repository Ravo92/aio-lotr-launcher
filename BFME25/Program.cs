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

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    return;
                }
                else if (args[0] == "--showLauncherUpdateLog")
                {
                    LogHelper.LoggerBFME25GUI.Information(string.Format("Launched after LauncherUpdate now with version: > {0} <", AssemblyNameHelper.BFMELauncherGameVerion));
                    Settings.Default.OpenLauncherChangelogPageAfterUpdate = true;
                    Settings.Default.Save();
                }
                else if (args[0] != "--official")
                {
                    LogHelper.LoggerBFME25GUI.Warning(string.Format("Parameter > {0} < not the expected one at void Main() args in {1}", args[0], AssemblyNameHelper.BFMELauncherGameName));
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME25GUI.Error(ex, "");
                return;
            }

            ApplicationConfiguration.Initialize();

            try
            {
                string configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                if (!File.Exists(configPath))
                {
                    Settings.Default.Upgrade();
                    Settings.Default.Reload();
                    Settings.Default.Save();
                }

                if (!Settings.Default.IsGameInstalled)
                {
                    switch (Settings.Default.LauncherLanguage)
                    {
                        case "de":
                            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");

                            if (RegistryService.ReadRegKeyBFME25("locale") == "de" || RegistryService.ReadRegKeyBFME25("locale") == ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                            {
                                Settings.Default.InstalledLanguageISOCode = "de";
                                Settings.Default.Save();
                            }
                            else
                            {
                                Settings.Default.InstalledLanguageISOCode = RegistryService.ReadRegKeyBFME25("locale");
                                Settings.Default.Save();
                            }

                            break;

                        default:
                            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

                            if (RegistryService.ReadRegKeyBFME25("locale") == "en_uk" || RegistryService.ReadRegKeyBFME25("locale") == ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
                            {
                                Settings.Default.InstalledLanguageISOCode = "en_uk";
                                Settings.Default.Save();
                            }
                            else
                            {
                                Settings.Default.InstalledLanguageISOCode = RegistryService.ReadRegKeyBFME25("locale");
                                Settings.Default.Save();
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME25GUI.Error(ex, "");
            }

            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    GameFileTools _gameFileTools = new();
                    GameFileDictionary gameFileDictionary = GameFileTools.LoadGameFileDictionary().Result;

                    JSONDataListHelper._DictionarylanguageSettings = gameFileDictionary.LanguagePacks[AssemblyNameHelper.BFMELauncherGameName].ToDictionary(x => x.RegistrySelectedLocale, x => x);
                    JSONDataListHelper._MainPackSettings = gameFileDictionary.MainPacks[AssemblyNameHelper.BFMELauncherGameName];
                    JSONDataListHelper._DictionaryPatchPacksSettings = gameFileDictionary.PatchPacks[AssemblyNameHelper.BFMELauncherGameName].ToDictionary(x => x.Version, x => x);

                    PatchPacks _latestPatchPack = JSONDataListHelper._DictionaryPatchPacksSettings[JSONDataListHelper._DictionaryPatchPacksSettings.Keys.Max()];
                    //PatchPacksBeta _betaPatchFiles = JSONDataListHelper._PatchBetaSettings = gameFileDictionary.PatchPacksBeta[AssemblyNameHelper.BFMELauncherGameName];

                    Settings.Default.LatestPatchVersion = _latestPatchPack.Version;
                    Settings.Default.Save();

                    GameFileTools.EnsureBFMEAppdataFolderExists(AssemblyNameHelper.BFMELauncherGameName);
                    GameFileTools.EnsureBFMEOptionsIniFileExists(AssemblyNameHelper.BFMELauncherGameName);

                    Application.Run(new WinFormsMainGUI());
                    _mutex.ReleaseMutex();
                    _mutex.Dispose();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME1GUI.Error(ex, "JSON File Error!");
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