using Helper;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatchLauncher.Properties;
using System.Reflection;
using System.Diagnostics;

namespace PatchLauncher
{
    public partial class LauncherOptionsForm : Form
    {
        //Launcher Settings
        bool FlagWindowed = Settings.Default.StartGameWindowed;
        bool FlagBrutalAI = Settings.Default.UseBrutalAI;
        bool FlagUseBetaChannel = Settings.Default.UseBetaChannel;
        string FlagLauncherLanguageIndex = Settings.Default.LauncherLanguage;

        bool FlagIsLanguageChanged = false;
        bool FlagIsBetaChannelChanged = false;

        readonly PatchPacksBeta _patchPacksBeta = JSONDataListHelper._PatchBetaSettings;

        public LauncherOptionsForm()
        {
            InitializeComponent();

            KeyPreview = true;

            #region Styles
            //Main Form style behaviour

            PibHeader.Image = Helper.Properties.Resources.BFME1_Header;
            PibBorderLauncherOptions.Image = Helper.Properties.Resources.BFME1BorderRectangle;
            BackgroundImage = Helper.Properties.Resources.BGMap;

            // Button-Styles
            BtnApply.FlatAppearance.BorderSize = 0;
            BtnApply.FlatStyle = FlatStyle.Flat;
            BtnApply.BackColor = Color.Transparent;
            BtnApply.Image = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnApply.Font = FontHelper.GetFont(0, 16);
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);

            BtnCancel.FlatAppearance.BorderSize = 0;
            BtnCancel.FlatStyle = FlatStyle.Flat;
            BtnCancel.BackColor = Color.Transparent;
            BtnCancel.Image = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnCancel.Font = FontHelper.GetFont(0, 16);
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);

