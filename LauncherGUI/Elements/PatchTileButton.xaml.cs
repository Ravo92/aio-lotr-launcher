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
    /// Interaction logic for PatchTileButton.xaml
    /// </summary>
    public partial class PatchTileButton : UserControl
    {
        public PatchTileButton()
        {
            InitializeComponent();
        }

        public event EventHandler? OnPatchSelected;

        public bool IsSelected
        {
            get => selectedIndicator.Visibility == Visibility.Visible;
            set
            {
                selectedIndicator.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                selectButton.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public string PatchName
        {
            get => patchName.Text;
            set => patchName.Text = value;
        }

        public string PatchVersion
        {
            get => patchVersion.Text;
            set => patchVersion.Text = value;
        }

        public string PatchReleaseDate
        {
            get => patchReleaseDate.Text;
            set => patchReleaseDate.Text = value;
        }

        private void OnSelectClicked(object sender, RoutedEventArgs e)
        {
            OnPatchSelected?.Invoke(this, EventArgs.Empty);
            IsSelected = true;
        }
    }
}
