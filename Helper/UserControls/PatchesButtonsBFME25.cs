namespace Helper.UserControls
{
    public partial class PatchesButtonsBFME25 : UserControl
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

        public PatchesButtonsBFME25()
        {
            InitializeComponent();

            // label-Styles
            LblPatchVersion.Font = FontHelper.GetFont(0, 17);
            LblPatchVersion.ForeColor = Color.FromArgb(114, 153, 169);
            LblPatchVersion.BackColor = Color.Transparent;
            LblPatchVersion.BorderStyle = BorderStyle.None;
            LblPatchVersion.OutlineWidth = 6;
        }
    }
}