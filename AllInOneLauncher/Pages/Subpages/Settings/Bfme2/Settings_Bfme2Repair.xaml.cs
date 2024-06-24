﻿using System;
using System.Windows.Controls;
using AllInOneLauncher.Logic;

namespace AllInOneLauncher.Pages.Subpages.Settings.Launcher
{
    /// <summary>
    /// Interaktionslogik für BFME2Settings_Repair.xaml
    /// </summary>
    public partial class Settings_Bfme2Repair : UserControl
    {
        bool isNotUserInteractionForResolutionDropDown = true;
        bool isNotUserInteractionForLanguageDropDown = true;

        public Settings_Bfme2Repair()
        {
            InitializeComponent();
        }

        private void ComboBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // The first call will always be the resolutions being added and set to the user-saved resolution.
            // We skip the first entry point here and then set the "isNotUserInteractionForLanguageDropDown" to false, so the user actually can change the value
            if (isNotUserInteractionForLanguageDropDown)
            {
                isNotUserInteractionForLanguageDropDown = false;
                return;
            }

            Properties.Settings.Default.BFME2LanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void ComboBoxResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // The first call will always be the resolutions being added and set to the user-saved resolution.
            // We skip the first entry point here and then set the "isNotUserInteractionForResolutionDropDown" to false, so the user actually can change the value
            if (isNotUserInteractionForResolutionDropDown)
            {
                isNotUserInteractionForResolutionDropDown = false;
                return;
            }

            Properties.Settings.Default.BFME2ResolutionSetting = ComboBoxResolution.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void BFME2ChildSettingsWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BFME2ChildSettingsWindow_Initialized(object sender, EventArgs e)
        {
            ComboBoxResolution.ItemsSource = SystemDisplayManager.GetAllSupportedResolutions();

            if (Properties.Settings.Default.BFME2ResolutionSetting != null)
                ComboBoxResolution.SelectedItem = Properties.Settings.Default.BFME2ResolutionSetting;
            else
            {
                ComboBoxResolution.SelectedItem = ComboBoxLanguage.Items.Count - 1;
            }

            if (Properties.Settings.Default.BFME2LanguageSetting != 0)
                ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.BFME2LanguageSetting;
            else
            {
                ComboBoxLanguage.SelectedIndex = 0;
            }
        }
    }
}