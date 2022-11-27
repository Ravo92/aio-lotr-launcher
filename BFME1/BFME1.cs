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
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.ComponentModel;
using Downloader;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using System.Linq;
using SharpCompress.Archives;
using System.Diagnostics;
using System.Xml;

namespace PatchLauncher
{
    public partial class BFME1 : Form
    {
        //Sound-Object
        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
        int iconNumber = Properties.Settings.Default.BackgroundMusicIcon;
        readonly string checkforPatchUpdatesXML = "https://ravo92.github.io/PatchUpdate_BFME1.xml";

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
            LblDownloadSpeed.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblDownloadSpeed.ForeColor = Color.FromArgb(192, 145, 69);
            LblDownloadSpeed.BackColor = Color.Transparent;

            LblFileName.Text = "";
            LblFileName.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblFileName.ForeColor = Color.FromArgb(192, 145, 69);
            LblFileName.BackColor = Color.Transparent;

            LblBytes.Text = "";
            LblBytes.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblBytes.ForeColor = Color.FromArgb(192, 145, 69);
            LblBytes.BackColor = Color.Transparent;

            PBarActualFile.ForeColor = Color.FromArgb(192, 145, 69);
            PBarActualFile.BackColor = Color.FromArgb(192, 145, 69);

            // Button-Styles
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.BackColor = Color.Transparent;
            BtnClose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnClose.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);

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

            if (ConstStrings.GameInstallPath() != null && File.Exists(ConstStrings.GameInstallPath() + @"\lotrbfme.exe"))
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
            else if (Properties.Settings.Default.GameInstallPath == "" || !File.Exists(ConstStrings.GameInstallPath() + @"\lotrbfme.exe")) 
            {
                BtnInstall.Show();
                BtnLaunch.Hide();
            }
            else
            {
                BtnInstall.Hide();
                BtnLaunch.Show();
            }

            XmlTextReader reader = new(checkforPatchUpdatesXML);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        Console.Write("<" + reader.Name);

