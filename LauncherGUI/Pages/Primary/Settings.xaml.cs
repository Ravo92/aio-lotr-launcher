using LauncherGUI.Pages.Settings.Launcher;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        private BFME1Settings_Repair? bFME1Settings_Repair;
        private BFME2Settings_General? bFME2Settings_General;
        private BFME2Settings_Repair? bFME2Settings_Repair;
        private ROTWKSettings_General? rOTWKSettings_General;
        private ROTWKSettings_Repair? rOTWKSettings_Repair;

        private void OnCloseClicked(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SetFullContent(null);
        }

        private void LauncherParentSettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            launcherSettings_General = new LauncherSettings_General();
            PanelSettings.Child = launcherSettings_General;
            DrawSelectionOnMenuEntry(SettingsMenuLauncherSettingsGeneralLabel);
        }

        private void SettingsMenuLauncherSettingsGeneralLabel_Click(object sender, RoutedEventArgs e)
        {
            DrawSelectionOnMenuEntry(sender);

            if (launcherSettings_General == null)
            {
                launcherSettings_General = new LauncherSettings_General();
                PanelSettings.Child = launcherSettings_General;
            }
            else
            {
                PanelSettings.Child = launcherSettings_General;
            }
        }

        private void SettingsBFME1General_Click(object sender, RoutedEventArgs e)
        {
            DrawSelectionOnMenuEntry(sender);

            if (bFME1Settings_General == null)
            {
                bFME1Settings_General = new BFME1Settings_General();
                    PanelSettings.Child = bFME1Settings_General;
            }
            else
            {
                PanelSettings.Child = bFME1Settings_General;
            }
        }

        private void SettingsBFME1Repair_Click(object sender, RoutedEventArgs e)
        {
            DrawSelectionOnMenuEntry(sender);

            if (bFME1Settings_Repair == null)
            {
                bFME1Settings_Repair = new BFME1Settings_Repair();
                PanelSettings.Child = bFME1Settings_Repair;
            }
            else
            {
                PanelSettings.Child = bFME1Settings_Repair;
            }
        }

        private void SettingsBFME2General_Click(object sender, RoutedEventArgs e)
        {
            DrawSelectionOnMenuEntry(sender);

            if (bFME2Settings_General == null)
            {
                bFME2Settings_General = new BFME2Settings_General();
                PanelSettings.Child = bFME2Settings_General;
            }
            else
            {
                PanelSettings.Child = bFME2Settings_General;
            }
        }

        private void SettingsBFME2Repair_Click(object sender, RoutedEventArgs e)
        {
            DrawSelectionOnMenuEntry(sender);

            if (bFME2Settings_Repair == null)
            {
                bFME2Settings_Repair = new BFME2Settings_Repair();
                PanelSettings.Child = bFME2Settings_Repair;
            }
            else
            {
                PanelSettings.Child = bFME2Settings_Repair;
            }
        }

        private void SettingsRotWKGeneral_Click(object sender, RoutedEventArgs e)
        {
            DrawSelectionOnMenuEntry(sender);

            if (rOTWKSettings_General == null)
            {
                rOTWKSettings_General = new ROTWKSettings_General();
                PanelSettings.Child = rOTWKSettings_General;
            }
            else
            {
                PanelSettings.Child = rOTWKSettings_General;
            }
        }

        private void SettingsRotWKRepair_Click(object sender, RoutedEventArgs e)
        {
            DrawSelectionOnMenuEntry(sender);

            if (rOTWKSettings_Repair == null)
            {
                rOTWKSettings_Repair = new ROTWKSettings_Repair();
                PanelSettings.Child = rOTWKSettings_Repair;
            }
            else
            {
                PanelSettings.Child = rOTWKSettings_Repair;
            }
        }

        private void DrawSelectionOnMenuEntry(object sender)
        {
            Button clickedButton = (Button)sender;
            clickedButton.Background = Brushes.Transparent;

            foreach (var child in ButtonStackPanel.Children)
            {
                if (child is Button button)
                {
                    button.Background = Brushes.Transparent;

                    if (clickedButton == button)
                    {
                        button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19FFFFFF"));
                    }
                }
            }
        }
    }
}