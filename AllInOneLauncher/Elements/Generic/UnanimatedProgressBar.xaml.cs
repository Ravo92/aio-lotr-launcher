using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for UnanimatedProgressBar.xaml
    /// </summary>
    public partial class UnanimatedProgressBar : UserControl
    {
        private readonly RectangleGeometry ProgressFillClip = new() { Rect = new Rect(0, 0, 0, 0) };

        public UnanimatedProgressBar()
        {
            InitializeComponent();
            progress.Clip = ProgressFillClip;
        }

        public Brush BackgroundFillBrush
        {
            get => background.Background;
            set => background.Background = value;
        }

        public Brush ProgressFillBrush
        {
            get => progress.Background;
            set => progress.Background = value;
        }

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(UnanimatedProgressBar), new PropertyMetadata(OnProgressChangedCallBack));
        private static void OnProgressChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UnanimatedProgressBar progressBar = (UnanimatedProgressBar)sender;
            if (progressBar != null)
                progressBar.ProgressFillClip.Rect = new Rect(0, 0, progressBar.background.ActualWidth * ((double)e.NewValue / 100), progressBar.background.ActualHeight);
        }

        private void Progress_SizeChanged(object sender, SizeChangedEventArgs e) => ProgressFillClip.Rect = new Rect(0, 0, background.ActualWidth * (Progress / 100), e.NewSize.Height);
    }
}
