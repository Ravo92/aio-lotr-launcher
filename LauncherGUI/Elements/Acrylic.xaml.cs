using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for Acrylic.xaml
    /// </summary>
    public partial class Acrylic : UserControl
    {
        public Acrylic()
        {
            InitializeComponent();
        }

        private void Update()
        {
            if (!IsLoaded || this.Background != null || this.FindCommonVisualAncestor(MainWindow.Instance!.windowGrid) == null)
                return;

            Point pos = this.TransformToVisual(MainWindow.Instance!.windowGrid).Transform(new Point(0, 0));
            image.Margin = new Thickness(-pos.X, -pos.Y, 0, 0);
            image.Width = MainWindow.Instance!.windowGrid.ActualWidth;
            image.Height = MainWindow.Instance!.windowGrid.ActualHeight;
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            Update();
        }

        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == (object)true)
                Update();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                this.Background = Brushes.Gray;

            Update();
        }
    }
}
