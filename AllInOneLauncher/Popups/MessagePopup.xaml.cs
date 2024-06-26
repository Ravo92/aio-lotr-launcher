using AllInOneLauncher.Elements;
using System.Windows;

namespace AllInOneLauncher.Popups
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
