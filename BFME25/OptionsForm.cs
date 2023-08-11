using Helper;
using PatchLauncher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class OptionsForm : Form
    {
        readonly static string GameInstallPath = RegistryService.GameInstallPath(AssemblyNameHelper.BFMELauncherGameName);
        readonly bool FlagEAXFileExists = File.Exists(GameInstallPath + @"\dsound.dll");
        readonly Regex regex = new(@"(?!^)(?=[A-Z])");

        string FlagStaticGameLOD = "UltraHigh";

        string FlagModelLOD = "UltraHigh";
        string FlagAnimationLOD = "UltraHigh";
        string FlagEffectsLOD = "UltraHigh";
        string FlagShadowLOD = "UltraHigh";
        string FlagShaderLOD = "UltraHigh";
        string FlagWaterLOD = "UltraHigh";
        string FlagTextureQualityLOD = "High";
        string FlagTerrainLOD = "High";
        string FlagDecalLOD = "High";

        string FlagResolution = "1024 768";

        string FlagSelectedIsoCode = "en_uk";
        string FlagHighAudio = "High";
        string FlagUseEAX = "yes";

        bool FlagWindowed = Settings.Default.StartGameWindowed;
        bool FlagBrutalAI = Settings.Default.UseBrutalAI;
        string FlagLauncherLanguageIndex = Settings.Default.LauncherLanguage;

        bool FlagIsLanguageChanged = false;

        enum GraphicSettingsFlagModelWaterAndShaderLOD { Low, Medium, High, UltraHigh }

        enum GraphicSettingsFlagAnimationAndVFXLOD { VeryLow, Low, Medium, High, UltraHigh }

        enum GraphicSettingsFlagShadowsLOD { Off, Low, Medium, High, UltraHigh }

        enum GraphicSettingsTextureAndTerrainLOD { Low, Medium, High }

        enum GraphicSettingsDecalsLOD { Off, Medium, High }

        readonly Dictionary<string, string> _selectedLanguageDictionary = JSONDataListHelper._DictionarylanguageSettings.ToDictionary(x => x.Key, x => x.Value.RegistrySelectedLanguage);

        public OptionsForm()
        {
            InitializeComponent();

            KeyPreview = true;

            #region Styles
            //Main Form style behaviour

            PibBorderLanguageOptions.Image = Helper.Properties.Resources.BFME25BorderRectangle;
            PibBorderGameOptions.Image = Helper.Properties.Resources.BFME25BorderRectangleLong;
            PibBorderLauncherSettings.Image = Helper.Properties.Resources.BFME25BorderRectangleSmallest;
            PibHeader.Image = Helper.Properties.Resources.BFME25_Header;
            BackgroundImage = Helper.Properties.Resources.BGMap;

            // Button-Styles
            BtnApply.FlatAppearance.BorderSize = 0;
            BtnApply.FlatStyle = FlatStyle.Flat;
            BtnApply.BackColor = Color.Transparent;
            BtnApply.Image = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnApply.Font = FontHelper.GetFont(0, 16);
            BtnApply.ForeColor = Color.FromArgb(114, 153, 169);

            BtnCancel.FlatAppearance.BorderSize = 0;
            BtnCancel.FlatStyle = FlatStyle.Flat;
            BtnCancel.BackColor = Color.Transparent;
            BtnCancel.Image = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnCancel.Font = FontHelper.GetFont(0, 16);
            BtnCancel.ForeColor = Color.FromArgb(114, 153, 169);

            BtnDefault.FlatAppearance.BorderSize = 0;
            BtnDefault.FlatStyle = FlatStyle.Flat;
            BtnDefault.BackColor = Color.Transparent;
            BtnDefault.Image = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnDefault.Font = FontHelper.GetFont(0, 16);
            BtnDefault.ForeColor = Color.FromArgb(114, 153, 169);

            //Label-Styles

            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "de")
                LblSettingsTitle.Font = FontHelper.GetFont(1, 15);
            else
                LblSettingsTitle.Font = FontHelper.GetFont(1, 20);

            LblSettingsTitle.ForeColor = Color.FromArgb(114, 153, 169);
            LblSettingsTitle.BackColor = Color.Black;

            LblVideoSettingsTitle.Font = FontHelper.GetFont(1, 16);
            LblVideoSettingsTitle.ForeColor = Color.FromArgb(114, 153, 169);
            LblVideoSettingsTitle.BackColor = Color.Transparent;

            LblAudioSettingsTitle.Font = FontHelper.GetFont(1, 16);
            LblAudioSettingsTitle.ForeColor = Color.FromArgb(114, 153, 169);
            LblAudioSettingsTitle.BackColor = Color.Transparent;

            LblLauncherLanguage.Font = FontHelper.GetFont(0, 16);
            LblLauncherLanguage.ForeColor = Color.FromArgb(114, 153, 169);
            LblLauncherLanguage.BackColor = Color.Transparent;

            LblLauncherVersionTitle.Font = FontHelper.GetFont(0, 16);
            LblLauncherVersionTitle.ForeColor = Color.FromArgb(114, 153, 169);
            LblLauncherVersionTitle.BackColor = Color.Transparent;

            LblPatchVersionTitle.Font = FontHelper.GetFont(0, 16);
            LblPatchVersionTitle.ForeColor = Color.FromArgb(114, 153, 169);
            LblPatchVersionTitle.BackColor = Color.Transparent;

            LblLauncherVersion.Text = Assembly.GetEntryAssembly()!.GetName().Version!.ToString();
            LblLauncherVersion.Font = FontHelper.GetFont(0, 14);
            LblLauncherVersion.ForeColor = Color.FromArgb(114, 153, 169);
            LblLauncherVersion.BackColor = Color.Transparent;

            LblPatchVersion.Font = FontHelper.GetFont(0, 14);
            LblPatchVersion.ForeColor = Color.FromArgb(114, 153, 169);
            LblPatchVersion.BackColor = Color.Transparent;

            LblWindowed.Font = FontHelper.GetFont(0, 16);
            LblWindowed.ForeColor = Color.FromArgb(114, 153, 169);
            LblWindowed.BackColor = Color.Transparent;

            LblPatchVersion.Text = Settings.Default.PatchVersionInstalled.ToString();

            CmbSelectLauncherLanguage.SelectedIndex = Settings.Default.LauncherLanguage switch
            {
                "de" => 1,
                _ => 0,
            };

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkWindowed.FlatAppearance.BorderSize = 0;
            ChkWindowed.FlatStyle = FlatStyle.Flat;
            ChkWindowed.BackColor = Color.Transparent;
            ChkWindowed.ForeColor = Color.FromArgb(114, 153, 169);

            if (FlagWindowed)
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            else
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_Unselected;

            ///////////////////////////////////////////////////////////////////////////////////////////

            LblTrackBarModelLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarModelLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarModelLOD.BackColor = Color.Transparent;

            LblTrackBarAnimationLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarAnimationLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarAnimationLOD.BackColor = Color.Transparent;

            LblTrackBarVFXLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarVFXLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarVFXLOD.BackColor = Color.Transparent;

            LblTrackBarShadowsLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarShadowsLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarShadowsLOD.BackColor = Color.Transparent;

            LblTrackBarShaderLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarShaderLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarShaderLOD.BackColor = Color.Transparent;

            LblTrackBarWaterLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarWaterLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarWaterLOD.BackColor = Color.Transparent;

            LblTrackBarTextureLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarTextureLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarTextureLOD.BackColor = Color.Transparent;

            LblTrackBarTerrainLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarTerrainLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarTerrainLOD.BackColor = Color.Transparent;

            LblTrackBarDecalLOD.Font = FontHelper.GetFont(0, 16);
            LblTrackBarDecalLOD.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarDecalLOD.BackColor = Color.Transparent;

            ///////////////////////////////////////////////////////////////////////////////////////////

            LblTrackBarModelValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarModelValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarModelValue.BackColor = Color.Transparent;

            LblTrackBarAnimationValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarAnimationValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarAnimationValue.BackColor = Color.Transparent;

            LblTrackBarVFXValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarVFXValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarVFXValue.BackColor = Color.Transparent;

            LblTrackBarShadowsValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarShadowsValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarShadowsValue.BackColor = Color.Transparent;

            LblTrackBarShaderValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarShaderValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarShaderValue.BackColor = Color.Transparent;

            LblTrackBarWaterValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarWaterValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarWaterValue.BackColor = Color.Transparent;

            LblTrackBarTextureValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarTextureValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarTextureValue.BackColor = Color.Transparent;

            LblTrackBarTerrainValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarTerrainValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarTerrainValue.BackColor = Color.Transparent;

            LblTrackBarDecalValue.Font = FontHelper.GetFont(0, 16);
            LblTrackBarDecalValue.ForeColor = Color.FromArgb(114, 153, 169);
            LblTrackBarDecalValue.BackColor = Color.Transparent;

            ///////////////////////////////////////////////////////////////////////////////////////////

            LblResolutionX.Font = FontHelper.GetFont(0, 16);
            LblResolutionX.ForeColor = Color.FromArgb(114, 153, 169);
            LblResolutionX.BackColor = Color.Transparent;

            LblResolution.Font = FontHelper.GetFont(0, 16);
            LblResolution.ForeColor = Color.FromArgb(114, 153, 169);
            LblResolution.BackColor = Color.Transparent;

            LblGameLanguage.Font = FontHelper.GetFont(0, 16);
            LblGameLanguage.ForeColor = Color.FromArgb(114, 153, 169);
            LblGameLanguage.BackColor = Color.Transparent;

            LblEAX.Font = FontHelper.GetFont(0, 16);
            LblEAX.ForeColor = Color.FromArgb(114, 153, 169);
            LblEAX.BackColor = Color.Transparent;

            LblHighAudio.Font = FontHelper.GetFont(0, 16);
            LblHighAudio.ForeColor = Color.FromArgb(114, 153, 169);
            LblHighAudio.BackColor = Color.Transparent;

            ///////////////////////////////////////////////////////////////////////////////////////////

            //Checkbox-Styles
            if (OptionIniParser.ReadKey("StaticGameLOD", AssemblyNameHelper.BFMELauncherGameName) == "Custom")
            {
                FlagModelLOD = OptionIniParser.ReadKey("ModelLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagAnimationLOD = OptionIniParser.ReadKey("AnimationLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagEffectsLOD = OptionIniParser.ReadKey("EffectsLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagShadowLOD = OptionIniParser.ReadKey("ShadowLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagShaderLOD = OptionIniParser.ReadKey("ShaderLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagWaterLOD = OptionIniParser.ReadKey("WaterLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagTextureQualityLOD = OptionIniParser.ReadKey("TextureQualityLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagTerrainLOD = OptionIniParser.ReadKey("TerrainLOD", AssemblyNameHelper.BFMELauncherGameName);
                FlagDecalLOD = OptionIniParser.ReadKey("DecalLOD", AssemblyNameHelper.BFMELauncherGameName);
            }
            else
            {
                FlagStaticGameLOD = OptionIniParser.ReadKey("StaticGameLOD", AssemblyNameHelper.BFMELauncherGameName);
            }

            FlagResolution = OptionIniParser.ReadKey("Resolution", AssemblyNameHelper.BFMELauncherGameName);
            FlagUseEAX = OptionIniParser.ReadKey("UseEAX3", AssemblyNameHelper.BFMELauncherGameName);
            FlagHighAudio = OptionIniParser.ReadKey("AudioLOD", AssemblyNameHelper.BFMELauncherGameName);

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkHighAudio.FlatAppearance.BorderSize = 0;
            ChkHighAudio.FlatStyle = FlatStyle.Flat;
            ChkHighAudio.BackColor = Color.Transparent;
            ChkHighAudio.ForeColor = Color.FromArgb(114, 153, 169);

            if (FlagHighAudio == "High")
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            else
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_Unselected;

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkEAX.FlatAppearance.BorderSize = 0;
            ChkEAX.FlatStyle = FlatStyle.Flat;
            ChkEAX.BackColor = Color.Transparent;
            ChkEAX.ForeColor = Color.FromArgb(114, 153, 169);

            if (FlagEAXFileExists && FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_Unselected;

            ///////////////////////////////////////////////////////////////////////////////////////////

            ResolutionX.BackColor = Color.Black;
            ResolutionX.Font = FontHelper.GetFont(0, 14);
            ResolutionX.ForeColor = Color.FromArgb(114, 153, 169);

            ResolutionY.BackColor = Color.Black;
            ResolutionY.Font = FontHelper.GetFont(0, 14);
            ResolutionY.ForeColor = Color.FromArgb(114, 153, 169);

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

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            FlagStaticGameLOD = OptionIniParser.ReadKey("StaticGameLOD", AssemblyNameHelper.BFMELauncherGameName);

            var dictionaryModelWaterAndShaderLOD = new Dictionary<int, string> {
                { 0, "Low" },
                { 1, "Medium" },
                { 2, "High" },
                { 3, "UltraHigh"}
            };

            var dictionaryAnimationAndVFXLOD = new Dictionary<int, string> {
                { 0, "VeryLow" },
                { 1, "Low" },
                { 2, "Medium" },
                { 3, "High" },
                { 4, "UltraHigh"}
            };

            var dictionaryShadowsLOD = new Dictionary<int, string> {
                { 0, "Off" },
                { 1, "Low" },
                { 2, "Medium" },
                { 3, "High" },
                { 4, "UltraHigh"}
            };

            var dictionaryTextureAndTerrainLOD = new Dictionary<int, string> {
                { 0, "Low" },
                { 1, "Medium" },
                { 2, "High" },
            };

            var dictionaryDecalsLOD = new Dictionary<int, string> {
                { 0, "Low" },
                { 1, "Medium" },
                { 2, "High" },
            };

            switch (FlagStaticGameLOD)
            {
                case "Ultralow":
                    TrackBarOptionsModelLOD.Value = 0;
                    TrackBarOptionsAnimationLOD.Value = 0;
                    TrackBarOptionsVFXLOD.Value = 0;
                    TrackBarOptionsShadowsLOD.Value = 0;
                    TrackBarOptionsShaderLOD.Value = 0;
                    TrackBarOptionsWaterLOD.Value = 0;
                    TrackBarOptionsTextureLOD.Value = 0;
                    TrackBarOptionsTerrainLOD.Value = 0;
                    TrackBarOptionsDecalLOD.Value = 0;
                    break;

                case "Low":
                    TrackBarOptionsModelLOD.Value = 0;
                    TrackBarOptionsAnimationLOD.Value = 2;
                    TrackBarOptionsVFXLOD.Value = 1;
                    TrackBarOptionsShadowsLOD.Value = 1;
                    TrackBarOptionsShaderLOD.Value = 0;
                    TrackBarOptionsWaterLOD.Value = 0;
                    TrackBarOptionsTextureLOD.Value = 1;
                    TrackBarOptionsTerrainLOD.Value = 0;
                    TrackBarOptionsDecalLOD.Value = 1;
                    break;

                case "Medium":
                    TrackBarOptionsModelLOD.Value = 1;
                    TrackBarOptionsAnimationLOD.Value = 3;
                    TrackBarOptionsVFXLOD.Value = 2;
                    TrackBarOptionsShadowsLOD.Value = 2;
                    TrackBarOptionsShaderLOD.Value = 1;
                    TrackBarOptionsWaterLOD.Value = 1;
                    TrackBarOptionsTextureLOD.Value = 2;
                    TrackBarOptionsTerrainLOD.Value = 1;
                    TrackBarOptionsDecalLOD.Value = 2;
                    break;

                case "High":
                    TrackBarOptionsModelLOD.Value = 2;
                    TrackBarOptionsAnimationLOD.Value = 3;
                    TrackBarOptionsVFXLOD.Value = 3;
                    TrackBarOptionsShadowsLOD.Value = 3;
                    TrackBarOptionsShaderLOD.Value = 2;
                    TrackBarOptionsWaterLOD.Value = 2;
                    TrackBarOptionsTextureLOD.Value = 2;
                    TrackBarOptionsTerrainLOD.Value = 2;
                    TrackBarOptionsDecalLOD.Value = 2;
                    break;

                case "UltraHigh":
                    TrackBarOptionsModelLOD.Value = 3;
                    TrackBarOptionsAnimationLOD.Value = 4;
                    TrackBarOptionsVFXLOD.Value = 4;
                    TrackBarOptionsShadowsLOD.Value = 4;
                    TrackBarOptionsShaderLOD.Value = 3;
                    TrackBarOptionsWaterLOD.Value = 3;
                    TrackBarOptionsTextureLOD.Value = 2;
                    TrackBarOptionsTerrainLOD.Value = 2;
                    TrackBarOptionsDecalLOD.Value = 2;
                    break;

                default:
                    TrackBarOptionsModelLOD.Value = dictionaryModelWaterAndShaderLOD.FirstOrDefault(x => x.Value == FlagModelLOD).Key;
                    TrackBarOptionsAnimationLOD.Value = dictionaryAnimationAndVFXLOD.FirstOrDefault(x => x.Value == FlagAnimationLOD).Key;
                    TrackBarOptionsVFXLOD.Value = dictionaryAnimationAndVFXLOD.FirstOrDefault(x => x.Value == FlagEffectsLOD).Key;
                    TrackBarOptionsShadowsLOD.Value = dictionaryShadowsLOD.FirstOrDefault(x => x.Value == FlagShadowLOD).Key;
                    TrackBarOptionsShaderLOD.Value = dictionaryModelWaterAndShaderLOD.FirstOrDefault(x => x.Value == FlagShaderLOD).Key;
                    TrackBarOptionsWaterLOD.Value = dictionaryModelWaterAndShaderLOD.FirstOrDefault(x => x.Value == FlagWaterLOD).Key;
                    TrackBarOptionsTextureLOD.Value = dictionaryTextureAndTerrainLOD.FirstOrDefault(x => x.Value == FlagTextureQualityLOD).Key;
                    TrackBarOptionsTerrainLOD.Value = dictionaryTextureAndTerrainLOD.FirstOrDefault(x => x.Value == FlagTerrainLOD).Key;
                    TrackBarOptionsDecalLOD.Value = dictionaryDecalsLOD.FirstOrDefault(x => x.Value == FlagDecalLOD).Key;
                    break;
            }

            LblTrackBarModelValue.Text = regex.Replace(dictionaryModelWaterAndShaderLOD[TrackBarOptionsModelLOD.Value].ToString(), " ");
            LblTrackBarAnimationValue.Text = regex.Replace(dictionaryAnimationAndVFXLOD[TrackBarOptionsAnimationLOD.Value].ToString(), " ");
            LblTrackBarVFXValue.Text = regex.Replace(dictionaryAnimationAndVFXLOD[TrackBarOptionsVFXLOD.Value].ToString(), " ");
            LblTrackBarShadowsValue.Text = regex.Replace(dictionaryShadowsLOD[TrackBarOptionsShadowsLOD.Value].ToString(), " ");
            LblTrackBarShaderValue.Text = regex.Replace(dictionaryModelWaterAndShaderLOD[TrackBarOptionsShaderLOD.Value].ToString(), " ");
            LblTrackBarWaterValue.Text = regex.Replace(dictionaryModelWaterAndShaderLOD[TrackBarOptionsWaterLOD.Value].ToString(), " ");
            LblTrackBarTextureValue.Text = regex.Replace(dictionaryTextureAndTerrainLOD[TrackBarOptionsTextureLOD.Value].ToString(), " ");
            LblTrackBarTerrainValue.Text = regex.Replace(dictionaryTextureAndTerrainLOD[TrackBarOptionsTerrainLOD.Value].ToString(), " ");
            LblTrackBarDecalValue.Text = regex.Replace(dictionaryDecalsLOD[TrackBarOptionsDecalLOD.Value].ToString(), " ");
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            FlagResolution = "yes";
            FlagUseEAX = "yes";
            FlagHighAudio = "High";

            ResolutionX.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
            ResolutionY.Text = Screen.PrimaryScreen.Bounds.Height.ToString();

            TrackBarOptionsModelLOD.Value = 3;
            TrackBarOptionsAnimationLOD.Value = 4;
            TrackBarOptionsVFXLOD.Value = 4;
            TrackBarOptionsShadowsLOD.Value = 4;
            TrackBarOptionsShaderLOD.Value = 3;
            TrackBarOptionsWaterLOD.Value = 3;
            TrackBarOptionsTextureLOD.Value = 2;
            TrackBarOptionsTerrainLOD.Value = 2;
            TrackBarOptionsDecalLOD.Value = 2;

            FlagModelLOD = "UltraHigh";
            FlagAnimationLOD = "UltraHigh";
            FlagEffectsLOD = "UltraHigh";
            FlagShadowLOD = "UltraHigh";
            FlagShaderLOD = "UltraHigh";
            FlagWaterLOD = "UltraHigh";
            FlagTextureQualityLOD = "High";
            FlagTerrainLOD = "High";
            FlagDecalLOD = "High";

            LblTrackBarModelValue.Text = "Ultra High";
            LblTrackBarAnimationValue.Text = "Ultra High";
            LblTrackBarVFXValue.Text = "Ultra High";
            LblTrackBarShadowsValue.Text = "Ultra High";
            LblTrackBarShaderValue.Text = "Ultra High";
            LblTrackBarWaterValue.Text = "Ultra High";
            LblTrackBarTextureValue.Text = "High";
            LblTrackBarTerrainValue.Text = "High";
            LblTrackBarDecalValue.Text = "High";

            Update();

            FlagWindowed = false;
            FlagBrutalAI = false;

            ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_Unselected;
            ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_Selected;
        }

        private void BtnDefault_MouseLeave(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnDefault.ForeColor = Color.FromArgb(114, 153, 169);
        }

        private void BtnDefault_MouseEnter(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME25_BUTTONIMAGE_HOVER;
            BtnDefault.ForeColor = Color.FromArgb(173, 215, 232);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnDefault_MouseDown(object sender, MouseEventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BFME25_BUTTONIMAGE_CLICK;
            BtnDefault.ForeColor = Color.FromArgb(114, 153, 169);
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

            if (FlagIsLanguageChanged)
            {
                DialogResult _dialogResult = MessageBox.Show(Strings.Msg_Restart_Text, Strings.Msg_Restart_Title, MessageBoxButtons.YesNo);
                if (_dialogResult == DialogResult.Yes)
                {
                    Process _restarterProcess = new();
                    _restarterProcess.StartInfo.FileName = ConstStrings.C_RESTARTEREXE_FILENAME;
                    _restarterProcess.StartInfo.Arguments = "--restart --BFME25Launcher";
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
            BtnApply.Image = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnApply.ForeColor = Color.FromArgb(114, 153, 169);
        }

        private void BtnApply_MouseEnter(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BFME25_BUTTONIMAGE_HOVER;
            BtnApply.ForeColor = Color.FromArgb(173, 215, 232);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnApply_MouseDown(object sender, MouseEventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BFME25_BUTTONIMAGE_CLICK;
            BtnApply.ForeColor = Color.FromArgb(114, 153, 169);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnCancel_MouseLeave(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME25_BUTTONIMAGE_NEUTR;
            BtnCancel.ForeColor = Color.FromArgb(114, 153, 169);
        }

        private void BtnCancel_MouseEnter(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME25_BUTTONIMAGE_HOVER;
            BtnCancel.ForeColor = Color.FromArgb(173, 215, 232);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BFME25_BUTTONIMAGE_CLICK;
            BtnCancel.ForeColor = Color.FromArgb(114, 153, 169);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void SaveSettings()
        {
            if (File.Exists(Path.Combine(RegistryService.GameAppdataFolderPath(AssemblyNameHelper.BFMELauncherGameName), ConstStrings.C_OPTIONSINI_FILENAME)))
            {
                if (FlagModelLOD == "UltraHigh" && FlagAnimationLOD == "UltraHigh" && FlagEffectsLOD == "UltraHigh" && FlagShadowLOD == "UltraHigh" && FlagShaderLOD == "UltraHigh"
                    && FlagWaterLOD == "UltraHigh" && FlagTextureQualityLOD == "High" && FlagTerrainLOD == "High" && FlagDecalLOD == "High")
                {
                    OptionIniParser.WriteKey("StaticGameLOD", "UltraHigh", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("Resolution", ResolutionX.Text + " " + ResolutionY.Text, AssemblyNameHelper.BFMELauncherGameName);

                    OptionIniParser.DeleteKey("ModelLOD", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("AnimationLOD", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("EffectsLOD", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("ShadowLOD", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("ShaderLOD", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("WaterLOD", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("TextureQualityLOD", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("TerrainLOD", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.DeleteKey("DecalLOD", AssemblyNameHelper.BFMELauncherGameName);
                }
                else
                {
                    OptionIniParser.WriteKey("ModelLOD", FlagModelLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("AnimationLOD", FlagAnimationLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("EffectsLOD", FlagEffectsLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("ShadowLOD", FlagShadowLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("ShaderLOD", FlagShaderLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("WaterLOD", FlagWaterLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("TextureQualityLOD", FlagTextureQualityLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("TerrainLOD", FlagTerrainLOD, AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("DecalLOD", FlagDecalLOD, AssemblyNameHelper.BFMELauncherGameName);

                    OptionIniParser.WriteKey("StaticGameLOD", "Custom", AssemblyNameHelper.BFMELauncherGameName);
                    OptionIniParser.WriteKey("Resolution", ResolutionX.Text + " " + ResolutionY.Text, AssemblyNameHelper.BFMELauncherGameName);
                }
            }

            OptionIniParser.WriteKey("FixedStaticGameLOD", "UltraHigh", AssemblyNameHelper.BFMELauncherGameName);
            OptionIniParser.WriteKey("IdealStaticGameLOD", "UltraHigh", AssemblyNameHelper.BFMELauncherGameName);
            OptionIniParser.WriteKey("UseEAX3", FlagUseEAX, AssemblyNameHelper.BFMELauncherGameName);
            OptionIniParser.WriteKey("AudioLOD", FlagHighAudio, AssemblyNameHelper.BFMELauncherGameName);

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

            if (FlagBrutalAI && GameInstallPath != null)
                File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, "_patch222LibrariesBrutalAI.big"), Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big"), true);
            else if (GameInstallPath != null && File.Exists(Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big")))
                File.Delete(Path.Combine(GameInstallPath, "_patch222LibrariesBrutalAI.big"));

            if (FlagLauncherLanguageIndex != Settings.Default.LauncherLanguage)
            {
                FlagIsLanguageChanged = true;
            }

            Settings.Default.LauncherLanguage = FlagLauncherLanguageIndex;
            Settings.Default.StartGameWindowed = FlagWindowed;
            Settings.Default.UseBrutalAI = FlagBrutalAI;
            Settings.Default.Save();
        }
        #endregion

        #region Checkboxes for game specific settings

        private void ChkHighAudio_Click(object sender, EventArgs e)
        {

            if (FlagHighAudio == "High")
            {
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
                FlagHighAudio = "Low";
            }
            else
            {
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
                FlagHighAudio = "High";
            }
        }

        private void ChkHighAudio_MouseEnter(object sender, EventArgs e)
        {
            if (FlagHighAudio == "High")
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void ChkHighAudio_MouseLeave(object sender, EventArgs e)
        {
            if (FlagHighAudio == "High")
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            else
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_Unselected;
        }

        private void ChkHighAudio_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagHighAudio == "High")
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkHighAudio.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void ChkEAX_Click(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
            {
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
                FlagUseEAX = "no";
            }
            else
            {
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
                FlagUseEAX = "yes";
            }
        }

        private void ChkEAX_MouseEnter(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void ChkEAX_MouseLeave(object sender, EventArgs e)
        {
            if (FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_Unselected;
        }

        private void ChkEAX_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagUseEAX == "yes")
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkEAX.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void ChkWindowed_Click(object sender, EventArgs e)
        {
            if (FlagWindowed == true)
            {
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
                FlagWindowed = false;
            }
            else
            {
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
                FlagWindowed = true;
            }
        }

        private void ChkWindowed_MouseEnter(object sender, EventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
        }

        private void ChkWindowed_MouseLeave(object sender, EventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_Selected;
            else
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_Unselected;
        }

        private void ChkWindowed_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_SelectedHover;
            else
                ChkWindowed.Image = Helper.Properties.Resources.BFME25CHK_UnselectedHover;
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

        private void CmbSelectLauncherLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CmbSelectLauncherLanguage.SelectedIndex)
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

        private void TrackBarOptionsModelLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsFlagModelWaterAndShaderLOD graphicSettingsFlagModelLOD = (GraphicSettingsFlagModelWaterAndShaderLOD)TrackBarOptionsModelLOD.Value;
            LblTrackBarModelValue.Text = regex.Replace(graphicSettingsFlagModelLOD.ToString(), " ");

            FlagModelLOD = graphicSettingsFlagModelLOD.ToString();
        }

        private void TrackBarOptionsAnimationLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsFlagAnimationAndVFXLOD graphicSettingsFlagAnimationLOD = (GraphicSettingsFlagAnimationAndVFXLOD)TrackBarOptionsAnimationLOD.Value;
            LblTrackBarAnimationValue.Text = regex.Replace(graphicSettingsFlagAnimationLOD.ToString(), " ");

            FlagAnimationLOD = graphicSettingsFlagAnimationLOD.ToString();
        }

        private void TrackBarOptionsVFXLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsFlagAnimationAndVFXLOD graphicSettingsFlagVFXLOD = (GraphicSettingsFlagAnimationAndVFXLOD)TrackBarOptionsVFXLOD.Value;
            LblTrackBarVFXValue.Text = regex.Replace(graphicSettingsFlagVFXLOD.ToString(), " ");

            FlagEffectsLOD = graphicSettingsFlagVFXLOD.ToString();
        }

        private void TrackBarOptionsShadowsLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsFlagShadowsLOD graphicSettingsFlagShadowsLOD = (GraphicSettingsFlagShadowsLOD)TrackBarOptionsShadowsLOD.Value;
            LblTrackBarShadowsValue.Text = regex.Replace(graphicSettingsFlagShadowsLOD.ToString(), " ");

            FlagShadowLOD = graphicSettingsFlagShadowsLOD.ToString();
        }

        private void TrackBarOptionsShaderLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsFlagModelWaterAndShaderLOD graphicSettingsFlagShadowLOD = (GraphicSettingsFlagModelWaterAndShaderLOD)TrackBarOptionsShaderLOD.Value;
            LblTrackBarShaderValue.Text = regex.Replace(graphicSettingsFlagShadowLOD.ToString(), " ");

            FlagShaderLOD = graphicSettingsFlagShadowLOD.ToString();
        }

        private void TrackBarOptionsWaterLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsFlagModelWaterAndShaderLOD graphicSettingsFlagWaterLOD = (GraphicSettingsFlagModelWaterAndShaderLOD)TrackBarOptionsWaterLOD.Value;
            LblTrackBarWaterValue.Text = regex.Replace(graphicSettingsFlagWaterLOD.ToString(), " ");

            FlagWaterLOD = graphicSettingsFlagWaterLOD.ToString();
        }

        private void TrackBarOptionsTextureLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsTextureAndTerrainLOD graphicSettingsTextureLOD = (GraphicSettingsTextureAndTerrainLOD)TrackBarOptionsTextureLOD.Value;
            LblTrackBarTextureValue.Text = regex.Replace(graphicSettingsTextureLOD.ToString(), " ");

            FlagTextureQualityLOD = graphicSettingsTextureLOD.ToString();
        }

        private void TrackBarOptionsTerrainLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsTextureAndTerrainLOD graphicSettingsTerrainLOD = (GraphicSettingsTextureAndTerrainLOD)TrackBarOptionsTerrainLOD.Value;
            LblTrackBarTerrainValue.Text = regex.Replace(graphicSettingsTerrainLOD.ToString(), " ");

            FlagTerrainLOD = graphicSettingsTerrainLOD.ToString();
        }

        private void TrackBarOptionsDecalLOD_Scroll(object sender, EventArgs e)
        {
            GraphicSettingsDecalsLOD graphicSettingsDecalsLOD = (GraphicSettingsDecalsLOD)TrackBarOptionsDecalLOD.Value;
            LblTrackBarDecalValue.Text = regex.Replace(graphicSettingsDecalsLOD.ToString(), " ");

            FlagDecalLOD = graphicSettingsDecalsLOD.ToString();
        }
    }
}