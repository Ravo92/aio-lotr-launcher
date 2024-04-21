using System;
using System.Windows.Input;
using System.Windows.Controls;

namespace LauncherGUI.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Guides.xaml
    /// </summary>
    public partial class Guides : UserControl
    {
        public Guides()
        {
            InitializeComponent();
            GuidesPage.Source = new Uri("https://ravo92.github.io/guidespage/index.html");
        }

        private void GuidesPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                GuidesPage.CoreWebView2.OpenDevToolsWindow();
                e.Handled = true;
            }
        }
    }
}