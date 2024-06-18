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
    /// Interaction logic for MessagePopup.xaml
    /// </summary>
    public partial class MessagePopup : PopupBody
    {
        public MessagePopup(string title, string message)
        {
            InitializeComponent();
            this.title.Text = title;
            this.message.Text = message;
        }

        private void ButtonCancelClicked(object sender, RoutedEventArgs e) => Dismiss();
    }
}
