using BfmeWorkshopKit.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LauncherGUI.Elements
{
    /// <summary>
    /// Interaction logic for LaunchButton.xaml
    /// </summary>
    public partial class LaunchButton : UserControl
    {
        public LaunchButton()
        {
            InitializeComponent();

            BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
            BfmeWorkshopSyncManager.OnSyncUpdate += OnSyncUpdate;
            BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
        }

        private void OnSyncBegin(BfmeWorkshopKit.Data.BfmeWorkshopEntry entry)
        {
            ButtonState = LaunchButtonState.Loading;
            LoadStatus = $"Switching to {entry.Name}";
        }

        private void OnSyncUpdate(int progress)
        {
            LoadProgress = progress;
        }

        private void OnSyncEnd()
        {
            ButtonState = LaunchButtonState.Launch;
        }

        public event EventHandler? OnLaunchClicked;
        public event EventHandler? OnInstallClicked;

        private LaunchButtonState _buttonState = LaunchButtonState.Launch;
        public LaunchButtonState ButtonState
        {
            get => _buttonState;
            set
            {
                _buttonState = value;

                if(value == LaunchButtonState.Launch)
                {
                    button.Content = "Launch";
                    button.Opacity = 1d;
                    button.IsHitTestVisible = true;
                    LoadProgress = 0;
                    progressIndication.Visibility = Visibility.Collapsed;
                }
                else if (value == LaunchButtonState.Install)
                {
                    button.Content = "Install";
                    button.Opacity = 1d;
                    button.IsHitTestVisible = true;
                    LoadProgress = 0;
                    progressIndication.Visibility = Visibility.Collapsed;
                }
                else if (value == LaunchButtonState.Loading)
                {
                    button.Content = "";
                    LoadStatus = "Loading";
                    button.Opacity = 0.4d;
                    button.IsHitTestVisible = false;
                    LoadProgress = 0;
                    progressIndication.Visibility = Visibility.Visible;
                }
            }
        }

        public double LoadProgress
        {
            get => (double)GetValue(LoadProgressProperty);
            set
            {
                SetValue(LoadProgressProperty, value);
                progressText.Text = $"{value}%";
            }
        }
        public static readonly DependencyProperty LoadProgressProperty = DependencyProperty.Register("LoadProgress", typeof(double), typeof(LaunchButton), new PropertyMetadata(OnLoadProgressChangedCallBack));
        private static void OnLoadProgressChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LaunchButton progressBar = (LaunchButton)sender;
            if (progressBar != null)
            {
                DoubleAnimation da = new DoubleAnimation() { To = (double)e.NewValue / 100d, Duration = TimeSpan.FromSeconds((double)e.NewValue == 0d ? 0d : 0.5d) };
                progressBar.progressGradientStop1.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
                progressBar.progressGradientStop2.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
            }
        }

        public string LoadStatus
        {
            get => statusText.Text;
            set => statusText.Text = value;
        }

        private void OnClicked(object sender, RoutedEventArgs e)
        {
            if (ButtonState == LaunchButtonState.Launch)
                OnLaunchClicked?.Invoke(this, EventArgs.Empty);
            else if(ButtonState == LaunchButtonState.Install)
                OnInstallClicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public enum LaunchButtonState
    {
        Launch,
        Install,
        Loading
    }
}
