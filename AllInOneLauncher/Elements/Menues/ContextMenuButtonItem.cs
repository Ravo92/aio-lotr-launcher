using System;
using System.Windows;
using System.Windows.Controls;

namespace AllInOneLauncher.Elements.Menues
{
    public class ContextMenuButtonItem : ContextMenuItem
    {
        public ContextMenuButtonItem(string text, bool enabled, Action? clicked = null)
        {
            Text = text;
            Enabled = enabled;
            Clicked = clicked;
        }

        public string Text { get; set; } = "";
        public bool Enabled { get; set; } = true;
        public Action? Clicked { get; set; }

        public override FrameworkElement GenerateElement()
        {
            Button b = new Button() { Content = Text, Style = Application.Current.FindResource("MenuButton") as Style, IsHitTestVisible = Enabled, Opacity = Enabled ? 1d : 0.4d, Height = 38 };
            if (Enabled)
                b.Click += (s, e) => Clicked?.Invoke();
            return b;
        }
    }
}
