using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            arena.Load("");
        }
    }
}
