namespace Helper.UserControls
{
    partial class Patch222Buttons
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
            SuspendLayout();
            // 
            // LblPatchVersion
            // 
            LblPatchVersion.AutoSize = true;
            LblPatchVersion.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            LblPatchVersion.ImeMode = ImeMode.NoControl;
            LblPatchVersion.Location = new Point(8, 143);
            LblPatchVersion.Name = "LblPatchVersion";
            LblPatchVersion.OutlineForeColor = Color.Black;
            LblPatchVersion.OutlineWidth = 4F;
            LblPatchVersion.Size = new Size(141, 37);
            LblPatchVersion.TabIndex = 29;
            LblPatchVersion.Text = "Version 35";
            // 
            // Patch222Buttons
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = Properties.Resources.BtnPatchSelection_222;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(LblPatchVersion);
            DoubleBuffered = true;
            Name = "Patch222Buttons";
            Size = new Size(150, 200);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox BtnPatchVersion;
        private CustomLabel LblPatchVersion;
    }
}
