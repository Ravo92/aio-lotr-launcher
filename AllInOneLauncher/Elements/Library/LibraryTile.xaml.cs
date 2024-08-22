﻿using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using AllInOneLauncher.Popups;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Collections.Generic;
using AllInOneLauncher.Elements.Menues;
using AllInOneLauncher.Pages.Primary;
using BfmeFoundationProject.BfmeRegistryManagement;
using System.Diagnostics;
using System.IO;
using Windows.Graphics.Printing.Workflow;

namespace AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for LibraryTile.xaml
    /// </summary>
    public partial class LibraryTile : UserControl
    {
        public LibraryTile()
        {
            InitializeComponent();
            Properties.Settings.Default.SettingsSaving += (s, e) => UpdateType();

            BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
            BfmeWorkshopSyncManager.OnSyncUpdate += OnSyncUpdate;
            BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
        }

        private void OnSyncBegin(BfmeWorkshopEntry entry)
        {
            if (entry.Game != WorkshopEntry.Game)
                return;

            Dispatcher.Invoke(() =>
            {
                IsHitTestVisible = false;
                IsLoading = entry.Guid == WorkshopEntry.Guid;
                UpdateIsActive();
            });
        }

        private void OnSyncUpdate(int progress)
        {
            Dispatcher.Invoke(() =>
            {
                if (IsLoading)
                    LoadProgress = progress;
            });
        }

        private void OnSyncEnd()
        {
            Dispatcher.Invoke(() =>
            {
                IsHitTestVisible = true;
                IsLoading = false;

                IsHitTestVisible = BfmeRegistryManager.IsInstalled(WorkshopEntry.Game);
                content.Opacity = IsHitTestVisible ? 1 : 0.5;
                if (IsHitTestVisible)
                    try
                    {
                        icon.Source = new BitmapImage(new Uri(WorkshopEntry.ArtworkUrl));
                    }
                    catch
                    {

                    }
                else
                    try
                    {
                        icon.Source = new FormatConvertedBitmap(new BitmapImage(new Uri(WorkshopEntry.ArtworkUrl)), PixelFormats.Gray16, BitmapPalettes.Gray16, 1);
                    }
                    catch
                    {

                    }
                UpdateIsActive();
            });
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
                author.Text = value.Author;
                UpdateType();

                IsHitTestVisible = BfmeRegistryManager.IsInstalled(value.Game);
                content.Opacity = IsHitTestVisible ? 1 : 0.5;
                if (IsHitTestVisible)
                    try { icon.Source = new BitmapImage(new Uri(value.ArtworkUrl)); } catch { }
                else
                    try { icon.Source = new FormatConvertedBitmap(new BitmapImage(new Uri(value.ArtworkUrl)), PixelFormats.Gray16, BitmapPalettes.Gray16, 1); } catch { }

                UpdateIsActive();
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
            else if (WorkshopEntry.Type == 4)
                entryType.Text = Application.Current.FindResource("LibraryTileSnapshotType").ToString()!;
        }

        public bool IsUpdateAvailable
        {
            get => updateAvailableIcon.Visibility == Visibility.Visible;
            set => updateAvailableIcon.Visibility = value ? Visibility.Visible : Visibility.Hidden;
        }

        private void UpdateIsActive()
        {
            if (WorkshopEntry.Type == 0 || WorkshopEntry.Type == 1 || WorkshopEntry.Type == 4)
            {
                activeText.Visibility = Visibility.Visible;
                BfmeWorkshopEntry? activeEntry = BfmeWorkshopSyncManager.GetActivePatch(WorkshopEntry.Game);
                isActiveIcon.Opacity = (activeEntry != null && activeEntry!.Value.Guid == WorkshopEntry.Guid) ? 1d : 0d;
            }
            else
            {
                activeText.Visibility = Visibility.Collapsed;
                Dictionary<string, BfmeWorkshopEntry> activeEnhancements = BfmeWorkshopSyncManager.GetActiveEnhancements(WorkshopEntry.Game);
                isActiveIcon.Opacity = activeEnhancements.ContainsKey(WorkshopEntry.Guid) ? 1d : 0d;
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

        private void OnClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Sync();
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if (WorkshopEntry.Type == 0 || WorkshopEntry.Type == 1 || WorkshopEntry.Type == 4)
                {
                    MenuVisualizer.ShowMenu(
                    menu: [
                        new ContextMenuButtonItem(IsUpdateAvailable ? "Update" : "Package up to date", IsUpdateAvailable, clicked: Update),
                        new ContextMenuButtonItem(isActiveIcon.Opacity == 0d ? $"Switch to \"{WorkshopEntry.Name}\"" : "Sync again", true, clicked: Sync),
                        new ContextMenuSpacerItem(),
                        new ContextMenuButtonItem("Open keybinds folder", true, clicked: () =>
                        {
                            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Keybinds", $"{WorkshopEntry.GameName()}-{WorkshopEntry.Name}")))
                                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Keybinds", $"{WorkshopEntry.GameName()}-{WorkshopEntry.Name}"));
                            Process.Start(new ProcessStartInfo("explorer.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Keybinds", $"{WorkshopEntry.GameName()}-{WorkshopEntry.Name}")));
                        }),
                        new ContextMenuButtonItem("Open game folder", true, clicked: () => Process.Start(new ProcessStartInfo("explorer.exe", Path.Combine(BfmeRegistryManager.GetKeyValue(WorkshopEntry.Game, BfmeFoundationProject.BfmeRegistryManagement.Data.BfmeRegistryKey.InstallPath))))),
                        new ContextMenuSpacerItem(),
                        new ContextMenuButtonItem("Copy package GUID", true, clicked: () => Clipboard.SetDataObject(WorkshopEntry.Guid)),
                        new ContextMenuSpacerItem(),
                        new ContextMenuButtonItem("Remove from library", true, clicked: RemoveFromLibrary)
                    ],
                    owner: this,
                    side: MenuSide.BottomLeft,
                    padding: 4,
                    tint: true,
                    minWidth: 200,
                    targetCursor: true);
                }
                else
                {
                    MenuVisualizer.ShowMenu(
                    menu: [
                        new ContextMenuButtonItem(IsUpdateAvailable ? "Update" : "Package up to date", IsUpdateAvailable, clicked: Update),
                        new ContextMenuButtonItem(isActiveIcon.Opacity == 0d ? $"Enable \"{WorkshopEntry.Name}\"" : "Disable", true, clicked: Sync),
                        new ContextMenuSpacerItem(),
                        new ContextMenuButtonItem("Copy package GUID", true, clicked: () => Clipboard.SetDataObject(WorkshopEntry.Guid)),
                        new ContextMenuSpacerItem(),
                        new ContextMenuButtonItem("Remove from library", true, clicked: RemoveFromLibrary)
                    ],
                    owner: this,
                    side: MenuSide.BottomLeft,
                    padding: 4,
                    tint: true,
                    minWidth: 200,
                    targetCursor: true);
                }
            }
        }

        private async void Sync()
        {
            try
            {
                await BfmeWorkshopSyncManager.Sync(WorkshopEntry, (progress) => { }, (downloadItem, downloadProgress) => { });
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            }
        }

        private async void Update()
        {
            try
            {
                WorkshopEntry = (await BfmeWorkshopQueryManager.Get(WorkshopEntry.Guid.Split(':')[0])).entry;
                BfmeWorkshopLibraryManager.AddToLibrary(WorkshopEntry);
                IsUpdateAvailable = false;
                if (isActiveIcon.Opacity == 1d) Sync();
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            }
        }

        private void RemoveFromLibrary()
        {
            BfmeWorkshopLibraryManager.RemoveFromLibrary(WorkshopEntry.Guid.Split(':')[0]);
            Offline.Instance.library.libraryTiles.Children.Remove(this);
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
                DoubleAnimation da = new() { To = (double)e.NewValue / 100d, Duration = TimeSpan.FromSeconds((double)e.NewValue == 0d ? 0d : 0.5d) };
                progressBar.progressGradientStop1.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
                progressBar.progressGradientStop2.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
            }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    if ((await BfmeWorkshopQueryManager.Get(WorkshopEntry.Guid)).entry.Version != WorkshopEntry.Version)
                        Dispatcher.Invoke(() => IsUpdateAvailable = true);
                }
                catch { }
            });
        }
    }
}