                        while (reader.MoveToNextAttribute())
                            Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                        Console.Write(">");
                        Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text:
                        Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
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
            BtnClose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnClose.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => PlaySoundHover());
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => PlaySoundClick());
        }

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            ProcessStartInfo _processInfo = new()
            {
                WorkingDirectory = @ConstStrings.GameInstallPath(),
                FileName = @ConstStrings.GameInstallPath() + @"\lotrbfme.exe"
            };

            // Start game windowed
            if (Properties.Settings.Default.StartGameWindowed)
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
            Task.Run(() => PlaySoundHover());
        }

        private void BtnLaunch_MouseDown(object sender, MouseEventArgs e)
        {
            BtnLaunch.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
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
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);
        }
        private void BtnOptions_MouseEnter(object sender, EventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnOptions.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => PlaySoundHover());
        }
        private void BtnOptions_MouseDown(object sender, MouseEventArgs e)
        {
            BtnOptions.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => PlaySoundClick());
        }

        private async void BtnInstall_Click(object sender, EventArgs e)
        {
            InstallPathDialog _install = new();

            DialogResult dr = _install.ShowDialog();
            if (dr == DialogResult.OK)
            {
                PBarActualFile.Show();
                LblBytes.Show();
                LblDownloadSpeed.Show();
                LblFileName.Show();

                BtnInstall.Hide();
                BtnOptions.Hide();
                BtnLaunch.Show();

                LblFileName.Text = "Preparing Setup...";

                BtnLaunch.Enabled = false;
                BtnClose.Enabled = false;

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
            Task.Run(() => PlaySoundHover());
        }

        private void BtnInstall_MouseDown(object sender, MouseEventArgs e)
        {
            BtnInstall.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
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
            PLaySoundFile(_xaudio2, "", ConstStrings.C_BUTTONSOUND_CLICK);
            _masteringVoice.Dispose();
            _xaudio2.Dispose();
        }

        public static void PlaySoundHover()
        {
            XAudio2 _xaudio2 = new();
            MasteringVoice _masteringVoice = new(_xaudio2);
            PLaySoundFile(_xaudio2, "", ConstStrings.C_BUTTONSOUND_HOVER);
            _masteringVoice.Dispose();
            _xaudio2.Dispose();
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

        #region GameInstall

        public async Task InstallRoutine()
        {
            RegistryFunctions.WriteRegKeysInstallation();

            if (!Directory.Exists(Properties.Settings.Default.GameInstallPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.GameInstallPath);
            }

            Task download = DownloadGame();
            await download;

            Task extract = ExtractGame();
            await extract;
        }

        public async Task DownloadGame()
        {
            var downloadOpt = new DownloadConfiguration()
            {
                ChunkCount = 1, // file parts to download, default value is 1
                ParallelDownload = false // download parts of file as parallel or not. Default value is false
            };

            var downloader = new DownloadService(downloadOpt);

            downloadOpt.ReserveStorageSpaceBeforeStartingDownload = true;
            downloadOpt.BufferBlockSize = 8000;
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

            if (!File.Exists(Application.StartupPath + "\\Download\\Bin.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=15Fp9jm21AC_908zlGA_xYkZ2yjB4bwkB&confirm=t", Application.StartupPath + "\\Download\\Bin.7z");
            }

            if (!File.Exists(Application.StartupPath + "\\Download\\System.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1Cag-rDgG6CNbb3Yx5mLsHiX5-JP55OuD&confirm=t", Application.StartupPath + "\\Download\\System.7z");
            }

            if (!File.Exists(Application.StartupPath + "\\Download\\Textures.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1L639ovBau8cZtwBETuRtCuQkYHMq6jdQ&confirm=t", Application.StartupPath + "\\Download\\Textures.7z");
            }

            if (!File.Exists(Application.StartupPath + "\\Download\\Movies.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1L6caFHjV5eq_o6Jt_Z_9IITKo1DcbgGj&confirm=t", Application.StartupPath + "\\Download\\Movies.7z");
            }

            if (!File.Exists(Application.StartupPath + "\\Download\\LangPack_EN.7z"))
            {
                await downloader.DownloadFileTaskAsync(@"https://drive.google.com/uc?export=download&id=1L5wHphcet9s0BMUbe8LR44LBLvHn2bJX&confirm=t", Application.StartupPath + "\\Download\\LangPack_EN.7z");
            }
        }

        public async Task ExtractGame()
        {
            int counter = 0;
            SetPBar(0);
            SetPBarMax(371);
            Invoke((MethodInvoker)(() => LblDownloadSpeed.Hide()));

            await Task.Run(() =>
                {
                    using SevenZipArchive archiveSystem = SevenZipArchive.Open(Application.StartupPath + "\\Download\\System.7z");
                    {
                        SetTextPercentages("Extracting from Archive 1/5: System.7zip");
                        foreach (var entry in archiveSystem.Entries)
                        {
                            SetTextFileName("Extracting file: " + entry.ToString());
                            counter++;
                            SetPBar(counter);
                            entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true,
                                PreserveFileTime = true,
                                PreserveAttributes = true
                            });
                        }
                    }

                    using SevenZipArchive archiveTextures = SevenZipArchive.Open(Application.StartupPath + "\\Download\\Textures.7z");
                    {
                        SetTextPercentages("Extracting from Archive 2/5: Textures.7zip");
                        foreach (var entry in archiveTextures.Entries)
                        {
                            SetTextFileName("Extracting file: " + entry.ToString());
                            counter++;
                            SetPBar(counter);
                            entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true,
                                PreserveFileTime = true,
                                PreserveAttributes = true
                            });
                        }
                    }

                    using SevenZipArchive archiveLangEN = SevenZipArchive.Open(Application.StartupPath + "\\Download\\LangPack_EN.7z");
                    {
                        SetTextPercentages("Extracting from Archive 3/5: LangPack_EN.7zip");
                        foreach (var entry in archiveLangEN.Entries)
                        {
                            SetTextFileName("Extracting file: " + entry.ToString());
                            counter++;
                            SetPBar(counter);
                            entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true,
                                PreserveFileTime = true,
                                PreserveAttributes = true
                            });
                        }
                    }

                    using SevenZipArchive archiveBin = SevenZipArchive.Open(Application.StartupPath + "\\Download\\Bin.7z");
                    {
                        SetTextPercentages("Extracting from Archive 4/5: Bin.7zip");
                        foreach (var entry in archiveBin.Entries.Where(entry => !entry.IsDirectory))
                        {
                            SetTextFileName("Extracting file: " + entry.ToString());
                            counter++;
                            SetPBar(counter);
                            entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true,
                                PreserveFileTime = true,
                                PreserveAttributes = true
                            });
                        }
                    }

                    using SevenZipArchive archiveMovies = SevenZipArchive.Open(Application.StartupPath + "\\Download\\Movies.7z");
                    {
                        SetTextPercentages("Extracting from Archive 5/5: Movies.7zip");
                        foreach (var entry in archiveMovies.Entries.Where(entry => !entry.IsDirectory))
                        {
                            SetTextFileName("Extracting file: " + entry.ToString());
                            counter++;
                            SetPBar(counter);
                            entry.WriteToDirectory(Properties.Settings.Default.GameInstallPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true,
                                PreserveFileTime = true,
                                PreserveAttributes = true
                            });
                        }
                    }
                });
                FinishingGameInstall();
        }

        private void FinishingGameInstall()
        {
            Invoke((MethodInvoker)(() => PBarActualFile.Hide()));
            Invoke((MethodInvoker)(() => LblBytes.Hide()));
            Invoke((MethodInvoker)(() => LblFileName.Hide()));
            Invoke((MethodInvoker)(() => BtnOptions.Show()));

            Invoke((MethodInvoker)(() => BtnLaunch.Enabled = true));
            Invoke((MethodInvoker)(() => BtnClose.Enabled = true));

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
                SetPBar(100);
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
        #endregion

        private void TmrPatchNotes_Tick(object sender, EventArgs e)
        {
            TmrPatchNotes.Stop();
            Wv2Patchnotes.Show();
        }
    }
}