using System;
using System.Windows.Controls;

namespace LauncherGUI.Pages.Settings.Launcher
{
    /// <summary>
    /// Interaktionslogik für ROTWKSettings_General.xaml
    /// </summary>
    public partial class ROTWKSettings_General : UserControl
    {
        bool isNotUserInteractionForResolutionDropDown = true;
        bool isNotUserInteractionForLanguageDropDown = true;

        public ROTWKSettings_General()
        {
            InitializeComponent();
        }

        private void ComboBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // The first call will always be the resolutions being added and set to the user-saved resolution.
            // We skip the first entry point here and then set the "isNotUserInteractionForLanguageDropDown" to false, so the user actually can change the value
            if (isNotUserInteractionForLanguageDropDown)
            {
                isNotUserInteractionForLanguageDropDown = false;
                return;
            }

            Properties.Settings.Default.ROTWKLanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void ComboBoxResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // The first call will always be the resolutions being added and set to the user-saved resolution.
            // We skip the first entry point here and then set the "isNotUserInteractionForResolutionDropDown" to false, so the user actually can change the value
            if (isNotUserInteractionForResolutionDropDown)
            {
                isNotUserInteractionForResolutionDropDown = false;
                return;
            }

            Properties.Settings.Default.ROTWKResolutionSetting = ComboBoxResolution.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void ROTWKChildSettingsWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ROTWKChildSettingsWindow_Initialized(object sender, EventArgs e)
        {
            ComboBoxResolution.ItemsSource = Helpers.DesktopResolutionHelper.GetAllSupportedResolutions();

            if (Properties.Settings.Default.ROTWKResolutionSetting != null)
                ComboBoxResolution.SelectedItem = Properties.Settings.Default.ROTWKResolutionSetting;
            else
            {
                ComboBoxResolution.SelectedItem = ComboBoxLanguage.Items.Count - 1;
            }

            if (Properties.Settings.Default.ROTWKLanguageSetting != 0)
                ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.ROTWKLanguageSetting;
            else
            {
                ComboBoxLanguage.SelectedIndex = 0;
            }
        }
    }
}