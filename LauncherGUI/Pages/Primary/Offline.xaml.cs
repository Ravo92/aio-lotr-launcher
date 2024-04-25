using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            Properties.Settings.Default.SettingsSaving += LauncherSettingsChanged;
        }

        private void OnLaunchGameClicked(object sender, EventArgs e)
        {
            launchButton.ButtonState = Elements.LaunchButtonState.Loading;
            launchButton.LoadProgress = 20;
            launchButton.LoadStatus = "Downloading x.zip";
        }

        private void TabChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedIndex == 0) // BFME1
            {
                ChangelogPage.Source = new Uri("https://ravo92.github.io/changelogpage/index.html");

                if (ChangelogPage.Visibility == Visibility.Hidden)
                    ChangelogPage.Visibility = Visibility.Visible;
            }
            else if (tabs.SelectedIndex == 1) // BFME2
            {
                if (ChangelogPage.Visibility == Visibility.Visible)
                    ChangelogPage.Visibility = Visibility.Hidden;
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {
                ChangelogPage.Source = new Uri("https://gitlab.com/forlongthefat/rotwk-unofficial-202/-/raw/develop/_202Changelog.txt");

                if (ChangelogPage.Visibility == Visibility.Hidden)
                    ChangelogPage.Visibility = Visibility.Visible;
            }

            UpdateTitleImage();
        }

        private void ChangelogPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                ChangelogPage.CoreWebView2.OpenDevToolsWindow();
                e.Handled = true;
            }
        }

        private void UpdateTitleImage()
        {
            if (tabs.SelectedIndex == 0) // BFME1
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_bfme1_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_bfme1_title.png"));
                        break;
                }
            }
            else if (tabs.SelectedIndex == 1) // BFME1
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_bfme2_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_bfme2_title.png"));
                        break;
                }
            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {
                switch (Properties.Settings.Default.LauncherLanguageSetting)
                {
                    case 0:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/en_rotwk_title.png"));
                        break;
                    case 1:
                        titleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/de_rotwk_title.png"));
                        break;
                }
            }
        }

        private void LauncherSettingsChanged(object sender, EventArgs e)
        {
            UpdateTitleImage();
        }
    }
}