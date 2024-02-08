namespace Helper.UserControls
{
    partial class Patch109Button
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PibSelectedIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PibSelectedIcon).BeginInit();
            SuspendLayout();
            // 
            // PibSelectedIcon
            // 
            PibSelectedIcon.BackColor = Color.Transparent;
            PibSelectedIcon.BackgroundImageLayout = ImageLayout.None;
            PibSelectedIcon.Image = Properties.Resources.PatchModSelected;
            PibSelectedIcon.Location = new Point(118, 168);
            PibSelectedIcon.Name = "PibSelectedIcon";
            PibSelectedIcon.Size = new Size(32, 32);
            PibSelectedIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            PibSelectedIcon.TabIndex = 30;
            PibSelectedIcon.TabStop = false;
            PibSelectedIcon.Visible = false;
            // 
            // Patch109Button
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = Properties.Resources.BFME1PatchModBG109;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(PibSelectedIcon);
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            Name = "Patch109Button";
            Size = new Size(150, 200);
            ((System.ComponentModel.ISupportInitialize)PibSelectedIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PibSelectedIcon;
    }
}
