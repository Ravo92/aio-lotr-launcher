using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using AllInOneLauncher.Elements;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;

namespace AllInOneLauncher.Pages.Subpages.Offline
{
    /// <summary>
    /// Interaction logic for Offline_Library.xaml
    /// </summary>
    public partial class Offline_Library : UserControl
    {
        public Offline_Library()
        {
            InitializeComponent();
            filter.Options = ["{LibraryPageFilterPatchesAndMods}", "{LibraryPageFilterEnhancements}", "{LibraryPageFilterEverything}"];
        }

        private int Game = 0;

        public void Load(int game)
        {
            Game = game;
            search.Text = "";

            UpdateQuery();
        }

        private async void UpdateQuery()
        {
            libraryTiles.Children.Clear();
            List<BfmeWorkshopEntry> entries = await BfmeWorkshopLibraryManager.Search(game: Game, keyword: search.Text, type: filter.Selected == 0 ? -2 : (filter.Selected == 1 ? -3 : -1));
            libraryTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in entries)
                libraryTiles.Children.Add(new LibraryTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
            libraryTiles.Children.Add(emptyLibraryTile);
        }

        private void OnInstallMoreClicked(object sender, MouseButtonEventArgs e) => Primary.Offline.Instance.ShowWorkshop();

        private void OnReloadClicked(object sender, RoutedEventArgs e) => UpdateQuery();

        private void OnFilterChanged(object sender, EventArgs e) => UpdateQuery();

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            searchPlaceholder.Visibility = search.Text == "" ? Visibility.Visible : Visibility.Hidden;
            UpdateQuery();
        }
    }
}
