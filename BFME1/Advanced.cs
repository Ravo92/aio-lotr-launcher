using Helper;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class Advanced : Form
    {
        public Advanced()
        {
            InitializeComponent();

            KeyPreview = true;

            BtnOpenAppDataFolder.FlatAppearance.BorderSize = 0;
            BtnOpenAppDataFolder.FlatStyle = FlatStyle.Flat;
            BtnOpenAppDataFolder.BackColor = Color.Transparent;
            BtnOpenAppDataFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnOpenAppDataFolder.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnOpenAppDataFolder.ForeColor = Color.FromArgb(192, 145, 69);

            BtnGameInstallFolder.FlatAppearance.BorderSize = 0;
            BtnGameInstallFolder.FlatStyle = FlatStyle.Flat;
            BtnGameInstallFolder.BackColor = Color.Transparent;
            BtnGameInstallFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnGameInstallFolder.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnGameInstallFolder.ForeColor = Color.FromArgb(192, 145, 69);

            BtnLauncherFolder.FlatAppearance.BorderSize = 0;
            BtnLauncherFolder.FlatStyle = FlatStyle.Flat;
            BtnLauncherFolder.BackColor = Color.Transparent;
            BtnLauncherFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnLauncherFolder.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnLauncherFolder.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnOpenAppDataFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", ConstStrings.GameAppdataFolderPath());
        }

        private void BtnOpenAppDataFolder_MouseEnter(object sender, EventArgs e)
        {
            BtnOpenAppDataFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnOpenAppDataFolder.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnOpenAppDataFolder_MouseLeave(object sender, EventArgs e)
        {
            BtnOpenAppDataFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnOpenAppDataFolder.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnOpenAppDataFolder_MouseDown(object sender, MouseEventArgs e)
        {
            BtnOpenAppDataFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnOpenAppDataFolder.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnGameInstallFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", ConstStrings.GameInstallPath());
        }

        private void BtnGameInstallFolder_MouseEnter(object sender, EventArgs e)
        {
            BtnGameInstallFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnGameInstallFolder.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnGameInstallFolder_MouseLeave(object sender, EventArgs e)
        {
            BtnGameInstallFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnGameInstallFolder.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnGameInstallFolder_MouseDown(object sender, MouseEventArgs e)
        {
            BtnGameInstallFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnGameInstallFolder.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnLauncherFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.StartupPath);
        }

        private void BtnLauncherFolder_MouseEnter(object sender, EventArgs e)
        {
            BtnLauncherFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnLauncherFolder.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnLauncherFolder_MouseLeave(object sender, EventArgs e)
        {
            BtnLauncherFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnLauncherFolder.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnLauncherFolder_MouseDown(object sender, MouseEventArgs e)
        {
            BtnLauncherFolder.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnLauncherFolder.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void Advanced_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
