using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using AllInOneLauncher.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AllInOneLauncher.Data;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

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

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    if (titleStack.ActualWidth <= availableTitleArea.ActualWidth)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            titleStack.BeginAnimation(FrameworkElement.MarginProperty, null);
                            titleStack.SetValue(FrameworkElement.MarginProperty, new Thickness(0));
                        });
                        await Task.Delay(TimeSpan.FromSeconds(2));
                        continue;
                    }

                    var duration = TimeSpan.FromSeconds((titleStack.ActualWidth - availableTitleArea.ActualWidth) * 0.05);

                    Dispatcher.Invoke(() =>
                    {
                        ThicknessAnimation l = new() { To = new Thickness(availableTitleArea.ActualWidth - titleStack.ActualWidth, 0, 0, 0), Duration = duration };
                        Dispatcher.Invoke(() => titleStack.BeginAnimation(FrameworkElement.MarginProperty, l));
                    });
                    await Task.Delay(duration.Add(TimeSpan.FromSeconds(2)));

                    if (titleStack.ActualWidth <= availableTitleArea.ActualWidth)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            titleStack.BeginAnimation(FrameworkElement.MarginProperty, null);
                            titleStack.SetValue(FrameworkElement.MarginProperty, new Thickness(0));
                        });
                        await Task.Delay(TimeSpan.FromSeconds(2));
                        continue;
                    }

                    Dispatcher.Invoke(() =>
                    {
                        ThicknessAnimation r = new() { To = new Thickness(0, 0, 0, 0), Duration = duration };
                        Dispatcher.Invoke(() => titleStack.BeginAnimation(FrameworkElement.MarginProperty, r));
                    });
                    await Task.Delay(duration.Add(TimeSpan.FromSeconds(2)));
                }
            });
        }

        BfmeWorkshopEntry? _entry = null;
        public BfmeWorkshopEntry? Entry
        {
            get => _entry;
            set
            {
                _entry = value;

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
                else if (value.Value.Type == 2)
                    activeEntryType.Text = "Enhancement";
                else if (value.Value.Type == 3)
                    activeEntryType.Text = "Map Pack";

                activeEntryLoading.Visibility = IsLoading ? Visibility.Visible : Visibility.Hidden;
                activeEntryActive.Visibility = IsLoading ? Visibility.Hidden : Visibility.Visible;
                activeEntryReloadButton.Visibility = IsLoading ? Visibility.Hidden : Visibility.Visible;

                IsHitTestVisible = BfmeRegistryManager.IsBfmeInstalled((BfmeGame)value.Value.Game);
                activeEntry.Opacity = IsHitTestVisible ? 1 : 0.5;
                if (IsHitTestVisible)
                    try { activeEntryIcon.Source = new BitmapImage(new Uri(value.Value.ArtworkUrl)); } catch { }
                else
                    try { activeEntryIcon.Source = new FormatConvertedBitmap(new BitmapImage(new Uri(value.Value.ArtworkUrl)), PixelFormats.Gray16, BitmapPalettes.Gray16, 1); } catch { }
            }
        }

        private bool isLoading = false;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;

                activeEntryLoading.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                activeEntryActive.Visibility = value ? Visibility.Hidden : Visibility.Visible;
                activeEntryReloadButton.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
        }

        private async void OnResyncActiveEntry(object sender, RoutedEventArgs e)
        {
            if (Entry != null)
            {
                Entry = BfmeWorkshopSyncManager.GetActivePatch(Entry!.Value.Game);
                IsLoading = false;
            }

            if (Entry != null)
                await BfmeWorkshopSyncManager.Sync(Entry!.Value, (progress) => { }, (downloadItem, downloadProgress) => { });
        }
    }
}
