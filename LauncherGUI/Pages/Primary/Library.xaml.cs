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
    /// Interaction logic for Library.xaml
    /// </summary>
    public partial class Library : UserControl
    {
        public Library()
        {
            InitializeComponent();
        }

        private void OnLaunchGameClicked(object sender, EventArgs e)
        {
            launchButton.ButtonState = Elements.LaunchButtonState.Loading;
            launchButton.LoadProgress = 20;
            launchButton.LoadStatus = "Downloading x.zip";
        }

        private void TabChanged(object sender, EventArgs e)
        {
            if(tabs.SelectedIndex == 0) // BFME1
            {
                titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/bfme1_title.png"));
            }
            else if (tabs.SelectedIndex == 1) // BFME2
            {
                titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/bfme2_title.png"));
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {
                titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/bfmeRotWK_title.png"));
            }
        }
    }
}
