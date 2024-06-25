using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Net.Http;
using AllInOneLauncher.Elements;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AllInOneLauncher.Popups;
using System.Windows.Media;
using System.Linq;
using BfmeWorkshopKit.Logic;
using AllInOneLauncher.Logic;
using BfmeWorkshopKit.Data;
using Windows.Media.Capture;

namespace AllInOneLauncher.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Offline.xaml
    /// </summary>
    public partial class Offline : UserControl
    {
        public static Offline Instance = new Offline();

        readonly Uri changelogBFME2 = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/changelogpages/bfme2/106/changelog.txt");
        readonly Uri changelogROTWK = new("https://gitlab.com/forlongthefat/rotwk-unofficial-202/-/raw/develop/_202Changelog.txt");

        private readonly string tempFileBFME2 = Path.GetTempFileName() + ".html";
        private readonly string tempFileROTWK = Path.GetTempFileName() + ".html";

        private int previousSelectedIndex = -1;

        public Offline()
        {
            InitializeComponent();
            InitializeWebView();
            Properties.Settings.Default.SettingsSaving += LauncherSettingsChanged;

            BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
            BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
        }

        private void LauncherSettingsChanged(object sender, EventArgs e) => UpdateTitleImage();
        private void OnLibraryTabClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) => ShowLibrary();
        private void OnWorkshopTabClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) => ShowWorkshop();
        private void OnNewsTabClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) => ShowNews();

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
            });
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

            library.Visibility = Visibility.Hidden;
            workshop.Visibility = Visibility.Visible;

            workshop.Load(gameTabs.SelectedIndex);
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
        }

        private void OnLaunchGameClicked(object sender, EventArgs e)
        {
            LauncherStateManager.Visible = false;
            BfmeLaunchManager.LaunchGame(gameTabs.SelectedIndex, ToggleLaunchWindowed.IsToggled);
            LauncherStateManager.Visible = true;
        }

        private void OnInstallGameClicked(object sender, EventArgs e)
        {
            LauncherStateManager.AsElevated(() =>
            {
                PopupVisualizer.ShowPopup(new InstallGameDialog(),
                OnPopupSubmited: async (submitedData) =>
                {
                    int game = gameTabs.SelectedIndex;
                    string selectedLanguage = submitedData[0];
                    string selectedLocation = Path.Combine(submitedData[1], game < 2 ? $"BFME{game + 1}" : "RotWK");

                    try
                    {
                        BfmeRegistryManager.CreateBfmeInstallRegistry(game, selectedLocation, selectedLanguage);
                        await BfmeWorkshopSyncManager.Sync(await BfmeWorkshopEntry.BaseGame(game), (progress) => { }, (downloadItem, downloadProgress) => { });
                    }
                    catch(Exception ex)
                    {
                        PopupVisualizer.ShowPopup(new MessagePopup("ERROR", $"An unexpected error had occured while installing the game.\n{ex.ToString()}"));
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
                ShowLibrary();
            }
        }

        private void UpdateTitleImage()
        {
            string game = "";
            if (gameTabs.SelectedIndex == 0)
                game = "BFME1";
            else if (gameTabs.SelectedIndex == 1)
                game = "BFME2";
            else if (gameTabs.SelectedIndex == 2)
                game = "ROTWK";
            else
                return;

            string language = "";
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
            if (BfmeRegistryManager.IsBfmeInstalled(gameTabs.SelectedIndex))
                launchButton.ButtonState = LaunchButtonState.Launch;
            else
                launchButton.ButtonState = LaunchButtonState.Install;
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

        private async void InitializeWebView()
        {
            string contentBFME2 = await LoadContentFromUriAsync(changelogBFME2);
            string contentROTWK = await LoadContentFromUriAsync(changelogROTWK);

            await WriteTextToFile(tempFileBFME2, contentBFME2, Encoding.UTF8, "transparent", "white");
            await WriteTextToFile(tempFileROTWK, contentROTWK, Encoding.UTF8, "transparent", "white");
        }

        private static async Task WriteTextToFile(string filePath, string content, Encoding encoding, string backgroundColor, string foregroundColor)
        {
            string htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                <style>
                body {{
                    background-color: {backgroundColor};
                    color: {foregroundColor};
                }}
                </style>
                </head>
                <body>
                <pre>{content}</pre>
                </body>
                </html>";

            await File.WriteAllTextAsync(filePath, htmlContent, encoding);
        }

        private static async Task<string> LoadContentFromUriAsync(Uri uri)
        {
            using HttpClient client = new();
            return await client.GetStringAsync(uri);
        }
    }
}