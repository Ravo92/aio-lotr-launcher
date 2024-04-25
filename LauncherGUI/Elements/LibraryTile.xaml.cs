using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace LauncherGUI.Elements
{
    /// <summary>
    /// Interaction logic for LibraryTile.xaml
    /// </summary>
    public partial class LibraryTile : UserControl
    {
        public LibraryTile()
        {
            InitializeComponent();
        }

        public event EventHandler? OnPathSelected;
        private void OnSelectClicked(object sender, RoutedEventArgs e)
        {
            OnPathSelected?.Invoke(this, EventArgs.Empty);
        }

        private string? _driveName;
        public string DriveName
        {
            get => _driveName!;
            set
            {
                _driveName = value;

                driveName.Text = value;
            }
        }

        private double _driveSize;
        public double DriveSize
        {
            get => _driveSize;
            set
            {
                _driveSize = value;

                driveSpaceUsageBar.Progress = (DriveSize - FreeSpace) / DriveSize * 100d;
                driveSize.Text = $"{FreeSpace} GB {Application.Current.FindResource("SettingsLauncherGeneralDriveSizeText")} {DriveSize} GB";
            }
        }

        private double _freeSpace;
        public double FreeSpace
        {
            get => _freeSpace;
            set
            {
                _freeSpace = value;

                driveSpaceUsageBar.Progress = (DriveSize - FreeSpace) / DriveSize * 100d;
                driveSize.Text = $"{FreeSpace} GB {Application.Current.FindResource("SettingsLauncherGeneralDriveSizeText")} {DriveSize} GB";
            }
        }
    }
}