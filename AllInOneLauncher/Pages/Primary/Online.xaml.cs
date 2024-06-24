using AllInOneLauncher.Logic;
using System.Windows;
using System.Windows.Controls;

namespace AllInOneLauncher.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Online.xaml
    /// </summary>
    public partial class Online : UserControl
    {
        public static Online Instance = new Online();

        private bool FirstLoad = true;

        public Online()
        {
            InitializeComponent();
        }

        public void Unload() => arena.Unload();

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!FirstLoad)
                return;

            FirstLoad = false;
            LauncherStateManager.AsElevated(() => arena.Load());
        }
    }
}
