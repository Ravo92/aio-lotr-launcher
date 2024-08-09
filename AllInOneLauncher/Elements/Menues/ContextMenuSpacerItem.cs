using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AllInOneLauncher.Elements.Menues
{
    public class ContextMenuSpacerItem : ContextMenuItem
    {
        public override FrameworkElement GenerateElement()
        {
            return new Border() { Height = 1, Margin = new Thickness(0, 3, 0, 3), Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19FFFFFF")), UseLayoutRounding = true, SnapsToDevicePixels = true };
        }
    }
}
