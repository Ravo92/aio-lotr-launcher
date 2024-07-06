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
        }

        private int Game = 0;

        public async void Load(int game)
        {
            Game = game;
            search.Text = "";

            libraryTiles.Children.Clear();
            List<BfmeWorkshopEntry> entries = await BfmeWorkshopLibraryManager.Search(game: game);
            libraryTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in entries)
                libraryTiles.Children.Add(new LibraryTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
            libraryTiles.Children.Add(emptyLibraryTile);
        }

        private async void Search(string keyword)
        {
            libraryTiles.Children.Clear();
            List<BfmeWorkshopEntry> entries = await BfmeWorkshopLibraryManager.Search(game: Game, keyword: keyword);
            libraryTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in entries)
                libraryTiles.Children.Add(new LibraryTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
            libraryTiles.Children.Add(emptyLibraryTile);
        }

        private void OnInstallMoreClicked(object sender, MouseButtonEventArgs e) => Primary.Offline.Instance.ShowWorkshop();

        private void OnReloadClicked(object sender, RoutedEventArgs e) => Search(search.Text);

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            searchPlaceholder.Visibility = search.Text == "" ? Visibility.Visible : Visibility.Hidden;
            Search(search.Text);
        }
    }
}
