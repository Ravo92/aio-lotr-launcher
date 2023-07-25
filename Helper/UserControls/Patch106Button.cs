namespace Helper.UserControls
{
    public partial class Patch106Button : UserControl
    {
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

        public Patch106Button()
        {
            InitializeComponent();
        }
    }
}