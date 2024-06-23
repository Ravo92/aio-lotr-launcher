using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using LauncherGUI.Logic;
using LauncherGUI.Popups;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace LauncherGUI.Elements
{
    /// <summary>
    /// Interaction logic for LibraryTile.xaml
    /// </summary>
    public partial class LibraryTile : UserControl
    {
        public LibraryTile()
        {
            InitializeComponent();

            BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
            BfmeWorkshopSyncManager.OnSyncUpdate += OnSyncUpdate;
            BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
        }

        private void OnSyncBegin(BfmeWorkshopKit.Data.BfmeWorkshopEntry entry)
        {
            isActiveIcon.Opacity = (entry.Guid == WorkshopEntry.Guid) ? 1d : 0d;
            IsHitTestVisible = false;
            IsLoading = entry.Guid == WorkshopEntry.Guid;
        }

        private void OnSyncUpdate(int progress)
        {
            if(IsLoading)
                LoadProgress = progress;
        }

        private void OnSyncEnd()
        {
            IsHitTestVisible = true;
            IsLoading = false;

            IsHitTestVisible = BfmeRegistryManager.IsBfmeInstalled(WorkshopEntry.Game);
            content.Opacity = IsHitTestVisible ? 1 : 0.5;
            if (IsHitTestVisible)
                try { icon.Source = new BitmapImage(new Uri(WorkshopEntry.ArtworkUrl)); } catch { }
            else
                try { icon.Source = new FormatConvertedBitmap(new BitmapImage(new Uri(WorkshopEntry.ArtworkUrl)), PixelFormats.Gray16, BitmapPalettes.Gray16, 1); } catch { }
        }

        BfmeWorkshopEntry _workshopEntry;
        public BfmeWorkshopEntry WorkshopEntry
        {
            get => _workshopEntry;
            set
            {
                _workshopEntry = value;
                title.Text = value.Name;
                version.Text = value.Version;
                author.Text = $"by {value.Author}";

                if (value.Type == 0)
                    type.Text = "Patch";
                else if (value.Type == 1)
                    type.Text = "Mod";

                BfmeWorkshopEntry? activeEntry = BfmeWorkshopSyncManager.GetActivePatch(value.Game);
                isActiveIcon.Opacity = (activeEntry != null && activeEntry!.Value.Guid == value.Guid) ? 1d : 0d;

                IsHitTestVisible = BfmeRegistryManager.IsBfmeInstalled(value.Game);
                content.Opacity = IsHitTestVisible ? 1 : 0.5;
                if (IsHitTestVisible)
                    try { icon.Source = new BitmapImage(new Uri(value.ArtworkUrl)); } catch { }
                else
                    try { icon.Source = new FormatConvertedBitmap(new BitmapImage(new Uri(value.ArtworkUrl)), PixelFormats.Gray16, BitmapPalettes.Gray16, 1); } catch { }
            }
        }

        public bool IsLoading
        {
            get => loadingBar.Visibility == Visibility.Visible;
            set
            {
                loadingBar.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                tags.Visibility = value ? Visibility.Hidden : Visibility.Visible;
                loadingIcon.IsLoading = value;
                isActiveIcon.Visibility = value ? Visibility.Collapsed : Visibility.Visible;

                if (value)
                    LoadProgress = 0;
            }
        }

        private void OnEnter(object sender, MouseEventArgs e)
        {
            hoverEffect.Opacity = 1;
        }

        private void OnLeave(object sender, MouseEventArgs e)
        {
            hoverEffect.Opacity = 0;
        }

        private async void OnClicked(object sender, MouseButtonEventArgs e)
        {
            try
            {
                await BfmeWorkshopSyncManager.Sync(WorkshopEntry, (progress) => { }, (downloadItem, downloadProgress) => { });
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new MessagePopup("SYNC ERROR", $"An unexpected error occured while trying to load {WorkshopEntry.Name}.\n{ex.ToString()}"));
            }
        }

        public double LoadProgress
        {
            get => (double)GetValue(LoadProgressProperty);
            set
            {
                SetValue(LoadProgressProperty, value);
                progressText.Text = $"{value}%";
            }
        }
        public static readonly DependencyProperty LoadProgressProperty = DependencyProperty.Register("LoadProgress", typeof(double), typeof(LibraryTile), new PropertyMetadata(OnLoadProgressChangedCallBack));
        private static void OnLoadProgressChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LibraryTile progressBar = (LibraryTile)sender;
            if (progressBar != null)
            {
                DoubleAnimation da = new DoubleAnimation() { To = (double)e.NewValue / 100d, Duration = TimeSpan.FromSeconds((double)e.NewValue == 0d ? 0d : 0.5d) };
                progressBar.progressGradientStop1.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
                progressBar.progressGradientStop2.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
            }
        }
    }
}
