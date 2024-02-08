using Helper;
using PatchLauncher.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class InstallPathDialog : Form
    {
        bool FlagCreateDesktopShortcut = true;
        bool FlagCreateStartMenuShortcut = true;
        string FlagSelectedIsoCode = "en_uk";

        readonly Dictionary<string, string> _selectedLanguageDictionary = JSONDataListHelper._DictionarylanguageSettings.ToDictionary(x => x.Key, x => x.Value.RegistrySelectedLanguage);

        public InstallPathDialog()
        {
            InitializeComponent();

            LblChooseDir.Font = FontHelper.GetFont(0, 12);
            LblChooseDir.ForeColor = Color.FromArgb(114, 153, 169);
            LblChooseDir.BackColor = Color.Transparent;

            LblFreeSpace.Font = FontHelper.GetFont(0, 12);
            LblFreeSpace.ForeColor = Color.FromArgb(114, 153, 169);
            LblFreeSpace.BackColor = Color.Transparent;

            LblNeededSpace.Font = FontHelper.GetFont(0, 12);
            LblNeededSpace.ForeColor = Color.FromArgb(114, 153, 169);
            LblNeededSpace.BackColor = Color.Transparent;

            LblSelectGameLanguage.Font = FontHelper.GetFont(0, 12);
            LblSelectGameLanguage.ForeColor = Color.FromArgb(114, 153, 169);
            LblSelectGameLanguage.BackColor = Color.Transparent;

            LblDesktopShortCut.Font = FontHelper.GetFont(0, 16);
            LblDesktopShortCut.ForeColor = Color.FromArgb(114, 153, 169);
            LblDesktopShortCut.BackColor = Color.Transparent;

            LblStartMenuShortCut.Font = FontHelper.GetFont(0, 16);
            LblStartMenuShortCut.ForeColor = Color.FromArgb(114, 153, 169);
            LblStartMenuShortCut.BackColor = Color.Transparent;

            BtnChoose.FlatAppearance.BorderSize = 0;
            BtnChoose.FlatStyle = FlatStyle.Flat;
            BtnChoose.BackColor = Color.Transparent;
            BtnChoose.BackgroundImage = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnChoose.Font = FontHelper.GetFont(0, 14);
            BtnChoose.ForeColor = Color.FromArgb(114, 153, 169);

            BtnAccept.FlatAppearance.BorderSize = 0;
            BtnAccept.FlatStyle = FlatStyle.Flat;
            BtnAccept.BackColor = Color.Transparent;
            BtnAccept.BackgroundImage = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnAccept.Font = FontHelper.GetFont(0, 14);
            BtnAccept.ForeColor = Color.FromArgb(114, 153, 169);

            TxtInstallPath.BackColor = Color.FromArgb(0, 0, 0);
            TxtInstallPath.Font = FontHelper.GetFont(0, 16);
            TxtInstallPath.ForeColor = Color.FromArgb(114, 153, 169);

            ChkDesktopShortcut.FlatAppearance.BorderSize = 0;
            ChkDesktopShortcut.FlatStyle = FlatStyle.Flat;
            ChkDesktopShortcut.BackColor = Color.Transparent;
            ChkDesktopShortcut.ForeColor = Color.FromArgb(114, 153, 169);

            if (FlagCreateDesktopShortcut)
            {
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            }
            else
            {
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_Unselected;
            }

            ChkStartMenuShortcut.FlatAppearance.BorderSize = 0;
            ChkStartMenuShortcut.FlatStyle = FlatStyle.Flat;
            ChkStartMenuShortcut.BackColor = Color.Transparent;
            ChkStartMenuShortcut.ForeColor = Color.FromArgb(114, 153, 169);

            if (FlagCreateStartMenuShortcut)
            {
                Settings.Default.CreateStartMenuShortcut = true;
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            }
            else
            {
                Settings.Default.CreateStartMenuShortcut = false;
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_Unselected;
            }

            PibLanguageSupport.Image = Helper.Properties.Resources.BFME25LanguageSupportTransparent;
            PibPathBorder.Image = Helper.Properties.Resources.BFME25BorderRectangleSmallest;
            PibLanguageSupport.BackColor = Color.Transparent;
            BackgroundImage = Helper.Properties.Resources.BGMap;

            if (Settings.Default.GameInstallPath != "")
            {
                TxtInstallPath.Text = Settings.Default.GameInstallPath;
            }
            else if (RegistryService.ReadRegKeyBFME25("path") != "ValueNotFound")
            {
                TxtInstallPath.Text = RegistryService.ReadRegKeyBFME25("path");
            }
            else
            {
                TxtInstallPath.Text = Path.Combine(Application.StartupPath, ConstStrings.C_GAMEINSTALLFOLDER_NAME, ConstStrings.C_GAMEFOLDER_NAME_BFME25);
            }

            CmbSelectGameLanguage.DisplayMember = "Value";
            CmbSelectGameLanguage.ValueMember = "Key";
            CmbSelectGameLanguage.DataSource = new BindingSource(_selectedLanguageDictionary, null);
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
                TxtInstallPath.Text = Path.Combine(_installPath.SelectedPath, ConstStrings.C_GAMEINSTALLFOLDER_NAME, ConstStrings.C_GAMEFOLDER_NAME_BFME25);
            }
        }

        private void BtnChoose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BFME25_BUTTONIMAGE_CLICK;
            BtnChoose.ForeColor = Color.FromArgb(114, 153, 169);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnChoose_MouseEnter(object sender, EventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BFME25_BUTTONIMAGE_HOVER;
            BtnChoose.ForeColor = Color.FromArgb(24, 63, 20);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnChoose_MouseLeave(object sender, EventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnChoose.ForeColor = Color.FromArgb(114, 153, 169);
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            Settings.Default.GameInstallPath = TxtInstallPath.Text;
            Settings.Default.InstalledLanguageISOCode = FlagSelectedIsoCode;
            Settings.Default.CreateDesktopShortcut = FlagCreateDesktopShortcut;
            Settings.Default.CreateStartMenuShortcut = FlagCreateStartMenuShortcut;
            Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnAccept_MouseDown(object sender, MouseEventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.C_BFME25_BUTTONIMAGE_CLICK;
            BtnAccept.ForeColor = Color.FromArgb(114, 153, 169);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnAccept_MouseEnter(object sender, EventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.C_BFME25_BUTTONIMAGE_HOVER;
            BtnAccept.ForeColor = Color.FromArgb(24, 63, 20);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnAccept_MouseLeave(object sender, EventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnAccept.ForeColor = Color.FromArgb(114, 153, 169);
        }

        private void ChkDesktopShortcut_Click(object sender, EventArgs e)
        {
            if (FlagCreateDesktopShortcut == true)
            {
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
                FlagCreateDesktopShortcut = false;
            }
            else
            {
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
                FlagCreateDesktopShortcut = true;
            }
        }

        private void ChkDesktopShortcut_MouseLeave(object sender, EventArgs e)
        {
            if (FlagCreateDesktopShortcut)
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            else
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_Unselected;
        }

        private void ChkDesktopShortcut_MouseEnter(object sender, EventArgs e)
        {
            if (FlagCreateDesktopShortcut)
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void ChkDesktopShortcut_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagCreateDesktopShortcut)
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkDesktopShortcut.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void ChkStartMenuShortcut_Click(object sender, EventArgs e)
        {
            if (FlagCreateStartMenuShortcut == true)
            {
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
                FlagCreateStartMenuShortcut = false;
            }
            else
            {
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
                FlagCreateStartMenuShortcut = true;
            }
        }

        private void ChkStartMenuShortcut_MouseLeave(object sender, EventArgs e)
        {
            if (FlagCreateStartMenuShortcut)
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            else
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_Unselected;
        }

        private void ChkStartMenuShortcut_MouseEnter(object sender, EventArgs e)
        {
            if (FlagCreateStartMenuShortcut)
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void ChkStartMenuShortcut_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagCreateStartMenuShortcut)
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void CmbSelectGameLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = (ComboBox)sender;
            string isoCode = (string)control.SelectedValue;
            var settings = JSONDataListHelper._DictionarylanguageSettings[isoCode];
            FlagSelectedIsoCode = settings.RegistrySelectedLocale;
        }

        private void TxtInstallPath_TextChanged(object sender, EventArgs e)
        {
            int totalDiskSpace = GameFileTools.CheckIfThereIsEnoughDiskSpace(TxtInstallPath.Text).totalDiskSpace;
            int totalFreeDiskSpace = GameFileTools.CheckIfThereIsEnoughDiskSpace(TxtInstallPath.Text).totalFreeDiskSpace;
            string driveLetter = GameFileTools.CheckIfThereIsEnoughDiskSpace(TxtInstallPath.Text).driveLetter;

            if (Settings.Default.LauncherLanguage == "de")
                LblFreeSpace.Text = "Freier Platz: " + totalFreeDiskSpace.ToString() + " GB / Gesamter Platz: " + totalDiskSpace.ToString() + " GB";
            else
                LblFreeSpace.Text = "Free Space: " + totalFreeDiskSpace.ToString() + " GB / Total Space: " + totalDiskSpace.ToString() + " GB";

            if (totalFreeDiskSpace <= 10)
            {
                if (Settings.Default.LauncherLanguage == "de")
                    MessageBox.Show(string.Format("Auf der Partition \"{0}\" ist nicht genügend freier Platz vorhanden.\nBitte etwas Platz freischaufeln und dann erneut versuchen!", driveLetter), "Nicht genug freier Speicherplatz", MessageBoxButtons.OK);
                else
                    MessageBox.Show(string.Format("There is not enough disk space on the partition \"{0}\".\nPlease free some space before you can install the game", driveLetter), "Not enough free space", MessageBoxButtons.OK);

                BtnAccept.Enabled = false;
            }
            else
            {
                BtnAccept.Enabled = true;
            }
        }
    }
}
