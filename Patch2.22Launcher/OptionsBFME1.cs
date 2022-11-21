using PatchLauncher.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class OptionsBFME1 : Form
    {
        bool FlagEAX = Properties.Settings.Default.EAXSupport;
        bool FlagEAXFileExists = File.Exists(ConstStrings.GameInstallPath() + "dsound.dll");
        bool FlagThemeMusic = Properties.Settings.Default.PlayBackgroundMusic;

        SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
        public OptionsBFME1()
        {
            InitializeComponent();

            #region Button Styles
            //Main Form style behaviour

            BackgroundImage = Image.FromFile(@"Images\bgMap.png");

            // Button-Styles
            BtnDefault.FlatAppearance.BorderSize = 0;
            BtnDefault.FlatStyle = FlatStyle.Flat;
            BtnDefault.BackColor = Color.Transparent;
            BtnDefault.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnDefault.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);

            BtnApply.FlatAppearance.BorderSize = 0;
            BtnApply.FlatStyle = FlatStyle.Flat;
            BtnApply.BackColor = Color.Transparent;
            BtnApply.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnApply.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);

            BtnCancel.FlatAppearance.BorderSize = 0;
            BtnCancel.FlatStyle = FlatStyle.Flat;
            BtnCancel.BackColor = Color.Transparent;
            BtnCancel.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnCancel.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);

            //Label-Styles
            LblTheme.Text = "Play theme music in launcher at start";
            LblTheme.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            LblTheme.ForeColor = Color.FromArgb(192, 145, 69);
            LblTheme.BackColor = Color.Transparent;

            LblEAX.Text = "Activate support for the EAX-Sound-System";
            LblEAX.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            LblEAX.ForeColor = Color.FromArgb(192, 145, 69);
            LblEAX.BackColor = Color.Transparent;

            LblLauncherSettings.Text = "Launcher-specific Settings";
            LblLauncherSettings.Font = new Font("SachaWynterTight", 16, FontStyle.Regular);
            LblLauncherSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblLauncherSettings.BackColor = Color.Transparent;

            //Checkbox-Styles
            ChkTheme.FlatAppearance.BorderSize = 0;
            ChkTheme.FlatStyle = FlatStyle.Flat;
            ChkTheme.BackColor = Color.Transparent;
            ChkTheme.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            ChkTheme.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselected.png");

            ChkEAX.FlatAppearance.BorderSize = 0;
            ChkEAX.FlatStyle = FlatStyle.Flat;
            ChkEAX.BackColor = Color.Transparent;
            ChkEAX.Font = new Font("Albertus MT", 16, FontStyle.Regular);
            ChkEAX.ForeColor = Color.FromArgb(192, 145, 69);

            if (Properties.Settings.Default.EAXSupport && FlagEAXFileExists && FlagEAX)
                ChkEAX.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselected.png");


            if (FlagEAXFileExists)
                FlagEAX = true;
            else
                FlagEAX = false;
            #endregion
        }
        private void BtnDefault_MouseLeave(object sender, EventArgs e)
        {
            BtnDefault.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnDefault_MouseEnter(object sender, EventArgs e)
        {
            BtnDefault.Image = Image.FromFile("Images\\btnHover.png");
            BtnDefault.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnDefault_MouseDown(object sender, MouseEventArgs e)
        {
            BtnDefault.Image = Image.FromFile("Images\\btnClick.png");
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }
        private void BtnApply_MouseLeave(object sender, EventArgs e)
        {
            BtnApply.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnApply_MouseEnter(object sender, EventArgs e)
        {
            BtnApply.Image = Image.FromFile("Images\\btnHover.png");
            BtnApply.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnApply_MouseDown(object sender, MouseEventArgs e)
        {
            BtnApply.Image = Image.FromFile("Images\\btnClick.png");
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }
        private void BtnCancel_MouseLeave(object sender, EventArgs e)
        {
            BtnCancel.Image = Image.FromFile("Images\\btnNeutral.png");
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnCancel_MouseEnter(object sender, EventArgs e)
        {
            BtnCancel.Image = Image.FromFile("Images\\btnHover.png");
            BtnCancel.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            BtnCancel.Image = Image.FromFile("Images\\btnClick.png");
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }

        private void ChkTheme_MouseEnter(object sender, EventArgs e)
        {
            if (FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkTheme_MouseLeave(object sender, EventArgs e)
        {
            if (FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void ChkTheme_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }
        private void ChkTheme_Click(object sender, EventArgs e)
        {
            if (FlagThemeMusic)
            {
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
                FlagThemeMusic = false;
            }
            else
            {
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
                FlagThemeMusic = true;
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PlayBackgroundMusic = FlagThemeMusic;
            Properties.Settings.Default.EAXSupport = FlagEAX;
            Properties.Settings.Default.Save();

            if (FlagThemeMusic)
            {
                SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                _theme.Play();
            }
            else
            {
                _theme.Stop();
                _theme.Dispose();
            }

            if (!FlagEAXFileExists && FlagEAX == true)
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Copy(Path.Combine("Tools", file), Path.Combine(ConstStrings.GameInstallPath(), file), true);
                }
            }

            if (FlagEAXFileExists && FlagEAX == false)
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Delete(Path.Combine(ConstStrings.GameInstallPath(), file));
                }
            }

            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PlayBackgroundMusic && FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselected.png");


            if (Properties.Settings.Default.EAXSupport && FlagThemeMusic)
                ChkEAX.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselected.png");

            Close();
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            FlagThemeMusic = true;
            FlagEAX = false;
            ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            ChkEAX.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void OptionsBFME1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowMover.ReleaseCapture();
                _ = WindowMover.SendMessage(Handle, WindowMover.WM_NCLBUTTONDOWN, WindowMover.HT_CAPTION, 0);
            }
        }

        private void ChkEAX_Click(object sender, EventArgs e)
        {
            if (FlagEAX == true)
            {
                ChkEAX.Image = Image.FromFile("Images\\chkUnselectedHover.png");
                FlagEAX = false;
            }
            else
            {
                ChkEAX.Image = Image.FromFile("Images\\chkSelectedHover.png");
                FlagEAX = true;
            }
        }

        private void ChkEAX_MouseEnter(object sender, EventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkEAX_MouseLeave(object sender, EventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void ChkEAX_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }
    }
}
