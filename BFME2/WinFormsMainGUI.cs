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

        LanguageFiles? patchPackLanguages;
        LanguagePacks languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[Settings.Default.InstalledLanguageISOCode];
        readonly MainPacks mainPack = JSONDataListHelper._MainPackSettings;
        readonly PatchPacks patchPack = JSONDataListHelper._DictionaryPatchPacksSettings[Settings.Default.LatestPatchVersion];
        readonly PatchPacksBeta patchPacksBeta = JSONDataListHelper._PatchBetaSettings;

        readonly ChangelogPagePatch changelogPagePatch = new();
        readonly ChangelogPageLauncher changelogPageLauncher = new();

        static bool IsLauncherCurrentlyWorking = false;

        public WinFormsMainGUI()
        {
            InitializeComponent();

            SysTray.ContextMenuStrip = NotifyContextMenu;
            BtnInstall.Text = Strings.BtnInstall_TextLaunch;

            // label-Styles
            LblWorkerFileName.Font = FontHelper.GetFont(0, 16); ;
            LblWorkerFileName.ForeColor = Color.FromArgb(168, 190, 98);
            LblWorkerFileName.BackColor = Color.Transparent;

            LblWorkerIOTask.Font = FontHelper.GetFont(0, 16); ;
            LblWorkerIOTask.ForeColor = Color.FromArgb(168, 190, 98);
            LblWorkerIOTask.BackColor = Color.Transparent;

            LabelLoadingPanel.Font = FontHelper.GetFont(0, 20);
            LabelLoadingPanel.ForeColor = Color.FromArgb(168, 190, 98);
            LabelLoadingPanel.BackColor = Color.Transparent;
            LabelLoadingPanel.BorderStyle = BorderStyle.None;
            LabelLoadingPanel.Text = Strings.Info_PleaseWait;

            LblModExplanation.Font = FontHelper.GetFont(0, 22);
            LblModExplanation.ForeColor = Color.FromArgb(168, 190, 98);
            LblModExplanation.BackColor = Color.Transparent;
            LblModExplanation.BorderStyle = BorderStyle.None;
            LblModExplanation.OutlineForeColor = Color.FromArgb(24, 63, 20);
            LblModExplanation.OutlineWidth = 2;

            PBarActualFile.ForeColor = Color.FromArgb(168, 190, 98);
            PBarActualFile.BackColor = Color.FromArgb(255, 100, 0);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnInstall.Font = FontHelper.GetFont(0, 16); ;
            BtnInstall.ForeColor = Color.FromArgb(168, 190, 98);

            PanelPlaceholder.BackgroundImage = Helper.Properties.Resources.BFME2BorderRectangleModPanel;
            PanelPlaceholder.BackColor = Color.Transparent;

            //Tooltips
            ToolTip.SetToolTip(PiBThemeSwitcher, Strings.ToolTip_MusicSwitcher);

            BackgroundImage = Helper.Properties.Resources.BFME2BGDefault;

            PibHeader.Image = Helper.Properties.Resources.BFME2_Header;
            PiBYoutube.Image = Helper.Properties.Resources.youtube;
            PiBDiscord.Image = Helper.Properties.Resources.discord;
            PiBModDB.Image = Helper.Properties.Resources.moddb;
            PiBTwitch.Image = Helper.Properties.Resources.twitch;

            PibHeader.Image = Helper.Properties.Resources.BFME2_Header;
            PibLoadingBorder.Image = Helper.Properties.Resources.BFME2LoadingBorder;
            PibLoadingRing.Image = Helper.Properties.Resources.loadingRing;

            PiBVersion106.Image = Helper.Properties.Resources.BFME2PatchModBG106;

            if (Settings.Default.PlayBackgroundMusic)
                PibMute.Image = Helper.Properties.Resources.Unmute;
            else
                PibMute.Image = Helper.Properties.Resources.Mute;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Settings.Default.BackgroundMusicIcon == 0)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoDefault;
            }
            else if (Settings.Default.BackgroundMusicIcon == 1)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoGondor;
            }
            else if (Settings.Default.BackgroundMusicIcon == 2)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoRohan;
            }
            else if (Settings.Default.BackgroundMusicIcon == 3)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoIsengard;
            }
            else if (Settings.Default.BackgroundMusicIcon == 4)
            {
                PiBThemeSwitcher.Image = Helper.Properties.Resources.icoMordor;
            }
        }

        private async void WinFormsMainGUI_Load(object sender, EventArgs e)
        {
            if (patchPack.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
            {
                LanguageFiles patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
            }

            try
            {
                if (Settings.Default.PlayBackgroundMusic)
                {
                    soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
                }

                PanelPlaceholder.Padding = new Padding(80, 60, 80, 60);

                if (!Settings.Default.UseBetaChannel)
                {
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

                if (Settings.Default.UseBetaChannel)
                {
                    TurnPatchesAndModsViewOff();
                    PanelPlaceholder.Visible = false;

                    LblModExplanation.Font = FontHelper.GetFont(0, 30);
                    LblModExplanation.Location = new Point(130, 300);
                    LblModExplanation.Text = Strings.Info_BetaActivated;
                    LblModExplanation.BringToFront();

                    if (patchPacksBeta.Version > Settings.Default.BetaChannelVersion)
                    {
                        await InstallUpdatRepairRoutine(patchPacksBeta.FileName, patchPacksBeta.URL, patchPacksBeta.MD5);
                    }

                    TurnPatchesAndModsViewOn();
                    PanelPlaceholder.Visible = false;
                }

                if ((Settings.Default.GameInstallPath == "" && !Directory.Exists(RegistryService.ReadRegKey("path"))) || RegistryService.ReadRegKey("path") == "ValueNotFound" || !File.Exists(Path.Combine(RegistryService.ReadRegKey("path"), ConstStrings.C_MAIN_GAME_FILE)))
                {
                    Settings.Default.IsGameInstalled = false;
                    BtnInstall.Text = Strings.BtnInstall_TextInstall;
                }
                else if (Settings.Default.LatestPatchVersion > Settings.Default.PatchVersionInstalled)
                {
                    if (Settings.Default.UseBetaChannel || Settings.Default.SelectedOlderPatch)
                    {
                        DialogResult _dialogResult = MessageBox.Show(Strings.Msg_RestartForUpdate_Text, Strings.Msg_Restart_Title, MessageBoxButtons.YesNo);
                        if (_dialogResult == DialogResult.Yes)
                        {
                            Settings.Default.UseBetaChannel = false;
                            Settings.Default.SelectedOlderPatch = false;
                            Settings.Default.Save();

                            Process _restarterProcess = new();
                            _restarterProcess.StartInfo.FileName = ConstStrings.C_RESTARTEREXE_FILENAME;
                            _restarterProcess.StartInfo.Arguments = "--restart --BFME2Launcher";
                            _restarterProcess.StartInfo.WorkingDirectory = Application.StartupPath;
                            _restarterProcess.StartInfo.UseShellExecute = true;
                            _restarterProcess.Start();
                            Application.ExitThread();
                            Application.Exit();
                        }
                        else if (_dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }

                    TurnPatchesAndModsViewOff();

                    Settings.Default.IsGameInstalled = true;
                    Settings.Default.UseBetaChannel = false;
                    Settings.Default.OpenPatchChangelogPage = true;
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKey("path");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage();
                    Settings.Default.PatchVersionInstalled = Settings.Default.LatestPatchVersion;
                    Settings.Default.Save();

                    await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URL, patchPack.MD5);

                    if (patchPack.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode))
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages!.FileName, patchPackLanguages.URL, patchPackLanguages.MD5);
                    }

                    TurnPatchesAndModsViewOn();
                    UpdatePanelButtonActiveState();
                }
                else
                {
                    Settings.Default.IsGameInstalled = true;
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKey("path");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage();
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
                LogHelper.LoggerBFME2GUI.Error(ex, "");
            }
        }

        private void BFME2_Shown(object sender, EventArgs e)
        {
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

        private async void PiBVersion106_Click(object sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();

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
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());
            }

            TurnPatchesAndModsViewOn();
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

                    if (patchPack222.LanguageFiles.ContainsKey(Settings.Default.InstalledLanguageISOCode) && patchPackLanguages != null)
                    {
                        await InstallUpdatRepairRoutine(patchPackLanguages!.FileName, patchPackLanguages.URL, patchPackLanguages.MD5);
                    }

                    Settings.Default.PatchVersionInstalled = version;
                    UpdatePanelButtonActiveState();
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerBFME2GUI.Error(ex.ToString());
                }
            }

            if (Settings.Default.LatestPatchVersion > version)
                Settings.Default.SelectedOlderPatch = true;

            Settings.Default.Save();
            TurnPatchesAndModsViewOn();
        }

        private void GameisClosedEvent(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                    SysTray.Visible = false;
                    TurnPatchesAndModsViewOn();
                }));
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                SysTray.Visible = false;
                TurnPatchesAndModsViewOn();
            }

            if (Settings.Default.PlayBackgroundMusic)
            {
                soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }
        }

        private void BtnInstall_MouseLeave(object sender, EventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnInstall.ForeColor = Color.FromArgb(168, 190, 98);
        }

        private void BtnInstall_MouseEnter(object sender, EventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_HOVER;
            BtnInstall.ForeColor = Color.FromArgb(24, 63, 20);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnInstall_MouseDown(object sender, MouseEventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_CLICK;
            BtnInstall.ForeColor = Color.FromArgb(168, 190, 98);
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

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            soundPlayerHelper.PlayTheme(ConstStrings.C_THEMESOUND_MORDOR);
                        }
                        break;
                    }
            }
        }

        private async Task InstallUpdatRepairRoutine(string ZIPFileName, string DownloadUrl, string CorrectMD5HashValue)
        {
            PBarActualFile.Value = 0;
            PBarActualFile.Maximum = 100;

            string fullPathToZIPFile = Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2, ZIPFileName);

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
                await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2), ZIPFileName, DownloadUrl, progressHandlerDownload);
                LblWorkerFileName.Text = Strings.Info_PleaseWait;
                string calculatedMD5Value = MD5Tools.CalculateMD5(fullPathToZIPFile);

                if (calculatedMD5Value == CorrectMD5HashValue)
                {
                    await gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2), ZIPFileName, Settings.Default.GameInstallPath, progressHandlerExtraction);
                }
                else
                {
                    LogHelper.LoggerBFME2GUI.Error(string.Format("MD5 HashSum check failed. Should be: {0} was: {1}", CorrectMD5HashValue, calculatedMD5Value));
                    LogHelper.LoggerBFME2GUI.Information(string.Format("Deleting file > {0} < and retry Download...", ZIPFileName));
                    File.Delete(fullPathToZIPFile);
                    await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2), ZIPFileName, DownloadUrl, progressHandlerDownload);
                    LogHelper.LoggerBFME2GUI.Information(string.Format("Now trying to extract > {0} <", ZIPFileName));
                    await gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2), ZIPFileName, Settings.Default.GameInstallPath, progressHandlerExtraction);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());
            }
        }

        private Task<bool> PrepareInstallFolder()
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
                        TurnPatchesAndModsViewOff();
                        return Task.FromResult(false);
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());
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

        private void BFME2_FormClosing(object sender, FormClosingEventArgs e)
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

            Settings.Default.Save();
        }

        private void BFME2_Resize(object sender, EventArgs e)
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
            TurnPatchesAndModsViewOff();

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
            TurnPatchesAndModsViewOff();

            LogHelper.LoggerGRepairFile.Information("Started Repairing...");
            await RepairFileHelper.RepairFeature();

            LogHelper.LoggerGRepairFile.Information("Downloading and/or extracting MainGame-Files if needed...");
            await InstallUpdatRepairRoutine(mainPack.FileName, mainPack.URL, mainPack.MD5);

            LogHelper.LoggerGRepairFile.Information("Downloading and/or extracting Language-Files if needed...");
            await InstallUpdatRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URL, languagePackSettings.MD5);

            LogHelper.LoggerGRepairFile.Information(string.Format("Downloading and/or extracting Latest Patch 2.22 Version \"{0}\" ...", patchPack.Version));
            await InstallUpdatRepairRoutine(patchPack.FileName, patchPack.URL, patchPack.MD5);

            if (patchPack.LanguageFiles.ContainsKey(languagePackSettings.RegistrySelectedLocale))
            {
                patchPackLanguages = patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode];
                LogHelper.LoggerGRepairFile.Information(string.Format("Downloading and/or extracting Language-Files for Patch 2.22 Version \"{0}\" in language \"{1}\" ...", patchPack.Version, patchPack.LanguageFiles[Settings.Default.InstalledLanguageISOCode]));
                await InstallUpdatRepairRoutine(patchPackLanguages.FileName, patchPackLanguages.URL, patchPackLanguages.MD5);
            }

            TurnPatchesAndModsViewOn();
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

                        if (Settings.Default.CreateDesktopShortcut)
                        {
                            GameDesktopShortcutToolStripMenuItem.PerformClick();
                        }

                        if (Settings.Default.CreateStartMenuShortcut)
                        {
                            GameStartmenuShortcutsToolStripMenuItem.PerformClick();
                        }

                        taskPrepareInstallFolder.Dispose();

                        TurnPatchesAndModsViewOn();
                    }
                }
                else
                {
                    LaunchGameToolStripMenuItem.PerformClick();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());
            }
        }

        private void MenuItemLaunchGame_Click(object sender, EventArgs e)
        {
            LaunchGameToolStripMenuItem.PerformClick();
        }

        private void UpdatePanelButtonActiveState()
        {
            foreach (Control childControl in PanelPlaceholder.Controls)
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
        private void TurnPatchesAndModsViewOn()
        {
            IsLauncherCurrentlyWorking = false;

            PanelPlaceholder.Visible = true;

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

            if (!Settings.Default.UseBetaChannel)
                PanelPlaceholder.Visible = true;
        }

        private void TurnPatchesAndModsViewOff()
        {
            IsLauncherCurrentlyWorking = true;

            PanelPlaceholder.Visible = false;

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

            if (Settings.Default.UseBetaChannel)
                PanelPlaceholder.Visible = false;
        }
    }
}