using Helper;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    partial class CreditsForm : Form
    {
        public CreditsForm()
        {
            InitializeComponent();

            KeyPreview = true;

#warning C) CHANGE THIS ONE LOCAL FILE TO A REMOTE FILE!!!
            Uri _Wv2CreditsUri = new("file:///" + Path.Combine(Application.StartupPath, ConstStrings.C_HTMLFOLDER_NAME) + "/credits.html");
            Wv2Credits.Source = _Wv2CreditsUri;

            BackColor = Color.FromArgb(18, 18, 18);

            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.BackColor = Color.FromArgb(18, 18, 18);
            BtnClose.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnClose.Font = FontHelper.GetFont(0, 16); ;
            BtnClose.ForeColor = Color.FromArgb(168, 190, 98);
        }

        private void BtnOptions_Click(object sender, EventArgs e)
        {
            Wv2Credits.Dispose();
            Close();
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_NEUTR;
            BtnClose.ForeColor = Color.FromArgb(168, 190, 98);
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_HOVER;
            BtnClose.ForeColor = Color.FromArgb(24, 63, 20);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.C_BFME2_BUTTONIMAGE_CLICK;
            BtnClose.ForeColor = Color.FromArgb(168, 190, 98);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
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
