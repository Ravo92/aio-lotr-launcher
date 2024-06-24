using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using AllInOneLauncher.Logic;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for LibraryTileHorizontal.xaml
    /// </summary>
    public partial class LibraryTileHorizontal : UserControl
    {
        public LibraryTileHorizontal()
        {
            InitializeComponent();
        }

        BfmeWorkshopEntry? _entry = null;
        public BfmeWorkshopEntry? Entry
        {
            get => _entry;
            set
            {
                _entry = value;

                IsLoading = false;

                if (value == null)
                {
                    activeEntry.Visibility = Visibility.Hidden;
                    activeEntryNullIndicator.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    activeEntry.Visibility = Visibility.Visible;
                    activeEntryNullIndicator.Visibility = Visibility.Hidden;
                }

                activeEntryIcon.Source = null;
                activeEntryTitle.Text = value.Value.Name;
                activeEntryVersion.Text = value.Value.Version;
                activeEntryAuthor.Text = $"by {value.Value.Author}";

                if (value.Value.Type == 0)
                    activeEntryType.Text = "Patch";
                else if (value.Value.Type == 1)
                    activeEntryType.Text = "Mod";

                activeEntryLoading.Visibility = Visibility.Hidden;
                activeEntryActive.Visibility = Visibility.Visible;
                activeEntryReloadButton.Visibility = Visibility.Visible;

                IsHitTestVisible = BfmeRegistryManager.IsBfmeInstalled(value.Value.Game);
                activeEntry.Opacity = IsHitTestVisible ? 1 : 0.5;
                if (IsHitTestVisible)
                    try { activeEntryIcon.Source = new BitmapImage(new Uri(value.Value.ArtworkUrl)); } catch { }
                else
                    try { activeEntryIcon.Source = new FormatConvertedBitmap(new BitmapImage(new Uri(value.Value.ArtworkUrl)), PixelFormats.Gray16, BitmapPalettes.Gray16, 1); } catch { }
            }
        }

        public bool IsLoading
        {
            get => activeEntryLoading.Visibility == Visibility.Visible;
            set
            {
                activeEntryLoading.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                activeEntryActive.Visibility = value ? Visibility.Hidden : Visibility.Visible;
                activeEntryReloadButton.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
        }

        private async void OnResyncActiveEntry(object sender, RoutedEventArgs e)
        {
            if (Entry != null)
                Entry = BfmeWorkshopSyncManager.GetActivePatch(Entry!.Value.Game);

            if (Entry != null)
                await BfmeWorkshopSyncManager.Sync(Entry!.Value, (progress) => { }, (downloadItem, downloadProgress) => { });
        }
    }
}
