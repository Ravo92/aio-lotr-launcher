using Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatchLauncher.Properties;
using System.Reflection;

namespace PatchLauncher
{
    public partial class OptionsBFME1 : Form
    {
        //Launcher Settings
        bool FlagEAX = Settings.Default.EAXSupport;
        readonly bool FlagEAXFileExists = File.Exists(ConstStrings.GameInstallPath() + @"\dsound.dll");
        bool FlagWindowed = Settings.Default.StartGameWindowed;
        bool FlagBrutalAI = Settings.Default.UseBrutalAI;
        bool IsSettingChanged = true;

        //Game Settings
        string FlagAnisotropicTextureFiltering = "yes";
        string FlagTerrainLighting = "yes";
        string Flag3DShadows = "yes";
        string Flag2DShadows = "yes";
        string FlagSmoothWaterBorder = "yes";
        string FlagShowProps = "yes";
        string FlagShowAnimations = "yes";
        string FlagHeatEffects = "yes";
        string FlagDynamicLOD = "no";
        string FlagResolution;

        public OptionsBFME1()
        {
            InitializeComponent();

            Settings.Default.SettingChanging += SettingChanging;

            KeyPreview = true;

            #region Styles
            //Main Form style behaviour

            BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "bgMap.png"));

            // Button-Styles
            BtnDefault.FlatAppearance.BorderSize = 0;
            BtnDefault.FlatStyle = FlatStyle.Flat;
            BtnDefault.BackColor = Color.Transparent;
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnDefault.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);

            //Label-Styles
            LblEAX.Text = "Activate support for EAX-Sound";
            LblEAX.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblEAX.ForeColor = Color.FromArgb(192, 145, 69);
            LblEAX.BackColor = Color.Transparent;

            LblLauncherSettings.Text = "Launcher Settings";
            LblLauncherSettings.Font = ConstStrings.UseFont("SachaWynterTight", 16);
            LblLauncherSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblLauncherSettings.BackColor = Color.Transparent;

            LblGameSettings.Text = "Game Settings";
            LblGameSettings.Font = ConstStrings.UseFont("SachaWynterTight", 16);
            LblGameSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblGameSettings.BackColor = Color.Transparent;

            LblOptions.Text = "Settings";
            LblOptions.Font = ConstStrings.UseFont("SachaWynterTight", 20);
            LblOptions.ForeColor = Color.FromArgb(192, 145, 69);
            LblOptions.BackColor = Color.Black;

            LblPatchVersion.Text = "Patch 2.22v29";
            LblPatchVersion.Font = ConstStrings.UseFont("Albertus Nova", 11);
            LblPatchVersion.ForeColor = Color.FromArgb(136, 82, 46);
            LblPatchVersion.BackColor = Color.Transparent;

            LblLauncherVersion.Text = "Launcher Version: \n" + Assembly.GetEntryAssembly()!.GetName().Version;
            LblLauncherVersion.Font = ConstStrings.UseFont("Albertus Nova", 11);
            LblLauncherVersion.ForeColor = Color.FromArgb(136, 82, 46);
            LblLauncherVersion.BackColor = Color.Transparent;

            LblWindowed.Text = "Launch game in windowed mode";
            LblWindowed.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblWindowed.ForeColor = Color.FromArgb(192, 145, 69);
            LblWindowed.BackColor = Color.Transparent;

            LblBrutalAI.Text = "Use the experimental brutal AI";
            LblBrutalAI.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblBrutalAI.ForeColor = Color.FromArgb(192, 145, 69);
            LblBrutalAI.BackColor = Color.Transparent;

            LblWarningAI.Text = "";
            LblWarningAI.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblWarningAI.ForeColor = Color.Red;
            LblWarningAI.BackColor = Color.Transparent;

            if (FlagBrutalAI)
            {
                LblWarningAI.Text = "WARNING: Brutal AI is activated. \n You may not be able to play online";
                LblWarningAI.Show();
            }
            else
            {
                LblWarningAI.Hide();
            }

            LblAniTextureFiltering.Text = "Anisotropic Texture Filtering";
            LblAniTextureFiltering.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblAniTextureFiltering.ForeColor = Color.FromArgb(192, 145, 69);
            LblAniTextureFiltering.BackColor = Color.Transparent;

            LblTerrainLighting.Text = "Terrain Lighting";
            LblTerrainLighting.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblTerrainLighting.ForeColor = Color.FromArgb(192, 145, 69);
            LblTerrainLighting.BackColor = Color.Transparent;

            Lbl3DShadows.Text = "3D Shadows";
            Lbl3DShadows.Font = ConstStrings.UseFont("Albertus Nova", 16);
            Lbl3DShadows.ForeColor = Color.FromArgb(192, 145, 69);
            Lbl3DShadows.BackColor = Color.Transparent;

            Lbl2DShadows.Text = "2D Shadows";
            Lbl2DShadows.Font = ConstStrings.UseFont("Albertus Nova", 16);
            Lbl2DShadows.ForeColor = Color.FromArgb(192, 145, 69);
            Lbl2DShadows.BackColor = Color.Transparent;

            LblSmoothWaterBorder.Text = "Smooth Water Border";
            LblSmoothWaterBorder.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblSmoothWaterBorder.ForeColor = Color.FromArgb(192, 145, 69);
            LblSmoothWaterBorder.BackColor = Color.Transparent;

            LblShowProps.Text = "Show Props";
            LblShowProps.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblShowProps.ForeColor = Color.FromArgb(192, 145, 69);
            LblShowProps.BackColor = Color.Transparent;

            LblShowAnimations.Text = "Show Animations";
            LblShowAnimations.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblShowAnimations.ForeColor = Color.FromArgb(192, 145, 69);
            LblShowAnimations.BackColor = Color.Transparent;

            LblHeatEffects.Text = "Heat Effects";
            LblHeatEffects.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblHeatEffects.ForeColor = Color.FromArgb(192, 145, 69);
            LblHeatEffects.BackColor = Color.Transparent;

            LblDynamicLOD.Text = "Dynamic Level of Detail";
            LblDynamicLOD.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblDynamicLOD.ForeColor = Color.FromArgb(192, 145, 69);
            LblDynamicLOD.BackColor = Color.Transparent;

            LblResolutionX.Text = "X";
            LblResolutionX.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblResolutionX.ForeColor = Color.FromArgb(192, 145, 69);
            LblResolutionX.BackColor = Color.Transparent;

            LblResolution.Text = "Set Game Resolution";
            LblResolution.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblResolution.ForeColor = Color.FromArgb(192, 145, 69);
            LblResolution.BackColor = Color.Transparent;

            //Checkbox-Styles
            if (Settings.Default.IsGameInstalled == true)
            {
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
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (FlagAnisotropicTextureFiltering == "yes")
            {
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkTerrainLighting.FlatAppearance.BorderSize = 0;
            ChkTerrainLighting.FlatStyle = FlatStyle.Flat;
            ChkTerrainLighting.BackColor = Color.Transparent;
            ChkTerrainLighting.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagTerrainLighting == "no")
            {
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (FlagTerrainLighting == "yes")
            {
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            Chk3DShadows.FlatAppearance.BorderSize = 0;
            Chk3DShadows.FlatStyle = FlatStyle.Flat;
            Chk3DShadows.BackColor = Color.Transparent;
            Chk3DShadows.ForeColor = Color.FromArgb(192, 145, 69);

            if (Flag3DShadows == "no")
            {
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (Flag3DShadows == "yes")
            {
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            Chk2DShadows.FlatAppearance.BorderSize = 0;
            Chk2DShadows.FlatStyle = FlatStyle.Flat;
            Chk2DShadows.BackColor = Color.Transparent;
            Chk2DShadows.ForeColor = Color.FromArgb(192, 145, 69);

            if (Flag2DShadows == "no")
            {
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (Flag2DShadows == "yes")
            {
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkSmoothWaterBorder.FlatAppearance.BorderSize = 0;
            ChkSmoothWaterBorder.FlatStyle = FlatStyle.Flat;
            ChkSmoothWaterBorder.BackColor = Color.Transparent;
            ChkSmoothWaterBorder.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagSmoothWaterBorder == "no")
            {
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (FlagSmoothWaterBorder == "yes")
            {
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkShowProps.FlatAppearance.BorderSize = 0;
            ChkShowProps.FlatStyle = FlatStyle.Flat;
            ChkShowProps.BackColor = Color.Transparent;
            ChkShowProps.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagShowProps == "no")
            {
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (FlagShowProps == "yes")
            {
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkShowAnimations.FlatAppearance.BorderSize = 0;
            ChkShowAnimations.FlatStyle = FlatStyle.Flat;
            ChkShowAnimations.BackColor = Color.Transparent;
            ChkShowAnimations.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagShowAnimations == "no")
            {
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (FlagShowAnimations == "yes")
            {
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkHeatEffects.FlatAppearance.BorderSize = 0;
            ChkHeatEffects.FlatStyle = FlatStyle.Flat;
            ChkHeatEffects.BackColor = Color.Transparent;
            ChkHeatEffects.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagHeatEffects == "no")
            {
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (FlagHeatEffects == "yes")
            {
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ChkDynamicLOD.FlatAppearance.BorderSize = 0;
            ChkDynamicLOD.FlatStyle = FlatStyle.Flat;
            ChkDynamicLOD.BackColor = Color.Transparent;
            ChkDynamicLOD.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagDynamicLOD == "no")
            {
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            }
            else if (FlagDynamicLOD == "yes")
            {
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            }

            ///////////////////////////////////////////////////////////////////////////////////////////
          
            ResolutionX.BackColor = Color.Black;
            ResolutionX.Font = ConstStrings.UseFont("Albertus Nova", 14);
            ResolutionX.ForeColor = Color.FromArgb(192, 145, 69);

            ResolutionY.BackColor = Color.Black;
            ResolutionY.Font = ConstStrings.UseFont("Albertus Nova", 14);
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
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));

            ChkWindowed.FlatAppearance.BorderSize = 0;
            ChkWindowed.FlatStyle = FlatStyle.Flat;
            ChkWindowed.BackColor = Color.Transparent;
            ChkWindowed.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagWindowed)
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));

            ChkBrutalAI.FlatAppearance.BorderSize = 0;
            ChkBrutalAI.FlatStyle = FlatStyle.Flat;
            ChkBrutalAI.BackColor = Color.Transparent;
            ChkBrutalAI.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagBrutalAI)
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));

            #endregion
        }

        #region Buttons and checkboxes for launcher specific settings

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            FlagEAX = false;
            FlagWindowed = false;
            FlagBrutalAI = false;
            ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
            ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));

            FlagAnisotropicTextureFiltering = "yes";
            FlagTerrainLighting = "yes";
            Flag3DShadows = "yes";
            Flag2DShadows = "yes";
            FlagSmoothWaterBorder = "yes";
            FlagShowProps = "yes";
            FlagShowAnimations = "yes";
            FlagHeatEffects = "yes";
            FlagDynamicLOD = "no";
            FlagResolution = "yes";

            ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));

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

        private void SaveSettings()
        {
            //Save Launcher-Settings
            Settings.Default.EAXSupport = FlagEAX;
            Settings.Default.StartGameWindowed = FlagWindowed;
            Settings.Default.UseBrutalAI = FlagBrutalAI;
            Settings.Default.Save();

            //Save Game-Settings
            if (File.Exists(Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME)))
            {
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

            //Settings-Valuations

            if (!FlagEAXFileExists && FlagEAX == true)
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Copy(Path.Combine("Tools", file), Path.Combine(ConstStrings.GameInstallPath(), file), true);
                }

                OptionIniParser.WriteKey("UseEAX3", "yes");
            }

            if (FlagEAXFileExists && FlagEAX == false)
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Delete(Path.Combine(ConstStrings.GameInstallPath(), file));
                }

                OptionIniParser.WriteKey("UseEAX3", "no");
            }

            if (FlagBrutalAI && ConstStrings.GameInstallPath() != null)
                File.Copy(Path.Combine("Tools", "_patch222LibrariesBrutalAI.big"), Path.Combine(ConstStrings.GameInstallPath(), "_patch222LibrariesBrutalAI.big"), true);
            else if (ConstStrings.GameInstallPath() != null && File.Exists(Path.Combine(ConstStrings.GameInstallPath(), "_patch222LibrariesBrutalAI.big")))
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), "_patch222LibrariesBrutalAI.big"));
        }

        private void DontSave()
        {
            if (Settings.Default.EAXSupport)
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));

            if (Settings.Default.StartGameWindowed)
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));

            if (Settings.Default.UseBrutalAI)
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkEAX_Click(object sender, EventArgs e)
        {
            if (FlagEAX == true)
            {
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagEAX = false;
            }
            else
            {
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagEAX = true;
            }
        }

        private void ChkEAX_MouseEnter(object sender, EventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkEAX_MouseLeave(object sender, EventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkEAX_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkEAX.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkWindowed_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagWindowed == true)
            {
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagWindowed = false;
            }
            else
            {
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagWindowed = true;
            }
        }

        private void ChkWindowed_MouseEnter(object sender, EventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkWindowed_MouseLeave(object sender, EventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkWindowed_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkWindowed.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkBrutalAI_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagBrutalAI == true)
            {
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                LblWarningAI.Hide();
                FlagBrutalAI = false;
            }
            else
            {
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                LblWarningAI.Show();
                FlagBrutalAI = true;
            }
        }

        private void ChkBrutalAI_MouseEnter(object sender, EventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkBrutalAI_MouseLeave(object sender, EventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkBrutalAI_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkBrutalAI.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        #endregion

        #region Checkboxes for game specific settings

        private void ChkAniTextureFiltering_Click(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
            {
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagAnisotropicTextureFiltering = "no";
            }
            else
            {
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagAnisotropicTextureFiltering = "yes";
            }
        }

        private void ChkAniTextureFiltering_MouseEnter(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkAniTextureFiltering_MouseLeave(object sender, EventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkAniTextureFiltering_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagAnisotropicTextureFiltering == "yes")
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkAniTextureFiltering.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkTerrainLighting_Click(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
            {
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagTerrainLighting = "no";
            }
            else
            {
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagTerrainLighting = "yes";
            }
        }

        private void ChkTerrainLighting_MouseEnter(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkTerrainLighting_MouseLeave(object sender, EventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkTerrainLighting_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagTerrainLighting == "yes")
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkTerrainLighting.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void Chk3DShadows_Click(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
            {
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                Flag3DShadows = "no";
            }
            else
            {
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                Flag3DShadows = "yes";
            }
        }

        private void Chk3DShadows_MouseEnter(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void Chk3DShadows_MouseLeave(object sender, EventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void Chk3DShadows_MouseDown(object sender, MouseEventArgs e)
        {
            if (Flag3DShadows == "yes")
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                Chk3DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void Chk2DShadows_Click(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
            {
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                Flag2DShadows = "no";
            }
            else
            {
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                Flag2DShadows = "yes";
            }
        }

        private void Chk2DShadows_MouseEnter(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void Chk2DShadows_MouseLeave(object sender, EventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void Chk2DShadows_MouseDown(object sender, MouseEventArgs e)
        {
            if (Flag2DShadows == "yes")
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                Chk2DShadows.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkSmoothWaterBorder_Click(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
            {
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagSmoothWaterBorder = "no";
            }
            else
            {
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagSmoothWaterBorder = "yes";
            }
        }

        private void ChkSmoothWaterBorder_MouseEnter(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkSmoothWaterBorder_MouseLeave(object sender, EventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkSmoothWaterBorder_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagSmoothWaterBorder == "yes")
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkSmoothWaterBorder.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkShowProps_Click(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
            {
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagShowProps = "no";
            }
            else
            {
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagShowProps = "yes";
            }
        }

        private void ChkShowProps_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkShowProps_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkShowProps_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowProps == "yes")
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkShowProps.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkShowAnimations_Click(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
            {
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagShowAnimations = "no";
            }
            else
            {
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagShowAnimations = "yes";
            }
        }

        private void ChkShowAnimations_MouseEnter(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkShowAnimations_MouseLeave(object sender, EventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkShowAnimations_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagShowAnimations == "yes")
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkShowAnimations.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkHeatEffects_Click(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
            {
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagHeatEffects = "no";
            }
            else
            {
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagHeatEffects = "yes";
            }
        }

        private void ChkHeatEffects_MouseEnter(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkHeatEffects_MouseLeave(object sender, EventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkHeatEffects_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagHeatEffects == "yes")
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkHeatEffects.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkDynamicLOD_Click(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "yes")
            {
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
                FlagDynamicLOD = "no";
            }
            else
            {
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
                FlagDynamicLOD = "yes";
            }
        }

        private void ChkDynamicLOD_MouseEnter(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "yes")
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }

        private void ChkDynamicLOD_MouseLeave(object sender, EventArgs e)
        {
            if (FlagDynamicLOD == "yes")
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelected.png"));
            else
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselected.png"));
        }

        private void ChkDynamicLOD_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagDynamicLOD == "yes")
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkSelectedHover.png"));
            else
                ChkDynamicLOD.Image = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "chkUnselectedHover.png"));
        }
        #endregion

        private void OptionsBFME1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSettingChanged)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save the settings made?", "Save Settings", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SaveSettings();
                }
                else if (dialogResult == DialogResult.No)
                {
                    DontSave();
                }
            }
        }

        void SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            IsSettingChanged = true;
        }

        private void OptionsBFME1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
