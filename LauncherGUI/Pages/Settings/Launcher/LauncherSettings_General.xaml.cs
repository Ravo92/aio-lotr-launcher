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
        public ObservableCollection<LibraryTileData> LibraryTileCollection { get; } = new();

        bool isNotUserInteractionForLanguageDropDown = true;
        public LauncherSettings_General()
        {
            InitializeComponent();
            DataContext = this;
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
                LauncherLanguageHelper.GetAvailableLauncherLanguage(0);
            else
                LauncherLanguageHelper.GetAvailableLauncherLanguage(ComboBoxLanguage.SelectedIndex);

            Properties.Settings.Default.LauncherLanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();

            LibraryTileCollection.Clear();
            GetDriveData();
        }

        private void GetDriveData()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.VolumeLabel.Contains("Google"))
                {
                    if (drive.DriveType != DriveType.CDRom || drive.DriveType != DriveType.Network || drive.DriveType != DriveType.Removable)
                    {
                        LibraryTileData libraryTileData = new()
                        {
                            DriveLetter = string.Concat(drive.VolumeLabel == string.Empty ? Application.Current.FindResource("SettingsLauncherGeneralDriveDefaultNameText") : drive.VolumeLabel, " (", drive.Name[..^1], ")"),
                            DriveSizes = string.Concat((drive.AvailableFreeSpace / Math.Pow(1024, 3)).ToString("0"), " GB ", Application.Current.FindResource("SettingsLauncherGeneralDriveSizeText"), " ", (drive.TotalSize / Math.Pow(1024, 3)).ToString("0"), " GB"),
                            ProgressBarValue = Convert.ToInt32((drive.AvailableFreeSpace / drive.TotalSize) * 100),
                            ProgressBarMaxValue = 100
                        };

                        LibraryTileCollection.Add(libraryTileData);
                    }
                }
            }
        }
    }
}