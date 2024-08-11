using System.Windows.Controls;

namespace AllInOneLauncher.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Patreons.xaml
    /// </summary>
    public partial class Patreons : UserControl
    {
        internal static Patreons Instance = new();

        public Patreons()
        {
            InitializeComponent();
            PatreonsPage.Source = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/patreons/index.html");
        }
    }
}