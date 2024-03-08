using System;
using System.Windows;
using System.Windows.Controls;

namespace LauncherGUI.Pages.Settings.Launcher
{
    /// <summary>
    /// Interaktionslogik für LauncherSettings_General.xaml
    /// </summary>
    public partial class LauncherSettings_General : UserControl
    {
        public LauncherSettings_General()
        {
            InitializeComponent();
        }

        private void ComboBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeLauncherLanguage(ComboBoxLanguage.SelectedIndex);
        }

        public static void ChangeLauncherLanguage(int languageIndex)
        {
            ResourceDictionary resourceDictionary = [];

            switch (languageIndex)
            {
                case 0:
                    resourceDictionary.Source = new Uri("..\\..\\..\\Resources\\Dictionary\\LanguageResources.en.xaml", UriKind.Relative);
                    break;
                case 1:
                    resourceDictionary.Source = new Uri("..\\..\\..\\Resources\\Dictionary\\LanguageResources.de.xaml", UriKind.Relative);
                    break;
            }

            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
