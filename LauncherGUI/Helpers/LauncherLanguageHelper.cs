using System.Windows;
using System;

namespace LauncherGUI.Helpers
{
    internal class LauncherLanguageHelper
    {
        public static void GetAvailableLauncherLanguage(int languageIndex)
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
