using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LauncherGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void ButtonGameBFME1_Click(object sender, RoutedEventArgs e)
        {
            Style? styleButtonBFME1 = FindResource("UnderlinedButton") as Style;
            GameBFME1.Style = styleButtonBFME1;

            Style? styleButtonBFME2 = FindResource("ButtonHovered") as Style;
            GameBFME2.Style = styleButtonBFME2;

            Style? styleButtonBFME2EP1 = FindResource("ButtonHovered") as Style;
            GameBFME2EP1.Style = styleButtonBFME2EP1;
        }

        private void ButtonGameBFME2_Click(object sender, RoutedEventArgs e)
        {
            Style? styleButtonBFME1 = FindResource("ButtonHovered") as Style;
            GameBFME1.Style = styleButtonBFME1;

            Style? styleButtonBFME2 = FindResource("UnderlinedButton") as Style;
            GameBFME2.Style = styleButtonBFME2;

            Style? styleButtonBFME2EP1 = FindResource("ButtonHovered") as Style;
            GameBFME2EP1.Style = styleButtonBFME2EP1;
        }

        private void ButtonGameBFME2EP1_Click(object sender, RoutedEventArgs e)
        {
            Style? styleButtonBFME1 = FindResource("ButtonHovered") as Style;
            GameBFME1.Style = styleButtonBFME1;

            Style? styleButtonBFME2 = FindResource("ButtonHovered") as Style;
            GameBFME2.Style = styleButtonBFME2;

            Style? styleButtonBFME2EP1 = FindResource("UnderlinedButton") as Style;
            GameBFME2EP1.Style = styleButtonBFME2EP1;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Style? styleButtonBFME1 = FindResource("UnderlinedButton") as Style;
            GameBFME1.Style = styleButtonBFME1;
        }
    }
}