using BFMECompetetiveArena_OnlineKitForms;

namespace PatchLauncher
{
    partial class OnlineMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineMode));
            OnlineMenu = new OnlineMenu();
            SuspendLayout();
            // 
            // OnlineMenu
            // 
            OnlineMenu.AccessToken = "*deleted*";
            OnlineMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            OnlineMenu.Location = new System.Drawing.Point(0, 0);
            OnlineMenu.Name = "OnlineMenu";
            OnlineMenu.Size = new System.Drawing.Size(1264, 681);
            OnlineMenu.TabIndex = 0;
            OnlineMenu.UpdateBranch = "main";
            // 
            // OnlineMode
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1264, 681);
            Controls.Add(OnlineMenu);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "OnlineMode";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Online Mode";
            FormClosing += OnlineMode_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private OnlineMenu OnlineMenu;
    }
}