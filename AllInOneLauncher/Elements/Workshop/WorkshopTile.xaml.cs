﻿using AllInOneLauncher.Elements.Menues;
using AllInOneLauncher.Popups;
using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopTile.xaml
    /// </summary>
    public partial class WorkshopTile : UserControl
    {
        public WorkshopTile()
        {
            InitializeComponent();
        }

        BfmeWorkshopEntry _workshopEntry;
        public BfmeWorkshopEntry WorkshopEntry
        {
            get => _workshopEntry;
            set
            {
                _workshopEntry = value;
                try { icon.Source = new BitmapImage(new Uri(value.ArtworkUrl)); } catch { }
                title.Text = value.Name;
                version.Text = value.Version;
                author.Text = $"by {value.Author}";

                if (value.Type == 0)
                    type.Text = "Patch";
                else if (value.Type == 1)
                    type.Text = "Mod";
                else if (value.Type == 2)
                    type.Text = "Enhancement";
                else if (value.Type == 3)
                    type.Text = "Map Pack";
            }
        }

        public bool IsInLibrary
        {
            get => inLibraryIcon.Visibility == Visibility.Visible;
            set => inLibraryIcon.Visibility = value ? Visibility.Visible : Visibility.Hidden;
        }

        public bool IsUpdateAvailable
        {
            get => updateAvailableIcon.Visibility == Visibility.Visible;
            set => updateAvailableIcon.Visibility = value ? Visibility.Visible : Visibility.Hidden;
        }

        private void OnEnter(object sender, MouseEventArgs e)
        {
            hoverEffect.Opacity = 1;
        }

        private void OnLeave(object sender, MouseEventArgs e)
        {
            hoverEffect.Opacity = 0;
        }

        private void OnClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                AddToLibrary();
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                MenuVisualizer.ShowMenu(
                menu: [
                    new ContextMenuButtonItem(IsUpdateAvailable ? "Update" : (IsInLibrary ? "Already in library" : "Add to library"), !IsInLibrary || IsUpdateAvailable, clicked: AddToLibrary),
                    new ContextMenuSpacerItem(),
                    new ContextMenuButtonItem("Copy package GUID", true, clicked: () => Clipboard.SetDataObject(WorkshopEntry.Guid))
                ],
                owner: this,
                side: MenuSide.BottomLeft,
                padding: 4,
                tint: true,
                minWidth: 200,
                targetCursor: true);
            }
        }

        private async void AddToLibrary()
        {
            try
            {
                await BfmeWorkshopLibraryManager.AddToLibrary(WorkshopEntry.Guid);
                IsInLibrary = true;
                IsUpdateAvailable = false;
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new MessagePopup("ERROR", $"An unexpected error occurred while trying to add {WorkshopEntry.Name} to your library.\n{ex}"));
            }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                BfmeWorkshopEntry? localEntry = (await BfmeWorkshopLibraryManager.Search(page: -1)).Select(x => new BfmeWorkshopEntry?(x)).FirstOrDefault(x => x != null && x.Value!.Guid == WorkshopEntry.Guid, null);
                Dispatcher.Invoke(() =>
                {
                    IsInLibrary = localEntry != null;
                    IsUpdateAvailable = localEntry != null && localEntry!.Value.Version != WorkshopEntry.Version;
                });
            });
        }
    }
}
