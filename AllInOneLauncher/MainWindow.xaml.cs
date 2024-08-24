using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using AllInOneLauncher.Pages.Primary;
using System.Windows.Media.Effects;
using AllInOneLauncher.Logic;
using AllInOneLauncher.Elements;
using AllInOneLauncher.Popups;
using System.Reflection;
using BfmeFoundationProject.WorkshopKit.Logic;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.BfmeRegistryManagement;

namespace AllInOneLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;

            LauncherStateManager.Init();
            LauncherUpdateManager.CheckForUpdates();
            BfmeWorkshopSyncManager.UseExperimentalBaseGameFiles = false;

            TrayIcon.Visibility = Visibility.Collapsed;
            fullContent.Visibility = Visibility.Visible;

            Width = SystemParameters.WorkArea.Width * 0.72;
            Height = SystemParameters.WorkArea.Height * 0.85;

            if (!Properties.Settings.Default.LibraryDrives.Contains(Path.Combine(Path.GetPathRoot(Environment.CurrentDirectory)!, "BfmeLibrary")))
            {
                Properties.Settings.Default.LibraryDrives = [Path.Combine(Path.GetPathRoot(Environment.CurrentDirectory)!, "BfmeLibrary")];
                Properties.Settings.Default.Save();
            }

            CheckSize();
            ReloadContextMenu();
            ShowOffline();

            Application.Current.Exit += OnApplicationExit;
            Loaded += (sender, e) => ProcessCommandLineArgs();

            BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
            BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
        }

        private static void ProcessCommandLineArgs()
        {
            if (App.Args.Length > 0)
            {
                if (App.Args[0] == "--Settings" && App.Args.Length > 1)
                    SetFullContent(new Settings(App.Args[1]));
                else if (App.Args[0] == "--Game" && App.Args.Length > 1)
                    Offline.Instance.gameTabs.InitialSelectedIndex = int.Parse(App.Args[1]);
                else if (App.Args[0] == "--Online")
                    ShowOnline();
                else if (App.Args[0] == "--LauncherChangelog")
                    PopupVisualizer.ShowPopup(new LauncherChangelogPopup());

                if (App.Args.Length > 4 && App.Args[2] == "--InstallGameDialog")
                    Offline.Instance.InstallGame(int.Parse(App.Args[1]), App.Args[3], App.Args[4]);
            }
        }

        private void OnSyncBegin(BfmeWorkshopEntry entry)
        {
            Dispatcher.Invoke(() =>
            {
                settingsIcon.IsHitTestVisible = false;
                settingsIcon.Opacity = 0.4;
            });
        }

        private void OnSyncEnd()
        {
            Dispatcher.Invoke(() =>
            {
                settingsIcon.IsHitTestVisible = true;
                settingsIcon.Opacity = 1;
            });
        }

        public static void SetContent(FrameworkElement? newContent) => Instance!.content.Child = newContent;

        public static async void SetFullContent(FrameworkElement? newContent)
        {
            Instance!.content.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.fullContent.Child = newContent;

            Instance.tabs.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
            Instance.icons.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;

            if (newContent is Settings)
                Instance.background.Effect = new BlurEffect() { Radius = 20 };
            else
                Instance.background.Effect = null;

            if (Settings.NeedsResync)
                for (int game = 0; game < 3; game++)
                {
                    if (!BfmeRegistryManager.IsInstalled(game) || (game == 2 && !BfmeRegistryManager.IsInstalled(1))) continue;
                    var activeEntry = await BfmeWorkshopSyncManager.GetActivePatch(game);
                    if (activeEntry != null) try { await BfmeWorkshopSyncManager.Sync(activeEntry.Value); } catch(Exception ex) { PopupVisualizer.ShowPopup(new ErrorPopup(ex)); }
                }

            Settings.NeedsResync = false;
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
                    tab.Style = (Style)Instance.FindResource("TextBlockHover");
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
                    tab.Style = (Style)Instance.FindResource("TextBlockHover");
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
                    tab.Style = (Style)Instance.FindResource("TextBlockHover");
                }
            }
        }

        public static void ShowPatreons()
        {
            SetContent(Patreons.Instance);

            foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
            {
                if (tab == Instance.patreonsTab)
                    tab.Foreground = new SolidColorBrush(Color.FromRgb(21, 167, 233));
                else
                {
                    tab.Foreground = Brushes.White;
                    tab.Style = (Style)Instance.FindResource("TextBlockHover");
                }
            }
        }

        private void OnOfflineTabClicked(object sender, MouseButtonEventArgs e) => ShowOffline();
        private void OnOnlineTabClicked(object sender, MouseButtonEventArgs e) => ShowOnline();
        private void OnGuidesTabClicked(object sender, MouseButtonEventArgs e) => ShowGuides();
        private void OnPatreonsTabClicked(object sender, MouseButtonEventArgs e) => ShowPatreons();

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