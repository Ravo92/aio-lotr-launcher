using System.Windows.Controls;
using System.IO;
using System;
using System.Windows;
using AllInOneLauncher.Elements;
using System.Collections.Specialized;
using AllInOneLauncher.Logic;

namespace AllInOneLauncher.Pages.Subpages.Settings.Launcher
{
    /// <summary>
    /// Interaktionslogik für LauncherSettings_General.xaml
    /// </summary>
    public partial class Settings_LauncherGeneral : UserControl
    {
        public Settings_LauncherGeneral()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            LanguageDropdown.Selected = Properties.Settings.Default.LauncherLanguageSetting;
            CloseToTrayToggle.IsToggled = Properties.Settings.Default.CloseLauncherToTray;
        }

        private void OnLanguageOptionSelected(object sender, EventArgs e)
        {
            LauncherStateManager.Language = LanguageDropdown.Selected;
        }

        private void OnCloseToTraySwitched(object sender, EventArgs e)
        {
            Properties.Settings.Default.CloseLauncherToTray = CloseToTrayToggle.IsToggled;
            Properties.Settings.Default.Save();
        }
    }
}