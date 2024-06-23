using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using LauncherGUI.Elements;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LauncherGUI.Pages.Subpages.Offline
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

        public async void Load(int game)
        {
            workshopTiles.Children.Clear();
            foreach (BfmeWorkshopEntry entry in await BfmeWorkshopQueryManager.Search(game: game))
                if (!entry.Guid.StartsWith("original-"))
                    workshopTiles.Children.Add(new WorkshopTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
        }
    }
}
