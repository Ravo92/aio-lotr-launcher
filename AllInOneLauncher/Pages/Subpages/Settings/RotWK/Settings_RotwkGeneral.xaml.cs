using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AllInOneLauncher.Logic;
using AllInOneLauncher.Data;
using BfmeFoundationProject.BfmeRegistryManagement;
using BfmeFoundationProject.BfmeRegistryManagement.Data;

namespace AllInOneLauncher.Pages.Subpages.Settings.Launcher
{
    public partial class Settings_RotwkGeneral : UserControl
    {
        private bool _isNotUserInteractionForLanguageDropDown = true;
        private string newRandomCDKey = string.Empty;

        public Settings_RotwkGeneral()
        {
            InitializeComponent();
            InitializePageElements();
        }

        private void InitializePageElements()
        {
            ComboBoxResolution.ItemsSource = SystemDisplayManager.GetAllSupportedResolutions();
            ComboBoxResolution.SelectedItem = !string.IsNullOrEmpty(Properties.Settings.Default.RotwkResolutionSetting) ? Properties.Settings.Default.RotwkResolutionSetting : ComboBoxResolution.Items[^1];
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.RotwkLanguageSetting != 0 ? Properties.Settings.Default.RotwkLanguageSetting : 0;

            string cdKey = BfmeRegistryManager.GetKeyValue((int)BfmeGame.ROTWK, BfmeRegistryKey.SerialKey);
            TextBoxCDKey.Text = string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)));

            TextBoxCDKey.IsEnabled = false;

            if (LauncherStateManager.IsElevated)
                ButtonChangeCdKey.Content = Application.Current.FindResource("SettingsPageBFMESectionGeneralRandomizeSerial");
            else
                TextBoxCDKey.IsEnabled = false;
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

        private void ButtonChangeCdKey_Click(object sender, RoutedEventArgs e)
        {
            LauncherStateManager.AsElevated(() =>
            {
                newRandomCDKey = string.Concat(from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 20) select s[System.Random.Shared.Next(s.Length)]);
                TextBoxCDKey.Text = string.Join("-", Enumerable.Range(0, newRandomCDKey.Length / 4).Select(i => newRandomCDKey.Substring(i * 4, 4)));
            });
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (LauncherStateManager.IsElevated)
                BfmeRegistryManager.SetKeyValue((int)BfmeGame.BFME1, BfmeRegistryKey.SerialKey, newRandomCDKey, Microsoft.Win32.RegistryValueKind.String);
        }
    }
}