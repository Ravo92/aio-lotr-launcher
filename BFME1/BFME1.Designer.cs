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
            this.LblPatchNotes = new Helper.CustomLabel();
            this.PibLoadingBorder = new System.Windows.Forms.PictureBox();
            this.Wv2Patchnotes = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.PiBTwitch = new System.Windows.Forms.PictureBox();
            this.PiBArrow = new System.Windows.Forms.PictureBox();
            this.TmrAnimation = new System.Windows.Forms.Timer(this.components);
            this.PnlPlaceholder = new System.Windows.Forms.Panel();
            this.PiBVersion222_5 = new System.Windows.Forms.PictureBox();
            this.PiBMod_4 = new System.Windows.Forms.PictureBox();
            this.PiBMod_3 = new System.Windows.Forms.PictureBox();
            this.PiBVersion222_6 = new System.Windows.Forms.PictureBox();
            this.PiBVersion222_4 = new System.Windows.Forms.PictureBox();
            this.PiBVersion222_3 = new System.Windows.Forms.PictureBox();
            this.PiBVersion222_2 = new System.Windows.Forms.PictureBox();
            this.PiBMod_2 = new System.Windows.Forms.PictureBox();
            this.PiBMod_1 = new System.Windows.Forms.PictureBox();
            this.PiBVersion222_1 = new System.Windows.Forms.PictureBox();
            this.PiBVersion106 = new System.Windows.Forms.PictureBox();
            this.PiBVersion103 = new System.Windows.Forms.PictureBox();
            this.LblInstalledMods = new Helper.CustomLabel();
            this.LblInstalledPatches = new Helper.CustomLabel();
            this.LblModExplanation = new Helper.CustomLabel();
            this.SysTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemLaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTheLauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnOpenAppDataFolder = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBMod_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBMod_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBMod_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBMod_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion106)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion103)).BeginInit();
            this.NotifyContextMenu.SuspendLayout();
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
            this.PibHeader.Location = new System.Drawing.Point(247, -1);
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
            this.PiBYoutube.Location = new System.Drawing.Point(491, 11);
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
            this.PiBDiscord.Location = new System.Drawing.Point(552, 11);
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
            this.PiBModDB.Location = new System.Drawing.Point(613, 11);
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
            this.PiBThemeSwitcher.Location = new System.Drawing.Point(735, 11);
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
            this.PiBTwitch.Location = new System.Drawing.Point(674, 12);
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
            this.PiBArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
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
            this.PnlPlaceholder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlPlaceholder.Controls.Add(this.PiBVersion222_5);
            this.PnlPlaceholder.Controls.Add(this.PiBMod_4);
            this.PnlPlaceholder.Controls.Add(this.PiBMod_3);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion222_6);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion222_4);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion222_3);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion222_2);
            this.PnlPlaceholder.Controls.Add(this.PiBMod_2);
            this.PnlPlaceholder.Controls.Add(this.PiBMod_1);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion222_1);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion106);
            this.PnlPlaceholder.Controls.Add(this.PiBVersion103);
            this.PnlPlaceholder.Location = new System.Drawing.Point(12, 110);
            this.PnlPlaceholder.Name = "PnlPlaceholder";
            this.PnlPlaceholder.Size = new System.Drawing.Size(1256, 514);
            this.PnlPlaceholder.TabIndex = 24;
            // 
            // PiBVersion222_5
            // 
            this.PiBVersion222_5.BackColor = System.Drawing.Color.Black;
            this.PiBVersion222_5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBVersion222_5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion222_5.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion222_5.Image")));
            this.PiBVersion222_5.Location = new System.Drawing.Point(400, 270);
            this.PiBVersion222_5.Name = "PiBVersion222_5";
            this.PiBVersion222_5.Size = new System.Drawing.Size(150, 200);
            this.PiBVersion222_5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion222_5.TabIndex = 40;
            this.PiBVersion222_5.TabStop = false;
            this.PiBVersion222_5.Click += new System.EventHandler(this.PiBVersion222_5_Click);
            // 
            // PiBMod_4
            // 
            this.PiBMod_4.BackColor = System.Drawing.Color.Black;
            this.PiBMod_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBMod_4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBMod_4.Image = ((System.Drawing.Image)(resources.GetObject("PiBMod_4.Image")));
            this.PiBMod_4.Location = new System.Drawing.Point(1040, 270);
            this.PiBMod_4.Name = "PiBMod_4";
            this.PiBMod_4.Size = new System.Drawing.Size(150, 200);
            this.PiBMod_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBMod_4.TabIndex = 39;
            this.PiBMod_4.TabStop = false;
            this.PiBMod_4.Click += new System.EventHandler(this.PiBMod_4_Click);
            // 
            // PiBMod_3
            // 
            this.PiBMod_3.BackColor = System.Drawing.Color.Black;
            this.PiBMod_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBMod_3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBMod_3.Image = ((System.Drawing.Image)(resources.GetObject("PiBMod_3.Image")));
            this.PiBMod_3.Location = new System.Drawing.Point(870, 270);
            this.PiBMod_3.Name = "PiBMod_3";
            this.PiBMod_3.Size = new System.Drawing.Size(150, 200);
            this.PiBMod_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBMod_3.TabIndex = 38;
            this.PiBMod_3.TabStop = false;
            this.PiBMod_3.Click += new System.EventHandler(this.PiBMod_3_Click);
            // 
            // PiBVersion222_6
            // 
            this.PiBVersion222_6.BackColor = System.Drawing.Color.Black;
            this.PiBVersion222_6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBVersion222_6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion222_6.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion222_6.Image")));
            this.PiBVersion222_6.Location = new System.Drawing.Point(570, 270);
            this.PiBVersion222_6.Name = "PiBVersion222_6";
            this.PiBVersion222_6.Size = new System.Drawing.Size(150, 200);
            this.PiBVersion222_6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion222_6.TabIndex = 37;
            this.PiBVersion222_6.TabStop = false;
            this.PiBVersion222_6.Click += new System.EventHandler(this.PiBVersion222_6_Click);
            // 
            // PiBVersion222_4
            // 
            this.PiBVersion222_4.BackColor = System.Drawing.Color.Black;
            this.PiBVersion222_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBVersion222_4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion222_4.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion222_4.Image")));
            this.PiBVersion222_4.Location = new System.Drawing.Point(230, 270);
            this.PiBVersion222_4.Name = "PiBVersion222_4";
            this.PiBVersion222_4.Size = new System.Drawing.Size(150, 200);
            this.PiBVersion222_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion222_4.TabIndex = 36;
            this.PiBVersion222_4.TabStop = false;
            this.PiBVersion222_4.Click += new System.EventHandler(this.PiBVersion222_4_Click);
            // 
            // PiBVersion222_3
            // 
            this.PiBVersion222_3.BackColor = System.Drawing.Color.Black;
            this.PiBVersion222_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBVersion222_3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion222_3.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion222_3.Image")));
            this.PiBVersion222_3.Location = new System.Drawing.Point(60, 270);
            this.PiBVersion222_3.Name = "PiBVersion222_3";
            this.PiBVersion222_3.Size = new System.Drawing.Size(150, 200);
            this.PiBVersion222_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion222_3.TabIndex = 35;
            this.PiBVersion222_3.TabStop = false;
            this.PiBVersion222_3.Click += new System.EventHandler(this.PiBVersion222_3_Click);
            // 
            // PiBVersion222_2
            // 
            this.PiBVersion222_2.BackColor = System.Drawing.Color.Black;
            this.PiBVersion222_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBVersion222_2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion222_2.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion222_2.Image")));
            this.PiBVersion222_2.Location = new System.Drawing.Point(570, 50);
            this.PiBVersion222_2.Name = "PiBVersion222_2";
            this.PiBVersion222_2.Size = new System.Drawing.Size(150, 200);
            this.PiBVersion222_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion222_2.TabIndex = 34;
            this.PiBVersion222_2.TabStop = false;
            this.PiBVersion222_2.Click += new System.EventHandler(this.PiBVersion222_2_Click);
            // 
            // PiBMod_2
            // 
            this.PiBMod_2.BackColor = System.Drawing.Color.Black;
            this.PiBMod_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBMod_2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBMod_2.Image = ((System.Drawing.Image)(resources.GetObject("PiBMod_2.Image")));
            this.PiBMod_2.Location = new System.Drawing.Point(1040, 50);
            this.PiBMod_2.Name = "PiBMod_2";
            this.PiBMod_2.Size = new System.Drawing.Size(150, 200);
            this.PiBMod_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBMod_2.TabIndex = 33;
            this.PiBMod_2.TabStop = false;
            this.PiBMod_2.Click += new System.EventHandler(this.PiBMod_2_Click);
            // 
            // PiBMod_1
            // 
            this.PiBMod_1.BackColor = System.Drawing.Color.Black;
            this.PiBMod_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBMod_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBMod_1.Image = ((System.Drawing.Image)(resources.GetObject("PiBMod_1.Image")));
            this.PiBMod_1.Location = new System.Drawing.Point(870, 50);
            this.PiBMod_1.Name = "PiBMod_1";
            this.PiBMod_1.Size = new System.Drawing.Size(150, 200);
            this.PiBMod_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBMod_1.TabIndex = 31;
            this.PiBMod_1.TabStop = false;
            this.PiBMod_1.Click += new System.EventHandler(this.PiBMod_1_Click);
            // 
            // PiBVersion222_1
            // 
            this.PiBVersion222_1.BackColor = System.Drawing.Color.Black;
            this.PiBVersion222_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBVersion222_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion222_1.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion222_1.Image")));
            this.PiBVersion222_1.Location = new System.Drawing.Point(400, 50);
            this.PiBVersion222_1.Name = "PiBVersion222_1";
            this.PiBVersion222_1.Size = new System.Drawing.Size(150, 200);
            this.PiBVersion222_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion222_1.TabIndex = 27;
            this.PiBVersion222_1.TabStop = false;
            this.PiBVersion222_1.Click += new System.EventHandler(this.PiBVersion222_1_Click);
            // 
            // PiBVersion106
            // 
            this.PiBVersion106.BackColor = System.Drawing.Color.Black;
            this.PiBVersion106.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBVersion106.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion106.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion106.Image")));
            this.PiBVersion106.Location = new System.Drawing.Point(230, 50);
            this.PiBVersion106.Name = "PiBVersion106";
            this.PiBVersion106.Size = new System.Drawing.Size(150, 200);
            this.PiBVersion106.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion106.TabIndex = 26;
            this.PiBVersion106.TabStop = false;
            this.PiBVersion106.Click += new System.EventHandler(this.PiBVersion106_Click);
            // 
            // PiBVersion103
            // 
            this.PiBVersion103.BackColor = System.Drawing.Color.Black;
            this.PiBVersion103.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PiBVersion103.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PiBVersion103.Image = ((System.Drawing.Image)(resources.GetObject("PiBVersion103.Image")));
            this.PiBVersion103.Location = new System.Drawing.Point(60, 50);
            this.PiBVersion103.Name = "PiBVersion103";
            this.PiBVersion103.Size = new System.Drawing.Size(150, 200);
            this.PiBVersion103.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PiBVersion103.TabIndex = 25;
            this.PiBVersion103.TabStop = false;
            this.PiBVersion103.Click += new System.EventHandler(this.PiBVersion103_Click);
            // 
            // LblInstalledMods
            // 
            this.LblInstalledMods.AutoSize = true;
            this.LblInstalledMods.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblInstalledMods.Location = new System.Drawing.Point(1030, 50);
            this.LblInstalledMods.Name = "LblInstalledMods";
            this.LblInstalledMods.OutlineForeColor = System.Drawing.Color.Black;
            this.LblInstalledMods.OutlineWidth = 4F;
            this.LblInstalledMods.Size = new System.Drawing.Size(108, 47);
            this.LblInstalledMods.TabIndex = 41;
            this.LblInstalledMods.Text = "Mods";
            // 
            // LblInstalledPatches
            // 
            this.LblInstalledPatches.AutoSize = true;
            this.LblInstalledPatches.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblInstalledPatches.Location = new System.Drawing.Point(60, 50);
            this.LblInstalledPatches.Name = "LblInstalledPatches";
            this.LblInstalledPatches.OutlineForeColor = System.Drawing.Color.Black;
            this.LblInstalledPatches.OutlineWidth = 4F;
            this.LblInstalledPatches.Size = new System.Drawing.Size(138, 47);
            this.LblInstalledPatches.TabIndex = 28;
            this.LblInstalledPatches.Text = "Patches";
            // 
            // LblModExplanation
            // 
            this.LblModExplanation.AutoSize = true;
            this.LblModExplanation.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblModExplanation.Location = new System.Drawing.Point(110, 612);
            this.LblModExplanation.Name = "LblModExplanation";
            this.LblModExplanation.OutlineForeColor = System.Drawing.Color.Black;
            this.LblModExplanation.OutlineWidth = 4F;
            this.LblModExplanation.Size = new System.Drawing.Size(945, 47);
            this.LblModExplanation.TabIndex = 29;
            this.LblModExplanation.Text = "Here you can choose which mod or patch you want to play.";
            // 
            // SysTray
            // 
            this.SysTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.SysTray.BalloonTipText = "Launcher is minimized to SysTray. If you want to Close, press \"X\" in the App or r" +
    "ightclick on the Icon in SysTray.";
            this.SysTray.BalloonTipTitle = "Launcher is still running.";
            this.SysTray.Icon = ((System.Drawing.Icon)(resources.GetObject("SysTray.Icon")));
            this.SysTray.Text = "BFME 2.22 Launcher";
            this.SysTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SysTray_MouseDoubleClick);
            // 
            // NotifyContextMenu
            // 
            this.NotifyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemLaunchGame,
            this.closeTheLauncherToolStripMenuItem});
            this.NotifyContextMenu.Name = "NotifyContextMenu";
            this.NotifyContextMenu.Size = new System.Drawing.Size(176, 48);
            // 
            // MenuItemLaunchGame
            // 
            this.MenuItemLaunchGame.Name = "MenuItemLaunchGame";
            this.MenuItemLaunchGame.Size = new System.Drawing.Size(175, 22);
            this.MenuItemLaunchGame.Text = "Launch the Game";
            this.MenuItemLaunchGame.Click += new System.EventHandler(this.MenuItemLaunchGame_Click);
            // 
            // closeTheLauncherToolStripMenuItem
            // 
            this.closeTheLauncherToolStripMenuItem.Name = "closeTheLauncherToolStripMenuItem";
            this.closeTheLauncherToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.closeTheLauncherToolStripMenuItem.Text = "Close the Launcher";
            this.closeTheLauncherToolStripMenuItem.Click += new System.EventHandler(this.CloseTheLauncherToolStripMenuItem_Click);
            // 
            // BtnOpenAppDataFolder
            // 
            this.BtnOpenAppDataFolder.BackColor = System.Drawing.Color.Black;
            this.BtnOpenAppDataFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOpenAppDataFolder.FlatAppearance.BorderSize = 0;
            this.BtnOpenAppDataFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnOpenAppDataFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnOpenAppDataFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOpenAppDataFolder.ForeColor = System.Drawing.Color.Transparent;
            this.BtnOpenAppDataFolder.Location = new System.Drawing.Point(839, 661);
            this.BtnOpenAppDataFolder.Name = "BtnOpenAppDataFolder";
            this.BtnOpenAppDataFolder.Size = new System.Drawing.Size(200, 51);
            this.BtnOpenAppDataFolder.TabIndex = 42;
            this.BtnOpenAppDataFolder.TabStop = false;
            this.BtnOpenAppDataFolder.Text = "APPDATA FOLDER";
            this.BtnOpenAppDataFolder.UseMnemonic = false;
            this.BtnOpenAppDataFolder.UseVisualStyleBackColor = false;
            this.BtnOpenAppDataFolder.Click += new System.EventHandler(this.BtnOpenAppDataFolder_Click);
            this.BtnOpenAppDataFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnOpenAppDataFolder_MouseDown);
            this.BtnOpenAppDataFolder.MouseEnter += new System.EventHandler(this.BtnOpenAppDataFolder_MouseEnter);
            this.BtnOpenAppDataFolder.MouseLeave += new System.EventHandler(this.BtnOpenAppDataFolder_MouseLeave);
            // 
            // BFME1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.BtnOpenAppDataFolder);
            this.Controls.Add(this.LblInstalledMods);
            this.Controls.Add(this.PiBArrow);
            this.Controls.Add(this.PiBTwitch);
            this.Controls.Add(this.LblPatchNotes);
            this.Controls.Add(this.LblFileName);
            this.Controls.Add(this.BtnInstall);
            this.Controls.Add(this.LblBytes);
            this.Controls.Add(this.LblDownloadSpeed);
            this.Controls.Add(this.PBarActualFile);
            this.Controls.Add(this.BtnOptions);
            this.Controls.Add(this.LblModExplanation);
            this.Controls.Add(this.LblInstalledPatches);
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
            this.Resize += new System.EventHandler(this.BFME1_Resize);
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
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBMod_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBMod_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBMod_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBMod_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion222_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion106)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiBVersion103)).EndInit();
            this.NotifyContextMenu.ResumeLayout(false);
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
        private PictureBox PiBVersion222_1;
        private PictureBox PiBVersion106;
        private PictureBox PiBVersion103;
        private Helper.CustomLabel LblInstalledPatches;
        private Helper.CustomLabel LblModExplanation;
        private PictureBox PiBMod_1;
        private PictureBox PiBMod_2;
        private PictureBox PiBMod_4;
        private PictureBox PiBMod_3;
        private PictureBox PiBVersion222_6;
        private PictureBox PiBVersion222_4;
        private PictureBox PiBVersion222_3;
        private PictureBox PiBVersion222_2;
        private PictureBox PiBVersion222_5;
        private Helper.CustomLabel LblInstalledMods;
        private NotifyIcon SysTray;
        private ContextMenuStrip NotifyContextMenu;
        private ToolStripMenuItem MenuItemLaunchGame;
        private ToolStripMenuItem closeTheLauncherToolStripMenuItem;
        private Button BtnOpenAppDataFolder;
    }
}