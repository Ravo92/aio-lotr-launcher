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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BFME1));
            BtnLaunch = new Button();
            PibHeader = new PictureBox();
            PiBYoutube = new PictureBox();
            PiBDiscord = new PictureBox();
            PiBModDB = new PictureBox();
            PiBThemeSwitcher = new PictureBox();
            ToolTip = new ToolTip(components);
            BtnOptions = new Button();
            TmrPatchNotes = new Timer(components);
            PBarActualFile = new ProgressBar();
            LblDownloadSpeed = new Label();
            LblBytes = new Label();
            BtnInstall = new Button();
            LblFileName = new Label();
            PibLoadingRing = new PictureBox();
            LblPatchNotes = new Helper.CustomLabel();
            PibLoadingBorder = new PictureBox();
            Wv2Patchnotes = new Microsoft.Web.WebView2.WinForms.WebView2();
            PiBTwitch = new PictureBox();
            PiBArrow = new PictureBox();
            TmrAnimation = new Timer(components);
            PnlPlaceholder = new Panel();
            PiBVersion222_7 = new PictureBox();
            PiBVersion222_5 = new PictureBox();
            PiBVersion222_6 = new PictureBox();
            PiBVersion106 = new PictureBox();
            PiBVersion103 = new PictureBox();
            LblInstalledPatches = new Helper.CustomLabel();
            LblModExplanation = new Helper.CustomLabel();
            SysTray = new NotifyIcon(components);
            NotifyContextMenu = new ContextMenuStrip(components);
            MenuItemLaunchGame = new ToolStripMenuItem();
            closeTheLauncherToolStripMenuItem = new ToolStripMenuItem();
            BtnAdvanced = new Button();
            PibMute = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PibHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBYoutube).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBDiscord).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBModDB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBThemeSwitcher).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PibLoadingRing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PibLoadingBorder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Wv2Patchnotes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBTwitch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBArrow).BeginInit();
            PnlPlaceholder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion106).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion103).BeginInit();
            NotifyContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PibMute).BeginInit();
            SuspendLayout();
            // 
            // BtnLaunch
            // 
            BtnLaunch.BackColor = System.Drawing.Color.Black;
            BtnLaunch.BackgroundImageLayout = ImageLayout.Stretch;
            BtnLaunch.FlatAppearance.BorderSize = 0;
            BtnLaunch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnLaunch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnLaunch.FlatStyle = FlatStyle.Flat;
            BtnLaunch.ForeColor = System.Drawing.Color.Transparent;
            BtnLaunch.Location = new System.Drawing.Point(12, 661);
            BtnLaunch.Name = "BtnLaunch";
            BtnLaunch.Size = new System.Drawing.Size(200, 51);
            BtnLaunch.TabIndex = 2;
            BtnLaunch.TabStop = false;
            BtnLaunch.Text = "LAUNCH";
            BtnLaunch.UseMnemonic = false;
            BtnLaunch.UseVisualStyleBackColor = false;
            BtnLaunch.Click += BtnLaunch_Click;
            BtnLaunch.MouseDown += BtnLaunch_MouseDown;
            BtnLaunch.MouseEnter += BtnLaunch_MouseEnter;
            BtnLaunch.MouseLeave += BtnLaunch_MouseLeave;
            // 
            // PibHeader
            // 
            PibHeader.BackColor = System.Drawing.Color.Transparent;
            PibHeader.BackgroundImageLayout = ImageLayout.Center;
            PibHeader.ErrorImage = null;
            PibHeader.Image = (System.Drawing.Image)resources.GetObject("PibHeader.Image");
            PibHeader.InitialImage = null;
            PibHeader.Location = new System.Drawing.Point(247, -1);
            PibHeader.Name = "PibHeader";
            PibHeader.Size = new System.Drawing.Size(792, 105);
            PibHeader.SizeMode = PictureBoxSizeMode.StretchImage;
            PibHeader.TabIndex = 5;
            PibHeader.TabStop = false;
            // 
            // PiBYoutube
            // 
            PiBYoutube.BackColor = System.Drawing.Color.Black;
            PiBYoutube.Cursor = Cursors.Hand;
            PiBYoutube.Location = new System.Drawing.Point(491, 11);
            PiBYoutube.Name = "PiBYoutube";
            PiBYoutube.Size = new System.Drawing.Size(55, 55);
            PiBYoutube.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBYoutube.TabIndex = 6;
            PiBYoutube.TabStop = false;
            PiBYoutube.Click += PiBYoutube_Click;
            // 
            // PiBDiscord
            // 
            PiBDiscord.BackColor = System.Drawing.Color.Black;
            PiBDiscord.Cursor = Cursors.Hand;
            PiBDiscord.Location = new System.Drawing.Point(552, 11);
            PiBDiscord.Name = "PiBDiscord";
            PiBDiscord.Size = new System.Drawing.Size(55, 55);
            PiBDiscord.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBDiscord.TabIndex = 7;
            PiBDiscord.TabStop = false;
            PiBDiscord.Click += PiBDiscord_Click;
            // 
            // PiBModDB
            // 
            PiBModDB.BackColor = System.Drawing.Color.Black;
            PiBModDB.Cursor = Cursors.Hand;
            PiBModDB.Location = new System.Drawing.Point(613, 11);
            PiBModDB.Name = "PiBModDB";
            PiBModDB.Size = new System.Drawing.Size(55, 55);
            PiBModDB.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBModDB.TabIndex = 8;
            PiBModDB.TabStop = false;
            PiBModDB.Click += PiBModDB_Click;
            // 
            // PiBThemeSwitcher
            // 
            PiBThemeSwitcher.BackColor = System.Drawing.Color.Black;
            PiBThemeSwitcher.Cursor = Cursors.Hand;
            PiBThemeSwitcher.Location = new System.Drawing.Point(735, 11);
            PiBThemeSwitcher.Name = "PiBThemeSwitcher";
            PiBThemeSwitcher.Size = new System.Drawing.Size(55, 55);
            PiBThemeSwitcher.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBThemeSwitcher.TabIndex = 9;
            PiBThemeSwitcher.TabStop = false;
            PiBThemeSwitcher.Click += PiBThemeSwitcher_Click;
            // 
            // ToolTip
            // 
            ToolTip.BackColor = System.Drawing.Color.Black;
            ToolTip.ForeColor = System.Drawing.Color.FromArgb(192, 145, 69);
            ToolTip.OwnerDraw = true;
            ToolTip.Draw += Tooltip_Draw;
            ToolTip.Popup += TooltipPopup;
            // 
            // BtnOptions
            // 
            BtnOptions.BackColor = System.Drawing.Color.Black;
            BtnOptions.BackgroundImageLayout = ImageLayout.Stretch;
            BtnOptions.FlatAppearance.BorderSize = 0;
            BtnOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnOptions.FlatStyle = FlatStyle.Flat;
            BtnOptions.ForeColor = System.Drawing.Color.Transparent;
            BtnOptions.Location = new System.Drawing.Point(1068, 661);
            BtnOptions.Name = "BtnOptions";
            BtnOptions.Size = new System.Drawing.Size(200, 51);
            BtnOptions.TabIndex = 10;
            BtnOptions.TabStop = false;
            BtnOptions.Text = "OPTIONS";
            BtnOptions.UseMnemonic = false;
            BtnOptions.UseVisualStyleBackColor = false;
            BtnOptions.Click += BtnOptions_Click;
            BtnOptions.MouseDown += BtnOptions_MouseDown;
            BtnOptions.MouseEnter += BtnOptions_MouseEnter;
            BtnOptions.MouseLeave += BtnOptions_MouseLeave;
            // 
            // TmrPatchNotes
            // 
            TmrPatchNotes.Enabled = true;
            // 
            // PBarActualFile
            // 
            PBarActualFile.Location = new System.Drawing.Point(218, 661);
            PBarActualFile.Name = "PBarActualFile";
            PBarActualFile.Size = new System.Drawing.Size(328, 51);
            PBarActualFile.TabIndex = 12;
            PBarActualFile.Visible = false;
            // 
            // LblDownloadSpeed
            // 
            LblDownloadSpeed.AutoSize = true;
            LblDownloadSpeed.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblDownloadSpeed.Location = new System.Drawing.Point(522, 633);
            LblDownloadSpeed.Name = "LblDownloadSpeed";
            LblDownloadSpeed.Size = new System.Drawing.Size(103, 25);
            LblDownloadSpeed.TabIndex = 13;
            LblDownloadSpeed.Text = "@ 32 MB/s";
            // 
            // LblBytes
            // 
            LblBytes.AutoSize = true;
            LblBytes.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblBytes.Location = new System.Drawing.Point(554, 671);
            LblBytes.Name = "LblBytes";
            LblBytes.Size = new System.Drawing.Size(114, 25);
            LblBytes.TabIndex = 14;
            LblBytes.Text = "Percentages";
            // 
            // BtnInstall
            // 
            BtnInstall.BackColor = System.Drawing.Color.Black;
            BtnInstall.BackgroundImageLayout = ImageLayout.Stretch;
            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnInstall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnInstall.FlatStyle = FlatStyle.Flat;
            BtnInstall.ForeColor = System.Drawing.Color.Transparent;
            BtnInstall.Location = new System.Drawing.Point(12, 661);
            BtnInstall.Name = "BtnInstall";
            BtnInstall.Size = new System.Drawing.Size(200, 51);
            BtnInstall.TabIndex = 16;
            BtnInstall.TabStop = false;
            BtnInstall.Text = "INSTALL GAME";
            BtnInstall.UseMnemonic = false;
            BtnInstall.UseVisualStyleBackColor = false;
            BtnInstall.Click += BtnInstall_Click;
            BtnInstall.MouseDown += BtnInstall_MouseDown;
            BtnInstall.MouseEnter += BtnInstall_MouseEnter;
            BtnInstall.MouseLeave += BtnInstall_MouseLeave;
            // 
            // LblFileName
            // 
            LblFileName.AutoSize = true;
            LblFileName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblFileName.Location = new System.Drawing.Point(12, 633);
            LblFileName.Name = "LblFileName";
            LblFileName.Size = new System.Drawing.Size(88, 25);
            LblFileName.TabIndex = 17;
            LblFileName.Text = "Filename";
            // 
            // PibLoadingRing
            // 
            PibLoadingRing.BackColor = System.Drawing.Color.Black;
            PibLoadingRing.Image = (System.Drawing.Image)resources.GetObject("PibLoadingRing.Image");
            PibLoadingRing.Location = new System.Drawing.Point(587, 292);
            PibLoadingRing.Name = "PibLoadingRing";
            PibLoadingRing.Size = new System.Drawing.Size(128, 128);
            PibLoadingRing.SizeMode = PictureBoxSizeMode.StretchImage;
            PibLoadingRing.TabIndex = 18;
            PibLoadingRing.TabStop = false;
            // 
            // LblPatchNotes
            // 
            LblPatchNotes.AutoSize = true;
            LblPatchNotes.BackColor = System.Drawing.Color.Transparent;
            LblPatchNotes.Cursor = Cursors.WaitCursor;
            LblPatchNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblPatchNotes.ForeColor = System.Drawing.SystemColors.Control;
            LblPatchNotes.Location = new System.Drawing.Point(544, 488);
            LblPatchNotes.Name = "LblPatchNotes";
            LblPatchNotes.OutlineForeColor = System.Drawing.Color.Black;
            LblPatchNotes.OutlineWidth = 4F;
            LblPatchNotes.Size = new System.Drawing.Size(231, 25);
            LblPatchNotes.TabIndex = 19;
            LblPatchNotes.Text = "Loading Patch-Notes...";
            // 
            // PibLoadingBorder
            // 
            PibLoadingBorder.BackColor = System.Drawing.Color.Transparent;
            PibLoadingBorder.Image = (System.Drawing.Image)resources.GetObject("PibLoadingBorder.Image");
            PibLoadingBorder.Location = new System.Drawing.Point(527, 235);
            PibLoadingBorder.Name = "PibLoadingBorder";
            PibLoadingBorder.Size = new System.Drawing.Size(250, 250);
            PibLoadingBorder.TabIndex = 21;
            PibLoadingBorder.TabStop = false;
            // 
            // Wv2Patchnotes
            // 
            Wv2Patchnotes.AllowExternalDrop = true;
            Wv2Patchnotes.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            Wv2Patchnotes.CreationProperties = null;
            Wv2Patchnotes.DefaultBackgroundColor = System.Drawing.Color.White;
            Wv2Patchnotes.Location = new System.Drawing.Point(12, 110);
            Wv2Patchnotes.Name = "Wv2Patchnotes";
            Wv2Patchnotes.Size = new System.Drawing.Size(1256, 514);
            Wv2Patchnotes.Source = new System.Uri("https://ravo92.github.io/changelogpage/index.html", System.UriKind.Absolute);
            Wv2Patchnotes.TabIndex = 1;
            Wv2Patchnotes.ZoomFactor = 1D;
            // 
            // PiBTwitch
            // 
            PiBTwitch.BackColor = System.Drawing.Color.Black;
            PiBTwitch.Cursor = Cursors.Hand;
            PiBTwitch.Location = new System.Drawing.Point(674, 12);
            PiBTwitch.Name = "PiBTwitch";
            PiBTwitch.Size = new System.Drawing.Size(55, 55);
            PiBTwitch.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBTwitch.TabIndex = 22;
            PiBTwitch.TabStop = false;
            PiBTwitch.Click += PiBTwitch_Click;
            // 
            // PiBArrow
            // 
            PiBArrow.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            PiBArrow.BackgroundImageLayout = ImageLayout.Stretch;
            PiBArrow.Cursor = Cursors.Hand;
            PiBArrow.Image = (System.Drawing.Image)resources.GetObject("PiBArrow.Image");
            PiBArrow.Location = new System.Drawing.Point(12, 110);
            PiBArrow.Name = "PiBArrow";
            PiBArrow.Size = new System.Drawing.Size(55, 55);
            PiBArrow.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBArrow.TabIndex = 23;
            PiBArrow.TabStop = false;
            PiBArrow.Click += PiBArrow_Click;
            // 
            // TmrAnimation
            // 
            TmrAnimation.Interval = 10;
            TmrAnimation.Tick += TmrAnimation_Tick;
            // 
            // PnlPlaceholder
            // 
            PnlPlaceholder.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            PnlPlaceholder.BackgroundImageLayout = ImageLayout.Stretch;
            PnlPlaceholder.Controls.Add(PiBVersion222_7);
            PnlPlaceholder.Controls.Add(PiBVersion222_5);
            PnlPlaceholder.Controls.Add(PiBVersion222_6);
            PnlPlaceholder.Controls.Add(PiBVersion106);
            PnlPlaceholder.Controls.Add(PiBVersion103);
            PnlPlaceholder.Controls.Add(LblInstalledPatches);
            PnlPlaceholder.Controls.Add(LblModExplanation);
            PnlPlaceholder.Location = new System.Drawing.Point(12, 110);
            PnlPlaceholder.Name = "PnlPlaceholder";
            PnlPlaceholder.Size = new System.Drawing.Size(1256, 514);
            PnlPlaceholder.TabIndex = 24;
            // 
            // PiBVersion222_7
            // 
            PiBVersion222_7.BackColor = System.Drawing.Color.Black;
            PiBVersion222_7.BackgroundImageLayout = ImageLayout.Stretch;
            PiBVersion222_7.Cursor = Cursors.Hand;
            PiBVersion222_7.Image = (System.Drawing.Image)resources.GetObject("PiBVersion222_7.Image");
            PiBVersion222_7.Location = new System.Drawing.Point(716, 155);
            PiBVersion222_7.Name = "PiBVersion222_7";
            PiBVersion222_7.Size = new System.Drawing.Size(150, 200);
            PiBVersion222_7.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBVersion222_7.TabIndex = 41;
            PiBVersion222_7.TabStop = false;
            PiBVersion222_7.Click += PiBVersion222_7_Click;
            // 
            // PiBVersion222_5
            // 
            PiBVersion222_5.BackColor = System.Drawing.Color.Black;
            PiBVersion222_5.BackgroundImageLayout = ImageLayout.Stretch;
            PiBVersion222_5.Cursor = Cursors.Hand;
            PiBVersion222_5.Image = (System.Drawing.Image)resources.GetObject("PiBVersion222_5.Image");
            PiBVersion222_5.Location = new System.Drawing.Point(376, 155);
            PiBVersion222_5.Name = "PiBVersion222_5";
            PiBVersion222_5.Size = new System.Drawing.Size(150, 200);
            PiBVersion222_5.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBVersion222_5.TabIndex = 40;
            PiBVersion222_5.TabStop = false;
            PiBVersion222_5.Click += PiBVersion222_5_Click;
            // 
            // PiBVersion222_6
            // 
            PiBVersion222_6.BackColor = System.Drawing.Color.Black;
            PiBVersion222_6.BackgroundImageLayout = ImageLayout.Stretch;
            PiBVersion222_6.Cursor = Cursors.Hand;
            PiBVersion222_6.Image = (System.Drawing.Image)resources.GetObject("PiBVersion222_6.Image");
            PiBVersion222_6.Location = new System.Drawing.Point(546, 155);
            PiBVersion222_6.Name = "PiBVersion222_6";
            PiBVersion222_6.Size = new System.Drawing.Size(150, 200);
            PiBVersion222_6.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBVersion222_6.TabIndex = 37;
            PiBVersion222_6.TabStop = false;
            PiBVersion222_6.Click += PiBVersion222_6_Click;
            // 
            // PiBVersion106
            // 
            PiBVersion106.BackColor = System.Drawing.Color.Black;
            PiBVersion106.BackgroundImageLayout = ImageLayout.Stretch;
            PiBVersion106.Cursor = Cursors.Hand;
            PiBVersion106.Image = (System.Drawing.Image)resources.GetObject("PiBVersion106.Image");
            PiBVersion106.Location = new System.Drawing.Point(206, 155);
            PiBVersion106.Name = "PiBVersion106";
            PiBVersion106.Size = new System.Drawing.Size(150, 200);
            PiBVersion106.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBVersion106.TabIndex = 26;
            PiBVersion106.TabStop = false;
            PiBVersion106.Click += PiBVersion106_Click;
            // 
            // PiBVersion103
            // 
            PiBVersion103.BackColor = System.Drawing.Color.Black;
            PiBVersion103.BackgroundImageLayout = ImageLayout.Stretch;
            PiBVersion103.Cursor = Cursors.Hand;
            PiBVersion103.Image = (System.Drawing.Image)resources.GetObject("PiBVersion103.Image");
            PiBVersion103.Location = new System.Drawing.Point(36, 155);
            PiBVersion103.Name = "PiBVersion103";
            PiBVersion103.Size = new System.Drawing.Size(150, 200);
            PiBVersion103.SizeMode = PictureBoxSizeMode.StretchImage;
            PiBVersion103.TabIndex = 25;
            PiBVersion103.TabStop = false;
            PiBVersion103.Click += PiBVersion103_Click;
            // 
            // LblInstalledPatches
            // 
            LblInstalledPatches.AutoSize = true;
            LblInstalledPatches.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblInstalledPatches.Location = new System.Drawing.Point(525, 35);
            LblInstalledPatches.Name = "LblInstalledPatches";
            LblInstalledPatches.OutlineForeColor = System.Drawing.Color.Black;
            LblInstalledPatches.OutlineWidth = 4F;
            LblInstalledPatches.Size = new System.Drawing.Size(138, 47);
            LblInstalledPatches.TabIndex = 28;
            LblInstalledPatches.Text = "Patches";
            // 
            // LblModExplanation
            // 
            LblModExplanation.AutoSize = true;
            LblModExplanation.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblModExplanation.Location = new System.Drawing.Point(23, 425);
            LblModExplanation.Name = "LblModExplanation";
            LblModExplanation.OutlineForeColor = System.Drawing.Color.Black;
            LblModExplanation.OutlineWidth = 4F;
            LblModExplanation.Size = new System.Drawing.Size(945, 47);
            LblModExplanation.TabIndex = 29;
            LblModExplanation.Text = "Here you can choose which mod or patch you want to play.";
            // 
            // SysTray
            // 
            SysTray.BalloonTipIcon = ToolTipIcon.Info;
            SysTray.BalloonTipText = "Launcher is minimized to SysTray. If you want to Close, press \"X\" in the App or rightclick on the Icon in SysTray.";
            SysTray.BalloonTipTitle = "Launcher is still running.";
            SysTray.Icon = (System.Drawing.Icon)resources.GetObject("SysTray.Icon");
            SysTray.Text = "BFME 2.22 Launcher";
            SysTray.MouseDoubleClick += SysTray_MouseDoubleClick;
            // 
            // NotifyContextMenu
            // 
            NotifyContextMenu.Items.AddRange(new ToolStripItem[] { MenuItemLaunchGame, closeTheLauncherToolStripMenuItem });
            NotifyContextMenu.Name = "NotifyContextMenu";
            NotifyContextMenu.Size = new System.Drawing.Size(176, 48);
            // 
            // MenuItemLaunchGame
            // 
            MenuItemLaunchGame.Name = "MenuItemLaunchGame";
            MenuItemLaunchGame.Size = new System.Drawing.Size(175, 22);
            MenuItemLaunchGame.Text = "Launch the Game";
            MenuItemLaunchGame.Click += MenuItemLaunchGame_Click;
            // 
            // closeTheLauncherToolStripMenuItem
            // 
            closeTheLauncherToolStripMenuItem.Name = "closeTheLauncherToolStripMenuItem";
            closeTheLauncherToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            closeTheLauncherToolStripMenuItem.Text = "Close the Launcher";
            closeTheLauncherToolStripMenuItem.Click += CloseTheLauncherToolStripMenuItem_Click;
            // 
            // BtnAdvanced
            // 
            BtnAdvanced.BackColor = System.Drawing.Color.Black;
            BtnAdvanced.BackgroundImageLayout = ImageLayout.Stretch;
            BtnAdvanced.FlatAppearance.BorderSize = 0;
            BtnAdvanced.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnAdvanced.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnAdvanced.FlatStyle = FlatStyle.Flat;
            BtnAdvanced.ForeColor = System.Drawing.Color.Transparent;
            BtnAdvanced.Location = new System.Drawing.Point(839, 661);
            BtnAdvanced.Name = "BtnAdvanced";
            BtnAdvanced.Size = new System.Drawing.Size(200, 51);
            BtnAdvanced.TabIndex = 42;
            BtnAdvanced.TabStop = false;
            BtnAdvanced.Text = "ADVANCED";
            BtnAdvanced.UseMnemonic = false;
            BtnAdvanced.UseVisualStyleBackColor = false;
            BtnAdvanced.Click += BtnOpenAppDataFolder_Click;
            BtnAdvanced.MouseDown += BtnOpenAppDataFolder_MouseDown;
            BtnAdvanced.MouseEnter += BtnOpenAppDataFolder_MouseEnter;
            BtnAdvanced.MouseLeave += BtnOpenAppDataFolder_MouseLeave;
            // 
            // PibMute
            // 
            PibMute.BackColor = System.Drawing.Color.Transparent;
            PibMute.Cursor = Cursors.Hand;
            PibMute.Image = (System.Drawing.Image)resources.GetObject("PibMute.Image");
            PibMute.Location = new System.Drawing.Point(1213, 11);
            PibMute.Name = "PibMute";
            PibMute.Size = new System.Drawing.Size(55, 55);
            PibMute.SizeMode = PictureBoxSizeMode.StretchImage;
            PibMute.TabIndex = 43;
            PibMute.TabStop = false;
            PibMute.Click += PibMute_Click;
            // 
            // BFME1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.ActiveBorder;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1280, 720);
            Controls.Add(PibMute);
            Controls.Add(BtnAdvanced);
            Controls.Add(PiBArrow);
            Controls.Add(PiBTwitch);
            Controls.Add(LblPatchNotes);
            Controls.Add(LblFileName);
            Controls.Add(BtnInstall);
            Controls.Add(LblBytes);
            Controls.Add(LblDownloadSpeed);
            Controls.Add(PBarActualFile);
            Controls.Add(BtnOptions);
            Controls.Add(BtnLaunch);
            Controls.Add(PiBThemeSwitcher);
            Controls.Add(PiBModDB);
            Controls.Add(PiBDiscord);
            Controls.Add(PiBYoutube);
            Controls.Add(PibHeader);
            Controls.Add(PibLoadingRing);
            Controls.Add(PibLoadingBorder);
            Controls.Add(Wv2Patchnotes);
            Controls.Add(PnlPlaceholder);
            DoubleBuffered = true;
            ForeColor = System.Drawing.SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "BFME1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bfme 2.22 Launcher";
            FormClosing += BFME1_FormClosing;
            Shown += BFME1_Shown;
            Resize += BFME1_Resize;
            ((System.ComponentModel.ISupportInitialize)PibHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBYoutube).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBDiscord).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBModDB).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBThemeSwitcher).EndInit();
            ((System.ComponentModel.ISupportInitialize)PibLoadingRing).EndInit();
            ((System.ComponentModel.ISupportInitialize)PibLoadingBorder).EndInit();
            ((System.ComponentModel.ISupportInitialize)Wv2Patchnotes).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBTwitch).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBArrow).EndInit();
            PnlPlaceholder.ResumeLayout(false);
            PnlPlaceholder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_5).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion106).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion103).EndInit();
            NotifyContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PibMute).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private PictureBox PiBVersion106;
        private PictureBox PiBVersion103;
        private Helper.CustomLabel LblInstalledPatches;
        private Helper.CustomLabel LblModExplanation;
        private PictureBox PiBVersion222_6;
        private PictureBox PiBVersion222_5;
        private NotifyIcon SysTray;
        private ContextMenuStrip NotifyContextMenu;
        private ToolStripMenuItem MenuItemLaunchGame;
        private ToolStripMenuItem closeTheLauncherToolStripMenuItem;
        private Button BtnAdvanced;
        private PictureBox PibMute;
        private PictureBox PiBVersion222_7;
    }
}