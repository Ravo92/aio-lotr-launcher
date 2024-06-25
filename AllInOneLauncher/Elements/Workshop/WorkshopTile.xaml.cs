using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
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

        private async void OnClicked(object sender, MouseButtonEventArgs e)
        {
            await BfmeWorkshopLibraryManager.AddToLibrary(WorkshopEntry.Guid);
            IsInLibrary = true;
            IsUpdateAvailable = false;
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
