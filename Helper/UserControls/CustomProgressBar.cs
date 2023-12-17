using System.Drawing.Drawing2D;

namespace Helper.UserControls
{
    public enum ProgressBarDisplayText
    {
        Percentage,
        CustomText
    }

    public enum ProgressBarGame
    {
        BFME1,
        BFME2,
        BFME25
    }

    public class CustomProgressBar : ProgressBar
    {
        // Property to set to decide whether to print a % or Text
        public ProgressBarDisplayText DisplayStyle { get; set; }

        // Property to hold the custom text
        public string? CustomText { get; set; }

        // Which game this progress bar is for (for styling)
        public static ProgressBarGame Game
        {
            get
            {
                return _game;
            }
            set
            {
                _game = value;
                _ProgressBarBackground = value switch
                {
                    ProgressBarGame.BFME1 => Properties.Resources.BFME1_PBarBG,
                    ProgressBarGame.BFME2 => Properties.Resources.BFME2_PBarBG,
                    ProgressBarGame.BFME25 => Properties.Resources.BFME25_PBarBG,
                    _ => Properties.Resources.BFME1_PBarBG
                };
            }
        }

        private static ProgressBarGame _game;
        private static Bitmap _ProgressBarBackground = Properties.Resources.BFME1_PBarBG;

        public CustomProgressBar()
        {
            // Modify the ControlStyles flags
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rec = e.ClipRectangle;

            LinearGradientBrush brush = new(rec, Color.FromArgb(175, ForeColor), Color.FromArgb(175, BackColor), LinearGradientMode.Vertical);
            rec.Width = (int)(rec.Width * ((double)Value / Maximum));
            e.Graphics.FillRectangle(brush, 0, 0, rec.Width, rec.Height);

            // Set the Display text (Either a % amount or our custom text
            int percent = (int)(Value / (double)Maximum * 100);
            string? text = DisplayStyle == ProgressBarDisplayText.Percentage ? percent.ToString() + '%' : CustomText;

            using Font f = FontHelper.GetFont(0, 20);

            SizeF len = g.MeasureString(text, f);
            // Calculate the location of the text (the middle of progress bar)
            // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
            Point location = new(Convert.ToInt32(Width / 2 - len.Width / 2), Convert.ToInt32(Height / 2 - len.Height / 2) + 4);
            // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
            // Draw the custom text

            // Draw the text itself
            Color textColor = Color.FromArgb(192, 145, 69);
            g.DrawString(text, f, new SolidBrush(textColor), location.X, location.Y);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.DrawImage(_ProgressBarBackground, 0, 0, Width, Height);
            // Add a rectangle with 10% opacity to make the background a bit lighter
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(25, 0, 0, 0)), 0, 0, Width, Height);
        }
    }
}