using LauncherGUI.Helpers;
using System.Windows.Controls;

namespace LauncherGUI.Pages.Settings.Launcher
{
    public partial class ROTWKSettings_General : UserControl
    {
        private bool _isNotUserInteractionForResolutionDropDown = true;
        private bool _isNotUserInteractionForLanguageDropDown = true;

        public ROTWKSettings_General()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            ComboBoxResolution.ItemsSource = DesktopResolutionHelper.GetAllSupportedResolutions();
            ComboBoxResolution.SelectedItem = !string.IsNullOrEmpty(Properties.Settings.Default.ROTWKResolutionSetting) ? Properties.Settings.Default.ROTWKResolutionSetting : ComboBoxResolution.Items.Count - 1;
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.ROTWKLanguageSetting != 0 ? Properties.Settings.Default.ROTWKLanguageSetting : 0;
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
            if (_isNotUserInteractionForResolutionDropDown)
            {
                _isNotUserInteractionForResolutionDropDown = false;
                return;
            }
            else
            {
                SaveResolutionSettings();
            }
        }

        private void SaveLanguageSettings()
        {
            Properties.Settings.Default.ROTWKLanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void SaveResolutionSettings()
        {
            Properties.Settings.Default.ROTWKResolutionSetting = ComboBoxResolution.SelectedItem?.ToString();
            BFMEIniEditorHelper.WriteKey("Resolution", ComboBoxResolution.SelectedValue?.ToString() ?? string.Empty, "ROTWK");
            Properties.Settings.Default.Save();
        }
    }
}