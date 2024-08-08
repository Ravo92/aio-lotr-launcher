﻿using AllInOneLauncher.Data;
using AllInOneLauncher.Logic;
using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
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
        }

        BfmeWorkshopEntry _entry;
        public BfmeWorkshopEntry Entry
        {
            get => _entry;
            set
            {
                _entry = value;

                activeEntryIcon.Source = null;
                activeEntryTitle.Text = value.Name;
                activeEntryVersion.Text = value.Version;
                activeEntryAuthor.Text = $"by {value.Author}";

                if (value.Type == 0)
                    activeEntryType.Text = "Patch";
                else if (value.Type == 1)
                    activeEntryType.Text = "Mod";
                else if (value.Type == 2)
                    activeEntryType.Text = "Enhancement";
                else if (value.Type == 3)
                    activeEntryType.Text = "Map Pack";

                IsHitTestVisible = BfmeRegistryManager.IsBfmeInstalled((BfmeGame)value.Game);
                activeEntry.Opacity = IsHitTestVisible ? 1 : 0.5;
                if (IsHitTestVisible)
                    try { activeEntryIcon.Source = new BitmapImage(new Uri(value.ArtworkUrl)); } catch { }
                else
                    try { activeEntryIcon.Source = new FormatConvertedBitmap(new BitmapImage(new Uri(value.ArtworkUrl)), PixelFormats.Gray16, BitmapPalettes.Gray16, 1); } catch { }
            }
        }

        private async void OnDeactivateClicked(object sender, RoutedEventArgs e)
        {
            await BfmeWorkshopSyncManager.Sync(Entry, (progress) => { }, (downloadItem, downloadProgress) => { });
        }
    }
}