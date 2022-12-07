using PatchLauncher.Helper;
using Color = System.Drawing.Color;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.ComponentModel;
using Downloader;
using System.Diagnostics;
using System.Collections.Generic;
using PatchLauncher.Properties;

namespace PatchLauncher
{
    public partial class BFME1 : Form
    {
        //Sound-Object
        SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
        int iconNumber = Settings.Default.BackgroundMusicIcon;

        public const string C_UPDATE_VERSION = "29";
        public const string C_MAIN_PATCH_FILE = "_patch222.big";
        public const string C_MAIN_GAME_FILE = "lotrbfme.exe";

        public BFME1()
        {
            #region logic

            InitializeComponent();

            XMLFileHelper.GetXMLFileData();

            TmrPatchNotes.Tick += new EventHandler(TmrPatchNotes_Tick);
            TmrPatchNotes.Interval = 2000;
            TmrPatchNotes.Start();

            BtnInstall.Hide();

            #endregion

            #region Styles
            //Main Form style behaviour

            BackgroundImage = Image.FromFile(RandomLauncherPicture.GetRandomizedPicture());
            BackgroundImageLayout = ImageLayout.Stretch;

            PibLoadingRing.Show();
            PibLoadingBorder.Show();
            LblPatchNotes.Show();
            Wv2Patchnotes.Hide();

            BtnLaunch.Text = "WORKING...";
            BtnLaunch.Enabled = false;

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

            LblCurrentVersion.Text = "Active: Patch 1.03";
            LblCurrentVersion.Font = ConstStrings.UseFont("Albertus Nova", 14);
            LblCurrentVersion.ForeColor = Color.FromArgb(192, 145, 69);
            LblCurrentVersion.BackColor = Color.Transparent;
            LblCurrentVersion.BorderStyle = BorderStyle.None;
            LblCurrentVersion.OutlineWidth = 2;

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

            #endregion

            #region Tooltips
            //Tooltips
            ToolTip.SetToolTip(PiBThemeSwitcher, "Switches the themes beetween 4 factions and the default theme");
            #endregion

            #region HUD Elements
            PibHeader.Image = Image.FromFile("Images\\header.png");
            PiBYoutube.Image = Image.FromFile("Images\\youtube.png");
            PiBDiscord.Image = Image.FromFile("Images\\discord.png");
            PiBModDB.Image = Image.FromFile("Images\\moddb.png");

            if (Settings.Default.BackgroundMusicIcon == 0)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoDefault.png");
                if (Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            else if (Settings.Default.BackgroundMusicIcon == 1)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoGondor.png");
                if (Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            else if (Settings.Default.BackgroundMusicIcon == 2)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoRohan.png");
                if (Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            else if (Settings.Default.BackgroundMusicIcon == 3)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoIsengard.png");
                if (Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            else if (Settings.Default.BackgroundMusicIcon == 4)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoMordor.png");
                if (Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            #endregion
        }

        #region Button Behaviours

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            ProcessStartInfo _processInfo = new()
            {
                WorkingDirectory = Settings.Default.GameInstallPath,
                FileName = Path.Combine(Settings.Default.GameInstallPath, C_MAIN_GAME_FILE)
            };

            // Start game windowed
            if (Settings.Default.StartGameWindowed)
            {
                _processInfo.Arguments = "-win";
            }

            _ = Process.Start(_processInfo)!;

            Thread.Sleep(1000);

            Application.Exit();
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

                await InstallRoutine();
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
            Process.Start(new ProcessStartInfo("https://discord.gg/beyondstandards") { UseShellExecute = true });
        }

        private void PiBModDB_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.moddb.com/mods/battle-for-middle-earth-patch-222") { UseShellExecute = true });
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
                        Settings.Default.BackgroundMusicFile = @"Sounds\\music_default.wav";
                        Settings.Default.BackgroundMusicIcon = 0;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoDefault.png");
                        _theme.Stop();

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                            _theme.Play();
                        }

                        break;
                    }
                case 1:
                    {
                        Settings.Default.BackgroundMusicFile = @"Sounds\\music_gondor.wav";
                        Settings.Default.BackgroundMusicIcon = 1;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoGondor.png");
                        _theme.Stop();

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                            _theme.Play();
                        }
                        break;
                    }
                case 2:
                    {
                        Settings.Default.BackgroundMusicFile = @"Sounds\\music_rohan.wav";
                        Settings.Default.BackgroundMusicIcon = 2;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoRohan.png");
                        _theme.Stop();

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                            _theme.Play();
                        }
                        break;
                    }
                case 3:
                    {
                        Settings.Default.BackgroundMusicFile = @"Sounds\\music_isengard.wav";
                        Settings.Default.BackgroundMusicIcon = 3;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoIsengard.png");
                        _theme.Stop();

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                            _theme.Play();
                        }
                        break;
                    }
                case 4:
                    {
                        Settings.Default.BackgroundMusicFile = @"Sounds\\music_mordor.wav";
                        Settings.Default.BackgroundMusicIcon = 4;
                        Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoMordor.png");
                        _theme.Stop();

                        if (Settings.Default.PlayBackgroundMusic == true)
                        {
                            SoundPlayer _theme = new(Settings.Default.BackgroundMusicFile);
                            _theme.Play();
                        }
                        break;
                    }
            }
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
                XMLFileHelper.GetXMLFileVersion();

                if (File.Exists(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) && MD5Tools.CalculateMD5(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) == "a007b2ea1f87a530c1e412255e1d7896")
                {
                    Properties.Settings.Default.PatchVersionInstalled = 29;
                    Properties.Settings.Default.Save();
                }
                else if (MD5Tools.CalculateMD5(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) != "a007b2ea1f87a530c1e412255e1d7896" && MD5Tools.CalculateMD5(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) != "404")
                {
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();
                    LblFileName.Text = "Installed Patch 2.22v29 is damaged and will be reaquired...";

                    Properties.Settings.Default.PatchVersionInstalled = 103;
                    Properties.Settings.Default.Save();

                    await UpdateRoutine();
                }
                else if (MD5Tools.CalculateMD5(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) == "404")
                {
                    PBarActualFile.Show();
                    LblBytes.Show();
                    LblDownloadSpeed.Show();
                    LblFileName.Show();

                    await UpdateRoutine();
                }
            }
            else
            {
                var timer = new PeriodicTimer(TimeSpan.FromSeconds(600));

                while (await timer.WaitForNextTickAsync())
                {
                    XMLFileHelper.GetXMLFileVersion();

                    if (File.Exists(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) && MD5Tools.CalculateMD5(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) == "a007b2ea1f87a530c1e412255e1d7896")
                    {
                        Properties.Settings.Default.PatchVersionInstalled = 29;
                        Properties.Settings.Default.Save();
                    }
                    else if (MD5Tools.CalculateMD5(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) != "a007b2ea1f87a530c1e412255e1d7896" && MD5Tools.CalculateMD5(Path.Combine(Properties.Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)) != "404")
                    {
                        LblFileName.Show();
                        LblFileName.Text = "Installed Patch 2.22v29 is damaged and will be reaquired...";
                        Properties.Settings.Default.PatchVersionInstalled = 103;
                        Properties.Settings.Default.Save();

                        await UpdateRoutine();
                    }
                }
            }
        }

        public async Task UpdateRoutine()
        {
            Task download = DownloadUpdate();
            await download;

            Task extract = ExtractUpdate();
            await extract;
        }

        public async Task DownloadUpdate()
        {
            SetPBarFiles(0);
            SetPBarFilesMax(100);

            var downloadOpt = new DownloadConfiguration()
            {
                ChunkCount = 1, // file parts to download, default value is 1
                ParallelDownload = false // download parts of file as parallel or not. Default value is false
            };

            var downloader = new DownloadService(downloadOpt);

            downloadOpt.ReserveStorageSpaceBeforeStartingDownload = true;
            downloadOpt.BufferBlockSize = 10240;
            downloadOpt.MaximumBytesPerSecond = 9223372036854775800;
            downloadOpt.ClearPackageOnCompletionWithFailure = true;

            // Provide `FileName` and `TotalBytesToReceive` at the start of each downloads
            downloader.DownloadStarted += OnDownloadStarted;

            // Provide any information about download progress, 
            // like progress percentage of sum of chunks, total speed, 
            // average speed, total received bytes and received bytes array 
            // to live streaming.
            downloader.DownloadProgressChanged += OnDownloadProgressChanged;

            // Download completed event that can include occurred errors or 
            // cancelled or download completed successfully.
            downloader.DownloadFileCompleted += OnDownloadFileCompleted;

            if (!File.Exists(Path.Combine(Application.StartupPath, "Patches", "Patch_2.22v29.7z")))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1LIpMSUGVdHlRerQl8Z6awyu-8KdwECZb&confirm=t", Path.Combine(Application.StartupPath, "Patches", "Patch_2.22v29.7z"));
            }
        }

        public async Task ExtractUpdate()
        {
            Invoke((MethodInvoker)(() => LblBytes.Hide()));
            Invoke((MethodInvoker)(() => LblDownloadSpeed.Hide()));

            Invoke((MethodInvoker)(() => LblFileName.Text = "Copy files and apply patch..."));

            var progressHandler = new Progress<ExtractionProgress>(progress =>
            {
                SetPBarFiles(progress.Count);
                SetPBarFilesMax(progress.Max);
                SetTextFileName(progress.Filename!);
                SetTextDlSpeed(string.Concat(progress.Count, "/", progress.Max));
            });

            ZIPFileHelper _ZIPFileHelper = new();
            await _ZIPFileHelper.ExtractArchive(Path.Combine("Patches", "Patch_2.22v29.7z"), Properties.Settings.Default.GameInstallPath, progressHandler);

            FinishingGameUpdate();
        }

        private void FinishingGameUpdate()
        {
            Invoke((MethodInvoker)(() => PBarActualFile.Hide()));
            Invoke((MethodInvoker)(() => LblBytes.Hide()));
            Invoke((MethodInvoker)(() => LblDownloadSpeed.Hide()));
            Invoke((MethodInvoker)(() => LblFileName.Hide()));

            Invoke((MethodInvoker)(() => BtnLaunch.Enabled = true));

            Properties.Settings.Default.PatchVersionInstalled = 29;
            Properties.Settings.Default.Save();

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
            RegistryService.WriteRegKeysInstallation(Settings.Default.GameInstallPath);

            if (!Directory.Exists(Settings.Default.GameInstallPath))
            {
                Directory.CreateDirectory(Settings.Default.GameInstallPath);
            }

            if (Settings.Default.GameInstallPath != null && File.Exists(Path.Combine(Settings.Default.GameInstallPath, C_MAIN_GAME_FILE)) && Settings.Default.PatchVersionInstalled == 29)
            {
                if (!Directory.Exists(ConstStrings.GameAppdataFolderPath()))
                    Directory.CreateDirectory(ConstStrings.GameAppdataFolderPath());

                if (!File.Exists(ConstStrings.GameAppdataFolderPath() + ConstStrings.C_OPTIONSINI_FILENAME))
                    File.Copy("Tools\\" + ConstStrings.C_OPTIONSINI_FILENAME, ConstStrings.GameAppdataFolderPath() + ConstStrings.C_OPTIONSINI_FILENAME);
            }

            Settings.Default.IsGameInstalled = true;
            Settings.Default.Save();

            Task download = DownloadGame();
            await download;

            Task extract = ExtractGame();
            await extract;

            Task update = UpdateRoutine();
            await update;
        }

        public async Task DownloadGame()
        {
            PBarActualFile.Show();

            var downloadOpt = new DownloadConfiguration()
            {
                ChunkCount = 1, // file parts to download, default value is 1
                ParallelDownload = false // download parts of file as parallel or not. Default value is false
            };

            var downloader = new DownloadService(downloadOpt);

            downloadOpt.ReserveStorageSpaceBeforeStartingDownload = true;
            downloadOpt.BufferBlockSize = 10240;
            downloadOpt.MaximumBytesPerSecond = 9223372036854775800;
            downloadOpt.ClearPackageOnCompletionWithFailure = true;

            // Provide `FileName` and `TotalBytesToReceive` at the start of each downloads
            downloader.DownloadStarted += OnDownloadStarted;

            // Provide any information about download progress, 
            // like progress percentage of sum of chunks, total speed, 
            // average speed, total received bytes and received bytes array 
            // to live streaming.
            downloader.DownloadProgressChanged += OnDownloadProgressChanged;

            // Download completed event that can include occurred errors or 
            // cancelled or download completed successfully.
            downloader.DownloadFileCompleted += OnDownloadFileCompleted;

            if (!File.Exists(Application.StartupPath + "\\Download\\BFME1.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1LHGbdAXxwlvshcF5suS-VKKyOMlfh1XC&confirm=t", Application.StartupPath + "\\Download\\BFME1.7z");
            }

            if (!File.Exists(Application.StartupPath + "\\Download\\LangPack_EN.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1L5wHphcet9s0BMUbe8LR44LBLvHn2bJX&confirm=t", Application.StartupPath + "\\Download\\LangPack_EN.7z");
            }
        }

        public async Task ExtractGame()
        {
            Invoke((MethodInvoker)(() => PBarActualFile.Show()));
            SetPBarFilesMax(100);

            var progressHandler = new Progress<ExtractionProgress>(progress =>
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
                await _ZIPFileHelper.ExtractArchive(Path.Combine(@"Download", archiveFileNames[i]), Properties.Settings.Default.GameInstallPath, progressHandler);
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
            PibLoadingRing.Hide();
            PibLoadingBorder.Hide();
            LblPatchNotes.Hide();
        }

        private void BFME1_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.GameInstallPath == "" || !File.Exists(Path.Combine(Settings.Default.GameInstallPath!, C_MAIN_GAME_FILE)))
            {
                Settings.Default.IsGameInstalled = false;
                Settings.Default.Save();
                BtnInstall.Show();
                BtnLaunch.Hide();
            }
            else if ((XMLFileHelper.GetXMLFileVersion() == 29 && File.Exists(Path.Combine(Settings.Default.GameInstallPath + C_MAIN_GAME_FILE))) || 
                (File.Exists(Path.Combine(Settings.Default.GameInstallPath, C_MAIN_GAME_FILE)) && Settings.Default.PatchVersionInstalled != 29) || 
                !File.Exists(Path.Combine(Settings.Default.GameInstallPath, C_MAIN_PATCH_FILE)))
            {
                LblFileName.Show();
                LblFileName.Text = "Preparing Update...";
                BtnLaunch.Enabled = false;

                CheckForUpdates(true);
            }
            else
            {
                BtnLaunch.Enabled = true;
                BtnLaunch.Text = "LAUNCH";
                CheckForUpdates(false);
            }
        }

        private void BFME1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}