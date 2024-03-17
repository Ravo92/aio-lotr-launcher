using System.Windows.Controls;

namespace LauncherGUI.Pages.Settings.Launcher
{
    /// <summary>
    /// Interaktionslogik für LauncherSettings_General.xaml
    /// </summary>
    public partial class LauncherSettings_General : UserControl
    {
        bool isNotUserInteractionForLanguageDropDown = true;
        public LauncherSettings_General()
        {
            InitializeComponent();
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.LauncherLanguageSetting;
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

            if (ComboBoxLanguage.SelectedIndex == 0)
                Helpers.LauncherLanguageHelper.GetAvailableLauncherLanguage(0);
            else
                Helpers.LauncherLanguageHelper.GetAvailableLauncherLanguage(ComboBoxLanguage.SelectedIndex);

            Properties.Settings.Default.LauncherLanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();
        }
    }
}