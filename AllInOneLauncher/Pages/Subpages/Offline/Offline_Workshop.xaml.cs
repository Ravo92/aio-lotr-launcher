using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using AllInOneLauncher.Elements;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace AllInOneLauncher.Pages.Subpages.Offline
{
    /// <summary>
    /// Interaction logic for Offline_Workshop.xaml
    /// </summary>
    public partial class Offline_Workshop : UserControl
    {
        public Offline_Workshop()
        {
            InitializeComponent();
        }

        private int Game = 0;

        public async void Load(int game)
        {
            Game = game;
            search.Text = "";

            List<BfmeWorkshopEntry> entries = await BfmeWorkshopQueryManager.Search(game: game);
            workshopTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in entries)
                if (!entry.Guid.StartsWith("original-"))
                    workshopTiles.Children.Add(new WorkshopTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
        }

        private async void Search(string keyword)
        {
            List<BfmeWorkshopEntry> entries = await BfmeWorkshopQueryManager.Search(game: Game, keyword: keyword);
            workshopTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in entries)
                if (!entry.Guid.StartsWith("original-"))
                    workshopTiles.Children.Add(new WorkshopTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
        }

        private void OnReloadClicked(object sender, RoutedEventArgs e) => Search(search.Text);

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            searchPlaceholder.Visibility = search.Text == "" ? Visibility.Visible : Visibility.Hidden;
            Search(search.Text);
        }
    }
}
