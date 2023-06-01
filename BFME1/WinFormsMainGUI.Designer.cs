using System.Windows.Forms;

namespace PatchLauncher
{
    partial class WinFormsMainGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinFormsMainGUI));
            PibHeader = new PictureBox();
            PiBYoutube = new PictureBox();
            PiBDiscord = new PictureBox();
            PiBModDB = new PictureBox();
            PiBThemeSwitcher = new PictureBox();
            ToolTip = new ToolTip(components);
            TmrPatchNotes = new Timer(components);
            LblDownloadSpeed = new Label();
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
            PibMute = new PictureBox();
            WinFormsMainMenuStrip = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            LaunchGameToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            OptionsToolStripMenuItem = new ToolStripMenuItem();
            LauncherSettingsToolStripMenuItem = new ToolStripMenuItem();
            GameSettingsToolStripMenuItem = new ToolStripMenuItem();
            AdvancedToolStripMenuItem = new ToolStripMenuItem();
            OpenLauncherDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            OpenGameDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            OpenMapDirectoryToolStripMenuItem = new ToolStripMenuItem();
            OpenSaveDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            RepairGameToolStripMenuItem = new ToolStripMenuItem();
            AboutToolStripMenuItem = new ToolStripMenuItem();
            PBarActualFile = new Helper.CustomProgressBar();
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
            WinFormsMainMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // PibHeader
            // 
            PibHeader.BackColor = System.Drawing.Color.Transparent;
            PibHeader.BackgroundImageLayout = ImageLayout.Center;
            PibHeader.ErrorImage = null;
            PibHeader.Image = (System.Drawing.Image)resources.GetObject("PibHeader.Image");
            PibHeader.InitialImage = null;
            PibHeader.Location = new System.Drawing.Point(247, 23);
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
            PiBYoutube.Location = new System.Drawing.Point(491, 35);
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
            PiBDiscord.Location = new System.Drawing.Point(552, 35);
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
            PiBModDB.Location = new System.Drawing.Point(613, 35);
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
            PiBThemeSwitcher.Location = new System.Drawing.Point(735, 35);
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
            // TmrPatchNotes
            // 
            TmrPatchNotes.Enabled = true;
            // 
            // LblDownloadSpeed
            // 
            LblDownloadSpeed.AutoSize = true;
            LblDownloadSpeed.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblDownloadSpeed.Location = new System.Drawing.Point(346, 684);
            LblDownloadSpeed.Name = "LblDownloadSpeed";
            LblDownloadSpeed.Size = new System.Drawing.Size(103, 25);
            LblDownloadSpeed.TabIndex = 13;
            LblDownloadSpeed.Text = "@ 32 MB/s";
            LblDownloadSpeed.Visible = false;
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
            BtnInstall.Location = new System.Drawing.Point(12, 685);
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
            LblFileName.Location = new System.Drawing.Point(346, 712);
            LblFileName.Name = "LblFileName";
            LblFileName.Size = new System.Drawing.Size(88, 25);
            LblFileName.TabIndex = 17;
            LblFileName.Text = "Filename";
            LblFileName.Visible = false;
            // 
            // PibLoadingRing
            // 
            PibLoadingRing.BackColor = System.Drawing.Color.Black;
            PibLoadingRing.Image = (System.Drawing.Image)resources.GetObject("PibLoadingRing.Image");
            PibLoadingRing.Location = new System.Drawing.Point(587, 316);
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
            LblPatchNotes.Location = new System.Drawing.Point(544, 512);
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
            PibLoadingBorder.Location = new System.Drawing.Point(527, 259);
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
            Wv2Patchnotes.Location = new System.Drawing.Point(12, 134);
            Wv2Patchnotes.Name = "Wv2Patchnotes";
            Wv2Patchnotes.Size = new System.Drawing.Size(1256, 538);
            Wv2Patchnotes.Source = new System.Uri("https://ravo92.github.io/changelogpage/index.html", System.UriKind.Absolute);
            Wv2Patchnotes.TabIndex = 1;
            Wv2Patchnotes.ZoomFactor = 1D;
            // 
            // PiBTwitch
            // 
            PiBTwitch.BackColor = System.Drawing.Color.Black;
            PiBTwitch.Cursor = Cursors.Hand;
            PiBTwitch.Location = new System.Drawing.Point(674, 35);
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
            PiBArrow.Location = new System.Drawing.Point(12, 134);
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
            PnlPlaceholder.Location = new System.Drawing.Point(12, 134);
            PnlPlaceholder.Name = "PnlPlaceholder";
            PnlPlaceholder.Size = new System.Drawing.Size(1256, 538);
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
            // 
            // closeTheLauncherToolStripMenuItem
            // 
            closeTheLauncherToolStripMenuItem.Name = "closeTheLauncherToolStripMenuItem";
            closeTheLauncherToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            closeTheLauncherToolStripMenuItem.Text = "Close the Launcher";
            closeTheLauncherToolStripMenuItem.Click += CloseTheLauncherToolStripMenuItem_Click;
            // 
            // PibMute
            // 
            PibMute.BackColor = System.Drawing.Color.Transparent;
            PibMute.Cursor = Cursors.Hand;
            PibMute.Image = (System.Drawing.Image)resources.GetObject("PibMute.Image");
            PibMute.Location = new System.Drawing.Point(1213, 35);
            PibMute.Name = "PibMute";
            PibMute.Size = new System.Drawing.Size(55, 55);
            PibMute.SizeMode = PictureBoxSizeMode.StretchImage;
            PibMute.TabIndex = 43;
            PibMute.TabStop = false;
            PibMute.Click += PibMute_Click;
            // 
            // WinFormsMainMenuStrip
            // 
            WinFormsMainMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, OptionsToolStripMenuItem, AdvancedToolStripMenuItem, AboutToolStripMenuItem });
            WinFormsMainMenuStrip.Location = new System.Drawing.Point(0, 0);
            WinFormsMainMenuStrip.Name = "WinFormsMainMenuStrip";
            WinFormsMainMenuStrip.Size = new System.Drawing.Size(1280, 24);
            WinFormsMainMenuStrip.TabIndex = 44;
            WinFormsMainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { LaunchGameToolStripMenuItem, toolStripSeparator1, ExitToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            FileToolStripMenuItem.Text = "File";
            // 
            // LaunchGameToolStripMenuItem
            // 
            LaunchGameToolStripMenuItem.Name = "LaunchGameToolStripMenuItem";
            LaunchGameToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            LaunchGameToolStripMenuItem.Text = "Launch Game";
            LaunchGameToolStripMenuItem.Click += LaunchGameToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            ExitToolStripMenuItem.Text = "Exit";
            ExitToolStripMenuItem.Click += CloseTheLauncherToolStripMenuItem_Click;
            // 
            // OptionsToolStripMenuItem
            // 
            OptionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { LauncherSettingsToolStripMenuItem, GameSettingsToolStripMenuItem });
            OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            OptionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            OptionsToolStripMenuItem.Text = "Options";
            // 
            // LauncherSettingsToolStripMenuItem
            // 
            LauncherSettingsToolStripMenuItem.Name = "LauncherSettingsToolStripMenuItem";
            LauncherSettingsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            LauncherSettingsToolStripMenuItem.Text = "Launcher Settings";
            // 
            // GameSettingsToolStripMenuItem
            // 
            GameSettingsToolStripMenuItem.Name = "GameSettingsToolStripMenuItem";
            GameSettingsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            GameSettingsToolStripMenuItem.Text = "Game Settings";
            // 
            // AdvancedToolStripMenuItem
            // 
            AdvancedToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenLauncherDirectoryToolStripMenuItem, toolStripSeparator2, OpenGameDirectoryToolStripMenuItem, toolStripSeparator3, OpenMapDirectoryToolStripMenuItem, OpenSaveDirectoryToolStripMenuItem, toolStripSeparator4, RepairGameToolStripMenuItem });
            AdvancedToolStripMenuItem.Name = "AdvancedToolStripMenuItem";
            AdvancedToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            AdvancedToolStripMenuItem.Text = "Advanced";
            // 
            // OpenLauncherDirectoryToolStripMenuItem
            // 
            OpenLauncherDirectoryToolStripMenuItem.Name = "OpenLauncherDirectoryToolStripMenuItem";
            OpenLauncherDirectoryToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            OpenLauncherDirectoryToolStripMenuItem.Text = "Open Launcher Directory";
            OpenLauncherDirectoryToolStripMenuItem.Click += OpenLauncherDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(265, 6);
            // 
            // OpenGameDirectoryToolStripMenuItem
            // 
            OpenGameDirectoryToolStripMenuItem.Name = "OpenGameDirectoryToolStripMenuItem";
            OpenGameDirectoryToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            OpenGameDirectoryToolStripMenuItem.Text = "Open Game Directory";
            OpenGameDirectoryToolStripMenuItem.Click += OpenGameDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(265, 6);
            // 
            // OpenMapDirectoryToolStripMenuItem
            // 
            OpenMapDirectoryToolStripMenuItem.Name = "OpenMapDirectoryToolStripMenuItem";
            OpenMapDirectoryToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            OpenMapDirectoryToolStripMenuItem.Text = "Open Custom Map Directory";
            OpenMapDirectoryToolStripMenuItem.Click += OpenMapDirectoryToolStripMenuItem_Click;
            // 
            // OpenSaveDirectoryToolStripMenuItem
            // 
            OpenSaveDirectoryToolStripMenuItem.Name = "OpenSaveDirectoryToolStripMenuItem";
            OpenSaveDirectoryToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            OpenSaveDirectoryToolStripMenuItem.Text = "Open Save Directory";
            OpenSaveDirectoryToolStripMenuItem.Click += OpenSaveDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(265, 6);
            // 
            // RepairGameToolStripMenuItem
            // 
            RepairGameToolStripMenuItem.Name = "RepairGameToolStripMenuItem";
            RepairGameToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            RepairGameToolStripMenuItem.Text = "Analyze Game Files and Repair Game";
            RepairGameToolStripMenuItem.Click += RepairGameToolStripMenuItem_Click;
            // 
            // AboutToolStripMenuItem
            // 
            AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            AboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            AboutToolStripMenuItem.Text = "About";
            AboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // PBarActualFile
            // 
            PBarActualFile.BackColor = System.Drawing.Color.Black;
            PBarActualFile.CustomText = null;
            PBarActualFile.DisplayStyle = Helper.ProgressBarDisplayText.Percentage;
            PBarActualFile.ForeColor = System.Drawing.Color.Transparent;
            PBarActualFile.Location = new System.Drawing.Point(12, 684);
            PBarActualFile.Name = "PBarActualFile";
            PBarActualFile.Size = new System.Drawing.Size(328, 52);
            PBarActualFile.Style = ProgressBarStyle.Continuous;
            PBarActualFile.TabIndex = 45;
            PBarActualFile.Visible = false;
            // 
            // WinFormsMainGUI
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.ActiveBorder;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1280, 744);
            Controls.Add(PBarActualFile);
            Controls.Add(BtnInstall);
            Controls.Add(WinFormsMainMenuStrip);
            Controls.Add(PibMute);
            Controls.Add(PiBArrow);
            Controls.Add(PiBTwitch);
            Controls.Add(LblPatchNotes);
            Controls.Add(LblFileName);
            Controls.Add(LblDownloadSpeed);
            Controls.Add(PiBThemeSwitcher);
            Controls.Add(PiBModDB);
            Controls.Add(PiBDiscord);
            Controls.Add(PiBYoutube);
            Controls.Add(PibHeader);
            Controls.Add(PibLoadingRing);
            Controls.Add(PibLoadingBorder);
            Controls.Add(PnlPlaceholder);
            Controls.Add(Wv2Patchnotes);
            DoubleBuffered = true;
            ForeColor = System.Drawing.SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "WinFormsMainGUI";
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
            WinFormsMainMenuStrip.ResumeLayout(false);
            WinFormsMainMenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox PibHeader;
        private PictureBox PiBYoutube;
        private PictureBox PiBDiscord;
        private PictureBox PiBModDB;
        private PictureBox PiBThemeSwitcher;
        private ToolTip ToolTip;
        private Timer TmrPatchNotes;
        private Label LblDownloadSpeed;
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
        private PictureBox PibMute;
        private PictureBox PiBVersion222_7;
        private MenuStrip WinFormsMainMenuStrip;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem LaunchGameToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem OptionsToolStripMenuItem;
        private ToolStripMenuItem LauncherSettingsToolStripMenuItem;
        private ToolStripMenuItem GameSettingsToolStripMenuItem;
        private ToolStripMenuItem AdvancedToolStripMenuItem;
        private ToolStripMenuItem OpenGameDirectoryToolStripMenuItem;
        private ToolStripMenuItem OpenLauncherDirectoryToolStripMenuItem;
        private ToolStripMenuItem OpenSaveDirectoryToolStripMenuItem;
        private ToolStripMenuItem OpenMapDirectoryToolStripMenuItem;
        private ToolStripMenuItem AboutToolStripMenuItem;
        private ToolStripMenuItem RepairGameToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private Helper.CustomProgressBar PBarActualFile;
    }
}