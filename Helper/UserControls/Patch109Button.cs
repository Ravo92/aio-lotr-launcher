namespace Helper.UserControls
{
    public partial class Patch109Button : UserControl
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

        public Patch109Button()
        {
            InitializeComponent();
        }
    }
}