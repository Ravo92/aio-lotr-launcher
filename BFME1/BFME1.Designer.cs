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
            this.Wv2Patchnotes = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.BtnLaunch = new System.Windows.Forms.Button();
            this.PibHeader = new System.Windows.Forms.PictureBox();
            this.PiBYoutube = new System.Windows.Forms.PictureBox();
            this.PiBDiscord = new System.Windows.Forms.PictureBox();
            this.PiBModDB = new System.Windows.Forms.PictureBox();
            this.PiBThemeSwitcher = new System.Windows.Forms.PictureBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.BtnOptions = new System.Windows.Forms.Button();
            this.TmrPatchNotes = new System.Windows.Forms.Timer(this.components);
            this.PBarActualFile = new System.Windows.Forms.ProgressBar();
            this.LblDownloadSpeed = new System.Windows.Forms.Label();
            this.LblBytes = new System.Windows.Forms.Label();
            this.BtnInstall = new System.Windows.Forms.Button();
            this.LblFileName = new System.Windows.Forms.Label();
            this.PibLoadingRing = new System.Windows.Forms.PictureBox();
            this.LblPatchNotes = new PatchLauncher.Helper.CustomLabel();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.PibLoadingBorder = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Wv2Patchnotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBYoutube)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBDiscord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBModDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBThemeSwitcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingRing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingBorder)).BeginInit();
            this.SuspendLayout();
            // 
            // Wv2Patchnotes
            // 
            this.Wv2Patchnotes.AllowExternalDrop = true;
            this.Wv2Patchnotes.BackColor = System.Drawing.Color.Black;
            this.Wv2Patchnotes.CreationProperties = null;
            this.Wv2Patchnotes.DefaultBackgroundColor = System.Drawing.Color.White;
            this.Wv2Patchnotes.Location = new System.Drawing.Point(218, 110);
            this.Wv2Patchnotes.Name = "Wv2Patchnotes";
            this.Wv2Patchnotes.Size = new System.Drawing.Size(1050, 539);
            this.Wv2Patchnotes.Source = new System.Uri("https://docs.google.com/document/d/1eG95mVD_TYnvkfKEwcXaQvWS5AYbFIq4rDdXrifsKUw", System.UriKind.Absolute);
            this.Wv2Patchnotes.TabIndex = 1;
            this.Wv2Patchnotes.ZoomFactor = 1D;
            // 
            // BtnLaunch
            // 
            this.BtnLaunch.BackColor = System.Drawing.Color.Black;
            this.BtnLaunch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnLaunch.FlatAppearance.BorderSize = 0;
            this.BtnLaunch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnLaunch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLaunch.ForeColor = System.Drawing.Color.Transparent;
            this.BtnLaunch.Location = new System.Drawing.Point(12, 661);
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
            this.PibHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PibHeader.ErrorImage = null;
            this.PibHeader.Image = ((System.Drawing.Image)(resources.GetObject("PibHeader.Image")));
            this.PibHeader.InitialImage = null;
            this.PibHeader.Location = new System.Drawing.Point(253, -1);
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
            this.PiBYoutube.Location = new System.Drawing.Point(497, 11);
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
            this.PiBDiscord.Location = new System.Drawing.Point(558, 11);
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
            this.PiBModDB.Location = new System.Drawing.Point(619, 11);
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
            this.PiBThemeSwitcher.Location = new System.Drawing.Point(727, 11);
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
            this.BtnOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOptions.FlatAppearance.BorderSize = 0;
            this.BtnOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOptions.ForeColor = System.Drawing.Color.Transparent;
            this.BtnOptions.Location = new System.Drawing.Point(1068, 661);
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
            // TmrPatchNotes
            // 
            this.TmrPatchNotes.Enabled = true;
            // 
            // PBarActualFile
            // 
            this.PBarActualFile.Location = new System.Drawing.Point(218, 693);
            this.PBarActualFile.Name = "PBarActualFile";
            this.PBarActualFile.Size = new System.Drawing.Size(439, 15);
            this.PBarActualFile.TabIndex = 12;
            this.PBarActualFile.Visible = false;
            // 
            // LblDownloadSpeed
            // 
            this.LblDownloadSpeed.AutoSize = true;
            this.LblDownloadSpeed.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblDownloadSpeed.Location = new System.Drawing.Point(558, 665);
            this.LblDownloadSpeed.Name = "LblDownloadSpeed";
            this.LblDownloadSpeed.Size = new System.Drawing.Size(103, 25);
            this.LblDownloadSpeed.TabIndex = 13;
            this.LblDownloadSpeed.Text = "@ 32 MB/s";
            // 
            // LblBytes
            // 
            this.LblBytes.AutoSize = true;
            this.LblBytes.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblBytes.Location = new System.Drawing.Point(663, 683);
            this.LblBytes.Name = "LblBytes";
            this.LblBytes.Size = new System.Drawing.Size(114, 25);
            this.LblBytes.TabIndex = 14;
            this.LblBytes.Text = "Percentages";
            // 
            // BtnInstall
            // 
            this.BtnInstall.BackColor = System.Drawing.Color.Black;
            this.BtnInstall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnInstall.FlatAppearance.BorderSize = 0;
            this.BtnInstall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnInstall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnInstall.ForeColor = System.Drawing.Color.Transparent;
            this.BtnInstall.Location = new System.Drawing.Point(12, 661);
            this.BtnInstall.Name = "BtnInstall";
            this.BtnInstall.Size = new System.Drawing.Size(200, 51);
            this.BtnInstall.TabIndex = 16;
            this.BtnInstall.TabStop = false;
            this.BtnInstall.Text = "INSTALL GAME";
            this.BtnInstall.UseMnemonic = false;
            this.BtnInstall.UseVisualStyleBackColor = false;
            this.BtnInstall.Click += new System.EventHandler(this.BtnInstall_Click);
            this.BtnInstall.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnInstall_MouseDown);
            this.BtnInstall.MouseEnter += new System.EventHandler(this.BtnInstall_MouseEnter);
            this.BtnInstall.MouseLeave += new System.EventHandler(this.BtnInstall_MouseLeave);
            // 
            // LblFileName
            // 
            this.LblFileName.AutoSize = true;
            this.LblFileName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblFileName.Location = new System.Drawing.Point(218, 665);
            this.LblFileName.Name = "LblFileName";
            this.LblFileName.Size = new System.Drawing.Size(88, 25);
            this.LblFileName.TabIndex = 17;
            this.LblFileName.Text = "Filename";
            // 
            // PibLoadingRing
            // 
            this.PibLoadingRing.BackColor = System.Drawing.Color.Black;
            this.PibLoadingRing.Image = ((System.Drawing.Image)(resources.GetObject("PibLoadingRing.Image")));
            this.PibLoadingRing.Location = new System.Drawing.Point(587, 292);
            this.PibLoadingRing.Name = "PibLoadingRing";
            this.PibLoadingRing.Size = new System.Drawing.Size(128, 128);
            this.PibLoadingRing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PibLoadingRing.TabIndex = 18;
            this.PibLoadingRing.TabStop = false;
            // 
            // LblPatchNotes
            // 
            this.LblPatchNotes.AutoSize = true;
            this.LblPatchNotes.BackColor = System.Drawing.Color.Transparent;
            this.LblPatchNotes.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.LblPatchNotes.Font = new System.Drawing.Font("Albertus MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblPatchNotes.ForeColor = System.Drawing.SystemColors.Control;
            this.LblPatchNotes.Location = new System.Drawing.Point(544, 488);
            this.LblPatchNotes.Name = "LblPatchNotes";
            this.LblPatchNotes.OutlineForeColor = System.Drawing.Color.Black;
            this.LblPatchNotes.OutlineWidth = 4F;
            this.LblPatchNotes.Size = new System.Drawing.Size(208, 25);
            this.LblPatchNotes.TabIndex = 19;
            this.LblPatchNotes.Text = "Loading Patch-Notes...";
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.BackColor = System.Drawing.Color.Black;
            this.BtnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnUpdate.FlatAppearance.BorderSize = 0;
            this.BtnUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUpdate.ForeColor = System.Drawing.Color.Transparent;
            this.BtnUpdate.Location = new System.Drawing.Point(12, 661);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(200, 51);
            this.BtnUpdate.TabIndex = 20;
            this.BtnUpdate.TabStop = false;
            this.BtnUpdate.Text = "UPDATE GAME";
            this.BtnUpdate.UseMnemonic = false;
            this.BtnUpdate.UseVisualStyleBackColor = false;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            this.BtnUpdate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnUpdate_MouseDown);
            this.BtnUpdate.MouseEnter += new System.EventHandler(this.BtnUpdate_MouseEnter);
            this.BtnUpdate.MouseLeave += new System.EventHandler(this.BtnUpdate_MouseLeave);
            // 
            // PibLoadingBorder
            // 
            this.PibLoadingBorder.BackColor = System.Drawing.Color.Transparent;
            this.PibLoadingBorder.Image = ((System.Drawing.Image)(resources.GetObject("PibLoadingBorder.Image")));
            this.PibLoadingBorder.Location = new System.Drawing.Point(527, 235);
            this.PibLoadingBorder.Name = "PibLoadingBorder";
            this.PibLoadingBorder.Size = new System.Drawing.Size(250, 250);
            this.PibLoadingBorder.TabIndex = 21;
            this.PibLoadingBorder.TabStop = false;
            // 
            // BFME1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.LblPatchNotes);
            this.Controls.Add(this.LblFileName);
            this.Controls.Add(this.BtnInstall);
            this.Controls.Add(this.LblBytes);
            this.Controls.Add(this.LblDownloadSpeed);
            this.Controls.Add(this.PBarActualFile);
            this.Controls.Add(this.BtnOptions);
            this.Controls.Add(this.BtnLaunch);
            this.Controls.Add(this.PiBThemeSwitcher);
            this.Controls.Add(this.PiBModDB);
            this.Controls.Add(this.PiBDiscord);
            this.Controls.Add(this.PiBYoutube);
            this.Controls.Add(this.PibHeader);
            this.Controls.Add(this.PibLoadingRing);
            this.Controls.Add(this.PibLoadingBorder);
            this.Controls.Add(this.Wv2Patchnotes);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MdiChildrenMinimizedAnchorBottom = false;
            this.Name = "BFME1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bfme 2.22 Launcher";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.Wv2Patchnotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBYoutube)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBDiscord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBModDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBThemeSwitcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingRing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingBorder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2Patchnotes;
        private Button BtnLaunch;
        private PictureBox PibHeader;
        private PictureBox PiBYoutube;
        private PictureBox PiBDiscord;
        private PictureBox PiBModDB;
        private PictureBox PiBThemeSwitcher;
        private ToolTip ToolTip;
        private Button BtnOptions;
        private Timer TmrPatchNotes;
        private ProgressBar PBarActualFile;
        private Label LblDownloadSpeed;
        private Label LblBytes;
        private Button BtnInstall;
        private Label LblFileName;
        private PictureBox PibLoadingRing;
        private Helper.CustomLabel LblPatchNotes;
        private Button BtnUpdate;
        private PictureBox PibLoadingBorder;
    }
}