using AllInOneLauncher.Data;
using AllInOneLauncher.Logic;
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

        readonly static Uri changelogBFME1 = new("https://ravo92.github.io/changelogpage/index.html");
        readonly static Uri changelogBFME2 = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/changelogpages/bfme2/106/changelog.txt");
        readonly static Uri changelogRotwk = new("https://gitlab.com/forlongthefat/rotwk-unofficial-202/-/raw/develop/_202Changelog.txt");

        public Offline_News()
        {
            InitializeComponent();
            Load(BfmeGame.BFME1);
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
            if (!LauncherStateManager.Offline)
                newsPage.Source = GetNewsPage(AvailableBFMEGame);

            newsPage.Visibility = LauncherStateManager.Offline ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            noConnection.Visibility = LauncherStateManager.Offline ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }
    }
}