using PatchLauncher.Classes;
using Color = System.Drawing.Color;
using SharpDX.XAudio2;
using SharpDX.Multimedia;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Media;
using System.Collections.Generic;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.ComponentModel;
using Downloader;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using System.Linq;
using SharpCompress.Archives;

namespace PatchLauncher
{
    public partial class BFME1 : Form
    {
        //Initialize Font-System
        readonly ConstStrings _font = new();

        //Sound-Object
        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
        int iconNumber = Properties.Settings.Default.BackgroundMusicIcon;

        public BFME1()
        {
            InitializeComponent();

            #region Styles
            //Main Form style behaviour

            BackgroundImage = Image.FromFile(RandomLauncherPicture.RandomizePicture());
            BackgroundImageLayout = ImageLayout.Stretch;

            Wv2Patchnotes.Hide();

            TmrPatchNotes.Tick += new EventHandler(TmrPatchNotes_Tick);
            TmrPatchNotes.Interval = 4000;
            TmrPatchNotes.Start();

            // label-Styles
            LblDownloadSpeed.Text = "";
            LblDownloadSpeed.Font = _font.UseFont("Albertus Nova", 16);
            LblDownloadSpeed.ForeColor = Color.FromArgb(192, 145, 69);
            LblDownloadSpeed.BackColor = Color.Transparent;

            LblFileName.Text = "";
            LblFileName.Font = _font.UseFont("Albertus Nova", 16);
            LblFileName.ForeColor = Color.FromArgb(192, 145, 69);
            LblFileName.BackColor = Color.Transparent;

            LblBytes.Text = "";
            LblBytes.Font = _font.UseFont("Albertus Nova", 16);
            LblBytes.ForeColor = Color.FromArgb(192, 145, 69);
            LblBytes.BackColor = Color.Transparent;

            PBarActualFile.ForeColor = Color.FromArgb(192, 145, 69);

            // Button-Styles
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.BackColor = Color.Transparent;
            BtnClose.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnClose.Font = _font.UseFont("Albertus Nova", 14);
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);

            BtnLaunch.FlatAppearance.BorderSize = 0;
            BtnLaunch.FlatStyle = FlatStyle.Flat;
            BtnLaunch.BackColor = Color.Transparent;
            BtnLaunch.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnLaunch.Font = _font.UseFont("Albertus Nova", 14);
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);

