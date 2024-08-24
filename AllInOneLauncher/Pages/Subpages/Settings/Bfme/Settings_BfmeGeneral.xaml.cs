﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AllInOneLauncher.Logic;
using AllInOneLauncher.Data;
using BfmeFoundationProject.BfmeRegistryManagement;
using BfmeFoundationProject.BfmeRegistryManagement.Data;

namespace AllInOneLauncher.Pages.Subpages.Settings.Launcher
{
    public partial class Settings_BfmeGeneral : UserControl
    {
        public BfmeGame Game = BfmeGame.NONE;

        public Settings_BfmeGeneral(BfmeGame game)
        {
            Game = game;
            InitializeComponent();
            InitializePageElements();
        }

        private void InitializePageElements()
        {
            ResolutionDropdown.Options = SystemDisplayManager.GetAllSupportedResolutions();

            ResolutionDropdown.SelectedValue = BfmeSettingsManager.Get(Game, "Resolution") ?? ResolutionDropdown.Options.First();
            LanguageDropdown.SelectedValue = BfmeRegistryManager.GetKeyValue((int)Game, BfmeRegistryKey.Language);
            title.Text = Game switch
            {
                BfmeGame.BFME1 => Application.Current.FindResource("SettingsPageBFME1SectionHeader").ToString(),
                BfmeGame.BFME2 => Application.Current.FindResource("SettingsPageBFME2SectionHeader").ToString(),
                BfmeGame.ROTWK => Application.Current.FindResource("SettingsPageRotWKSectionHeader").ToString(),
                _ => ""
            };

            string cdKey = BfmeRegistryManager.GetKeyValue((int)Game, BfmeRegistryKey.SerialKey);
            curentSerialNumber.Text = $"Current serial number: {string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)))}";
        }

        private void OnLanguageOptionSelected(object sender, System.EventArgs e)
        {
            BfmeRegistryManager.SetKeyValue((int)Game, BfmeRegistryKey.Language, LanguageDropdown.SelectedValue);
            Primary.Settings.NeedsResync = true;
            Properties.Settings.Default.Save();
        }

        private void OnGameResolutionOptionSelected(object sender, System.EventArgs e)
        {
            BfmeSettingsManager.Set(Game, "Resolution", ResolutionDropdown.SelectedValue);
            Properties.Settings.Default.Save();
        }

        private void ButtonChangeCdKey_Click(object sender, RoutedEventArgs e)
        {
            string cdKey = string.Concat(from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 20) select s[System.Random.Shared.Next(s.Length)]);
            curentSerialNumber.Text = $"Current serial number: {string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)))}";
            BfmeRegistryManager.SetKeyValue((int)Game, BfmeRegistryKey.SerialKey, cdKey, Microsoft.Win32.RegistryValueKind.String);
        }
    }
}