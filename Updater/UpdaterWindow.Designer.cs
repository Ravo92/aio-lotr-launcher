namespace PatchLauncher
{
    partial class UpdaterWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdaterWindow));
            this.PBarLoading = new System.Windows.Forms.ProgressBar();
            this.TmrCowndown = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // PBarLoading
            // 
            this.PBarLoading.Location = new System.Drawing.Point(365, 12);
            this.PBarLoading.Name = "PBarLoading";
            this.PBarLoading.Size = new System.Drawing.Size(121, 36);
            this.PBarLoading.TabIndex = 0;
            this.PBarLoading.UseWaitCursor = true;
            // 
            // TmrCowndown
            // 
            this.TmrCowndown.Enabled = true;
            this.TmrCowndown.Interval = 200;
            this.TmrCowndown.Tick += new System.EventHandler(this.TmrCowndown_Tick);
            // 
            // UpdaterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(498, 60);
            this.Controls.Add(this.PBarLoading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdaterWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar PBarLoading;
        private System.Windows.Forms.Timer TmrCowndown;
    }
}