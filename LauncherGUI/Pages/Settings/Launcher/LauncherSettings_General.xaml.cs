using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;
using System;
using System.Windows;
using LauncherGUI.Elements;
using LauncherGUI.Helpers;

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
            DataContext = this;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.LauncherLanguageSetting;
            GetDriveData();
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
                LauncherLanguageHelper.GetAvailableLauncherLanguage(0);
            else
                LauncherLanguageHelper.GetAvailableLauncherLanguage(ComboBoxLanguage.SelectedIndex);

            Properties.Settings.Default.LauncherLanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();

            GetDriveData();
        }

        private void GetDriveData()
        {
            libraryTiles.Children.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.VolumeLabel.Contains("Google"))
                {
                    if (drive.DriveType != DriveType.CDRom || drive.DriveType != DriveType.Network || drive.DriveType != DriveType.Removable)
                    {
                        LibraryTile libraryTile = new()
                        {
                            DriveName = string.Concat(drive.VolumeLabel == string.Empty ? Application.Current.FindResource("SettingsLauncherGeneralDriveDefaultNameText") : drive.VolumeLabel, " (", drive.Name[..^1], ")"),
                            DriveSize = Math.Floor(drive.TotalSize / Math.Pow(1024, 3)),
                            FreeSpace = Math.Floor(drive.AvailableFreeSpace / Math.Pow(1024, 3))
                        };

                        libraryTiles.Children.Add(libraryTile);
                    }
                }
            }
        }
    }
}