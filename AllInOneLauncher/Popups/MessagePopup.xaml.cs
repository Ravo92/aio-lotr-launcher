using AllInOneLauncher.Elements;
using System.Windows;
using System.Windows.Controls;

namespace AllInOneLauncher.Popups
{
    /// <summary>
    /// Interaction logic for MessagePopup.xaml
    /// </summary>
    public partial class MessagePopup : PopupBody
    {
        public MessagePopup(string title, string errorMessage, string stackTrace)
        {
            InitializeComponent();
            this.title.Text = title;
            this.errorMessage.Text = errorMessage;
            stackTraceBlock.Text = stackTrace;
        }

        private void ButtonCancelClicked(object sender, RoutedEventArgs e) => Dismiss();

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(stackTraceBlock.Text);

            if (sender is Button button)
            {
                button.Content = Application.Current.FindResource("LauncherTextCopyButtonMessageBoxSystemErrorSucess").ToString()!;
            }
        }
    }
}