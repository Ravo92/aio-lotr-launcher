using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using AllInOneLauncher.Elements;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public async void Load(int game)
        {
            libraryTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in await BfmeWorkshopLibraryManager.Search(game: game))
                libraryTiles.Children.Add(new LibraryTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
            libraryTiles.Children.Add(emptyLibraryTile);
        }

        private void OnInstallMoreClicked(object sender, MouseButtonEventArgs e) => Pages.Primary.Offline.Instance.ShowWorkshop();
    }
}
