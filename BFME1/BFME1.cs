using AutoUpdaterDotNET;
using Downloader;
using Helper;
using Microsoft.Web.WebView2.Core;
using PatchLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = System.Drawing.Color;
using DownloadProgressChangedEventArgs = Downloader.DownloadProgressChangedEventArgs;

namespace PatchLauncher
{
    public partial class BFME1 : Form
    {
        int iconNumber = Settings.Default.BackgroundMusicIcon;
        SoundPlayerHelper _soundPlayerHelper = new();

        public BFME1()
        {
            #region logic

            InitializeComponent();

            InitializeWebView2Settings();
            CheckForUpdates();

            SysTray.ContextMenuStrip = NotifyContextMenu;

            if (!Directory.Exists(ConstStrings.GameAppdataFolderPath()))
                Directory.CreateDirectory(ConstStrings.GameAppdataFolderPath());

            if (!File.Exists(Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME)))
                File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_OPTIONSINI_FILENAME), Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME));

            XMLFileHelper.GetXMLFileData(true);
            XMLFileHelper.GetXMLFileData(false);

            TmrPatchNotes.Tick += new EventHandler(TmrPatchNotes_Tick);
            TmrPatchNotes.Interval = 2000;
            TmrPatchNotes.Start();

            BtnInstall.Hide();

            #endregion

            #region Styles
            //Main Form style behaviour

            PibLoadingRing.Show();
            PibLoadingBorder.Show();
            PiBArrow.Hide();
            LblPatchNotes.Show();
            PnlPlaceholder.Hide();
            Wv2Patchnotes.Hide();

            PiBVersion222_4.Show();
            PiBVersion222_5.Show();
            PiBVersion222_6.Show();

            BtnLaunch.Text = "WORKING...";
            BtnLaunch.Enabled = false;

            // label-Styles
            LblDownloadSpeed.Text = "";
            LblDownloadSpeed.Font = FontHelper.GetFont(0, 16); ;
            LblDownloadSpeed.ForeColor = Color.FromArgb(192, 145, 69);
            LblDownloadSpeed.BackColor = Color.Transparent;

            LblFileName.Text = "";
            LblFileName.Font = FontHelper.GetFont(0, 16); ;
            LblFileName.ForeColor = Color.FromArgb(192, 145, 69);
            LblFileName.BackColor = Color.Transparent;

            LblBytes.Text = "";
            LblBytes.Font = FontHelper.GetFont(0, 16); ;
            LblBytes.ForeColor = Color.FromArgb(192, 145, 69);
            LblBytes.BackColor = Color.Transparent;

            LblPatchNotes.Text = "Loading Patch-Notes...";
            LblPatchNotes.Font = FontHelper.GetFont(0, 16);
            LblPatchNotes.ForeColor = Color.FromArgb(192, 145, 69);
            LblPatchNotes.BackColor = Color.Transparent;
            LblPatchNotes.BorderStyle = BorderStyle.None;

            LblInstalledPatches.Text = "Patches";
            LblInstalledPatches.Font = FontHelper.GetFont(1, 24);
            LblInstalledPatches.ForeColor = Color.FromArgb(192, 145, 69);
            LblInstalledPatches.BackColor = Color.Transparent;
            LblInstalledPatches.BorderStyle = BorderStyle.None;
            LblInstalledPatches.OutlineWidth = 6;

            LblModExplanation.Text = "Here you can choose which patch you want to play. The active one will get a star symbol in bottom right.";
            LblModExplanation.Font = FontHelper.GetFont(0, 20);
            LblModExplanation.ForeColor = Color.FromArgb(192, 145, 69);
            LblModExplanation.BackColor = Color.Transparent;
            LblModExplanation.BorderStyle = BorderStyle.None;
            LblModExplanation.OutlineWidth = 6;

            PBarActualFile.ForeColor = Color.FromArgb(192, 145, 69);
            PBarActualFile.BackColor = Color.FromArgb(192, 145, 69);

            BtnLaunch.FlatAppearance.BorderSize = 0;
            BtnLaunch.FlatStyle = FlatStyle.Flat;
            BtnLaunch.BackColor = Color.Transparent;
            BtnLaunch.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnLaunch.Font = FontHelper.GetFont(0, 16); ;
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);

            BtnOptions.FlatAppearance.BorderSize = 0;
            BtnOptions.FlatStyle = FlatStyle.Flat;
            BtnOptions.BackColor = Color.Transparent;
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnOptions.Font = FontHelper.GetFont(0, 16); ;
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnInstall.Font = FontHelper.GetFont(0, 16); ;
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);

            BtnAdvanced.FlatAppearance.BorderSize = 0;
            BtnAdvanced.FlatStyle = FlatStyle.Flat;
            BtnAdvanced.BackColor = Color.Transparent;
            BtnAdvanced.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnAdvanced.Font = FontHelper.GetFont(0, 16); ;
            BtnAdvanced.ForeColor = Color.FromArgb(192, 145, 69);

            #endregion

            #region Tooltips
            //Tooltips
            ToolTip.SetToolTip(PiBThemeSwitcher, "Switch between faction music and default theme music");
            #endregion

            #region HUD Elements
            PibHeader.Image = Helper.Properties.Resources.header;
            PiBYoutube.Image = Helper.Properties.Resources.youtube;
            PiBDiscord.Image = Helper.Properties.Resources.discord;
            PiBModDB.Image = Helper.Properties.Resources.moddb;
            PiBTwitch.Image = Helper.Properties.Resources.twitch;
            PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
            PiBVersion103.Image = Helper.Properties.Resources.BtnPatchSelection_103;

            if (Settings.Default.IsPatch30Installed)
                Settings.Default.PatchVersionInstalled = 30;

            if (Settings.Default.PlayBackgroundMusic)
                PibMute.Image = Helper.Properties.Resources.Unmute;
            else
                PibMute.Image = Helper.Properties.Resources.Mute;


            if (Settings.Default.IsPatch106Installed)
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Selected;
            else
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;


            if (Settings.Default.IsPatch29Installed)
                PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Selected;
            else
                PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;


            if (Settings.Default.IsPatch30Installed)
                PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Selected;
            else
                PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;


            if (Settings.Default.IsPatch31Installed)
                PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Selected;
            else
                PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;


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
        }

        #region Launcher Auto-Updater

        private static async void InitializeWebView2Settings()
        {
            try
            {
                var version = CoreWebView2Environment.GetAvailableBrowserVersionString();

                File.WriteAllText("webView2_Version.log", version);
            }
            catch (WebView2RuntimeNotFoundException)
            {
                string fileName = Path.Combine(Application.StartupPath, "Tools", "MicrosoftEdgeWebview2Setup.exe");
                await RunWebViewSilentSetupAsync(fileName);
            }
        }

        public static async Task RunWebViewSilentSetupAsync(string fileName)
        {
            var p = Process.Start(fileName, new[] { "/silent", "/install" });
            await p.WaitForExitAsync().ConfigureAwait(false);
        }

        public static void CheckForUpdates()
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

        #endregion

        #region Button Behaviours

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

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

            BtnLaunch.Enabled = false;
            BtnLaunch.Text = "GAME RUNNING";
            BtnAdvanced.Hide();

            SysTray.Visible = true;
            SysTray.ShowBalloonTip(2000);
            _soundPlayerHelper.StopTheme();

            _process.WaitForExitAsync();
            _process.Exited += GameisClosed;
        }

        public void GameisClosed(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                    SysTray.Visible = false;
                    BtnAdvanced.Show();
                    BtnLaunch.Enabled = true;
                    BtnLaunch.Text = "PLAY GAME";
                    PiBVersion103.Enabled = true;
                    PiBVersion106.Enabled = true;
                    PiBVersion222_4.Enabled = true;
                    PiBVersion222_5.Enabled = true;
                    PiBVersion222_6.Enabled = true;
                }));
            }

            if (Settings.Default.PlayBackgroundMusic)
            {
                _soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }
        }

        private void BtnLaunch_MouseLeave(object sender, EventArgs e)
        {
            BtnLaunch.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnLaunch_MouseEnter(object sender, EventArgs e)
        {
            BtnLaunch.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnLaunch.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnLaunch_MouseDown(object sender, MouseEventArgs e)
        {
            BtnLaunch.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }
        private void BtnOptions_Click(object sender, EventArgs e)
        {
            OptionsBFME1 _options = new();
            _options.ShowDialog();
        }
        private void BtnOptions_MouseLeave(object sender, EventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);
        }
        private void BtnOptions_MouseEnter(object sender, EventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnOptions.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }
        private void BtnOptions_MouseDown(object sender, MouseEventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private async void BtnInstall_Click(object sender, EventArgs e)
        {
            if (!UACHelper.UACHelper.IsElevated)
            {
                ProcessStartInfo startInfo = new()
                {
                    FileName = Application.ExecutablePath,
                    Verb = "runas",
                    UseShellExecute = true,
                    Arguments = "--install"
                };

                UACHelper.UACHelper.StartElevated(startInfo);
                Application.Exit();
            }
            else
            {
                InstallPathDialog _install = new();

                DialogResult dr = _install.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    await InstallRoutine();
                }
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

        private void BtnOpenAppDataFolder_Click(object sender, EventArgs e)
        {
            Advanced _advanced = new();
            _advanced.ShowDialog();
        }

        private void BtnOpenAppDataFolder_MouseLeave(object sender, EventArgs e)
        {
            BtnAdvanced.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnAdvanced.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnOpenAppDataFolder_MouseEnter(object sender, EventArgs e)
        {
            BtnAdvanced.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnAdvanced.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnOpenAppDataFolder_MouseDown(object sender, MouseEventArgs e)
        {
            BtnAdvanced.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnAdvanced.ForeColor = Color.FromArgb(192, 145, 69);
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
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;

            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (PatchModDetectionHelper.DetectPatch106())
            {
                PatchModDetectionHelper.DeletePatch106();
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;
            }

            PatchModDetectionHelper.DeletePatch222Files();

            Settings.Default.PatchVersionInstalled = 103;

            Settings.Default.IsPatch106Installed = false;
            Settings.Default.IsPatch29Installed = false;
            Settings.Default.IsPatch30Installed = false;
            Settings.Default.IsPatch31Installed = false;

            if (Settings.Default.IsPatch106Downloaded)
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;
            else
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Download;


            if (Settings.Default.IsPatch29Downloaded)
                PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
            else
                PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;


            if (Settings.Default.IsPatch30Downloaded)
                PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;
            else
                PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Download;


            if (Settings.Default.IsPatch31Downloaded)
                PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;
            else
                PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Download;

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;

            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;

            Settings.Default.Save();
        }

        private async void PiBVersion106_Click(object sender, EventArgs e)
        {
            PatchModDetectionHelper.DeletePatch106();
            PatchModDetectionHelper.DeletePatch222Files();

            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;

            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch106Downloaded)
            {
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Selected;

                Settings.Default.PatchVersionInstalled = 106;
                Settings.Default.IsPatch106Downloaded = true;
                Settings.Default.IsPatch106Installed = true;

                Settings.Default.IsPatch29Installed = false;
                Settings.Default.IsPatch30Installed = false;
                Settings.Default.IsPatch31Installed = false;


                if (Settings.Default.IsPatch29Downloaded)
                    PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
                else
                    PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;


                if (Settings.Default.IsPatch30Downloaded)
                    PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;
                else
                    PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Download;


                if (Settings.Default.IsPatch31Downloaded)
                    PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;
                else
                    PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Download;
            }
            else
            {
                if (Settings.Default.IsPatch106Installed)
                {
                    PatchModDetectionHelper.DeletePatch106();
                    PatchModDetectionHelper.DeletePatch222Files();
                    Settings.Default.PatchVersionInstalled = 103;
                    Settings.Default.IsPatch106Installed = false;
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;


                    if (Settings.Default.IsPatch29Downloaded)
                        PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
                    else
                        PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;
                }
                else
                {
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Selected;

                    Settings.Default.PatchVersionInstalled = 106;
                    Settings.Default.IsPatch106Downloaded = true;
                    Settings.Default.IsPatch106Installed = true;

                    Settings.Default.IsPatch29Installed = false;
                    Settings.Default.IsPatch30Installed = false;
                    Settings.Default.IsPatch31Installed = false;


                    if (Settings.Default.IsPatch29Downloaded)
                        PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
                    else
                        PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;


                    if (Settings.Default.IsPatch30Downloaded)
                        PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;
                    else
                        PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Download;


                    if (Settings.Default.IsPatch31Downloaded)
                        PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;
                    else
                        PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Download;
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;

            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;

            Settings.Default.Save();
        }

        private async void PiBVersion222_4_Click(object sender, EventArgs e)
        {
            PatchModDetectionHelper.DeletePatch106();
            PatchModDetectionHelper.DeletePatch222Files();

            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;

            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch29Downloaded)
            {
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP29_NAME, "https://dl.dropboxusercontent.com/s/ej1mdbuv4xi53ln/Patch_2.22v29.7z");

                PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Selected;
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                Settings.Default.PatchVersionInstalled = 29;
                Settings.Default.IsPatch29Downloaded = true;
                Settings.Default.IsPatch29Installed = true;

                Settings.Default.IsPatch106Installed = false;
                Settings.Default.IsPatch30Installed = false;
                Settings.Default.IsPatch31Installed = false;


                if (Settings.Default.IsPatch30Downloaded)
                    PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;
                else
                    PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Download;


                if (Settings.Default.IsPatch31Downloaded)
                    PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;
                else
                    PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Download;
            }
            else
            {
                if (Settings.Default.IsPatch29Installed)
                {
                    Settings.Default.PatchVersionInstalled = 103;
                    Settings.Default.IsPatch29Installed = false;
                    PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
                }
                else
                {
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP29_NAME, "https://dl.dropboxusercontent.com/s/ej1mdbuv4xi53ln/Patch_2.22v29.7z");

                    PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Selected;
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                    Settings.Default.PatchVersionInstalled = 29;
                    Settings.Default.IsPatch29Downloaded = true;
                    Settings.Default.IsPatch29Installed = true;

                    Settings.Default.IsPatch106Installed = false;
                    Settings.Default.IsPatch30Installed = false;
                    Settings.Default.IsPatch31Installed = false;


                    if (Settings.Default.IsPatch30Downloaded)
                        PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;
                    else
                        PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Download;


                    if (Settings.Default.IsPatch31Downloaded)
                        PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;
                    else
                        PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Download;
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;

            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;

            Settings.Default.Save();
        }

        private async void PiBVersion222_5_Click(object sender, EventArgs e)
        {
            PatchModDetectionHelper.DeletePatch106();
            PatchModDetectionHelper.DeletePatch222Files();

            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;

            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch30Downloaded)
            {
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP30_NAME, "https://dl.dropboxusercontent.com/s/ie90sxlbx0mpm8s/Patch_2.22v30.7z");

                Settings.Default.PatchVersionInstalled = 30;
                PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Selected;
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                Settings.Default.IsPatch30Downloaded = true;
                Settings.Default.IsPatch30Installed = true;

                Settings.Default.IsPatch106Installed = false;
                Settings.Default.IsPatch29Installed = false;
                Settings.Default.IsPatch31Installed = false;


                if (Settings.Default.IsPatch29Downloaded)
                    PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
                else
                    PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;


                if (Settings.Default.IsPatch31Downloaded)
                    PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;
                else
                    PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Download;
            }
            else
            {
                if (Settings.Default.IsPatch30Installed)
                {
                    Settings.Default.PatchVersionInstalled = 103;
                    Settings.Default.IsPatch30Installed = false;
                    PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;
                }
                else
                {
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP30_NAME, "https://dl.dropboxusercontent.com/s/ie90sxlbx0mpm8s/Patch_2.22v30.7z");

                    PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Selected;
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                    Settings.Default.PatchVersionInstalled = 30;
                    Settings.Default.IsPatch30Downloaded = true;
                    Settings.Default.IsPatch30Installed = true;

                    Settings.Default.IsPatch106Installed = false;
                    Settings.Default.IsPatch29Installed = false;
                    Settings.Default.IsPatch31Installed = false;


                    if (Settings.Default.IsPatch29Downloaded)
                        PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
                    else
                        PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;


                    if (Settings.Default.IsPatch31Downloaded)
                        PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;
                    else
                        PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Download;
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;

            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;

            Settings.Default.Save();
        }

        private async void PiBVersion222_6_Click(object sender, EventArgs e)
        {
            PatchModDetectionHelper.DeletePatch106();
            PatchModDetectionHelper.DeletePatch222Files();

            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;

            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch31Downloaded)
            {
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/ey7222uixxpj1oi/Patch_2.22v31.7z");

                PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Selected;
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                Settings.Default.PatchVersionInstalled = 31;
                Settings.Default.IsPatch31Downloaded = true;
                Settings.Default.IsPatch31Installed = true;

                Settings.Default.IsPatch106Installed = false;
                Settings.Default.IsPatch29Installed = false;
                Settings.Default.IsPatch30Installed = false;

                if (Settings.Default.IsPatch29Downloaded)
                    PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
                else
                    PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;

                if (Settings.Default.IsPatch30Downloaded)
                    PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;
                else
                    PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Download;
            }
            else
            {
                if (Settings.Default.IsPatch31Installed)
                {
                    Settings.Default.PatchVersionInstalled = 103;
                    Settings.Default.IsPatch31Installed = false;
                    PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31;
                }
                else
                {
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/ey7222uixxpj1oi/Patch_2.22v31.7z");

                    PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Selected;
                    PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106;

                    Settings.Default.PatchVersionInstalled = 31;
                    Settings.Default.IsPatch31Downloaded = true;
                    Settings.Default.IsPatch31Installed = true;

                    Settings.Default.IsPatch106Installed = false;
                    Settings.Default.IsPatch29Installed = false;
                    Settings.Default.IsPatch30Installed = false;


                    if (Settings.Default.IsPatch29Downloaded)
                        PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29;
                    else
                        PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;


                    if (Settings.Default.IsPatch30Downloaded)
                        PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30;
                    else
                        PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Download;
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;

            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;

            Settings.Default.Save();
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

        #region Update

        public async void CheckForUpdates(bool manual)
        {
            if (manual)
            {
                if (File.Exists(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) && MD5Tools.CalculateMD5(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) == ConstStrings.C_UPDATEMD5_HASH && !Settings.Default.UseBetaChannel)
                {
                    Settings.Default.PatchVersionInstalled = ConstStrings.C_UPDATE_VERSION;
                    Settings.Default.Save();
                    BtnLaunch.Enabled = true;
                    BtnLaunch.Text = "PLAY GAME";
                    LblFileName.Hide();
                    PiBArrow.Enabled = true;

                    if (Settings.Default.IsPatchModsShown)
                    {
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                    }
                    else
                    {
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
                    }
                }
                else if (MD5Tools.CalculateMD5(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) == "404" && !Settings.Default.SelectedOlderPatch)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    PatchModDetectionHelper.DeletePatch106();
                    await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/ey7222uixxpj1oi/Patch_2.22v31.7z");
                }
                else if (XMLFileHelper.GetXMLFileVersion(false) != ConstStrings.C_UPDATE_VERSION && !Settings.Default.SelectedOlderPatch)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    PatchModDetectionHelper.DeletePatch106();
                    await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/ey7222uixxpj1oi/Patch_2.22v31.7z");
                }
                else if (XMLFileHelper.GetXMLFileVersion(true) > Settings.Default.BetaChannelVersion)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    PatchModDetectionHelper.DeletePatch106();
                    await UpdateRoutineBeta();
                }
            }
            else
            {
                var timer = new PeriodicTimer(TimeSpan.FromSeconds(600));

                while (await timer.WaitForNextTickAsync())
                {
                    XMLFileHelper.GetXMLFileVersion(false);

                    if (File.Exists(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) && MD5Tools.CalculateMD5(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) == ConstStrings.C_UPDATEMD5_HASH)
                    {
                        Settings.Default.PatchVersionInstalled = ConstStrings.C_UPDATE_VERSION;
                        Settings.Default.Save();
                    }
                    else if (MD5Tools.CalculateMD5(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) != ConstStrings.C_UPDATEMD5_HASH && MD5Tools.CalculateMD5(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) != "404" && !Settings.Default.SelectedOlderPatch)
                    {
                        LblFileName.Show();
                        LblFileName.Text = "Installed Patch 2.22 is old or damaged and will be reaquired...";
                        Settings.Default.PatchVersionInstalled = 103;
                        Settings.Default.Save();

                        PatchModDetectionHelper.DeletePatch222Files();
                        PatchModDetectionHelper.DeletePatch106();
                        await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/ey7222uixxpj1oi/Patch_2.22v31.7z");
                    }
                }
            }
        }

        public async Task UpdateRoutine(string ZIPFileName, string DownloadUrl)
        {
            PiBArrow.Enabled = false;

            if (Settings.Default.IsPatchModsShown)
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
            }
            else
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;
            }

            PBarActualFile.Show();

            LblBytes.Show();
            LblDownloadSpeed.Show();
            LblFileName.Show();

            BtnInstall.Hide();
            BtnAdvanced.Hide();
            BtnLaunch.Show();

            LblFileName.Text = "Preparing Update...";

            BtnLaunch.Enabled = false;

            Task download = DownloadUpdate(ZIPFileName, DownloadUrl);
            await download;

            Task extract = ExtractUpdate(ZIPFileName);
            await extract;

            PBarActualFile.Hide();

            LblBytes.Hide();
            LblDownloadSpeed.Hide();
            LblFileName.Hide();

            BtnInstall.Hide();
            BtnAdvanced.Show();
            BtnLaunch.Show();

            BtnLaunch.Text = "LAUNCH GAME";

            BtnLaunch.Enabled = true;
            PiBArrow.Enabled = true;

            if (Settings.Default.IsPatchModsShown)
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                LblModExplanation.Show();
            }
            else
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
                LblModExplanation.Hide();
            }
        }

        public async Task UpdateRoutineBeta()
        {
            int XmlVersion = XMLFileHelper.GetXMLFileVersion(true);

            PiBArrow.Enabled = false;

            if (Settings.Default.IsPatchModsShown)
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
            }
            else
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;
            }

            PBarActualFile.Show();

            LblBytes.Show();
            LblDownloadSpeed.Show();
            LblFileName.Show();

            BtnInstall.Hide();
            BtnAdvanced.Hide();
            BtnLaunch.Show();

            BtnLaunch.Enabled = false;

            LblFileName.Text = "Preparing Beta Update...";

            if (XmlVersion > Settings.Default.BetaChannelVersion && Settings.Default.BetaChannelVersion > 0 && Directory.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + Settings.Default.BetaChannelVersion.ToString())))
                Directory.Delete(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + Settings.Default.BetaChannelVersion.ToString()), true);

            else if (Settings.Default.BetaChannelVersion == 0)
            {
                var Directories = Directory.GetDirectories(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME), "Beta*");

                foreach (var directory in Directories)
                {
                    Directory.Delete(directory, true);
                }
            }

            Task download = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAIN_ASSET_FILE), "https://dl.dropboxusercontent.com/s/zmze7c5asdlq44u/asset.dat");
            await download;

            Task download2 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_TEXTURES_PATCH_V30_FILE), "https://dl.dropboxusercontent.com/s/ok5a5507fpwmge3/_patch222textures.big");
            await download2;

            Task download3 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAIN_PATCH_V30_FILE), "https://dl.dropboxusercontent.com/s/gi431h0gnqgjfh3/_patch222.big");
            await download3;

            Task download4 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_BASES_PATCH_V30_FILE), "https://dl.dropboxusercontent.com/s/rlshiuuiaalsu1t/_patch222bases.big");
            await download4;

            Task download5 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAPS_PATCH_V30_FILE), "https://dl.dropboxusercontent.com/s/b4ubdfi8uuk181m/_patch222maps.big");
            await download5;

            Task download6 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_LIBRARIES_PATCH_V30_FILE), "https://dl.dropboxusercontent.com/s/9027uabrxkx7z85/_patch222libraries.big");
            await download6;

            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAIN_ASSET_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE), true);
            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_TEXTURES_PATCH_V30_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_TEXTURES_PATCH_V30_FILE), true);
            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAIN_PATCH_V30_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_PATCH_V30_FILE), true);
            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_BASES_PATCH_V30_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_BASES_PATCH_V30_FILE), true);
            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_LIBRARIES_PATCH_V30_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_LIBRARIES_PATCH_V30_FILE), true);
            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAPS_PATCH_V30_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAPS_PATCH_V30_FILE), true);

            PBarActualFile.Hide();

            LblBytes.Hide();
            LblDownloadSpeed.Hide();
            LblFileName.Hide();

            BtnInstall.Hide();
            BtnAdvanced.Show();
            BtnLaunch.Show();

            BtnLaunch.Enabled = true;

            Settings.Default.BetaChannelVersion = XMLFileHelper.GetXMLFileVersion(true);
            Settings.Default.PatchVersionInstalled = ConstStrings.C_UPDATE_VERSION + 1;
            Settings.Default.IsPatch30Installed = false;
            Settings.Default.IsPatch106Installed = false;
            Settings.Default.Save();

            if (Settings.Default.IsPatchModsShown)
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;
                LblModExplanation.Show();
            }
            else
            {
                PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
                LblModExplanation.Hide();
            }

            PiBArrow.Enabled = true;
            BtnLaunch.Text = "LAUNCH GAME";
        }

        public async Task DownloadUpdate(string ZIPFileName, string DownloadUrl)
        {
            try
            {
                SetPBarFiles(0);
                SetPBarFilesMax(100);

                var downloadOpt = new DownloadConfiguration()
                {
                    ChunkCount = 1,
                    ParallelDownload = false,
                    ReserveStorageSpaceBeforeStartingDownload = true,
                    BufferBlockSize = 8000,
                    MaximumBytesPerSecond = 9223372036854775800,
                    ClearPackageOnCompletionWithFailure = true
                };

                var downloader = new DownloadService(downloadOpt);

                downloader.DownloadStarted += OnDownloadStarted;
                downloader.DownloadProgressChanged += OnDownloadProgressChanged;
                downloader.DownloadFileCompleted += OnDownloadFileCompleted;

                if (!File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ZIPFileName)))
                {
                    await downloader.DownloadFileTaskAsync(DownloadUrl, Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ZIPFileName));
                }
            }
            catch (Exception e)
            {
                using StreamWriter file = new("Error.log", append: true);
                await file.WriteLineAsync(e.Message);
            }
        }

        public async Task ExtractUpdate(string ZIPFileName)
        {
            Invoke((MethodInvoker)(() => LblBytes.Hide()));
            Invoke((MethodInvoker)(() => LblDownloadSpeed.Hide()));

            Invoke((MethodInvoker)(() => LblFileName.Text = "Copy files and apply patch..."));

            var progressHandler = new Progress<ProgressHelper>(progress =>
            {
                SetPBarFiles(progress.Count);
                SetPBarFilesMax(progress.Max);
                SetTextFileName(progress.Filename!);
                SetTextDlSpeed(string.Concat(progress.Count, "/", progress.Max));
            });

            ZIPFileHelper _ZIPFileHelper = new();
            await _ZIPFileHelper.ExtractArchive(Path.Combine(ConstStrings.C_PATCHFOLDER_NAME, ZIPFileName), Settings.Default.GameInstallPath, progressHandler)!;

            FinishingGameUpdate();
        }

        private void FinishingGameUpdate()
        {
            Invoke((MethodInvoker)(() => PBarActualFile.Hide()));
            Invoke((MethodInvoker)(() => LblBytes.Hide()));
            Invoke((MethodInvoker)(() => LblDownloadSpeed.Hide()));
            Invoke((MethodInvoker)(() => LblFileName.Hide()));
            Invoke((MethodInvoker)(() => BtnLaunch.Enabled = true));

            Settings.Default.PatchVersionInstalled = ConstStrings.C_UPDATE_VERSION;
            Settings.Default.Save();

            Invoke((MethodInvoker)(() => BtnLaunch.Text = "PLAY GAME"));

            //if (!Directory.Exists(RegistryFunctions.ReadStartMenuFolder()))
            //{
            //    Directory.CreateDirectory(RegistryFunctions.ReadStartMenuFolder()!);
            //
            //    object shDesktop = "Desktop";
            //    WshShell shell = new();
            //    string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\The Battle for Middle-earth (tm).lnk";
            //    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            //    shortcut.Description = "Play The Battle for Middle-earth (tm)";
            //    shortcut.Hotkey = "Ctrl+Shift+N";
            //    shortcut.TargetPath = Path.Combine(Properties.Settings.Default.GameInstallPath, @"\lotrbfme.exe");
            //    shortcut.Save();
            //}
        }

        #endregion

        #region GameInstall

        public async Task InstallRoutine()
        {
            PBarActualFile.Show();

            LblBytes.Show();
            LblDownloadSpeed.Show();
            LblFileName.Show();

            BtnInstall.Hide();
            BtnAdvanced.Hide();
            BtnLaunch.Show();

            LblFileName.Text = "Preparing Setup...";

            BtnLaunch.Enabled = false;

            PiBArrow.Enabled = false;
            PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;

            try
            {
                RegistryService.WriteRegKeysInstallation(Settings.Default.GameInstallPath);

                if (!Directory.Exists(Settings.Default.GameInstallPath))
                {
                    Directory.CreateDirectory(Settings.Default.GameInstallPath);
                }

                await DownloadGame();

                await ExtractGame();

                if (Settings.Default.UseBetaChannel == true)
                {
                    await UpdateRoutineBeta();
                }
                else
                {
                    if (Settings.Default.IsPatch31Downloaded == false)
                    {
                        await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/ey7222uixxpj1oi/Patch_2.22v31.7z");
                    }
                }

                BtnOptions.Show();
                PiBArrow.Enabled = true;

                if (Settings.Default.IsPatchModsShown)
                {
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft;

                }
                else
                {
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowRight;
                }

                Settings.Default.IsGameInstalled = true;
                Settings.Default.IsPatch31Downloaded = true;
                Settings.Default.IsPatch31Installed = true;
                Settings.Default.Save();
                PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Selected;
            }
            catch (Exception e)
            {
                using StreamWriter file = new("Error.log", append: true);
                await file.WriteLineAsync(e.Message);
            }

            LblBytes.Hide();
            LblDownloadSpeed.Hide();
            LblFileName.Hide();

            BtnInstall.Hide();
            BtnLaunch.Show();

            PBarActualFile.Hide();

            BtnLaunch.Enabled = true;
            BtnLaunch.Text = "LAUNCH GAME";
        }

        public async Task DownloadGame()
        {
            try
            {
                var downloadOpt = new DownloadConfiguration()
                {
                    ChunkCount = 1,
                    ParallelDownload = false,
                    ReserveStorageSpaceBeforeStartingDownload = true,
                    BufferBlockSize = 8000,
                    MaximumBytesPerSecond = 9223372036854775800,
                    ClearPackageOnCompletionWithFailure = true
                };

                var downloader = new DownloadService(downloadOpt);

                downloader.DownloadStarted += OnDownloadStarted;
                downloader.DownloadProgressChanged += OnDownloadProgressChanged;
                downloader.DownloadFileCompleted += OnDownloadFileCompleted;

                if (!File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, "BFME1.7z")))
                {
                    await downloader.DownloadFileTaskAsync(@"https://dl.dropboxusercontent.com/s/9sn0e8w8w834ywi/BFME1.7z", Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, "BFME1.7z"));
                }

                if (!File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, "LangPack_EN.7z")))
                {
                    await downloader.DownloadFileTaskAsync(@"https://dl.dropboxusercontent.com/s/ek7fuypqh8oysvn/LangPack_EN.7z", Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, "LangPack_EN.7z"));
                }
            }
            catch (Exception e)
            {
                using StreamWriter file = new("Error.log", append: true);
                await file.WriteLineAsync(e.Message);
            }
        }

        public async Task ExtractGame()
        {
            try
            {
                SetPBarFilesMax(100);

                var progressHandler = new Progress<ProgressHelper>(progress =>
                {
                    SetPBarFiles(progress.Count);
                    SetPBarFilesMax(progress.Max);
                    SetTextFileName(progress.Filename!);
                    SetTextDlSpeed(string.Concat(progress.Count, "/", progress.Max));
                });

                var archiveFileNames = new List<string>()
                {
                    "BFME1.7z",
                    "LangPack_EN.7z"
                };

                for (int i = 0; i < archiveFileNames.Count; i++)
                {
                    SetTextPercentages($"Extracting {i + 1}/{archiveFileNames.Count}: {archiveFileNames[i]}");
                    ZIPFileHelper _ZIPFileHelper = new();
                    await _ZIPFileHelper.ExtractArchive(Path.Combine(ConstStrings.C_DOWNLOADFOLDER_NAME, archiveFileNames[i]), Settings.Default.GameInstallPath, progressHandler)!;
                }
            }
            catch (Exception e)
            {
                using StreamWriter file = new("Error.log", append: true);
                await file.WriteLineAsync(e.Message);
            }
        }

        private void OnDownloadStarted(object sender, DownloadStartedEventArgs e)
        {
            SetPBarFiles(0);
            SetTextFileName("Downloading: " + Path.GetFileName(e.FileName));
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            SetPBarFiles((int)e.ProgressPercentage);
            SetTextDlSpeed("@ " + Math.Round(e.AverageBytesPerSecondSpeed / 1024000).ToString() + " MB/s");
            SetTextPercentages(Math.Round(e.ProgressPercentage).ToString() + " %");
        }

        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            SetTextFileName("Working...");

            if (e.Error != null)
            {
                if (PBarActualFile is null)
                {
                    SetTextFileName(e.Error.Message);
                }
            }
            else
            {
                SetTextFileName("Configuring...");
                SetPBarFiles(100);
            }
        }
        #endregion

        #region Delegates
        delegate void SetTextDLSpeedCallback(string text);
        private void SetTextDlSpeed(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (LblDownloadSpeed.InvokeRequired)
            {
                SetTextDLSpeedCallback d = new(SetTextDlSpeed);
                Invoke(d, new object[] { text });
            }
            else
            {
                LblDownloadSpeed.Text = text;
            }
        }

        delegate void SetTextFileNameCallback(string text);
        private void SetTextFileName(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (LblFileName.InvokeRequired)
            {
                SetTextFileNameCallback d = new(SetTextFileName);
                Invoke(d, new object[] { text });
            }
            else
            {
                LblFileName.Text = text;
            }
        }

        delegate void SetTextPercentagesCallback(string text);
        public void SetTextPercentages(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (LblBytes.InvokeRequired)
            {
                SetTextFileNameCallback d = new(SetTextPercentages);
                Invoke(d, new object[] { text });
            }
            else
            {
                LblBytes.Text = text;
            }
        }

        delegate void SetPBarFilesCallback(int value);
        public void SetPBarFiles(int value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (PBarActualFile.InvokeRequired)
            {
                SetPBarFilesCallback d = new(SetPBarFiles);
                Invoke(d, new object[] { value });
            }
            else
            {
                PBarActualFile.Value = value;
            }
        }

        delegate void SetPBarFilesMaxCallback(int value);
        private void SetPBarFilesMax(int value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (PBarActualFile.InvokeRequired)
            {
                SetPBarFilesMaxCallback d = new(SetPBarFilesMax);
                Invoke(d, new object[] { value });
            }
            else
            {
                PBarActualFile.Maximum = value;
            }
        }
        #endregion

        private void TmrPatchNotes_Tick(object sender, EventArgs e)
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
            //TODO: Add Twitch integration to show Stream in Webview Object when gone live.
            //TwitchHelper.IsOnline(C_TWITCHCHANNEL_NAME);

            if (Settings.Default.PlayBackgroundMusic)
            {
                _soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            }

            if (ProgramState.CurrentProgramState == ProgramState.ProgramStates.repair)
            {
                
            }

            if (Settings.Default.GameInstallPath == "" || !File.Exists(Path.Combine(Settings.Default.GameInstallPath!, ConstStrings.C_MAIN_GAME_FILE)))
            {
                Settings.Default.IsGameInstalled = false;

                PiBArrow.Enabled = false;

                if (Settings.Default.IsPatchModsShown)
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
                else
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;

                BtnInstall.Show();

                BtnLaunch.Hide();
                BtnAdvanced.Hide();
                BtnOptions.Hide();
            }
            else if (XMLFileHelper.GetXMLFileVersion(false) != ConstStrings.C_UPDATE_VERSION && !Settings.Default.SelectedOlderPatch)
            {
                LblFileName.Show();
                LblFileName.Text = "Preparing Update...";
                BtnLaunch.Enabled = false;
                PiBArrow.Enabled = false;

                Settings.Default.BetaChannelVersion = 0;
                Settings.Default.UseBetaChannel = false;
                Settings.Default.Save();

                if (Settings.Default.IsPatchModsShown)
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
                else
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;


                CheckForUpdates(true);
            }
            else if (Settings.Default.FirstTimeUse && Settings.Default.IsGameInstalled)
            {
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                PiBArrow.Enabled = false;

                if (Settings.Default.IsPatchModsShown)
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
                else
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                PatchModDetectionHelper.DeletePatch106();
                PatchModDetectionHelper.DeletePatch222Files();

                await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/ey7222uixxpj1oi/Patch_2.22v31.7z");

                Settings.Default.IsPatch31Downloaded = true;
                Settings.Default.IsPatch31Installed = true;

                Settings.Default.IsPatch106Installed = false;
                Settings.Default.FirstTimeUse = false;

                PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Selected;
            }
            else if (Settings.Default.IsGameInstalled && MD5Tools.CalculateMD5(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) != ConstStrings.C_UPDATEMD5_HASH && !Settings.Default.UseBetaChannel && !Settings.Default.SelectedOlderPatch)
            {
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                PiBArrow.Enabled = false;

                if (Settings.Default.IsPatchModsShown)
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
                else
                    PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                PatchModDetectionHelper.DeletePatch106();
                PatchModDetectionHelper.DeletePatch222Files();

                await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/ey7222uixxpj1oi/Patch_2.22v31.7z");

                Settings.Default.IsPatch31Downloaded = true;
                Settings.Default.IsPatch31Installed = true;

                Settings.Default.IsPatch106Installed = false;

                PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Selected;
            }
            else
            {
                PiBArrow.Enabled = true;
                BtnLaunch.Enabled = true;
                BtnLaunch.Text = "PLAY GAME";

                CheckForUpdates(false);
            }

            if (Settings.Default.UseBetaChannel)
            {
                PiBVersion103.Hide();
                PiBVersion106.Hide();
                PiBVersion222_4.Hide();
                PiBVersion222_5.Hide();
                PiBVersion222_6.Hide();

                LblModExplanation.Text = "Beta-Channel is activated! Deactivate in \"Options\" first and restart the Launcher to see Patches.";

                if (XMLFileHelper.GetXMLFileVersion(true) > Settings.Default.BetaChannelVersion)
                {
                    LblFileName.Show();
                    LblFileName.Text = "Preparing Beta Update...";
                    BtnLaunch.Enabled = false;
                    PiBArrow.Enabled = false;

                    if (Settings.Default.IsPatchModsShown)
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowLeft_Disabled;
                    else
                        PiBArrow.Image = Helper.Properties.Resources.btnArrowRight_Disabled;

                    CheckForUpdates(true);
                }
            }

            if (!Settings.Default.IsPatch106Downloaded)
                PiBVersion106.Image = Helper.Properties.Resources.BtnPatchSelection_106_Download;

            if (!Settings.Default.IsPatch29Downloaded)
                PiBVersion222_4.Image = Helper.Properties.Resources.BtnPatchSelection_222V29_Download;

            if (!Settings.Default.IsPatch30Downloaded)
                PiBVersion222_5.Image = Helper.Properties.Resources.BtnPatchSelection_222V30_Download;

            if (!Settings.Default.IsPatch31Downloaded)
                PiBVersion222_6.Image = Helper.Properties.Resources.BtnPatchSelection_222V31_Download;

            if (ProgramState.CurrentProgramState == ProgramState.ProgramStates.install)
            {
                BtnInstall.PerformClick();
            }

            Settings.Default.Save();
        }

        private void BFME1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Default.IsPatch106Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
                Settings.Default.Save();
            }

            if (Settings.Default.IsPatch29Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
                Settings.Default.Save();
            }

            if (Settings.Default.IsPatch30Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
                Settings.Default.Save();
            }

            if (Settings.Default.IsPatch31Installed)
            {
                Settings.Default.SelectedOlderPatch = false;
                Settings.Default.Save();
            }
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

        private void MenuItemLaunchGame_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            Process _process = new();
            _process.StartInfo.FileName = Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_GAME_FILE);

            // Start game windowed
            if (Settings.Default.StartGameWindowed)
            {
                _process.StartInfo.Arguments = "-win";
            }

            _process.StartInfo.WorkingDirectory = Settings.Default.GameInstallPath;
            _process.Start();

            BtnLaunch.Enabled = false;
            BtnLaunch.Text = "GAME RUNNING";

            BtnAdvanced.Hide();

            _process.WaitForExitAsync();
            _process.Exited += GameisClosed;
        }

        private void CloseTheLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}