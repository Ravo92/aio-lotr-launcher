using System.Windows.Forms;
using Helper.UserControls;

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
            LblWorkerFileName = new Label();
            BtnInstall = new Button();
            LblWorkerIOTask = new Label();
            PibLoadingRing = new PictureBox();
            LblPatchNotes = new CustomLabel();
            PibLoadingBorder = new PictureBox();
            Wv2Patchnotes = new Microsoft.Web.WebView2.WinForms.WebView2();
            PiBTwitch = new PictureBox();
            PiBArrow = new PictureBox();
            PnlPlaceholder = new FlowLayoutPanel();
            PiBVersion103 = new PictureBox();
            PiBVersion106 = new PictureBox();
            PiBVersion222_33 = new PictureBox();
            PiBVersion222_34 = new PictureBox();
            LblModExplanation = new CustomLabel();
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
            toolStripSeparator5 = new ToolStripSeparator();
            GameDesktopShortcutToolStripMenuItem = new ToolStripMenuItem();
            GameStartmenuShortcutsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            LauncherDesktopShortcutToolStripMenuItem = new ToolStripMenuItem();
            LauncherStartmenuShortcutToolStripMenuItem = new ToolStripMenuItem();
            AdvancedToolStripMenuItem = new ToolStripMenuItem();
            OpenLauncherDirectoryToolStripMenuItem = new ToolStripMenuItem();
            openLauncherLogfileDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            OpenGameDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            OpenMapDirectoryToolStripMenuItem = new ToolStripMenuItem();
            OpenSaveDirectoryToolStripMenuItem = new ToolStripMenuItem();
            openReplayDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            RepairGameToolStripMenuItem = new ToolStripMenuItem();
            AboutToolStripMenuItem = new ToolStripMenuItem();
            CreditsToolStripMenuItem = new ToolStripMenuItem();
            MessagesFromTheTeamToolStripMenuItem = new ToolStripMenuItem();
            PBarActualFile = new CustomProgressBar();
            TmrPatchNotes = new Timer(components);
            TmrAnimation = new Timer(components);
            SysTray = new NotifyIcon(components);
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
            ((System.ComponentModel.ISupportInitialize)PiBVersion103).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion106).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_34).BeginInit();
            NotifyContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PibMute).BeginInit();
            WinFormsMainMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // PibHeader
            // 
            PibHeader.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(PibHeader, "PibHeader");
            PibHeader.Name = "PibHeader";
            PibHeader.TabStop = false;
            // 
            // PiBYoutube
            // 
            PiBYoutube.BackColor = System.Drawing.Color.Black;
            PiBYoutube.Cursor = Cursors.Hand;
            resources.ApplyResources(PiBYoutube, "PiBYoutube");
            PiBYoutube.Name = "PiBYoutube";
            PiBYoutube.TabStop = false;
            PiBYoutube.Click += PiBYoutube_Click;
            // 
            // PiBDiscord
            // 
            PiBDiscord.BackColor = System.Drawing.Color.Black;
            PiBDiscord.Cursor = Cursors.Hand;
            resources.ApplyResources(PiBDiscord, "PiBDiscord");
            PiBDiscord.Name = "PiBDiscord";
            PiBDiscord.TabStop = false;
            PiBDiscord.Click += PiBDiscord_Click;
            // 
            // PiBModDB
            // 
            PiBModDB.BackColor = System.Drawing.Color.Black;
            PiBModDB.Cursor = Cursors.Hand;
            resources.ApplyResources(PiBModDB, "PiBModDB");
            PiBModDB.Name = "PiBModDB";
            PiBModDB.TabStop = false;
            PiBModDB.Click += PiBModDB_Click;
            // 
            // PiBThemeSwitcher
            // 
            PiBThemeSwitcher.BackColor = System.Drawing.Color.Black;
            PiBThemeSwitcher.Cursor = Cursors.Hand;
            resources.ApplyResources(PiBThemeSwitcher, "PiBThemeSwitcher");
            PiBThemeSwitcher.Name = "PiBThemeSwitcher";
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
            // LblWorkerFileName
            // 
            resources.ApplyResources(LblWorkerFileName, "LblWorkerFileName");
            LblWorkerFileName.Name = "LblWorkerFileName";
            // 
            // BtnInstall
            // 
            BtnInstall.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(BtnInstall, "BtnInstall");
            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnInstall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnInstall.ForeColor = System.Drawing.Color.Transparent;
            BtnInstall.Name = "BtnInstall";
            BtnInstall.TabStop = false;
            BtnInstall.UseMnemonic = false;
            BtnInstall.UseVisualStyleBackColor = false;
            BtnInstall.Click += BtnInstall_Click;
            BtnInstall.MouseDown += BtnInstall_MouseDown;
            BtnInstall.MouseEnter += BtnInstall_MouseEnter;
            BtnInstall.MouseLeave += BtnInstall_MouseLeave;
            // 
            // LblWorkerIOTask
            // 
            resources.ApplyResources(LblWorkerIOTask, "LblWorkerIOTask");
            LblWorkerIOTask.Name = "LblWorkerIOTask";
            // 
            // PibLoadingRing
            // 
            PibLoadingRing.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(PibLoadingRing, "PibLoadingRing");
            PibLoadingRing.Name = "PibLoadingRing";
            PibLoadingRing.TabStop = false;
            // 
            // LblPatchNotes
            // 
            resources.ApplyResources(LblPatchNotes, "LblPatchNotes");
            LblPatchNotes.BackColor = System.Drawing.Color.Transparent;
            LblPatchNotes.Cursor = Cursors.WaitCursor;
            LblPatchNotes.ForeColor = System.Drawing.SystemColors.Control;
            LblPatchNotes.Name = "LblPatchNotes";
            LblPatchNotes.OutlineForeColor = System.Drawing.Color.Black;
            LblPatchNotes.OutlineWidth = 4F;
            // 
            // PibLoadingBorder
            // 
            PibLoadingBorder.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(PibLoadingBorder, "PibLoadingBorder");
            PibLoadingBorder.Name = "PibLoadingBorder";
            PibLoadingBorder.TabStop = false;
            // 
            // Wv2Patchnotes
            // 
            Wv2Patchnotes.AllowExternalDrop = true;
            Wv2Patchnotes.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            Wv2Patchnotes.CreationProperties = null;
            Wv2Patchnotes.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(Wv2Patchnotes, "Wv2Patchnotes");
            Wv2Patchnotes.Name = "Wv2Patchnotes";
            Wv2Patchnotes.Source = new System.Uri("https://ravo92.github.io/changelogpage/index.html", System.UriKind.Absolute);
            Wv2Patchnotes.ZoomFactor = 1D;
            // 
            // PiBTwitch
            // 
            PiBTwitch.BackColor = System.Drawing.Color.Black;
            PiBTwitch.Cursor = Cursors.Hand;
            resources.ApplyResources(PiBTwitch, "PiBTwitch");
            PiBTwitch.Name = "PiBTwitch";
            PiBTwitch.TabStop = false;
            PiBTwitch.Click += PiBTwitch_Click;
            // 
            // PiBArrow
            // 
            PiBArrow.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(PiBArrow, "PiBArrow");
            PiBArrow.Cursor = Cursors.Hand;
            PiBArrow.Name = "PiBArrow";
            PiBArrow.TabStop = false;
            PiBArrow.Click += PiBArrow_Click;
            // 
            // PnlPlaceholder
            // 
            resources.ApplyResources(PnlPlaceholder, "PnlPlaceholder");
            PnlPlaceholder.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            PnlPlaceholder.Controls.Add(PiBVersion103);
            PnlPlaceholder.Controls.Add(PiBVersion106);
            PnlPlaceholder.Controls.Add(PiBVersion222_33);
            PnlPlaceholder.Controls.Add(PiBVersion222_34);
            PnlPlaceholder.Name = "PnlPlaceholder";
            // 
            // PiBVersion103
            // 
            PiBVersion103.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(PiBVersion103, "PiBVersion103");
            PiBVersion103.Cursor = Cursors.Hand;
            PiBVersion103.Name = "PiBVersion103";
            PiBVersion103.TabStop = false;
            PiBVersion103.Click += PiBVersion103_Click;
            // 
            // PiBVersion106
            // 
            PiBVersion106.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(PiBVersion106, "PiBVersion106");
            PiBVersion106.Cursor = Cursors.Hand;
            PiBVersion106.Name = "PiBVersion106";
            PiBVersion106.TabStop = false;
            PiBVersion106.Click += PiBVersion106_Click;
            // 
            // PiBVersion222_33
            // 
            PiBVersion222_33.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(PiBVersion222_33, "PiBVersion222_33");
            PiBVersion222_33.Cursor = Cursors.Hand;
            PiBVersion222_33.Name = "PiBVersion222_33";
            PiBVersion222_33.TabStop = false;
            PiBVersion222_33.Click += PiBVersion222_33_Click;
            // 
            // PiBVersion222_34
            // 
            PiBVersion222_34.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(PiBVersion222_34, "PiBVersion222_34");
            PiBVersion222_34.Cursor = Cursors.Hand;
            PiBVersion222_34.Name = "PiBVersion222_34";
            PiBVersion222_34.TabStop = false;
            PiBVersion222_34.Click += PiBVersion222_34_Click;
            // 
            // LblModExplanation
            // 
            resources.ApplyResources(LblModExplanation, "LblModExplanation");
            LblModExplanation.Name = "LblModExplanation";
            LblModExplanation.OutlineForeColor = System.Drawing.Color.Black;
            LblModExplanation.OutlineWidth = 4F;
            // 
            // NotifyContextMenu
            // 
            NotifyContextMenu.Items.AddRange(new ToolStripItem[] { MenuItemLaunchGame, closeTheLauncherToolStripMenuItem });
            NotifyContextMenu.Name = "NotifyContextMenu";
            resources.ApplyResources(NotifyContextMenu, "NotifyContextMenu");
            // 
            // MenuItemLaunchGame
            // 
            MenuItemLaunchGame.Name = "MenuItemLaunchGame";
            resources.ApplyResources(MenuItemLaunchGame, "MenuItemLaunchGame");
            // 
            // closeTheLauncherToolStripMenuItem
            // 
            closeTheLauncherToolStripMenuItem.Name = "closeTheLauncherToolStripMenuItem";
            resources.ApplyResources(closeTheLauncherToolStripMenuItem, "closeTheLauncherToolStripMenuItem");
            closeTheLauncherToolStripMenuItem.Click += CloseTheLauncherToolStripMenuItem_Click;
            // 
            // PibMute
            // 
            PibMute.BackColor = System.Drawing.Color.Transparent;
            PibMute.Cursor = Cursors.Hand;
            resources.ApplyResources(PibMute, "PibMute");
            PibMute.Name = "PibMute";
            PibMute.TabStop = false;
            PibMute.Click += PibMute_Click;
            // 
            // WinFormsMainMenuStrip
            // 
            WinFormsMainMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, OptionsToolStripMenuItem, AdvancedToolStripMenuItem, AboutToolStripMenuItem });
            resources.ApplyResources(WinFormsMainMenuStrip, "WinFormsMainMenuStrip");
            WinFormsMainMenuStrip.Name = "WinFormsMainMenuStrip";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { LaunchGameToolStripMenuItem, toolStripSeparator1, ExitToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            resources.ApplyResources(FileToolStripMenuItem, "FileToolStripMenuItem");
            // 
            // LaunchGameToolStripMenuItem
            // 
            LaunchGameToolStripMenuItem.Name = "LaunchGameToolStripMenuItem";
            resources.ApplyResources(LaunchGameToolStripMenuItem, "LaunchGameToolStripMenuItem");
            LaunchGameToolStripMenuItem.Click += LaunchGameToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            resources.ApplyResources(ExitToolStripMenuItem, "ExitToolStripMenuItem");
            ExitToolStripMenuItem.Click += CloseTheLauncherToolStripMenuItem_Click;
            // 
            // OptionsToolStripMenuItem
            // 
            OptionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { LauncherSettingsToolStripMenuItem, GameSettingsToolStripMenuItem, toolStripSeparator5, GameDesktopShortcutToolStripMenuItem, GameStartmenuShortcutsToolStripMenuItem, toolStripSeparator6, LauncherDesktopShortcutToolStripMenuItem, LauncherStartmenuShortcutToolStripMenuItem });
            OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            resources.ApplyResources(OptionsToolStripMenuItem, "OptionsToolStripMenuItem");
            // 
            // LauncherSettingsToolStripMenuItem
            // 
            LauncherSettingsToolStripMenuItem.Name = "LauncherSettingsToolStripMenuItem";
            resources.ApplyResources(LauncherSettingsToolStripMenuItem, "LauncherSettingsToolStripMenuItem");
            LauncherSettingsToolStripMenuItem.Click += LauncherSettingsToolStripMenuItem_Click;
            // 
            // GameSettingsToolStripMenuItem
            // 
            GameSettingsToolStripMenuItem.Name = "GameSettingsToolStripMenuItem";
            resources.ApplyResources(GameSettingsToolStripMenuItem, "GameSettingsToolStripMenuItem");
            GameSettingsToolStripMenuItem.Click += GameSettingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            // 
            // GameDesktopShortcutToolStripMenuItem
            // 
            GameDesktopShortcutToolStripMenuItem.CheckOnClick = true;
            GameDesktopShortcutToolStripMenuItem.Name = "GameDesktopShortcutToolStripMenuItem";
            resources.ApplyResources(GameDesktopShortcutToolStripMenuItem, "GameDesktopShortcutToolStripMenuItem");
            GameDesktopShortcutToolStripMenuItem.Click += GameDesktopShortcutToolStripMenuItem_Click;
            // 
            // GameStartmenuShortcutsToolStripMenuItem
            // 
            GameStartmenuShortcutsToolStripMenuItem.CheckOnClick = true;
            GameStartmenuShortcutsToolStripMenuItem.Name = "GameStartmenuShortcutsToolStripMenuItem";
            resources.ApplyResources(GameStartmenuShortcutsToolStripMenuItem, "GameStartmenuShortcutsToolStripMenuItem");
            GameStartmenuShortcutsToolStripMenuItem.Click += GameStartmenuShortcutsToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
            // 
            // LauncherDesktopShortcutToolStripMenuItem
            // 
            LauncherDesktopShortcutToolStripMenuItem.CheckOnClick = true;
            LauncherDesktopShortcutToolStripMenuItem.Name = "LauncherDesktopShortcutToolStripMenuItem";
            resources.ApplyResources(LauncherDesktopShortcutToolStripMenuItem, "LauncherDesktopShortcutToolStripMenuItem");
            LauncherDesktopShortcutToolStripMenuItem.Click += LauncherDesktopShortcutToolStripMenuItem_Click;
            // 
            // LauncherStartmenuShortcutToolStripMenuItem
            // 
            LauncherStartmenuShortcutToolStripMenuItem.CheckOnClick = true;
            LauncherStartmenuShortcutToolStripMenuItem.Name = "LauncherStartmenuShortcutToolStripMenuItem";
            resources.ApplyResources(LauncherStartmenuShortcutToolStripMenuItem, "LauncherStartmenuShortcutToolStripMenuItem");
            LauncherStartmenuShortcutToolStripMenuItem.Click += LauncherStartmenuShortcutToolStripMenuItem_Click;
            // 
            // AdvancedToolStripMenuItem
            // 
            AdvancedToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenLauncherDirectoryToolStripMenuItem, openLauncherLogfileDirectoryToolStripMenuItem, toolStripSeparator2, OpenGameDirectoryToolStripMenuItem, toolStripSeparator3, OpenMapDirectoryToolStripMenuItem, OpenSaveDirectoryToolStripMenuItem, openReplayDirectoryToolStripMenuItem, toolStripSeparator4, RepairGameToolStripMenuItem });
            AdvancedToolStripMenuItem.Name = "AdvancedToolStripMenuItem";
            resources.ApplyResources(AdvancedToolStripMenuItem, "AdvancedToolStripMenuItem");
            // 
            // OpenLauncherDirectoryToolStripMenuItem
            // 
            OpenLauncherDirectoryToolStripMenuItem.Name = "OpenLauncherDirectoryToolStripMenuItem";
            resources.ApplyResources(OpenLauncherDirectoryToolStripMenuItem, "OpenLauncherDirectoryToolStripMenuItem");
            OpenLauncherDirectoryToolStripMenuItem.Click += OpenLauncherDirectoryToolStripMenuItem_Click;
            // 
            // openLauncherLogfileDirectoryToolStripMenuItem
            // 
            openLauncherLogfileDirectoryToolStripMenuItem.Name = "openLauncherLogfileDirectoryToolStripMenuItem";
            resources.ApplyResources(openLauncherLogfileDirectoryToolStripMenuItem, "openLauncherLogfileDirectoryToolStripMenuItem");
            openLauncherLogfileDirectoryToolStripMenuItem.Click += OpenLauncherLogfileDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // OpenGameDirectoryToolStripMenuItem
            // 
            OpenGameDirectoryToolStripMenuItem.Name = "OpenGameDirectoryToolStripMenuItem";
            resources.ApplyResources(OpenGameDirectoryToolStripMenuItem, "OpenGameDirectoryToolStripMenuItem");
            OpenGameDirectoryToolStripMenuItem.Click += OpenGameDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // OpenMapDirectoryToolStripMenuItem
            // 
            OpenMapDirectoryToolStripMenuItem.Name = "OpenMapDirectoryToolStripMenuItem";
            resources.ApplyResources(OpenMapDirectoryToolStripMenuItem, "OpenMapDirectoryToolStripMenuItem");
            OpenMapDirectoryToolStripMenuItem.Click += OpenMapDirectoryToolStripMenuItem_Click;
            // 
            // OpenSaveDirectoryToolStripMenuItem
            // 
            OpenSaveDirectoryToolStripMenuItem.Name = "OpenSaveDirectoryToolStripMenuItem";
            resources.ApplyResources(OpenSaveDirectoryToolStripMenuItem, "OpenSaveDirectoryToolStripMenuItem");
            OpenSaveDirectoryToolStripMenuItem.Click += OpenSaveDirectoryToolStripMenuItem_Click;
            // 
            // openReplayDirectoryToolStripMenuItem
            // 
            openReplayDirectoryToolStripMenuItem.Name = "openReplayDirectoryToolStripMenuItem";
            resources.ApplyResources(openReplayDirectoryToolStripMenuItem, "openReplayDirectoryToolStripMenuItem");
            openReplayDirectoryToolStripMenuItem.Click += OpenReplayDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
            // 
            // RepairGameToolStripMenuItem
            // 
            RepairGameToolStripMenuItem.Name = "RepairGameToolStripMenuItem";
            resources.ApplyResources(RepairGameToolStripMenuItem, "RepairGameToolStripMenuItem");
            RepairGameToolStripMenuItem.Click += RepairGameToolStripMenuItem_Click;
            // 
            // AboutToolStripMenuItem
            // 
            AboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { CreditsToolStripMenuItem, MessagesFromTheTeamToolStripMenuItem });
            AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            resources.ApplyResources(AboutToolStripMenuItem, "AboutToolStripMenuItem");
            // 
            // CreditsToolStripMenuItem
            // 
            CreditsToolStripMenuItem.Name = "CreditsToolStripMenuItem";
            resources.ApplyResources(CreditsToolStripMenuItem, "CreditsToolStripMenuItem");
            CreditsToolStripMenuItem.Click += CreditsToolStripMenuItem_Click;
            // 
            // MessagesFromTheTeamToolStripMenuItem
            // 
            MessagesFromTheTeamToolStripMenuItem.Name = "MessagesFromTheTeamToolStripMenuItem";
            resources.ApplyResources(MessagesFromTheTeamToolStripMenuItem, "MessagesFromTheTeamToolStripMenuItem");
            MessagesFromTheTeamToolStripMenuItem.Click += MessagesFromTheTeamToolStripMenuItem_Click;
            // 
            // PBarActualFile
            // 
            PBarActualFile.BackColor = System.Drawing.Color.Black;
            PBarActualFile.CustomText = null;
            PBarActualFile.DisplayStyle = ProgressBarDisplayText.Percentage;
            PBarActualFile.ForeColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(PBarActualFile, "PBarActualFile");
            PBarActualFile.Name = "PBarActualFile";
            PBarActualFile.Style = ProgressBarStyle.Continuous;
            // 
            // TmrPatchNotes
            // 
            TmrPatchNotes.Enabled = true;
            // 
            // TmrAnimation
            // 
            TmrAnimation.Interval = 10;
            TmrAnimation.Tick += TmrAnimation_Tick;
            // 
            // SysTray
            // 
            SysTray.BalloonTipIcon = ToolTipIcon.Info;
            resources.ApplyResources(SysTray, "SysTray");
            SysTray.MouseDoubleClick += SysTray_MouseDoubleClick;
            // 
            // WinFormsMainGUI
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.ActiveBorder;
            resources.ApplyResources(this, "$this");
            Controls.Add(LblModExplanation);
            Controls.Add(PBarActualFile);
            Controls.Add(BtnInstall);
            Controls.Add(WinFormsMainMenuStrip);
            Controls.Add(PibMute);
            Controls.Add(PiBArrow);
            Controls.Add(PiBTwitch);
            Controls.Add(LblPatchNotes);
            Controls.Add(LblWorkerIOTask);
            Controls.Add(LblWorkerFileName);
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
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "WinFormsMainGUI";
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
            ((System.ComponentModel.ISupportInitialize)PiBVersion103).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion106).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_33).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBVersion222_34).EndInit();
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
        private PictureBox PibLoadingRing;
        private PictureBox PibLoadingBorder;
        private PictureBox PiBTwitch;
        private PictureBox PiBArrow;
        private PictureBox PiBVersion103;
        private PictureBox PibMute;
        private ToolTip ToolTip;
        private Timer TmrPatchNotes;
        private Label LblWorkerFileName;
        private Button BtnInstall;
        private Label LblWorkerIOTask;
        private Timer TmrAnimation;
        private FlowLayoutPanel PnlPlaceholder;
        private NotifyIcon SysTray;
        private ContextMenuStrip NotifyContextMenu;
        private ToolStripMenuItem MenuItemLaunchGame;
        private ToolStripMenuItem closeTheLauncherToolStripMenuItem;
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
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem GameDesktopShortcutToolStripMenuItem;
        private ToolStripMenuItem GameStartmenuShortcutsToolStripMenuItem;
        private ToolStripMenuItem openReplayDirectoryToolStripMenuItem;
        private ToolStripMenuItem CreditsToolStripMenuItem;
        private ToolStripMenuItem MessagesFromTheTeamToolStripMenuItem;
        private ToolStripMenuItem openLauncherLogfileDirectoryToolStripMenuItem;
        private Microsoft.Web.WebView2.WinForms.WebView2 Wv2Patchnotes;
        private CustomLabel LblPatchNotes;
        private CustomLabel LblModExplanation;
        private CustomProgressBar PBarActualFile;
        private ToolStripMenuItem LauncherDesktopShortcutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem LauncherStartmenuShortcutToolStripMenuItem;
        private PictureBox PiBVersion222_33;
        private PictureBox PiBVersion222_34;
        private PictureBox PiBVersion106;
    }
}