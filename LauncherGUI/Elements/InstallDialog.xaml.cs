using System;
using System.Windows;
using System.Windows.Controls;

namespace LauncherGUI.Elements
{
    /// <summary>
    /// Interaction logic for InstallDialog.xaml
    /// </summary>
    public partial class InstallDialog : UserControl
    {
        public InstallDialog()
        {
            InitializeComponent();
        }

        public string PatchName
        {
            get => patchName.Text;
            set => patchName.Text = value;
        }

        public string PatchVersion
        {
            get => patchVersion.Text;
            set => patchVersion.Text = value;
        }

        private void ButtonAcceptClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
