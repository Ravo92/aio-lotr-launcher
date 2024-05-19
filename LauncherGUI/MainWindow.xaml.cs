using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using LauncherGUI.Helpers;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using LauncherGUI.Pages.Primary;
using System.Collections.Specialized;

namespace LauncherGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }
        private static readonly Library Library = new();
        private static readonly Online Online = new();
        private static readonly Guides Guides = new();
        private static readonly Workshop Workshop = new();

        public MainWindow(string argument)
        {
            InitializeComponent();
            Instance = this;

            TrayIcon.Visibility = Visibility.Collapsed;
            fullContent.Visibility = Visibility.Visible;

            Width = SystemParameters.WorkArea.Width * 0.7;
            Height = SystemParameters.WorkArea.Height * 0.8;

            LauncherConfigHelper.MigrateLauncherSettings();
            LauncherLanguageHelper.SetAvailableLauncherLanguage(Properties.Settings.Default.LauncherLanguageSetting);

            Properties.Settings.Default.DefaultLibraryPath = Path.GetPathRoot(Environment.CurrentDirectory);
            StringCollection myStringCollection = Properties.Settings.Default.UsedLibraryPartitions;

            if (!Properties.Settings.Default.UsedLibraryPartitions.Contains(Properties.Settings.Default.DefaultLibraryPath))
            {
                myStringCollection.Add(Path.GetPathRoot(Environment.CurrentDirectory));
                Properties.Settings.Default.UsedLibraryPartitions = myStringCollection;
                Properties.Settings.Default.Save();
            }

            CheckSize();
            ShowLibrary();

            if (!string.IsNullOrEmpty(argument))
            {
                if (argument == "--SetKeyBFME1")
                    SetFullContent(new Settings(GameSelectorHelper.AvailableBFMEGames.BFME1));
                else if (argument == "--SetKeyBFME2")
                    SetFullContent(new Settings(GameSelectorHelper.AvailableBFMEGames.BFME2));
                else if (argument == "--SetKeyROTWK")
                    SetFullContent(new Settings(GameSelectorHelper.AvailableBFMEGames.ROTWK));
            }
        }

        public static void SetContent(FrameworkElement? newContent)
        {
            Instance!.content.Child = newContent;
        }

        public static void SetFullContent(FrameworkElement? newContent)
        {
            Instance!.content.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.fullContent.Child = newContent;

            Instance.tabs.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.icons.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
        }

        public static void ShowLibrary()
        {
            SetContent(Library);

            foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
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

            foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
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

            foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
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

            foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
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

        private void OnSettingsTabClicked(object sender, MouseButtonEventArgs e) => SetFullContent(new Settings(GameSelectorHelper.AvailableBFMEGames.NONE));

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

        private void TrayMenuShowApplication_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal || Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                TrayIcon.Visibility = Visibility.Visible;
                LauncherConfigHelper.SetWindowInvisible();
            }
            else
            {
                TrayIcon.Visibility = Visibility.Collapsed;
                LauncherConfigHelper.SetWindowVisible();
            }
        }

        private void TrayMenuCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TrayIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            TrayIcon.Visibility = Visibility.Hidden;
            LauncherConfigHelper.SetWindowVisible();
        }

        private void LauncherMainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ReloadContextMenu();
            TrayIcon.Visibility = Visibility.Visible;
            LauncherConfigHelper.SetWindowInvisible();
        }

        private void ReloadContextMenu()
        {
            if (TrayIcon.ContextMenu != null)
            {
                TrayIcon.ContextMenu = null;
            }

            ContextMenu newContextMenu = new()
            {
                Background = Brushes.White
            };

            MenuItem showApplicationItem = new()
            {
                Header = Application.Current.FindResource("LauncherTrayContextMenuShowApplication")
            };
            showApplicationItem.Click += TrayMenuShowApplication_Click;

            MenuItem closeApplicationItem = new()
            {
                Header = Application.Current.FindResource("LauncherTrayContextMenuCloseApplication")
            };

            closeApplicationItem.Click += TrayMenuCloseApplication_Click;
            newContextMenu.Items.Add(showApplicationItem);
            newContextMenu.Items.Add(closeApplicationItem);

            TrayIcon.ContextMenu = newContextMenu;
        }
    }
}