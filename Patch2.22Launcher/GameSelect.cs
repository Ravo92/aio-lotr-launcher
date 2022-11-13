using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class GameSelect : Form
    {
        public GameSelect()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            Focus();
        }

        private void BtnBFME1_Click(object sender, EventArgs e)
        {
            Hide();
            BFME1 _bFME1 = new();
            _bFME1.Show();
        }

        private void BtnBFME2_Click(object sender, EventArgs e)
        {

        }

        private void BtnBFME2EP1_Click(object sender, EventArgs e)
        {

        }
    }
}
