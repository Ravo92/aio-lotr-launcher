using AutoUpdaterDotNET;
using Helper;
using Microsoft.Web.WebView2.Core;
using PatchLauncher.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class WinFormsMainGUI : Form
    {
        int iconNumber = Settings.Default.BackgroundMusicIcon;
        readonly SoundPlayerHelper _soundPlayerHelper = new();

        readonly LanguagePacks _languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[Settings.Default.InstalledLanguageISOCode];
        readonly MainPacks _mainPack = JSONDataListHelper._MainPackSettings;

        private static bool _IsLauncherCurrentlyWorking = false;

        private bool IsLauncherCurrentlyWorking
        {
            get { return _IsLauncherCurrentlyWorking; }
            set
            {
                _IsLauncherCurrentlyWorking = value;
                if (value)
                {
                    PiBVersion103.Enabled = false;
                    PiBVersion106.Enabled = false;

                    PiBVersion222_33.Enabled = false;
                    PiBVersion222_34.Enabled = false;

                    PiBArrow.Enabled = false;

                    if (Settings.Default.IsPatchModsShown)
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
                    else
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;

                    PBarActualFile.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    PiBArrow.Enabled = true;
                    BtnInstall.Enabled = false;

                    LaunchGameToolStripMenuItem.Enabled = false;
                    RepairGameToolStripMenuItem.Enabled = false;
                }
                else
                {
                    PiBVersion103.Enabled = true;
                    PiBVersion106.Enabled = true;

                    PiBVersion222_33.Enabled = true;
                    PiBVersion222_34.Enabled = true;

                    PBarActualFile.Hide();
                    LblDownloadSpeed.Hide();
                    LblFileName.Hide();

                    PiBArrow.Enabled = true;
                    BtnInstall.Enabled = true;

                    if (Settings.Default.IsPatchModsShown)
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                    else
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;

                    LaunchGameToolStripMenuItem.Enabled = true;
                    RepairGameToolStripMenuItem.Enabled = true;
                }
            }
        }

        public WinFormsMainGUI()
        {
            #region logic

            InitializeComponent();
            InitializeWebView2Settings();

            var test = JSONDataListHelper._DictionaryPatchPacksSettings.Keys.Max();

            SysTray.ContextMenuStrip = NotifyContextMenu;

            try
            {
                if (!Directory.Exists(RegistryService.GameAppdataFolderPath()))
                    Directory.CreateDirectory(RegistryService.GameAppdataFolderPath());

                if (!File.Exists(Path.Combine(RegistryService.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME)))
                    File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_OPTIONSINI_FILENAME), Path.Combine(RegistryService.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME));
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLine(ConstStrings.LogTime + ex.ToString());
            }

            BtnInstall.Text = Strings.BtnInstall_TextLaunch;

            #endregion

            #region Styles
            //Main Form style behaviour

            PibLoadingRing.Show();
            PibLoadingBorder.Show();
            PiBArrow.Hide();
            LblPatchNotes.Show();
            PnlPlaceholder.Hide();
            Wv2Patchnotes.Hide();

            PiBVersion222_33.Show();
            PiBVersion222_34.Show();

            // label-Styles
            LblDownloadSpeed.Font = FontHelper.GetFont(0, 16); ;
            LblDownloadSpeed.ForeColor = Color.FromArgb(192, 145, 69);
            LblDownloadSpeed.BackColor = Color.Transparent;

            LblFileName.Font = FontHelper.GetFont(0, 16); ;
            LblFileName.ForeColor = Color.FromArgb(192, 145, 69);
            LblFileName.BackColor = Color.Transparent;

            LblPatchNotes.Font = FontHelper.GetFont(0, 16);
            LblPatchNotes.ForeColor = Color.FromArgb(192, 145, 69);
            LblPatchNotes.BackColor = Color.Transparent;
            LblPatchNotes.BorderStyle = BorderStyle.None;

            LblInstalledPatches.Font = FontHelper.GetFont(1, 24);
            LblInstalledPatches.ForeColor = Color.FromArgb(192, 145, 69);
            LblInstalledPatches.BackColor = Color.Transparent;
            LblInstalledPatches.BorderStyle = BorderStyle.None;
            LblInstalledPatches.OutlineWidth = 6;

            LblModExplanation.Font = FontHelper.GetFont(0, 20);
            LblModExplanation.ForeColor = Color.FromArgb(192, 145, 69);
            LblModExplanation.BackColor = Color.Transparent;
            LblModExplanation.BorderStyle = BorderStyle.None;
            LblModExplanation.OutlineWidth = 6;

            PBarActualFile.ForeColor = Color.FromArgb(192, 145, 69);
            PBarActualFile.BackColor = Color.FromArgb(255, 100, 0);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnInstall.Font = FontHelper.GetFont(0, 16); ;
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);

            #endregion

            #region Tooltips
            //Tooltips
            ToolTip.SetToolTip(PiBThemeSwitcher, Strings.ToolTip_MusicSwitcher);
            #endregion

            #region HUD Elements
            PibHeader.Image = Helper.Properties.Resources.header;
            PiBYoutube.Image = Helper.Properties.Resources.youtube;
            PiBDiscord.Image = Helper.Properties.Resources.discord;
            PiBModDB.Image = Helper.Properties.Resources.moddb;
            PiBTwitch.Image = Helper.Properties.Resources.twitch;

            if (Settings.Default.IsPatchModsShown)
                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;

            PibHeader.Image = Helper.Properties.Resources.header;
            PibLoadingBorder.Image = Helper.Properties.Resources.loadingBorder;
            PibLoadingRing.Image = Helper.Properties.Resources.loadingRing;

            PiBVersion103.Image = Helper.Properties.Resources.BtnPatchSelection_103;

            if (Settings.Default.PlayBackgroundMusic)
                PibMute.Image = Helper.Properties.Resources.Unmute;
            else
                PibMute.Image = Helper.Properties.Resources.Mute;


            if (Settings.Default.IsPatch106Installed)
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Selected;
            else
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;


            if (Settings.Default.IsPatch33Installed)
                PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Selected;
            else
                PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33;


            if (Settings.Default.IsPatch34Installed)
                PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Selected;
            else
                PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34;

            if (Settings.Default.IsPatch34Installed)
                Settings.Default.PatchVersionInstalled = 34;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Settings.Default.BackgroundMusicIcon == 0)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoDefault;
                BackgroundImage = Helper.Properties.Resources.bgDefault;
            }
            else if (Settings.Default.BackgroundMusicIcon == 1)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoGondor;
                BackgroundImage = Helper.Properties.Resources.bgGondor;
            }
            else if (Settings.Default.BackgroundMusicIcon == 2)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoRohan;
                BackgroundImage = Helper.Properties.Resources.bgRohan;
            }
            else if (Settings.Default.BackgroundMusicIcon == 3)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoIsengard;
                BackgroundImage = Helper.Properties.Resources.bgIsengard;
            }
            else if (Settings.Default.BackgroundMusicIcon == 4)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoMordor;
                BackgroundImage = Helper.Properties.Resources.bgMordor;
            }
            #endregion

            TmrPatchNotes.Tick += new EventHandler(TmrPatchNotes_Tick);
            TmrPatchNotes.Interval = 2000;
            TmrPatchNotes.Start();
        }

        #region Launcher Auto-Updater

        private static async void InitializeWebView2Settings()
        {
            try
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, ConstStrings.C_LOGFOLDER_NAME, "webView2_Version.log"), CoreWebView2Environment.GetAvailableBrowserVersionString());
            }
            catch (WebView2RuntimeNotFoundException)
            {
                await RunWebViewSilentSetupAsync(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, "MicrosoftEdgeWebview2Setup.exe"));
            }
        }

        public static async Task RunWebViewSilentSetupAsync(string fileName)
        {
            Process _p = Process.Start(fileName, new[] { "/silent", "/install" });
            await _p.WaitForExitAsync().ConfigureAwait(false);
        }

        public static void CheckForUpdates()
        {
            try
            {
                AutoUpdater.Start("https://ravo92.github.io/LauncherUpdater.xml");
                AutoUpdater.InstalledVersion = Assembly.GetEntryAssembly()!.GetName().Version;
                AutoUpdater.ShowSkipButton = false;
                AutoUpdater.ShowRemindLaterButton = true;
                AutoUpdater.LetUserSelectRemindLater = false;
                AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Minutes;
                AutoUpdater.RemindLaterAt = 10;
                AutoUpdater.UpdateFormSize = new Size(1296, 759);
                AutoUpdater.HttpUserAgent = "BFME Launcher Update";
                AutoUpdater.AppTitle = Application.ProductName;
                AutoUpdater.RunUpdateAsAdmin = true;
                AutoUpdater.DownloadPath = Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME);
                AutoUpdater.ClearAppDirectory = false;
                AutoUpdater.ReportErrors = false;

                string jsonPath = Path.Combine(Environment.CurrentDirectory, "AutoUpdaterSettings.json");
                AutoUpdater.PersistenceProvider = new JsonFilePersistenceProvider(jsonPath);
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLine(ConstStrings.LogTime + ex.ToString());
            }
        }

        #endregion

        #region Button Behaviours

        public void GameisClosed(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                    SysTray.Visible = false;
                    IsLauncherCurrentlyWorking = false;
                }));
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                SysTray.Visible = false;
                IsLauncherCurrentlyWorking = false;
            }

            if (Settings.Default.PlayBackgroundMusic)
            {
                _soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }
        }

        private async void BtnInstall_Click(object sender, EventArgs e)
        {
            if (Settings.Default.IsGameInstalled == false)
            {
                InstallPathDialog _install = new();

                DialogResult dr = _install.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    await InstallUpdatRepairRoutine(ConstStrings.C_MAINGAMEFILE_ZIP, _mainPack.URL, _mainPack.MD5, true);
                }
            }
            else
            {
                LaunchGameToolStripMenuItem.PerformClick();
            }
        }

        private void BtnInstall_MouseLeave(object sender, EventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnInstall_MouseEnter(object sender, EventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnInstall.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnInstall_MouseDown(object sender, MouseEventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void PiBYoutube_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.youtube.com/BeyondStandards") { UseShellExecute = true });
        }

        private void PiBDiscord_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/Q5Yyy3XCuu") { UseShellExecute = true });
        }

        private void PiBModDB_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.moddb.com/mods/battle-for-middle-earth-patch-222") { UseShellExecute = true });
        }

        private void PiBTwitch_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.twitch.tv/beyondstandards") { UseShellExecute = true });
        }

        private void PibMute_Click(object sender, EventArgs e)
        {
            if (Settings.Default.PlayBackgroundMusic)
            {
                PibMute.Image = Helper.Properties.Resources.Mute;
                Settings.Default.PlayBackgroundMusic = false;
                _soundPlayerHelper.StopTheme();
                Settings.Default.Save();
            }
            else
            {
                PibMute.Image = Helper.Properties.Resources.Unmute;
                Settings.Default.PlayBackgroundMusic = true;
                Settings.Default.Save();
                _soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }
        }

        private void PiBThemeSwitcher_Click(object sender, EventArgs e)
        {
            iconNumber++;
            if (iconNumber >= 5)
                iconNumber = 0;

            switch (iconNumber)
            {
                case 0:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_DEFAULT;
                        Settings.Default.BackgroundMusicIcon = 0;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoDefault;
                        BackgroundImage = Helper.Properties.Resources.bgDefault;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            _soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_DEFAULT);
                        }

                        break;
                    }
                case 1:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_GONDOR;
                        Settings.Default.BackgroundMusicIcon = 1;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoGondor;
                        BackgroundImage = Helper.Properties.Resources.bgGondor;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            _soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_GONDOR);
                        }
                        break;
                    }
                case 2:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_ROHAN;
                        Settings.Default.BackgroundMusicIcon = 2;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoRohan;
                        BackgroundImage = Helper.Properties.Resources.bgRohan;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            _soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_ROHAN);
                        }
                        break;
                    }
                case 3:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_ISENGARD;
                        Settings.Default.BackgroundMusicIcon = 3;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoIsengard;
                        BackgroundImage = Helper.Properties.Resources.bgIsengard;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            _soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_ISENGARD);
                        }
                        break;
                    }
                case 4:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_MORDOR;
                        Settings.Default.BackgroundMusicIcon = 4;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoMordor;
                        BackgroundImage = Helper.Properties.Resources.bgMordor;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            _soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_MORDOR);
                        }
                        break;
                    }
            }
        }

        private void PiBVersion103_Click(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;

            try
            {
                PatchModDetectionHelper.DeletePatch106();
                PatchModDetectionHelper.DeletePatch222Files();

                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                Settings.Default.PatchVersionInstalled = 103;
                Settings.Default.IsPatch106Installed = false;
                Settings.Default.IsPatch33Installed = false;
                Settings.Default.IsPatch34Installed = false;


                if (Settings.Default.IsPatch106Downloaded)
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;
                else
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Download;


                if (Settings.Default.IsPatch33Downloaded)
                    PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33;
                else
                    PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Download;


                if (Settings.Default.IsPatch34Downloaded)
                    PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34;
                else
                    PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Download;

                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLine(ConstStrings.LogTime + ex.ToString());
            }

            IsLauncherCurrentlyWorking = false;
        }

        private async void PiBVersion106_Click(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;
            PatchPacks _patchpack106 = JSONDataListHelper._DictionaryPatchPacksSettings[106];
            LanguageFiles patchPackLanguages = _patchpack106.LanguageFiles[Settings.Default.InstalledLanguageISOCode];

            IsLauncherCurrentlyWorking = true;

            try
            {
                if (!Settings.Default.IsPatch106Downloaded)
                {
                    await InstallUpdatRepairRoutine(_patchpack106.FileName, _patchpack106.URL, _patchpack106.MD5, false);

                    if (_patchpack106.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5, false);
                    }

                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Selected;

                    Settings.Default.PatchVersionInstalled = 106;
                    Settings.Default.IsPatch106Downloaded = true;
                    Settings.Default.IsPatch106Installed = true;

                    Settings.Default.IsPatch33Installed = false;
                    Settings.Default.IsPatch34Installed = false;


                    if (Settings.Default.IsPatch33Downloaded)
                        PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33;
                    else
                        PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Download;


                    if (Settings.Default.IsPatch34Downloaded)
                        PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34;
                    else
                        PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Download;
                }
                else
                {
                    if (Settings.Default.IsPatch106Installed)
                    {
                        Settings.Default.PatchVersionInstalled = 103;
                        Settings.Default.IsPatch106Installed = false;
                        PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PatchModDetectionHelper.DeletePatch106();
                    }
                    else
                    {
                        await InstallUpdatRepairRoutine(_patchpack106.FileName, _patchpack106.URL, _patchpack106.MD5, false);

                        if (_patchpack106.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                        {
                            await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5, false);
                        }

                        PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Selected;

                        Settings.Default.PatchVersionInstalled = 106;
                        Settings.Default.IsPatch106Downloaded = true;
                        Settings.Default.IsPatch106Installed = true;

                        Settings.Default.IsPatch33Installed = false;
                        Settings.Default.IsPatch34Installed = false;


                        if (Settings.Default.IsPatch33Downloaded)
                            PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33;
                        else
                            PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Download;


                        if (Settings.Default.IsPatch34Downloaded)
                            PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34;
                        else
                            PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Download;
                    }
                }

                Settings.Default.Save();

            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLine(ConstStrings.LogTime + ex.ToString());
            }

            IsLauncherCurrentlyWorking = false;
        }

        private async void PiBVersion222_33_Click(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;
            PatchPacks _patchpack22233 = JSONDataListHelper._DictionaryPatchPacksSettings[22233];
            LanguageFiles patchPackLanguages = _patchpack22233.LanguageFiles[Settings.Default.InstalledLanguageISOCode];

            try
            {
                if (!Settings.Default.IsPatch33Downloaded)
                {
                    await InstallUpdatRepairRoutine(_patchpack22233.FileName, _patchpack22233.URL, _patchpack22233.MD5, false);

                    if (_patchpack22233.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5, false);
                    }

                    File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_GERMANLANGUAGE_PATCH_FILE), Path.Combine(RegistryService.GameInstallPath(), ConstStrings.C_GERMANLANGUAGE_PATCH_FILE), true);

                    PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Selected;
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                    Settings.Default.PatchVersionInstalled = 33;
                    Settings.Default.IsPatch33Downloaded = true;
                    Settings.Default.IsPatch33Installed = true;

                    Settings.Default.IsPatch106Installed = false;
                    Settings.Default.IsPatch34Installed = false;


                    if (Settings.Default.IsPatch34Downloaded)
                        PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34;
                    else
                        PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Download;
                }
                else
                {
                    if (Settings.Default.IsPatch33Installed)
                    {
                        Settings.Default.PatchVersionInstalled = 103;
                        Settings.Default.IsPatch33Installed = false;
                        PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PatchModDetectionHelper.DeletePatch106();
                    }
                    else
                    {
                        PBarActualFile.Show();
                        LblDownloadSpeed.Show();
                        LblFileName.Show();

                        await InstallUpdatRepairRoutine(_patchpack22233.FileName, _patchpack22233.URL, _patchpack22233.MD5, false);

                        if (_patchpack22233.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                        {
                            await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5, false);
                        }

                        PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Selected;
                        PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                        Settings.Default.PatchVersionInstalled = 33;
                        Settings.Default.IsPatch33Downloaded = true;
                        Settings.Default.IsPatch33Installed = true;

                        Settings.Default.IsPatch106Installed = false;
                        Settings.Default.IsPatch34Installed = false;


                        if (Settings.Default.IsPatch34Downloaded)
                            PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34;
                        else
                            PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Download;
                    }
                }

                Settings.Default.Save();

            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLine(ConstStrings.LogTime + ex.ToString());
            }

            IsLauncherCurrentlyWorking = false;
        }

        private async void PiBVersion222_34_Click(object sender, EventArgs? e)
        {
            IsLauncherCurrentlyWorking = true;
            PatchPacks _patchpack22234 = JSONDataListHelper._DictionaryPatchPacksSettings[22234];
            LanguageFiles patchPackLanguages = _patchpack22234.LanguageFiles[Settings.Default.InstalledLanguageISOCode];

            try
            {
                if (!Settings.Default.IsPatch34Downloaded)
                {
                    PBarActualFile.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    await InstallUpdatRepairRoutine(_patchpack22234.FileName, _patchpack22234.URL, _patchpack22234.MD5, false);

                    if (_patchpack22234.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5, false);
                    }

                    PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Selected;
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                    Settings.Default.PatchVersionInstalled = 34;
                    Settings.Default.IsPatch34Downloaded = true;
                    Settings.Default.IsPatch34Installed = true;

                    Settings.Default.IsPatch106Installed = false;
                    Settings.Default.IsPatch33Installed = false;


                    if (Settings.Default.IsPatch33Downloaded)
                        PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33;
                    else
                        PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Download;
                }
                else
                {
                    if (Settings.Default.IsPatch34Installed)
                    {
                        Settings.Default.PatchVersionInstalled = 103;
                        Settings.Default.IsPatch34Installed = false;
                        PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PatchModDetectionHelper.DeletePatch106();
                    }
                    else
                    {
                        PBarActualFile.Show();
                        LblDownloadSpeed.Show();
                        LblFileName.Show();

                        await InstallUpdatRepairRoutine(_patchpack22234.FileName, _patchpack22234.URL, _patchpack22234.MD5, false);

                        if (_patchpack22234.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                        {
                            await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5, false);
                        }

                        PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Selected;
                        PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                        Settings.Default.PatchVersionInstalled = 34;
                        Settings.Default.IsPatch34Downloaded = true;
                        Settings.Default.IsPatch34Installed = true;

                        Settings.Default.IsPatch106Installed = false;
                        Settings.Default.IsPatch33Installed = false;


                        if (Settings.Default.IsPatch33Downloaded)
                            PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33;
                        else
                            PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Download;
                    }
                }
                Settings.Default.Save();

            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLine(ConstStrings.LogTime + ex.ToString());
            }

            IsLauncherCurrentlyWorking = false;
        }

        private void PiBArrow_Click(object sender, EventArgs e)
        {
            TmrAnimation.Enabled = true;
        }

        #endregion

        #region ToolTip System
        public void Tooltip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Font tooltipFont = FontHelper.GetFont(0, 16); ;
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, tooltipFont, Brushes.SandyBrown, new PointF(2, 2));
        }

        public void TooltipPopup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = TextRenderer.MeasureText(ToolTip.GetToolTip(e.AssociatedControl), FontHelper.GetFont(0, 16));
        }
        #endregion

        #region GameInstall
        public async Task InstallUpdatRepairRoutine(string ZIPFileName, string DownloadUrl, string md5hash, bool isFreshInstall)
        {
            IsLauncherCurrentlyWorking = true;

            try
            {
                if (!await PrepareInstallFolder())
                {
                    return;
                }

                BtnInstall.Text = Strings.BtnInstall_TextLaunch;

                if (isFreshInstall)
                {
                    RegistryService.WriteRegKeysInstallation(Settings.Default.GameInstallPath, _languagePackSettings.RegistrySelectedLocale, _languagePackSettings.RegistrySelectedLanguageName, _languagePackSettings.RegistrySelectedLanguage);
                }

                GameFileTools _gameFileTools = new();
                await _gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME), ZIPFileName, DownloadUrl);
                await _gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME), ZIPFileName, DownloadUrl);

                if (isFreshInstall)
                {
                    if (Settings.Default.CreateDesktopShortcut && !StartMenuHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                    {
                        GameDesktopShortcutToolStripMenuItem.PerformClick();
                    }

                    if (Settings.Default.CreateStartMenuShortcut && !StartMenuHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs\\Electronic Arts" + ConstStrings.C_GAMETITLE_NAME_EN), ConstStrings.C_GAMETITLE_NAME_EN))
                    {
                        GameStartmenuShortcutsToolStripMenuItem.PerformClick();
                    }

                    PiBVersion222_34_Click(PiBVersion222_34, null);
                }

                if (Settings.Default.IsPatchModsShown)
                {
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                }
                else
                {
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
                }
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                await file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }

            IsLauncherCurrentlyWorking = false;
        }

        private static async Task<bool> PrepareInstallFolder()
        {
            _IsLauncherCurrentlyWorking = true;

            try
            {
                if (!Directory.Exists(Settings.Default.GameInstallPath))
                {
                    Directory.CreateDirectory(Settings.Default.GameInstallPath);
                }

                if (Directory.Exists(Settings.Default.GameInstallPath) && Directory.GetFileSystemEntries(Settings.Default.GameInstallPath).Length != 0)
                {
                    DialogResult _dialogResult = MessageBox.Show(Strings.Msg_InstallFolderNotEmpty_Text, Strings.Msg_InstallFolderNotEmpty_Title, MessageBoxButtons.OKCancel);
                    if (_dialogResult == DialogResult.OK)
                    {
                        Directory.Delete(Settings.Default.GameInstallPath, true);
                    }
                    else if (_dialogResult == DialogResult.Cancel)
                    {
                        _IsLauncherCurrentlyWorking = false;
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                await file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
                _IsLauncherCurrentlyWorking = false;
                return false;
            }
        }
        #endregion

        private void TmrPatchNotes_Tick(object? sender, EventArgs e)
        {
            TmrPatchNotes.Stop();
            PibLoadingRing.Hide();
            PibLoadingBorder.Hide();
            LblPatchNotes.Hide();

            PiBArrow.BackColor = Color.FromArgb(24, 24, 24);
            PiBArrow.Show();

            Wv2Patchnotes.Show();
            PnlPlaceholder.Show();

            if (Settings.Default.ShowPatchesFirst)
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                PiBArrow.BackColor = Color.Transparent;
                PnlPlaceholder.BackgroundImage = Helper.Properties.Resources.borderRectangleModPanel;
                PnlPlaceholder.BackColor = Color.Transparent;
                LblInstalledPatches.BackColor = Color.Transparent;
                LblModExplanation.BackColor = Color.Transparent;
                LblModExplanation.Show();
                LblInstalledPatches.Show();

                PiBArrow.Left = 1212;
                Wv2Patchnotes.Left = 1300;

                Settings.Default.IsPatchModsShown = true;
                Settings.Default.Save();
            }
            else
            {
                PnlPlaceholder.BackColor = Color.FromArgb(24, 24, 24);
                PnlPlaceholder.BackgroundImage = null;
                PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
                PiBArrow.BackColor = Color.FromArgb(24, 24, 24);
                LblModExplanation.Hide();
                LblInstalledPatches.Hide();

                PiBArrow.Left = 14;
                Wv2Patchnotes.Left = 12;

                Settings.Default.IsPatchModsShown = false;
                Settings.Default.Save();
            }
        }

        private void TmrAnimation_Tick(object sender, EventArgs e)
        {
            int _startLeft = 12;  // start position of the panel
            int _endLeft = 1300;      // end position of the panel
            int _endLeftArrow = 1212;
            int _stepSize = 5;     // pixels to move
            int _endRight = 12;      // end position of the panel

            // incrementally move

            if (Wv2Patchnotes.Left == _startLeft)
            {
                while (Wv2Patchnotes.Left != _endLeft)
                {
                    Wv2Patchnotes.Left += _stepSize;

                    if (PiBArrow.Left < _endLeftArrow)
                    {
                        PiBArrow.Left += _stepSize;
                    }
                    // make sure we didn't over shoot
                    if (Wv2Patchnotes.Left > _endLeft) Wv2Patchnotes.Left = _endLeft;

                    // have we arrived?
                    if (Wv2Patchnotes.Left == _endLeft)
                    {
                        TmrAnimation.Enabled = false;
                    }
                }

                PiBArrow.Left = _endLeftArrow;

                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                PiBArrow.BackColor = Color.Transparent;
                PnlPlaceholder.BackgroundImage = Helper.Properties.Resources.borderRectangleModPanel;
                PnlPlaceholder.BackColor = Color.Transparent;
                LblInstalledPatches.BackColor = Color.Transparent;
                LblModExplanation.BackColor = Color.Transparent;
                LblModExplanation.Show();
                LblInstalledPatches.Show();

                Wv2Patchnotes.Hide();

                Settings.Default.IsPatchModsShown = true;
                Settings.Default.Save();
            }
            else
            {
                PnlPlaceholder.BackColor = Color.FromArgb(24, 24, 24);
                PnlPlaceholder.BackgroundImage = null;
                PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
                PiBArrow.BackColor = Color.FromArgb(24, 24, 24);
                LblModExplanation.Hide();
                LblInstalledPatches.Hide();
                Wv2Patchnotes.Show();

                Settings.Default.IsPatchModsShown = false;
                Settings.Default.Save();

                while (Wv2Patchnotes.Left != _endRight)
                {
                    Wv2Patchnotes.Left -= _stepSize;

                    if (PiBArrow.Left > _endRight)
                    {
                        PiBArrow.Left -= _stepSize;
                    }
                    // make sure we didn't over shoot
                    if (Wv2Patchnotes.Left < _endRight) Wv2Patchnotes.Left = _endRight;

                    // have we arrived?
                    if (Wv2Patchnotes.Left == _endRight)
                    {
                        TmrAnimation.Enabled = false;
                    }
                }

                PiBArrow.Left = _endRight;
            }
        }

        private async void BFME1_Shown(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;
            PatchPacks _patchpack222 = JSONDataListHelper._DictionaryPatchPacksSettings[Settings.Default.LatestPatchVersion];
            LanguageFiles patchPackLanguages = _patchpack222.LanguageFiles[Settings.Default.InstalledLanguageISOCode];

            try
            {
                // Check Music Settings
                if (Settings.Default.PlayBackgroundMusic)
                {
                    _soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
                }

                // Check if Game is installed, if not show install button
                if ((Settings.Default.GameInstallPath == "" && !Directory.Exists(RegistryService.ReadRegKey("path"))) || RegistryService.ReadRegKey("path") == "ValueNotFound" || !File.Exists(Path.Combine(RegistryService.ReadRegKey("path"), ConstStrings.C_MAIN_GAME_FILE)))
                {
                    Settings.Default.IsGameInstalled = false;
                    BtnInstall.Text = Strings.BtnInstall_TextInstall;
                }

                // Check if new Update is available and Update to latest 2.22 Patch version if not -> Update;
                // If Older patch is selected manually, dont Update!
                // If BetaChannel is selected, dont Update either!
                else if (Settings.Default.LatestPatchVersion > Settings.Default.PatchVersionInstalled && !Settings.Default.SelectedOlderPatch && !Settings.Default.UseBetaChannel)
                {
                    Settings.Default.IsPatch106Installed = false;
                    Settings.Default.IsPatch33Installed = false;
                    Settings.Default.IsPatch34Installed = false;

                    Settings.Default.IsGameInstalled = true;
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKey("path");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage();

                    await InstallUpdatRepairRoutine(_patchpack222.FileName, _patchpack222.URL, _patchpack222.MD5, false);

                    if (_patchpack222.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5, false);
                    }

                    Settings.Default.IsPatch34Downloaded = true;
                    Settings.Default.IsPatch34Installed = true;

                    PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Selected;
                }
                else
                {
                    Settings.Default.IsGameInstalled = true;
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKey("path");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage();
                    PiBArrow.Enabled = true;
                }

                if (Settings.Default.UseBetaChannel)
                {
                    PiBVersion103.Hide();
                    PiBVersion106.Hide();
                    PiBVersion222_33.Hide();
                    PiBVersion222_34.Hide();

                    LblModExplanation.Text = Strings.Info_BetaActivated;

                    if (Settings.Default.LatestBetaPatchVersion > Settings.Default.BetaChannelVersion)
                    {
                        await InstallUpdatRepairRoutine(_patchpack222.FileName, _patchpack222.URL, _patchpack222.MD5, false);
                    }
                }

                if (!Settings.Default.IsPatch106Downloaded)
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Download;

                if (!Settings.Default.IsPatch33Downloaded)
                    PiBVersion222_33.Image = Helper.Properties.Resources.BtnPatchSelection_222V33_Download;

                if (!Settings.Default.IsPatch34Downloaded)
                    PiBVersion222_34.Image = Helper.Properties.Resources.BtnPatchSelection_222V34_Download;



                if (StartMenuHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_GAMETITLE_NAME_EN))
                    GameDesktopShortcutToolStripMenuItem.Checked = true;
                else
                    GameDesktopShortcutToolStripMenuItem.Checked = false;

                if (StartMenuHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN), ConstStrings.C_GAMETITLE_NAME_EN))
                    GameStartmenuShortcutsToolStripMenuItem.Checked = true;
                else
                    GameStartmenuShortcutsToolStripMenuItem.Checked = false;



                if (StartMenuHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                    LauncherDesktopShortcutToolStripMenuItem.Checked = true;
                else
                    LauncherDesktopShortcutToolStripMenuItem.Checked = false;

                if (StartMenuHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Patch 2.22 Launcher"), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                    LauncherStartmenuShortcutToolStripMenuItem.Checked = true;
                else
                    LauncherStartmenuShortcutToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                await file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }

            IsLauncherCurrentlyWorking = false;

            if (!IsLauncherCurrentlyWorking)
            {
                CheckForUpdates();
            }
        }

        private void BFME1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsLauncherCurrentlyWorking)
            {
                MessageBox.Show(Strings.Warning_CantStopNow, Strings.Warning_CantStopNowTitle);
                e.Cancel = true;
            }

            if (Settings.Default.PatchVersionInstalled == 103 || Settings.Default.IsPatch106Installed || Settings.Default.IsPatch33Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
            }
            else
            {
                Settings.Default.SelectedOlderPatch = false;
            }

            Settings.Default.Save();
        }

        private void BFME1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                SysTray.Visible = true;
                SysTray.ShowBalloonTip(2000);
                _soundPlayerHelper.StopTheme();
            }
        }

        private void SysTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            SysTray.Visible = false;

            if (Settings.Default.PlayBackgroundMusic)
            {
                _soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }
        }

        private void LaunchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;

            Process _process = new();
            _process.StartInfo.FileName = Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_GAME_FILE);

            // Start game windowed
            if (Settings.Default.StartGameWindowed)
            {
                _process.StartInfo.Arguments = "-win";
            }

            _process.StartInfo.WorkingDirectory = Settings.Default.GameInstallPath;
            _process.Start();

            WindowState = FormWindowState.Minimized;
            Hide();

            SysTray.Visible = true;
            SysTray.ShowBalloonTip(2000);
            _soundPlayerHelper.StopTheme();

            _process.WaitForExitAsync();
            _process.Exited += GameisClosed;
        }

        private void CloseTheLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Wv2Patchnotes.Dispose();
            Application.Exit();
        }

        private void OpenSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppdataFolderPath() + "\\Save");
        }

        private void OpenMapDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppdataFolderPath() + "\\Maps");
        }

        private void OpenReplayDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppdataFolderPath() + "\\Replays");
        }

        private void OpenGameDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameInstallPath());
        }

        private void OpenLauncherDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.StartupPath);
        }

        private void OpenLauncherLogfileDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(Application.StartupPath, ConstStrings.C_LOGFOLDER_NAME));
        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreditsForm _about = new();

            DialogResult dr = _about.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _about.Close();
            }
        }

        private void MessagesFromTheTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessagesForm _message = new();

            DialogResult dr = _message.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _message.Close();
            }
        }

        private async void RepairGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;

            //await RepairFileHelper.RepairFeature();

            IsLauncherCurrentlyWorking = false;
        }

        private void LauncherSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LauncherOptionsForm _Launcheroptions = new();
            _Launcheroptions.ShowDialog();

            if (StartMenuHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_GAMETITLE_NAME_EN))
                GameDesktopShortcutToolStripMenuItem.Checked = true;
            else
                GameDesktopShortcutToolStripMenuItem.Checked = false;

            if (StartMenuHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN), ConstStrings.C_GAMETITLE_NAME_EN))
                GameStartmenuShortcutsToolStripMenuItem.Checked = true;
            else
                GameStartmenuShortcutsToolStripMenuItem.Checked = false;
        }

        private void GameSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameOptionsForm _Gameoptions = new();
            _Gameoptions.ShowDialog();
        }

        private void GameDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (StartMenuHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_GAMETITLE_NAME_EN))
                {
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_GAMETITLE_NAME_EN + ".lnk"));
                    GameDesktopShortcutToolStripMenuItem.Checked = false;
                }
                else
                {
                    StartMenuHelper.CreateShortcutToDesktop(Path.Combine(RegistryService.GameInstallPath(), ConstStrings.C_MAIN_GAME_FILE), ConstStrings.C_GAMETITLE_NAME_EN, Settings.Default.StartGameWindowed == true ? "-win" : "");
                    GameDesktopShortcutToolStripMenuItem.Checked = true;
                }
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }
        }

        private void GameStartmenuShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (StartMenuHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN), ConstStrings.C_GAMETITLE_NAME_EN))
                {
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN, ConstStrings.C_GAMETITLE_NAME_EN + ".lnk"));
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN, "Worldbuilder" + ".lnk"));

                    if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN)))
                        Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN));

                    GameStartmenuShortcutsToolStripMenuItem.Checked = false;
                }
                else
                {
                    if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN)))
                        Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN));

                    StartMenuHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(), ConstStrings.C_MAIN_GAME_FILE), ConstStrings.C_GAMETITLE_NAME_EN, Path.Combine("Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN), Settings.Default.StartGameWindowed == true ? "-win" : "");
                    StartMenuHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(), ConstStrings.C_WORLDBUILDER_FILE), "Worldbuilder", Path.Combine("Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN));
                    GameStartmenuShortcutsToolStripMenuItem.Checked = true;
                }
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }
        }

        private void LauncherDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (StartMenuHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                {
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME + ".lnk"));
                    LauncherDesktopShortcutToolStripMenuItem.Checked = false;
                }
                else
                {
                    StartMenuHelper.CreateShortcutToDesktop(Path.Combine(Application.StartupPath, "Restarter.exe"), ConstStrings.C_LAUNCHER_SHORTCUT_NAME, "--startLauncher");
                    LauncherDesktopShortcutToolStripMenuItem.Checked = true;
                }
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }
        }

        private void LauncherStartmenuShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (StartMenuHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Path.Combine("Programs", "Patch 2.22 Launcher")), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                {
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Path.Combine("Programs", "Patch 2.22 Launcher"), ConstStrings.C_LAUNCHER_SHORTCUT_NAME + ".lnk"));
                    LauncherStartmenuShortcutToolStripMenuItem.Checked = false;
                }
                else
                {
                    if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Path.Combine("Programs", "Patch 2.22 Launcher"))))
                        Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Path.Combine("Programs", "Patch 2.22 Launcher")));

                    StartMenuHelper.CreateShortcutToStartMenu(Path.Combine(Application.StartupPath, "Restarter.exe"), ConstStrings.C_LAUNCHER_SHORTCUT_NAME, Path.Combine("Programs", "Patch 2.22 Launcher"), "--startLauncher");

                    LauncherStartmenuShortcutToolStripMenuItem.Checked = true;
                }
            }

            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }
        }
    }
}