            BtnDefault.FlatAppearance.BorderSize = 0;
            BtnDefault.FlatStyle = FlatStyle.Flat;
            BtnDefault.BackColor = Color.Transparent;
            BtnDefault.Image = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnDefault.Font = FontHelper.GetFont(0, 16);
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);

            //Label-Styles
            LblLauncherSettings.Font = FontHelper.GetFont(1, 16);
            LblLauncherSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblLauncherSettings.BackColor = Color.Transparent;

            LblOptions.Font = FontHelper.GetFont(1, 20);
            LblOptions.ForeColor = Color.FromArgb(192, 145, 69);
            LblOptions.BackColor = Color.Black;

            LblLauncherVersionTitle.Font = FontHelper.GetFont(0, 16);
            LblLauncherVersionTitle.ForeColor = Color.FromArgb(136, 82, 46);
            LblLauncherVersionTitle.BackColor = Color.Transparent;

            LblPatchVersionTitle.Font = FontHelper.GetFont(0, 16);
            LblPatchVersionTitle.ForeColor = Color.FromArgb(136, 82, 46);
            LblPatchVersionTitle.BackColor = Color.Transparent;

            LblLauncherVersion.Text = Assembly.GetEntryAssembly()!.GetName().Version!.ToString();
            LblLauncherVersion.Font = FontHelper.GetFont(0, 14);
            LblLauncherVersion.ForeColor = Color.FromArgb(136, 82, 46);
            LblLauncherVersion.BackColor = Color.Transparent;

            LblPatchVersion.Font = FontHelper.GetFont(0, 14);
            LblPatchVersion.ForeColor = Color.FromArgb(136, 82, 46);
            LblPatchVersion.BackColor = Color.Transparent;

            LblWindowed.Font = FontHelper.GetFont(0, 16);
            LblWindowed.ForeColor = Color.FromArgb(192, 145, 69);
            LblWindowed.BackColor = Color.Transparent;

            LblBrutalAI.Font = FontHelper.GetFont(0, 16);
            LblBrutalAI.ForeColor = Color.FromArgb(192, 145, 69);
            LblBrutalAI.BackColor = Color.Transparent;

            LblWarning.Font = FontHelper.GetFont(0, 16);
            LblWarning.ForeColor = Color.Red;
            LblWarning.BackColor = Color.Transparent;

            LblUseBetaChannel.Font = FontHelper.GetFont(0, 16);
            LblUseBetaChannel.ForeColor = Color.FromArgb(192, 145, 69);
            LblUseBetaChannel.BackColor = Color.Transparent;

            LblLanguage.Font = FontHelper.GetFont(0, 16);
            LblLanguage.ForeColor = Color.FromArgb(192, 145, 69);
            LblLanguage.BackColor = Color.Transparent;

            if (Settings.Default.PatchVersionInstalled > 106 && !FlagUseBetaChannel)
            {
                LblPatchVersion.Text = "2.22 v " + Settings.Default.PatchVersionInstalled.ToString()[3..];
            }
            else if (Settings.Default.PatchVersionInstalled >= 103 && !FlagUseBetaChannel)
            {
                LblPatchVersion.Text = Settings.Default.PatchVersionInstalled.ToString();
            }
            else if (FlagUseBetaChannel)
            {
                LblPatchVersion.Text = _patchPacksBeta.MajorVersion.ToString() + "v" + _patchPacksBeta.MinorVersion.ToString() + " BETA " + _patchPacksBeta.Version.ToString();
            }

            if (FlagBrutalAI)
            {
                LblWarning.Text = Strings.Warning_BrutalAI;
            }
            else
            {
                LblWarning.Text = "";
            }

            if (!Settings.Default.IsGameInstalled)
            {
                ChkBrutalAI.Enabled = false;
                ChkUseBetaChannel.Enabled = false;
                LblWarning.Text = Strings.Warning_GameNotInstalled;
            }

            CmBLanguage.SelectedIndex = Settings.Default.LauncherLanguage switch
            {
                "de" => 1,
                _ => 0,
            };

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkWindowed.FlatAppearance.BorderSize = 0;
            ChkWindowed.FlatStyle = FlatStyle.Flat;
            ChkWindowed.BackColor = Color.Transparent;
            ChkWindowed.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagWindowed)
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_Unselected;

            ChkBrutalAI.FlatAppearance.BorderSize = 0;
            ChkBrutalAI.FlatStyle = FlatStyle.Flat;
            ChkBrutalAI.BackColor = Color.Transparent;
            ChkBrutalAI.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagBrutalAI)
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_Unselected;

            ChkUseBetaChannel.FlatAppearance.BorderSize = 0;
            ChkUseBetaChannel.FlatStyle = FlatStyle.Flat;
            ChkUseBetaChannel.BackColor = Color.Transparent;
            ChkUseBetaChannel.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagUseBetaChannel)
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_Unselected;

            #endregion
        }

        #region Buttons and checkboxes for launcher specific settings

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            FlagWindowed = false;
            FlagBrutalAI = false;

            ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void BtnDefault_MouseLeave(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnDefault_MouseEnter(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME1_BUTTONIMAGE_HOVER;
            BtnDefault.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnDefault_MouseDown(object sender, MouseEventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME1_BUTTONIMAGE_CLICK;
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            SaveSettings();

            if (FlagIsLanguageChanged || FlagIsBetaChannelChanged)
            {
                DialogResult _dialogResult = MessageBox.Show(Strings.Msg_Restart_Text, Strings.Msg_Restart_Title, MessageBoxButtons.YesNo);
                if (_dialogResult == DialogResult.Yes)
                {
                    Process _restarterProcess = new();
                    _restarterProcess.StartInfo.FileName = ConstStrings.C_RESTARTEREXE_FILENAME;
                    _restarterProcess.StartInfo.Arguments = "--restart --BFME1Launcher";
                    _restarterProcess.StartInfo.WorkingDirectory = Application.StartupPath;
                    _restarterProcess.StartInfo.UseShellExecute = true;
                    _restarterProcess.Start();
                    Application.Exit();
                }
                else if (_dialogResult == DialogResult.No)
                {
                    Close();
                }
            }
            Close();
        }

        private void BtnApply_MouseLeave(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnApply_MouseEnter(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BFME1_BUTTONIMAGE_HOVER;
            BtnApply.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnApply_MouseDown(object sender, MouseEventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BFME1_BUTTONIMAGE_CLICK;
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnCancel_MouseLeave(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME1_BUTTONIMAGE_NEUTR;
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnCancel_MouseEnter(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME1_BUTTONIMAGE_HOVER;
            BtnCancel.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME1_BUTTONIMAGE_CLICK;
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void SaveSettings()
        {
            //Save Launcher-Settings
            Settings.Default.UseBrutalAI = FlagBrutalAI;

            if (FlagLauncherLanguageIndex != Settings.Default.LauncherLanguage)
            {
                FlagIsLanguageChanged = true;
            }

            if (FlagUseBetaChannel != Settings.Default.UseBetaChannel)
            {
                FlagIsBetaChannelChanged = true;
                Settings.Default.BetaChannelVersion = 0;
            }

            Settings.Default.LauncherLanguage = FlagLauncherLanguageIndex;
            Settings.Default.UseBetaChannel = FlagUseBetaChannel;
            Settings.Default.StartGameWindowed = FlagWindowed;

            if (FlagUseBetaChannel)
                Settings.Default.SelectedOlderPatch = false;

            if (FlagIsBetaChannelChanged && !Settings.Default.UseBetaChannel)
            {
                Settings.Default.PatchVersionInstalled = Settings.Default.LatestPatchVersion - 1;
            }

            Settings.Default.Save();

            //Settings-Valuations

            string GameInstallPath = RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName);

            if (FlagBrutalAI && GameInstallPath != null)
                File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, "_patch222LibrariesBrutalAI.big"), Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big"), true);
            else if (GameInstallPath != null && File.Exists(Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big")))
                File.Delete(Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big"));
        }

        private void ChkWindowed_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagWindowed == true)
            {
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagWindowed = false;
            }
            else
            {
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagWindowed = true;
            }
        }

        private void ChkWindowed_MouseEnter(object sender, EventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkWindowed_MouseLeave(object sender, EventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkWindowed_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkBrutalAI_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagBrutalAI == true)
            {
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                LblWarning.Text = "";
                FlagBrutalAI = false;
            }
            else
            {
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                LblWarning.Text = Strings.Warning_BrutalAI;
                FlagBrutalAI = true;
            }
        }

        private void ChkBrutalAI_MouseEnter(object sender, EventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkBrutalAI_MouseLeave(object sender, EventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkBrutalAI_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkUseBetaChannel_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagUseBetaChannel == true)
            {
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagUseBetaChannel = false;

            }
            else
            {
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagUseBetaChannel = true;
            }
        }

        private void ChkUseBetaChannel_MouseEnter(object sender, EventArgs e)
        {
            if (FlagUseBetaChannel)
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkUseBetaChannel_MouseLeave(object sender, EventArgs e)
        {
            if (FlagUseBetaChannel)
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkUseBetaChannel_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagUseBetaChannel)
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        #endregion

        private void OptionsBFME1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void CmBLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CmBLanguage.SelectedIndex)
            {
                case 0:
                    FlagLauncherLanguageIndex = "en";
                    break;
                case 1:
                    FlagLauncherLanguageIndex = "de";
                    break;
                default:
                    break;
            }
        }
    }
}
