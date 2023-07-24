using Helper;
using PatchLauncher.Properties;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace PatchLauncher
{
    internal class Program
    {
        static readonly Mutex _mutex = new(true, ConstStrings.C_MUTEX_NAME);
        readonly static string assemblyName = AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Name!;

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
                else if (args[0] != "--official")
                {
                    LogHelper.LoggerBFME1GUI.Warning("Parameter > {0} < not the expected one at void Main() args in {1}", args[0], assemblyName);
                    File.Copy("lol", "lol2");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex, "");
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

                switch (Settings.Default.Language)
                {
                    case 0:

                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                        if (Settings.Default.InstalledLanguageISOCode == "" || Settings.Default.InstalledLanguageISOCode is null)
                            Settings.Default.InstalledLanguageISOCode = "en_us";
                        break;

                    case 1:

                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                        if (Settings.Default.InstalledLanguageISOCode == "" || Settings.Default.InstalledLanguageISOCode is null)
                            Settings.Default.InstalledLanguageISOCode = "de";
                        break;

                    default:
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                        if (Settings.Default.InstalledLanguageISOCode == "" || Settings.Default.InstalledLanguageISOCode is null)
                            Settings.Default.InstalledLanguageISOCode = "en_us";
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex, "");
            }

            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                GameFileTools _gameFileTools = new();
                GameFileDictionary gameFileDictionary = GameFileTools.LoadGameFileDictionary().Result;

                JSONDataListHelper._DictionarylanguageSettings = gameFileDictionary.LanguagePacks[assemblyName].ToDictionary(x => x.RegistrySelectedLocale, x => x);
                JSONDataListHelper._MainPackSettings = gameFileDictionary.MainPacks[assemblyName];
                JSONDataListHelper._DictionaryPatchPacksSettings = gameFileDictionary.PatchPacks[assemblyName].ToDictionary(x => x.Version, x => x);

                PatchPacks _latestPatchPack = JSONDataListHelper._DictionaryPatchPacksSettings[JSONDataListHelper._DictionaryPatchPacksSettings.Keys.Max()];
                PatchPacksBeta _betaPatchFiles = JSONDataListHelper._PatchBetaSettings = gameFileDictionary.PatchPacksBeta[assemblyName];

                Settings.Default.LatestBetaPatchVersion = _betaPatchFiles.Version;
                Settings.Default.LatestPatchVersion = _latestPatchPack.Version;
                Settings.Default.Save();

                GameFileTools.EnsureBFMEAppdataFolderExists();
                GameFileTools.EnsureBFMEOptionsIniFileExists();

                //InstallLanguageList._DictionarylanguageSettings = gameFileDictionary.LanguagePacks["BFME1"]
                //  .ToDictionary(x => x.RegistrySelectedLocale, x => new LanguageSettings { RegistrySelectedLanguageName = x.RegistrySelectedLanguageName, RegistrySelectedLanguage = x.RegistrySelectedLanguage, RegistrySelectedLocale = x.RegistrySelectedLocale, LanguagPackName = x.LanguagePackName });

                Application.Run(new WinFormsMainGUI());
                _mutex.ReleaseMutex();
                _mutex.Dispose();
            }
            else
            {
                MessageBox.Show(Strings.Warning_AlreadyRunning, Strings.Warning_AlreadyRunningTitle);
                Application.Exit();
            }
        }
    }
}