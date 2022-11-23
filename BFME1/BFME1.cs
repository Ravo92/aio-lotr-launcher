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

namespace PatchLauncher
{
    public partial class BFME1 : Form
    {
        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
        int iconNumber = Properties.Settings.Default.BackgroundMusicIcon;

        public BFME1()
        {
            InitializeComponent();

            #region Button Styles
            //Main Form style behaviour

            BackgroundImage = Image.FromFile(RandomLauncherPicture.RandomizePicture());
            BackgroundImageLayout = ImageLayout.Stretch;

            //Uri _uri = new Uri(Application.StartupPath + "Html/index.html");
            //Wv2Background.Source = new Uri(_uri.ToString());
            Wv2Patchnotes.Hide();

            TmrPatchNotes.Tick += new EventHandler(TmrPatchNotes_Tick);
            TmrPatchNotes.Interval = 4000;
            TmrPatchNotes.Start();

            // Button-Styles
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.BackColor = Color.Transparent;
            BtnClose.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnClose.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);

            BtnLaunch.FlatAppearance.BorderSize = 0;
            BtnLaunch.FlatStyle = FlatStyle.Flat;
            BtnLaunch.BackColor = Color.Transparent;
            BtnLaunch.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnLaunch.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);

            BtnOptions.FlatAppearance.BorderSize = 0;
            BtnOptions.FlatStyle = FlatStyle.Flat;
            BtnOptions.BackColor = Color.Transparent;
            BtnOptions.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnOptions.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnOptions.ForeColor = Color.FromArgb(192, 145, 69);

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

            if (ConstStrings.GameInstallPath() != null)
            {
                if (!Directory.Exists(ConstStrings.GameAppdataFolderPath()))
                    Directory.CreateDirectory(ConstStrings.GameAppdataFolderPath());

                if (!File.Exists(ConstStrings.GameAppdataFolderPath() + ConstStrings.OptionsIniFileName()))
                    File.Copy("Tools\\" + ConstStrings.OptionsIniFileName(), ConstStrings.GameAppdataFolderPath() + ConstStrings.OptionsIniFileName());
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

        private void TmrPatchNotes_Tick(object sender, EventArgs e)
        {
            TmrPatchNotes.Stop();
            Wv2Patchnotes.Show();
        }
    }
}