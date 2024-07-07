using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AllInOneLauncher.Logic;
using AllInOneLauncher.Data;

namespace AllInOneLauncher.Pages.Subpages.Settings.Launcher
{
    public partial class Settings_RotwkGeneral : UserControl
    {
        private bool _isNotUserInteractionForLanguageDropDown = true;

        public Settings_RotwkGeneral()
        {
            InitializeComponent();

            BfmeRegistryManager.EnsureBfmeAppRegistry(BfmeGame.ROTWK);
            BfmeSettingsManager.EnsureOptionsFile(BfmeGame.ROTWK);

            InitializePageElements();
        }

        private void InitializePageElements()
        {
            ComboBoxResolution.ItemsSource = SystemDisplayManager.GetAllSupportedResolutions();
            ComboBoxResolution.SelectedItem = !string.IsNullOrEmpty(Properties.Settings.Default.RotwkResolutionSetting) ? Properties.Settings.Default.RotwkResolutionSetting : ComboBoxResolution.Items[^1];
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.RotwkLanguageSetting != 0 ? Properties.Settings.Default.RotwkLanguageSetting : 0;

            string cdKey = BfmeRegistryManager.GetBfmeSerialKey(BfmeGame.ROTWK);
            TextBoxCDKey.Text = string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)));

            if (LauncherStateManager.IsElevated)
                ButtonChangeCdKey.Content = Application.Current.FindResource("SettingsBFMEGeneralCDKeyButtonTextGenerate");
        }

        private void ComboBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isNotUserInteractionForLanguageDropDown)
            {
                _isNotUserInteractionForLanguageDropDown = false;
                return;
            }
            else
            {
                SaveLanguageSettings();
            }
        }

        private void ComboBoxResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveResolutionSettings();
        }

        private void ComboBoxGraphicsAPI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveGraphicsAPISettings();
        }

        private void SaveLanguageSettings()
        {
            Properties.Settings.Default.RotwkLanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void SaveGraphicsAPISettings()
        {
            Properties.Settings.Default.RotwkGraphicAPISetting = ComboBoxGraphicsAPI.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void SaveResolutionSettings()
        {
            Properties.Settings.Default.RotwkResolutionSetting = ComboBoxResolution.SelectedItem?.ToString();
            BfmeSettingsManager.Set(BfmeGame.ROTWK, "Resolution", ComboBoxResolution.SelectedValue?.ToString() ?? string.Empty);
            Properties.Settings.Default.Save();
        }

        private void ButtonChangeCdKey_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LauncherStateManager.AsElevated(() =>
            {
                BfmeRegistryManager.EnsureBfmeAppRegistry(BfmeGame.ROTWK);
                string cdKey = BfmeRegistryManager.GetBfmeSerialKey(BfmeGame.ROTWK);
                TextBoxCDKey.Text = string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)));
            });
        }
    }
}