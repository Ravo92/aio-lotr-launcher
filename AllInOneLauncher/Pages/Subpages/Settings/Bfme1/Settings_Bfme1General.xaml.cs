using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Security.Principal;
using AllInOneLauncher.Logic;
using AllInOneLauncher.Elements;
using AllInOneLauncher.Popups;

namespace AllInOneLauncher.Pages.Subpages.Settings.Launcher
{
    public partial class Settings_BFME1General : UserControl
    {
        private bool _isNotUserInteractionForLanguageDropDown = true;

        public Settings_BFME1General()
        {
            InitializeComponent();

            BFMERegistryManager.EnsureBFMEAppRegistry(0);
            BFMESettingsManager.EnsureOptionsFile(0);

            InitializeWindowElements();
        }

        private void InitializeWindowElements()
        {
            ComboBoxResolution.ItemsSource = SystemDisplayManager.GetAllSupportedResolutions();
            ComboBoxResolution.SelectedItem = !string.IsNullOrEmpty(Properties.Settings.Default.BFME1ResolutionSetting) ? Properties.Settings.Default.BFME1ResolutionSetting : ComboBoxResolution.Items[^1];
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.BFME1LanguageSetting != 0 ? Properties.Settings.Default.BFME1LanguageSetting : 0;

            string cdKey = BFMERegistryManager.GetBFMESerialKey(0);
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

        private void SaveLanguageSettings()
        {
            Properties.Settings.Default.BFME1LanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void SaveResolutionSettings()
        {
            Properties.Settings.Default.BFME1ResolutionSetting = ComboBoxResolution.SelectedItem?.ToString();
            BFMESettingsManager.Set(0, "Resolution", ComboBoxResolution.SelectedValue?.ToString() ?? string.Empty);
            Properties.Settings.Default.Save();
        }

        private void ButtonChangeCdKey_Click(object sender, RoutedEventArgs e)
        {
            LauncherStateManager.AsElevated(() =>
            {
                BFMERegistryManager.EnsureBFMEAppRegistry(0);
                string cdKey = BFMERegistryManager.GetBFMESerialKey(0);
                TextBoxCDKey.Text = string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)));
            });
        }
    }
}