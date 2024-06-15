using System.Windows.Controls;

namespace LauncherGUI.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Guides.xaml
    /// </summary>
    public partial class Guides : UserControl
    {
        public Guides()
        {
            InitializeComponent();
            GuidesPage.Source = new("https://bfmelauncherfiles.ravonator.at/LauncherPages/guides/index.html");
        }
    }
}