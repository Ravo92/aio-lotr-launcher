namespace PatchLauncher
{
    partial class ChangelogPagePatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangelogPagePatch));
            Wv2ChangelogPatch = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)Wv2ChangelogPatch).BeginInit();
            SuspendLayout();
            // 
            // Wv2ChangelogPatch
            // 
            Wv2ChangelogPatch.AllowExternalDrop = true;
            Wv2ChangelogPatch.CreationProperties = null;
            Wv2ChangelogPatch.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(Wv2ChangelogPatch, "Wv2ChangelogPatch");
            Wv2ChangelogPatch.Name = "Wv2ChangelogPatch";
            Wv2ChangelogPatch.Source = new System.Uri("https://ravo92.github.io/changelogpage/index.html", System.UriKind.Absolute);
            Wv2ChangelogPatch.ZoomFactor = 1D;
            // 
            // ChangelogPagePatch
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(Wv2ChangelogPatch);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangelogPagePatch";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            KeyDown += AboutForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)Wv2ChangelogPatch).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2ChangelogPatch;
    }
}
