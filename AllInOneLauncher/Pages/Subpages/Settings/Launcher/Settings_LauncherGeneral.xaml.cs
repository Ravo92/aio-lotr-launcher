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
        bool isNotUserInteractionForLanguageDropDown = true;

        public Settings_LauncherGeneral()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.LauncherLanguageSetting;
            CheckBoxCloseToTray.IsChecked = Properties.Settings.Default.CloseLauncherToTray;
            GetDriveData();
        }

        private void ComboBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isNotUserInteractionForLanguageDropDown)
            {
                isNotUserInteractionForLanguageDropDown = false;
                return;
            }

            if (ComboBoxLanguage.SelectedIndex == 0)
                LauncherStateManager.Language = 0;
            else
                LauncherStateManager.Language = ComboBoxLanguage.SelectedIndex;

            GetDriveData();
        }

        private void GetDriveData()
        {
            libraryTiles.Children.Clear();

            StringCollection myStringCollection = Properties.Settings.Default.UsedLibraryPartitions;

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.VolumeLabel.Contains("Google"))
                {
                    if (drive.DriveType != DriveType.CDRom || drive.DriveType != DriveType.Network || drive.DriveType != DriveType.Removable)
                    {
                        DiskDriveElement libraryTile = new()
                        {
                            DriveName = string.Concat(drive.VolumeLabel == string.Empty ? Application.Current.FindResource("SettingsLauncherGeneralDriveDefaultNameText") : drive.VolumeLabel, " (", drive.Name[..^1], ")"),
                            DriveSize = Math.Floor(drive.TotalSize / Math.Pow(1024, 3)),
                            FreeSpace = Math.Floor(drive.AvailableFreeSpace / Math.Pow(1024, 3))
                        };

                        if (myStringCollection.Contains(drive.Name))
                            libraryTiles.Children.Add(libraryTile);
                    }
                }
            }
        }

        private void CheckBoxCloseToTray_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CloseLauncherToTray = true;
            Properties.Settings.Default.Save();
        }

        private void CheckBoxCloseToTray_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CloseLauncherToTray = false;
            Properties.Settings.Default.Save();
        }

        private void ButtonAddLibrary_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}