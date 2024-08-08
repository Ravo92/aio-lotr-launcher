﻿using AllInOneLauncher.Elements.Menues;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for DropdownPicker.xaml
    /// </summary>
    public partial class DropdownPicker : UserControl
    {
        public DropdownPicker()
        {
            InitializeComponent();
        }

        public event EventHandler? OnOptionSelected;

        private List<string> options = new List<string>();
        public List<string> Options
        {
            get => options;
            set
            {
                options = value;
                if (value.Count > Selected)
                    title.Text = value[selected];
                else
                    Selected = 0;

                MenuVisualizer.HideMenuOn(frame);
            }
        }

        private int selected = 0;
        public int Selected
        {
            get => selected;
            set
            {
                if (value == int.MinValue || Options.Count <= value)
                    return;

                if (selected != value)
                {
                    selected = value;
                    title.Text = Options[value];
                    OnOptionSelected?.Invoke(this, EventArgs.Empty);
                }

                MenuVisualizer.HideMenuOn(frame);
            }
        }

        private void OnDropdownClicked(object sender, RoutedEventArgs e)
        {
            if (MenuVisualizer.HasMenuOn(frame))
                return;

            MenuVisualizer.ShowMenu(
            menu: Options.Select(x => new ContextMenuButtonItem(x, true, () => Selected = Options.Contains(x) ? Options.IndexOf(x) : int.MinValue) as ContextMenuItem).ToList(),
            owner: frame,
            side: MenuSide.Bottom,
            space: 0,
            corners: new CornerRadius(0, 0, 20, 20),
            fullWidth: true,
            onDestroy: () =>
            {
                CornerRadiusAnimation ca = new CornerRadiusAnimation() { From = new CornerRadius(20, 20, 0, 0), To = new CornerRadius(20), Duration = TimeSpan.FromSeconds(0.075), EasingFunction = new QuadraticEase() };
                frame.BeginAnimation(Border.CornerRadiusProperty, ca);
            });

            CornerRadiusAnimation ca = new CornerRadiusAnimation() { From = new CornerRadius(20), To = new CornerRadius(20, 20, 0, 0), Duration = TimeSpan.FromSeconds(0.075), EasingFunction = new QuadraticEase() };
            frame.BeginAnimation(Border.CornerRadiusProperty, ca);
        }
    }

    public class CornerRadiusAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(CornerRadius);

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(CornerRadius), typeof(CornerRadiusAnimation));
        public CornerRadius From
        {
            get { return (CornerRadius)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(CornerRadius), typeof(CornerRadiusAnimation));
        public CornerRadius To
        {
            get { return (CornerRadius)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(CornerRadiusAnimation));
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            CornerRadius fromVal = From;
            CornerRadius toVal = To;

            if (animationClock.CurrentProgress == null)
            {
                return fromVal;
            }

            double progress = animationClock.CurrentProgress.Value;

            // Apply easing function if specified
            if (EasingFunction != null)
            {
                progress = EasingFunction.Ease(progress);
            }

            return new CornerRadius(
                fromVal.TopLeft + (toVal.TopLeft - fromVal.TopLeft) * progress,
                fromVal.TopRight + (toVal.TopRight - fromVal.TopRight) * progress,
                fromVal.BottomRight + (toVal.BottomRight - fromVal.BottomRight) * progress,
                fromVal.BottomLeft + (toVal.BottomLeft - fromVal.BottomLeft) * progress);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new CornerRadiusAnimation();
        }
    }
}
