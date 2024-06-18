using System;
using System.Windows.Controls;

namespace LauncherGUI.Pages.Primary
{
    /// <summary>
    /// Interaction logic for Workshop.xaml
    /// </summary>
    public partial class Workshop : UserControl
    {
        public static Workshop Instance = new Workshop();

        public Workshop()
        {
            InitializeComponent();
        }

        private void TabChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedIndex == 0) // BFME1
            {

            }
            else if (tabs.SelectedIndex == 1) // BFME2
            {

            }
            else if (tabs.SelectedIndex == 2) // ROTWK
            {

            }
        }
    }
}