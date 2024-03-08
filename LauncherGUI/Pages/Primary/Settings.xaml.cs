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

        private void OnCloseClicked(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SetFullContent(null);
        }

        private void SettingsMenuLauncherSettingsGeneralLabel_Click(object sender, RoutedEventArgs e)
        {
            if (launcherSettings_General == null)
            {
                launcherSettings_General = new LauncherSettings_General();
                PanelSettings.Children.Add(launcherSettings_General);
            }
        }
    }
}