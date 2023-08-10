namespace Helper.UserControls
{
    partial class PatchesButtonsBFME25
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
            LblPatchVersion = new CustomLabel();
            PibSelectedIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PibSelectedIcon).BeginInit();
            SuspendLayout();
            // 
            // LblPatchVersion
            // 
            LblPatchVersion.AutoSize = true;
            LblPatchVersion.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            LblPatchVersion.ImeMode = ImeMode.NoControl;
            LblPatchVersion.Location = new Point(0, 172);
            LblPatchVersion.Name = "LblPatchVersion";
            LblPatchVersion.OutlineForeColor = Color.Black;
            LblPatchVersion.OutlineWidth = 4F;
            LblPatchVersion.Size = new Size(100, 25);
            LblPatchVersion.TabIndex = 29;
            LblPatchVersion.Text = "Version 35";
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
            // PatchesButtonsBFME25
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = Properties.Resources.BFME25PatchModBG;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(LblPatchVersion);
            Controls.Add(PibSelectedIcon);
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            Name = "PatchesButtonsBFME25";
            Size = new Size(150, 200);
            ((System.ComponentModel.ISupportInitialize)PibSelectedIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomLabel LblPatchVersion;
        private PictureBox PibSelectedIcon;
    }
}
