using Helper;
using PatchLauncher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class OptionsForm : Form
    {
        readonly static string GameInstallPath = RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName);
        readonly bool FlagEAXFileExists = File.Exists(GameInstallPath + @"\dsound.dll");

        string FlagAnisotropicTextureFiltering = "yes";
        string FlagTerrainLighting = "yes";
        string Flag3DShadows = "yes";
        string Flag2DShadows = "yes";
        string FlagSmoothWaterBorder = "yes";
        string FlagShowProps = "yes";
        string FlagShowAnimations = "yes";
        string FlagHeatEffects = "yes";
        string FlagDynamicLOD = "yes";
        string FlagResolution = "800 600";
        string FlagUnitDecals = "yes";

        string FlagSelectedIsoCode = "en_us";
        string FlagUseEAX = "yes";

        //Launcher Settings
        bool FlagWindowed = Settings.Default.StartGameWindowed;
        bool FlagBrutalAI = Settings.Default.UseBrutalAI;
        bool FlagUseBetaChannel = Settings.Default.UseBetaChannel;
        string FlagLauncherLanguageIndex = Settings.Default.LauncherLanguage;

        bool FlagIsLanguageChanged = false;
        bool FlagIsBetaChannelChanged = false;

        readonly PatchPacksBeta _patchPacksBeta = JSONDataListHelper._PatchBetaSettings;

        readonly Dictionary<string, string> _selectedLanguageDictionary = JSONDataListHelper._DictionarylanguageSettings.ToDictionary(x => x.Key, x => x.Value.RegistrySelectedLanguage);

        public OptionsForm()
        {
            InitializeComponent();

            KeyPreview = true;

            #region Styles
            //Main Form style behavioral

            PibBorderLauncherOptions.Image = Helper.Properties.Resources.BFME1BorderRectangle;
            PibBorderGameOptions.Image = Helper.Properties.Resources.BFME1BorderRectangleLong;
            PibHeader.Image = Helper.Properties.Resources.BFME1_Header;

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
            LblVideoSettings.Font = FontHelper.GetFont(1, 16);
            LblVideoSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblVideoSettings.BackColor = Color.Transparent;

            LblAudioSettings.Font = FontHelper.GetFont(1, 16);
            LblAudioSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblAudioSettings.BackColor = Color.Transparent;

            LblOptions.Font = FontHelper.GetFont(1, 20);
            LblOptions.ForeColor = Color.FromArgb(192, 145, 69);
            LblOptions.BackColor = Color.Black;

            LblInfoLOD.Font = FontHelper.GetFont(0, 16);
            LblInfoLOD.ForeColor = Color.FromArgb(192, 145, 69);
            LblInfoLOD.BackColor = Color.Transparent;

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

            ///////////////////////////////////////////////////////////////////////////////////////////

            LblAniTextureFiltering.Font = FontHelper.GetFont(0, 16);
            LblAniTextureFiltering.ForeColor = Color.FromArgb(192, 145, 69);
            LblAniTextureFiltering.BackColor = Color.Transparent;

            LblTerrainLighting.Font = FontHelper.GetFont(0, 16);
            LblTerrainLighting.ForeColor = Color.FromArgb(192, 145, 69);
            LblTerrainLighting.BackColor = Color.Transparent;

            Lbl2DShadows.Font = FontHelper.GetFont(0, 16);
            Lbl2DShadows.ForeColor = Color.FromArgb(192, 145, 69);
            Lbl2DShadows.BackColor = Color.Transparent;

            Lbl3DShadows.Font = FontHelper.GetFont(0, 16);
            Lbl3DShadows.ForeColor = Color.FromArgb(192, 145, 69);
            Lbl3DShadows.BackColor = Color.Transparent;

            LblSmoothWaterBorder.Font = FontHelper.GetFont(0, 16);
            LblSmoothWaterBorder.ForeColor = Color.FromArgb(192, 145, 69);
            LblSmoothWaterBorder.BackColor = Color.Transparent;

            LblShowProps.Font = FontHelper.GetFont(0, 16);
            LblShowProps.ForeColor = Color.FromArgb(192, 145, 69);
            LblShowProps.BackColor = Color.Transparent;

            LblShowAnimations.Font = FontHelper.GetFont(0, 16);
            LblShowAnimations.ForeColor = Color.FromArgb(192, 145, 69);
            LblShowAnimations.BackColor = Color.Transparent;

            LblHeatEffects.Font = FontHelper.GetFont(0, 16);
            LblHeatEffects.ForeColor = Color.FromArgb(192, 145, 69);
            LblHeatEffects.BackColor = Color.Transparent;

            LblDynamicLOD.Font = FontHelper.GetFont(0, 16);
            LblDynamicLOD.ForeColor = Color.FromArgb(192, 145, 69);
            LblDynamicLOD.BackColor = Color.Transparent;

            LblResolutionX.Font = FontHelper.GetFont(0, 16);
            LblResolutionX.ForeColor = Color.FromArgb(192, 145, 69);
            LblResolutionX.BackColor = Color.Transparent;

            LblResolution.Font = FontHelper.GetFont(0, 16);
            LblResolution.ForeColor = Color.FromArgb(192, 145, 69);
            LblResolution.BackColor = Color.Transparent;

            LblUnitDecals.Font = FontHelper.GetFont(0, 16);
            LblUnitDecals.ForeColor = Color.FromArgb(192, 145, 69);
            LblUnitDecals.BackColor = Color.Transparent;

            LblGameLanguage.Font = FontHelper.GetFont(0, 16);
            LblGameLanguage.ForeColor = Color.FromArgb(192, 145, 69);
            LblGameLanguage.BackColor = Color.Transparent;

            LblEAX.Font = FontHelper.GetFont(0, 16);
            LblEAX.ForeColor = Color.FromArgb(192, 145, 69);
            LblEAX.BackColor = Color.Transparent;

            ///////////////////////////////////////////////////////////////////////////////////////////

            //Checkbox-Styles
            if (OptionIniParser.ReadKey("StaticGameLOD", AssemblyNameHelper.BFMELauncherGameName) == "UltraHigh")
            {
                FlagAnisotropicTextureFiltering = "yes";
                FlagTerrainLighting = "yes";
                Flag3DShadows = "yes";
                Flag2DShadows = "yes";
                FlagSmoothWaterBorder = "yes";
                FlagShowProps = "yes";
                FlagShowAnimations = "yes";
                FlagHeatEffects = "yes";
                FlagDynamicLOD = "yes";
                FlagResolution = OptionIniParser.ReadKey("Resolution", AssemblyNameHelper.BFMELauncherGameName);
            }
            else
            {
                FlagAnisotropicTextureFiltering = OptionIniParser.ReadKey("AnisotropicTextureFiltering", AssemblyNameHelper.BFMELauncherGameName);
                FlagTerrainLighting = OptionIniParser.ReadKey("TerrainLighting", AssemblyNameHelper.BFMELauncherGameName);
                Flag3DShadows = OptionIniParser.ReadKey("3DShadows", AssemblyNameHelper.BFMELauncherGameName);
                Flag2DShadows = OptionIniParser.ReadKey("2DShadows", AssemblyNameHelper.BFMELauncherGameName);
                FlagSmoothWaterBorder = OptionIniParser.ReadKey("SmoothWaterBorder", AssemblyNameHelper.BFMELauncherGameName);
                FlagShowProps = OptionIniParser.ReadKey("ShowProps", AssemblyNameHelper.BFMELauncherGameName);
                FlagShowAnimations = OptionIniParser.ReadKey("ExtraAnimations", AssemblyNameHelper.BFMELauncherGameName);
                FlagHeatEffects = OptionIniParser.ReadKey("HeatEffects", AssemblyNameHelper.BFMELauncherGameName);
                FlagDynamicLOD = OptionIniParser.ReadKey("DynamicLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagResolution = OptionIniParser.ReadKey("Resolution", AssemblyNameHelper.BFMELauncherGameName);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkAniTextureFiltering.FlatAppearance.BorderSize = 0;
            ChkAniTextureFiltering.FlatStyle = FlatStyle.Flat;
            ChkAniTextureFiltering.BackColor = Color.Transparent;
            ChkAniTextureFiltering.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagAnisotropicTextureFiltering == "no")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }
            else if (FlagAnisotropicTextureFiltering == "yes")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkTerrainLighting.FlatAppearance.BorderSize = 0;
            ChkTerrainLighting.FlatStyle = FlatStyle.Flat;
            ChkTerrainLighting.BackColor = Color.Transparent;
            ChkTerrainLighting.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagTerrainLighting == "no")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }
            else if (FlagTerrainLighting == "yes")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            Chk3DShadows.FlatAppearance.BorderSize = 0;
            Chk3DShadows.FlatStyle = FlatStyle.Flat;
            Chk3DShadows.BackColor = Color.Transparent;
            Chk3DShadows.ForeColor = Color.FromArgb(192, 145, 69);

            if (Flag3DShadows == "no")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }
            else if (Flag3DShadows == "yes")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            Chk2DShadows.FlatAppearance.BorderSize = 0;
            Chk2DShadows.FlatStyle = FlatStyle.Flat;
            Chk2DShadows.BackColor = Color.Transparent;
            Chk2DShadows.ForeColor = Color.FromArgb(192, 145, 69);

            if (Flag2DShadows == "no")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }
            else if (Flag2DShadows == "yes")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkSmoothWaterBorder.FlatAppearance.BorderSize = 0;
            ChkSmoothWaterBorder.FlatStyle = FlatStyle.Flat;
            ChkSmoothWaterBorder.BackColor = Color.Transparent;
            ChkSmoothWaterBorder.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagSmoothWaterBorder == "no")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }
            else if (FlagSmoothWaterBorder == "yes")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkShowProps.FlatAppearance.BorderSize = 0;
            ChkShowProps.FlatStyle = FlatStyle.Flat;
            ChkShowProps.BackColor = Color.Transparent;
            ChkShowProps.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagShowProps == "no")
            {
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }
            else if (FlagShowProps == "yes")
            {
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkShowAnimations.FlatAppearance.BorderSize = 0;
            ChkShowAnimations.FlatStyle = FlatStyle.Flat;
            ChkShowAnimations.BackColor = Color.Transparent;
            ChkShowAnimations.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagShowAnimations == "no")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }
            else if (FlagShowAnimations == "yes")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkHeatEffects.FlatAppearance.BorderSize = 0;
            ChkHeatEffects.FlatStyle = FlatStyle.Flat;
            ChkHeatEffects.BackColor = Color.Transparent;
            ChkHeatEffects.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagHeatEffects == "no")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }
            else if (FlagHeatEffects == "yes")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkDynamicLOD.FlatAppearance.BorderSize = 0;
            ChkDynamicLOD.FlatStyle = FlatStyle.Flat;
            ChkDynamicLOD.BackColor = Color.Transparent;
            ChkDynamicLOD.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagDynamicLOD == "yes")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
                LblInfoLOD.Hide();
            }
            else if (FlagDynamicLOD == "no")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_Selected;
                LblInfoLOD.Show();
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            FlagUnitDecals = OptionIniParser.ReadKey("UnitDecals", AssemblyNameHelper.BFMELauncherGameName);

            ChkUnitDecals.FlatAppearance.BorderSize = 0;
            ChkUnitDecals.FlatStyle = FlatStyle.Flat;
            ChkUnitDecals.BackColor = Color.Transparent;
            ChkUnitDecals.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagUnitDecals == "yes")
            {
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            }
            else if (FlagUnitDecals == "no")
            {
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkEAX.FlatAppearance.BorderSize = 0;
            ChkEAX.FlatStyle = FlatStyle.Flat;
            ChkEAX.BackColor = Color.Transparent;
            ChkEAX.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagEAXFileExists)
                FlagUseEAX = "yes";
            else
                FlagUseEAX = "no";

            if (FlagEAXFileExists && FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Unselected;

            ///////////////////////////////////////////////////////////////////////////////////////////

            ResolutionX.BackColor = Color.Black;
            ResolutionX.Font = FontHelper.GetFont(0, 14);
            ResolutionX.ForeColor = Color.FromArgb(192, 145, 69);

            ResolutionY.BackColor = Color.Black;
            ResolutionY.Font = FontHelper.GetFont(0, 14);
            ResolutionY.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagResolution != null)
            {
                string[] resolutionXY = FlagResolution.Split(" ", StringSplitOptions.TrimEntries);

                string resolutionX = resolutionXY[0];
                string resolutionY = resolutionXY[1];

                ResolutionX.Text = resolutionX;
                ResolutionY.Text = resolutionY;
            }
            else if (FlagResolution == null)
            {
                ResolutionX.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
                ResolutionY.Text = Screen.PrimaryScreen.Bounds.Height.ToString();
            }

            if (Settings.Default.InstalledLanguageISOCode != FlagSelectedIsoCode)
            {
                FlagSelectedIsoCode = Settings.Default.InstalledLanguageISOCode;
            }

            CmbSelectGameLanguage.DisplayMember = "Value";
            CmbSelectGameLanguage.ValueMember = "Key";
            CmbSelectGameLanguage.DataSource = new BindingSource(_selectedLanguageDictionary, null);
            CmbSelectGameLanguage.SelectedValue = Settings.Default.InstalledLanguageISOCode;

            ///////////////////////////////////////////////////////////////////////////////////////////

            if (Settings.Default.PatchVersionInstalled > 106 && !FlagUseBetaChannel)
            {
                if (Settings.Default.PatchVersionInstalled.ToString().EndsWith("0"))
                    LblPatchVersion.Text = string.Concat("2.22 v ", Settings.Default.PatchVersionInstalled.ToString().AsSpan(0, 2));
                else
                    LblPatchVersion.Text = "2.22 v " + Settings.Default.PatchVersionInstalled.ToString()[0..].Insert(2, ".");
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
            FlagAnisotropicTextureFiltering = "yes";
            FlagTerrainLighting = "yes";
            Flag3DShadows = "yes";
            Flag2DShadows = "yes";
            FlagSmoothWaterBorder = "yes";
            FlagShowProps = "yes";
            FlagShowAnimations = "yes";
            FlagHeatEffects = "yes";
            FlagDynamicLOD = "yes";
            FlagResolution = "yes";
            FlagUnitDecals = "yes";
            FlagUseEAX = "yes";

            ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_Unselected;

            ResolutionX.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
            ResolutionY.Text = Screen.PrimaryScreen.Bounds.Height.ToString();

            FlagWindowed = false;
            FlagBrutalAI = false;

            ChkWindowed.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            ChkBrutalAI.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            ChkUseBetaChannel.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
            ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Selected;
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

            if (FlagSelectedIsoCode != Settings.Default.InstalledLanguageISOCode)
            {
                ChangedGameLanguage.UserChangedGameLanguageInSettings = true;
                Settings.Default.InstalledLanguageISOCode = FlagSelectedIsoCode;
                Settings.Default.Save();
            }

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
            if (File.Exists(Path.Combine(RegistryService.GameAppDataFolderPath(AssemblyNameHelper.BFMELauncherGameName), ConstStrings.C_OPTIONSINI_FILENAME)))
            {
                if (FlagAnisotropicTextureFiltering == "yes" && FlagTerrainLighting == "yes" && Flag3DShadows == "yes" && Flag2DShadows == "yes" && FlagSmoothWaterBorder == "yes"
                    && FlagShowProps == "yes" && FlagShowAnimations == "yes" && FlagHeatEffects == "yes" && FlagDynamicLOD == "yes")
                {
                    OptionIniParser.WriteKey("StaticGameLOD", "UltraHigh", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("Resolution", ResolutionX.Text + " " + ResolutionY.Text, AssemblyNameHelper.BFMELauncherGameName);

                    OptionIniParser.DeleteKey("AnisotropicTextureFiltering", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("TerrainLighting", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("3DShadows", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("2DShadows", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("SmoothWaterBorder", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("ShowProps", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("ExtraAnimations", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("HeatEffects", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("DynamicLOD", AssemblyNameHelper.BFMELauncherGameName);
                }
                else
                {
                    OptionIniParser.WriteKey("StaticGameLOD", "Custom", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("AnisotropicTextureFiltering", FlagAnisotropicTextureFiltering, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("TerrainLighting", FlagTerrainLighting, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("3DShadows", Flag3DShadows, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("2DShadows", Flag2DShadows, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("SmoothWaterBorder", FlagSmoothWaterBorder, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("ShowProps", FlagShowProps, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("ExtraAnimations", FlagShowAnimations, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("HeatEffects", FlagHeatEffects, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("DynamicLOD", FlagDynamicLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("Resolution", ResolutionX.Text + " " + ResolutionY.Text, AssemblyNameHelper.BFMELauncherGameName);
                }
            }

            OptionIniParser.WriteKey("FixedStaticGameLOD", "UltraHigh", AssemblyNameHelper.BFMELauncherGameName);
            OptionIniParser.WriteKey("IdealStaticGameLOD", "UltraHigh", AssemblyNameHelper.BFMELauncherGameName);
            OptionIniParser.WriteKey("UnitDecals", FlagUnitDecals, AssemblyNameHelper.BFMELauncherGameName);
            OptionIniParser.WriteKey("UseEAX3", FlagUseEAX, AssemblyNameHelper.BFMELauncherGameName);

            //Settings-Valuations

            if (!FlagEAXFileExists && FlagUseEAX == "yes")
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, file), Path.Combine(GameInstallPath, file), true);
                }

                OptionIniParser.WriteKey("UseEAX3", "yes", AssemblyNameHelper.BFMELauncherGameName);
            }

            if (FlagEAXFileExists && FlagUseEAX == "no")
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Delete(Path.Combine(GameInstallPath, file));
                }

                OptionIniParser.WriteKey("UseEAX3", "no", AssemblyNameHelper.BFMELauncherGameName);
            }

            OptionIniParser.ClearOptionsFile(AssemblyNameHelper.BFMELauncherGameName);

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

            if (FlagBrutalAI && GameInstallPath != null)
                File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, "_patch222LibrariesBrutalAI.big"), Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big"), true);
            else if (GameInstallPath != null && File.Exists(Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big")))
                File.Delete(Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big"));
        }
        #endregion

        #region Checkboxes for game specific settings

        private void ChkAniTextureFiltering_Click(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagAnisotropicTextureFiltering = "no";
            }
            else
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagAnisotropicTextureFiltering = "yes";
            }
        }

        private void ChkAniTextureFiltering_MouseEnter(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkAniTextureFiltering_MouseLeave(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkAniTextureFiltering_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkTerrainLighting_Click(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagTerrainLighting = "no";
            }
            else
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagTerrainLighting = "yes";
            }
        }

        private void ChkTerrainLighting_MouseEnter(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkTerrainLighting_MouseLeave(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkTerrainLighting_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void Chk3DShadows_Click(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                Flag3DShadows = "no";
            }
            else
            {
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                Flag3DShadows = "yes";
            }
        }

        private void Chk3DShadows_MouseEnter(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void Chk3DShadows_MouseLeave(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void Chk3DShadows_MouseDown(object sender, MouseEventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void Chk2DShadows_Click(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                Flag2DShadows = "no";
            }
            else
            {
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                Flag2DShadows = "yes";
            }
        }

        private void Chk2DShadows_MouseEnter(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void Chk2DShadows_MouseLeave(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void Chk2DShadows_MouseDown(object sender, MouseEventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkSmoothWaterBorder_Click(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagSmoothWaterBorder = "no";
            }
            else
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagSmoothWaterBorder = "yes";
            }
        }

        private void ChkSmoothWaterBorder_MouseEnter(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkSmoothWaterBorder_MouseLeave(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkSmoothWaterBorder_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkShowProps_Click(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
            {
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagShowProps = "no";
            }
            else
            {
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagShowProps = "yes";
            }
        }

        private void ChkShowProps_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkShowProps_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkShowProps_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkShowProps.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkShowAnimations_Click(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagShowAnimations = "no";
            }
            else
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagShowAnimations = "yes";
            }
        }

        private void ChkShowAnimations_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkShowAnimations_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkShowAnimations_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkHeatEffects_Click(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagHeatEffects = "no";
            }
            else
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagHeatEffects = "yes";
            }
        }

        private void ChkHeatEffects_MouseEnter(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkHeatEffects_MouseLeave(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkHeatEffects_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkDynamicLOD_Click(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagDynamicLOD = "yes";
                LblInfoLOD.Hide();
            }
            else
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagDynamicLOD = "no";
                LblInfoLOD.Show();
            }
        }

        private void ChkDynamicLOD_MouseEnter(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkDynamicLOD_MouseLeave(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkDynamicLOD_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkUnitDecals_Click(object sender, EventArgs e)
        {
            if (FlagUnitDecals == "yes")
            {
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagUnitDecals = "no";
            }
            else
            {
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagUnitDecals = "yes";
            }
        }

        private void ChkUnitDecals_MouseEnter(object sender, EventArgs e)
        {
            if (FlagUnitDecals == "yes")
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkUnitDecals_MouseLeave(object sender, EventArgs e)
        {
            if (FlagUnitDecals == "yes")
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkUnitDecals_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagUnitDecals == "yes")
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkEAX_Click(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
            {
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
                FlagUseEAX = "no";
            }
            else
            {
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
                FlagUseEAX = "yes";
            }
        }

        private void ChkEAX_MouseEnter(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_SelectedHover;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_UnselectedHover;
        }

        private void ChkEAX_MouseLeave(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Selected;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME1CHK_Unselected;
        }

        private void ChkEAX_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagUseEAX == "yes")
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

        private void CmbSelectGameLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox control = (ComboBox)sender;
            string isoCode = (string)control.SelectedValue;
            LanguagePacks settings = JSONDataListHelper._DictionarylanguageSettings[isoCode];
            FlagSelectedIsoCode = settings.RegistrySelectedLocale;
        }
    }
}