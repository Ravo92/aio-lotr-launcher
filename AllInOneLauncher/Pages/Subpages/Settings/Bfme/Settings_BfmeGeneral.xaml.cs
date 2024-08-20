using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AllInOneLauncher.Logic;
using AllInOneLauncher.Data;
using BfmeFoundationProject.BfmeRegistryManagement;
using BfmeFoundationProject.BfmeRegistryManagement.Data;

namespace AllInOneLauncher.Pages.Subpages.Settings.Launcher
{
    public partial class Settings_BfmeGeneral : UserControl
    {
        public BfmeGame Game = BfmeGame.NONE;

        public Settings_BfmeGeneral(BfmeGame game)
        {
            Game = game;
            InitializeComponent();
            InitializePageElements();
        }

        private void InitializePageElements()
        {
            ResolutionDropdown.Options = SystemDisplayManager.GetAllSupportedResolutions();

            ResolutionDropdown.SelectedValue = Game switch
            {
                BfmeGame.BFME1 => !string.IsNullOrEmpty(Properties.Settings.Default.BFME1ResolutionSetting) ? Properties.Settings.Default.BFME1ResolutionSetting : ResolutionDropdown.Options.Last(),
                BfmeGame.BFME2 => !string.IsNullOrEmpty(Properties.Settings.Default.BFME2ResolutionSetting) ? Properties.Settings.Default.BFME2ResolutionSetting : ResolutionDropdown.Options.Last(),
                BfmeGame.ROTWK => !string.IsNullOrEmpty(Properties.Settings.Default.RotwkResolutionSetting) ? Properties.Settings.Default.RotwkResolutionSetting : ResolutionDropdown.Options.Last(),
                _ => ResolutionDropdown.Options.Last()
            };
            LanguageDropdown.Selected = Game switch
            {
                BfmeGame.BFME1 => Properties.Settings.Default.BFME1LanguageSetting != 0 ? Properties.Settings.Default.BFME1LanguageSetting : 0,
                BfmeGame.BFME2 => Properties.Settings.Default.BFME2LanguageSetting != 0 ? Properties.Settings.Default.BFME2LanguageSetting : 0,
                BfmeGame.ROTWK => Properties.Settings.Default.RotwkLanguageSetting != 0 ? Properties.Settings.Default.RotwkLanguageSetting : 0,
                _ => 0
            };
            title.Text = Game switch
            {
                BfmeGame.BFME1 => Application.Current.FindResource("SettingsPageBFME1SectionHeader").ToString(),
                BfmeGame.BFME2 => Application.Current.FindResource("SettingsPageBFME2SectionHeader").ToString(),
                BfmeGame.ROTWK => Application.Current.FindResource("SettingsPageRotWKSectionHeader").ToString(),
                _ => ""
            };

            string cdKey = BfmeRegistryManager.GetKeyValue((int)Game, BfmeRegistryKey.SerialKey);
            curentSerialNumber.Text = $"Curent serial number: {string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)))}";
            IconUAC.Visibility = LauncherStateManager.IsElevated ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnLanguageOptionSelected(object sender, System.EventArgs e)
        {
            if (Game == BfmeGame.BFME1) Properties.Settings.Default.BFME1LanguageSetting = LanguageDropdown.Selected;
            if (Game == BfmeGame.BFME2) Properties.Settings.Default.BFME2LanguageSetting = LanguageDropdown.Selected;
            if (Game == BfmeGame.ROTWK) Properties.Settings.Default.RotwkLanguageSetting = LanguageDropdown.Selected;
            BfmeRegistryManager.SetKeyValue((int)Game, BfmeRegistryKey.Language, LanguageDropdown.SelectedValue);
            Primary.Settings.NeedsResync = true;
            Properties.Settings.Default.Save();
        }

        private void OnGameResolutionOptionSelected(object sender, System.EventArgs e)
        {
            if (Game == BfmeGame.BFME1) Properties.Settings.Default.BFME1ResolutionSetting = ResolutionDropdown.SelectedValue;
            if (Game == BfmeGame.BFME2) Properties.Settings.Default.BFME2ResolutionSetting = ResolutionDropdown.SelectedValue;
            if (Game == BfmeGame.ROTWK) Properties.Settings.Default.RotwkResolutionSetting = ResolutionDropdown.SelectedValue;
            BfmeSettingsManager.Set(Game, "Resolution", ResolutionDropdown.SelectedValue);
            Properties.Settings.Default.Save();
        }

        private void OnGraphicsApiOptionSelected(object sender, System.EventArgs e)
        {
            if (Game == BfmeGame.BFME1) Properties.Settings.Default.BFME1GraphicAPISetting = GraphicsApiDropdown.Selected;
            if (Game == BfmeGame.BFME2) Properties.Settings.Default.BFME2GraphicAPISetting = GraphicsApiDropdown.Selected;
            if (Game == BfmeGame.ROTWK) Properties.Settings.Default.RotwkGraphicAPISetting = GraphicsApiDropdown.Selected;
            Properties.Settings.Default.Save();
        }

        private void ButtonChangeCdKey_Click(object sender, RoutedEventArgs e)
        {
            LauncherStateManager.AsElevated(() =>
            {
                string cdKey = string.Concat(from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 20) select s[System.Random.Shared.Next(s.Length)]);
                curentSerialNumber.Text = $"Curent serial number: {string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)))}";
                BfmeRegistryManager.SetKeyValue((int)Game, BfmeRegistryKey.SerialKey, cdKey, Microsoft.Win32.RegistryValueKind.String);
            });
        }
    }
}