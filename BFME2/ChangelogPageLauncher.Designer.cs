namespace PatchLauncher
{
    partial class ChangelogPageLauncher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangelogPageLauncher));
            Wv2ChangelogLauncher = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)Wv2ChangelogLauncher).BeginInit();
            SuspendLayout();
            // 
            // Wv2ChangelogLauncher
            // 
            Wv2ChangelogLauncher.AllowExternalDrop = true;
            Wv2ChangelogLauncher.CreationProperties = null;
            Wv2ChangelogLauncher.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(Wv2ChangelogLauncher, "Wv2ChangelogLauncher");
            Wv2ChangelogLauncher.Name = "Wv2ChangelogLauncher";
            Wv2ChangelogLauncher.Source = new System.Uri("https://ravo92.github.io/changelogpagelauncher/index.html", System.UriKind.Absolute);
            Wv2ChangelogLauncher.ZoomFactor = 1D;
            // 
            // ChangelogPageLauncher
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(Wv2ChangelogLauncher);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangelogPageLauncher";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            KeyDown += AboutForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)Wv2ChangelogLauncher).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2ChangelogLauncher;
    }
}
