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
    public partial class GameOptionsForm : Form
    {
        readonly static string GameInstallPath = RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName);
        readonly bool FlagEAXFileExists = File.Exists(GameInstallPath + @"\dsound.dll");

        readonly string FlagStaticGameLOD = "UltraHigh";
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

        string FlagSelectedIsoCode = "en_uk";
        string FlagUseEAX = "yes";

        readonly Dictionary<string, string> _selectedLanguageDictionary = JSONDataListHelper._DictionarylanguageSettings.ToDictionary(x => x.Key, x => x.Value.RegistrySelectedLanguage);

        public GameOptionsForm()
        {
            InitializeComponent();

            KeyPreview = true;

            #region Styles
            //Main Form style behaviour

            PibBorderLauncherOptions.Image = Helper.Properties.Resources.BFME2BorderRectangle;
            PibBorderGameOptions.Image = Helper.Properties.Resources.BFME2BorderRectangleLong;
            PibHeader.Image = Helper.Properties.Resources.BFME2_Header;

            BackgroundImage = Helper.Properties.Resources.BGMap;

            // Button-Styles
            BtnApply.FlatAppearance.BorderSize = 0;
            BtnApply.FlatStyle = FlatStyle.Flat;
            BtnApply.BackColor = Color.Transparent;
            BtnApply.Image = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnApply.Font = FontHelper.GetFont(0, 16);
            BtnApply.ForeColor = Color.FromArgb(168, 190, 98);

            BtnCancel.FlatAppearance.BorderSize = 0;
            BtnCancel.FlatStyle = FlatStyle.Flat;
            BtnCancel.BackColor = Color.Transparent;
            BtnCancel.Image = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnCancel.Font = FontHelper.GetFont(0, 16);
            BtnCancel.ForeColor = Color.FromArgb(168, 190, 98);

            BtnDefault.FlatAppearance.BorderSize = 0;
            BtnDefault.FlatStyle = FlatStyle.Flat;
            BtnDefault.BackColor = Color.Transparent;
            BtnDefault.Image = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnDefault.Font = FontHelper.GetFont(0, 16);
            BtnDefault.ForeColor = Color.FromArgb(168, 190, 98);

            //Label-Styles
            LblVideoSettings.Font = FontHelper.GetFont(1, 16);
            LblVideoSettings.ForeColor = Color.FromArgb(168, 190, 98);
            LblVideoSettings.BackColor = Color.Transparent;

            LblAudioSettings.Font = FontHelper.GetFont(1, 16);
            LblAudioSettings.ForeColor = Color.FromArgb(168, 190, 98);
            LblAudioSettings.BackColor = Color.Transparent;

            LblOptions.Font = FontHelper.GetFont(1, 20);
            LblOptions.ForeColor = Color.FromArgb(168, 190, 98);
            LblOptions.BackColor = Color.Black;

            LblInfoLOD.Font = FontHelper.GetFont(0, 16);
            LblInfoLOD.ForeColor = Color.FromArgb(168, 190, 98);
            LblInfoLOD.BackColor = Color.Transparent;

            ///////////////////////////////////////////////////////////////////////////////////////////

            LblAniTextureFiltering.Font = FontHelper.GetFont(0, 16);
            LblAniTextureFiltering.ForeColor = Color.FromArgb(168, 190, 98);
            LblAniTextureFiltering.BackColor = Color.Transparent;

            LblTerrainLighting.Font = FontHelper.GetFont(0, 16);
            LblTerrainLighting.ForeColor = Color.FromArgb(168, 190, 98);
            LblTerrainLighting.BackColor = Color.Transparent;

            Lbl2DShadows.Font = FontHelper.GetFont(0, 16);
            Lbl2DShadows.ForeColor = Color.FromArgb(168, 190, 98);
            Lbl2DShadows.BackColor = Color.Transparent;

            Lbl3DShadows.Font = FontHelper.GetFont(0, 16);
            Lbl3DShadows.ForeColor = Color.FromArgb(168, 190, 98);
            Lbl3DShadows.BackColor = Color.Transparent;

            LblSmoothWaterBorder.Font = FontHelper.GetFont(0, 16);
            LblSmoothWaterBorder.ForeColor = Color.FromArgb(168, 190, 98);
            LblSmoothWaterBorder.BackColor = Color.Transparent;

            LblShowProps.Font = FontHelper.GetFont(0, 16);
            LblShowProps.ForeColor = Color.FromArgb(168, 190, 98);
            LblShowProps.BackColor = Color.Transparent;

            LblShowAnimations.Font = FontHelper.GetFont(0, 16);
            LblShowAnimations.ForeColor = Color.FromArgb(168, 190, 98);
            LblShowAnimations.BackColor = Color.Transparent;

            LblHeatEffects.Font = FontHelper.GetFont(0, 16);
            LblHeatEffects.ForeColor = Color.FromArgb(168, 190, 98);
            LblHeatEffects.BackColor = Color.Transparent;

            LblDynamicLOD.Font = FontHelper.GetFont(0, 16);
            LblDynamicLOD.ForeColor = Color.FromArgb(168, 190, 98);
            LblDynamicLOD.BackColor = Color.Transparent;

            LblResolutionX.Font = FontHelper.GetFont(0, 16);
            LblResolutionX.ForeColor = Color.FromArgb(168, 190, 98);
            LblResolutionX.BackColor = Color.Transparent;

            LblResolution.Font = FontHelper.GetFont(0, 16);
            LblResolution.ForeColor = Color.FromArgb(168, 190, 98);
            LblResolution.BackColor = Color.Transparent;

            LblUnitDecals.Font = FontHelper.GetFont(0, 16);
            LblUnitDecals.ForeColor = Color.FromArgb(168, 190, 98);
            LblUnitDecals.BackColor = Color.Transparent;

            LblGameLanguage.Font = FontHelper.GetFont(0, 16);
            LblGameLanguage.ForeColor = Color.FromArgb(168, 190, 98);
            LblGameLanguage.BackColor = Color.Transparent;

            LblEAX.Font = FontHelper.GetFont(0, 16);
            LblEAX.ForeColor = Color.FromArgb(168, 190, 98);
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
                FlagStaticGameLOD = OptionIniParser.ReadKey("StaticGameLOD", AssemblyNameHelper.BFMELauncherGameName);
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
            ChkAniTextureFiltering.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagAnisotropicTextureFiltering == "no")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }
            else if (FlagAnisotropicTextureFiltering == "yes")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkTerrainLighting.FlatAppearance.BorderSize = 0;
            ChkTerrainLighting.FlatStyle = FlatStyle.Flat;
            ChkTerrainLighting.BackColor = Color.Transparent;
            ChkTerrainLighting.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagTerrainLighting == "no")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }
            else if (FlagTerrainLighting == "yes")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            Chk3DShadows.FlatAppearance.BorderSize = 0;
            Chk3DShadows.FlatStyle = FlatStyle.Flat;
            Chk3DShadows.BackColor = Color.Transparent;
            Chk3DShadows.ForeColor = Color.FromArgb(168, 190, 98);

            if (Flag3DShadows == "no")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }
            else if (Flag3DShadows == "yes")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            Chk2DShadows.FlatAppearance.BorderSize = 0;
            Chk2DShadows.FlatStyle = FlatStyle.Flat;
            Chk2DShadows.BackColor = Color.Transparent;
            Chk2DShadows.ForeColor = Color.FromArgb(168, 190, 98);

            if (Flag2DShadows == "no")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }
            else if (Flag2DShadows == "yes")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkSmoothWaterBorder.FlatAppearance.BorderSize = 0;
            ChkSmoothWaterBorder.FlatStyle = FlatStyle.Flat;
            ChkSmoothWaterBorder.BackColor = Color.Transparent;
            ChkSmoothWaterBorder.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagSmoothWaterBorder == "no")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }
            else if (FlagSmoothWaterBorder == "yes")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkShowProps.FlatAppearance.BorderSize = 0;
            ChkShowProps.FlatStyle = FlatStyle.Flat;
            ChkShowProps.BackColor = Color.Transparent;
            ChkShowProps.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagShowProps == "no")
            {
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }
            else if (FlagShowProps == "yes")
            {
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkShowAnimations.FlatAppearance.BorderSize = 0;
            ChkShowAnimations.FlatStyle = FlatStyle.Flat;
            ChkShowAnimations.BackColor = Color.Transparent;
            ChkShowAnimations.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagShowAnimations == "no")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }
            else if (FlagShowAnimations == "yes")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkHeatEffects.FlatAppearance.BorderSize = 0;
            ChkHeatEffects.FlatStyle = FlatStyle.Flat;
            ChkHeatEffects.BackColor = Color.Transparent;
            ChkHeatEffects.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagHeatEffects == "no")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }
            else if (FlagHeatEffects == "yes")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkDynamicLOD.FlatAppearance.BorderSize = 0;
            ChkDynamicLOD.FlatStyle = FlatStyle.Flat;
            ChkDynamicLOD.BackColor = Color.Transparent;
            ChkDynamicLOD.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagDynamicLOD == "yes")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
                LblInfoLOD.Hide();
            }
            else if (FlagDynamicLOD == "no")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_Selected;
                LblInfoLOD.Show();
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            FlagUnitDecals = OptionIniParser.ReadKey("UnitDecals", AssemblyNameHelper.BFMELauncherGameName);

            ChkUnitDecals.FlatAppearance.BorderSize = 0;
            ChkUnitDecals.FlatStyle = FlatStyle.Flat;
            ChkUnitDecals.BackColor = Color.Transparent;
            ChkUnitDecals.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagUnitDecals == "yes")
            {
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            }
            else if (FlagUnitDecals == "no")
            {
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkEAX.FlatAppearance.BorderSize = 0;
            ChkEAX.FlatStyle = FlatStyle.Flat;
            ChkEAX.BackColor = Color.Transparent;
            ChkEAX.ForeColor = Color.FromArgb(168, 190, 98);

            if (FlagEAXFileExists)
                FlagUseEAX = "yes";
            else
                FlagUseEAX = "no";

            if (FlagEAXFileExists && FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_Unselected;

            ///////////////////////////////////////////////////////////////////////////////////////////

            ResolutionX.BackColor = Color.Black;
            ResolutionX.Font = FontHelper.GetFont(0, 14);
            ResolutionX.ForeColor = Color.FromArgb(168, 190, 98);

            ResolutionY.BackColor = Color.Black;
            ResolutionY.Font = FontHelper.GetFont(0, 14);
            ResolutionY.ForeColor = Color.FromArgb(168, 190, 98);

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

            ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_Unselected;

            ResolutionX.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
            ResolutionY.Text = Screen.PrimaryScreen.Bounds.Height.ToString();

            ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_Selected;

            ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_Selected;
        }

        private void BtnDefault_MouseLeave(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnDefault.ForeColor = Color.FromArgb(168, 190, 98);
        }

        private void BtnDefault_MouseEnter(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME2_BUTTONIMAGE_HOVER;
            BtnDefault.ForeColor = Color.FromArgb(24, 63, 20);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnDefault_MouseDown(object sender, MouseEventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME2_BUTTONIMAGE_CLICK;
            BtnDefault.ForeColor = Color.FromArgb(168, 190, 98);
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

            Close();
        }

        private void BtnApply_MouseLeave(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnApply.ForeColor = Color.FromArgb(168, 190, 98);
        }

        private void BtnApply_MouseEnter(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BFME2_BUTTONIMAGE_HOVER;
            BtnApply.ForeColor = Color.FromArgb(24, 63, 20);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnApply_MouseDown(object sender, MouseEventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BFME2_BUTTONIMAGE_CLICK;
            BtnApply.ForeColor = Color.FromArgb(168, 190, 98);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnCancel_MouseLeave(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnCancel.ForeColor = Color.FromArgb(168, 190, 98);
        }

        private void BtnCancel_MouseEnter(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME2_BUTTONIMAGE_HOVER;
            BtnCancel.ForeColor = Color.FromArgb(24, 63, 20);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME2_BUTTONIMAGE_CLICK;
            BtnCancel.ForeColor = Color.FromArgb(168, 190, 98);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void SaveSettings()
        {
            if (File.Exists(Path.Combine(RegistryService.GameAppdataFolderPath(AssemblyNameHelper.BFMELauncherGameName), ConstStrings.C_OPTIONSINI_FILENAME)))
            {
                if (FlagAnisotropicTextureFiltering == "yes" && FlagTerrainLighting == "yes" && Flag3DShadows == "yes" && Flag2DShadows == "yes" && FlagSmoothWaterBorder == "yes"
                    && FlagShowProps == "yes" && FlagShowAnimations == "yes" && FlagHeatEffects == "yes" && FlagDynamicLOD == "yes")
                {
                    OptionIniParser.WriteKey("StaticGameLOD", FlagStaticGameLOD, AssemblyNameHelper.BFMELauncherGameName);
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
                    File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, file), Path.Combine(GameInstallPath, file), true);
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
        }
        #endregion

        #region Checkboxes for game specific settings

        private void ChkAniTextureFiltering_Click(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagAnisotropicTextureFiltering = "no";
            }
            else
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagAnisotropicTextureFiltering = "yes";
            }
        }

        private void ChkAniTextureFiltering_MouseEnter(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkAniTextureFiltering_MouseLeave(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkAniTextureFiltering_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkTerrainLighting_Click(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagTerrainLighting = "no";
            }
            else
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagTerrainLighting = "yes";
            }
        }

        private void ChkTerrainLighting_MouseEnter(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkTerrainLighting_MouseLeave(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkTerrainLighting_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void Chk3DShadows_Click(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                Flag3DShadows = "no";
            }
            else
            {
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                Flag3DShadows = "yes";
            }
        }

        private void Chk3DShadows_MouseEnter(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void Chk3DShadows_MouseLeave(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void Chk3DShadows_MouseDown(object sender, MouseEventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void Chk2DShadows_Click(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                Flag2DShadows = "no";
            }
            else
            {
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                Flag2DShadows = "yes";
            }
        }

        private void Chk2DShadows_MouseEnter(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void Chk2DShadows_MouseLeave(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void Chk2DShadows_MouseDown(object sender, MouseEventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkSmoothWaterBorder_Click(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagSmoothWaterBorder = "no";
            }
            else
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagSmoothWaterBorder = "yes";
            }
        }

        private void ChkSmoothWaterBorder_MouseEnter(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkSmoothWaterBorder_MouseLeave(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkSmoothWaterBorder_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkShowProps_Click(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
            {
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagShowProps = "no";
            }
            else
            {
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagShowProps = "yes";
            }
        }

        private void ChkShowProps_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkShowProps_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkShowProps_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkShowProps.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkShowAnimations_Click(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagShowAnimations = "no";
            }
            else
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagShowAnimations = "yes";
            }
        }

        private void ChkShowAnimations_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkShowAnimations_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkShowAnimations_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkHeatEffects_Click(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagHeatEffects = "no";
            }
            else
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagHeatEffects = "yes";
            }
        }

        private void ChkHeatEffects_MouseEnter(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkHeatEffects_MouseLeave(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkHeatEffects_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkDynamicLOD_Click(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagDynamicLOD = "yes";
                LblInfoLOD.Hide();
            }
            else
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagDynamicLOD = "no";
                LblInfoLOD.Show();
            }
        }

        private void ChkDynamicLOD_MouseEnter(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkDynamicLOD_MouseLeave(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkDynamicLOD_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkUnitDecals_Click(object sender, EventArgs e)
        {
            if (FlagUnitDecals == "yes")
            {
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagUnitDecals = "no";
            }
            else
            {
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagUnitDecals = "yes";
            }
        }

        private void ChkUnitDecals_MouseEnter(object sender, EventArgs e)
        {
            if (FlagUnitDecals == "yes")
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkUnitDecals_MouseLeave(object sender, EventArgs e)
        {
            if (FlagUnitDecals == "yes")
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkUnitDecals_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagUnitDecals == "yes")
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkUnitDecals.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkEAX_Click(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
            {
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
                FlagUseEAX = "no";
            }
            else
            {
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
                FlagUseEAX = "yes";
            }
        }

        private void ChkEAX_MouseEnter(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        private void ChkEAX_MouseLeave(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_Selected;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_Unselected;
        }

        private void ChkEAX_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_SelectedHover;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME2CHK_UnselectedHover;
        }

        #endregion

        private void OptionsBFME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
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