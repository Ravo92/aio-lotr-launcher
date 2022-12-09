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
            this.PibLoadingBorder = new System.Windows.Forms.PictureBox();
            this.BtnPatch103 = new System.Windows.Forms.Button();
            this.LblCurrentVersion = new PatchLauncher.Helper.CustomLabel();
            this.BtnPatch106 = new System.Windows.Forms.Button();
            this.BtnPatch222 = new System.Windows.Forms.Button();
            this.Wv2Patchnotes = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.PibHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBYoutube)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBDiscord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBModDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBThemeSwitcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingRing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wv2Patchnotes)).BeginInit();
            this.SuspendLayout();
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
            this.PBarActualFile.Location = new System.Drawing.Point(218, 661);
            this.PBarActualFile.Name = "PBarActualFile";
            this.PBarActualFile.Size = new System.Drawing.Size(440, 51);
            this.PBarActualFile.TabIndex = 12;
            this.PBarActualFile.Visible = false;
            // 
            // LblDownloadSpeed
            // 
            this.LblDownloadSpeed.AutoSize = true;
            this.LblDownloadSpeed.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblDownloadSpeed.Location = new System.Drawing.Point(554, 633);
            this.LblDownloadSpeed.Name = "LblDownloadSpeed";
            this.LblDownloadSpeed.Size = new System.Drawing.Size(103, 25);
            this.LblDownloadSpeed.TabIndex = 13;
            this.LblDownloadSpeed.Text = "@ 32 MB/s";
            // 
            // LblBytes
            // 
            this.LblBytes.AutoSize = true;
            this.LblBytes.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblBytes.Location = new System.Drawing.Point(668, 671);
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
            this.LblFileName.Location = new System.Drawing.Point(12, 633);
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
            this.LblPatchNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblPatchNotes.ForeColor = System.Drawing.SystemColors.Control;
            this.LblPatchNotes.Location = new System.Drawing.Point(544, 488);
            this.LblPatchNotes.Name = "LblPatchNotes";
            this.LblPatchNotes.OutlineForeColor = System.Drawing.Color.Black;
            this.LblPatchNotes.OutlineWidth = 4F;
            this.LblPatchNotes.Size = new System.Drawing.Size(231, 25);
            this.LblPatchNotes.TabIndex = 19;
            this.LblPatchNotes.Text = "Loading Patch-Notes...";
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
            // BtnPatch103
            // 
            this.BtnPatch103.BackColor = System.Drawing.Color.Black;
            this.BtnPatch103.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPatch103.BackgroundImage")));
            this.BtnPatch103.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPatch103.FlatAppearance.BorderSize = 0;
            this.BtnPatch103.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnPatch103.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnPatch103.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPatch103.ForeColor = System.Drawing.Color.Transparent;
            this.BtnPatch103.Location = new System.Drawing.Point(12, 110);
            this.BtnPatch103.Name = "BtnPatch103";
            this.BtnPatch103.Size = new System.Drawing.Size(235, 55);
            this.BtnPatch103.TabIndex = 23;
            this.BtnPatch103.TabStop = false;
            this.BtnPatch103.Text = "Patch 1.03 (Default)";
            this.BtnPatch103.UseMnemonic = false;
            this.BtnPatch103.UseVisualStyleBackColor = false;
            // 
            // LblCurrentVersion
            // 
            this.LblCurrentVersion.AutoSize = true;
            this.LblCurrentVersion.BackColor = System.Drawing.Color.Transparent;
            this.LblCurrentVersion.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.LblCurrentVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblCurrentVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.LblCurrentVersion.Location = new System.Drawing.Point(51, 79);
            this.LblCurrentVersion.Name = "LblCurrentVersion";
            this.LblCurrentVersion.OutlineForeColor = System.Drawing.Color.Black;
            this.LblCurrentVersion.OutlineWidth = 4F;
            this.LblCurrentVersion.Size = new System.Drawing.Size(186, 25);
            this.LblCurrentVersion.TabIndex = 24;
            this.LblCurrentVersion.Text = "Active Patch: 1.03";
            // 
            // BtnPatch106
            // 
            this.BtnPatch106.BackColor = System.Drawing.Color.Black;
            this.BtnPatch106.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPatch106.BackgroundImage")));
            this.BtnPatch106.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPatch106.FlatAppearance.BorderSize = 0;
            this.BtnPatch106.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnPatch106.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnPatch106.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPatch106.ForeColor = System.Drawing.Color.Transparent;
            this.BtnPatch106.Location = new System.Drawing.Point(12, 171);
            this.BtnPatch106.Name = "BtnPatch106";
            this.BtnPatch106.Size = new System.Drawing.Size(235, 55);
            this.BtnPatch106.TabIndex = 25;
            this.BtnPatch106.TabStop = false;
            this.BtnPatch106.Text = "Patch 1.06";
            this.BtnPatch106.UseMnemonic = false;
            this.BtnPatch106.UseVisualStyleBackColor = false;
            // 
            // BtnPatch222
            // 
            this.BtnPatch222.BackColor = System.Drawing.Color.Black;
            this.BtnPatch222.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPatch222.BackgroundImage")));
            this.BtnPatch222.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPatch222.FlatAppearance.BorderSize = 0;
            this.BtnPatch222.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnPatch222.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnPatch222.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPatch222.ForeColor = System.Drawing.Color.Transparent;
            this.BtnPatch222.Location = new System.Drawing.Point(12, 235);
            this.BtnPatch222.Name = "BtnPatch222";
            this.BtnPatch222.Size = new System.Drawing.Size(235, 55);
            this.BtnPatch222.TabIndex = 26;
            this.BtnPatch222.TabStop = false;
            this.BtnPatch222.Text = "Patch 2.22 V29";
            this.BtnPatch222.UseMnemonic = false;
            this.BtnPatch222.UseVisualStyleBackColor = false;
            // 
            // Wv2Patchnotes
            // 
            this.Wv2Patchnotes.AllowExternalDrop = true;
            this.Wv2Patchnotes.BackColor = System.Drawing.Color.Black;
            this.Wv2Patchnotes.CreationProperties = null;
            this.Wv2Patchnotes.DefaultBackgroundColor = System.Drawing.Color.White;
            this.Wv2Patchnotes.Location = new System.Drawing.Point(253, 110);
            this.Wv2Patchnotes.Name = "Wv2Patchnotes";
            this.Wv2Patchnotes.Size = new System.Drawing.Size(818, 514);
            this.Wv2Patchnotes.Source = new System.Uri("https://ravo92.github.io/changelogpage/index.html", System.UriKind.Absolute);
            this.Wv2Patchnotes.TabIndex = 1;
            this.Wv2Patchnotes.ZoomFactor = 1D;
            // 
            // BFME1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.BtnPatch222);
            this.Controls.Add(this.BtnPatch106);
            this.Controls.Add(this.LblCurrentVersion);
            this.Controls.Add(this.BtnPatch103);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BFME1_FormClosing);
            this.Shown += new System.EventHandler(this.BFME1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PibHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBYoutube)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBDiscord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBModDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBThemeSwitcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingRing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wv2Patchnotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private PictureBox PibLoadingBorder;
        private Button BtnPatch103;
        private Helper.CustomLabel LblCurrentVersion;
        private Button BtnPatch106;
        private Button BtnPatch222;
        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2Patchnotes;
    }
}