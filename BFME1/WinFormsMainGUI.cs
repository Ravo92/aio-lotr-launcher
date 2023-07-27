using Helper;
using Helper.UserControls;
using PatchLauncher.Properties;
using System;
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

        LanguagePacks languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[Settings.Default.InstalledLanguageISOCode];
        readonly MainPacks mainPack = JSONDataListHelper._MainPackSettings;
        readonly PatchPacks patchPack = JSONDataListHelper._DictionaryPatchPacksSettings[Settings.Default.LatestPatchVersion];
        LanguageFiles? patchPackLanguages;

        static bool _IsLauncherCurrentlyWorking = false;

        private bool IsLauncherCurrentlyWorking
        {
            get { return _IsLauncherCurrentlyWorking; }
            set
            {
                _IsLauncherCurrentlyWorking = value;
                if (value)
                {
                    LblModExplanation.Visible = false;

                    PBarActualFile.Visible = true;
                    LblWorkerFileName.Visible = true;
                    LblWorkerIOTask.Visible = true;

                    PiBArrow.Enabled = false;

                    if (Settings.Default.IsPatchModsShown)
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
                    else
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;

                    BtnInstall.Enabled = false;

                    LaunchGameToolStripMenuItem.Enabled = false;
                    RepairGameToolStripMenuItem.Enabled = false;
                }
                else
                {
                    LblModExplanation.Visible = false;

                    PBarActualFile.Visible = false;
                    LblWorkerFileName.Visible = false;
                    LblWorkerIOTask.Visible = false;

                    PiBArrow.Enabled = true;

                    if (Settings.Default.IsPatchModsShown)
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                    else
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;

                    BtnInstall.Enabled = true;

                    LaunchGameToolStripMenuItem.Enabled = true;
                    RepairGameToolStripMenuItem.Enabled = true;
                }
            }
        }

        public WinFormsMainGUI()
        {
            WebView2Helper.InitializeWebView2Settings();
            InitializeComponent();

            SysTray.ContextMenuStrip = NotifyContextMenu;
            BtnInstall.Text = Strings.BtnInstall_TextLaunch;

            //Main Form style behaviour
            PibLoadingRing.Visible = true;
            PibLoadingBorder.Visible = true;
            PiBArrow.Visible = false;
            LblPatchNotes.Visible = true;
            PnlPlaceholder.Visible = false;
            Wv2Patchnotes.Visible = false;

            // label-Styles
            LblWorkerFileName.Font = FontHelper.GetFont(0, 16); ;
            LblWorkerFileName.ForeColor = Color.FromArgb(192, 145, 69);
            LblWorkerFileName.BackColor = Color.Transparent;

            LblWorkerIOTask.Font = FontHelper.GetFont(0, 16); ;
            LblWorkerIOTask.ForeColor = Color.FromArgb(192, 145, 69);
            LblWorkerIOTask.BackColor = Color.Transparent;

            LblPatchNotes.Font = FontHelper.GetFont(0, 16);
            LblPatchNotes.ForeColor = Color.FromArgb(192, 145, 69);
            LblPatchNotes.BackColor = Color.Transparent;
            LblPatchNotes.BorderStyle = BorderStyle.None;

            LblModExplanation.Font = FontHelper.GetFont(0, 21);
            LblModExplanation.ForeColor = Color.FromArgb(192, 145, 69);
            LblModExplanation.BackColor = Color.Transparent;
            LblModExplanation.BorderStyle = BorderStyle.None;
            LblModExplanation.OutlineWidth = 10;

            PBarActualFile.ForeColor = Color.FromArgb(192, 145, 69);
            PBarActualFile.BackColor = Color.FromArgb(255, 100, 0);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnInstall.Font = FontHelper.GetFont(0, 16); ;
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);

            //Tooltips
            ToolTip.SetToolTip(PiBThemeSwitcher, Strings.ToolTip_MusicSwitcher);

            PibHeader.Image = Helper.Properties.Resources.BFME1_Header;
            PiBYoutube.Image = Helper.Properties.Resources.youtube;
            PiBDiscord.Image = Helper.Properties.Resources.discord;
            PiBModDB.Image = Helper.Properties.Resources.moddb;
            PiBTwitch.Image = Helper.Properties.Resources.twitch;

            if (Settings.Default.IsPatchModsShown)
                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;

            PibHeader.Image = Helper.Properties.Resources.BFME1_Header;
            PibLoadingBorder.Image = Helper.Properties.Resources.loadingBorder;
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

            TmrPatchNotes.Tick += new EventHandler(TmrPatchNotes_Tick);
            TmrPatchNotes.Interval = 1;
            TmrPatchNotes.Start();
        }

        private async void BFME1_Shown(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;
            LanguageFiles patchPackLanguages = patchPack.LanguageFiles["de"];

            try
            {
                // Check Music Settings
                if (Settings.Default.PlayBackgroundMusic)
                {
                    soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
                }

                PnlPlaceholder.Padding = new Padding(80, 60, 80, 60);
                //PnlPlaceholder.Margin = new Padding(80);

                if (!Settings.Default.UseBetaChannel)
                {
                    if (JSONDataListHelper._DictionaryPatchPacksSettings.ContainsKey(106))
                    {
                        Patch106Button patch106Button = new();
                        PnlPlaceholder.Controls.Add(patch106Button);
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
                        PnlPlaceholder.Controls.Add(patch222Buttons);
                    }
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
                    //Settings.Default.IsPatch106Installed = false;
                    //Settings.Default.IsPatch33Installed = false;
                    //Settings.Default.IsPatch34Installed = false;

                    Settings.Default.IsGameInstalled = true;
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKey("path");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage();

                    await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URL, patchPack.MD5);

                    if (patchPack.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5);
                    }

                    //Settings.Default.IsPatch34Downloaded = true;
                    //Settings.Default.IsPatch34Installed = true;
                }
                else
                {
                    Settings.Default.IsGameInstalled = true;
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKey("path");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage();
                }

                if (Settings.Default.UseBetaChannel)
                {
                    LblModExplanation.Text = Strings.Info_BetaActivated;

                    if (Settings.Default.LatestBetaPatchVersion > Settings.Default.BetaChannelVersion)
                    {
                        await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URL, patchPack.MD5);
                    }
                }

                if (ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_GAMETITLE_NAME_EN))
                    GameDesktopShortcutToolStripMenuItem.Checked = true;
                else
                    GameDesktopShortcutToolStripMenuItem.Checked = false;

                if (ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN), ConstStrings.C_GAMETITLE_NAME_EN))
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
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex, "");
            }

            IsLauncherCurrentlyWorking = false;
        }

        private async void PiBVersion103_Click(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;

            try
            {
                await PatchModDetectionHelper.DeletePatch106();
                await PatchModDetectionHelper.DeletePatch222Files();

                Settings.Default.PatchVersionInstalled = 103;
                Settings.Default.Save();
                UpdatePanelButtonActiveState();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex.ToString());
            }

            IsLauncherCurrentlyWorking = false;
        }

        private async void PatchButton106Clicked(object? sender, EventArgs e)
        {
            if (IsLauncherCurrentlyWorking)
                return;
            else
                IsLauncherCurrentlyWorking = true;

            Patch106Button patch106Button = (Patch106Button)sender!;
            int version = Convert.ToInt32(patch106Button.Tag);
            PatchPacks patchPack106 = JSONDataListHelper._DictionaryPatchPacksSettings[version];
            LanguageFiles patchPackLanguages = patchPack106.LanguageFiles[Settings.Default.InstalledLanguageISOCode];

            if (Settings.Default.PatchVersionInstalled == version)
            {
                await PatchModDetectionHelper.DeletePatch106();
                await PatchModDetectionHelper.DeletePatch222Files();

                Settings.Default.PatchVersionInstalled = 103;
                UpdatePanelButtonActiveState();
            }
            else
            {
                try
                {
                    await PatchModDetectionHelper.DeletePatch106();
                    await PatchModDetectionHelper.DeletePatch222Files();

                    await InstallUpdatRepairRoutine(patchPack106.FileName, patchPack106.URL, patchPack106.MD5);

                    if (patchPack106.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5);
                    }

                    Settings.Default.PatchVersionInstalled = version;
                    UpdatePanelButtonActiveState();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                }
            }

            Settings.Default.Save();
            IsLauncherCurrentlyWorking = false;
        }

        private async void PatchButton222Clicked(object? sender, EventArgs e)
        {
            if (IsLauncherCurrentlyWorking)
                return;
            else
                IsLauncherCurrentlyWorking = true;

            Patch222Buttons patch222Buttons = (Patch222Buttons)sender!;
            int version = Convert.ToInt32(patch222Buttons.Tag);
            PatchPacks patchPack222 = JSONDataListHelper._DictionaryPatchPacksSettings[version];
            LanguageFiles patchPackLanguages = patchPack222.LanguageFiles[Settings.Default.InstalledLanguageISOCode];

            if (Settings.Default.PatchVersionInstalled == version)
            {
                await PatchModDetectionHelper.DeletePatch106();
                await PatchModDetectionHelper.DeletePatch222Files();

                Settings.Default.PatchVersionInstalled = 103;
                UpdatePanelButtonActiveState();
            }
            else
            {
                try
                {
                    await PatchModDetectionHelper.DeletePatch106();
                    await PatchModDetectionHelper.DeletePatch222Files();

                    await InstallUpdatRepairRoutine(patchPack222.FileName, patchPack222.URL, patchPack222.MD5);

                    if (patchPack222.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5);
                    }

                    Settings.Default.PatchVersionInstalled = version;
                    UpdatePanelButtonActiveState();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                }
            }

            Settings.Default.Save();
            IsLauncherCurrentlyWorking = false;
        }

        public void GameisClosedEvent(object? sender, EventArgs e)
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
                soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }
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

        private void PiBArrow_Click(object sender, EventArgs e)
        {
            TmrAnimation.Enabled = true;
        }

        public async Task InstallUpdatRepairRoutine(string ZIPFileName, string DownloadUrl, string CorrectMD5HashValue)
        {
            PBarActualFile.Value = 0;
            PBarActualFile.Maximum = 100;

            string fullPathToZIPFile = Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, ZIPFileName);

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

            try
            {
                BtnInstall.Text = Strings.BtnInstall_TextLaunch;
                GameFileTools gameFileTools = new();
                await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME), ZIPFileName, DownloadUrl, progressHandlerDownload);
                LblWorkerFileName.Text = Strings.Info_PleaseWait;
                string calculatedMD5Value = MD5Tools.CalculateMD5(fullPathToZIPFile);

                if (calculatedMD5Value == CorrectMD5HashValue)
                {
                    await gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME), ZIPFileName, Settings.Default.GameInstallPath, progressHandlerExtraction);
                }
                else
                {
                    LogHelper.LoggerBFME1GUI.Error(string.Format("MD5 HashSum check failed. Should be: {0} was: {1}", CorrectMD5HashValue, calculatedMD5Value));
                    LogHelper.LoggerBFME1GUI.Information(string.Format("Deleting file > {0} < and retry Download...", ZIPFileName));
                    File.Delete(fullPathToZIPFile);
                    await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME), ZIPFileName, DownloadUrl, progressHandlerDownload);
                    LogHelper.LoggerBFME1GUI.Information(string.Format("Now trying to extract > {0} <", ZIPFileName));
                    await gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME), ZIPFileName, Settings.Default.GameInstallPath, progressHandlerExtraction);
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
                LogHelper.LoggerBFME1GUI.Error(ex.ToString());
            }
        }

        private static Task<bool> PrepareInstallFolder()
        {
            try
            {
                if (!Directory.Exists(Settings.Default.GameInstallPath))
                {
                    Directory.CreateDirectory(Settings.Default.GameInstallPath);
                }

                if (Directory.Exists(Settings.Default.GameInstallPath) && Directory.GetFileSystemEntries(Settings.Default.GameInstallPath).Length != 0)
                {
                    DialogResult dialogResult = MessageBox.Show(Strings.Msg_InstallFolderNotEmpty_Text, Strings.Msg_InstallFolderNotEmpty_Title, MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        Directory.Delete(Settings.Default.GameInstallPath, true);
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        _IsLauncherCurrentlyWorking = false;
                        return Task.FromResult(false);
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME1GUI.Error(ex.ToString());
                return Task.FromResult(false);
            }
        }

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

        private void TmrPatchNotes_Tick(object? sender, EventArgs e)
        {
            TmrPatchNotes.Stop();
            PibLoadingRing.Visible = false;
            PibLoadingBorder.Visible = false;
            LblPatchNotes.Visible = false;

            PiBArrow.BackColor = Color.FromArgb(24, 24, 24);
            PiBArrow.Visible = true;

            Wv2Patchnotes.Visible = true;
            PnlPlaceholder.Visible = true;

            if (Settings.Default.ShowPatchesFirst)
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                PiBArrow.BackColor = Color.Transparent;
                PnlPlaceholder.BackgroundImage = Helper.Properties.Resources.BorderRectangleModPanel;
                PnlPlaceholder.BackColor = Color.Transparent;
                LblModExplanation.BackColor = Color.Transparent;
                LblModExplanation.Visible = true;

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
                LblModExplanation.Visible = false;

                PiBArrow.Left = 14;
                Wv2Patchnotes.Left = 12;

                Settings.Default.IsPatchModsShown = false;
                Settings.Default.Save();
            }
        }

        private void TmrAnimation_Tick(object sender, EventArgs e)
        {
            int startLeft = 12;  // start position of the panel
            int endLeft = 1300;      // end position of the panel
            int endLeftArrow = 1212;
            int stepSize = 5;     // pixels to move
            int endRight = 12;      // end position of the panel

            // incrementally move

            if (Wv2Patchnotes.Left == startLeft)
            {
                while (Wv2Patchnotes.Left != endLeft)
                {
                    Wv2Patchnotes.Left += stepSize;

                    if (PiBArrow.Left < endLeftArrow)
                    {
                        PiBArrow.Left += stepSize;
                    }
                    // make sure we didn't over shoot
                    if (Wv2Patchnotes.Left > endLeft) Wv2Patchnotes.Left = endLeft;

                    // have we arrived?
                    if (Wv2Patchnotes.Left == endLeft)
                    {
                        TmrAnimation.Enabled = false;
                    }
                }

                PiBArrow.Left = endLeftArrow;

                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                PiBArrow.BackColor = Color.Transparent;
                PnlPlaceholder.BackgroundImage = Helper.Properties.Resources.BorderRectangleModPanel;
                PnlPlaceholder.BackColor = Color.Transparent;
                LblModExplanation.BackColor = Color.Transparent;
                LblModExplanation.Visible = true;
                Wv2Patchnotes.Visible = false;

                Settings.Default.IsPatchModsShown = true;
                Settings.Default.Save();
            }
            else
            {
                PnlPlaceholder.BackColor = Color.FromArgb(24, 24, 24);
                PnlPlaceholder.BackgroundImage = null;
                PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
                PiBArrow.BackColor = Color.FromArgb(24, 24, 24);
                LblModExplanation.Visible = false;
                Wv2Patchnotes.Visible = true;

                Settings.Default.IsPatchModsShown = false;
                Settings.Default.Save();

                while (Wv2Patchnotes.Left != endRight)
                {
                    Wv2Patchnotes.Left -= stepSize;

                    if (PiBArrow.Left > endRight)
                    {
                        PiBArrow.Left -= stepSize;
                    }
                    // make sure we didn't over shoot
                    if (Wv2Patchnotes.Left < endRight) Wv2Patchnotes.Left = endRight;

                    // have we arrived?
                    if (Wv2Patchnotes.Left == endRight)
                    {
                        TmrAnimation.Enabled = false;
                    }
                }

                PiBArrow.Left = endRight;
            }
        }

        private void BFME1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsLauncherCurrentlyWorking)
            {
                MessageBox.Show(Strings.Warning_CantStopNow, Strings.Warning_CantStopNowTitle);
                e.Cancel = true;
            }

            if (Settings.Default.PatchVersionInstalled < Settings.Default.LatestPatchVersion)
            {
                Settings.Default.SelectedOlderPatch = true;
            }
            else
            {
                Settings.Default.SelectedOlderPatch = false;
            }

            Wv2Patchnotes.Dispose();
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

        private void SysTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            SysTray.Visible = false;

            if (Settings.Default.PlayBackgroundMusic)
            {
                soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }
        }

        private void LaunchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsLauncherCurrentlyWorking = true;

            Process processLaunchGame = new();
            processLaunchGame.StartInfo.FileName = Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_GAME_FILE);

            // Start game windowed
            if (Settings.Default.StartGameWindowed)
            {
                processLaunchGame.StartInfo.Arguments = "-win";
            }

            processLaunchGame.StartInfo.WorkingDirectory = Settings.Default.GameInstallPath;
            processLaunchGame.Start();

            WindowState = FormWindowState.Minimized;
            Hide();

            SysTray.Visible = true;
            SysTray.ShowBalloonTip(2000);
            soundPlayerHelper.StopTheme();

            processLaunchGame.WaitForExitAsync();
            processLaunchGame.Exited += GameisClosedEvent;
        }

        private void CloseTheLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            CreditsForm creditsForm = new();

            DialogResult dialogResult = creditsForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                creditsForm.Close();
            }
        }

        private void MessagesFromTheTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangelogPageLauncher changelogPageLauncher = new();

            DialogResult dialogResult = changelogPageLauncher.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                changelogPageLauncher.Close();
            }
        }

        private void LauncherSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LauncherOptionsForm launcherOptionsForm = new();
            launcherOptionsForm.ShowDialog();
        }

        private void GameSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameOptionsForm _Gameoptions = new();
            _Gameoptions.ShowDialog();
        }

        private void GameDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_GAMETITLE_NAME_EN))
            {
                ShortCutHelper.DeleteGameShortcutFromDesktop();
                GameDesktopShortcutToolStripMenuItem.Checked = false;
            }
            else
            {
                ShortCutHelper.CreateShortcutToDesktop(Path.Combine(RegistryService.GameInstallPath(),
                    ConstStrings.C_MAIN_GAME_FILE),
                    ConstStrings.C_GAMETITLE_NAME_EN,
                    Settings.Default.StartGameWindowed == true ? "-win" : "");
                GameDesktopShortcutToolStripMenuItem.Checked = true;
            }
        }

        private void GameStartmenuShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Electronic Arts", ConstStrings.C_GAMETITLE_NAME_EN),
                ConstStrings.C_GAMETITLE_NAME_EN))
            {
                ShortCutHelper.DeleteGameShortcutsFromStartMenu();
                GameStartmenuShortcutsToolStripMenuItem.Checked = false;
            }
            else
            {
                ShortCutHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(), ConstStrings.C_MAIN_GAME_FILE), ConstStrings.C_GAMETITLE_NAME_EN,
                    Path.Combine("Programs", "Electronic Arts",
                    ConstStrings.C_GAMETITLE_NAME_EN),
                    Settings.Default.StartGameWindowed == true ? "-win" : "");

                ShortCutHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(), ConstStrings.C_WORLDBUILDER_FILE), "Worldbuilder",
                    Path.Combine("Programs",
                    "Electronic Arts",
                    ConstStrings.C_GAMETITLE_NAME_EN));

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
            IsLauncherCurrentlyWorking = true;

            //await RepairFileHelper.RepairFeature();

            IsLauncherCurrentlyWorking = false;
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
                        IsLauncherCurrentlyWorking = true;

                        Task<bool> taskPrepareInstallFolder = PrepareInstallFolder();
                        taskPrepareInstallFolder.Wait();

                        if (!taskPrepareInstallFolder.Result)
                        {
                            throw new Exception("Question for cleaning target install folder answered with no");
                        }

                        languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[Settings.Default.InstalledLanguageISOCode];

                        RegistryService.WriteRegKeysInstallation(Settings.Default.GameInstallPath, languagePackSettings.RegistrySelectedLocale, languagePackSettings.RegistrySelectedLanguageName, languagePackSettings.RegistrySelectedLanguage);

                        await InstallUpdatRepairRoutine(mainPack.FileName, mainPack.URL, mainPack.MD5);
                        await InstallUpdatRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URL, languagePackSettings.MD5);
                        await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URL, patchPack.MD5);

                        if (patchPack.LanguageFiles.ContainsKey(languagePackSettings.RegistrySelectedLocale))
                        {
                            patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                            await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5);
                        }

                        patchPack.Version = Settings.Default.PatchVersionInstalled;
                        Settings.Default.Save();

                        if (Settings.Default.CreateDesktopShortcut) //&& !ShortCutHelper.DoesTheShortCutExist(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ConstStrings.C_LAUNCHER_SHORTCUT_NAME))
                        {
                            GameDesktopShortcutToolStripMenuItem.PerformClick();
                        }

                        if (Settings.Default.CreateStartMenuShortcut) //&& !ShortCutHelper.DoesTheShortCutExist(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs\\Electronic Arts" + ConstStrings.C_GAMETITLE_NAME_EN), ConstStrings.C_GAMETITLE_NAME_EN))
                        {
                            GameStartmenuShortcutsToolStripMenuItem.PerformClick();
                        }

                        taskPrepareInstallFolder.Dispose();

                        IsLauncherCurrentlyWorking = false;
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
            }
        }

        private void MenuItemLaunchGame_Click(object sender, EventArgs e)
        {
            LaunchGameToolStripMenuItem.PerformClick();
        }

        private void UpdatePanelButtonActiveState()
        {
            foreach (Control childControl in PnlPlaceholder.Controls)
            {
                if (childControl is Patch106Button)
                {
                    Patch106Button patch106Buttons = (Patch106Button)childControl;
                    patch106Buttons.SelectedIconVisible = (int)patch106Buttons.Tag == Settings.Default.PatchVersionInstalled;
                }
                else if (childControl is Patch222Buttons)
                {
                    Patch222Buttons patch222Buttons = (Patch222Buttons)childControl;
                    patch222Buttons.SelectedIconVisible = (int)patch222Buttons.Tag == Settings.Default.PatchVersionInstalled;
                }
            }
        }
    }
}