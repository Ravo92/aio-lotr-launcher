using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using AllInOneLauncher.Elements;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using AllInOneLauncher.Popups;
using System.ComponentModel;

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
            filter.Options = ["{LibraryPageFilterPatchesAndMods}", "{LibraryPageFilterEnhancements}", "{LibraryPageFilterSnapshots}", "{LibraryPageFilterEverything}"];
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
            List<BfmeWorkshopEntry> entries = await BfmeWorkshopLibraryManager.Search(game: Game, keyword: search.Text, type: new []{ -2, -3, 4, -1 }[filter.Selected]);
            libraryTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in entries)
                libraryTiles.Children.Add(new LibraryTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
            if (filter.Selected != 2) libraryTiles.Children.Add(emptyLibraryTile);
        }

        private async void OnCreateSnapshotClicked(object sender, RoutedEventArgs e)
        {
            Primary.Offline.Instance.Disabled = true;
            snapshotSpinner.IsLoading = true;
            snapshotIcon.Visibility = Visibility.Hidden;

            try
            {
                var entry = await BfmeWorkshopSyncManager.CreateSnapshot(Game);

                BfmeWorkshopLibraryManager.AddToLibrary(entry);
                if (filter.Selected == 2) UpdateQuery();
            }
            catch(Exception ex)
            {
                PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            }
            finally
            {
                snapshotSpinner.IsLoading = false;
                snapshotIcon.Visibility = Visibility.Visible;
                Primary.Offline.Instance.Disabled = false;
            }
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
