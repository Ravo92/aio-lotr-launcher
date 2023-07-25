using Helper;
using System;
using System.Collections.Generic;
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
        readonly static string GameInstallPath = RegistryService.GameInstallPath();
        readonly bool FlagEAXFileExists = File.Exists(GameInstallPath + @"\dsound.dll");

        //Launcher Settings
        bool FlagEAX = Settings.Default.EAXSupport;
        bool FlagWindowed = Settings.Default.StartGameWindowed;
        bool FlagBrutalAI = Settings.Default.UseBrutalAI;
        bool FlagShowPatchesFirst = Settings.Default.ShowPatchesFirst;
        bool FlagUseBetaChannel = Settings.Default.UseBetaChannel;
        int FlagLanguageIndex = Settings.Default.Language;

        bool FlagIsLanguageChanged = false;
        bool FlagIsBetaChannelChanged = false;

        readonly PatchPacksBeta _patchPacksBeta = JSONDataListHelper._PatchBetaSettings;//[Settings.Default.LatestBetaPatchVersion];

        public LauncherOptionsForm()
        {
            InitializeComponent();

            KeyPreview = true;

            #region Styles
            //Main Form style behaviour

            PibHeader.Image = Helper.Properties.Resources.BFME1_Header;
            PibBorderLauncherOptions.Image = Helper.Properties.Resources.borderRectangle;
            BackgroundImage = Helper.Properties.Resources.BGMap;

            CmBLanguage.SelectedIndex = FlagLanguageIndex;

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
            LblEAX.Font = FontHelper.GetFont(0, 16);
            LblEAX.ForeColor = Color.FromArgb(192, 145, 69);
            LblEAX.BackColor = Color.Transparent;

            LblLauncherSettings.Font = FontHelper.GetFont(1, 16);
            LblLauncherSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblLauncherSettings.BackColor = Color.Transparent;

            LblOptions.Font = FontHelper.GetFont(1, 20);
            LblOptions.ForeColor = Color.FromArgb(192, 145, 69);
            LblOptions.BackColor = Color.Black;

            LblLauncherVersion.Text += Assembly.GetEntryAssembly()!.GetName().Version;
            LblLauncherVersion.Font = FontHelper.GetFont(0, 16);
            LblLauncherVersion.ForeColor = Color.FromArgb(136, 82, 46);
            LblLauncherVersion.BackColor = Color.Transparent;

            LblPatchVersion.Font = FontHelper.GetFont(0, 16);
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

            LblShowPatchesFirst.Font = FontHelper.GetFont(0, 16);
            LblShowPatchesFirst.ForeColor = Color.FromArgb(192, 145, 69);
            LblShowPatchesFirst.BackColor = Color.Transparent;

            LblUseBetaChannel.Font = FontHelper.GetFont(0, 16);
            LblUseBetaChannel.ForeColor = Color.FromArgb(192, 145, 69);
            LblUseBetaChannel.BackColor = Color.Transparent;

            LblLanguage.Font = FontHelper.GetFont(0, 16);
            LblLanguage.ForeColor = Color.FromArgb(192, 145, 69);
            LblLanguage.BackColor = Color.Transparent;

            if (Settings.Default.PatchVersionInstalled < 103 && !FlagUseBetaChannel)
            {
                LblPatchVersion.Text += "2.22v" + Settings.Default.PatchVersionInstalled.ToString();
            }
            else if (FlagUseBetaChannel)
            {
                LblPatchVersion.Text += _patchPacksBeta.MajorVersion.ToString() + "v" + _patchPacksBeta.MinorVersion.ToString() + " BETA " + _patchPacksBeta.Version.ToString();
            }

            if (FlagBrutalAI)
            {
                LblWarning.Show();
            }
            else
            {
                LblWarning.Hide();
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkEAX.FlatAppearance.BorderSize = 0;
            ChkEAX.FlatStyle = FlatStyle.Flat;
            ChkEAX.BackColor = Color.Transparent;
            ChkEAX.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagEAXFileExists)
                FlagEAX = true;
            else
                FlagEAX = false;

            if (FlagEAXFileExists && FlagEAX)
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Unselected;

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

            ChkShowPatchesFirst.FlatAppearance.BorderSize = 0;
            ChkShowPatchesFirst.FlatStyle = FlatStyle.Flat;
            ChkShowPatchesFirst.BackColor = Color.Transparent;
            ChkShowPatchesFirst.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagShowPatchesFirst)
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_Unselected;

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
            FlagEAX = false;
            FlagWindowed = false;
            FlagBrutalAI = false;
            FlagShowPatchesFirst = false;
            ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
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
            Settings.Default.EAXSupport = FlagEAX;
            Settings.Default.UseBrutalAI = FlagBrutalAI;
            Settings.Default.ShowPatchesFirst = FlagShowPatchesFirst;

            if (FlagLanguageIndex != Settings.Default.Language)
            {
                FlagIsLanguageChanged = true;
            }

            if (FlagUseBetaChannel != Settings.Default.UseBetaChannel)
            {
                FlagIsBetaChannelChanged = true;
                Settings.Default.BetaChannelVersion = 0;
            }

            Settings.Default.Language = FlagLanguageIndex;
            Settings.Default.UseBetaChannel = FlagUseBetaChannel;
            Settings.Default.StartGameWindowed = FlagWindowed;
            Settings.Default.Save();

            //Settings-Valuations

            if (!FlagEAXFileExists && FlagEAX == true)
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, file), Path.Combine(GameInstallPath, file), true);
                }

                OptionIniParser.WriteKey("UseEAX3", "yes");
            }

            if (FlagEAXFileExists && FlagEAX == false)
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Delete(Path.Combine(GameInstallPath, file));
                }

                OptionIniParser.WriteKey("UseEAX3", "no");
            }

            if (FlagBrutalAI && GameInstallPath != null)
                File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, "_patch222LibrariesBrutalAI.big"), Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big"), true);
            else if (GameInstallPath != null && File.Exists(Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big")))
                File.Delete(Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big"));
        }

        private void ChkShowPatchesFirst_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagShowPatchesFirst == true)
            {
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagShowPatchesFirst = false;
            }
            else
            {
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagShowPatchesFirst = true;
            }
        }

        private void ChkShowPatchesFirst_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowPatchesFirst)
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkShowPatchesFirst_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowPatchesFirst)
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkShowPatchesFirst_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowPatchesFirst)
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkShowPatchesFirst.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkEAX_Click(object sender, EventArgs e)
        {
            if (FlagShowPatchesFirst == true)
            {
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagEAX = false;
            }
            else
            {
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagEAX = true;
            }
        }

        private void ChkEAX_MouseEnter(object sender, EventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkEAX_MouseLeave(object sender, EventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkEAX_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
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
                LblWarning.Hide();
                FlagBrutalAI = false;
            }
            else
            {
                ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                LblWarning.Show();
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
                    FlagLanguageIndex = 0;
                    break;
                case 1:
                    FlagLanguageIndex = 1;
                    break;
                default:
                    break;
            }
        }
    }
}
