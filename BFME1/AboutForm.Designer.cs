namespace PatchLauncher
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            Wv2Credits = new Microsoft.Web.WebView2.WinForms.WebView2();
            BtnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)Wv2Credits).BeginInit();
            SuspendLayout();
            // 
            // Wv2Credits
            // 
            resources.ApplyResources(Wv2Credits, "Wv2Credits");
            Wv2Credits.AllowExternalDrop = true;
            Wv2Credits.CreationProperties = null;
            Wv2Credits.DefaultBackgroundColor = System.Drawing.Color.White;
            Wv2Credits.Name = "Wv2Credits";
            Wv2Credits.Source = new System.Uri("https://www.google.com", System.UriKind.Absolute);
            Wv2Credits.ZoomFactor = 1D;
            // 
            // BtnClose
            // 
            resources.ApplyResources(BtnClose, "BtnClose");
            BtnClose.BackColor = System.Drawing.Color.Black;
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnClose.ForeColor = System.Drawing.Color.Transparent;
            BtnClose.Name = "BtnClose";
            BtnClose.TabStop = false;
            BtnClose.UseMnemonic = false;
            BtnClose.UseVisualStyleBackColor = false;
            BtnClose.Click += BtnOptions_Click;
            BtnClose.MouseDown += BtnClose_MouseDown;
            BtnClose.MouseEnter += BtnClose_MouseEnter;
            BtnClose.MouseLeave += BtnClose_MouseLeave;
            // 
            // AboutForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(BtnClose);
            Controls.Add(Wv2Credits);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)Wv2Credits).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2Credits;
        private System.Windows.Forms.Button BtnClose;
    }
}
