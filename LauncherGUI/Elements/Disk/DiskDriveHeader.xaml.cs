using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LauncherGUI.Elements
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
