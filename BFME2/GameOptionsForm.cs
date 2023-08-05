using Helper;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class GameOptionsForm : Form
    {
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
        string FlagResolution;

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
            LblGameSettings.Font = FontHelper.GetFont(1, 16);
            LblGameSettings.ForeColor = Color.FromArgb(168, 190, 98);
            LblGameSettings.BackColor = Color.Transparent;

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
            if (File.Exists(Path.Combine(RegistryService.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME)))
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
        #endregion

        private void OptionsBFME2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
