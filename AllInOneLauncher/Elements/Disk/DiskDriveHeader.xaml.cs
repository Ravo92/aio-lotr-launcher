using System;
using System.Windows.Controls;

namespace AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for DiskDriveHeader.xaml
    /// </summary>
    public partial class DiskDriveHeader : UserControl
    {
        public DiskDriveHeader()
        {
            InitializeComponent();
        }

        public string DriveName
        {
            get => name.Text;
            set => name.Text = value;
        }

        long _freeSpace = 0;
        public long FreeSpace
        {
            get => _freeSpace;
            set
            {
                _freeSpace = value;
                space.Text = $"{Math.Round(value / Math.Pow(1024, 3), 1)} GB FREE";
            }
        }
    }
}
