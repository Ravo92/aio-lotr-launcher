using System.Drawing;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class RepairLogConsole : Form
    {
        public RepairLogConsole()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
        }

        private void RepairLogConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
