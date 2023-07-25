namespace Helper.UserControls
{
    public partial class Patch222Buttons : UserControl
    {
        public string LabelTextPatchVersion
        {
            get
            {
                return LblPatchVersion.Text;
            }
            set
            {
                LblPatchVersion.Text = value;
            }
        }

        public bool SelectedIconVisible
        {
            get
            {
                return PibSelectedIcon.Visible;
            }
            set
            {
                PibSelectedIcon.Visible = value;
            }
        }

        public Patch222Buttons()
        {
            InitializeComponent();

            // label-Styles
            LblPatchVersion.Font = FontHelper.GetFont(0, 17);
            LblPatchVersion.ForeColor = Color.FromArgb(192, 145, 69);
            LblPatchVersion.BackColor = Color.Transparent;
            LblPatchVersion.BorderStyle = BorderStyle.None;
            LblPatchVersion.OutlineWidth = 6;
        }
    }
}