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
        }

        private void SetChangelogVisibility(bool isVisible)
        {
            if (isVisible)
            {
                ChangelogPage.Visibility = Visibility.Visible;
                ChangelogPageImage.Visibility = Visibility.Hidden;
            }
            else
            {
                Dispatcher.Invoke(async () =>
                {
                    // Cursed workaround for WebView2 airspace issue
                    MemoryStream ms = new MemoryStream();
                    await ChangelogPage.CoreWebView2.CapturePreviewAsync(Microsoft.Web.WebView2.Core.CoreWebView2CapturePreviewImageFormat.Png, ms);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    ChangelogPageImage.Source = bi;

                    ChangelogPageImage.Visibility = Visibility.Visible;
                    ChangelogPage.Visibility = Visibility.Hidden;
                });
            }
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

        private void OnInstallGameClicked(object sender, EventArgs e)
        {
            SetChangelogVisibility(false);

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
                        if (tabs.SelectedIndex == 0) // BFME1
                        {
                            await installerHelper.InstallGame(AvailableBFMEGames.BFME1);
                        }
                        else if (tabs.SelectedIndex == 1) // BFME2
                        {
                            await installerHelper.InstallGame(AvailableBFMEGames.BFME2);
                        }
                        else if (tabs.SelectedIndex == 2) // ROTWK
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
            },
            OnPopupClosed: () =>
            {
                SetChangelogVisibility(true);
            });
        }

        private void TabChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedIndex != previousSelectedIndex)
            {
                previousSelectedIndex = tabs.SelectedIndex;

                switch (tabs.SelectedIndex)
                {
                    case 0: // BFME1
                        ChangelogPage.Source = new("https://ravo92.github.io/changelogpage");
                        break;
                    case 1: // BFME2
                        ChangelogPage.Source = new Uri(tempFileBFME2);
                        break;
                    case 2: // ROTWK
                        ChangelogPage.Source = new Uri(tempFileROTWK);
                        break;
                }

                UpdateTitleImage();
                InitializePlayButton();
            }
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

            if (CheckBoxWindowed.IsChecked == true)
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

            if (CheckBoxWindowed.IsChecked == false)
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
    }
}