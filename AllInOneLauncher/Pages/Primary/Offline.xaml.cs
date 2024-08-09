using System;
using System.IO;
using System.Windows;
using AllInOneLauncher.Elements;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AllInOneLauncher.Popups;
using System.Windows.Media;
using System.Linq;
using BfmeWorkshopKit.Logic;
using AllInOneLauncher.Logic;
using BfmeWorkshopKit.Data;
using System.Windows.Input;
using AllInOneLauncher.Data;

namespace AllInOneLauncher.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Offline.xaml
    /// </summary>
    public partial class Offline : UserControl
    {
        internal static readonly Offline Instance = new();
        private int previousSelectedIndex = -1;

        public Offline()
        {
            InitializeComponent();
            Properties.Settings.Default.SettingsSaving += LauncherSettingsChanged;

            BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
            BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
        }

        private void LauncherSettingsChanged(object sender, EventArgs e) => UpdateTitleImage();

        private void OnNewsTabClicked(object sender, MouseButtonEventArgs e) => ShowNews();
        private void OnLibraryTabClicked(object sender, MouseButtonEventArgs e) => ShowLibrary();
        private void OnWorkshopTabClicked(object sender, MouseButtonEventArgs e) => ShowWorkshop();

        private void OnSyncBegin(BfmeWorkshopEntry entry)
        {
            Dispatcher.Invoke(() =>
            {
                if (entry.Game == gameTabs.SelectedIndex)
                {
                    activeEntry.Entry = entry;
                    activeEntry.IsLoading = true;
                }

                gameTabs.IsHitTestVisible = false;
                innerTabs.IsHitTestVisible = false;
                library.IsHitTestVisible = false;
                enabledEnhancements.IsHitTestVisible = false;
            });
        }

        private void OnSyncEnd()
        {
            Dispatcher.Invoke(() =>
            {
                activeEntry.IsLoading = false;
                gameTabs.IsHitTestVisible = true;
                innerTabs.IsHitTestVisible = true;
                library.IsHitTestVisible = true;
                enabledEnhancements.IsHitTestVisible = true;
                UpdateEnabledEnhancements();
            });
        }

        public void ShowNews()
        {
            foreach (Border tab in Instance!.innerTabs.Children.OfType<Border>())
            {
                if (tab == newsTab)
                    tab.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1EFFFFFF"));
                else
                    tab.Background = Brushes.Transparent;
            }

            news.Visibility = Visibility.Visible;
            library.Visibility = Visibility.Hidden;
            workshop.Visibility = Visibility.Hidden;

            news.Load((BfmeGame)gameTabs.SelectedIndex);
        }

        public void ShowLibrary()
        {
            foreach (Border tab in Instance!.innerTabs.Children.OfType<Border>())
            {
                if (tab == libraryTab)
                    tab.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1EFFFFFF"));
                else
                    tab.Background = Brushes.Transparent;
            }

            news.Visibility = Visibility.Hidden;
            library.Visibility = Visibility.Visible;
            workshop.Visibility = Visibility.Hidden;

            activeEntry.Entry = BfmeWorkshopSyncManager.GetActivePatch(gameTabs.SelectedIndex);
            library.Load(gameTabs.SelectedIndex);
        }

        public void ShowWorkshop()
        {
            foreach (Border tab in Instance!.innerTabs.Children.OfType<Border>())
            {
                if (tab == workshopTab)
                    tab.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1EFFFFFF"));
                else
                    tab.Background = Brushes.Transparent;
            }

            news.Visibility = Visibility.Hidden;
            library.Visibility = Visibility.Hidden;
            workshop.Visibility = Visibility.Visible;

            workshop.Load(gameTabs.SelectedIndex);
        }

        private void OnLaunchGameClicked(object sender, EventArgs e)
        {
            LauncherStateManager.Visible = false;
            BfmeLaunchManager.LaunchGame((BfmeGame)gameTabs.SelectedIndex, ToggleLaunchWindowed.IsToggled);
            LauncherStateManager.Visible = true;
        }

        private void OnInstallGameClicked(object sender, EventArgs e)
        {
            LauncherStateManager.AsElevated(() =>
            {
                PopupVisualizer.ShowPopup(new InstallGameDialog(),
                OnPopupSubmited: async (submittedData) =>
                {
                    int game = gameTabs.SelectedIndex;
                    string selectedLanguage = submittedData[0];
                    string selectedLocation = Path.Combine(submittedData[1], game < 2 ? $"BFME{game + 1}" : "RotWK");

                    try
                    {
                        BfmeRegistryManager.CreateBfmeInstallRegistry((BfmeGame)game, selectedLocation, selectedLanguage);
                        await BfmeWorkshopSyncManager.Sync(await BfmeWorkshopEntry.BaseGame(game), (progress) => { }, (downloadItem, downloadProgress) => { });
                    }
                    catch (Exception ex)
                    {
                        PopupVisualizer.ShowPopup(new MessagePopup("ERROR", $"An unexpected error had occurred while installing the game.\n{ex}"));
                    }
                });
            });
        }

        private void TabChanged(object sender, EventArgs e)
        {
            if (gameTabs.SelectedIndex != previousSelectedIndex)
            {
                previousSelectedIndex = gameTabs.SelectedIndex;
                activeEntry.Entry = BfmeWorkshopSyncManager.GetActivePatch(gameTabs.SelectedIndex);

                UpdateTitleImage();
                UpdatePlayButton();
                UpdateEnabledEnhancements();
                ShowNews();
            }
        }

        private void UpdateTitleImage()
        {
            string game;
            if (gameTabs.SelectedIndex == 0)
                game = "BFME1";
            else if (gameTabs.SelectedIndex == 1)
                game = "BFME2";
            else if (gameTabs.SelectedIndex == 2)
                game = "Rotwk";
            else
                return;

            string language;
            if (LauncherStateManager.Language == 0)
                language = "en";
            else if (LauncherStateManager.Language == 1)
                language = "de";
            else
                return;

            titleImage.Source = new BitmapImage(new Uri($"pack://application:,,,/Resources/Images/{language}_{game}_title.png"));
        }

        private void UpdatePlayButton()
        {
            if (BfmeRegistryManager.IsBfmeInstalled((BfmeGame)gameTabs.SelectedIndex))
                launchButton.ButtonState = LaunchButtonState.Launch;
            else
                launchButton.ButtonState = LaunchButtonState.Install;
        }

        private void UpdateEnabledEnhancements()
        {
            enabledEnhancements.Children.Clear();
            foreach (BfmeWorkshopEntry entry in BfmeWorkshopSyncManager.GetActiveEnhancements(gameTabs.SelectedIndex).Values)
                enabledEnhancements.Children.Add(new EnabledEnhancementTile() { Entry = entry, Margin = new Thickness(0, 0, 0, 10) });
        }

        private void CheckBoxWindowed_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsWindowed = true;
            Properties.Settings.Default.Save();
        }

        private void CheckBoxWindowed_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsWindowed = false;
            Properties.Settings.Default.Save();
        }
    }
}