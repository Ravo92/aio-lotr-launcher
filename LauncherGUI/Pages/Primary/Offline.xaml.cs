using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Net.Http;
using System.Diagnostics;
using LauncherGUI.Helpers;
using LauncherGUI.Elements;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static LauncherGUI.Helpers.GameSelectorHelper;
using LauncherGUI.Popups;
using System.Windows.Media;
using System.Linq;
using BfmeWorkshopKit.Logic;
using BfmeWorkshopKit.Data;
using Newtonsoft.Json.Linq;

namespace LauncherGUI.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Offline.xaml
    /// </summary>
    public partial class Offline : UserControl
    {
        public static Offline Instance = new Offline();

        private const string winParameter = "-win";
        private const string winXResParameter = " -xres ";
        private const string winYResParameter = " -yres ";
        readonly Uri changelogBFME2 = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/changelogpages/bfme2/106/changelog.txt");
        readonly Uri changelogROTWK = new("https://gitlab.com/forlongthefat/rotwk-unofficial-202/-/raw/develop/_202Changelog.txt");

        private readonly string tempFileBFME2 = Path.GetTempFileName() + ".html";
        private readonly string tempFileROTWK = Path.GetTempFileName() + ".html";

        int xPosition = 30;
        int yPosition = 30;
        int xResolution = FullscreenWindowedHelper.GetScreenResolutionX() - 200;
        int yResolution = FullscreenWindowedHelper.GetScreenResolutionY() - 200;

        bool installationCanceled = false;

        IntPtr gameHandle = IntPtr.Zero;

        private int previousSelectedIndex = -1;

        public Offline()
        {
            InitializeComponent();
            InitializeWebView();
            Properties.Settings.Default.SettingsSaving += LauncherSettingsChanged;

            BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
            BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
        }

        private void OnSyncBegin(BfmeWorkshopKit.Data.BfmeWorkshopEntry entry)
        {
            SetActiveEntry(entry);

            activeEntryLoading.Visibility = Visibility.Visible;
            activeEntryActive.Visibility = Visibility.Hidden;
            activeEntryReloadButton.Visibility = Visibility.Hidden;
        }

        private void OnSyncEnd()
        {
            activeEntryLoading.Visibility = Visibility.Hidden;
            activeEntryActive.Visibility = Visibility.Visible;
            activeEntryReloadButton.Visibility = Visibility.Visible;
        }

        public void SetActiveEntry(BfmeWorkshopEntry? entry)
        {
            if(entry == null)
            {
                activeEntry.Visibility = Visibility.Hidden;
                activeEntryNullIndicator.Visibility = Visibility.Visible;
                return;
            }

            activeEntry.Visibility = Visibility.Visible;
            activeEntryNullIndicator.Visibility = Visibility.Hidden;

            activeEntryIcon.Source = null;
            try { activeEntryIcon.Source = new BitmapImage(new Uri(entry.Value.ArtworkUrl)); } catch { }
            activeEntryTitle.Text = entry.Value.Name;
            activeEntryVersion.Text = entry.Value.Version;
            activeEntryAuthor.Text = entry.Value.Author;

            if (entry.Value.Type == 0)
                activeEntryType.Text = "Patch";
            else if (entry.Value.Type == 1)
                activeEntryType.Text = "Mod";

            activeEntryLoading.Visibility = Visibility.Hidden;
            activeEntryActive.Visibility = Visibility.Visible;
            activeEntryReloadButton.Visibility = Visibility.Visible;
        }

        private async void ShowLibrary()
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

            SetActiveEntry(BfmeWorkshopSyncManager.GetActivePatch(gameTabs.SelectedIndex));

            libraryTiles.Children.Clear();
            foreach(BfmeWorkshopEntry entry in await BfmeWorkshopLibraryManager.Search(game: gameTabs.SelectedIndex))
                libraryTiles.Children.Add(new LibraryTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
            libraryTiles.Children.Add(emptyLibraryTile);
        }

        private async void ShowWorkshop()
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

            workshopTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in await BfmeWorkshopQueryManager.Search(game: gameTabs.SelectedIndex))
                if(!entry.Guid.StartsWith("original-"))
                    workshopTiles.Children.Add(new WorkshopTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
        }

        private void ShowNews()
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
            LauncherConfigHelper.SetWindowInvisible();

            if (gameTabs.SelectedIndex == 0)
            {
                LaunchSelectedGame(AvailableBFMEGames.BFME1);
            }
            else if (gameTabs.SelectedIndex == 1)
            {
                LaunchSelectedGame(AvailableBFMEGames.BFME2);
            }
            else if (gameTabs.SelectedIndex == 2)
            {
                LaunchSelectedGame(AvailableBFMEGames.ROTWK);
            }

            LauncherConfigHelper.SetWindowVisible();
        }

        private void OnInstallGameClicked(object sender, EventArgs e)
        {
            PopupVisualizer.ShowPopup(new InstallGameDialog(),
            OnPopupSubmited: async (submitedData) =>
            {
                string selectedLanguage = submitedData[0];
                string selectedLibrary = submitedData[1];

                installationCanceled = false;

                launchButton.ButtonState = LaunchButtonState.Loading;

                GameInstallerHelper installerHelper = new GameInstallerHelper();
                installerHelper.ProgressChanged += (sender, progress) => Dispatcher.Invoke(() => launchButton.LoadProgress = progress);
                installerHelper.StatusChanged += (sender, status) => Dispatcher.Invoke(() => launchButton.LoadStatus = status);

                if (!installationCanceled)
                    try
                    {
                        if (gameTabs.SelectedIndex == 0) // BFME1
                        {
                            await installerHelper.InstallGame(AvailableBFMEGames.BFME1);
                        }
                        else if (gameTabs.SelectedIndex == 1) // BFME2
                        {
                            await installerHelper.InstallGame(AvailableBFMEGames.BFME2);
                        }
                        else if (gameTabs.SelectedIndex == 2) // ROTWK
                        {
                            await installerHelper.InstallGame(AvailableBFMEGames.ROTWK);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        launchButton.ButtonState = LaunchButtonState.Launch;
                    }
            });
        }

        private void TabChanged(object sender, EventArgs e)
        {
            if (gameTabs.SelectedIndex != previousSelectedIndex)
            {
                previousSelectedIndex = gameTabs.SelectedIndex;

                //switch (tabs.SelectedIndex)
                //{
                //    case 0: // BFME1
                //        ChangelogPage.Source = new("https://ravo92.github.io/changelogpage");
                //        break;
                //    case 1: // BFME2
                //        ChangelogPage.Source = new Uri(tempFileBFME2);
                //        break;
                //    case 2: // ROTWK
                //        ChangelogPage.Source = new Uri(tempFileROTWK);
                //        break;
                //}

                UpdateTitleImage();
                InitializePlayButton();
                SetActiveEntry(BfmeWorkshopSyncManager.GetActivePatch(gameTabs.SelectedIndex));
                ShowLibrary();
            }
        }

        private void UpdateTitleImage()
        {
            if (gameTabs.SelectedIndex == 0) // BFME1
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_BFME1_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_BFME1_title.png"));
                        break;
                }
            }
            else if (gameTabs.SelectedIndex == 1) // BFME1
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_BFME2_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_BFME2_title.png"));
                        break;
                }
            }
            else if (gameTabs.SelectedIndex == 2) // ROTWK
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_ROTWK_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_ROTWK_title.png"));
                        break;
                }
            }
        }

        private void LauncherSettingsChanged(object sender, EventArgs e)
        {
            UpdateTitleImage();
        }

        private void InitializePlayButton()
        {
            if (gameTabs.SelectedIndex == 0) // BFME1
            {
                if (Properties.Settings.Default.BFME1GameInstalled)
                    launchButton.ButtonState = LaunchButtonState.Launch;
                else
                    launchButton.ButtonState = LaunchButtonState.Install;
            }
            else if (gameTabs.SelectedIndex == 1) // BFME2
            {
                if (Properties.Settings.Default.BFME2GameInstalled)
                    launchButton.ButtonState = LaunchButtonState.Launch;
                else
                    launchButton.ButtonState = LaunchButtonState.Install;
            }
            else if (gameTabs.SelectedIndex == 2) // ROTWK
            {
                if (Properties.Settings.Default.ROTWKGameInstalled)
                    launchButton.ButtonState = LaunchButtonState.Launch;
                else
                    launchButton.ButtonState = LaunchButtonState.Install;
            }
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

        private void LaunchSelectedGame(AvailableBFMEGames availableBFMEGames)
        {
            Process processLaunchGame = new();

            if (ToggleLaunchWindowed.IsToggled)
                processLaunchGame.StartInfo.Arguments = winParameter + winXResParameter + xResolution + winYResParameter + yResolution;
            else
                processLaunchGame.StartInfo.Arguments = winParameter;

            switch (availableBFMEGames)
            {
                case AvailableBFMEGames.BFME1:
                    processLaunchGame.StartInfo.FileName = Path.Combine(BFMERegistryHelper.ReadRegKeyBFME1("path"), ConstStringsHelper.C_BFME1_MAIN_GAME_FILE);
                    processLaunchGame.StartInfo.WorkingDirectory = BFMERegistryHelper.ReadRegKeyBFME1("path");
                    break;
                case AvailableBFMEGames.BFME2:
                    processLaunchGame.StartInfo.FileName = Path.Combine(BFMERegistryHelper.ReadRegKeyBFME2("path"), ConstStringsHelper.C_BFME2_MAIN_GAME_FILE);
                    processLaunchGame.StartInfo.WorkingDirectory = BFMERegistryHelper.ReadRegKeyBFME2("path");
                    break;
                case AvailableBFMEGames.ROTWK:
                    processLaunchGame.StartInfo.FileName = Path.Combine(BFMERegistryHelper.ReadRegKeyROTWK("path"), ConstStringsHelper.C_ROTWK_MAIN_GAME_FILE);
                    processLaunchGame.StartInfo.WorkingDirectory = BFMERegistryHelper.ReadRegKeyROTWK("path");
                    break;
                default:
                    break;
            }

            processLaunchGame.Start();

            switch (availableBFMEGames)
            {
                case AvailableBFMEGames.BFME1:
                    processLaunchGame = FullscreenWindowedHelper.GetProcessByFileName(ConstStringsHelper.C_BFME1_MAIN_GAME_FILE);
                    break;
                case AvailableBFMEGames.BFME2:
                    processLaunchGame = FullscreenWindowedHelper.GetProcessByFileName(ConstStringsHelper.C_BFME2_MAIN_GAME_FILE);
                    break;
                case AvailableBFMEGames.ROTWK:
                    processLaunchGame = FullscreenWindowedHelper.GetProcessByFileName(ConstStringsHelper.C_ROTWK_MAIN_GAME_FILE);
                    break;
                default:
                    break;
            }

            CatchMousePointerHelper.InitializeGlobalHook();
            gameHandle = processLaunchGame.MainWindowHandle;

            if (ToggleLaunchWindowed.IsToggled)
            {
                xPosition = 0;
                yPosition = 0;
                xResolution = FullscreenWindowedHelper.GetScreenResolutionX();
                yResolution = FullscreenWindowedHelper.GetScreenResolutionY();

                CatchMousePointerHelper.SetupBorderlessGameWindowWithMouseClipping(gameHandle, xPosition, yPosition, xResolution, yResolution, true);
            }
            else
            {
                CatchMousePointerHelper.SetupBorderlessGameWindowWithMouseClipping(gameHandle, xPosition, yPosition, xResolution, yResolution, false);
            }

            processLaunchGame.WaitForExit();

            CatchMousePointerHelper.UnclipCursor();
            CatchMousePointerHelper.TerminateGlobalHook();

            processLaunchGame.Dispose();
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

        private void OnLibraryTabClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) => ShowLibrary();

        private void OnWorkshopTabClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) => ShowWorkshop();

        private void OnNewsTabClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) => ShowNews();

        private async void OnResyncActiveEntry(object sender, RoutedEventArgs e)
        {
            BfmeWorkshopEntry? activeEntry = BfmeWorkshopSyncManager.GetActivePatch(gameTabs.SelectedIndex);
            
            if(activeEntry != null)
                await BfmeWorkshopSyncManager.Sync(activeEntry!.Value, (progress) => { }, (downloadItem, downloadProgress) => { });
        }
    }
}