            BtnOptions.FlatAppearance.BorderSize = 0;
            BtnOptions.FlatStyle = FlatStyle.Flat;
            BtnOptions.BackColor = Color.Transparent;
            BtnOptions.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnOptions.Font = _font.UseFont("Albertus Nova", 14);
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);

            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.BackColor = Color.Transparent;
            BtnInstall.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnInstall.Font = _font.UseFont("Albertus Nova", 14);
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

            if (Properties.Settings.Default.BackgroundMusicIcon == 0)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoDefault.png");
                if (Properties.Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            else if (Properties.Settings.Default.BackgroundMusicIcon == 1)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoGondor.png");
                if (Properties.Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            else if (Properties.Settings.Default.BackgroundMusicIcon == 2)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoRohan.png");
                if (Properties.Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            else if (Properties.Settings.Default.BackgroundMusicIcon == 3)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoIsengard.png");
                if (Properties.Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            else if (Properties.Settings.Default.BackgroundMusicIcon == 4)
            {
                PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoMordor.png");
                if (Properties.Settings.Default.PlayBackgroundMusic)
                {
                    this._theme.Stop();
                    SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                    _theme.Play();
                }
            }
            #endregion

            #region Internal Logic
            //Internal Logic

            if (ConstStrings.GameInstallPath() != null && !File.Exists(ConstStrings.GameInstallPath() + "lotrbfme.exe"))
            {
                LblBytes.Hide();
                LblDownloadSpeed.Hide();
                PBarActualFile.Hide();
                BtnInstall.Hide();
                BtnLaunch.Show();

                if (!Directory.Exists(ConstStrings.GameAppdataFolderPath()))
                    Directory.CreateDirectory(ConstStrings.GameAppdataFolderPath());

                if (!File.Exists(ConstStrings.GameAppdataFolderPath() + ConstStrings.OptionsIniFileName()))
                    File.Copy("Tools\\" + ConstStrings.OptionsIniFileName(), ConstStrings.GameAppdataFolderPath() + ConstStrings.OptionsIniFileName());
            }
            else if (Properties.Settings.Default.GameInstallPath == "" || !File.Exists(ConstStrings.GameInstallPath() + "lotrbfme.exe")) 
            {
                BtnInstall.Show();
                BtnLaunch.Hide();
            }
            else
            {
                BtnInstall.Hide();
                BtnLaunch.Show();
            }
            #endregion
        }

        #region Form Behaviours
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowMover.ReleaseCapture();
                _ = WindowMover.SendMessage(Handle, WindowMover.WM_NCLBUTTONDOWN, WindowMover.HT_CAPTION, 0);
            }
        }

        private void PibHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowMover.ReleaseCapture();
                _ = WindowMover.SendMessage(Handle, WindowMover.WM_NCLBUTTONDOWN, WindowMover.HT_CAPTION, 0);
            }
        }
        #endregion

        #region Button Behaviours

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Application.Exit();
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.ButtonImageHover();
            BtnClose.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => PlaySoundHover());
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.ButtonImageClick();
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => PlaySoundClick());
        }

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);

            Process _process = new();
            _process.StartInfo.FileName = ConstStrings.GameInstallPath() + "lotrbfme.exe";

            // Start game windowed
            if (Properties.Settings.Default.StartGameWindowed)
            {
                _process.StartInfo.Arguments = "-win";
            }

            _process.StartInfo.WorkingDirectory = Application.StartupPath;
            _process.Start();
            Dispose();

            Application.Exit();
        }

        private void BtnLaunch_MouseLeave(object sender, EventArgs e)
        {
            BtnLaunch.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnLaunch_MouseEnter(object sender, EventArgs e)
        {
            BtnLaunch.BackgroundImage = ConstStrings.ButtonImageHover();
            BtnLaunch.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => PlaySoundHover());
        }

        private void BtnLaunch_MouseDown(object sender, MouseEventArgs e)
        {
            BtnLaunch.BackgroundImage = ConstStrings.ButtonImageClick();
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => PlaySoundClick());
        }
        private void BtnOptions_Click(object sender, EventArgs e)
        {
            OptionsBFME1 _options = new();
            _options.ShowDialog();
        }
        private void BtnOptions_MouseLeave(object sender, EventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);
        }
        private void BtnOptions_MouseEnter(object sender, EventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.ButtonImageHover();
            BtnOptions.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => PlaySoundHover());
        }
        private void BtnOptions_MouseDown(object sender, MouseEventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.ButtonImageClick();
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => PlaySoundClick());
        }

        private void BtnInstall_Click(object sender, EventArgs e)
        {
            InstallPathDialog _install = new();
            _install.ShowDialog();

            InstallRoutine(Properties.Settings.Default.GameInstallPath);
        }

        private void BtnInstall_MouseLeave(object sender, EventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnInstall_MouseEnter(object sender, EventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.ButtonImageHover();
            BtnInstall.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => PlaySoundHover());
        }

        private void BtnInstall_MouseDown(object sender, MouseEventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.ButtonImageClick();
            BtnInstall.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => PlaySoundClick());
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

            switch (iconNumber) {
                case 0:
                    {
                        Properties.Settings.Default.BackgroundMusicFile = @"Sounds\\music_default.wav";
                        Properties.Settings.Default.BackgroundMusicIcon = 0;
                        Properties.Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoDefault.png");
                        this._theme.Stop();
                        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                        _theme.Play();
                        break;
                    }
                case 1:
                    {
                        Properties.Settings.Default.BackgroundMusicFile = @"Sounds\\music_gondor.wav";
                        Properties.Settings.Default.BackgroundMusicIcon = 1;
                        Properties.Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoGondor.png");
                        this._theme.Stop();
                        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                        _theme.Play();
                        break;
                    }
                case 2:
                    {
                        Properties.Settings.Default.BackgroundMusicFile = @"Sounds\\music_rohan.wav";
                        Properties.Settings.Default.BackgroundMusicIcon = 2;
                        Properties.Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoRohan.png");
                        this._theme.Stop();
                        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                        _theme.Play();
                        break;
                    }
                case 3:
                    {
                        Properties.Settings.Default.BackgroundMusicFile = @"Sounds\\music_isengard.wav";
                        Properties.Settings.Default.BackgroundMusicIcon = 3;
                        Properties.Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoIsengard.png");
                        this._theme.Stop();
                        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                        _theme.Play();
                        break;
                    }
                case 4:
                    {
                        Properties.Settings.Default.BackgroundMusicFile = @"Sounds\\music_mordor.wav";
                        Properties.Settings.Default.BackgroundMusicIcon = 4;
                        Properties.Settings.Default.Save();
                        PiBThemeSwitcher.Image = Image.FromFile("Images\\IcoMordor.png");
                        this._theme.Stop();
                        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                        _theme.Play();
                        break;
                    }
            }
        }

        #endregion

        #region Sound System
        //Initialize Sound-System
        public static void PLaySoundFile(XAudio2 device, string text, string fileName)
        {
            var stream = new SoundStream(File.OpenRead(fileName));
            var waveFormat = stream.Format;
            var buffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream
            };
            stream.Close();

            var sourceVoice = new SourceVoice(device, waveFormat, true);
            // Adds a sample callback to check that they are working on source voices
            sourceVoice.SubmitSourceBuffer(buffer, stream.DecodedPacketsInfo);
            sourceVoice.Start();

            int count = 0;
            while (sourceVoice.State.BuffersQueued > 0)
            {
                if (count == 50)
                {
                    count = 0;
                }
                Thread.Sleep(10);
                count++;
            }

            sourceVoice.DestroyVoice();
            sourceVoice.Dispose();
            buffer.Stream.Dispose();
        }

        public static void PlaySoundClick()
        {
            XAudio2 _xaudio2 = new();
            MasteringVoice _masteringVoice = new(_xaudio2);
            PLaySoundFile(_xaudio2, "", ConstStrings.ButtonSoundClick());
            _masteringVoice.Dispose();
            _xaudio2.Dispose();
        }

        public static void PlaySoundHover()
        {
            XAudio2 _xaudio2 = new();
            MasteringVoice _masteringVoice = new(_xaudio2);
            PLaySoundFile(_xaudio2, "", ConstStrings.ButtonSoundHover());
            _masteringVoice.Dispose();
            _xaudio2.Dispose();
        }
        #endregion

        #region ToolTip System
        public void Tooltip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Font tooltipFont = _font.UseFont("Albertus Nova", 14);
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, tooltipFont, Brushes.SandyBrown, new PointF(2, 2));
        }

        public void TooltipPopup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = TextRenderer.MeasureText(ToolTip.GetToolTip(e.AssociatedControl), _font.UseFont("Albertus Nova", 14));
        }
        #endregion

        #region GameInstall

        public async void InstallRoutine(string installPath)
        {
            List<string> _regKeyFolderNames = new()
            {
                "EA GAMES",
                "The Battle for Middle-earth",
                "1.0",

                "Electronic Arts",
                "EA Games",
                "The Battle for Middle-earth",
                "ergc"
            };

            List<string> _regKeyNames = new()
            {
                "CacheSize",
                "CD Drive",
                "DisplayName",
                "Folder",
                "Install Dir",
                "Installed From",
                "Language",
                "Locale",
                "Patch URL",
                "Product GUID",
                "Region",
                "Registration",
                "Suppression Exe",
                "SwapSize",

                "DisplayName",
                "Language",
                "LanguageName",

                "InstallPath",
                "Language",
                "MapPackVersion",
                "UseLocalUserMaps",
                "UserDataLeafName",
                "Version",
                "(Default)"
            };

            List<string> _regKeyValues = new()
            {
                "3351006208",
                "I:\\",
                "The Battle for Middle-earth (tm)",
                "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\EA GAMES\\The Battle for Middle-earth (tm)\\",
                "E:\\Spiele\\The Battle for Middle-earth (tm)\\", // TODO: Changing the Install dir dynamically when installing the actual Game!!!
                "I:\\",
                "English US",
                "en_us",
                "http://transtest.ea.com/Electronic Arts/The Battle for Middle-earth/Europe",
                "{3F290582-3F4E-4B96-009C-E0BABAA40C42}",
                "Europe",
                "SOFTWARE\\Electronic Arts\\EA GAMES\\The Battle for Middle-earth\\ergc",
                "rtsi.exe",
                "0",

                "The Battle for Middle-earth (tm)",
                "1", // DWORD
                "English US",

                "E:\\Spiele\\The Battle for Middle-earth (tm)\\", // TODO: Changing the Install dir dynamically when installing the actual Game!!!
                "english",
                "10000", //DWORD
                "0", //DWORD
                "My Battle for Middle-earth Files",
                "10003", //DWORD
                "FVZZM2FSUCWDH2F73B5P" //CDKEY UPPERCASE ALPHANUMERAL 18 CHARAKTERS WIDE
            };

            PBarActualFile.Show();
            LblBytes.Show();
            LblDownloadSpeed.Show();
            LblFileName.Show();

            BtnInstall.Hide();
            BtnLaunch.Show();
            BtnLaunch.Enabled = false;

            var downloadOpt = new DownloadConfiguration()
            {
                ChunkCount = 1, // file parts to download, default value is 1
                ParallelDownload = false // download parts of file as parallel or not. Default value is false
            };

            var downloader = new DownloadService(downloadOpt);

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

            if (!File.Exists(Application.StartupPath + "\\Download\\Bin.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=15Fp9jm21AC_908zlGA_xYkZ2yjB4bwkB&confirm=t", Application.StartupPath + "\\Download\\Bin.7z");
            }
            else if (!File.Exists(Application.StartupPath + "\\Download\\System.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1Cag-rDgG6CNbb3Yx5mLsHiX5-JP55OuD&confirm=t", Application.StartupPath + "\\Download\\System.7z");
            }
            else if (!File.Exists(Application.StartupPath + "\\Download\\Textures.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1jMOkrbTASVpE6VNzxA3i3XyO-LqG6BrH&confirm=t", Application.StartupPath + "\\Download\\Textures.7z");
            }
            else if (!File.Exists(Application.StartupPath + "\\Download\\LangEN.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1L5wHphcet9s0BMUbe8LR44LBLvHn2bJX&confirm=t", Application.StartupPath + "\\Download\\LangEN.7z");
            }

            PBarActualFile.Value = 0;

            if (!Directory.Exists(Properties.Settings.Default.GameInstallPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.GameInstallPath);
            }

            Thread _thread = new(new ThreadStart(ExtractGame));
            _thread.Start();
        }

        public void ExtractGame()
        {
            using (var archiveTest = SevenZipArchive.Open(Application.StartupPath + "\\Download\\Bin.7z"))
            {
                SetPBarMax(archiveTest.Entries.Count);
                foreach (var entry in archiveTest.Entries)
                {
                    SetTextFileName("Extracting file: " + entry.ToString());
                    SetPBar(+1);
                    if (!entry.IsDirectory)
                        entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true, PreserveFileTime = true, PreserveAttributes = true });
                }
            }

            using SevenZipArchive archiveBin = SevenZipArchive.Open(Application.StartupPath + "\\Download\\Bin.7z");
            SetPBarMax(archiveBin.Entries.Count);
            foreach (var entry in archiveBin.Entries.Where(entry => !entry.IsDirectory))
            {
                SetTextFileName("Extracting file: " + entry.ToString());
                SetPBar(+1);
                entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true,
                    PreserveFileTime = true,
                    PreserveAttributes = true
                });
            }

            using SevenZipArchive archiveSystem = SevenZipArchive.Open(Application.StartupPath + "\\Download\\System.7z");
            SetPBarMax(archiveBin.Entries.Count);
            foreach (var entry in archiveSystem.Entries.Where(entry => !entry.IsDirectory))
            {
                SetTextFileName("Extracting file: " + entry.ToString());
                SetPBar(+1);
                entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true,
                    PreserveFileTime = true,
                    PreserveAttributes = true
                });
            }

            using SevenZipArchive archiveTextures = SevenZipArchive.Open(Application.StartupPath + "\\Download\\Textures.7z");
            SetPBarMax(archiveBin.Entries.Count);
            foreach (var entry in archiveTextures.Entries.Where(entry => !entry.IsDirectory))
            {
                SetTextFileName("Extracting file: " + entry.ToString());
                SetPBar(+1);
                entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true,
                    PreserveFileTime = true,
                    PreserveAttributes = true
                });
            }

            using SevenZipArchive archiveLangEN = SevenZipArchive.Open(Application.StartupPath + "\\Download\\LangEN.7z");
            SetPBarMax(archiveBin.Entries.Count);
            foreach (var entry in archiveLangEN.Entries.Where(entry => !entry.IsDirectory))
            {
                SetTextFileName("Extracting file: " + entry.ToString());
                SetPBar(+1);
                entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true,
                    PreserveFileTime = true,
                    PreserveAttributes = true
                });
            }
        }

        private void OnDownloadStarted(object sender, DownloadStartedEventArgs e)
        {
            SetPBar(0);
            SetTextFileName("Downloading: " + Path.GetFileName(e.FileName));
        }

        private void OnDownloadProgressChanged(object sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            SetPBar((int)e.ProgressPercentage);
            SetTextDlSpeed("@ " + Math.Round(e.AverageBytesPerSecondSpeed / 1024000).ToString() + " MB/s");
            SetTextPercentages(Math.Round(e.ProgressPercentage).ToString() + " %");
        }

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
        private void SetTextPercentages(string text)
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

        delegate void SetPBarCallback(int value);
        private void SetPBar(int value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (PBarActualFile.InvokeRequired)
            {
                SetPBarCallback d = new(SetPBar);
                Invoke(d, new object[] { value });
            }
            else
            {
                PBarActualFile.Value = value;
            }
        }

        delegate void SetPBarMaxCallback(int value);
        private void SetPBarMax(int value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (PBarActualFile.InvokeRequired)
            {
                SetPBarMaxCallback d = new(SetPBarMax);
                Invoke(d, new object[] { value });
            }
            else
            {
                PBarActualFile.Maximum = value;
            }
        }

        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            SetTextFileName("Working...");
            //PBarActualFile.Tick(10000);


            if (e.Error != null)
            {
                if (PBarActualFile is null)
                {
                    SetTextFileName(e.Error.Message);
                }
                else
                {
                    SetTextFileName(e.Error.Message);
                }
                Debugger.Break();
            }
            else
            {
                SetTextFileName("Configuring...");
                SetPBar(100);
            }
        }

        #endregion

        private void TmrPatchNotes_Tick(object sender, EventArgs e)
        {
            TmrPatchNotes.Stop();
            Wv2Patchnotes.Show();
        }
    }
}