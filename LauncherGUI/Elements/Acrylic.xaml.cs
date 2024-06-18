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

        public Int32Rect GetAbsolutePlacement()
        {
            Point pos = this.TransformToVisual(MainWindow.Instance!.windowGrid).Transform(new Point(0, 0));
            return new Int32Rect((int)pos.X, (int)pos.Y, (int)this.ActualWidth, (int)this.ActualHeight);
        }

        private void Update()
        {
            try
            {
                Int32Rect rec = GetAbsolutePlacement();
                if (rec.Width > 0 && rec.Height > 0 && rec.X + rec.Width <= MainWindow.Instance!.AcrylicBackground.PixelWidth && rec.Y + rec.Height <= MainWindow.Instance!.AcrylicBackground.PixelHeight)
                    image.Source = new CroppedBitmap(MainWindow.Instance.AcrylicBackground, rec);
            }
            catch { }
        }

        private bool IgnoreNextUpdate = false;
        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this) || !IsLoaded)
                return;

            if (IgnoreNextUpdate)
            {
                IgnoreNextUpdate = false;
                return;
            }

            IgnoreNextUpdate = true;

            Update();
        }

        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this) || !IsLoaded)
                return;

            if (e.NewValue == (object)true)
                Update();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this) && this.Background != null)
                this.Background = null;

            if (!DesignerProperties.GetIsInDesignMode(this))
                Update();
        }
    }
}
