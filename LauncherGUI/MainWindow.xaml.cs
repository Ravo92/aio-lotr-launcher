using LauncherGUI.Helpers;
using LauncherGUI.Pages.Primary;
using System;
using System.Diagnostics;
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
        private static readonly Library Library = new();
        private static readonly Online Online = new();
        private static readonly Guides Guides = new();
        private static readonly Workshop Workshop = new();

        public MainWindow()
        {
            MutexHelper.BringApplicationInFrontOrCreateNewMutex();

            if (MutexHelper.MutexAlreadyExists)
            {
                Application.Current.Shutdown();
            }

            InitializeComponent();
            Instance = this;

            fullContent.Visibility = Visibility.Visible;

            Width = SystemParameters.WorkArea.Width * 0.7;
            Height = SystemParameters.WorkArea.Height * 0.8;

            LauncherLanguageHelper.GetAvailableLauncherLanguage(Properties.Settings.Default.LauncherLanguageSetting);

            CheckSize();
            ShowLibrary();
        }

        public static void SetContent(FrameworkElement? newContent)
        {
            Instance.content.Child = newContent;
        }

        public static void SetFullContent(FrameworkElement? newContent)
        {
            Instance.content.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.fullContent.Child = newContent;

            Instance.libraryTab.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.onlineTab.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.workshopTab.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.guidesTab.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
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

        public static void ShowWorkShop()
        {
            SetContent(Workshop);

            foreach (TextBlock tab in Instance.tabs.Children.OfType<TextBlock>())
            {
                if (tab == Instance.workshopTab)
                    tab.Foreground = new SolidColorBrush(Color.FromRgb(21, 167, 233));
                else
                {
                    tab.Foreground = Brushes.White;
                    tab.Style = (Style)Instance.FindResource("TextBlockButton");
                }
            }
        }

        public static void ShowGuides()
        {
            SetContent(Guides);

            foreach (TextBlock tab in Instance.tabs.Children.OfType<TextBlock>())
            {
                if (tab == Instance.guidesTab)
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

        private void OnWorkShopItemsTabClicked(object sender, MouseButtonEventArgs e) => ShowWorkShop();

        private void OnGuidesTabClicked(object sender, MouseButtonEventArgs e) => ShowGuides();

        private void OnYouTubeTabClicked(object sender, MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo("explorer", "https://www.youtube.com/@BeyondStandards"));

        private void OnTwitchTabClicked(object sender, MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo("explorer", "https://www.twitch.tv/beyondstandards"));

        private void OnDiscordTabClicked(object sender, MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo("explorer", "https://discord.com/invite/Q5Yyy3XCuu"));

        private void OnModDBTabClicked(object sender, MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo("explorer", "https://www.moddb.com/mods/battle-for-middle-earth-patch-222"));

        private void OnPatreonTabClicked(object sender, MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo("explorer", "https://www.patreon.com/AllInOneLauncher"));

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => CheckSize();

        public void CheckSize()
        {
            var dpi = VisualTreeHelper.GetDpi(this);
            windowGrid.LayoutTransform = new ScaleTransform(1 / dpi.DpiScaleX * Math.Min(1, Math.Min((ActualWidth / 1500), (ActualHeight / 900))), 1 / dpi.DpiScaleX * Math.Min(1, Math.Min((ActualWidth / 1500), (ActualHeight / 900))));
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}