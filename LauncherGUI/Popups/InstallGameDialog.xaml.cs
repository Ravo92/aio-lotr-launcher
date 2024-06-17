using LauncherGUI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LauncherGUI.Popups
{
    /// <summary>
    /// Interaction logic for InstallGameDialog.xaml
    /// </summary>
    public partial class InstallGameDialog : PopupBody
    {
        public InstallGameDialog()
        {
            InitializeComponent();
        }

        private void ComboBoxGameLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxLibrarySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonAcceptClicked(object sender, RoutedEventArgs e) => Submit("selected_language", "selected_library"); // TODO: Put actual values here.

        private void ButtonCancelClicked(object sender, RoutedEventArgs e) => Dismiss();
    }
}
