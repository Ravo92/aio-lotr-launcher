using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using AllInOneLauncher.Pages.Primary;
using System.Collections.Specialized;
using System.Windows.Media.Effects;
using AllInOneLauncher.Logic;
using AllInOneLauncher.Elements;
using AllInOneLauncher.Popups;
using System.Reflection;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace AllInOneLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }

        public MainWindow(string[] args)
        {
            InitializeComponent();
            Instance = this;

            LauncherStateManager.Init();
            SystemInputManager.Init();
            LauncherUpdateManager.CheckForUpdates();
            BfmeWorkshopSyncManager.UseExperimentalBaseGameFiles = true;

            TrayIcon.Visibility = Visibility.Collapsed;
            fullContent.Visibility = Visibility.Visible;

            if (LauncherStateManager.IsElevated)
                IconUAC.Visibility = Visibility.Collapsed;

            Width = SystemParameters.WorkArea.Width * 0.7;
            Height = SystemParameters.WorkArea.Height * 0.8;

            Properties.Settings.Default.DefaultLibraryPath = Path.GetPathRoot(Environment.CurrentDirectory);
            StringCollection myStringCollection = Properties.Settings.Default.UsedLibraryPartitions;

            if (!Properties.Settings.Default.UsedLibraryPartitions.Contains(Properties.Settings.Default.DefaultLibraryPath))
            {
                myStringCollection.Add(Path.GetPathRoot(Environment.CurrentDirectory));
                Properties.Settings.Default.UsedLibraryPartitions = myStringCollection;
                Properties.Settings.Default.Save();
            }

            CheckSize();
            ReloadContextMenu();
            ShowOffline();

            if (args.Length > 0)
            {
                if (args[0] == "--Settings" && args.Length > 1)
                    SetFullContent(new Settings(args[1]));
                else if (args[0] == "--Game" && args.Length > 1)
                    Offline.Instance.gameTabs.InitialSelectedIndex = int.Parse(args[1]);
                else if (args[0] == "--Online")
                    ShowOnline();
                else if (args[0] == "--showLauncherUpdateLog")
                    PopupVisualizer.ShowPopup(new LauncherChangelogPopup());
            }

            Application.Current.Exit += OnApplicationExit;
        }

        public static void SetContent(FrameworkElement? newContent) => Instance!.content.Child = newContent;

        public static void SetFullContent(FrameworkElement? newContent)
        {
            Instance!.content.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.fullContent.Child = newContent;

            Instance.tabs.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.icons.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;

            if (newContent is Settings)
                Instance.background.Effect = new BlurEffect() { Radius = 20 };
            else
                Instance.background.Effect = null;
        }

        public static void ShowOffline()
        {
            SetContent(Offline.Instance);

            foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
            {
                if (tab == Instance.offlineTab)
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
            SetContent(Online.Instance);

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

        public static void ShowGuides()
        {
            SetContent(Guides.Instance);

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

        private void OnOfflineTabClicked(object sender, MouseButtonEventArgs e) => ShowOffline();
        private void OnOnlineTabClicked(object sender, MouseButtonEventArgs e) => ShowOnline();
        private void OnGuidesTabClicked(object sender, MouseButtonEventArgs e) => ShowGuides();

        private void OnSettingsButtonClicked(object sender, MouseButtonEventArgs e) => SetFullContent(new Settings("LauncherGeneral"));
        private void OnLinkButtonClicked(object sender, MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo("explorer", ((FrameworkElement)sender).Tag.ToString() ?? ""));

        private void OnLoad(object sender, RoutedEventArgs e) => CheckSize();
        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => CheckSize();

        public void CheckSize() => windowGrid.LayoutTransform = new ScaleTransform(Math.Min(10, Math.Min((ActualWidth / 1700), (ActualHeight / 1200))), Math.Min(10, Math.Min((ActualWidth / 1700), (ActualHeight / 1200))));

        private void OnTrayIconDoubleClicked(object sender, RoutedEventArgs e) => LauncherStateManager.Visible = true;

        private void LauncherMainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Properties.Settings.Default.CloseLauncherToTray)
            {
                e.Cancel = true;
                ReloadContextMenu();
                TrayIcon.Visibility = Visibility.Visible;
                LauncherStateManager.Visible = false;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void ReloadContextMenu()
        {
            if (TrayIcon.ContextMenu != null)
                TrayIcon.ContextMenu = null;

            ContextMenu newContextMenu = new()
            {
                Background = Brushes.White
            };
            TrayIcon.ContextMenu = newContextMenu;

            MenuItem showApplicationItem = new()
            {
                Header = Application.Current.FindResource("TrayIconShowApplication")
            };
            showApplicationItem.Click += (s, e) => LauncherStateManager.Visible = true;
            newContextMenu.Items.Add(showApplicationItem);

            MenuItem closeApplicationItem = new()
            {
                Header = Application.Current.FindResource("TrayIconCloseApplication")
            };
            closeApplicationItem.Click += (s, e) => Application.Current.Shutdown();
            newContextMenu.Items.Add(closeApplicationItem);
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            string appTempPath = Path.Combine(Path.GetTempPath(), Assembly.GetExecutingAssembly().GetName().Name!);
            if (Directory.Exists(appTempPath))
            {
                try
                {
                    Directory.Delete(appTempPath, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting the folder with the name: {ex.Message}");
                }
            }
        }
    }
}