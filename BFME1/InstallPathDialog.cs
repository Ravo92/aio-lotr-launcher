using Helper;
using PatchLauncher.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class InstallPathDialog : Form
    {
        int FlagSelectedGameLanguage = 1;
        bool FlagCreateDesktopShortcut = true;
        bool FlagCreateStartMenuShortcut = true;

        public InstallPathDialog()
        {
            switch (Settings.Default.Language)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                    break;
                default:
                    break;
            }

            InitializeComponent();

            LblChooseDir.Font = FontHelper.GetFont(0, 12);
            LblChooseDir.ForeColor = Color.FromArgb(192, 145, 69);
            LblChooseDir.BackColor = Color.Transparent;

            LblSelectGameLanguage.Font = FontHelper.GetFont(0, 12);
            LblSelectGameLanguage.ForeColor = Color.FromArgb(192, 145, 69);
            LblSelectGameLanguage.BackColor = Color.Transparent;

            LblDesktopShortCut.Font = FontHelper.GetFont(0, 16);
            LblDesktopShortCut.ForeColor = Color.FromArgb(192, 145, 69);
            LblDesktopShortCut.BackColor = Color.Transparent;

            LblStartMenuShortCut.Font = FontHelper.GetFont(0, 16);
            LblStartMenuShortCut.ForeColor = Color.FromArgb(192, 145, 69);
            LblStartMenuShortCut.BackColor = Color.Transparent;

            BtnChoose.FlatAppearance.BorderSize = 0;
            BtnChoose.FlatStyle = FlatStyle.Flat;
            BtnChoose.BackColor = Color.Transparent;
            BtnChoose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnChoose.Font = FontHelper.GetFont(0, 14);
            BtnChoose.ForeColor = Color.FromArgb(192, 145, 69);

            BtnAccept.FlatAppearance.BorderSize = 0;
            BtnAccept.FlatStyle = FlatStyle.Flat;
            BtnAccept.BackColor = Color.Transparent;
            BtnAccept.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnAccept.Font = FontHelper.GetFont(0, 14);
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);

            TxtInstallPath.BackColor = Color.FromArgb(0, 0, 0);
            TxtInstallPath.Font = FontHelper.GetFont(0, 16);
            TxtInstallPath.ForeColor = Color.FromArgb(192, 145, 69);

            ChkDesktopShortcut.FlatAppearance.BorderSize = 0;
            ChkDesktopShortcut.FlatStyle = FlatStyle.Flat;
            ChkDesktopShortcut.BackColor = Color.Transparent;
            ChkDesktopShortcut.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagCreateDesktopShortcut)
            {
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkSelected;
            }
            else
            {
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkUnselected;
            }

            ChkStartMenuShortcut.FlatAppearance.BorderSize = 0;
            ChkStartMenuShortcut.FlatStyle = FlatStyle.Flat;
            ChkStartMenuShortcut.BackColor = Color.Transparent;
            ChkStartMenuShortcut.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagCreateStartMenuShortcut)
            {
                Settings.Default.CreateStartMenuShortcut = true;
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkSelected;
            }
            else
            {
                Settings.Default.CreateStartMenuShortcut = false;
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkUnselected;
            }

            CmbSelectGameLanguage.SelectedIndex = Settings.Default.GameLanguageIndex;
            PibLanguageSupport.Image = Helper.Properties.Resources.languageSupportTransparent;
            PibPathBorder.Image = Helper.Properties.Resources.borderRectangleSmallest;
            PibLanguageSupport.BackColor = Color.Transparent;
            BackgroundImage = Helper.Properties.Resources.bgMap;

            if (Settings.Default.GameInstallPath != "")
            {
                TxtInstallPath.Text = Settings.Default.GameInstallPath;
            }
            else if (RegistryService.ReadRegKey("path") != "ValueNotFound")
            {
                TxtInstallPath.Text = RegistryService.ReadRegKey("path");
            }
            else
            {
                TxtInstallPath.Text = Path.Combine(Application.StartupPath, ConstStrings.C_GAMEINSTALLFOLDER_NAME, ConstStrings.C_GAMEFOLDER_NAME_BFME1);
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
                TxtInstallPath.Text = Path.Combine(_installPath.SelectedPath, ConstStrings.C_GAMEINSTALLFOLDER_NAME, ConstStrings.C_GAMEFOLDER_NAME_BFME1);
            }
        }

        private void BtnChoose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnChoose.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnChoose_MouseEnter(object sender, EventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnChoose.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnChoose_MouseLeave(object sender, EventArgs e)
        {
            BtnChoose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnChoose.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            Settings.Default.GameInstallPath = TxtInstallPath.Text;
            Settings.Default.GameLanguage = FlagSelectedGameLanguage;
            Settings.Default.GameLanguageIndex = CmbSelectGameLanguage.SelectedIndex;
            Settings.Default.InstalledLanguagePackName = GameLanguageHelper.LanguagPackName;
            Settings.Default.CreateDesktopShortcut = FlagCreateDesktopShortcut;
            Settings.Default.CreateStartMenuShortcut = FlagCreateStartMenuShortcut;
            Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnAccept_MouseDown(object sender, MouseEventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnAccept_MouseEnter(object sender, EventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnAccept.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnAccept_MouseLeave(object sender, EventArgs e)
        {
            BtnAccept.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnAccept.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void ChkDesktopShortcut_Click(object sender, EventArgs e)
        {
            if (FlagCreateDesktopShortcut == true)
            {
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagCreateDesktopShortcut = false;
            }
            else
            {
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagCreateDesktopShortcut = true;
            }
        }

        private void ChkDesktopShortcut_MouseLeave(object sender, EventArgs e)
        {
            if (FlagCreateDesktopShortcut)
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkDesktopShortcut_MouseEnter(object sender, EventArgs e)
        {
            if (FlagCreateDesktopShortcut)
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkDesktopShortcut_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagCreateDesktopShortcut)
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkDesktopShortcut.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkStartMenuShortcut_Click(object sender, EventArgs e)
        {
            if (FlagCreateStartMenuShortcut == true)
            {
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagCreateStartMenuShortcut = false;
            }
            else
            {
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagCreateStartMenuShortcut = true;
            }
        }

        private void ChkStartMenuShortcut_MouseLeave(object sender, EventArgs e)
        {
            if (FlagCreateStartMenuShortcut)
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkStartMenuShortcut_MouseEnter(object sender, EventArgs e)
        {
            if (FlagCreateStartMenuShortcut)
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkStartMenuShortcut_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagCreateStartMenuShortcut)
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkStartMenuShortcut.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void CmbSelectGameLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Index List of Languages -> CmbSelectGameLanguage.SelectedIndex
            //   1 = English | Registry "LanguageName" = English US | "Language" = English | "Locale" = en_us
            //   2 = Français | Registry "LanguageName" = French | "Language" = French | "Locale" = fr_fr
            //   3 = Deutsch | Registry "LanguageName" = German | "Language" = German | "Locale" = de
            //   4 = Italiano | Registry "LanguageName" = Italian | "Language" = Italian | "Locale" = it
            //   5 = Español | Registry "LanguageName" = Spanish | "Language" = English | "Locale" = es
            //   6 = Svenska | Registry "LanguageName" = Swedish | "Language" = Swedish | "Locale" = sv
            //   8 = Nederlands | Registry "LanguageName" = Dutch | "Language" = Dutch | "Locale" = nl
            //   14 = Polski | Registry "LanguageName" = Polish | "Language" = Polish | "Locale" = pl
            //   16 = Norsk | Registry "LanguageName" = Norwegian | "Language" = Norwegian | "Locale" = no

            switch (CmbSelectGameLanguage.SelectedIndex)
            {
                case 0:
                    FlagSelectedGameLanguage = 1;
                    GameLanguageHelper.RegistrySelectedLanguageName = "English US";
                    GameLanguageHelper.RegistrySelectedLanguage = "English";
                    GameLanguageHelper.RegistrySelectedLocale = "en_us";
                    GameLanguageHelper.LanguagPackName = "LangPack_EN.7z";
                    break;
                case 1:
                    FlagSelectedGameLanguage = 2;
                    GameLanguageHelper.RegistrySelectedLanguageName = "French";
                    GameLanguageHelper.RegistrySelectedLanguage = "French";
                    GameLanguageHelper.RegistrySelectedLocale = "fr_fr";
                    GameLanguageHelper.LanguagPackName = "LangPack_FR.7z";
                    break;
                case 2:
                    FlagSelectedGameLanguage = 3;
                    GameLanguageHelper.RegistrySelectedLanguageName = "German";
                    GameLanguageHelper.RegistrySelectedLanguage = "German";
                    GameLanguageHelper.RegistrySelectedLocale = "de";
                    GameLanguageHelper.LanguagPackName = "LangPack_DE.7z";
                    break;
                case 3:
                    FlagSelectedGameLanguage = 4;
                    GameLanguageHelper.RegistrySelectedLanguageName = "Italian";
                    GameLanguageHelper.RegistrySelectedLanguage = "Italian";
                    GameLanguageHelper.RegistrySelectedLocale = "it";
                    GameLanguageHelper.LanguagPackName = "LangPack_IT.7z";
                    break;
                case 4:
                    FlagSelectedGameLanguage = 5;
                    GameLanguageHelper.RegistrySelectedLanguageName = "Spanish";
                    GameLanguageHelper.RegistrySelectedLanguage = "Spanish";
                    GameLanguageHelper.RegistrySelectedLocale = "es";
                    GameLanguageHelper.LanguagPackName = "LangPack_ES.7z";
                    break;
                case 5:
                    FlagSelectedGameLanguage = 6;
                    GameLanguageHelper.RegistrySelectedLanguageName = "Swedish";
                    GameLanguageHelper.RegistrySelectedLanguage = "Swedish";
                    GameLanguageHelper.RegistrySelectedLocale = "sv";
                    GameLanguageHelper.LanguagPackName = "LangPack_SV.7z";
                    break;
                case 6:
                    FlagSelectedGameLanguage = 8;
                    GameLanguageHelper.RegistrySelectedLanguageName = "Dutch";
                    GameLanguageHelper.RegistrySelectedLanguage = "Dutch";
                    GameLanguageHelper.RegistrySelectedLocale = "nl";
                    GameLanguageHelper.LanguagPackName = "LangPack_NL.7z";
                    break;
                case 7:
                    FlagSelectedGameLanguage = 14;
                    GameLanguageHelper.RegistrySelectedLanguageName = "Polish";
                    GameLanguageHelper.RegistrySelectedLanguage = "Polish";
                    GameLanguageHelper.RegistrySelectedLocale = "pl";
                    GameLanguageHelper.LanguagPackName = "LangPack_PL.7z";
                    break;
                case 8:
                    FlagSelectedGameLanguage = 16;
                    GameLanguageHelper.RegistrySelectedLanguageName = "Norwegian";
                    GameLanguageHelper.RegistrySelectedLanguage = "Norwegian";
                    GameLanguageHelper.RegistrySelectedLocale = "no";
                    GameLanguageHelper.LanguagPackName = "LangPack_NO.7z";
                    break;
                default:
                    FlagSelectedGameLanguage = 1;
                    GameLanguageHelper.RegistrySelectedLanguageName = "English US";
                    GameLanguageHelper.RegistrySelectedLanguage = "English";
                    GameLanguageHelper.RegistrySelectedLocale = "en_us";
                    GameLanguageHelper.LanguagPackName = "LangPack_EN.7z";
                    break;
            }
        }
    }
}
