using System.Windows.Controls;
using System.IO;
using System;
using System.Windows;
using AllInOneLauncher.Logic;
using WindowsShortcutFactory;

namespace AllInOneLauncher.Pages.Subpages.Settings.Launcher
{
    public partial class Settings_LauncherGeneral : UserControl
    {
        private readonly string desktopShortCutFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Patch 2.22 Launcher.lnk");

        public Settings_LauncherGeneral()
        {
            InitializeComponent();
            DataContext = this;
            UpdateDesktopIconStatus();
        }

        private void UpdateDesktopIconStatus()
        {
            if (File.Exists(desktopShortCutFilePath))
            {
                CreateDesktopIcon.IsToggled = true;
                Properties.Settings.Default.CreateLauncherDesktopIcon = true;
            }
            else
            {
                CreateDesktopIcon.IsToggled = false;
                Properties.Settings.Default.CreateLauncherDesktopIcon = false;
            }
            Properties.Settings.Default.Save();
        }

        private void CreateOrDeleteDesktopShortcut(bool create)
        {
            if (create)
            {
                using var shortcut = new WindowsShortcut
                {
                    Path = Environment.ProcessPath ?? "",
                    Description = "All-in-One Launcher by Ravo92, MarcellVokk & the Bfme Foundation Project"
                };
                shortcut.Save(desktopShortCutFilePath);
            }
            else
            {
                if (File.Exists(desktopShortCutFilePath))
                {
                    File.Delete(desktopShortCutFilePath);
                }
            }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            LanguageDropdown.Selected = Properties.Settings.Default.LauncherLanguageSetting;
            CloseToTrayToggle.IsToggled = Properties.Settings.Default.CloseLauncherToTray;
            CreateDesktopIcon.IsToggled = Properties.Settings.Default.CreateLauncherDesktopIcon;
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

        private void OnCreateDesktopIconSwitched(object sender, EventArgs e)
        {
            CreateOrDeleteDesktopShortcut(CreateDesktopIcon.IsToggled);
            Properties.Settings.Default.CreateLauncherDesktopIcon = CreateDesktopIcon.IsToggled;
            Properties.Settings.Default.Save();
        }
    }
}