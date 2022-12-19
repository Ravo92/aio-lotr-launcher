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
            this.BtnOpenAppDataFolder = new System.Windows.Forms.Button();
            this.BtnGameInstallFolder = new System.Windows.Forms.Button();
            this.BtnLauncherFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnOpenAppDataFolder
            // 
            this.BtnOpenAppDataFolder.BackColor = System.Drawing.Color.Black;
            this.BtnOpenAppDataFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOpenAppDataFolder.FlatAppearance.BorderSize = 0;
            this.BtnOpenAppDataFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnOpenAppDataFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnOpenAppDataFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOpenAppDataFolder.ForeColor = System.Drawing.Color.Transparent;
            this.BtnOpenAppDataFolder.Location = new System.Drawing.Point(12, 12);
            this.BtnOpenAppDataFolder.Name = "BtnOpenAppDataFolder";
            this.BtnOpenAppDataFolder.Size = new System.Drawing.Size(230, 51);
            this.BtnOpenAppDataFolder.TabIndex = 43;
            this.BtnOpenAppDataFolder.TabStop = false;
            this.BtnOpenAppDataFolder.Text = "APPDATA FOLDER";
            this.BtnOpenAppDataFolder.UseMnemonic = false;
            this.BtnOpenAppDataFolder.UseVisualStyleBackColor = false;
            this.BtnOpenAppDataFolder.Click += new System.EventHandler(this.BtnOpenAppDataFolder_Click);
            this.BtnOpenAppDataFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnOpenAppDataFolder_MouseDown);
            this.BtnOpenAppDataFolder.MouseEnter += new System.EventHandler(this.BtnOpenAppDataFolder_MouseEnter);
            this.BtnOpenAppDataFolder.MouseLeave += new System.EventHandler(this.BtnOpenAppDataFolder_MouseLeave);
            // 
            // BtnGameInstallFolder
            // 
            this.BtnGameInstallFolder.BackColor = System.Drawing.Color.Black;
            this.BtnGameInstallFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnGameInstallFolder.FlatAppearance.BorderSize = 0;
            this.BtnGameInstallFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnGameInstallFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnGameInstallFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGameInstallFolder.ForeColor = System.Drawing.Color.Transparent;
            this.BtnGameInstallFolder.Location = new System.Drawing.Point(258, 12);
            this.BtnGameInstallFolder.Name = "BtnGameInstallFolder";
            this.BtnGameInstallFolder.Size = new System.Drawing.Size(230, 51);
            this.BtnGameInstallFolder.TabIndex = 44;
            this.BtnGameInstallFolder.TabStop = false;
            this.BtnGameInstallFolder.Text = "GAME FOLDER";
            this.BtnGameInstallFolder.UseMnemonic = false;
            this.BtnGameInstallFolder.UseVisualStyleBackColor = false;
            this.BtnGameInstallFolder.Click += new System.EventHandler(this.BtnGameInstallFolder_Click);
            this.BtnGameInstallFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnGameInstallFolder_MouseDown);
            this.BtnGameInstallFolder.MouseEnter += new System.EventHandler(this.BtnGameInstallFolder_MouseEnter);
            this.BtnGameInstallFolder.MouseLeave += new System.EventHandler(this.BtnGameInstallFolder_MouseLeave);
            // 
            // BtnLauncherFolder
            // 
            this.BtnLauncherFolder.BackColor = System.Drawing.Color.Black;
            this.BtnLauncherFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnLauncherFolder.FlatAppearance.BorderSize = 0;
            this.BtnLauncherFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnLauncherFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnLauncherFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLauncherFolder.ForeColor = System.Drawing.Color.Transparent;
            this.BtnLauncherFolder.Location = new System.Drawing.Point(504, 12);
            this.BtnLauncherFolder.Name = "BtnLauncherFolder";
            this.BtnLauncherFolder.Size = new System.Drawing.Size(230, 51);
            this.BtnLauncherFolder.TabIndex = 45;
            this.BtnLauncherFolder.TabStop = false;
            this.BtnLauncherFolder.Text = "LAUNCHER FOLDER";
            this.BtnLauncherFolder.UseMnemonic = false;
            this.BtnLauncherFolder.UseVisualStyleBackColor = false;
            this.BtnLauncherFolder.Click += new System.EventHandler(this.BtnLauncherFolder_Click);
            this.BtnLauncherFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnLauncherFolder_MouseDown);
            this.BtnLauncherFolder.MouseEnter += new System.EventHandler(this.BtnLauncherFolder_MouseEnter);
            this.BtnLauncherFolder.MouseLeave += new System.EventHandler(this.BtnLauncherFolder_MouseLeave);
            // 
            // Advanced
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(746, 77);
            this.Controls.Add(this.BtnLauncherFolder);
            this.Controls.Add(this.BtnGameInstallFolder);
            this.Controls.Add(this.BtnOpenAppDataFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Advanced";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Easy Access Options.";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Advanced_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnOpenAppDataFolder;
        private System.Windows.Forms.Button BtnGameInstallFolder;
        private System.Windows.Forms.Button BtnLauncherFolder;
    }
}