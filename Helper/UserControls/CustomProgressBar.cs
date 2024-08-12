namespace Helper.UserControls
{
    public enum ProgressBarDisplayText
    {
        Percentage,
        CustomText
    }

    public class CustomProgressBar : ProgressBar
    {
        private Color textColor = Color.FromArgb(192, 145, 69);

        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The color of the text displayed on the progress bar.")]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                Invalidate();
            }
        }

        public ProgressBarDisplayText DisplayStyle { get; set; }
        public string? CustomText { get; set; }

        public CustomProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rec = e.ClipRectangle;

            using System.Drawing.Drawing2D.LinearGradientBrush brush = new(rec, Color.FromArgb(175, ForeColor), Color.FromArgb(175, BackColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            rec.Width = (int)(rec.Width * ((double)Value / Maximum));
            e.Graphics.FillRectangle(brush, 0, 0, rec.Width, rec.Height);

            int percent = (int)(Value / (double)Maximum * 100);
            string? text = DisplayStyle == ProgressBarDisplayText.Percentage ? percent.ToString() + '%' : CustomText;

            using Font f = FontHelper.GetFont(0, 20);

            SizeF len = g.MeasureString(text, f);
            Point location = new(Convert.ToInt32(Width / 2 - len.Width / 2), Convert.ToInt32(Height / 2 - len.Height / 2) + 4);

            using SolidBrush textBrush = new(textColor);
            g.DrawString(text, f, textBrush, location.X, location.Y);
        }
    }
}