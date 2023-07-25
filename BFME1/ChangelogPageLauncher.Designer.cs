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
            Wv2Messages = new Microsoft.Web.WebView2.WinForms.WebView2();
            BtnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)Wv2Messages).BeginInit();
            SuspendLayout();
            // 
            // Wv2Messages
            // 
            Wv2Messages.AllowExternalDrop = true;
            Wv2Messages.CreationProperties = null;
            Wv2Messages.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(Wv2Messages, "Wv2Messages");
            Wv2Messages.Name = "Wv2Messages";
            Wv2Messages.Source = new System.Uri("https://ravo92.github.io/messagespage/index.html", System.UriKind.Absolute);
            Wv2Messages.ZoomFactor = 1D;
            // 
            // BtnClose
            // 
            BtnClose.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(BtnClose, "BtnClose");
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
            // MessagesForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(BtnClose);
            Controls.Add(Wv2Messages);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MessagesForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            KeyDown += AboutForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)Wv2Messages).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2Messages;
        private System.Windows.Forms.Button BtnClose;
    }
}
