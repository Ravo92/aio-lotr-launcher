using AllInOneLauncher.Data;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AllInOneLauncher.Pages.Subpages.Offline
{
    /// <summary>
    /// Interaktionslogik für Offline_News.xaml
    /// </summary>
    public partial class Offline_News : UserControl
    {
        public BfmeGame AvailableBFMEGame { get; set; }

        readonly static Uri changelogBFME2 = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/changelogpages/bfme2/106/changelog.txt");
        readonly static Uri changelogRotwk = new("https://gitlab.com/forlongthefat/rotwk-unofficial-202/-/raw/develop/_202Changelog.txt");

        private readonly string tempFileBFME2 = Path.GetTempFileName() + ".html";
        private readonly string tempFileRotwk = Path.GetTempFileName() + ".html";

        public Offline_News()
        {
            InitializeComponent();
            InitializeWebView();
            Load(BfmeGame.BFME1);
        }

        private async void InitializeWebView()
        {
            string contentBFME2 = await LoadContentFromUriAsync(changelogBFME2);
            string contentRotwk = await LoadContentFromUriAsync(changelogRotwk);

            await WriteTextToFile(tempFileBFME2, contentBFME2, Encoding.UTF8, "transparent", "white");
            await WriteTextToFile(tempFileRotwk, contentRotwk, Encoding.UTF8, "transparent", "white");
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

        private static Uri GetNewsPage(BfmeGame game)
        {
            return game switch
            {
                BfmeGame.BFME1 => new("https://ravo92.github.io/changelogpage/index.html"),
                BfmeGame.BFME2 => changelogBFME2,
                BfmeGame.ROTWK => changelogRotwk,
                _ => throw new ArgumentOutOfRangeException(nameof(game), game, null)
            };
        }

        public void Load(BfmeGame AvailableBFMEGame)
        {
            newsPage.Source = GetNewsPage(AvailableBFMEGame);
        }
    }
}