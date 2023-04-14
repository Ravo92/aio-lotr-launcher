using LauncherGUI.Helpers;
using System.Collections.Generic;
using System.Windows;

namespace LauncherGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<PatchesAndModsVM> PatchesAndMods { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void ButtonGameBFME1_Click(object sender, RoutedEventArgs e)
        {
            Style? styleButtonBFME1 = FindResource("UnderlinedButton") as Style;
            GameBFME1.Style = styleButtonBFME1;

            Style? styleButtonBFME2 = FindResource("ButtonHovered") as Style;
            GameBFME2.Style = styleButtonBFME2;

            Style? styleButtonBFME2EP1 = FindResource("ButtonHovered") as Style;
            GameBFME2EP1.Style = styleButtonBFME2EP1;
        }

        private void ButtonGameBFME2_Click(object sender, RoutedEventArgs e)
        {
            Style? styleButtonBFME1 = FindResource("ButtonHovered") as Style;
            GameBFME1.Style = styleButtonBFME1;

            Style? styleButtonBFME2 = FindResource("UnderlinedButton") as Style;
            GameBFME2.Style = styleButtonBFME2;

            Style? styleButtonBFME2EP1 = FindResource("ButtonHovered") as Style;
            GameBFME2EP1.Style = styleButtonBFME2EP1;
        }

        private void ButtonGameBFME2EP1_Click(object sender, RoutedEventArgs e)
        {
            Style? styleButtonBFME1 = FindResource("ButtonHovered") as Style;
            GameBFME1.Style = styleButtonBFME1;

            Style? styleButtonBFME2 = FindResource("ButtonHovered") as Style;
            GameBFME2.Style = styleButtonBFME2;

            Style? styleButtonBFME2EP1 = FindResource("UnderlinedButton") as Style;
            GameBFME2EP1.Style = styleButtonBFME2EP1;
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Style? styleButtonBFME1 = FindResource("UnderlinedButton") as Style;
            GameBFME1.Style = styleButtonBFME1;

            PatchesAndMods = new List<PatchesAndModsVM>
            {
                new()
                {
                    DataGridVerionNumber = "1.03",
                    DataGridDescription = "Official Patch 1.03",
                    DataGridKindOf = DataGridKindOf.Patch,
                    DataGridActivated = true,
                },
                new()
                {
                    DataGridVerionNumber = "1.06",
                    DataGridDescription = "Unofficial Patch 1.06",
                    DataGridKindOf = DataGridKindOf.Patch,
                    DataGridActivated = false,
                },
                new()
                {
                    DataGridVerionNumber = "2.22.32",
                    DataGridDescription = "Patch 2.22",
                    DataGridKindOf = DataGridKindOf.Patch,
                    DataGridActivated = true,
                },
                new()
                {
                    DataGridVerionNumber = "6.01",
                    DataGridDescription = "Elvenstar Mod",
                    DataGridKindOf = DataGridKindOf.Mod,
                    DataGridActivated = true,
                },
                new()
                {
                    DataGridVerionNumber = "1.1",
                    DataGridDescription = "Shadow and Flame",
                    DataGridKindOf = DataGridKindOf.Mod,
                    DataGridActivated = true,
                },
                new()
                {
                    DataGridVerionNumber = "3.15",
                    DataGridDescription = "Edain Mod",
                    DataGridKindOf = DataGridKindOf.Mod,
                    DataGridActivated = true,
                },
            };

            DataContext = this;

            //foreach (string entries in PatchModDetectionHelper.AllPatchesAndMods)
            //{
            //    MainTable.Items.Add(entries);
            //}

            // MainTable.ItemsSource = ;
        }
    }
}