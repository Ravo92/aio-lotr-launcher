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
            Process _process = new();
            _process.StartInfo.FileName = "PatchLauncherBFME1.exe";
            _process.StartInfo.Arguments = "-official";
            _process.StartInfo.WorkingDirectory = Application.StartupPath;
            _process.Start();
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
