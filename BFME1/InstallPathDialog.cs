using PatchLauncher.Helper;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class InstallPathDialog : Form
    {
        public InstallPathDialog()
        {
            InitializeComponent();

            LblChooseDir.Text = "Where do you want to install the game \"The Battle for Middle-earth\"?";
            LblChooseDir.Font = ConstStrings.UseFont("Albertus Nova", 12);
            LblChooseDir.ForeColor = Color.FromArgb(192, 145, 69);
            LblChooseDir.BackColor = Color.Transparent;

            BtnChoose.FlatAppearance.BorderSize = 0;
            BtnChoose.FlatStyle = FlatStyle.Flat;
            BtnChoose.BackColor = Color.Transparent;
            BtnChoose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnChoose.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnChoose.ForeColor = Color.FromArgb(192, 145, 69);

            BtnAccept.FlatAppearance.BorderSize = 0;
            BtnAccept.FlatStyle = FlatStyle.Flat;
            BtnAccept.BackColor = Color.Transparent;
            BtnAccept.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnAccept.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);

            TxtInstallPath.BackColor = Color.Black;
            TxtInstallPath.Font = ConstStrings.UseFont("Albertus Nova", 14);
            TxtInstallPath.ForeColor = Color.FromArgb(192, 145, 69);

            if (Properties.Settings.Default.GameInstallPath != "")
            {
                TxtInstallPath.Text = Properties.Settings.Default.GameInstallPath;
            }
            else if (RegistryService.ReadRegKey("path") != null)
            {
                TxtInstallPath.Text = RegistryService.ReadRegKey("path");
            }
            else
            {
                TxtInstallPath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), ConstStrings.C_GAMEFOLDER_NAME);
            }
        }

        private void BtnChoose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _installPath = new()
            {
                ShowNewFolderButton = true
            };
            DialogResult _result = _installPath.ShowDialog();

            if (_result == DialogResult.OK)
            {
                TxtInstallPath.Text = Path.Combine(_installPath.SelectedPath, ConstStrings.C_GAMEFOLDER_NAME);
            }
        }

        private void BtnChoose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnChoose.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }

        private void BtnChoose_MouseEnter(object sender, EventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnChoose.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnChoose_MouseLeave(object sender, EventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
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
            BtnAccept.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }

        private void BtnAccept_MouseEnter(object sender, EventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnAccept.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnAccept_MouseLeave(object sender, EventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);
        }
    }
}
