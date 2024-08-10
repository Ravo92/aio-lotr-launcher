using AllInOneLauncher.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AllInOneLauncher.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings(string page)
        {
            InitializeComponent();
            Page = page;
        }

        private string _page = "";
        public string Page
        {
            get => _page;
            set
            {
                _page = value;

                foreach (var child in categoryTabs.Children)
                {
                    if (child is Button button)
                    {
                        if (button.Tag.ToString() == value)
                            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19FFFFFF"));
                        else
                            button.Background = Brushes.Transparent;
                    }
                }

                content.Child = (FrameworkElement)Activator.CreateInstance(LauncherStateManager.TypeMap![$"Settings_{value}"])!;
            }
        }

        private void OnCloseClicked(object sender, MouseButtonEventArgs e) => MainWindow.SetFullContent(null);

        private void OnCategoryButtonClicked(object sender, RoutedEventArgs e) => Page = ((Button)sender).Tag.ToString()!;
    }
}