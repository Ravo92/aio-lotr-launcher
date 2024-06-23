using LauncherGUI.Elements;
using System.IO;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LauncherGUI.Popups
{
    /// <summary>
    /// Interaction logic for InstallGameDialog.xaml
    /// </summary>
    public partial class InstallGameDialog : PopupBody
    {
        public InstallGameDialog()
        {
            InitializeComponent();

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.VolumeLabel.Contains("Google"))
                {
                    if (drive.DriveType != DriveType.CDRom && drive.DriveType != DriveType.Network && drive.DriveType != DriveType.Removable)
                    {
                        Selectable element = new Selectable()
                        {
                            Title = new DiskDriveHeader() { DriveName = string.Concat(drive.VolumeLabel, " (", drive.Name[..^1], ")"), FreeSpace = drive.AvailableFreeSpace },
                            Tag = drive.Name,
                            Margin = new Thickness(0, 0, 0, 5)
                        };

                        locations.Children.Add(element);
                    }
                }
            }
        }

        private void ComboBoxGameLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxLibrarySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonAcceptClicked(object sender, RoutedEventArgs e) => Submit("English", Selectable.GetSelectedTagInContainer(locations)!.ToString()!);

        private void ButtonCancelClicked(object sender, RoutedEventArgs e) => Dismiss();
    }
}
