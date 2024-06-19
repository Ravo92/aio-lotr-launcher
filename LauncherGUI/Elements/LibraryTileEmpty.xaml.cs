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

namespace LauncherGUI.Elements
{
    /// <summary>
    /// Interaction logic for LibraryTileEmpty.xaml
    /// </summary>
    public partial class LibraryTileEmpty : UserControl
    {
        public LibraryTileEmpty()
        {
            InitializeComponent();
        }

        private void OnEnter(object sender, MouseEventArgs e)
        {
            hoverEffect.Opacity = 1;
        }

        private void OnLeave(object sender, MouseEventArgs e)
        {
            hoverEffect.Opacity = 0;
        }
    }
}
