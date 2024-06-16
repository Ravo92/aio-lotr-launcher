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
        public event EventHandler? AcceptClicked;
        public event EventHandler? CancelClicked;

        public string TextInstallTitle
        {
            get => TextTitleInstall.Text;
            set => TextTitleInstall.Text = value;
        }

        public InstallDialog()
        {
            InitializeComponent();
            ComboBoxGameLanguage.SelectedIndex = 0;
        }

        private void ComboBoxLibrarySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxGameLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonAcceptClicked(object sender, RoutedEventArgs e)
        {
            AcceptClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonCancelClicked(object sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}