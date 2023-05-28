using System.Drawing;
using System.Drawing.Drawing2D;

namespace Helper
{
    public enum ProgressBarDisplayText
    {
        Percentage,
        CustomText
    }

    public class CustomProgressBar : ProgressBar
    {
        //Property to set to decide whether to print a % or Text
        public ProgressBarDisplayText DisplayStyle { get; set; }

        //Property to hold the custom text
        public string? CustomText { get; set; }

        public CustomProgressBar()
        {
            // Modify the ControlStyles flags
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Rectangle rec = new(0, 0, Width, Height);
            //double scaleFactor = (Value - (double)Minimum) / (Maximum - (double)Minimum);

            //if (ProgressBarRenderer.IsSupported)
            //    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);

            //rec.Width = (int)((rec.Width * scaleFactor) - 4);
            //rec.Height -= 4;
            //e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);

            Graphics g = e.Graphics;
            Rectangle rec = e.ClipRectangle;

            LinearGradientBrush brush = new(rec, ForeColor, BackColor, LinearGradientMode.Vertical);

            rec.Width = (int)(rec.Width * ((double)Value / Maximum));
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);

            e.Graphics.FillRectangle(brush, 0, 0, rec.Width, rec.Height);

            // Set the Display text (Either a % amount or our custom text
            int percent = (int)(Value / (double)Maximum * 100);
            string? text = DisplayStyle == ProgressBarDisplayText.Percentage ? percent.ToString() + '%' : CustomText;

            using Font f = FontHelper.GetFont(0, 20);

            SizeF len = g.MeasureString(text, f);
            // Calculate the location of the text (the middle of progress bar)
            // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
            Point location = new(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
            // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
            // Draw the custom text
            g.DrawString(text, f, Brushes.Black, location);
        }
    }
}