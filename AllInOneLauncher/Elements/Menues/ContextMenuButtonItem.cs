using System;
using System.Windows;
using System.Windows.Controls;

namespace AllInOneLauncher.Elements.Menues
{
    public class ContextMenuButtonItem : ContextMenuItem
    {
        public ContextMenuButtonItem(string text, bool enabled, bool round = true, double height = 34, Action? clicked = null)
        {
            Text = text;
            Enabled = enabled;
            Round = round;
            Height = height;
            Clicked = clicked;
        }

        public string Text { get; set; } = "";
        public bool Enabled { get; set; } = true;
        public bool Round { get; set; } = true;
        public double Height { get; set; } = 34;
        public Action? Clicked { get; set; }

        public override FrameworkElement GenerateElement()
        {
            Button b = new Button() { Content = Text, Style = Round ? Application.Current.FindResource("RoundedMenuButton") as Style : Application.Current.FindResource("MenuButton") as Style, IsHitTestVisible = Enabled, Opacity = Enabled ? 1d : 0.4d, Height = Height };
            if (Enabled)
                b.Click += (s, e) => Clicked?.Invoke();
            return b;
        }
    }
}
