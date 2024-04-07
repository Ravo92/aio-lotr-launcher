using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class OnlineMode : Form
    {
        public OnlineMode()
        {
            InitializeComponent();

            OnlineMenu.Load(BFMECompetetiveArena_OnlineKit.Data.BfmeGame.BFME2);
        }

        private void OnlineMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to quit the online mode? You will be logged out.", "Quit Online Mode?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                OnlineMenu.Unload();
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
