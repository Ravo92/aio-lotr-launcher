using AllInOneLauncher.Data;
using AllInOneLauncher.Elements;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AllInOneLauncher.Pages.Subpages.Offline
{
    /// <summary>
    /// Interaktionslogik für Offline_News.xaml
    /// </summary>
    public partial class Offline_News : UserControl
    {
        public BfmeGame AvailableBFMEGame { get; set; }

        readonly static Uri changelogBFME1 = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/changelogpages/bfme1/index.html");
        readonly static Uri changelogBFME2 = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/changelogpages/bfme2/106/changelog.html");
        readonly static Uri changelogRotwk = new("https://gitlab.com/forlongthefat/rotwk-unofficial-202/-/raw/develop/_202Changelog.txt");

        public Offline_News()
        {
            InitializeComponent();
            Load(BfmeGame.BFME1);
            PopupVisualizer.OnPopupOpened += (s, e) => SetNewsVisibility(false);
            PopupVisualizer.OnPopupClosed += (s, e) => SetNewsVisibility(true);
        }

        private void SetNewsVisibility(bool isVisible)
        {
            if (isVisible && newsPage.Height == 0)
            {
                newsPageImage.Visibility = System.Windows.Visibility.Hidden;
                newsPage.Height = double.NaN;
            }
            else if (!isVisible && newsPage.Height != 0)
            {
                newsPageImage.Visibility = System.Windows.Visibility.Visible;
                newsPage.Height = 0;
            }
        }

        private static Uri GetNewsPage(BfmeGame game)
        {
            return game switch
            {
                BfmeGame.BFME1 => changelogBFME1,
                BfmeGame.BFME2 => changelogBFME2,
                BfmeGame.ROTWK => changelogRotwk,
                _ => throw new ArgumentOutOfRangeException(nameof(game), game, null)
            };
        }

        public void Load(BfmeGame AvailableBFMEGame)
        {
            newsPage.Visibility = System.Windows.Visibility.Visible;
            noConnection.Visibility = System.Windows.Visibility.Hidden;

            newsPage.Source = GetNewsPage(AvailableBFMEGame);
        }

        private void OnNavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
            {
                newsPage.Visibility = System.Windows.Visibility.Hidden;
                noConnection.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Dispatcher.Invoke(async () =>
                {
                    // Cursed workaround for WebView2 airspace issue
                    MemoryStream ms = new MemoryStream();
                    await newsPage.CoreWebView2.CapturePreviewAsync(Microsoft.Web.WebView2.Core.CoreWebView2CapturePreviewImageFormat.Png, ms);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    newsPageImage.Source = bi;
                });
            }
        }
    }
}