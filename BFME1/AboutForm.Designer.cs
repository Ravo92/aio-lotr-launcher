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
            Wv2Credits = new Microsoft.Web.WebView2.WinForms.WebView2();
            BtnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)Wv2Credits).BeginInit();
            SuspendLayout();
            // 
            // Wv2Credits
            // 
            Wv2Credits.AllowExternalDrop = true;
            Wv2Credits.CreationProperties = null;
            Wv2Credits.DefaultBackgroundColor = System.Drawing.Color.White;
            Wv2Credits.Location = new System.Drawing.Point(10, 5);
            Wv2Credits.Name = "Wv2Credits";
            Wv2Credits.Size = new System.Drawing.Size(1240, 495);
            Wv2Credits.Source = new System.Uri("file:///Tools/credits.html", System.UriKind.Absolute);
            Wv2Credits.TabIndex = 0;
            Wv2Credits.ZoomFactor = 1D;
            // 
            // BtnClose
            // 
            BtnClose.BackColor = System.Drawing.Color.Black;
            BtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnClose.ForeColor = System.Drawing.Color.Transparent;
            BtnClose.Location = new System.Drawing.Point(1040, 440);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new System.Drawing.Size(200, 51);
            BtnClose.TabIndex = 11;
            BtnClose.TabStop = false;
            BtnClose.Text = "CLOSE";
            BtnClose.UseMnemonic = false;
            BtnClose.UseVisualStyleBackColor = false;
            BtnClose.Click += BtnOptions_Click;
            BtnClose.MouseDown += BtnClose_MouseDown;
            BtnClose.MouseEnter += BtnClose_MouseEnter;
            BtnClose.MouseLeave += BtnClose_MouseLeave;
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1259, 511);
            Controls.Add(BtnClose);
            Controls.Add(Wv2Credits);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            Padding = new System.Windows.Forms.Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)Wv2Credits).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2Credits;
        private System.Windows.Forms.Button BtnClose;
    }
}
