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
            this.TxtInstallPath = new System.Windows.Forms.TextBox();
            this.LblChooseDir = new System.Windows.Forms.Label();
            this.BtnChoose = new System.Windows.Forms.Button();
            this.BtnAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtInstallPath
            // 
            this.TxtInstallPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtInstallPath.Location = new System.Drawing.Point(12, 48);
            this.TxtInstallPath.Name = "TxtInstallPath";
            this.TxtInstallPath.Size = new System.Drawing.Size(537, 16);
            this.TxtInstallPath.TabIndex = 0;
            this.TxtInstallPath.WordWrap = false;
            // 
            // LblChooseDir
            // 
            this.LblChooseDir.AutoSize = true;
            this.LblChooseDir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.LblChooseDir.Location = new System.Drawing.Point(12, 4);
            this.LblChooseDir.Name = "LblChooseDir";
            this.LblChooseDir.Size = new System.Drawing.Size(40, 15);
            this.LblChooseDir.TabIndex = 14;
            this.LblChooseDir.Text = "LABEL";
            // 
            // BtnChoose
            // 
            this.BtnChoose.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnChoose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnChoose.FlatAppearance.BorderSize = 0;
            this.BtnChoose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnChoose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnChoose.ForeColor = System.Drawing.Color.Transparent;
            this.BtnChoose.Location = new System.Drawing.Point(555, 30);
            this.BtnChoose.Name = "BtnChoose";
            this.BtnChoose.Size = new System.Drawing.Size(200, 51);
            this.BtnChoose.TabIndex = 15;
            this.BtnChoose.TabStop = false;
            this.BtnChoose.Text = "CHOOSE...";
            this.BtnChoose.UseMnemonic = false;
            this.BtnChoose.UseVisualStyleBackColor = false;
            this.BtnChoose.Click += new System.EventHandler(this.BtnChoose_Click);
            this.BtnChoose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnChoose_MouseDown);
            this.BtnChoose.MouseEnter += new System.EventHandler(this.BtnChoose_MouseEnter);
            this.BtnChoose.MouseLeave += new System.EventHandler(this.BtnChoose_MouseLeave);
            // 
            // BtnAccept
            // 
            this.BtnAccept.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnAccept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAccept.FlatAppearance.BorderSize = 0;
            this.BtnAccept.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnAccept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAccept.ForeColor = System.Drawing.Color.Transparent;
            this.BtnAccept.Location = new System.Drawing.Point(761, 30);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(200, 51);
            this.BtnAccept.TabIndex = 16;
            this.BtnAccept.TabStop = false;
            this.BtnAccept.Text = "ACCEPT";
            this.BtnAccept.UseMnemonic = false;
            this.BtnAccept.UseVisualStyleBackColor = false;
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            this.BtnAccept.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnAccept_MouseDown);
            this.BtnAccept.MouseEnter += new System.EventHandler(this.BtnAccept_MouseEnter);
            this.BtnAccept.MouseLeave += new System.EventHandler(this.BtnAccept_MouseLeave);
            // 
            // InstallPathDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(969, 93);
            this.Controls.Add(this.BtnAccept);
            this.Controls.Add(this.BtnChoose);
            this.Controls.Add(this.LblChooseDir);
            this.Controls.Add(this.TxtInstallPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InstallPathDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Installation Directory";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtInstallPath;
        private System.Windows.Forms.Label LblChooseDir;
        private System.Windows.Forms.Button BtnChoose;
        private System.Windows.Forms.Button BtnAccept;
    }
}