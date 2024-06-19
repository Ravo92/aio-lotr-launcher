using BfmeWorkshopKit.Data;
using BfmeWorkshopKit.Logic;
using LauncherGUI.Popups;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace LauncherGUI.Elements
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
            set
            {
                updateAvailableIcon.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                inLibraryIcon.Opacity = value ? 0d : 1d;
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
            await BfmeWorkshopLibraryManager.AddToLibrary(WorkshopEntry.Guid);
            IsInLibrary = true;
            IsUpdateAvailable = false;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                string libraryDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Library");
                foreach (var file in Directory.GetFiles(libraryDirectory))
                {
                    try
                    {
                        BfmeWorkshopEntry entry = JsonConvert.DeserializeObject<BfmeWorkshopEntry>(File.ReadAllText(file));
                        if(entry.Guid == WorkshopEntry.Guid)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                IsInLibrary = true;
                                IsUpdateAvailable = entry.Version != WorkshopEntry.Version;
                            });
                        }
                    }
                    catch { }
                }
            });
        }
    }
}
