using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AllInOneLauncher.Elements.Menues
{
    public class ContextMenuSpacerItem : ContextMenuItem
    {
        public override FrameworkElement GenerateElement()
        {
            return new Rectangle() { Height = 1, Margin = new Thickness(0, 3, 0, 3), Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#454545")) };
        }
    }
}
