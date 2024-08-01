using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using AllInOneLauncher.Elements;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using AllInOneLauncher.Logic;
using System.Data.Common;

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

        public void Load(int game)
        {
            Game = game;
            search.Text = "";

            Query("");
        }

        private async void Query(string keyword)
        {
            try
            {
                workshopContent.Visibility = Visibility.Visible;
                noConnection.Visibility = Visibility.Hidden;

                workshopTiles.Children.Clear();
                List<BfmeWorkshopEntry> entries = await BfmeWorkshopQueryManager.Query(game: Game, keyword: keyword);
                workshopTiles.Children.Clear();
                foreach (BfmeWorkshopEntry entry in entries)
                    workshopTiles.Children.Add(new WorkshopTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
            }
            catch
            {
                workshopContent.Visibility = Visibility.Hidden;
                noConnection.Visibility = Visibility.Visible;
            }
        }

        private void OnReloadClicked(object sender, RoutedEventArgs e) => Query(search.Text);

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            searchPlaceholder.Visibility = search.Text == "" ? Visibility.Visible : Visibility.Hidden;
            Query(search.Text);
        }
    }
}
