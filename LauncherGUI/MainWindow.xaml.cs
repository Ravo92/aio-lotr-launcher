using LauncherGUI.Helpers;
using LauncherGUI.Pages.Primary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LauncherGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        private static Library Library = new Library();
        private static Online Online = new Online();

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;

            fullContent.Visibility = Visibility.Visible;

            Width = System.Windows.SystemParameters.WorkArea.Width * 0.7;
            Height = System.Windows.SystemParameters.WorkArea.Height * 0.8;

            CheckSize();

            ShowLibrary();
        }

        public static void SetContent(FrameworkElement? newContent) => Instance.content.Child = newContent;
        public static void SetFullContent(FrameworkElement? newContent)
        {
            Instance.content.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.fullContent.Child = newContent;
        }

        public static void ShowLibrary()
        {
            SetContent(Library);

            foreach (TextBlock tab in Instance.tabs.Children.OfType<TextBlock>())
            {
                if (tab == Instance.libraryTab)
                    tab.Foreground = new SolidColorBrush(Color.FromRgb(21, 167, 233));
                else
                {
                    tab.Foreground = Brushes.White;
                    tab.Style = (Style)Instance.FindResource("TextBlockButton");
                }
            }
        }

        public static void ShowOnline()
        {
            SetContent(Online);

            foreach (TextBlock tab in Instance.tabs.Children.OfType<TextBlock>())
            {
                if (tab == Instance.onlineTab)
                    tab.Foreground = new SolidColorBrush(Color.FromRgb(21, 167, 233));
                else
                {
                    tab.Foreground = Brushes.White;
                    tab.Style = (Style)Instance.FindResource("TextBlockButton");
                }
            }
        }

        private void OnSettingsClicked(object sender, MouseButtonEventArgs e) => SetFullContent(new Settings());

        private void OnLibraryTabClicked(object sender, MouseButtonEventArgs e) => ShowLibrary();

        private void OnOnlineTabClicked(object sender, MouseButtonEventArgs e) => ShowOnline();

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => CheckSize();

        public void CheckSize()
        {
            var dpi = VisualTreeHelper.GetDpi(this);
            windowGrid.LayoutTransform = new ScaleTransform(1 / dpi.DpiScaleX * Math.Min(1, Math.Min((this.ActualWidth / 1500), (this.ActualHeight / 900))), 1 / dpi.DpiScaleX * Math.Min(1, Math.Min((this.ActualWidth / 1500), (this.ActualHeight / 900))));
        }
    }
}