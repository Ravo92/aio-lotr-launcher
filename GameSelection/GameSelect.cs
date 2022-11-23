using System.Diagnostics;

namespace PatchLauncher
{
    public partial class GameSelect : Form
    {
        public GameSelect()
        {
            InitializeComponent();
        }

        private void BtnBFME1_Click(object sender, EventArgs e)
        {
            Process.Start("PatchLauncherBFME1");
            Dispose();
        }

        private void BtnBFME2_Click(object sender, EventArgs e)
        {

        }

        private void BtnBFME2EP1_Click(object sender, EventArgs e)
        {

        }
    }
}
