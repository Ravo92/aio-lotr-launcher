using AllInOneLauncher.Elements;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace AllInOneLauncher.Popups
{
    /// <summary>
    /// Interaction logic for LauncherChangelogPopup.xaml
    /// </summary>
    public partial class LauncherChangelogPopup : PopupBody
    {
        public LauncherChangelogPopup()
        {
            InitializeComponent();
        }

        private void ButtonCancelClicked(object sender, RoutedEventArgs e)
        {
            ChangelogPage.Visibility = Visibility.Collapsed;

            Dismiss();
        }
    }
}
