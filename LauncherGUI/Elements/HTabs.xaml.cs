using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LauncherGUI.Elements
{
    /// <summary>
    /// Interaction logic for HTabs.xaml
    /// </summary>
    public partial class HTabs : UserControl
    {
        private bool FirstLoad = true;

        public HTabs()
        {
            InitializeComponent();
        }

        public event EventHandler? SelectedIndexChanged;

        private List<ImageSource> _tabs = new List<ImageSource>();
        public List<ImageSource> Tabs
        {
            get => _tabs;
            set
            {
                _tabs = value;

                foreach(var tab in _tabs)
                {
                    tabs.Children.Add(new HTab() { Owner = this, Icon = tab });
                }
            }
        }

        public int SelectedIndex
        {
            get => tabs.Children.OfType<HTab>().ToList().FindIndex(x => x.Selected);
            set
            {
                int i = 0;
                foreach(var tab in tabs.Children.OfType<HTab>())
                {
                    tab.Selected = i == value;
                    i++;
                }

                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!FirstLoad)
                return;

            FirstLoad = false;
            Tabs = _tabs;
            SelectedIndex = 0;
        }
    }
}
