using Helper;
using Helper.UserControls;
using PatchLauncher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class WinFormsMainGUI : Form
    {
        int iconNumber = Settings.Default.BackgroundMusicIcon;
        readonly SoundPlayerHelper soundPlayerHelper = new();

        LanguageFiles? patchPackLanguages;
        LanguagePacks languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[Settings.Default.InstalledLanguageISOCode];
        readonly MainPacks mainPack = JSONDataListHelper._MainPackSettings;
        readonly PatchPacks patchPack = JSONDataListHelper._DictionaryPatchPacksSettings[Settings.Default.LatestPatchVersion];
        readonly PatchPacksBeta patchPacksBeta = JSONDataListHelper._PatchBetaSettings;

        readonly ChangelogPagePatch changelogPagePatch = new();
        readonly ChangelogPageLauncher changelogPageLauncher = new();

        readonly string displayNameFromRegistry = RegistryService.ReadRegKeyBFME1("displayName");

        static bool IsLauncherCurrentlyWorking = false;

        public WinFormsMainGUI()
        {
            InitializeComponent();

            SysTray.ContextMenuStrip = NotifyContextMenu;
            BtnInstall.Text = Strings.BtnInstall_TextLaunch;

            // label-Styles
            LblWorkerFileName.Font = FontHelper.GetFont(0, 16); ;
            LblWorkerFileName.ForeColor = Color.FromArgb(192, 145, 69);
            LblWorkerFileName.BackColor = Color.Transparent;
            LblWorkerFileName.Text = "";

            LblWorkerIOTask.Font = FontHelper.GetFont(0, 16); ;
            LblWorkerIOTask.ForeColor = Color.FromArgb(192, 145, 69);
            LblWorkerIOTask.BackColor = Color.Transparent;
            LblWorkerIOTask.Text = "";

            LabelLoadingPanel.Font = FontHelper.GetFont(0, 20);
            LabelLoadingPanel.ForeColor = Color.FromArgb(192, 145, 69);
            LabelLoadingPanel.BackColor = Color.Transparent;
            LabelLoadingPanel.BorderStyle = BorderStyle.None;
            LabelLoadingPanel.Text = Strings.Info_PleaseWait;

            LblModExplanation.Font = FontHelper.GetFont(0, 22);
            LblModExplanation.ForeColor = Color.FromArgb(192, 145, 69);
            LblModExplanation.BackColor = Color.Transparent;
            LblModExplanation.BorderStyle = BorderStyle.None;
            LblModExplanation.OutlineWidth = 4;

            PBarActualFile.ForeColor = Color.FromArgb(87, 24, 0);
            PBarActualFile.BackColor = Color.FromArgb(11, 14, 17);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnInstall.Font = FontHelper.GetFont(0, 16); ;
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);

            PanelPlaceholder.BackgroundImage = Helper.Properties.Resources.BFME1BorderRectangleModPanel;
            PanelPlaceholder.BackColor = Color.Transparent;

            //Tooltips
            ToolTip.SetToolTip(PiBThemeSwitcher, Strings.ToolTip_MusicSwitcher);

            PibHeader.Image = Helper.Properties.Resources.BFME1_Header;
            PiBYoutube.Image = Helper.Properties.Resources.youtube;
            PiBDiscord.Image = Helper.Properties.Resources.discord;
            PiBModDB.Image = Helper.Properties.Resources.moddb;
            PiBTwitch.Image = Helper.Properties.Resources.twitch;

            PibHeader.Image = Helper.Properties.Resources.BFME1_Header;
            PibLoadingBorder.Image = Helper.Properties.Resources.BFME1LoadingBorder;
            PibLoadingRing.Image = Helper.Properties.Resources.loadingRing;

            PiBVersion103.Image = Helper.Properties.Resources.BFME1PatchModBG103;

            if (Settings.Default.PlayBackgroundMusic)
                PibMute.Image = Helper.Properties.Resources.Unmute;
            else
                PibMute.Image = Helper.Properties.Resources.Mute;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Settings.Default.BackgroundMusicIcon == 0)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoDefault;
                BackgroundImage = Helper.Properties.Resources.BFME1BGDefault;
            }
            else if (Settings.Default.BackgroundMusicIcon == 1)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoGondor;
                BackgroundImage = Helper.Properties.Resources.BFME1BGGondor;
            }
            else if (Settings.Default.BackgroundMusicIcon == 2)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoRohan;
                BackgroundImage = Helper.Properties.Resources.BFME1BGRohan;
            }
            else if (Settings.Default.BackgroundMusicIcon == 3)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoIsengard;
                BackgroundImage = Helper.Properties.Resources.BFME1BGIsengard;
            }
            else if (Settings.Default.BackgroundMusicIcon == 4)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoMordor;
                BackgroundImage = Helper.Properties.Resources.BFME1BGMordor;
            }
        }

        private void WinFormsMainGUI_Load(object sender, EventArgs e)
        {
            try
            {
                if (Settings.Default.PlayBackgroundMusic)
                {
                    soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
                }

                PanelPlaceholder.Padding = new Padding(80, 60, 80, 60);

                if (!Settings.Default.UseBetaChannel)
                {
                    if (JSONDataListHelper._DictionaryPatchPacksSettings.ContainsKey(106))
                    {
                        Patch106Button patch106Button = new();
                        PanelPlaceholder.Controls.Add(patch106Button);
                        patch106Button.Tag = 106;
                        patch106Button.Click += PatchButton106Clicked;

                        if (Settings.Default.PatchVersionInstalled == 106)
                        {
                            patch106Button.SelectedIconVisible = true;
                        }
                    }

                    foreach (var version in JSONDataListHelper._DictionaryPatchPacksSettings.Where(x => x.Key is >= 22220 and <= 22250))
                    {
                        string patch222Version = version.Value.Version.ToString()[3..];
                        Patch222Buttons patch222Buttons = new()
                        {
                            LabelTextPatchVersion = "Version " + patch222Version,
                            Tag = version.Key
                        };

                        if (version.Value.Version == Settings.Default.PatchVersionInstalled)
                        {
                            patch222Buttons.SelectedIconVisible = true;
                        }

                        UpdatePanelButtonActiveState();

                        patch222Buttons.Click += PatchButton222Clicked;
                        PanelPlaceholder.Controls.Add(patch222Buttons);
                    }
                }

                if ((Settings.Default.GameInstallPath == "" && !Directory.Exists(RegistryService.ReadRegKeyBFME1("path"))) || RegistryService.ReadRegKeyBFME1("path") == "ValueNotFound" || !File.Exists(Path.Combine(RegistryService.ReadRegKeyBFME1("path"), ConstStrings.C_BFME1_MAIN_GAME_FILE)))
                {
                    Settings.Default.IsGameInstalled = false;
                    BtnInstall.Text = Strings.BtnInstall_TextInstall;

                    PanelPlaceholder.Visible = false;
                    LaunchGameToolStripMenuItem.Enabled = false;
                    OptionsToolStripMenuItem.Enabled = false;
                    RepairGameToolStripMenuItem.Enabled = false;
                    MenuItemLaunchGame.Enabled = false;
                    LblModExplanation.Visible = false;
                }
                else
                {
                    Settings.Default.IsGameInstalled = true;
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKeyBFME1("path");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage(AssemblyNameHelper.BFMELauncherGameName);
                }

                if (ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), displayNameFromRegistry))
                    GameDesktopShortcutToolStripMenuItem.Checked = true;
                else
                    GameDesktopShortcutToolStripMenuItem.Checked = false;

                if (ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", displayNameFromRegistry), displayNameFromRegistry))
                    GameStartmenuShortcutsToolStripMenuItem.Checked = true;
                else
                    GameStartmenuShortcutsToolStripMenuItem.Checked = false;



                if (ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                    LauncherDesktopShortcutToolStripMenuItem.Checked = true;
                else
                    LauncherDesktopShortcutToolStripMenuItem.Checked = false;

                if (ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Patch 2.22 Launcher"), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                    LauncherStartmenuShortcutToolStripMenuItem.Checked = true;
                else
                    LauncherStartmenuShortcutToolStripMenuItem.Checked = false;

                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex, "");
            }
        }

        private async void BFME1_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.LatestPatchVersion > Settings.Default.PatchVersionInstalled && Settings.Default.IsGameInstalled && !Settings.Default.SelectedOlderPatch && !Settings.Default.UseBetaChannel)
            {
                TurnPatchesAndModsViewOff();

                Settings.Default.OpenPatchChangelogPage = true;
                Settings.Default.GameInstallPath = RegistryService.ReadRegKeyBFME1("path");
                Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage(AssemblyNameHelper.BFMELauncherGameName);
                Settings.Default.PatchVersionInstalled = Settings.Default.LatestPatchVersion;
                Settings.Default.Save();

                await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5);

                if (patchPack.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                {
                    LanguageFiles patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                    await InstallUpdatRepairRoutine(patchPackLanguages!.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                }

                await TurnPatchesAndModsViewOn();
                UpdatePanelButtonActiveState();
            }
            else if (Settings.Default.LatestPatchVersion > Settings.Default.PatchVersionInstalled && Settings.Default.IsGameInstalled && Settings.Default.SelectedOlderPatch)
            {
                DialogResult _dialogResult = MessageBox.Show(Strings.Msg_RestartForUpdate_Text, Strings.Msg_Restart_Title, MessageBoxButtons.YesNo);
                if (_dialogResult == DialogResult.Yes)
                {
                    Settings.Default.UseBetaChannel = false;
                    Settings.Default.SelectedOlderPatch = false;
                    Settings.Default.Save();

                    Process _restarterProcess = new();
                    _restarterProcess.StartInfo.FileName = ConstStrings.C_RESTARTEREXE_FILENAME;
                    _restarterProcess.StartInfo.Arguments = "--restart --BFME1Launcher";
                    _restarterProcess.StartInfo.WorkingDirectory = Application.StartupPath;
                    _restarterProcess.StartInfo.UseShellExecute = true;
                    _restarterProcess.Start();
                    Application.ExitThread();
                    Application.Exit();
                }
            }
            else if (Settings.Default.UseBetaChannel)
            {
                TurnPatchesAndModsViewOff();

                LblModExplanation.Font = FontHelper.GetFont(0, 30);
                LblModExplanation.Location = new Point(130, 300);
                LblModExplanation.Text = Strings.Info_BetaActivated;
                LblModExplanation.BringToFront();

                if (patchPacksBeta.Version > Settings.Default.BetaChannelVersion)
                {
                    await InstallUpdatRepairRoutine(patchPacksBeta.FileName, patchPacksBeta.URLs, patchPacksBeta.MD5);
                    Settings.Default.BetaChannelVersion = patchPacksBeta.Version;
                    Settings.Default.Save();
                }
                else if (patchPacksBeta.Version < Settings.Default.BetaChannelVersion)
                {
                    Settings.Default.BetaChannelVersion = patchPacksBeta.Version;
                    Settings.Default.Save();
                }

                await TurnPatchesAndModsViewOn();
            }

            if (Settings.Default.OpenLauncherChangelogPageAfterUpdate)
            {
                Settings.Default.OpenLauncherChangelogPageAfterUpdate = false;
                Settings.Default.Save();
                ChangelogLauncherToolStripMenuItem.PerformClick();
            }

            if (Settings.Default.OpenPatchChangelogPage)
            {
                Settings.Default.OpenPatchChangelogPage = false;
                Settings.Default.Save();
                ChangelogPatchToolStripMenuItem.PerformClick();
            }
        }

        private async void PiBVersion103_Click(object sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();

            try
            {
                await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                Settings.Default.PatchVersionInstalled = 103;
                Settings.Default.SelectedOlderPatch = true;
                Settings.Default.Save();
                UpdatePanelButtonActiveState();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex.ToString());
            }

            await TurnPatchesAndModsViewOn();
        }

        private async void PatchButton106Clicked(object? sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();

            Patch106Button patch106Button = (Patch106Button)sender!;
            int version = Convert.ToInt32(patch106Button.Tag);
            PatchPacks patchPack106 = JSONDataListHelper._DictionaryPatchPacksSettings[version];

            if (Settings.Default.PatchVersionInstalled == version)
            {
                await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                Settings.Default.PatchVersionInstalled = 103;
                Settings.Default.Save();
                UpdatePanelButtonActiveState();
            }
            else
            {
                try
                {
                    await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                    await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                    await InstallUpdatRepairRoutine(patchPack106.FileName, patchPack106.URLs, patchPack106.MD5);

                    if (patchPack106.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        LanguageFiles patchPackLanguages = patchPack106.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                        await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                    }

                    Settings.Default.PatchVersionInstalled = version;
                    UpdatePanelButtonActiveState();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                }
            }

            Settings.Default.SelectedOlderPatch = true;
            Settings.Default.Save();
            await TurnPatchesAndModsViewOn();
        }

        private async void PatchButton222Clicked(object? sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();

            Patch222Buttons patch222Buttons = (Patch222Buttons)sender!;
            int version = Convert.ToInt32(patch222Buttons.Tag);
            PatchPacks patchPack222 = JSONDataListHelper._DictionaryPatchPacksSettings[version];
            LanguageFiles? patchPackLanguages = null;

            if (patchPack222.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
            {
                patchPackLanguages = patchPack222.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
            }

            if (Settings.Default.PatchVersionInstalled == version)
            {
                await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                Settings.Default.PatchVersionInstalled = 103;
                UpdatePanelButtonActiveState();
            }
            else
            {
                try
                {
                    await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                    await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                    await InstallUpdatRepairRoutine(patchPack222.FileName, patchPack222.URLs, patchPack222.MD5);

                    if (patchPack222.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode) && patchPackLanguages != null)
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages!.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                    }

                    Settings.Default.PatchVersionInstalled = version;
                    UpdatePanelButtonActiveState();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                }
            }

            if (Settings.Default.LatestPatchVersion > version || Settings.Default.PatchVersionInstalled == 103)
                Settings.Default.SelectedOlderPatch = true;

            Settings.Default.Save();
            await TurnPatchesAndModsViewOn();
        }

        private async void BtnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                if (Settings.Default.IsGameInstalled == false)
                {
                    InstallPathDialog installPathDialog = new();

                    DialogResult dialogResult = installPathDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        TurnPatchesAndModsViewOff();

                        Task<bool> taskPrepareInstallFolder = PrepareInstallFolder();
                        taskPrepareInstallFolder.Wait();

                        if (!taskPrepareInstallFolder.Result)
                        {
                            throw new Exception("Question for cleaning target install folder answered with no");
                        }

                        languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[Settings.Default.InstalledLanguageISOCode];

                        RegistryService.WriteRegKeysInstallationBFME1(Settings.Default.GameInstallPath, languagePackSettings.RegistrySelectedLocale, languagePackSettings.RegistrySelectedLanguageName, languagePackSettings.RegistrySelectedLanguage);

                        await InstallUpdatRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);
                        await InstallUpdatRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);
                        await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5);

                        if (patchPack.LanguageFiles.ContainsKey(languagePackSettings.RegistrySelectedLocale))
                        {
                            patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                            await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                        }

                        patchPack.Version = Settings.Default.PatchVersionInstalled;
                        Settings.Default.IsGameInstalled = true;

                        if (Settings.Default.CreateDesktopShortcut)
                        {
                            GameDesktopShortcutToolStripMenuItem.PerformClick();
                        }

                        if (Settings.Default.CreateStartMenuShortcut)
                        {
                            GameStartmenuShortcutsToolStripMenuItem.PerformClick();
                        }

                        taskPrepareInstallFolder.Dispose();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    LaunchGameToolStripMenuItem.PerformClick();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex.ToString());

                Settings.Default.IsGameInstalled = false;
                BtnInstall.Text = Strings.BtnInstall_TextInstall;
                Settings.Default.Save();
            }

            OptionsToolStripMenuItem.Enabled = true;
            await TurnPatchesAndModsViewOn();

            Settings.Default.Save();

            Update();
        }

        private void BtnInstall_MouseLeave(object sender, EventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnInstall_MouseEnter(object sender, EventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_HOVER;
            BtnInstall.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnInstall_MouseDown(object sender, MouseEventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_CLICK;
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
                soundPlayerHelper.StopTheme();
                Settings.Default.Save();
            }
            else
            {
                PibMute.Image = Helper.Properties.Resources.Unmute;
                Settings.Default.PlayBackgroundMusic = true;
                Settings.Default.Save();
                soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
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
                        BackgroundImage = Helper.Properties.Resources.BFME1BGDefault;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_DEFAULT);
                        }

                        break;
                    }
                case 1:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_GONDOR;
                        Settings.Default.BackgroundMusicIcon = 1;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoGondor;
                        BackgroundImage = Helper.Properties.Resources.BFME1BGGondor;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_GONDOR);
                        }
                        break;
                    }
                case 2:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_ROHAN;
                        Settings.Default.BackgroundMusicIcon = 2;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoRohan;
                        BackgroundImage = Helper.Properties.Resources.BFME1BGRohan;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_ROHAN);
                        }
                        break;
                    }
                case 3:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_ISENGARD;
                        Settings.Default.BackgroundMusicIcon = 3;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoIsengard;
                        BackgroundImage = Helper.Properties.Resources.BFME1BGIsengard;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_ISENGARD);
                        }
                        break;
                    }
                case 4:
                    {
                        Settings.Default.BackgroundMusicFile = ConstStrings.C_THEMESOUND_MORDOR;
                        Settings.Default.BackgroundMusicIcon = 4;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Helper.Properties.Resources.icoMordor;
                        BackgroundImage = Helper.Properties.Resources.BFME1BGMordor;

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_MORDOR);
                        }
                        break;
                    }
            }
        }

        private async Task InstallUpdatRepairRoutine(string ZIPFileName, List<string> DownloadUrls, string CorrectMD5HashValue)
        {
            try
            {
                PBarActualFile.Value = 0;
                PBarActualFile.Maximum = 100;

                string fullPathToZIPFile = Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME1, ZIPFileName);

                var progressHandlerDownload = new Progress<ProgressHelper>(progress =>
                {
                    PBarActualFile.Value = (int)progress.PercentageValue;
                    LblWorkerFileName.Text = Path.GetFileNameWithoutExtension(ZIPFileName);
                    LblWorkerIOTask.Text = string.Concat(progress.ProgressedDownloadSizeInBytes / 1024000, " / ", progress.TotalDownloadSizeInBytes / 1024000, " MB @ ", Math.Round(progress.DownloadSpeedSizeInBytes / 1024000), " MB/s");
                });

                var progressHandlerExtraction = new Progress<ProgressHelper>(progress =>
                {
                    PBarActualFile.Value = progress.CurrentlyExtractedFileCount;
                    PBarActualFile.Maximum = progress.TotalArchiveFileCount;
                    LblWorkerFileName.Text = progress.CurrentFileName;
                    LblWorkerIOTask.Text = string.Concat(progress.CurrentlyExtractedFileCount, " / ", progress.TotalArchiveFileCount);
                });


                BtnInstall.Text = Strings.BtnInstall_TextLaunch;
                GameFileTools gameFileTools = new();
                await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME1), ZIPFileName, DownloadUrls, 0, progressHandlerDownload, AssemblyNameHelper.BFMELauncherGameName);
                LblWorkerFileName.Text = "";
                LblWorkerIOTask.Text = "";
                Update();
                string calculatedMD5Value = MD5Tools.CalculateMD5(fullPathToZIPFile);

                if (calculatedMD5Value == CorrectMD5HashValue)
                {
                    await gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME1), ZIPFileName, Settings.Default.GameInstallPath, progressHandlerExtraction);
                }
                else
                {
                    LogHelper.LoggerBFME1GUI.Error(string.Format("MD5 HashSum check failed. Should be: {0} was: {1}", CorrectMD5HashValue, calculatedMD5Value));
                    LogHelper.LoggerBFME1GUI.Information(string.Format("Deleting file > {0} < and retry Download...", ZIPFileName));
                    File.Delete(fullPathToZIPFile);
                    await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME1), ZIPFileName, DownloadUrls, 1, progressHandlerDownload, AssemblyNameHelper.BFMELauncherGameName);
                    LogHelper.LoggerBFME1GUI.Information(string.Format("Now trying to extract > {0} <", ZIPFileName));
                    await gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME1), ZIPFileName, Settings.Default.GameInstallPath, progressHandlerExtraction);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex.ToString());
            }
        }

        private Task<bool> PrepareInstallFolder(bool AskForFolderClearance = true)
        {
            try
            {
                if (!Directory.Exists(Settings.Default.GameInstallPath))
                {
                    Directory.CreateDirectory(Settings.Default.GameInstallPath);
                }

                if (Directory.Exists(Settings.Default.GameInstallPath) && Directory.GetFileSystemEntries(Settings.Default.GameInstallPath).Length != 0 && AskForFolderClearance)
                {
                    DialogResult dialogResult = MessageBox.Show(Strings.Msg_InstallFolderNotEmpty_Text, Strings.Msg_InstallFolderNotEmpty_Title, MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        Directory.Delete(Settings.Default.GameInstallPath, true);
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        TurnPatchesAndModsViewOff();
                        return Task.FromResult(false);
                    }
                }
                else if (!AskForFolderClearance)
                {
                    Directory.Delete(Settings.Default.GameInstallPath, true);
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                return Task.FromResult(false);
            }
        }

        private void Tooltip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Font tooltipFont = FontHelper.GetFont(0, 16); ;
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, tooltipFont, Brushes.SandyBrown, new PointF(2, 2));
        }

        private void TooltipPopup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = TextRenderer.MeasureText(ToolTip.GetToolTip(e.AssociatedControl), FontHelper.GetFont(0, 16));
        }

        private void BFME1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsLauncherCurrentlyWorking)
            {
                MessageBox.Show(Strings.Warning_CantStopNow, Strings.Warning_CantStopNowTitle);
                e.Cancel = true;
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
                soundPlayerHelper.StopTheme();
            }
        }

        private void SysTray_MouseDoubleClick(object? sender, MouseEventArgs? e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            SysTray.Visible = false;

            if (Settings.Default.PlayBackgroundMusic)
            {
                soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }
        }

        private async void LaunchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process processLaunchGame = new();
                processLaunchGame.StartInfo.FileName = Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_BFME1_MAIN_GAME_FILE);

                // Start game windowed
                if (Settings.Default.StartGameWindowed)
                {
                    processLaunchGame.StartInfo.Arguments = "-win";
                }

                processLaunchGame.StartInfo.WorkingDirectory = Settings.Default.GameInstallPath;
                WindowState = FormWindowState.Minimized;
                processLaunchGame.Start();
                await processLaunchGame.WaitForExitAsync();
                await TurnPatchesAndModsViewOn();
                processLaunchGame.Dispose();
                SysTray_MouseDoubleClick(null, null);
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());
                DialogResult dialogResult = MessageBox.Show(Strings.Msg_ErrorStartingGame_Text, Strings.Msg_ErrorStartingGame_Title, MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    Settings.Default.PatchVersionInstalled = patchPack.Version;
                    Settings.Default.Save();

                    RepairGameToolStripMenuItem.Enabled = true;
                    RepairGameToolStripMenuItem.PerformClick();

                    UpdatePanelButtonActiveState();
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    await TurnPatchesAndModsViewOn();
                }
            }
        }

        private void CloseTheLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppdataFolderPath(AssemblyNameHelper.BFMELauncherGameName) + "\\Save");
        }

        private void OpenMapDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppdataFolderPath(AssemblyNameHelper.BFMELauncherGameName) + "\\Maps");
        }

        private void OpenReplayDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppdataFolderPath(AssemblyNameHelper.BFMELauncherGameName) + "\\Replays");
        }

        private void OpenGameDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName));
        }

        private void OpenLauncherDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.StartupPath);
        }

        private void OpenLauncherLogfileDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(Application.StartupPath, ConstStrings.C_LOGFOLDER_NAME));
        }

        private void BFME2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process _restarterProcess = new();
            _restarterProcess.StartInfo.FileName = ConstStrings.C_RESTARTEREXE_FILENAME;
            _restarterProcess.StartInfo.Arguments = "--restart --BFME2Launcher";
            _restarterProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            _restarterProcess.StartInfo.UseShellExecute = true;
            _restarterProcess.Start();
            Application.Exit();
        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreditsForm creditsForm = new();

            DialogResult dialogResult = creditsForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                creditsForm.Close();
            }
        }

        private void ChangelogLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = changelogPageLauncher.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                changelogPageLauncher.Close();
            }
        }

        private void ChangelogPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = changelogPagePatch.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                changelogPagePatch.Close();
            }
        }

        private async void GameSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameOptionsForm _Gameoptions = new();
            _Gameoptions.ShowDialog();
            _Gameoptions.Dispose();

            if (ChangedGameLanguage.UserChangedGameLanguageInSettings)
            {
                ChangedGameLanguage.UserChangedGameLanguageInSettings = false;
                TurnPatchesAndModsViewOff();

                Task<bool> taskPrepareInstallFolder = PrepareInstallFolder(false);
                taskPrepareInstallFolder.Wait();

                languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[Settings.Default.InstalledLanguageISOCode];

                RegistryService.WriteRegKeysInstallationBFME1(Settings.Default.GameInstallPath, languagePackSettings.RegistrySelectedLocale, languagePackSettings.RegistrySelectedLanguageName, languagePackSettings.RegistrySelectedLanguage);

                await InstallUpdatRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);
                await InstallUpdatRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);
                await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5);

                if (patchPack.LanguageFiles.ContainsKey(languagePackSettings.RegistrySelectedLocale))
                {
                    patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                    await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                }

                patchPack.Version = Settings.Default.PatchVersionInstalled;
                Settings.Default.Save();

                if (Settings.Default.CreateDesktopShortcut && !ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                {
                    GameDesktopShortcutToolStripMenuItem.PerformClick();
                }

                if (Settings.Default.CreateStartMenuShortcut && !ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", displayNameFromRegistry), displayNameFromRegistry))
                {
                    GameStartmenuShortcutsToolStripMenuItem.PerformClick();
                }

                taskPrepareInstallFolder.Dispose();

                await TurnPatchesAndModsViewOn();
            }
        }

        private void GameDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), displayNameFromRegistry))
            {
                ShortCutHelper.DeleteGameShortcutFromDesktop(displayNameFromRegistry);
                GameDesktopShortcutToolStripMenuItem.Checked = false;
            }
            else
            {
                ShortCutHelper.CreateShortcutToDesktop(Path.Combine(RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName),
                    ConstStrings.C_BFME1_MAIN_GAME_FILE),
                    displayNameFromRegistry,
                    Settings.Default.StartGameWindowed == true ? "-win" : "");
                GameDesktopShortcutToolStripMenuItem.Checked = true;
            }
        }

        private void GameStartmenuShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", displayNameFromRegistry), displayNameFromRegistry))
            {
                ShortCutHelper.DeleteGameShortcutsFromStartMenu(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", displayNameFromRegistry));
                GameStartmenuShortcutsToolStripMenuItem.Checked = false;
            }
            else
            {
                ShortCutHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName), ConstStrings.C_BFME1_MAIN_GAME_FILE), displayNameFromRegistry,
                    Path.Combine("Programs", "Electronic Arts",
                    displayNameFromRegistry),
                    Settings.Default.StartGameWindowed == true ? "-win" : "");

                ShortCutHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName), ConstStrings.C_WORLDBUILDER_FILE), "Worldbuilder",
                    Path.Combine("Programs",
                    "Electronic Arts",
                    displayNameFromRegistry));

                GameStartmenuShortcutsToolStripMenuItem.Checked = true;
            }
        }

        private void LauncherDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
            {
                ShortCutHelper.DeleteLauncherShortcutFromDesktop();
                LauncherDesktopShortcutToolStripMenuItem.Checked = false;
            }
            else
            {
                ShortCutHelper.CreateShortcutToDesktop(Path.Combine(Application.StartupPath, "Restarter.exe"), ConstStrings.C_LAUNCHER_SHORTCUT_NAME, "--startLauncher");
                LauncherDesktopShortcutToolStripMenuItem.Checked = true;
            }
        }

        private void LauncherStartmenuShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Patch 2.22 Launcher"),
                ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
            {
                ShortCutHelper.DeleteLauncherShortcutsFromStartMenu();
                GameStartmenuShortcutsToolStripMenuItem.Checked = false;
            }
            else
            {
                ShortCutHelper.CreateShortcutToStartMenu(Path.Combine(Application.StartupPath, "Restarter.exe"),
                    ConstStrings.C_LAUNCHER_SHORTCUT_NAME,
                    Path.Combine("Programs", "Patch 2.22 Launcher"),
                    "--startLauncher");

                LauncherStartmenuShortcutToolStripMenuItem.Checked = true;
            }
        }

        private async void RepairGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();

            LogHelper.LoggerRepairFile.Information("Started Repairing...");
            await RepairFileHelper.RepairFeature(AssemblyNameHelper.BFMELauncherGameName);

            LogHelper.LoggerRepairFile.Information("Downloading and/or extracting MainGame-Files if needed...");
            await InstallUpdatRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);

            LogHelper.LoggerRepairFile.Information("Downloading and/or extracting Language-Files if needed...");
            await InstallUpdatRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);

            LogHelper.LoggerRepairFile.Information(string.Format("Downloading and/or extracting Latest Patch 2.22 Version \"{0}\" ...", patchPack.Version));
            await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5);

            if (patchPack.LanguageFiles.ContainsKey(languagePackSettings.RegistrySelectedLocale))
            {
                patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                LogHelper.LoggerRepairFile.Information(string.Format("Downloading and/or extracting Language-Files for Patch 2.22 Version \"{0}\" in language \"{1}\" ...", patchPack.Version, patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode]));
                await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
            }

            await TurnPatchesAndModsViewOn();
        }

        private void MenuItemLaunchGame_Click(object sender, EventArgs e)
        {
            LaunchGameToolStripMenuItem.PerformClick();
        }

        private void UpdatePanelButtonActiveState()
        {
            foreach (Control childControl in PanelPlaceholder.Controls)
            {
                if (childControl is Patch106Button patch106Buttons)
                {
                    patch106Buttons.SelectedIconVisible = (int)patch106Buttons.Tag == Settings.Default.PatchVersionInstalled;
                }
                else if (childControl is Patch222Buttons patch222Buttons)
                {
                    patch222Buttons.SelectedIconVisible = (int)patch222Buttons.Tag == Settings.Default.PatchVersionInstalled;
                }
            }
        }
        private async Task TurnPatchesAndModsViewOn()
        {
            Update();
            await Task.Delay(1000);
            IsLauncherCurrentlyWorking = false;

            PibLoadingRing.Visible = false;
            PibLoadingBorder.Visible = false;
            LabelLoadingPanel.Visible = false;
            LblModExplanation.Visible = true;

            PBarActualFile.Visible = false;
            LblWorkerFileName.Visible = false;
            LblWorkerIOTask.Visible = false;

            BtnInstall.Enabled = true;

            LaunchGameToolStripMenuItem.Enabled = true;
            RepairGameToolStripMenuItem.Enabled = true;
            SelectGameToolStripMenuItem.Enabled = true;
            MenuItemLaunchGame.Enabled = true;

            if (!Settings.Default.UseBetaChannel)
                PanelPlaceholder.Visible = true;

            Update();
        }

        private void TurnPatchesAndModsViewOff()
        {
            PanelPlaceholder.Visible = false;
            Update();

            IsLauncherCurrentlyWorking = true;

            PibLoadingRing.Visible = true;
            PibLoadingBorder.Visible = true;
            LabelLoadingPanel.Visible = true;
            LblModExplanation.Visible = false;

            PBarActualFile.Visible = true;
            LblWorkerFileName.Visible = true;
            LblWorkerIOTask.Visible = true;

            BtnInstall.Enabled = false;

            LaunchGameToolStripMenuItem.Enabled = false;
            RepairGameToolStripMenuItem.Enabled = false;
            SelectGameToolStripMenuItem.Enabled = false;
            MenuItemLaunchGame.Enabled = false;

            Update();
        }
    }
}