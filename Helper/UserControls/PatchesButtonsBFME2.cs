namespace Helper.UserControls
{
    public partial class PatchesButtonsBFME2 : UserControl
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

        public PatchesButtonsBFME2()
        {
            InitializeComponent();

            // label-Styles
            LblPatchVersion.Font = FontHelper.GetFont(0, 17);
            LblPatchVersion.ForeColor = Color.FromArgb(168, 190, 98);
            LblPatchVersion.BackColor = Color.Transparent;
            LblPatchVersion.BorderStyle = BorderStyle.None;
            LblPatchVersion.OutlineWidth = 6;
        }
    }
}