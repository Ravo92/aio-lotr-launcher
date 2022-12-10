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
            this.Wv2Patchnotes = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.PiBTwitch = new System.Windows.Forms.PictureBox();
            this.PiBArrow = new System.Windows.Forms.PictureBox();
            this.TmrAnimation = new System.Windows.Forms.Timer(this.components);
            this.PnlPlaceholder = new System.Windows.Forms.Panel();
            this.PibMod1 = new System.Windows.Forms.PictureBox();
            this.PibMod2 = new System.Windows.Forms.PictureBox();
            this.LblModExplanation = new PatchLauncher.Helper.CustomLabel();
            this.LblInstalledMods = new PatchLauncher.Helper.CustomLabel();
            this.PiBVersion222 = new System.Windows.Forms.PictureBox();
            this.PiBVersion106 = new System.Windows.Forms.PictureBox();
            this.PiBVersion103 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PibHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBYoutube)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBDiscord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBModDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBThemeSwitcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingRing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibLoadingBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wv2Patchnotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBTwitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBArrow)).BeginInit();
            this.PnlPlaceholder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PibMod1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibMod2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion106)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion103)).BeginInit();
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
            this.PibHeader.Size = new System.Drawing.Size(792, 105);
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
            this.PiBThemeSwitcher.Location = new System.Drawing.Point(741, 11);
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
            // Wv2Patchnotes
            // 
            this.Wv2Patchnotes.AllowExternalDrop = true;
            this.Wv2Patchnotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Wv2Patchnotes.CreationProperties = null;
            this.Wv2Patchnotes.DefaultBackgroundColor = System.Drawing.Color.White;
            this.Wv2Patchnotes.Location = new System.Drawing.Point(12, 110);
            this.Wv2Patchnotes.Name = "Wv2Patchnotes";
            this.Wv2Patchnotes.Size = new System.Drawing.Size(1256, 514);
            this.Wv2Patchnotes.Source = new System.Uri("https://ravo92.github.io/changelogpage/index.html", System.UriKind.Absolute);
            this.Wv2Patchnotes.TabIndex = 1;
            this.Wv2Patchnotes.ZoomFactor = 1D;
            // 
            // PiBTwitch
            // 
            this.PiBTwitch.BackColor = System.Drawing.Color.Black;
            this.PiBTwitch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBTwitch.Location = new System.Drawing.Point(680, 12);
            this.PiBTwitch.Name = "PiBTwitch";
            this.PiBTwitch.Size = new System.Drawing.Size(55, 55);
            this.PiBTwitch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBTwitch.TabIndex = 22;
            this.PiBTwitch.TabStop = false;
            this.PiBTwitch.Click += new System.EventHandler(this.PiBTwitch_Click);
            // 
            // PiBArrow
            // 
            this.PiBArrow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.PiBArrow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBArrow.Image = ((System.Drawing.Image)(resources.GetObject("PiBArrow.Image")));
            this.PiBArrow.Location = new System.Drawing.Point(12, 110);
            this.PiBArrow.Name = "PiBArrow";
            this.PiBArrow.Size = new System.Drawing.Size(55, 55);
            this.PiBArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBArrow.TabIndex = 23;
            this.PiBArrow.TabStop = false;
            this.PiBArrow.Click += new System.EventHandler(this.PiBArrow_Click);
            // 
            // TmrAnimation
            // 
            this.TmrAnimation.Interval = 10;
            this.TmrAnimation.Tick += new System.EventHandler(this.TmrAnimation_Tick);
            // 
            // PnlPlaceholder
            // 
            this.PnlPlaceholder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.PnlPlaceholder.Controls.Add(this.PibMod1);
            this.PnlPlaceholder.Controls.Add(this.PibMod2);
            this.PnlPlaceholder.Controls.Add(this.LblModExplanation);
            this.PnlPlaceholder.Controls.Add(this.LblInstalledMods);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion222);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion106);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion103);
            this.PnlPlaceholder.Location = new System.Drawing.Point(12, 110);
            this.PnlPlaceholder.Name = "PnlPlaceholder";
            this.PnlPlaceholder.Size = new System.Drawing.Size(1256, 514);
            this.PnlPlaceholder.TabIndex = 24;
            // 
            // PibMod1
            // 
            this.PibMod1.BackColor = System.Drawing.Color.Black;
            this.PibMod1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PibMod1.Image = ((System.Drawing.Image)(resources.GetObject("PibMod1.Image")));
            this.PibMod1.Location = new System.Drawing.Point(755, 160);
            this.PibMod1.Name = "PibMod1";
            this.PibMod1.Size = new System.Drawing.Size(200, 250);
            this.PibMod1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PibMod1.TabIndex = 32;
            this.PibMod1.TabStop = false;
            // 
            // PibMod2
            // 
            this.PibMod2.BackColor = System.Drawing.Color.Black;
            this.PibMod2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PibMod2.Image = ((System.Drawing.Image)(resources.GetObject("PibMod2.Image")));
            this.PibMod2.Location = new System.Drawing.Point(985, 160);
            this.PibMod2.Name = "PibMod2";
            this.PibMod2.Size = new System.Drawing.Size(200, 250);
            this.PibMod2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PibMod2.TabIndex = 31;
            this.PibMod2.TabStop = false;
            // 
            // LblModExplanation
            // 
            this.LblModExplanation.AutoSize = true;
            this.LblModExplanation.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblModExplanation.Location = new System.Drawing.Point(85, 427);
            this.LblModExplanation.Name = "LblModExplanation";
            this.LblModExplanation.OutlineForeColor = System.Drawing.Color.Black;
            this.LblModExplanation.OutlineWidth = 4F;
            this.LblModExplanation.Size = new System.Drawing.Size(945, 47);
            this.LblModExplanation.TabIndex = 29;
            this.LblModExplanation.Text = "Here you can choose which mod or patch you want to play.";
            // 
            // LblInstalledMods
            // 
            this.LblInstalledMods.AutoSize = true;
            this.LblInstalledMods.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblInstalledMods.Location = new System.Drawing.Point(420, 50);
            this.LblInstalledMods.Name = "LblInstalledMods";
            this.LblInstalledMods.OutlineForeColor = System.Drawing.Color.Black;
            this.LblInstalledMods.OutlineWidth = 4F;
            this.LblInstalledMods.Size = new System.Drawing.Size(277, 47);
            this.LblInstalledMods.TabIndex = 28;
            this.LblInstalledMods.Text = "Installed Patches";
            // 
            // PiBVersion222
            // 
            this.PiBVersion222.BackColor = System.Drawing.Color.Black;
            this.PiBVersion222.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion222.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion222.Image")));
            this.PiBVersion222.Location = new System.Drawing.Point(525, 160);
            this.PiBVersion222.Name = "PiBVersion222";
            this.PiBVersion222.Size = new System.Drawing.Size(200, 250);
            this.PiBVersion222.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion222.TabIndex = 27;
            this.PiBVersion222.TabStop = false;
            // 
            // PiBVersion106
            // 
            this.PiBVersion106.BackColor = System.Drawing.Color.Black;
            this.PiBVersion106.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion106.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion106.Image")));
            this.PiBVersion106.Location = new System.Drawing.Point(295, 160);
            this.PiBVersion106.Name = "PiBVersion106";
            this.PiBVersion106.Size = new System.Drawing.Size(200, 250);
            this.PiBVersion106.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion106.TabIndex = 26;
            this.PiBVersion106.TabStop = false;
            // 
            // PiBVersion103
            // 
            this.PiBVersion103.BackColor = System.Drawing.Color.Black;
            this.PiBVersion103.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion103.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion103.Image")));
            this.PiBVersion103.Location = new System.Drawing.Point(65, 160);
            this.PiBVersion103.Name = "PiBVersion103";
            this.PiBVersion103.Size = new System.Drawing.Size(200, 250);
            this.PiBVersion103.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion103.TabIndex = 25;
            this.PiBVersion103.TabStop = false;
            // 
            // BFME1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.PiBArrow);
            this.Controls.Add(this.PiBTwitch);
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
            this.Controls.Add(this.PnlPlaceholder);
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
            ((System.ComponentModel.ISupportInitialize)(this.PiBTwitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBArrow)).EndInit();
            this.PnlPlaceholder.ResumeLayout(false);
            this.PnlPlaceholder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PibMod1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibMod2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion106)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion103)).EndInit();
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
        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2Patchnotes;
        private PictureBox PiBTwitch;
        private PictureBox PiBArrow;
        private Timer TmrAnimation;
        private Panel PnlPlaceholder;
        private PictureBox PiBVersion222;
        private PictureBox PiBVersion106;
        private PictureBox PiBVersion103;
        private Helper.CustomLabel LblInstalledMods;
        private Helper.CustomLabel LblModExplanation;
        private PictureBox PibMod2;
        private PictureBox PibMod1;
    }
}