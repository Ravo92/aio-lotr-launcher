namespace PatchLauncher
{
    partial class Advanced
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Advanced));
            BtnOpenAppDataFolder = new System.Windows.Forms.Button();
            BtnGameInstallFolder = new System.Windows.Forms.Button();
            BtnLauncherFolder = new System.Windows.Forms.Button();
            BtnRepair = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // BtnOpenAppDataFolder
            // 
            resources.ApplyResources(BtnOpenAppDataFolder, "BtnOpenAppDataFolder");
            BtnOpenAppDataFolder.BackColor = System.Drawing.Color.Black;
            BtnOpenAppDataFolder.FlatAppearance.BorderSize = 0;
            BtnOpenAppDataFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnOpenAppDataFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnOpenAppDataFolder.ForeColor = System.Drawing.Color.Transparent;
            BtnOpenAppDataFolder.Name = "BtnOpenAppDataFolder";
            BtnOpenAppDataFolder.TabStop = false;
            BtnOpenAppDataFolder.UseMnemonic = false;
            BtnOpenAppDataFolder.UseVisualStyleBackColor = false;
            BtnOpenAppDataFolder.Click += BtnOpenAppDataFolder_Click;
            BtnOpenAppDataFolder.MouseDown += BtnOpenAppDataFolder_MouseDown;
            BtnOpenAppDataFolder.MouseEnter += BtnOpenAppDataFolder_MouseEnter;
            BtnOpenAppDataFolder.MouseLeave += BtnOpenAppDataFolder_MouseLeave;
            // 
            // BtnGameInstallFolder
            // 
            resources.ApplyResources(BtnGameInstallFolder, "BtnGameInstallFolder");
            BtnGameInstallFolder.BackColor = System.Drawing.Color.Black;
            BtnGameInstallFolder.FlatAppearance.BorderSize = 0;
            BtnGameInstallFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnGameInstallFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnGameInstallFolder.ForeColor = System.Drawing.Color.Transparent;
            BtnGameInstallFolder.Name = "BtnGameInstallFolder";
            BtnGameInstallFolder.TabStop = false;
            BtnGameInstallFolder.UseMnemonic = false;
            BtnGameInstallFolder.UseVisualStyleBackColor = false;
            BtnGameInstallFolder.Click += BtnGameInstallFolder_Click;
            BtnGameInstallFolder.MouseDown += BtnGameInstallFolder_MouseDown;
            BtnGameInstallFolder.MouseEnter += BtnGameInstallFolder_MouseEnter;
            BtnGameInstallFolder.MouseLeave += BtnGameInstallFolder_MouseLeave;
            // 
            // BtnLauncherFolder
            // 
            resources.ApplyResources(BtnLauncherFolder, "BtnLauncherFolder");
            BtnLauncherFolder.BackColor = System.Drawing.Color.Black;
            BtnLauncherFolder.FlatAppearance.BorderSize = 0;
            BtnLauncherFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnLauncherFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnLauncherFolder.ForeColor = System.Drawing.Color.Transparent;
            BtnLauncherFolder.Name = "BtnLauncherFolder";
            BtnLauncherFolder.TabStop = false;
            BtnLauncherFolder.UseMnemonic = false;
            BtnLauncherFolder.UseVisualStyleBackColor = false;
            BtnLauncherFolder.Click += BtnLauncherFolder_Click;
            BtnLauncherFolder.MouseDown += BtnLauncherFolder_MouseDown;
            BtnLauncherFolder.MouseEnter += BtnLauncherFolder_MouseEnter;
            BtnLauncherFolder.MouseLeave += BtnLauncherFolder_MouseLeave;
            // 
            // BtnRepair
            // 
            resources.ApplyResources(BtnRepair, "BtnRepair");
            BtnRepair.BackColor = System.Drawing.Color.Black;
            BtnRepair.FlatAppearance.BorderSize = 0;
            BtnRepair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnRepair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnRepair.ForeColor = System.Drawing.Color.Transparent;
            BtnRepair.Name = "BtnRepair";
            BtnRepair.TabStop = false;
            BtnRepair.UseMnemonic = false;
            BtnRepair.UseVisualStyleBackColor = false;
            BtnRepair.Click += BtnRepair_Click;
            BtnRepair.MouseDown += BtnRepair_MouseDown;
            BtnRepair.MouseEnter += BtnRepair_MouseEnter;
            BtnRepair.MouseLeave += BtnRepair_MouseLeave;
            // 
            // Advanced
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(BtnRepair);
            Controls.Add(BtnLauncherFolder);
            Controls.Add(BtnGameInstallFolder);
            Controls.Add(BtnOpenAppDataFolder);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "Advanced";
            KeyDown += Advanced_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button BtnOpenAppDataFolder;
        private System.Windows.Forms.Button BtnGameInstallFolder;
        private System.Windows.Forms.Button BtnLauncherFolder;
        private System.Windows.Forms.Button BtnRepair;
    }
}