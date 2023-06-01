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
    public partial class GameOptionsForm : Form
    {
        string FlagStaticGameLOD = "UltraHigh";
        string FlagAnisotropicTextureFiltering = "yes";
        string FlagTerrainLighting = "yes";
        string Flag3DShadows = "yes";
        string Flag2DShadows = "yes";
        string FlagSmoothWaterBorder = "yes";
        string FlagShowProps = "yes";
        string FlagShowAnimations = "yes";
        string FlagHeatEffects = "yes";
        string FlagDynamicLOD = "yes";
        string FlagResolution;

        public GameOptionsForm()
        {
            SelectLanguage.Language _language = (SelectLanguage.Language)Settings.Default.Language;

            switch (_language)
            {
                case SelectLanguage.Language.English:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                    break;
                case SelectLanguage.Language.German:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                    break;
                default:
                    break;
            }

            InitializeComponent();

            KeyPreview = true;

            #region Styles
            //Main Form style behaviour

            BackgroundImage = Helper.Properties.Resources.bgMap;

            // Button-Styles
            BtnApply.FlatAppearance.BorderSize = 0;
            BtnApply.FlatStyle = FlatStyle.Flat;
            BtnApply.BackColor = Color.Transparent;
            BtnApply.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnApply.Font = FontHelper.GetFont(0, 16);
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);

            BtnCancel.FlatAppearance.BorderSize = 0;
            BtnCancel.FlatStyle = FlatStyle.Flat;
            BtnCancel.BackColor = Color.Transparent;
            BtnCancel.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnCancel.Font = FontHelper.GetFont(0, 16);
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);

            BtnDefault.FlatAppearance.BorderSize = 0;
            BtnDefault.FlatStyle = FlatStyle.Flat;
            BtnDefault.BackColor = Color.Transparent;
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnDefault.Font = FontHelper.GetFont(0, 16);
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);

            //Label-Styles
            LblGameSettings.Font = FontHelper.GetFont(1, 16);
            LblGameSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblGameSettings.BackColor = Color.Transparent;

            LblOptions.Font = FontHelper.GetFont(1, 20);
            LblOptions.ForeColor = Color.FromArgb(192, 145, 69);
            LblOptions.BackColor = Color.Black;

            LblInfoLOD.Font = FontHelper.GetFont(0, 16);
            LblInfoLOD.ForeColor = Color.FromArgb(192, 145, 69);
            LblInfoLOD.BackColor = Color.Transparent;

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

            ///////////////////////////////////////////////////////////////////////////////////////////

            //Checkbox-Styles
            if (OptionIniParser.ReadKey("StaticGameLOD") == "UltraHigh")
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
                FlagResolution = OptionIniParser.ReadKey("Resolution");
            }
            else
            {
                FlagStaticGameLOD = OptionIniParser.ReadKey("StaticGameLOD");
                FlagAnisotropicTextureFiltering = OptionIniParser.ReadKey("AnisotropicTextureFiltering");
                FlagTerrainLighting = OptionIniParser.ReadKey("TerrainLighting");
                Flag3DShadows = OptionIniParser.ReadKey("3DShadows");
                Flag2DShadows = OptionIniParser.ReadKey("2DShadows");
                FlagSmoothWaterBorder = OptionIniParser.ReadKey("SmoothWaterBorder");
                FlagShowProps = OptionIniParser.ReadKey("ShowProps");
                FlagShowAnimations = OptionIniParser.ReadKey("ExtraAnimations");
                FlagHeatEffects = OptionIniParser.ReadKey("HeatEffects");
                FlagDynamicLOD = OptionIniParser.ReadKey("DynamicLOD");
                FlagResolution = OptionIniParser.ReadKey("Resolution");
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkAniTextureFiltering.FlatAppearance.BorderSize = 0;
            ChkAniTextureFiltering.FlatStyle = FlatStyle.Flat;
            ChkAniTextureFiltering.BackColor = Color.Transparent;
            ChkAniTextureFiltering.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagAnisotropicTextureFiltering == "no")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkUnselected;
            }
            else if (FlagAnisotropicTextureFiltering == "yes")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkSelected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkTerrainLighting.FlatAppearance.BorderSize = 0;
            ChkTerrainLighting.FlatStyle = FlatStyle.Flat;
            ChkTerrainLighting.BackColor = Color.Transparent;
            ChkTerrainLighting.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagTerrainLighting == "no")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkUnselected;
            }
            else if (FlagTerrainLighting == "yes")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkSelected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            Chk3DShadows.FlatAppearance.BorderSize = 0;
            Chk3DShadows.FlatStyle = FlatStyle.Flat;
            Chk3DShadows.BackColor = Color.Transparent;
            Chk3DShadows.ForeColor = Color.FromArgb(192, 145, 69);

            if (Flag3DShadows == "no")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.chkUnselected;
            }
            else if (Flag3DShadows == "yes")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.chkSelected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            Chk2DShadows.FlatAppearance.BorderSize = 0;
            Chk2DShadows.FlatStyle = FlatStyle.Flat;
            Chk2DShadows.BackColor = Color.Transparent;
            Chk2DShadows.ForeColor = Color.FromArgb(192, 145, 69);

            if (Flag2DShadows == "no")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.chkUnselected;
            }
            else if (Flag2DShadows == "yes")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.chkSelected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkSmoothWaterBorder.FlatAppearance.BorderSize = 0;
            ChkSmoothWaterBorder.FlatStyle = FlatStyle.Flat;
            ChkSmoothWaterBorder.BackColor = Color.Transparent;
            ChkSmoothWaterBorder.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagSmoothWaterBorder == "no")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkUnselected;
            }
            else if (FlagSmoothWaterBorder == "yes")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkSelected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkShowProps.FlatAppearance.BorderSize = 0;
            ChkShowProps.FlatStyle = FlatStyle.Flat;
            ChkShowProps.BackColor = Color.Transparent;
            ChkShowProps.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagShowProps == "no")
            {
                ChkShowProps.Image = Helper.Properties.Resources.chkUnselected;
            }
            else if (FlagShowProps == "yes")
            {
                ChkShowProps.Image = Helper.Properties.Resources.chkSelected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkShowAnimations.FlatAppearance.BorderSize = 0;
            ChkShowAnimations.FlatStyle = FlatStyle.Flat;
            ChkShowAnimations.BackColor = Color.Transparent;
            ChkShowAnimations.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagShowAnimations == "no")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.chkUnselected;
            }
            else if (FlagShowAnimations == "yes")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.chkSelected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkHeatEffects.FlatAppearance.BorderSize = 0;
            ChkHeatEffects.FlatStyle = FlatStyle.Flat;
            ChkHeatEffects.BackColor = Color.Transparent;
            ChkHeatEffects.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagHeatEffects == "no")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.chkUnselected;
            }
            else if (FlagHeatEffects == "yes")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.chkSelected;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkDynamicLOD.FlatAppearance.BorderSize = 0;
            ChkDynamicLOD.FlatStyle = FlatStyle.Flat;
            ChkDynamicLOD.BackColor = Color.Transparent;
            ChkDynamicLOD.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagDynamicLOD == "yes")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkUnselected;
                LblInfoLOD.Hide();
            }
            else if (FlagDynamicLOD == "no")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkSelected;
                LblInfoLOD.Show();
            }

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

            ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkSelected;
            ChkTerrainLighting.Image = Helper.Properties.Resources.chkSelected;
            Chk3DShadows.Image = Helper.Properties.Resources.chkSelected;
            Chk2DShadows.Image = Helper.Properties.Resources.chkSelected;
            ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkSelected;
            ChkShowProps.Image = Helper.Properties.Resources.chkSelected;
            ChkShowAnimations.Image = Helper.Properties.Resources.chkSelected;
            ChkHeatEffects.Image = Helper.Properties.Resources.chkSelected;
            ChkDynamicLOD.Image = Helper.Properties.Resources.chkUnselected;

            ResolutionX.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
            ResolutionY.Text = Screen.PrimaryScreen.Bounds.Height.ToString();
        }

        private void BtnDefault_MouseLeave(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnDefault_MouseEnter(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnDefault.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnDefault_MouseDown(object sender, MouseEventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void BtnApply_MouseLeave(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnApply_MouseEnter(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnApply.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnApply_MouseDown(object sender, MouseEventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnCancel_MouseLeave(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnCancel_MouseEnter(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnCancel.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void SaveSettings()
        {
            if (File.Exists(Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME)))
            {
                if (FlagAnisotropicTextureFiltering == "yes" && FlagTerrainLighting == "yes" && Flag3DShadows == "yes" && Flag2DShadows == "yes" && FlagSmoothWaterBorder == "yes"
                    && FlagShowProps == "yes" && FlagShowAnimations == "yes" && FlagHeatEffects == "yes" && FlagDynamicLOD == "yes")
                {
                    OptionIniParser.WriteKey("StaticGameLOD", FlagStaticGameLOD);
                    OptionIniParser.WriteKey("Resolution", ResolutionX.Text + " " + ResolutionY.Text);

                    OptionIniParser.DeleteKey("AnisotropicTextureFiltering");
                    OptionIniParser.DeleteKey("TerrainLighting");
                    OptionIniParser.DeleteKey("3DShadows");
                    OptionIniParser.DeleteKey("2DShadows");
                    OptionIniParser.DeleteKey("SmoothWaterBorder");
                    OptionIniParser.DeleteKey("ShowProps");
                    OptionIniParser.DeleteKey("ExtraAnimations");
                    OptionIniParser.DeleteKey("HeatEffects");
                    OptionIniParser.DeleteKey("DynamicLOD");
                }
                else
                {
                    OptionIniParser.WriteKey("StaticGameLOD", "Custom");
                    OptionIniParser.WriteKey("AnisotropicTextureFiltering", FlagAnisotropicTextureFiltering);
                    OptionIniParser.WriteKey("TerrainLighting", FlagTerrainLighting);
                    OptionIniParser.WriteKey("3DShadows", Flag3DShadows);
                    OptionIniParser.WriteKey("2DShadows", Flag2DShadows);
                    OptionIniParser.WriteKey("SmoothWaterBorder", FlagSmoothWaterBorder);
                    OptionIniParser.WriteKey("ShowProps", FlagShowProps);
                    OptionIniParser.WriteKey("ExtraAnimations", FlagShowAnimations);
                    OptionIniParser.WriteKey("HeatEffects", FlagHeatEffects);
                    OptionIniParser.WriteKey("DynamicLOD", FlagDynamicLOD);
                    OptionIniParser.WriteKey("Resolution", ResolutionX.Text + " " + ResolutionY.Text);
                }
            }

            OptionIniParser.WriteKey("FixedStaticGameLOD", "UltraHigh");
            OptionIniParser.WriteKey("IdealStaticGameLOD", "UltraHigh");
            OptionIniParser.ClearOptionsFile();
        }
        #endregion

        #region Checkboxes for game specific settings

        private void ChkAniTextureFiltering_Click(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagAnisotropicTextureFiltering = "no";
            }
            else
            {
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagAnisotropicTextureFiltering = "yes";
            }
        }

        private void ChkAniTextureFiltering_MouseEnter(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkAniTextureFiltering_MouseLeave(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkAniTextureFiltering_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkAniTextureFiltering.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkTerrainLighting_Click(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagTerrainLighting = "no";
            }
            else
            {
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagTerrainLighting = "yes";
            }
        }

        private void ChkTerrainLighting_MouseEnter(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkTerrainLighting_MouseLeave(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkTerrainLighting_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkTerrainLighting.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void Chk3DShadows_Click(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
            {
                Chk3DShadows.Image = Helper.Properties.Resources.chkUnselectedHover;
                Flag3DShadows = "no";
            }
            else
            {
                Chk3DShadows.Image = Helper.Properties.Resources.chkSelectedHover;
                Flag3DShadows = "yes";
            }
        }

        private void Chk3DShadows_MouseEnter(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void Chk3DShadows_MouseLeave(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.chkSelected;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void Chk3DShadows_MouseDown(object sender, MouseEventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                Chk3DShadows.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void Chk2DShadows_Click(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
            {
                Chk2DShadows.Image = Helper.Properties.Resources.chkUnselectedHover;
                Flag2DShadows = "no";
            }
            else
            {
                Chk2DShadows.Image = Helper.Properties.Resources.chkSelectedHover;
                Flag2DShadows = "yes";
            }
        }

        private void Chk2DShadows_MouseEnter(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void Chk2DShadows_MouseLeave(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.chkSelected;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void Chk2DShadows_MouseDown(object sender, MouseEventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                Chk2DShadows.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkSmoothWaterBorder_Click(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagSmoothWaterBorder = "no";
            }
            else
            {
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagSmoothWaterBorder = "yes";
            }
        }

        private void ChkSmoothWaterBorder_MouseEnter(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkSmoothWaterBorder_MouseLeave(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkSmoothWaterBorder_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkSmoothWaterBorder.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkShowProps_Click(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
            {
                ChkShowProps.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagShowProps = "no";
            }
            else
            {
                ChkShowProps.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagShowProps = "yes";
            }
        }

        private void ChkShowProps_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkShowProps.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkShowProps_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkShowProps.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkShowProps_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkShowProps.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkShowAnimations_Click(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagShowAnimations = "no";
            }
            else
            {
                ChkShowAnimations.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagShowAnimations = "yes";
            }
        }

        private void ChkShowAnimations_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkShowAnimations_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkShowAnimations_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkShowAnimations.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkHeatEffects_Click(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagHeatEffects = "no";
            }
            else
            {
                ChkHeatEffects.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagHeatEffects = "yes";
            }
        }

        private void ChkHeatEffects_MouseEnter(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkHeatEffects_MouseLeave(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkHeatEffects_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkHeatEffects.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkDynamicLOD_Click(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkUnselectedHover;
                FlagDynamicLOD = "yes";
                LblInfoLOD.Hide();
            }
            else
            {
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkSelectedHover;
                FlagDynamicLOD = "no";
                LblInfoLOD.Show();
            }
        }

        private void ChkDynamicLOD_MouseEnter(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkUnselectedHover;
        }

        private void ChkDynamicLOD_MouseLeave(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkSelected;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkUnselected;
        }

        private void ChkDynamicLOD_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagDynamicLOD == "no")
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkSelectedHover;
            else
                ChkDynamicLOD.Image = Helper.Properties.Resources.chkUnselectedHover;
        }
        #endregion

        private void OptionsBFME1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
