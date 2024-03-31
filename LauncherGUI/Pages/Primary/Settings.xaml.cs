using LauncherGUI.Pages.Settings.Launcher;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LauncherGUI.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        LauncherSettings_General? launcherSettings_General;
        private BFME1Settings_General? bFME1Settings_General;

        private void OnCloseClicked(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SetFullContent(null);
        }

        private void SettingsMenuLauncherSettingsGeneralLabel_Click(object sender, RoutedEventArgs e)
        {
            DisposeAnyChildrenInPanel();

            if (launcherSettings_General == null)
            {
                launcherSettings_General = new LauncherSettings_General();
                PanelSettings.Children.Add(launcherSettings_General);
            }
            else
            {
                PanelSettings.Children.Add(launcherSettings_General);
            }
        }

        private void DisposeAnyChildrenInPanel()
        {
            if (PanelSettings.Children.Count > 0)
                PanelSettings.Children.Clear();
        }

        private void LauncherParentSettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            launcherSettings_General = new LauncherSettings_General();
            PanelSettings.Children.Add(launcherSettings_General);
        }

        private void SettingsBFME1General_Click(object sender, RoutedEventArgs e)
        {
            DisposeAnyChildrenInPanel();

            if (bFME1Settings_General == null)
            {
                bFME1Settings_General = new BFME1Settings_General();
                PanelSettings.Children.Add(bFME1Settings_General);
            }
            else
            {
                PanelSettings.Children.Add(bFME1Settings_General);
            }
        }
    }
}