using System.Windows.Controls;
using System.IO;
using System;
using System.Windows;
using LauncherGUI.Elements;
using LauncherGUI.Helpers;
using System.Collections.Specialized;

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
            // The first call will always be the language being added and set to the user-saved language.
            // We skip the first entry point here and then set the "isNotUserInteractionForLanguageDropDown" to false, so the user can actually change the value
            if (isNotUserInteractionForLanguageDropDown)
            {
                isNotUserInteractionForLanguageDropDown = false;
                return;
            }

            if (ComboBoxLanguage.SelectedIndex == 0)
                LauncherLanguageHelper.SetAvailableLauncherLanguage(0);
            else
                LauncherLanguageHelper.SetAvailableLauncherLanguage(ComboBoxLanguage.SelectedIndex);

            Properties.Settings.Default.LauncherLanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();

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
                        LibraryTile libraryTile = new()
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
    }
}