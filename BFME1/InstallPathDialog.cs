using PatchLauncher.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class InstallPathDialog : Form
    {
        readonly ConstStrings _font = new();
        public InstallPathDialog()
        {
            InitializeComponent();

            LblChooseDir.Text = "Where do you want to install the game \"The Battle for Middle-earth\"?";
            LblChooseDir.Font = _font.UseFont("Albertus Nova", 12);
            LblChooseDir.ForeColor = Color.FromArgb(192, 145, 69);
            LblChooseDir.BackColor = Color.Transparent;

            BtnChoose.FlatAppearance.BorderSize = 0;
            BtnChoose.FlatStyle = FlatStyle.Flat;
            BtnChoose.BackColor = Color.Transparent;
            BtnChoose.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnChoose.Font = _font.UseFont("Albertus Nova", 14);
            BtnChoose.ForeColor = Color.FromArgb(192, 145, 69);

            BtnAccept.FlatAppearance.BorderSize = 0;
            BtnAccept.FlatStyle = FlatStyle.Flat;
            BtnAccept.BackColor = Color.Transparent;
            BtnAccept.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnAccept.Font = _font.UseFont("Albertus Nova", 14);
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);

            TxtInstallPath.BackColor = Color.Black;
            TxtInstallPath.Font = _font.UseFont("Albertus Nova", 14);
            TxtInstallPath.ForeColor = Color.FromArgb(192, 145, 69);

            if (Properties.Settings.Default.GameInstallPath != "")
            {
                TxtInstallPath.Text = Properties.Settings.Default.GameInstallPath;
            }
            else
            {
                TxtInstallPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + ConstStrings.gameFolderName;
            }
        }

        private void BtnChoose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _installPath = new();
            _installPath.ShowNewFolderButton = true;
            DialogResult _result = _installPath.ShowDialog();

            if (_result == DialogResult.OK)
            {
                TxtInstallPath.Text = _installPath.SelectedPath + "\\" + ConstStrings.gameFolderName;
            }
        }

        private void BtnChoose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.ButtonImageClick();
            BtnChoose.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }

        private void BtnChoose_MouseEnter(object sender, EventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.ButtonImageHover();
            BtnChoose.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnChoose_MouseLeave(object sender, EventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnChoose.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GameInstallPath = TxtInstallPath.Text;
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnAccept_MouseDown(object sender, MouseEventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.ButtonImageClick();
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }

        private void BtnAccept_MouseEnter(object sender, EventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.ButtonImageHover();
            BtnAccept.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnAccept_MouseLeave(object sender, EventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.ButtonImageNeutral();
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);
        }
    }
}
