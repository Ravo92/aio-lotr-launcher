using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for DiskDriveElement.xaml
    /// </summary>
    public partial class DiskDriveElement : UserControl
    {
        public DiskDriveElement()
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
                driveSpaceUsageBar.ProgressFillBrush = new SolidColorBrush(driveSpaceUsageBar.Progress < 85 ? (Color)ColorConverter.ConvertFromString("#FF7BFF7B") : (Color)ColorConverter.ConvertFromString("#FFFF8D7B"));
                driveSize.Text = $"{FreeSpace:N0} GB {Application.Current.FindResource("SettingsPageLauncherSectionGeneralDriveSizeText")} {DriveSize:N0} GB";
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
                driveSpaceUsageBar.ProgressFillBrush = new SolidColorBrush(driveSpaceUsageBar.Progress < 85 ? (Color)ColorConverter.ConvertFromString("#FF7BFF7B") : (Color)ColorConverter.ConvertFromString("#FFFF8D7B"));
                driveSize.Text = $"{FreeSpace:N0} GB {Application.Current.FindResource("SettingsPageLauncherSectionGeneralDriveSizeText")} {DriveSize:N0} GB";
            }
        }
    }
}