using System.Windows;
using System.Windows.Controls;

namespace LauncherGUI.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Online.xaml
    /// </summary>
    public partial class Online : UserControl
    {
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
            arena.Load();
        }
    }
}
