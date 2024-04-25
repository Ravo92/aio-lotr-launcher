using System;
using System.ComponentModel;
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
    }

    public class LibraryTileData
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _driveLetter;
        private string? _driveSizes;
        private double _progressBarValue;
        private double _progressBarMaxValue;

        public string DriveLetter
        {
            get => _driveLetter!;
            set
            {
                _driveLetter = value;
                OnPropertyChanged();
            }
        }

        public string DriveSizes
        {
            get => _driveSizes!;
            set
            {
                _driveSizes = value;
                OnPropertyChanged();
            }
        }

        public double ProgressBarValue
        {
            get => _progressBarValue;
            set
            {
                _progressBarValue = value;
                OnPropertyChanged();
            }
        }

        public double ProgressBarMaxValue
        {
            get => _progressBarMaxValue;
            set
            {
                _progressBarMaxValue = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}