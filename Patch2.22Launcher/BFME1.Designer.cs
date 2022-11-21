using System.Windows.Forms;

namespace PatchLauncher
{
    partial class BFME1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BFME1));
            this.BtnClose = new System.Windows.Forms.Button();
            this.wv2Patchnotes = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.BtnLaunch = new System.Windows.Forms.Button();
            this.PibHeader = new System.Windows.Forms.PictureBox();
            this.PiBYoutube = new System.Windows.Forms.PictureBox();
            this.PiBDiscord = new System.Windows.Forms.PictureBox();
            this.PiBModDB = new System.Windows.Forms.PictureBox();
            this.PiBThemeSwitcher = new System.Windows.Forms.PictureBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.BtnOptions = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wv2Patchnotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBYoutube)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBDiscord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBModDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBThemeSwitcher)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Black;
            this.BtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.ForeColor = System.Drawing.Color.Transparent;
            this.BtnClose.Location = new System.Drawing.Point(1321, 717);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(200, 51);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.TabStop = false;
            this.BtnClose.Text = "QUIT";
            this.BtnClose.UseMnemonic = false;
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.BtnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnClose_MouseDown);
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // wv2Patchnotes
            // 
            this.wv2Patchnotes.AllowExternalDrop = true;
            this.wv2Patchnotes.CreationProperties = null;
            this.wv2Patchnotes.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wv2Patchnotes.Location = new System.Drawing.Point(390, 132);
            this.wv2Patchnotes.Name = "wv2Patchnotes";
            this.wv2Patchnotes.Size = new System.Drawing.Size(1131, 537);
            this.wv2Patchnotes.Source = new System.Uri("https://docs.google.com/document/d/1eG95mVD_TYnvkfKEwcXaQvWS5AYbFIq4rDdXrifsKUw", System.UriKind.Absolute);
            this.wv2Patchnotes.TabIndex = 1;
            this.wv2Patchnotes.ZoomFactor = 1D;
            // 
            // BtnLaunch
            // 
            this.BtnLaunch.BackColor = System.Drawing.Color.Black;
            this.BtnLaunch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnLaunch.FlatAppearance.BorderSize = 0;
            this.BtnLaunch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnLaunch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLaunch.ForeColor = System.Drawing.Color.Transparent;
            this.BtnLaunch.Location = new System.Drawing.Point(12, 717);
            this.BtnLaunch.Name = "BtnLaunch";
            this.BtnLaunch.Size = new System.Drawing.Size(200, 51);
            this.BtnLaunch.TabIndex = 2;
            this.BtnLaunch.TabStop = false;
            this.BtnLaunch.Text = "LAUNCH";
            this.BtnLaunch.UseMnemonic = false;
            this.BtnLaunch.UseVisualStyleBackColor = false;
            this.BtnLaunch.Click += new System.EventHandler(this.BtnLaunch_Click);
            this.BtnLaunch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnLaunch_MouseDown);
            this.BtnLaunch.MouseEnter += new System.EventHandler(this.BtnLaunch_MouseEnter);
            this.BtnLaunch.MouseLeave += new System.EventHandler(this.BtnLaunch_MouseLeave);
            // 
            // PibHeader
            // 
            this.PibHeader.BackColor = System.Drawing.Color.Transparent;
            this.PibHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PibHeader.BackgroundImage")));
            this.PibHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PibHeader.ErrorImage = null;
            this.PibHeader.InitialImage = null;
            this.PibHeader.Location = new System.Drawing.Point(390, 0);
            this.PibHeader.Name = "PibHeader";
            this.PibHeader.Size = new System.Drawing.Size(775, 105);
            this.PibHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PibHeader.TabIndex = 5;
            this.PibHeader.TabStop = false;
            this.PibHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PibHeader_MouseDown);
            // 
            // PiBYoutube
            // 
            this.PiBYoutube.BackColor = System.Drawing.Color.Black;
            this.PiBYoutube.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBYoutube.Location = new System.Drawing.Point(635, 12);
            this.PiBYoutube.Name = "PiBYoutube";
            this.PiBYoutube.Size = new System.Drawing.Size(55, 55);
            this.PiBYoutube.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBYoutube.TabIndex = 6;
            this.PiBYoutube.TabStop = false;
            this.PiBYoutube.Click += new System.EventHandler(this.PiBYoutube_Click);
            // 
            // PiBDiscord
            // 
            this.PiBDiscord.BackColor = System.Drawing.Color.Black;
            this.PiBDiscord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBDiscord.Location = new System.Drawing.Point(696, 12);
            this.PiBDiscord.Name = "PiBDiscord";
            this.PiBDiscord.Size = new System.Drawing.Size(55, 55);
            this.PiBDiscord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBDiscord.TabIndex = 7;
            this.PiBDiscord.TabStop = false;
            this.PiBDiscord.Click += new System.EventHandler(this.PiBDiscord_Click);
            // 
            // PiBModDB
            // 
            this.PiBModDB.BackColor = System.Drawing.Color.Black;
            this.PiBModDB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBModDB.Location = new System.Drawing.Point(757, 12);
            this.PiBModDB.Name = "PiBModDB";
            this.PiBModDB.Size = new System.Drawing.Size(55, 55);
            this.PiBModDB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBModDB.TabIndex = 8;
            this.PiBModDB.TabStop = false;
            this.PiBModDB.Click += new System.EventHandler(this.PiBModDB_Click);
            // 
            // PiBThemeSwitcher
            // 
            this.PiBThemeSwitcher.BackColor = System.Drawing.Color.Black;
            this.PiBThemeSwitcher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBThemeSwitcher.Location = new System.Drawing.Point(865, 12);
            this.PiBThemeSwitcher.Name = "PiBThemeSwitcher";
            this.PiBThemeSwitcher.Size = new System.Drawing.Size(55, 55);
            this.PiBThemeSwitcher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBThemeSwitcher.TabIndex = 9;
            this.PiBThemeSwitcher.TabStop = false;
            this.PiBThemeSwitcher.Click += new System.EventHandler(this.PiBThemeSwitcher_Click);
            // 
            // ToolTip
            // 
            this.ToolTip.BackColor = System.Drawing.Color.Black;
            this.ToolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(145)))), ((int)(((byte)(69)))));
            this.ToolTip.OwnerDraw = true;
            this.ToolTip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.Tooltip_Draw);
            this.ToolTip.Popup += new System.Windows.Forms.PopupEventHandler(this.TooltipPopup);
            // 
            // BtnOptions
            // 
            this.BtnOptions.BackColor = System.Drawing.Color.Black;
            this.BtnOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnOptions.FlatAppearance.BorderSize = 0;
            this.BtnOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOptions.ForeColor = System.Drawing.Color.Transparent;
            this.BtnOptions.Location = new System.Drawing.Point(1115, 717);
            this.BtnOptions.Name = "BtnOptions";
            this.BtnOptions.Size = new System.Drawing.Size(200, 51);
            this.BtnOptions.TabIndex = 10;
            this.BtnOptions.TabStop = false;
            this.BtnOptions.Text = "OPTIONS";
            this.BtnOptions.UseMnemonic = false;
            this.BtnOptions.UseVisualStyleBackColor = false;
            this.BtnOptions.Click += new System.EventHandler(this.BtnOptions_Click);
            this.BtnOptions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnOptions_MouseDown);
            this.BtnOptions.MouseEnter += new System.EventHandler(this.BtnOptions_MouseEnter);
            this.BtnOptions.MouseLeave += new System.EventHandler(this.BtnOptions_MouseLeave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(218, 717);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 51);
            this.button1.TabIndex = 11;
            this.button1.TabStop = false;
            this.button1.Text = "UPDATE";
            this.button1.UseMnemonic = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BFME1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1533, 778);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.wv2Patchnotes);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnOptions);
            this.Controls.Add(this.BtnLaunch);
            this.Controls.Add(this.PiBThemeSwitcher);
            this.Controls.Add(this.PiBModDB);
            this.Controls.Add(this.PiBDiscord);
            this.Controls.Add(this.PiBYoutube);
            this.Controls.Add(this.PibHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MdiChildrenMinimizedAnchorBottom = false;
            this.MinimizeBox = false;
            this.Name = "BFME1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bfme 2.22 Launcher";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.wv2Patchnotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBYoutube)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBDiscord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBModDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBThemeSwitcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button BtnClose;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv2Patchnotes;
        private Button BtnLaunch;
        private PictureBox PibHeader;
        private PictureBox PiBYoutube;
        private PictureBox PiBDiscord;
        private PictureBox PiBModDB;
        private PictureBox PiBThemeSwitcher;
        private ToolTip ToolTip;
        private Button BtnOptions;
        private Button button1;
    }
}