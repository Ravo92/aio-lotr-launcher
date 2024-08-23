﻿using AllInOneLauncher.Data;
using AllInOneLauncher.Logic;
using BfmeFoundationProject.BfmeRegistryManagement;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for EnabledEnhancementTile.xaml
    /// </summary>
    public partial class EnabledEnhancementTile : UserControl
    {
        public EnabledEnhancementTile()
        {
            InitializeComponent();
            Properties.Settings.Default.SettingsSaving += (s, e) => UpdateType();
        }

        BfmeWorkshopEntry _workshopEntry;
        public BfmeWorkshopEntry WorkshopEntry
        {
            get => _workshopEntry;
            set
            {
                _workshopEntry = value;

                activeEntryIcon.Source = null;
                activeEntryTitle.Text = value.Name;
                activeEntryVersion.Text = value.Version;
                activeEntryAuthor.Text = value.Author;
                UpdateType();

                IsHitTestVisible = BfmeRegistryManager.IsInstalled(value.Game);
                activeEntry.Opacity = IsHitTestVisible ? 1 : 0.5;
                if (IsHitTestVisible)
                    try { activeEntryIcon.Source = new BitmapImage(new Uri(value.ArtworkUrl)); } catch { }
                else
                    try { activeEntryIcon.Source = new FormatConvertedBitmap(new BitmapImage(new Uri(value.ArtworkUrl)), PixelFormats.Gray16, BitmapPalettes.Gray16, 1); } catch { }
            }
        }

        private void UpdateType()
        {
            if (WorkshopEntry.Type == 0)
                entryType.Text = Application.Current.FindResource("LibraryTilePatchType").ToString()!;
            else if (WorkshopEntry.Type == 1)
                entryType.Text = Application.Current.FindResource("LibraryTileModType").ToString()!;
            else if (WorkshopEntry.Type == 2)
                entryType.Text = Application.Current.FindResource("LibraryTileEnhancementType").ToString()!;
            else if (WorkshopEntry.Type == 3)
                entryType.Text = Application.Current.FindResource("LibraryTileMapPackType").ToString()!;
        }

        private async void OnDeactivateClicked(object sender, RoutedEventArgs e)
        {
            await BfmeWorkshopSyncManager.Sync(WorkshopEntry);
        }
    }
}
