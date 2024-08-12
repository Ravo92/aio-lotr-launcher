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
        LanguagePacks languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[GameFileTools.CheckIfJSONLanguageExists(Settings.Default.InstalledLanguageISOCode, AssemblyNameHelper.BFMELauncherGameName)];
        readonly MainPacks mainPack = JSONDataListHelper._MainPackSettings;
        readonly PatchPacks patchPack = JSONDataListHelper._DictionaryPatchPacksSettings[JSONDataListHelper._DictionaryPatchPacksSettings.Keys.Max()];
        readonly PatchPacksBeta patchPacksBeta = JSONDataListHelper._PatchBetaSettings;

        readonly ChangelogPagePatch changelogPagePatch = new();
        readonly ChangelogPageLauncher changelogPageLauncher = new();

        readonly string displayName = "The Battle for Middle-earth";

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

            PBarActualFile.ForeColor = Color.FromArgb(100, 53, 5);
            PBarActualFile.BackColor = Color.FromArgb(192, 145, 69);
            PBarActualFile.TextColor = Color.FromArgb(80, 50, 20);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnInstall.Font = FontHelper.GetFont(0, 16); ;
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);

            BtnPlayOnline.FlatAppearance.BorderSize = 0;
            BtnPlayOnline.FlatStyle = FlatStyle.Flat;
            BtnPlayOnline.BackColor = Color.Transparent;
            BtnPlayOnline.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnPlayOnline.Font = FontHelper.GetFont(0, 16); ;
            BtnPlayOnline.ForeColor = Color.FromArgb(192, 145, 69);

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
                    if (JSONDataListHelper._DictionaryPatchPacksSettings.ContainsKey(0))
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

                    if (JSONDataListHelper._DictionaryPatchPacksSettings.ContainsKey(1))
                    {
                        Patch109Button patch109Button = new();
                        PanelPlaceholder.Controls.Add(patch109Button);
                        patch109Button.Tag = 109;
                        patch109Button.Click += PatchButton109Clicked;

                        if (Settings.Default.PatchVersionInstalled == 109)
                        {
                            patch109Button.SelectedIconVisible = true;
                        }
                    }

                    foreach (var version in JSONDataListHelper._DictionaryPatchPacksSettings.Where(x => x.Key is > 2))
                    {
                        if (version.Value.Visible)
                        {
                            string patch222Version = "";
                            Patch222Buttons patch222Buttons = new();

                            int fullVersionAsInteger = version.Value.MinorVersion * 10 + version.Value.Revision;
                            string fullVersionAsString = fullVersionAsInteger.ToString();

                            if (version.Value.Revision > 0)
                            {
                                patch222Version = fullVersionAsString[0..].Insert(2, ".");
                                patch222Buttons.LabelTextPatchVersion = "Version " + patch222Version;
                                patch222Buttons.Tag = fullVersionAsInteger;
                            }
                            else
                            {
                                patch222Version = fullVersionAsString[0..];
                                patch222Buttons.LabelTextPatchVersion = "Version " + version.Value.MinorVersion.ToString();
                                patch222Buttons.Tag = fullVersionAsInteger;
                            }

                            if (fullVersionAsInteger == Settings.Default.PatchVersionInstalled)
                            {
                                patch222Buttons.SelectedIconVisible = true;
                            }

                            UpdatePanelButtonActiveState();

                            patch222Buttons.Click += PatchButton222Clicked;
                            PanelPlaceholder.Controls.Add(patch222Buttons);
                        }
                    }
                }

                BtnPlayOnline.Enabled = false;

                if ((Settings.Default.GameInstallPath == "" && !Directory.Exists(RegistryService.ReadRegKeyBFME1("InstallPath"))) || RegistryService.ReadRegKeyBFME1("InstallPath") == "ValueNotFound" || !File.Exists(Path.Combine(RegistryService.ReadRegKeyBFME1("InstallPath"), ConstStrings.C_BFME1_MAIN_GAME_FILE)))
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
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKeyBFME1("InstallPath");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage(AssemblyNameHelper.BFMELauncherGameName);

                    if (Settings.Default.PatchVersionInstalled == patchPack.MinorVersion * 10 + patchPack.Revision || Settings.Default.PatchVersionInstalled == 106 || Settings.Default.PatchVersionInstalled == 109)
                        BtnPlayOnline.Enabled = true;
                }

                if (ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), displayName))
                    GameDesktopShortcutToolStripMenuItem.Checked = true;
                else
                    GameDesktopShortcutToolStripMenuItem.Checked = false;

                if (ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", displayName), displayName))
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
            if (Settings.Default.LatestPatchVersion != Settings.Default.PatchVersionInstalled && Settings.Default.IsGameInstalled && !Settings.Default.SelectedOlderPatch && !Settings.Default.UseBetaChannel)
            {
                TurnPatchesAndModsViewOff();

                Settings.Default.OpenPatchChangelogPage = true;
                Settings.Default.GameInstallPath = RegistryService.ReadRegKeyBFME1("InstallPath");
                Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage(AssemblyNameHelper.BFMELauncherGameName);
                Settings.Default.PatchVersionInstalled = Settings.Default.LatestPatchVersion;
                Settings.Default.Save();

                await InstallUpdateRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5);

                if (patchPack.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                {
                    LanguageFiles patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                    await InstallUpdateRepairRoutine(patchPackLanguages!.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                }

                TurnPatchesAndModsViewOn();
                UpdatePanelButtonActiveState();
            }
            else if (Settings.Default.LatestPatchVersion != Settings.Default.PatchVersionInstalled && Settings.Default.IsGameInstalled && Settings.Default.SelectedOlderPatch)
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
                    await InstallUpdateRepairRoutine(patchPacksBeta.FileName, patchPacksBeta.URLs, patchPacksBeta.MD5);
                    Settings.Default.BetaChannelVersion = patchPacksBeta.Version;
                    Settings.Default.Save();
                }
                else if (patchPacksBeta.Version < Settings.Default.BetaChannelVersion)
                {
                    Settings.Default.BetaChannelVersion = patchPacksBeta.Version;
                    Settings.Default.Save();
                }

                TurnPatchesAndModsViewOn();

                BtnPlayOnline.Visible = false;
                Update();
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
                await PatchModDetectionHelper.DeletePatch109ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
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

            TurnPatchesAndModsViewOn();
        }

        private async void PatchButton106Clicked(object? sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();
            PatchPacks patchPack106 = JSONDataListHelper._DictionaryPatchPacksSettings[0];

            if (Settings.Default.PatchVersionInstalled == 106)
            {
                await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                await PatchModDetectionHelper.DeletePatch109ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
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
                    await PatchModDetectionHelper.DeletePatch109ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                    await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                    await InstallUpdateRepairRoutine(patchPack106.FileName, patchPack106.URLs, patchPack106.MD5);

                    if (patchPack106.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        LanguageFiles patchPackLanguages = patchPack106.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                        await InstallUpdateRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                    }

                    Settings.Default.PatchVersionInstalled = 106;
                    UpdatePanelButtonActiveState();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                }
            }

            Settings.Default.SelectedOlderPatch = true;
            Settings.Default.Save();
            TurnPatchesAndModsViewOn();
        }

        private async void PatchButton109Clicked(object? sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();
            PatchPacks patchPack109 = JSONDataListHelper._DictionaryPatchPacksSettings[1];

            if (Settings.Default.PatchVersionInstalled == 109)
            {
                await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                await PatchModDetectionHelper.DeletePatch109ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
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
                    await PatchModDetectionHelper.DeletePatch109ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                    await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                    await InstallUpdateRepairRoutine(patchPack109.FileName, patchPack109.URLs, patchPack109.MD5);

                    Settings.Default.PatchVersionInstalled = 109;
                    UpdatePanelButtonActiveState();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                }
            }

            Settings.Default.SelectedOlderPatch = true;
            Settings.Default.Save();
            TurnPatchesAndModsViewOn();
        }

        private async void PatchButton222Clicked(object? sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();

            Patch222Buttons patch222Buttons = (Patch222Buttons)sender!;
            int patch222Version = Convert.ToInt32(patch222Buttons.Tag);
            PatchPacks patchPack222 = JSONDataListHelper._DictionaryPatchPacksSettings[patch222Version];
            LanguageFiles? patchPackLanguages = null;

            if (patchPack222.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
            {
                patchPackLanguages = patchPack222.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
            }

            if (Settings.Default.PatchVersionInstalled == patchPack222.MinorVersion * 10 + patchPack222.Revision)
            {
                await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                await PatchModDetectionHelper.DeletePatch109ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                Settings.Default.PatchVersionInstalled = 103;
                UpdatePanelButtonActiveState();
            }
            else
            {
                try
                {
                    await PatchModDetectionHelper.DeletePatch106ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                    await PatchModDetectionHelper.DeletePatch109ForBFME1(AssemblyNameHelper.BFMELauncherGameName);
                    await PatchModDetectionHelper.DeletePatch222FilesForBFME1(AssemblyNameHelper.BFMELauncherGameName);

                    await InstallUpdateRepairRoutine(patchPack222.FileName, patchPack222.URLs, patchPack222.MD5);

                    if (patchPack222.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode) && patchPackLanguages != null)
                    {
                        await InstallUpdateRepairRoutine(patchPackLanguages!.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                    }

                    Settings.Default.PatchVersionInstalled = patchPack222.MinorVersion * 10 + patchPack222.Revision;
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                }
            }

            if (Settings.Default.LatestPatchVersion > patch222Version || Settings.Default.PatchVersionInstalled == 103)
                Settings.Default.SelectedOlderPatch = true;

            Settings.Default.Save();
            TurnPatchesAndModsViewOn();
            UpdatePanelButtonActiveState();
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

                        RegistryService.WriteRegKeysInstallationBFME1(Settings.Default.GameInstallPath, languagePackSettings.RegistrySelectedLanguage);

                        await InstallUpdateRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);
                        await InstallUpdateRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);
                        await InstallUpdateRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5);

                        if (patchPack.LanguageFiles.ContainsKey(languagePackSettings.RegistrySelectedLanguage))
                        {
                            patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                            await InstallUpdateRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                        }

                        Settings.Default.PatchVersionInstalled = patchPack.MinorVersion * 10 + patchPack.Revision;
                        Settings.Default.IsGameInstalled = true;
                        Settings.Default.Save();

                        if (Settings.Default.CreateDesktopShortcut)
                        {
                            GameDesktopShortcutToolStripMenuItem.PerformClick();
                        }

                        if (Settings.Default.CreateStartMenuShortcut)
                        {
                            GameStartmenuShortcutsToolStripMenuItem.PerformClick();
                        }

                        taskPrepareInstallFolder.Dispose();
                        UpdatePanelButtonActiveState();
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
            TurnPatchesAndModsViewOn();
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

        private void BtnPlayOnline_Click(object sender, EventArgs e)
        {
            OnlineMode onlineMode = new();
            DialogResult dialogResult = onlineMode.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                onlineMode.Dispose();
            }
        }

        private void BtnPlayOnline_MouseLeave(object sender, EventArgs e)
        {
            BtnPlayOnline.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnPlayOnline.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnPlayOnline_MouseEnter(object sender, EventArgs e)
        {
            BtnPlayOnline.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_HOVER;
            BtnPlayOnline.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnPlayOnline_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPlayOnline.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_CLICK;
            BtnPlayOnline.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void PiBYouTube_Click(object sender, EventArgs e)
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

        private async Task InstallUpdateRepairRoutine(string ZIPFileName, List<string> DownloadURLs, string CorrectMD5HashValue)
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
                await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME1), ZIPFileName, DownloadURLs, 0, progressHandlerDownload);
                LblWorkerFileName.Text = "";
                LblWorkerIOTask.Text = "";
                Update();
                string calculatedMD5Value = await MD5Tools.CalculateMD5Async(fullPathToZIPFile);

                if (calculatedMD5Value == CorrectMD5HashValue)
                {
                    await gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME1), ZIPFileName, Settings.Default.GameInstallPath, progressHandlerExtraction);
                }
                else
                {
                    LogHelper.LoggerBFME1GUI.Error(string.Format("MD5 HashSum check failed. Should be: {0} was: {1}", CorrectMD5HashValue, calculatedMD5Value));
                    LogHelper.LoggerBFME1GUI.Information(string.Format("Deleting file > {0} < and retry Download...", ZIPFileName));
                    File.Delete(fullPathToZIPFile);
                    await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME1), ZIPFileName, DownloadURLs, 1, progressHandlerDownload);
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
                TurnPatchesAndModsViewOn();
                processLaunchGame.Dispose();
                SysTray_MouseDoubleClick(null, null);
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                DialogResult dialogResult = MessageBox.Show(Strings.Msg_ErrorStartingGame_Text, Strings.Msg_ErrorStartingGame_Title, MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    Settings.Default.PatchVersionInstalled = patchPack.MinorVersion * 10 + patchPack.Revision;
                    Settings.Default.Save();

                    RepairGameToolStripMenuItem.Enabled = true;
                    RepairGameToolStripMenuItem.PerformClick();

                    UpdatePanelButtonActiveState();
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    TurnPatchesAndModsViewOn();
                }
            }
        }

        private void CloseTheLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppDataFolderPath(AssemblyNameHelper.BFMELauncherGameName) + "\\Save");
        }

        private void OpenMapDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppDataFolderPath(AssemblyNameHelper.BFMELauncherGameName) + "\\Maps");
        }

        private void OpenReplayDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", RegistryService.GameAppDataFolderPath(AssemblyNameHelper.BFMELauncherGameName) + "\\Replays");
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
            OptionsForm _Gameoptions = new();
            _Gameoptions.ShowDialog();
            _Gameoptions.Dispose();

            if (ChangedGameLanguage.UserChangedGameLanguageInSettings)
            {
                ChangedGameLanguage.UserChangedGameLanguageInSettings = false;
                TurnPatchesAndModsViewOff();

                Task<bool> taskPrepareInstallFolder = PrepareInstallFolder(false);
                taskPrepareInstallFolder.Wait();

                languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[Settings.Default.InstalledLanguageISOCode];

                RegistryService.WriteRegKeysInstallationBFME1(Settings.Default.GameInstallPath, languagePackSettings.RegistrySelectedLanguage);

                await InstallUpdateRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);
                await InstallUpdateRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);
                await InstallUpdateRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5);

                if (patchPack.LanguageFiles.ContainsKey(languagePackSettings.RegistrySelectedLanguage))
                {
                    patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                    await InstallUpdateRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
                }

                if (Settings.Default.CreateDesktopShortcut && !ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                {
                    GameDesktopShortcutToolStripMenuItem.PerformClick();
                }

                if (Settings.Default.CreateStartMenuShortcut && !ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", displayName), displayName))
                {
                    GameStartmenuShortcutsToolStripMenuItem.PerformClick();
                }

                taskPrepareInstallFolder.Dispose();

                TurnPatchesAndModsViewOn();
            }
        }

        private void GameDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), displayName))
            {
                ShortCutHelper.DeleteGameShortcutFromDesktop(displayName);
                GameDesktopShortcutToolStripMenuItem.Checked = false;
            }
            else
            {
                ShortCutHelper.CreateShortcutToDesktop(Path.Combine(RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName),
                    ConstStrings.C_BFME1_MAIN_GAME_FILE),
                    displayName,
                    Settings.Default.StartGameWindowed == true ? "-win" : "");
                GameDesktopShortcutToolStripMenuItem.Checked = true;
            }
        }

        private void GameStartmenuShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", displayName), displayName))
            {
                ShortCutHelper.DeleteGameShortcutsFromStartMenu(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", displayName));
                GameStartmenuShortcutsToolStripMenuItem.Checked = false;
            }
            else
            {
                ShortCutHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName), ConstStrings.C_BFME1_MAIN_GAME_FILE), displayName,
                    Path.Combine("Programs", "Electronic Arts",
                    displayName),
                    Settings.Default.StartGameWindowed == true ? "-win" : "");

                ShortCutHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName), ConstStrings.C_WORLDBUILDER_FILE), "Worldbuilder",
                    Path.Combine("Programs",
                    "Electronic Arts",
                    displayName));

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
                ShortCutHelper.CreateShortcutToDesktop(Path.Combine(Application.StartupPath, "Restarter.exe"), ConstStrings.C_LAUNCHER_SHORTCUT_NAME);
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

            LogHelper.LoggerRepairFile.Information("Downloading and/or extracting main game files if needed...");
            await InstallUpdateRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);

            LogHelper.LoggerRepairFile.Information("Downloading and/or extracting language files if needed...");
            await InstallUpdateRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);

            LogHelper.LoggerRepairFile.Information(string.Format("Downloading and/or extracting Latest Patch 2.22 Version \"{0}\" ...", patchPack.MinorVersion));
            await InstallUpdateRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5);

            if (patchPack.LanguageFiles.ContainsKey(languagePackSettings.RegistrySelectedLanguage))
            {
                patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                LogHelper.LoggerRepairFile.Information(string.Format("Downloading and/or extracting language files for Patch 2.22 Version \"{0}\" inlocale \"{1}\" ...", patchPack.MinorVersion, patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode]));
                await InstallUpdateRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URLs, patchPackLanguages.MD5);
            }

            TurnPatchesAndModsViewOn();
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
                else if (childControl is Patch109Button patch109Buttons)
                {
                    patch109Buttons.SelectedIconVisible = (int)patch109Buttons.Tag == Settings.Default.PatchVersionInstalled;
                }
                else if (childControl is Patch222Buttons patch222Buttons)
                {
                    patch222Buttons.SelectedIconVisible = (int)patch222Buttons.Tag == Settings.Default.PatchVersionInstalled;
                }
            }
        }

        private void TurnPatchesAndModsViewOn()
        {
            Update();

            IsLauncherCurrentlyWorking = false;

            PibLoadingRing.Visible = false;
            PibLoadingBorder.Visible = false;
            LabelLoadingPanel.Visible = false;
            LblModExplanation.Visible = true;

            PBarActualFile.Visible = false;
            LblWorkerFileName.Visible = false;
            LblWorkerIOTask.Visible = false;

            BtnInstall.Enabled = true;

            if (Settings.Default.PatchVersionInstalled == patchPack.MinorVersion * 10 + patchPack.Revision || Settings.Default.PatchVersionInstalled == 106 || Settings.Default.PatchVersionInstalled == 109)
                BtnPlayOnline.Enabled = true;

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
            BtnPlayOnline.Enabled = false;

            LaunchGameToolStripMenuItem.Enabled = false;
            RepairGameToolStripMenuItem.Enabled = false;
            SelectGameToolStripMenuItem.Enabled = false;
            MenuItemLaunchGame.Enabled = false;

            Update();
        }
    }
}