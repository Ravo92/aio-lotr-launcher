using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;
using LauncherGUI.Helpers;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using LauncherGUI.Elements;
using System.Threading.Tasks;

namespace LauncherGUI.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Library.xaml
    /// </summary>
    public partial class Library : UserControl
    {
        string json = "";

        public Library()
        {
            InitializeComponent();
            Properties.Settings.Default.SettingsSaving += LauncherSettingsChanged;
        }

        private void OnLaunchGameClicked(object sender, EventArgs e)
        {
            Process processLaunchGame = new();

            double getSmallerWindowedResolutionX = FullscreenWindowedHelper.GetScreenResolutionX() - 100;
            double getSmallerWindowedResolutionY = FullscreenWindowedHelper.GetScreenResolutionY() - 100;

            if (CheckBoxWindowed.IsChecked == false)
                processLaunchGame.StartInfo.Arguments = "-win -xres " + getSmallerWindowedResolutionX + " -yres " + getSmallerWindowedResolutionY;
            else
                processLaunchGame.StartInfo.Arguments = "-win";

            LauncherConfigHelper.SetWindowInvisible();

            if (tabs.SelectedIndex == 0) // BFME1
            {
                processLaunchGame.StartInfo.FileName = Path.Combine(BFMERegistryHelper.ReadRegKeyBFME1("path"), ConstStringsHelper.C_BFME1_MAIN_GAME_FILE);
                processLaunchGame.StartInfo.WorkingDirectory = BFMERegistryHelper.ReadRegKeyBFME1("path");
                processLaunchGame.Start();

                if (CheckBoxWindowed.IsChecked == false)
                {
                    int xPos = 0;
                    int yPos = 0;
                    int xRes = (int)FullscreenWindowedHelper.GetScreenResolutionX();
                    int yRes = (int)FullscreenWindowedHelper.GetScreenResolutionY();

                    while (Process.GetProcessesByName(ConstStringsHelper.C_MAIN_GAMEDAT_FILE).Length == 0)
                    {
                        Thread.Sleep(2000);
                    }

                    IntPtr handle = Process.GetProcessesByName(ConstStringsHelper.C_MAIN_GAMEDAT_FILE)[0].MainWindowHandle;
                    FullscreenWindowedHelper.GoBorderless(handle, xPos, yPos, xRes, yRes);
                }
            }
            else if (tabs.SelectedIndex == 1) // BFME2
            {
                processLaunchGame.StartInfo.FileName = Path.Combine(BFMERegistryHelper.ReadRegKeyBFME2("path"), ConstStringsHelper.C_BFME2_MAIN_GAME_FILE);
                processLaunchGame.StartInfo.WorkingDirectory = BFMERegistryHelper.ReadRegKeyBFME2("path");
                processLaunchGame.Start();

                if (CheckBoxWindowed.IsChecked == false)
                {
                    int xPos = 0;
                    int yPos = 0;
                    int xRes = (int)FullscreenWindowedHelper.GetScreenResolutionX();
                    int yRes = (int)FullscreenWindowedHelper.GetScreenResolutionY();

                    while (Process.GetProcessesByName(ConstStringsHelper.C_MAIN_GAMEDAT_FILE).Length == 0)
                    {
                        Thread.Sleep(2000);
                    }

                    IntPtr handle = Process.GetProcessesByName(ConstStringsHelper.C_MAIN_GAMEDAT_FILE)[0].MainWindowHandle;
                    FullscreenWindowedHelper.GoBorderless(handle, xPos, yPos, xRes, yRes);
                }
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {
                processLaunchGame.StartInfo.FileName = Path.Combine(BFMERegistryHelper.ReadRegKeyROTWK("path"), ConstStringsHelper.C_ROTWK_MAIN_GAME_FILE);
                processLaunchGame.StartInfo.WorkingDirectory = BFMERegistryHelper.ReadRegKeyROTWK("path");
                processLaunchGame.Start();

                if (CheckBoxWindowed.IsChecked == false)
                {
                    int xPos = 0;
                    int yPos = 0;
                    int xRes = (int)FullscreenWindowedHelper.GetScreenResolutionX();
                    int yRes = (int)FullscreenWindowedHelper.GetScreenResolutionY();

                    Process process = FullscreenWindowedHelper.GetProcessByFileName(ConstStringsHelper.C_ROTWK_MAIN_GAME_FILE);

                    IntPtr handle = process.MainWindowHandle;
                    FullscreenWindowedHelper.GoBorderless(handle, xPos, yPos, xRes, yRes);
                }
            }

            processLaunchGame.WaitForExit();
            processLaunchGame.Dispose();

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
            if (tabs.SelectedIndex == 0) // BFME1
            {
                ChangelogPage.Source = new Uri("https://ravo92.github.io/changelogpage/index.html");

                if (ChangelogPage.Visibility == Visibility.Hidden)
                    ChangelogPage.Visibility = Visibility.Visible;
            }
            else if (tabs.SelectedIndex == 1) // BFME2
            {
                ChangelogPage.Source = new Uri("https://ravo92.github.io/changelogpages/bfme2/106/changelog.txt");

                if (ChangelogPage.Visibility == Visibility.Hidden)
                    ChangelogPage.Visibility = Visibility.Visible;
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {
                ChangelogPage.Source = new Uri("https://gitlab.com/forlongthefat/rotwk-unofficial-202/-/raw/develop/_202Changelog.txt");

                if (ChangelogPage.Visibility == Visibility.Hidden)
                    ChangelogPage.Visibility = Visibility.Visible;
            }

            UpdateTitleImage();
            InitializePlayButton();
        }

        private void ChangelogPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                ChangelogPage.CoreWebView2.OpenDevToolsWindow();
                e.Handled = true;
            }
        }

        private void UpdateTitleImage()
        {
            if (tabs.SelectedIndex == 0) // BFME1
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_bfme1_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_bfme1_title.png"));
                        break;
                }
            }
            else if (tabs.SelectedIndex == 1) // BFME1
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_bfme2_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_bfme2_title.png"));
                        break;
                }
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_rotwk_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_rotwk_title.png"));
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
                    launchButton.ButtonState = Elements.LaunchButtonState.Launch;
                else
                    launchButton.ButtonState = Elements.LaunchButtonState.Install;
            }
            else if (tabs.SelectedIndex == 1) // BFME2
            {
                if (Properties.Settings.Default.BFME2GameInstalled)
                    launchButton.ButtonState = Elements.LaunchButtonState.Launch;
                else
                    launchButton.ButtonState = Elements.LaunchButtonState.Install;
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {
                if (Properties.Settings.Default.ROTWKGameInstalled)
                    launchButton.ButtonState = Elements.LaunchButtonState.Launch;
                else
                    launchButton.ButtonState = Elements.LaunchButtonState.Install;
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
    }
}