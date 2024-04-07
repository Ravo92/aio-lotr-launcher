using LauncherGUI.Helpers;
using System.Windows.Controls;

namespace LauncherGUI.Pages.Settings.Launcher
{
    public partial class BFME1Settings_General : UserControl
    {
        private bool _isNotUserInteractionForResolutionDropDown = true;
        private bool _isNotUserInteractionForLanguageDropDown = true;

        public BFME1Settings_General()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            ComboBoxResolution.ItemsSource = DesktopResolutionHelper.GetAllSupportedResolutions();
            ComboBoxResolution.SelectedItem = !string.IsNullOrEmpty(Properties.Settings.Default.BFME1ResolutionSetting) ? Properties.Settings.Default.BFME1ResolutionSetting : ComboBoxResolution.Items.Count - 1;
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.BFME1LanguageSetting != 0 ? Properties.Settings.Default.BFME1LanguageSetting : 0;
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
            Properties.Settings.Default.BFME1LanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void SaveResolutionSettings()
        {
            Properties.Settings.Default.BFME1ResolutionSetting = ComboBoxResolution.SelectedItem?.ToString();
            BFMEIniEditorHelper.WriteKey("Resolution", ComboBoxResolution.SelectedValue?.ToString() ?? string.Empty, "BFME1");
            Properties.Settings.Default.Save();
        }
    }
}