namespace Helper.UserControls
{
    public partial class Patch222Buttons : UserControl
    {
        public Patch222Buttons()
        {
            InitializeComponent();

            // label-Styles
            LblPatchVersion.Font = FontHelper.GetFont(0, 10); ;
            LblPatchVersion.ForeColor = Color.FromArgb(192, 145, 69);
            LblPatchVersion.BackColor = Color.Transparent;
        }
    }
}
