using Helper;
using Helper.UserControls;
using PatchLauncher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class WinFormsMainGUI : Form
    {
        int iconNumber = Settings.Default.BackgroundMusicIcon;
        readonly SoundPlayerHelper soundPlayerHelper = new();

        readonly MainPacks mainPack = JSONDataListHelper._MainPackSettings;
        LanguagePacks languagePackSettings = JSONDataListHelper._DictionarylanguageSettings[GameFileTools.CheckIfJSONLanguageExists(Settings.Default.InstalledLanguageISOCode, AssemblyNameHelper.BFMELauncherGameName)];

        readonly ChangelogPagePatch changelogPagePatch = new();
        readonly ChangelogPageLauncher changelogPageLauncher = new();

        readonly string displayName = "The Battle for Middle-earth II";

        static bool IsLauncherCurrentlyWorking = false;

        public WinFormsMainGUI()
        {
            InitializeComponent();

            SysTray.ContextMenuStrip = NotifyContextMenu;
            BtnInstall.Text = Strings.BtnInstall_TextLaunch;

            // label-Styles
            LblWorkerFileName.Font = FontHelper.GetFont(0, 20); ;
            LblWorkerFileName.ForeColor = Color.FromArgb(168, 190, 98);
            LblWorkerFileName.BackColor = Color.Transparent;
            LblWorkerFileName.Text = "";

            LblWorkerIOTask.Font = FontHelper.GetFont(0, 20); ;
            LblWorkerIOTask.ForeColor = Color.FromArgb(168, 190, 98);
            LblWorkerIOTask.BackColor = Color.Transparent;
            LblWorkerIOTask.Text = "";

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

            PBarActualFile.ForeColor = Color.FromArgb(24, 63, 20);
            PBarActualFile.BackColor = Color.FromArgb(168, 190, 98);
            PBarActualFile.TextColor = Color.FromArgb(60, 80, 20);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnInstall.Font = FontHelper.GetFont(0, 16);
            BtnInstall.ForeColor = Color.FromArgb(168, 190, 98);

            BtnPlayOnline.FlatAppearance.BorderSize = 0;
            BtnPlayOnline.FlatStyle = FlatStyle.Flat;
            BtnPlayOnline.BackColor = Color.Transparent;
            BtnPlayOnline.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnPlayOnline.Font = FontHelper.GetFont(0, 16);
            BtnPlayOnline.ForeColor = Color.FromArgb(168, 190, 98);

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

        private void WinFormsMainGUI_Load(object sender, EventArgs e)
        {
            try
            {
                if (Settings.Default.PlayBackgroundMusic)
                {
                    soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
                }

                PanelPlaceholder.Padding = new Padding(80, 60, 80, 60);

                if ((Settings.Default.GameInstallPath == "" && !Directory.Exists(RegistryService.ReadRegKeyBFME2("InstallPath"))) || RegistryService.ReadRegKeyBFME2("path") == ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND || !File.Exists(Path.Combine(RegistryService.ReadRegKeyBFME2("InstallPath"), ConstStrings.C_BFME2_MAIN_GAME_FILE)))
                {
                    Settings.Default.IsGameInstalled = false;
                    BtnInstall.Text = Strings.BtnInstall_TextInstall;
                    Settings.Default.Save();

                    PanelPlaceholder.Visible = false;
                    LaunchGameToolStripMenuItem.Enabled = false;
                    SettingsToolStripMenuItem.Enabled = false;
                    RepairGameToolStripMenuItem.Enabled = false;
                    BFME25ToolStripMenuItem.Enabled = false;
                    MenuItemLaunchGame.Enabled = false;
                    LblModExplanation.Visible = false;
                    BtnPlayOnline.Enabled = false;

                    Update();
                }
                else
                {
                    Settings.Default.IsGameInstalled = true;
                    Settings.Default.GameInstallPath = RegistryService.ReadRegKeyBFME2("InstallPath");
                    Settings.Default.InstalledLanguageISOCode = RegistryService.GameLanguage(AssemblyNameHelper.BFMELauncherGameName);
                    Settings.Default.Save();
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
        }

        private async void PiBVersion106_Click(object sender, EventArgs e)
        {
            TurnPatchesAndModsViewOff();

            try
            {
                if (Settings.Default.PatchVersionInstalled == mainPack.LatestPatchVersionOfficial)
                {
                    MessageBox.Show(Strings.Msg_UpdateAlreadyActive_Text, Strings.Msg_UpdateAlreadyActive_Title, MessageBoxButtons.OK);
                    TurnPatchesAndModsViewOn();
                    UpdatePanelButtonActiveState();
                    return;
                }

                if (File.Exists(Settings.Default.GameInstallPath + @"\dsound.dll"))
                    AssemblyNameHelper.EAXWasActivated = true;
                else
                    AssemblyNameHelper.EAXWasActivated = false;

                LogHelper.LoggerBFME2GUI.Information("Performing Repair Routine after clicking on button > Patch 1.06 <");

                LogHelper.LoggerRepairFile.Information("Started Repairing...");
                await RepairFileHelper.RepairFeature(AssemblyNameHelper.BFMELauncherGameName);

                LogHelper.LoggerRepairFile.Information("Downloading and/or extracting main game files if needed...");
                await InstallUpdateRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);

                LogHelper.LoggerRepairFile.Information("Downloading and/or extracting language files if needed...");
                await InstallUpdateRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);

                if (AssemblyNameHelper.EAXWasActivated)
                {
                    List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                    foreach (var file in _EAXFiles)
                    {
                        File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, file), Path.Combine(Settings.Default.GameInstallPath, file), true);
                    }
                }

                Settings.Default.PatchVersionInstalled = mainPack.LatestPatchVersionOfficial;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());
                MessageBox.Show("Something went wrong. Please see Logfiles for further Details. \n We will no reset the launcher state so you can close it. \n Please click on the discord Logo to get support.");
            }

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

                        RegistryService.WriteRegKeysInstallationBFME2(Settings.Default.GameInstallPath, languagePackSettings.RegistrySelectedLanguage);

                        await InstallUpdateRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);
                        await InstallUpdateRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);

                        List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                        foreach (var file in _EAXFiles)
                        {
                            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, file), Path.Combine(Settings.Default.GameInstallPath, file), true);
                        }

                        Settings.Default.PatchVersionInstalled = mainPack.LatestPatchVersionOfficial;
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
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());

                Settings.Default.IsGameInstalled = false;
                BtnInstall.Text = Strings.BtnInstall_TextInstall;
                Settings.Default.Save();
            }

            SettingsToolStripMenuItem.Enabled = true;
            BFME25ToolStripMenuItem.Enabled = true;

            TurnPatchesAndModsViewOn();
            Update();
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

        private async Task InstallUpdateRepairRoutine(string ZIPFileName, List<string> DownloadURLs, string CorrectMD5HashValue, bool hasExternalInstaller = false)
        {
            try
            {
                string fullPathToZIPFile = Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2, ZIPFileName);
                string calculatedMD5Value = "404";
                bool doesTheFileExist = File.Exists(fullPathToZIPFile);
                bool doesTheFileHasTheCorrectMD5Value = false;

                GameFileTools gameFileTools = new();

                LblWorkerFileName.Visible = true;
                LblWorkerIOTask.Visible = true;

                for (int RetryCount = 0; RetryCount <= DownloadURLs.Count; RetryCount++)
                {
                    BtnInstall.Text = Strings.BtnInstall_TextLaunch;
                    LblWorkerFileName.Text = Strings.Info_MD5Check;
                    LblWorkerIOTask.Text = "";

                    Update();

                    LogHelper.LoggerBFME2GUI.Information("Found File: > {0} <, start to calculate", ZIPFileName);
                    calculatedMD5Value = await MD5Tools.CalculateMD5Async(fullPathToZIPFile);

                    if (calculatedMD5Value == CorrectMD5HashValue)
                    {
                        doesTheFileHasTheCorrectMD5Value = true;
                        doesTheFileExist = true;
                    }

                    if (doesTheFileExist && doesTheFileHasTheCorrectMD5Value)
                    {
                        LogHelper.LoggerBFME2GUI.Information("File > {0} < has the correct value of > {1} <, start to extract file...", ZIPFileName, calculatedMD5Value);
                    }
                    else
                    {
                        if (doesTheFileExist)
                        {
                            LogHelper.LoggerBFME2GUI.Error(string.Format("MD5 HashSum check failed. Should be: > {0} < was: > {1} <", CorrectMD5HashValue, calculatedMD5Value));
                            LogHelper.LoggerBFME2GUI.Information(string.Format("Deleting file > {0} < and retry Download...", ZIPFileName));
                            File.Delete(fullPathToZIPFile);
                        }
                        else
                        {
                            LogHelper.LoggerBFME2GUI.Error(string.Format("File > {0} < does not exist, start downloading...", ZIPFileName));
                        }

                        Progress<ProgressHelper> progressHandlerDownload = new(progress =>
                        {
                            PBarActualFile.Value = (int)progress.PercentageValue;
                            LblWorkerFileName.Text = Path.GetFileNameWithoutExtension(ZIPFileName);
                            LblWorkerIOTask.Text = string.Concat(progress.ProgressedDownloadSizeInBytes / 1024000, " / ", progress.TotalDownloadSizeInBytes / 1024000, " MB @ ", Math.Round(progress.DownloadSpeedSizeInBytes / 1024000), " MB/s");
                        });

                        PBarActualFile.Visible = true;
                        PBarActualFile.Value = 0;
                        PBarActualFile.Maximum = 100;

                        LogHelper.LoggerBFME2GUI.Information("Start downloading file: > {0} <", ZIPFileName);
                        await gameFileTools.DownloadFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2), ZIPFileName, DownloadURLs, RetryCount, progressHandlerDownload);
                    }

                    if (doesTheFileHasTheCorrectMD5Value)
                    {
                        PBarActualFile.Visible = true;

                        Progress<ProgressHelper> progressHandlerExtraction = new(progress =>
                        {
                            PBarActualFile.Value = progress.CurrentlyExtractedFileCount;
                            PBarActualFile.Maximum = progress.TotalArchiveFileCount;
                            LblWorkerFileName.Text = progress.CurrentFileName;
                            LblWorkerIOTask.Text = string.Concat(progress.CurrentlyExtractedFileCount, " / ", progress.TotalArchiveFileCount);
                        });

                        await gameFileTools.ExtractFile(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2), ZIPFileName, Settings.Default.GameInstallPath, progressHandlerExtraction, hasExternalInstaller);

                        RetryCount = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());
                MessageBox.Show("Something went wrong. Please see logfiles for further details. \n We will now reset the launcher state so you can close it. \n Please click on the discord logo to get support.");
                TurnPatchesAndModsViewOn();
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
            TurnPatchesAndModsViewOff();

            try
            {
                Process processLaunchGame = new();
                processLaunchGame.StartInfo.FileName = Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_BFME2_MAIN_GAME_FILE);

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
                LogHelper.LoggerBFME2GUI.Error(ex.ToString());
                DialogResult dialogResult = MessageBox.Show(Strings.Msg_ErrorStartingGame_Text, Strings.Msg_ErrorStartingGame_Title, MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    Settings.Default.PatchVersionInstalled = mainPack.LatestPatchVersionOfficial;
                    Settings.Default.Save();

                    RepairGameToolStripMenuItem.Enabled = true;
                    Update();
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
            Process.Start("explorer.exe", "\"" + RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName) + "\"");
        }

        private void OpenLauncherDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.StartupPath);
        }

        private void OpenLauncherLogfileDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(Application.StartupPath, ConstStrings.C_LOGFOLDER_NAME));
        }

        private void BFME1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process _restarterProcess = new();
            _restarterProcess.StartInfo.FileName = ConstStrings.C_RESTARTEREXE_FILENAME;
            _restarterProcess.StartInfo.Arguments = "--restart --BFME1Launcher";
            _restarterProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            _restarterProcess.StartInfo.UseShellExecute = true;
            _restarterProcess.Start();
            Application.Exit();
        }

        private void BFME25ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process _restarterProcess = new();
            _restarterProcess.StartInfo.FileName = ConstStrings.C_RESTARTEREXE_FILENAME;
            _restarterProcess.StartInfo.Arguments = "--restart --BFME25Launcher";
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

        private async void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
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

                RegistryService.WriteRegKeysInstallationBFME2(Settings.Default.GameInstallPath, languagePackSettings.RegistrySelectedLanguage);

                await InstallUpdateRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);
                await InstallUpdateRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);

                Settings.Default.Save();

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
                    ConstStrings.C_BFME2_MAIN_GAME_FILE),
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
                ShortCutHelper.CreateShortcutToStartMenu(Path.Combine(RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName), ConstStrings.C_BFME2_MAIN_GAME_FILE), displayName,
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
            try
            {
                TurnPatchesAndModsViewOff();

                if (File.Exists(Settings.Default.GameInstallPath + @"\dsound.dll"))
                    AssemblyNameHelper.EAXWasActivated = true;
                else
                    AssemblyNameHelper.EAXWasActivated = false;

                LogHelper.LoggerRepairFile.Information("Started Repairing...");
                await RepairFileHelper.RepairFeature(AssemblyNameHelper.BFMELauncherGameName);

                LogHelper.LoggerRepairFile.Information("Downloading and/or extracting main game files if needed...");
                await InstallUpdateRepairRoutine(mainPack.FileName, mainPack.URLs, mainPack.MD5);

                LogHelper.LoggerRepairFile.Information("Downloading and/or extracting language files if needed...");
                await InstallUpdateRepairRoutine(languagePackSettings.LanguagePackName, languagePackSettings.URLs, languagePackSettings.MD5);

                // LogHelper.LoggerRepairFile.Information("Downloading and/or extracting Patch 109v3 if needed...");
                // await InstallUpdateRepairRoutine(patchPack.FileName, patchPack.URLs, patchPack.MD5, patchPack.HasExternalInstaller);

                if (AssemblyNameHelper.EAXWasActivated)
                {
                    List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                    foreach (var file in _EAXFiles)
                    {
                        File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, file), Path.Combine(Settings.Default.GameInstallPath, file), true);
                    }
                }

                Settings.Default.PatchVersionInstalled = mainPack.LatestPatchVersionOfficial;
                Settings.Default.Save();

                TurnPatchesAndModsViewOn();
                UpdatePanelButtonActiveState();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRepairFile.Error(ex.ToString());
                MessageBox.Show("Something went wrong. Please see Logfiles for further Details. \n We will no reset the launcher state so you can close it. \n Please click on the discord Logo to get support.");

                TurnPatchesAndModsViewOn();
                UpdatePanelButtonActiveState();
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
                if (childControl is PatchesButtonsBFME2 patchesButtonsBFME2)
                {
                    patchesButtonsBFME2.SelectedIconVisible = (int)patchesButtonsBFME2.Tag == Settings.Default.PatchVersionInstalled;
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
            BtnPlayOnline.Enabled = true;

            LaunchGameToolStripMenuItem.Enabled = true;
            RepairGameToolStripMenuItem.Enabled = true;
            SelectGameToolStripMenuItem.Enabled = true;
            MenuItemLaunchGame.Enabled = true;

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

            BtnPlayOnline.Enabled = false;
            BtnInstall.Enabled = false;

            LaunchGameToolStripMenuItem.Enabled = false;
            RepairGameToolStripMenuItem.Enabled = false;
            SelectGameToolStripMenuItem.Enabled = false;
            MenuItemLaunchGame.Enabled = false;

            Update();
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
            BtnPlayOnline.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnPlayOnline.ForeColor = Color.FromArgb(168, 190, 98);
        }

        private void BtnPlayOnline_MouseEnter(object sender, EventArgs e)
        {
            BtnPlayOnline.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_HOVER;
            BtnPlayOnline.ForeColor = Color.FromArgb(24, 63, 20);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnPlayOnline_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPlayOnline.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_CLICK;
            BtnPlayOnline.ForeColor = Color.FromArgb(168, 190, 98);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }
    }
}