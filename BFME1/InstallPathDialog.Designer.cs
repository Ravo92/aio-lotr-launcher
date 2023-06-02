namespace PatchLauncher
{
    partial class InstallPathDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallPathDialog));
            TxtInstallPath = new System.Windows.Forms.TextBox();
            LblChooseDir = new System.Windows.Forms.Label();
            BtnChoose = new System.Windows.Forms.Button();
            BtnAccept = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // TxtInstallPath
            // 
            resources.ApplyResources(TxtInstallPath, "TxtInstallPath");
            TxtInstallPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            TxtInstallPath.Name = "TxtInstallPath";
            // 
            // LblChooseDir
            // 
            resources.ApplyResources(LblChooseDir, "LblChooseDir");
            LblChooseDir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            LblChooseDir.Name = "LblChooseDir";
            // 
            // BtnChoose
            // 
            resources.ApplyResources(BtnChoose, "BtnChoose");
            BtnChoose.BackColor = System.Drawing.SystemColors.ActiveCaption;
            BtnChoose.FlatAppearance.BorderSize = 0;
            BtnChoose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnChoose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnChoose.ForeColor = System.Drawing.Color.Transparent;
            BtnChoose.Name = "BtnChoose";
            BtnChoose.TabStop = false;
            BtnChoose.UseMnemonic = false;
            BtnChoose.UseVisualStyleBackColor = false;
            BtnChoose.Click += BtnChoose_Click;
            BtnChoose.MouseDown += BtnChoose_MouseDown;
            BtnChoose.MouseEnter += BtnChoose_MouseEnter;
            BtnChoose.MouseLeave += BtnChoose_MouseLeave;
            // 
            // BtnAccept
            // 
            resources.ApplyResources(BtnAccept, "BtnAccept");
            BtnAccept.BackColor = System.Drawing.SystemColors.ActiveCaption;
            BtnAccept.FlatAppearance.BorderSize = 0;
            BtnAccept.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnAccept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnAccept.ForeColor = System.Drawing.Color.Transparent;
            BtnAccept.Name = "BtnAccept";
            BtnAccept.TabStop = false;
            BtnAccept.UseMnemonic = false;
            BtnAccept.UseVisualStyleBackColor = false;
            BtnAccept.Click += BtnAccept_Click;
            BtnAccept.MouseDown += BtnAccept_MouseDown;
            BtnAccept.MouseEnter += BtnAccept_MouseEnter;
            BtnAccept.MouseLeave += BtnAccept_MouseLeave;
            // 
            // InstallPathDialog
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            Controls.Add(BtnAccept);
            Controls.Add(BtnChoose);
            Controls.Add(LblChooseDir);
            Controls.Add(TxtInstallPath);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "InstallPathDialog";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox TxtInstallPath;
        private System.Windows.Forms.Label LblChooseDir;
        private System.Windows.Forms.Button BtnChoose;
        private System.Windows.Forms.Button BtnAccept;
    }
}