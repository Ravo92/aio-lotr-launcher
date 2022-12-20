using Downloader;
using Helper;
using PatchLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = System.Drawing.Color;

namespace PatchLauncher
{
    public partial class BFME1 : Form
    {
        int iconNumber = Settings.Default.BackgroundMusicIcon;
        SoundPlayerHelper _soundPlayerHelper = new();

        System.Timers.Timer _ButtonFlashYellow = new();
        System.Timers.Timer _ButtonFlashNormal = new();

        public BFME1()
        {
            #region logic

            InitializeComponent();

            SysTray.ContextMenuStrip = NotifyContextMenu;

            string configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            if (!File.Exists(configPath))
            {
                Settings.Default.Upgrade();
                Settings.Default.Reload();
                Settings.Default.Save();
            }

            if (!Directory.Exists(ConstStrings.GameAppdataFolderPath()))
                Directory.CreateDirectory(ConstStrings.GameAppdataFolderPath());

            if (!File.Exists(Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME)))
                File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_OPTIONSINI_FILENAME), Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME));

            XMLFileHelper.GetXMLFileData();

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

            PiBVersion222_1.Show();
            PiBVersion222_2.Show();
            PiBVersion222_3.Show();
            PiBVersion222_4.Show();
            PiBVersion222_5.Hide();
            PiBVersion222_6.Hide();

            PiBMod_1.Hide();
            PiBMod_2.Hide();
            PiBMod_3.Hide();
            PiBMod_4.Hide();

            LblInstalledMods.Hide();
            LblInstalledPatches.Hide();
            LblModExplanation.Hide();

            BtnLaunch.Text = "WORKING...";
            BtnLaunch.Enabled = false;
            BtnAdvanced.Hide();

            // label-Styles
            LblDownloadSpeed.Text = "";
            LblDownloadSpeed.Font = ConstStrings.UseFont("Albertus Nova", 14);
            LblDownloadSpeed.ForeColor = Color.FromArgb(192, 145, 69);
            LblDownloadSpeed.BackColor = Color.Transparent;

            LblFileName.Text = "";
            LblFileName.Font = ConstStrings.UseFont("Albertus Nova", 14);
            LblFileName.ForeColor = Color.FromArgb(192, 145, 69);
            LblFileName.BackColor = Color.Transparent;

            LblBytes.Text = "";
            LblBytes.Font = ConstStrings.UseFont("Albertus Nova", 14);
            LblBytes.ForeColor = Color.FromArgb(192, 145, 69);
            LblBytes.BackColor = Color.Transparent;

            LblPatchNotes.Text = "Loading Patch-Notes...";
            LblPatchNotes.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblPatchNotes.ForeColor = Color.FromArgb(192, 145, 69);
            LblPatchNotes.BackColor = Color.Transparent;
            LblPatchNotes.BorderStyle = BorderStyle.None;

            LblInstalledPatches.Text = "Patches";
            LblInstalledPatches.Font = ConstStrings.UseFont("SachaWynterTight", 24);
            LblInstalledPatches.ForeColor = Color.FromArgb(192, 145, 69);
            LblInstalledPatches.BackColor = Color.Transparent;
            LblInstalledPatches.BorderStyle = BorderStyle.None;
            LblInstalledPatches.OutlineWidth = 6;

            LblInstalledMods.Text = "Mods";
            LblInstalledMods.Font = ConstStrings.UseFont("SachaWynterTight", 24);
            LblInstalledMods.ForeColor = Color.FromArgb(192, 145, 69);
            LblInstalledMods.BackColor = Color.Transparent;
            LblInstalledMods.BorderStyle = BorderStyle.None;
            LblInstalledMods.OutlineWidth = 6;

            LblModExplanation.Text = "Here you can choose which patch you want to play. The active one will get a check-sign.";
            LblModExplanation.Font = ConstStrings.UseFont("Albertus Nova", 20);
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
            BtnLaunch.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);

            BtnOptions.FlatAppearance.BorderSize = 0;
            BtnOptions.FlatStyle = FlatStyle.Flat;
            BtnOptions.BackColor = Color.Transparent;
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnOptions.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnInstall.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);

            BtnAdvanced.FlatAppearance.BorderSize = 0;
            BtnAdvanced.FlatStyle = FlatStyle.Flat;
            BtnAdvanced.BackColor = Color.Transparent;
            BtnAdvanced.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnAdvanced.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnAdvanced.ForeColor = Color.FromArgb(192, 145, 69);

            #endregion

            #region Tooltips
            //Tooltips
            ToolTip.SetToolTip(PiBThemeSwitcher, "Switch between faction music and default theme music");
            #endregion

            #region HUD Elements
            PibHeader.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "header.png"));
            PiBYoutube.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "youtube.png"));
            PiBDiscord.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "discord.png"));
            PiBModDB.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "moddb.png"));
            PiBTwitch.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "twitch.png"));
            PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight.png"));
            PiBVersion103.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_103.png"));
            PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));

            if (Settings.Default.PlayBackgroundMusic)
            {
                PibMute.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "Unmute.png"));
            }
            else
            {
                PibMute.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "Mute.png"));
            }

            if (Settings.Default.IsPatch26Installed)
            {
                PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Selected.png"));
            }
            else
            {
                PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
            }

            if (Settings.Default.IsPatch27Installed)
            {
                PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Selected.png"));
            }
            else
            {
                PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
            }

            if (Settings.Default.IsPatch28Installed)
            {
                PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Selected.png"));
            }
            else
            {
                PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
            }

            if (Settings.Default.IsPatch29Installed)
            {
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Selected.png"));
            }
            else
            {
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
            }

            if (Settings.Default.IsPatch30Installed)
            {
                PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Selected.png"));
            }
            else
            {
                PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
            }

            if (Settings.Default.IsPatch31Installed)
            {
                PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Selected.png"));
            }
            else
            {
                PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Settings.Default.BackgroundMusicIcon == 0)
            {
                PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoDefault.png"));
                BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgDefault.png"));
            }
            else if (Settings.Default.BackgroundMusicIcon == 1)
            {
                PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoGondor.png"));
                BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgGondor.png"));
            }
            else if (Settings.Default.BackgroundMusicIcon == 2)
            {
                PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoRohan.png"));
                BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgRohan.png"));
            }
            else if (Settings.Default.BackgroundMusicIcon == 3)
            {
                PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoIsengard.png"));
                BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgIsengard.png"));
            }
            else if (Settings.Default.BackgroundMusicIcon == 4)
            {
                PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoMordor.png"));
                BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgMordor.png"));
            }
            #endregion
        }

        #region Button Behaviours

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            ProcessStartInfo _processInfo = new()
            {
                WorkingDirectory = Settings.Default.GameInstallPath,
                FileName = Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_GAME_FILE)
            };

            // Start game windowed
            if (Settings.Default.StartGameWindowed)
            {
                _processInfo.Arguments = "-win";
            }

            _ = Process.Start(_processInfo)!;

            Thread.Sleep(1000);

            WindowState = FormWindowState.Minimized;
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
            if (_ButtonFlashYellow.Enabled && _ButtonFlashNormal.Enabled)
            {
                _ButtonFlashYellow.Stop();
                _ButtonFlashNormal.Stop();
            }

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
            InstallPathDialog _install = new();

            DialogResult dr = _install.ShowDialog();
            if (dr == DialogResult.OK)
            {
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnInstall.Hide();
                BtnLaunch.Show();

                LblFileName.Text = "Preparing Setup...";

                BtnLaunch.Enabled = false;
                BtnAdvanced.Hide();

                await InstallRoutine();

                LblBytes.Hide();
                LblDownloadSpeed.Hide();
                LblFileName.Hide();

                BtnInstall.Hide();
                BtnLaunch.Show();

                PBarActualFile.Hide();

                BtnLaunch.Text = "LAUNCH GAME";

                BtnLaunch.Enabled = true;
                BtnAdvanced.Show();
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
                PibMute.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "Mute.png"));
                Settings.Default.PlayBackgroundMusic = false;
                _soundPlayerHelper.StopTheme();
                Settings.Default.Save();
            }
            else
            {
                PibMute.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "Unmute.png"));
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
                        PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoDefault.png"));
                        BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgDefault.png"));

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
                        PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoGondor.png"));
                        BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgGondor.png"));

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
                        PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoRohan.png"));
                        BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgRohan.png"));

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
                        PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoIsengard.png"));
                        BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgIsengard.png"));

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
                        PiBThemeSwitcher.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "IcoMordor.png"));
                        BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgMordor.png"));

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
            PiBVersion222_1.Enabled = false;
            PiBVersion222_2.Enabled = false;
            PiBVersion222_3.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (PatchModDetectionHelper.DetectPatch106())
            {
                PatchModDetectionHelper.DeletePatch106();
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));
            }

            PatchModDetectionHelper.DeletePatch222Files();

            if (Settings.Default.IsPatch26Downloaded)
            {
                Settings.Default.IsPatch26Installed = false;
                PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
            }
            else
            {
                PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
            }

            if (Settings.Default.IsPatch27Downloaded)
            {
                Settings.Default.IsPatch27Installed = false;
                PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
            }
            else
            {
                PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
            }

            if (Settings.Default.IsPatch28Downloaded)
            {
                Settings.Default.IsPatch28Installed = false;
                PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
            }
            else
            {
                PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
            }

            if (Settings.Default.IsPatch29Downloaded)
            {
                Settings.Default.IsPatch29Installed = false;
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
            }
            else
            {
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
            }

            if (Settings.Default.IsPatch30Downloaded)
            {
                Settings.Default.IsPatch30Installed = false;
                PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
            }
            else
            {
                PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
            }

            if (Settings.Default.IsPatch31Downloaded)
            {
                Settings.Default.IsPatch26Installed = false;
                PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
            }
            else
            {
                PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;
            PiBVersion222_1.Enabled = true;
            PiBVersion222_2.Enabled = true;
            PiBVersion222_3.Enabled = true;
            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;
        }

        private async void PiBVersion106_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_1.Enabled = false;
            PiBVersion222_2.Enabled = false;
            PiBVersion222_3.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!PatchModDetectionHelper.DetectPatch106())
            {
                Settings.Default.IsPatch106Installed = true;

                await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106_Selected.png"));

                if (Settings.Default.IsPatch26Downloaded)
                {
                    if (Settings.Default.IsPatch26Installed == true)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                    }
                }
                else
                {
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                }

                if (Settings.Default.IsPatch27Downloaded)
                {
                    if (Settings.Default.IsPatch27Installed == true)
                    {
                        Settings.Default.IsPatch27Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                    }
                }
                else
                {
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                }

                if (Settings.Default.IsPatch28Downloaded)
                {
                    if (Settings.Default.IsPatch28Installed == true)
                    {
                        Settings.Default.IsPatch28Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                    }
                }
                else
                {
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                }

                if (Settings.Default.IsPatch29Downloaded)
                {
                    if (Settings.Default.IsPatch29Installed == true)
                    {
                        Settings.Default.IsPatch29Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                    }
                }
                else
                {
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                }

                if (Settings.Default.IsPatch30Downloaded)
                {
                    if (Settings.Default.IsPatch30Installed == true)
                    {
                        Settings.Default.IsPatch30Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                    }
                }
                else
                {
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                }

                if (Settings.Default.IsPatch31Downloaded)
                {
                    if (Settings.Default.IsPatch31Installed == true)
                    {
                        Settings.Default.IsPatch31Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                    }
                }
                else
                {
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                }
            }
            else
            {
                Settings.Default.IsPatch106Installed = false;
                PatchModDetectionHelper.DeletePatch106();
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));

                if (Settings.Default.IsPatch26Downloaded)
                {
                    if (Settings.Default.IsPatch26Installed == true)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                    }
                }
                else
                {
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                }

                if (Settings.Default.IsPatch27Downloaded)
                {
                    if (Settings.Default.IsPatch27Installed == true)
                    {
                        Settings.Default.IsPatch27Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                    }
                }
                else
                {
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                }

                if (Settings.Default.IsPatch28Downloaded)
                {
                    if (Settings.Default.IsPatch28Installed == true)
                    {
                        Settings.Default.IsPatch28Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                    }
                }
                else
                {
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                }

                if (Settings.Default.IsPatch29Downloaded)
                {
                    if (Settings.Default.IsPatch29Installed == true)
                    {
                        Settings.Default.IsPatch29Installed = false;
                        PatchModDetectionHelper.DeletePatch222Files();
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                    }
                }
                else
                {
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                }
            }

            Settings.Default.Save();

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;
            PiBVersion222_1.Enabled = true;
            PiBVersion222_2.Enabled = true;
            PiBVersion222_3.Enabled = true;
            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;
        }

        private async void PiBVersion222_1_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_1.Enabled = false;
            PiBVersion222_2.Enabled = false;
            PiBVersion222_3.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch26Downloaded)
            {
                LblModExplanation.Hide();
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                await UpdateRoutine(ConstStrings.C_PATCHZIP26_NAME, "https://dl.dropboxusercontent.com/s/mbqfa8n5swxydeo/Patch_2.22v26.7z");

                Settings.Default.IsPatch26Downloaded = true;
                Settings.Default.IsPatch26Installed = true;
                PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Selected.png"));
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106_Selected.png"));

                Settings.Default.IsPatch27Installed = false;
                Settings.Default.IsPatch28Installed = false;
                Settings.Default.IsPatch29Installed = false;
                Settings.Default.IsPatch30Installed = false;
                Settings.Default.IsPatch31Installed = false;

                if (Settings.Default.IsPatch27Downloaded)
                {
                    Settings.Default.IsPatch27Installed = false;
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                }
                else
                {
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                }

                if (Settings.Default.IsPatch28Downloaded)
                {
                    Settings.Default.IsPatch28Installed = false;
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                }
                else
                {
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                }

                if (Settings.Default.IsPatch29Downloaded)
                {
                    Settings.Default.IsPatch29Installed = false;
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                }
                else
                {
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                }

                if (Settings.Default.IsPatch30Downloaded)
                {
                    Settings.Default.IsPatch30Installed = false;
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                }
                else
                {
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                }

                if (Settings.Default.IsPatch31Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                }
                else
                {
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                }

                Settings.Default.Save();

                LblModExplanation.Show();
            }
            else
            {
                if (Settings.Default.IsPatch26Installed)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                }
                else
                {
                    LblModExplanation.Hide();
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                    await UpdateRoutine(ConstStrings.C_PATCHZIP26_NAME, "https://dl.dropboxusercontent.com/s/mbqfa8n5swxydeo/Patch_2.22v26.7z");

                    Settings.Default.IsPatch26Installed = true;
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Selected.png"));
                    PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106_Selected.png"));

                    Settings.Default.IsPatch27Installed = false;
                    Settings.Default.IsPatch28Installed = false;
                    Settings.Default.IsPatch29Installed = false;
                    Settings.Default.IsPatch30Installed = false;
                    Settings.Default.IsPatch31Installed = false;

                    if (Settings.Default.IsPatch27Downloaded)
                    {
                        Settings.Default.IsPatch27Installed = false;
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                    }
                    else
                    {
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                    }

                    if (Settings.Default.IsPatch28Downloaded)
                    {
                        Settings.Default.IsPatch28Installed = false;
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                    }
                    else
                    {
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                    }

                    if (Settings.Default.IsPatch29Downloaded)
                    {
                        Settings.Default.IsPatch29Installed = false;
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                    }
                    else
                    {
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                    }

                    if (Settings.Default.IsPatch30Downloaded)
                    {
                        Settings.Default.IsPatch30Installed = false;
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                    }
                    else
                    {
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                    }

                    if (Settings.Default.IsPatch31Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                    }
                    else
                    {
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                    }

                    Settings.Default.Save();

                    LblModExplanation.Show();
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;
            PiBVersion222_1.Enabled = true;
            PiBVersion222_2.Enabled = true;
            PiBVersion222_3.Enabled = true;
            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;
        }

        private async void PiBVersion222_2_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_1.Enabled = false;
            PiBVersion222_2.Enabled = false;
            PiBVersion222_3.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch27Downloaded)
            {
                LblModExplanation.Hide();
                LblModExplanation.Hide();
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                await UpdateRoutine(ConstStrings.C_PATCHZIP27_NAME, "https://dl.dropboxusercontent.com/s/18q8awyhbddrnl4/Patch_2.22v27.7z");
                PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Selected.png"));
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106_Selected.png"));

                Settings.Default.IsPatch27Downloaded = true;
                Settings.Default.IsPatch27Installed = true;

                Settings.Default.IsPatch26Installed = false;
                Settings.Default.IsPatch28Installed = false;
                Settings.Default.IsPatch29Installed = false;
                Settings.Default.IsPatch30Installed = false;
                Settings.Default.IsPatch31Installed = false;

                if (Settings.Default.IsPatch26Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                }
                else
                {
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                }

                if (Settings.Default.IsPatch28Downloaded)
                {
                    Settings.Default.IsPatch28Installed = false;
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                }
                else
                {
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                }

                if (Settings.Default.IsPatch29Downloaded)
                {
                    Settings.Default.IsPatch29Installed = false;
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                }
                else
                {
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                }

                if (Settings.Default.IsPatch30Downloaded)
                {
                    Settings.Default.IsPatch30Installed = false;
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                }
                else
                {
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                }

                if (Settings.Default.IsPatch31Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                }
                else
                {
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                }

                Settings.Default.Save();

                LblModExplanation.Show();
            }
            else
            {
                if (Settings.Default.IsPatch27Installed)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    Settings.Default.IsPatch27Installed = false;
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                }
                else
                {
                    LblModExplanation.Hide();
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                    await UpdateRoutine(ConstStrings.C_PATCHZIP27_NAME, "https://dl.dropboxusercontent.com/s/18q8awyhbddrnl4/Patch_2.22v27.7z");

                    Settings.Default.IsPatch27Installed = true;
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Selected.png"));
                    PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106_Selected.png"));

                    Settings.Default.IsPatch26Installed = false;
                    Settings.Default.IsPatch28Installed = false;
                    Settings.Default.IsPatch29Installed = false;
                    Settings.Default.IsPatch30Installed = false;
                    Settings.Default.IsPatch31Installed = false;

                    if (Settings.Default.IsPatch26Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                    }
                    else
                    {
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                    }

                    if (Settings.Default.IsPatch28Downloaded)
                    {
                        Settings.Default.IsPatch28Installed = false;
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                    }
                    else
                    {
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                    }

                    if (Settings.Default.IsPatch29Downloaded)
                    {
                        Settings.Default.IsPatch29Installed = false;
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                    }
                    else
                    {
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                    }

                    if (Settings.Default.IsPatch30Downloaded)
                    {
                        Settings.Default.IsPatch30Installed = false;
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                    }
                    else
                    {
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                    }

                    if (Settings.Default.IsPatch31Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                    }
                    else
                    {
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                    }

                    LblModExplanation.Show();
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;
            PiBVersion222_1.Enabled = true;
            PiBVersion222_2.Enabled = true;
            PiBVersion222_3.Enabled = true;
            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;
        }

        private async void PiBVersion222_3_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_1.Enabled = false;
            PiBVersion222_2.Enabled = false;
            PiBVersion222_3.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch28Downloaded)
            {
                LblModExplanation.Hide();
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                await UpdateRoutine(ConstStrings.C_PATCHZIP28_NAME, "https://dl.dropboxusercontent.com/s/s5pkt4zvwk2gnra/Patch_2.22v28.7z");
                Settings.Default.IsPatch28Downloaded = true;
                Settings.Default.IsPatch28Installed = true;
                PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Selected.png"));
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106_Selected.png"));

                Settings.Default.IsPatch26Installed = false;
                Settings.Default.IsPatch27Installed = false;
                Settings.Default.IsPatch29Installed = false;
                Settings.Default.IsPatch30Installed = false;
                Settings.Default.IsPatch31Installed = false;

                if (Settings.Default.IsPatch26Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                }
                else
                {
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                }

                if (Settings.Default.IsPatch27Downloaded)
                {
                    Settings.Default.IsPatch27Installed = false;
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                }
                else
                {
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                }

                if (Settings.Default.IsPatch29Downloaded)
                {
                    Settings.Default.IsPatch29Installed = false;
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                }
                else
                {
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                }

                if (Settings.Default.IsPatch30Downloaded)
                {
                    Settings.Default.IsPatch30Installed = false;
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                }
                else
                {
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                }

                if (Settings.Default.IsPatch31Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                }
                else
                {
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                }

                Settings.Default.Save();

                LblModExplanation.Show();
            }
            else
            {
                if (Settings.Default.IsPatch28Installed)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    Settings.Default.IsPatch28Installed = false;
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                }
                else
                {
                    LblModExplanation.Hide();
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP06_NAME, "https://dl.dropboxusercontent.com/s/0j4u35hetr3if5j/Patch_1.06.7z");

                    await UpdateRoutine(ConstStrings.C_PATCHZIP28_NAME, "https://dl.dropboxusercontent.com/s/s5pkt4zvwk2gnra/Patch_2.22v28.7z");

                    Settings.Default.IsPatch28Installed = true;
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Selected.png"));
                    PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106_Selected.png"));

                    Settings.Default.IsPatch26Installed = false;
                    Settings.Default.IsPatch27Installed = false;
                    Settings.Default.IsPatch29Installed = false;
                    Settings.Default.IsPatch30Installed = false;
                    Settings.Default.IsPatch31Installed = false;

                    if (Settings.Default.IsPatch26Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                    }
                    else
                    {
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                    }

                    if (Settings.Default.IsPatch27Downloaded)
                    {
                        Settings.Default.IsPatch27Installed = false;
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                    }
                    else
                    {
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                    }

                    if (Settings.Default.IsPatch29Downloaded)
                    {
                        Settings.Default.IsPatch29Installed = false;
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                    }
                    else
                    {
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                    }

                    if (Settings.Default.IsPatch30Downloaded)
                    {
                        Settings.Default.IsPatch30Installed = false;
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                    }
                    else
                    {
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                    }

                    if (Settings.Default.IsPatch31Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                    }
                    else
                    {
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                    }

                    LblModExplanation.Show();
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;
            PiBVersion222_1.Enabled = true;
            PiBVersion222_2.Enabled = true;
            PiBVersion222_3.Enabled = true;
            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;
        }

        private async void PiBVersion222_4_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_1.Enabled = false;
            PiBVersion222_2.Enabled = false;
            PiBVersion222_3.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch29Downloaded)
            {
                LblModExplanation.Hide();
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP29_NAME, "https://dl.dropboxusercontent.com/s/ej1mdbuv4xi53ln/Patch_2.22v29.7z");
                Settings.Default.IsPatch29Downloaded = true;
                Settings.Default.IsPatch29Installed = true;
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Selected.png"));

                PatchModDetectionHelper.DeletePatch106();
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));

                Settings.Default.IsPatch26Installed = false;
                Settings.Default.IsPatch27Installed = false;
                Settings.Default.IsPatch28Installed = false;
                Settings.Default.IsPatch30Installed = false;
                Settings.Default.IsPatch31Installed = false;

                if (Settings.Default.IsPatch26Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                }
                else
                {
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                }

                if (Settings.Default.IsPatch27Downloaded)
                {
                    Settings.Default.IsPatch27Installed = false;
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                }
                else
                {
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                }

                if (Settings.Default.IsPatch28Downloaded)
                {
                    Settings.Default.IsPatch28Installed = false;
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                }
                else
                {
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                }

                if (Settings.Default.IsPatch30Downloaded)
                {
                    Settings.Default.IsPatch30Installed = false;
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                }
                else
                {
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                }

                if (Settings.Default.IsPatch31Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                }
                else
                {
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                }

                Settings.Default.Save();

                LblModExplanation.Show();
            }
            else
            {
                if (Settings.Default.IsPatch29Installed)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    Settings.Default.IsPatch29Installed = false;
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                }
                else
                {
                    LblModExplanation.Hide();
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP29_NAME, "https://dl.dropboxusercontent.com/s/ej1mdbuv4xi53ln/Patch_2.22v29.7z");

                    Settings.Default.IsPatch29Installed = true;
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Selected.png"));

                    PatchModDetectionHelper.DeletePatch106();
                    PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));

                    Settings.Default.IsPatch26Installed = false;
                    Settings.Default.IsPatch27Installed = false;
                    Settings.Default.IsPatch28Installed = false;
                    Settings.Default.IsPatch30Installed = false;
                    Settings.Default.IsPatch31Installed = false;

                    if (Settings.Default.IsPatch26Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                    }
                    else
                    {
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                    }

                    if (Settings.Default.IsPatch27Downloaded)
                    {
                        Settings.Default.IsPatch27Installed = false;
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                    }
                    else
                    {
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                    }

                    if (Settings.Default.IsPatch28Downloaded)
                    {
                        Settings.Default.IsPatch28Installed = false;
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                    }
                    else
                    {
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                    }

                    if (Settings.Default.IsPatch30Downloaded)
                    {
                        Settings.Default.IsPatch30Installed = false;
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                    }
                    else
                    {
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                    }

                    if (Settings.Default.IsPatch31Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                    }
                    else
                    {
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                    }

                    LblModExplanation.Show();
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;
            PiBVersion222_1.Enabled = true;
            PiBVersion222_2.Enabled = true;
            PiBVersion222_3.Enabled = true;
            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;
        }

        private async void PiBVersion222_5_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_1.Enabled = false;
            PiBVersion222_2.Enabled = false;
            PiBVersion222_3.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch30Downloaded)
            {
                LblModExplanation.Hide();
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP30_NAME, "https://dl.dropboxusercontent.com/s/mbqfa8n5swxydeo/Patch_2.22v30.7z");
                Settings.Default.IsPatch30Downloaded = true;
                Settings.Default.IsPatch30Installed = true;
                PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Selected.png"));

                PatchModDetectionHelper.DeletePatch106();
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));

                Settings.Default.IsPatch26Installed = false;
                Settings.Default.IsPatch27Installed = false;
                Settings.Default.IsPatch28Installed = false;
                Settings.Default.IsPatch29Installed = false;
                Settings.Default.IsPatch31Installed = false;

                if (Settings.Default.IsPatch26Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                }
                else
                {
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                }

                if (Settings.Default.IsPatch27Downloaded)
                {
                    Settings.Default.IsPatch27Installed = false;
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                }
                else
                {
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                }

                if (Settings.Default.IsPatch28Downloaded)
                {
                    Settings.Default.IsPatch28Installed = false;
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                }
                else
                {
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                }

                if (Settings.Default.IsPatch29Downloaded)
                {
                    Settings.Default.IsPatch29Installed = false;
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                }
                else
                {
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                }

                if (Settings.Default.IsPatch31Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                }
                else
                {
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                }

                Settings.Default.Save();

                LblModExplanation.Show();
            }
            else
            {
                if (Settings.Default.IsPatch30Installed)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    Settings.Default.IsPatch30Installed = false;
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                }
                else
                {
                    LblModExplanation.Hide();
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP30_NAME, "https://dl.dropboxusercontent.com/s/mbqfa8n5swxydeo/Patch_2.22v30.7z");

                    Settings.Default.IsPatch30Installed = true;
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Selected.png"));

                    PatchModDetectionHelper.DeletePatch106();
                    PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));

                    Settings.Default.IsPatch26Installed = false;
                    Settings.Default.IsPatch27Installed = false;
                    Settings.Default.IsPatch28Installed = false;
                    Settings.Default.IsPatch29Installed = false;
                    Settings.Default.IsPatch31Installed = false;

                    if (Settings.Default.IsPatch26Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                    }
                    else
                    {
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                    }

                    if (Settings.Default.IsPatch27Downloaded)
                    {
                        Settings.Default.IsPatch27Installed = false;
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                    }
                    else
                    {
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                    }

                    if (Settings.Default.IsPatch28Downloaded)
                    {
                        Settings.Default.IsPatch28Installed = false;
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                    }
                    else
                    {
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                    }

                    if (Settings.Default.IsPatch29Downloaded)
                    {
                        Settings.Default.IsPatch29Installed = false;
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                    }
                    else
                    {
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                    }

                    if (Settings.Default.IsPatch31Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                    }
                    else
                    {
                        PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
                    }

                    LblModExplanation.Show();
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;
            PiBVersion222_1.Enabled = true;
            PiBVersion222_2.Enabled = true;
            PiBVersion222_3.Enabled = true;
            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;
        }

        private async void PiBVersion222_6_Click(object sender, EventArgs e)
        {
            PiBVersion103.Enabled = false;
            PiBVersion106.Enabled = false;
            PiBVersion222_1.Enabled = false;
            PiBVersion222_2.Enabled = false;
            PiBVersion222_3.Enabled = false;
            PiBVersion222_4.Enabled = false;
            PiBVersion222_5.Enabled = false;
            PiBVersion222_6.Enabled = false;

            if (!Settings.Default.IsPatch31Downloaded)
            {
                LblModExplanation.Hide();
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/mbqfa8n5swxydeo/Patch_2.22v31.7z");
                Settings.Default.IsPatch31Downloaded = true;
                Settings.Default.IsPatch31Installed = true;
                PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Selected.png"));

                PatchModDetectionHelper.DeletePatch106();
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));

                Settings.Default.IsPatch26Installed = false;
                Settings.Default.IsPatch27Installed = false;
                Settings.Default.IsPatch28Installed = false;
                Settings.Default.IsPatch29Installed = false;
                Settings.Default.IsPatch30Installed = false;

                if (Settings.Default.IsPatch26Downloaded)
                {
                    Settings.Default.IsPatch26Installed = false;
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                }
                else
                {
                    PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                }

                if (Settings.Default.IsPatch27Downloaded)
                {
                    Settings.Default.IsPatch27Installed = false;
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                }
                else
                {
                    PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                }

                if (Settings.Default.IsPatch28Downloaded)
                {
                    Settings.Default.IsPatch28Installed = false;
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                }
                else
                {
                    PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                }

                if (Settings.Default.IsPatch29Downloaded)
                {
                    Settings.Default.IsPatch29Installed = false;
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                }
                else
                {
                    PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                }

                if (Settings.Default.IsPatch30Downloaded)
                {
                    Settings.Default.IsPatch30Installed = false;
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                }
                else
                {
                    PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                }

                Settings.Default.Save();

                LblModExplanation.Show();
            }
            else
            {
                if (Settings.Default.IsPatch31Installed)
                {
                    PatchModDetectionHelper.DeletePatch222Files();
                    Settings.Default.IsPatch31Installed = false;
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31.png"));
                }
                else
                {
                    LblModExplanation.Hide();
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    BtnLaunch.Enabled = false;
                    BtnLaunch.Text = "PATCHING...";

                    await UpdateRoutine(ConstStrings.C_PATCHZIP31_NAME, "https://dl.dropboxusercontent.com/s/mbqfa8n5swxydeo/Patch_2.22v31.7z");

                    Settings.Default.IsPatch31Installed = true;
                    PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Selected.png"));

                    PatchModDetectionHelper.DeletePatch106();
                    PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));

                    Settings.Default.IsPatch26Installed = false;
                    Settings.Default.IsPatch27Installed = false;
                    Settings.Default.IsPatch28Installed = false;
                    Settings.Default.IsPatch29Installed = false;
                    Settings.Default.IsPatch30Installed = false;

                    if (Settings.Default.IsPatch26Downloaded)
                    {
                        Settings.Default.IsPatch26Installed = false;
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26.png"));
                    }
                    else
                    {
                        PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
                    }

                    if (Settings.Default.IsPatch27Downloaded)
                    {
                        Settings.Default.IsPatch27Installed = false;
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27.png"));
                    }
                    else
                    {
                        PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
                    }

                    if (Settings.Default.IsPatch28Downloaded)
                    {
                        Settings.Default.IsPatch28Installed = false;
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28.png"));
                    }
                    else
                    {
                        PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
                    }

                    if (Settings.Default.IsPatch29Downloaded)
                    {
                        Settings.Default.IsPatch29Installed = false;
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
                    }
                    else
                    {
                        PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
                    }

                    if (Settings.Default.IsPatch30Downloaded)
                    {
                        Settings.Default.IsPatch30Installed = false;
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30.png"));
                    }
                    else
                    {
                        PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
                    }

                    LblModExplanation.Show();
                }
            }

            PiBVersion103.Enabled = true;
            PiBVersion106.Enabled = true;
            PiBVersion222_1.Enabled = true;
            PiBVersion222_2.Enabled = true;
            PiBVersion222_3.Enabled = true;
            PiBVersion222_4.Enabled = true;
            PiBVersion222_5.Enabled = true;
            PiBVersion222_6.Enabled = true;
        }

        private void PiBMod_1_Click(object sender, EventArgs e)
        {

        }

        private void PiBMod_2_Click(object sender, EventArgs e)
        {

        }

        private void PiBMod_3_Click(object sender, EventArgs e)
        {

        }

        private void PiBMod_4_Click(object sender, EventArgs e)
        {

        }

        private void PiBArrow_Click(object sender, EventArgs e)
        {
            TmrAnimation.Enabled = true;
        }

        #endregion

        #region ToolTip System
        public void Tooltip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Font tooltipFont = ConstStrings.UseFont("Albertus Nova", 14);
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, tooltipFont, Brushes.SandyBrown, new PointF(2, 2));
        }

        public void TooltipPopup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = TextRenderer.MeasureText(ToolTip.GetToolTip(e.AssociatedControl), ConstStrings.UseFont("Albertus Nova", 14));
        }
        #endregion

        #region Update

        public async void CheckForUpdates(bool manual)
        {
            if (manual)
            {
                if (File.Exists(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) && MD5Tools.CalculateMD5(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) == ConstStrings.C_UPDATEMD5_HASH)
                {
                    Settings.Default.PatchVersionInstalled = ConstStrings.C_UPDATE_VERSION;
                    Settings.Default.Save();
                    BtnLaunch.Enabled = true;
                    BtnAdvanced.Show();
                    BtnLaunch.Text = "PLAY GAME";
                    LblFileName.Hide();
                    PiBArrow.Enabled = true;

                    if (Settings.Default.IsPatchModsShown)
                    {
                        PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowLeftpng"));
                    }
                    else
                    {
                        PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight.png"));
                    }
                }
                else if (MD5Tools.CalculateMD5(Path.Combine(Settings.Default.GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE)) == "404" && !Settings.Default.SelectedOlderPatch)
                {
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    await UpdateRoutine(ConstStrings.C_PATCHZIP30_NAME, "TBA");
                }
                else if (XMLFileHelper.GetXMLFileVersion() > ConstStrings.C_UPDATE_VERSION)
                {
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    await UpdateRoutine(ConstStrings.C_PATCHZIP30_NAME, "TBA");
                }
            }
            else
            {
                var timer = new PeriodicTimer(TimeSpan.FromSeconds(600));

                while (await timer.WaitForNextTickAsync())
                {
                    XMLFileHelper.GetXMLFileVersion();

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

                        await UpdateRoutine(ConstStrings.C_PATCHZIP30_NAME, "TBA");
                    }
                }
            }
        }

        public async Task UpdateRoutine(string ZIPFileName, string DownloadUrl)
        {
            PiBArrow.Enabled = false;

            if (Settings.Default.IsPatchModsShown)
            {
                PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowLeft_Disabled.png"));
            }
            else
            {
                PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight_Disabled.png"));
            }

            LblBytes.Show();
            LblDownloadSpeed.Show();
            LblFileName.Show();

            BtnInstall.Hide();
            BtnLaunch.Show();

            LblFileName.Text = "Preparing Update...";
            LblModExplanation.Hide();

            BtnLaunch.Enabled = false;
            BtnAdvanced.Hide();

            PBarActualFile.Show();

            Task download = DownloadUpdate(ZIPFileName, DownloadUrl);
            await download;

            Task extract = ExtractUpdate(ZIPFileName);
            await extract;

            LblBytes.Hide();
            LblDownloadSpeed.Hide();
            LblFileName.Hide();

            BtnInstall.Hide();
            BtnLaunch.Show();

            PBarActualFile.Hide();

            BtnLaunch.Text = "LAUNCH GAME";

            BtnLaunch.Enabled = true;
            BtnAdvanced.Show();

            PiBArrow.Enabled = true;

            if (Settings.Default.IsPatchModsShown)
            {
                PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowLeft.png"));
                LblModExplanation.Show();
            }
            else
            {
                PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight.png"));
                LblModExplanation.Hide();
            }
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
            Invoke((MethodInvoker)(() => BtnAdvanced.Show()));

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
            try
            {
                PiBArrow.Enabled = false;
                PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight_Disabled.png"));

                RegistryService.WriteRegKeysInstallation(Settings.Default.GameInstallPath);

                if (!Directory.Exists(Settings.Default.GameInstallPath))
                {
                    Directory.CreateDirectory(Settings.Default.GameInstallPath);
                }

                await DownloadGame();

                await ExtractGame();

                if (Settings.Default.IsPatch29Downloaded == false)
                {
                    await UpdateRoutine(ConstStrings.C_PATCHZIP29_NAME, "https://dl.dropboxusercontent.com/s/ej1mdbuv4xi53ln/Patch_2.22v29.7z");
                }

                BtnOptions.Show();

                _ButtonFlashYellow.Start();
                _ButtonFlashYellow.Interval = 1000;
                _ButtonFlashYellow.Elapsed += new ElapsedEventHandler(TimerFlashYellow);

                _ButtonFlashNormal.Start();
                _ButtonFlashNormal.Interval = 2000;
                _ButtonFlashNormal.Elapsed += new ElapsedEventHandler(TimerFlashNormal);

                PiBArrow.Enabled = true;

                if (Settings.Default.IsPatchModsShown)
                {
                    PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowLeft.png"));
                    
                }
                else
                {
                    PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight.png"));
                }

                Settings.Default.IsGameInstalled = true;
                Settings.Default.IsPatch29Downloaded = true;
                Settings.Default.IsPatch29Installed = true;
                Settings.Default.Save();
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Selected.png"));
            }
            catch (Exception e)
            {
                using StreamWriter file = new("Error.log", append: true);
                await file.WriteLineAsync(e.Message);
            }
        }

        private void TimerFlashYellow(object source, ElapsedEventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
        }

        private void TimerFlashNormal(object source, ElapsedEventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
        }

        public async Task DownloadGame()
        {
            try
            {
                PBarActualFile.Show();

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

                if (!File.Exists(Application.StartupPath + "\\Download\\BFME1.7z"))
                {
                    await downloader.DownloadFileTaskAsync(@"https://dl.dropboxusercontent.com/s/9sn0e8w8w834ywi/BFME1.7z", Application.StartupPath + "\\Download\\BFME1.7z");
                }

                if (!File.Exists(Application.StartupPath + "\\Download\\LangPack_EN.7z"))
                {
                    await downloader.DownloadFileTaskAsync(@"https://dl.dropboxusercontent.com/s/ek7fuypqh8oysvn/LangPack_EN.7z", Application.StartupPath + "\\Download\\LangPack_EN.7z");
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
                Invoke((MethodInvoker)(() => PBarActualFile.Show()));
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
                    await _ZIPFileHelper.ExtractArchive(Path.Combine(@"Download", archiveFileNames[i]), Settings.Default.GameInstallPath, progressHandler)!;
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
            Wv2Patchnotes.Show();
            PiBArrow.BackColor = Color.FromArgb(24, 24, 24);
            PiBArrow.Show();
            PibLoadingRing.Hide();
            PibLoadingBorder.Hide();
            LblPatchNotes.Hide();
            PnlPlaceholder.Show();
        }

        private void TmrAnimation_Tick(object sender, EventArgs e)
        {
            int _startLeft = 12;  // start position of the panel
            int _endLeft = 1300;      // end position of the panel
            int _endLeftArrow = 1210;
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
                PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowLeft.png"));
                PiBArrow.BackColor = Color.Transparent;
                PnlPlaceholder.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "borderRectangleModPanel.png"));
                PnlPlaceholder.BackColor = Color.Transparent;
                LblInstalledPatches.BackColor = Color.Transparent;
                LblModExplanation.BackColor = Color.Transparent;
                LblModExplanation.Show();
                LblInstalledMods.Show();
                LblInstalledPatches.Show();

                Settings.Default.IsPatchModsShown = true;
                Settings.Default.Save();

            }
            else
            {
                PnlPlaceholder.BackColor = Color.FromArgb(24, 24, 24);
                PnlPlaceholder.BackgroundImage = null;
                PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight.png"));
                PiBArrow.BackColor = Color.FromArgb(24, 24, 24);
                LblModExplanation.Hide();
                LblInstalledMods.Hide();
                LblInstalledPatches.Hide();

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

            if (Settings.Default.GameInstallPath == "" || !File.Exists(Path.Combine(Settings.Default.GameInstallPath!, ConstStrings.C_MAIN_GAME_FILE)))
            {
                Settings.Default.IsGameInstalled = false;
                Settings.Default.Save();

                PiBArrow.Enabled = false;

                if (Settings.Default.IsPatchModsShown)
                {
                    PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowLeft_Disabled.png"));
                }
                else
                {
                    PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight_Disabled.png"));
                }

                BtnInstall.Show();
                BtnOptions.Hide();
                BtnLaunch.Hide();
            }
            else if (XMLFileHelper.GetXMLFileVersion() > ConstStrings.C_UPDATE_VERSION)
            {
                LblFileName.Show();
                LblFileName.Text = "Preparing Update...";
                BtnLaunch.Enabled = false;
                PiBArrow.Enabled = false;
                BtnAdvanced.Hide();

                if (Settings.Default.IsPatchModsShown)
                {
                    PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowLeft_Disabled.png"));
                }
                else
                {
                    PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight_Disabled.png"));
                }


                CheckForUpdates(true);
            }
            else if (Settings.Default.FirstTimeUse && Settings.Default.IsGameInstalled)
            {
                LblModExplanation.Hide();
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                PiBArrow.Enabled = false;

                if (Settings.Default.IsPatchModsShown)
                {
                    PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowLeft_Disabled.png"));
                }
                else
                {
                    PiBArrow.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnArrowRight_Disabled.png"));
                }

                BtnAdvanced.Hide();
                BtnLaunch.Enabled = false;
                BtnLaunch.Text = "PATCHING...";

                await UpdateRoutine(ConstStrings.C_PATCHZIP29_NAME, "https://dl.dropboxusercontent.com/s/ej1mdbuv4xi53ln/Patch_2.22v29.7z");
                Settings.Default.IsPatch29Downloaded = true;
                Settings.Default.IsPatch29Installed = true;
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Selected.png"));

                Settings.Default.FirstTimeUse = false;
                Settings.Default.Save();
            }
            else
            {
                PiBArrow.Enabled = true;
                BtnLaunch.Enabled = true;
                BtnLaunch.Text = "PLAY GAME";

                BtnAdvanced.Show();

                CheckForUpdates(false);
            }


            if (PatchModDetectionHelper.DetectPatch106())
            {
                Settings.Default.IsPatch106Installed = true;
                Settings.Default.Save();
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106_Selected.png"));
            }
            else
            {
                PiBVersion106.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_106.png"));
            }

            if (!Settings.Default.IsPatch26Downloaded)
            {
                PiBVersion222_1.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V26_Download.png"));
            }

            if (!Settings.Default.IsPatch27Downloaded)
            {
                PiBVersion222_2.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V27_Download.png"));
            }

            if (!Settings.Default.IsPatch28Downloaded)
            {
                PiBVersion222_3.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V28_Download.png"));
            }

            if (!Settings.Default.IsPatch29Downloaded)
            {
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Download.png"));
            }

            if (!Settings.Default.IsPatch30Downloaded)
            {
                PiBVersion222_5.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V30_Download.png"));
            }

            if (!Settings.Default.IsPatch31Downloaded)
            {
                PiBVersion222_6.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V31_Download.png"));
            }

            Settings.Default.SettingsSaving += SettingSaved;
        }

        void SettingSaved(object sender, CancelEventArgs e)
        {
            if (!Settings.Default.PlayBackgroundMusic)
            {
                _soundPlayerHelper.StopTheme();
            }
            //else
            //{
            //    _soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
            //}

            if (Settings.Default.IsPatch29Installed)
            {
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29_Selected.png"));
            }
            else
            {
                PiBVersion222_4.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "BtnPatchSelection_222V29.png"));
            }
        }

        private void BFME1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Default.IsPatch106Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
                Settings.Default.Save();
            }

            if (Settings.Default.IsPatch26Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
                Settings.Default.Save();
            }

            if (Settings.Default.IsPatch27Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
                Settings.Default.Save();
            }

            if (Settings.Default.IsPatch28Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
                Settings.Default.Save();
            }

            if (Settings.Default.IsPatch29Installed)
            {
                Settings.Default.SelectedOlderPatch = true;
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
            _soundPlayerHelper.PlayTheme(Settings.Default.BackgroundMusicFile);
        }

        private void MenuItemLaunchGame_Click(object sender, EventArgs e)
        {
            BtnLaunch.PerformClick();
        }

        private void CloseTheLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}