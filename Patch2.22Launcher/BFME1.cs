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
using System.Collections.Generic;
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

            // Button-Styles
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.BackColor = Color.Transparent;
            BtnClose.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnClose.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);

            BtnLaunch.FlatAppearance.BorderSize = 0;
            BtnLaunch.FlatStyle = FlatStyle.Flat;
            BtnLaunch.BackColor = Color.Transparent;
            BtnLaunch.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnLaunch.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);

            //Checkbox-Styles
            ChkTheme.FlatAppearance.BorderSize = 0;
            ChkTheme.FlatStyle = FlatStyle.Flat;
            ChkTheme.BackColor = Color.Transparent;
            ChkTheme.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            ChkTheme.ForeColor = Color.FromArgb(192, 145, 69);
            if (Properties.Settings.Default.PlayBackgroundMusic)
            {
                ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
                SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                _theme.Play();
            }
            else
            {
                ChkTheme.Image = Image.FromFile("Images\\chkUnselected.png");
                _theme.Stop();
                _theme.Dispose();
            }
            #endregion

            #region Labels and Tooltips
            //Label-Styles
            LblTheme.Text = "Play Theme Music";
            LblTheme.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            LblTheme.ForeColor = Color.FromArgb(192, 145, 69);
            LblTheme.BackColor = Color.Transparent;

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
            string language = RegistryFunctions.ReadRegKey("lang");
            string gameInstallPath = RegistryFunctions.ReadRegKey("path");
            string appdataFolder = RegistryFunctions.ReadRegKey("appData");
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + appdataFolder + "\\";
            string optionsFile = "Options.ini";

            if (gameInstallPath != null)
            {
                if (!Directory.Exists(appdataPath))
                    Directory.CreateDirectory(appdataPath);

                if (!File.Exists(appdataPath + optionsFile))
                    File.Copy("Tools\\" + optionsFile, appdataPath + optionsFile);
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


        #endregion

        #region Button Behaviours

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Application.Exit();
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.Image = Image.FromFile("Images\\btnHover.png");
            BtnClose.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => PlaySoundHover());
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnClose.Image = Image.FromFile("Images\\btnClick.png");
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => PlaySoundClick());
        }

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);

            //TO-DO: Start Game Routine

            Application.Exit();
        }

        private void BtnLaunch_MouseLeave(object sender, EventArgs e)
        {
            BtnLaunch.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnLaunch_MouseEnter(object sender, EventArgs e)
        {
            BtnLaunch.Image = Image.FromFile("Images\\btnHover.png");
            BtnLaunch.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => PlaySoundHover());
        }

        private void BtnLaunch_MouseDown(object sender, MouseEventArgs e)
        {
            BtnLaunch.Image = Image.FromFile("Images\\btnClick.png");
            BtnLaunch.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => PlaySoundClick());
        }

        private void ChkTheme_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PlayBackgroundMusic)
            {
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
                Properties.Settings.Default.PlayBackgroundMusic = false;
                Properties.Settings.Default.Save();
                _theme.Stop();
                _theme.Dispose();
            }
            else
            {
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
                Properties.Settings.Default.PlayBackgroundMusic = true;
                Properties.Settings.Default.Save();
                SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                _theme.Play();
            }
        }

        private void ChkTheme_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PlayBackgroundMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkTheme_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PlayBackgroundMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void ChkTheme_MouseDown(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.PlayBackgroundMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
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
            List<string> ThemeIcons = ThemenIconList.GetThemeIcons();

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
        static void PLaySoundFile(XAudio2 device, string text, string fileName)
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

        static void PlaySoundClick()
        {
            XAudio2 _xaudio2 = new();
            MasteringVoice _masteringVoice = new(_xaudio2);
            PLaySoundFile(_xaudio2, "", "Sounds\\btnClick.wav");
            _masteringVoice.Dispose();
            _xaudio2.Dispose();
        }

        static void PlaySoundHover()
        {
            XAudio2 _xaudio2 = new();
            MasteringVoice _masteringVoice = new(_xaudio2);
            PLaySoundFile(_xaudio2, "", "Sounds\\btnHover.wav");
            _masteringVoice.Dispose();
            _xaudio2.Dispose();
        }
        #endregion

        private void Tooltip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Font tooltipFont = new("Albertus MT", 16, FontStyle.Regular);
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, tooltipFont, Brushes.SandyBrown, new PointF(2, 2));
        }

        private void _tooltip_Popup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = TextRenderer.MeasureText(ToolTip.GetToolTip(e.AssociatedControl), new Font("Albertus MT", 16, FontStyle.Regular));
        }
    }
}