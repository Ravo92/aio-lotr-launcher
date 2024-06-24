using System.Windows.Controls;

namespace AllInOneLauncher.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Guides.xaml
    /// </summary>
    public partial class Guides : UserControl
    {
        public static Guides Instance = new Guides();

        public Guides()
        {
            InitializeComponent();
            GuidesPage.Source = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/guides/index.html");
        }
    }
}