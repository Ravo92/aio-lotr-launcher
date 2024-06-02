using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Diagnostics;
using LauncherGUI.Helpers;
using LauncherGUI.Elements;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using static LauncherGUI.Helpers.GameSelectorHelper;
using Windows.Web.Http;

namespace LauncherGUI.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Offline.xaml
    /// </summary>
    public partial class Offline : UserControl
    {
        string json = "";
        readonly Uri changelogBFME1 = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/changelogpages/bfme1/index.html");
        readonly Uri changelogBFME2 = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/changelogpages/bfme2/106/changelog.txt");
        readonly Uri changelogROTWK = new("https://gitlab.com/forlongthefat/rotwk-unofficial-202/-/raw/develop/_202Changelog.txt");

        private static readonly HttpClient httpClient = new();

        public Offline()
        {
            InitializeComponent();
            InitializeWebView();
            Properties.Settings.Default.SettingsSaving += LauncherSettingsChanged;
        }

        private void OnLaunchGameClicked(object sender, EventArgs e)
        {
            LauncherConfigHelper.SetWindowInvisible();

            if (tabs.SelectedIndex == 0)
            {
                LaunchSelectedGame(AvailableBFMEGames.BFME1);
            }
            else if (tabs.SelectedIndex == 1)
            {
                LaunchSelectedGame(AvailableBFMEGames.BFME2);
            }
            else if (tabs.SelectedIndex == 2)
            {
                LaunchSelectedGame(AvailableBFMEGames.ROTWK);
            }

            LauncherConfigHelper.SetWindowVisible();
        }

        private async void OnInstallGameClicked(object sender, EventArgs e)
        {
            if (tabs.SelectedIndex == 0) // BFME1
            {
                launchButton.ButtonState = LaunchButtonState.Loading;
                launchButton.LoadProgress = 20;
                launchButton.LoadStatus = "Downloading x.zip";
            }
            else if (tabs.SelectedIndex == 1) // BFME2
            {
                launchButton.ButtonState = LaunchButtonState.Loading;

                List<GameFileDictionary> gameFiles = JsonConvert.DeserializeObject<List<GameFileDictionary>>(await GameFileToolsHelper.DownloadJSONFile("https://bfmelauncherfiles.ravonator.at/LauncherJson/BFME2BaseGameFiles.json")) ?? [];
                GameFileToolsHelper gameFileToolsHelper = new();
                int totalCount = gameFiles.Count;
                int currentCount = 0;

                foreach (GameFileDictionary gameFile in gameFiles)
                {
                    launchButton.LoadProgress = Math.Round((double)currentCount / totalCount * 100, 0);
                    launchButton.LoadStatus = Path.GetFileName(gameFile.FileName);

                    await gameFileToolsHelper.DownloadFile(BFMERegistryHelper.ReadRegKeyBFME2("path"), gameFile.FileName, gameFile.FileURL);

                    currentCount++;
                }
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {
                launchButton.ButtonState = LaunchButtonState.Loading;
            }
        }


        private void TabChanged(object sender, EventArgs e)
        {
            switch (tabs.SelectedIndex)
            {
                case 0: // BFME1
                    ChangelogPage.Source = changelogBFME1;
                    break;
                case 1: // BFME2
                    ChangelogPage.Source = changelogBFME2;
                    break;
                case 2: // ROTWK
                    ChangelogPage.Source = changelogROTWK;
                    break;
            }

            UpdateTitleImage();
            InitializePlayButton();
        }

        private void UpdateTitleImage()
        {
            if (tabs.SelectedIndex == 0) // BFME1
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
            else if (tabs.SelectedIndex == 1) // BFME1
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
            else if (tabs.SelectedIndex == 2) // ROTWK
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
            if (tabs.SelectedIndex == 0) // BFME1
            {
                if (Properties.Settings.Default.BFME1GameInstalled)
                    launchButton.ButtonState = LaunchButtonState.Launch;
                else
                    launchButton.ButtonState = LaunchButtonState.Install;
            }
            else if (tabs.SelectedIndex == 1) // BFME2
            {
                if (Properties.Settings.Default.BFME2GameInstalled)
                    launchButton.ButtonState = LaunchButtonState.Launch;
                else
                    launchButton.ButtonState = LaunchButtonState.Install;
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
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

            double getSmallerWindowedResolutionX = FullscreenWindowedHelper.GetScreenResolutionX() - 100;
            double getSmallerWindowedResolutionY = FullscreenWindowedHelper.GetScreenResolutionY() - 100;

            if (CheckBoxWindowed.IsChecked == true)
                processLaunchGame.StartInfo.Arguments = "-win -xres " + getSmallerWindowedResolutionX + " -yres " + getSmallerWindowedResolutionY;
            else
                processLaunchGame.StartInfo.Arguments = "-win";

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

            if (CheckBoxWindowed.IsChecked == false)
            {
                int xPos = 0;
                int yPos = 0;
                int xRes = (int)FullscreenWindowedHelper.GetScreenResolutionX();
                int yRes = (int)FullscreenWindowedHelper.GetScreenResolutionY();

                Process process = new();

                switch (availableBFMEGames)
                {
                    case AvailableBFMEGames.BFME1:
                        process = FullscreenWindowedHelper.GetProcessByFileName(ConstStringsHelper.C_BFME1_MAIN_GAME_FILE);
                        break;
                    case AvailableBFMEGames.BFME2:
                        process = FullscreenWindowedHelper.GetProcessByFileName(ConstStringsHelper.C_BFME2_MAIN_GAME_FILE);
                        break;
                    case AvailableBFMEGames.ROTWK:
                        process = FullscreenWindowedHelper.GetProcessByFileName(ConstStringsHelper.C_ROTWK_MAIN_GAME_FILE);
                        break;
                    default:
                        break;
                }

                IntPtr handle = process.MainWindowHandle;
                FullscreenWindowedHelper.GoBorderless(handle, xPos, yPos, xRes, yRes);
            }

            processLaunchGame.WaitForExit();
            processLaunchGame.Dispose();
        }

        private void ChangelogPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                ChangelogPage.CoreWebView2.OpenDevToolsWindow();
                e.Handled = true;
            }
        }

        private async void InitializeWebView()
        {
            await ChangelogPage.EnsureCoreWebView2Async();

            ChangelogPage.CoreWebView2.NavigationStarting += ChangelogPage_NavigationStarting;
            ChangelogPage.CoreWebView2.NavigationCompleted += ChangelogPage_NavigationCompleted;
        }

        private void ChangelogPage_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            LoadingText.Visibility = Visibility.Visible;
        }

        private void ChangelogPage_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            LoadingText.Visibility = Visibility.Collapsed;
        }
    }
}