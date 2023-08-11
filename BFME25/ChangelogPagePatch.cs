using System.Drawing;
using System.Windows.Forms;

namespace PatchLauncher
{
    partial class ChangelogPagePatch : Form
    {
        public ChangelogPagePatch()
        {
            InitializeComponent();
            KeyPreview = true;
            BackColor = Color.FromArgb(18, 18, 18);
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
