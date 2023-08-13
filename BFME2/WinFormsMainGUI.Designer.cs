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
            LabelLoadingPanel = new CustomLabel();
            PibLoadingBorder = new PictureBox();
            PiBTwitch = new PictureBox();
            PanelPlaceholder = new FlowLayoutPanel();
            PiBVersion106 = new PictureBox();
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
            SettingsToolStripMenuItem = new ToolStripMenuItem();
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
            SelectGameToolStripMenuItem = new ToolStripMenuItem();
            BFME1ToolStripMenuItem = new ToolStripMenuItem();
            BFME25ToolStripMenuItem = new ToolStripMenuItem();
            AboutToolStripMenuItem = new ToolStripMenuItem();
            CreditsToolStripMenuItem = new ToolStripMenuItem();
            ChangelogLauncherToolStripMenuItem = new ToolStripMenuItem();
            PBarActualFile = new CustomProgressBar();
            SysTray = new NotifyIcon(components);
            ((System.ComponentModel.ISupportInitialize)PibHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBYoutube).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBDiscord).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBModDB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBThemeSwitcher).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PibLoadingRing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PibLoadingBorder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PiBTwitch).BeginInit();
            PanelPlaceholder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PiBVersion106).BeginInit();
            NotifyContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PibMute).BeginInit();
            WinFormsMainMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // PibHeader
            // 
            resources.ApplyResources(PibHeader, "PibHeader");
            PibHeader.BackColor = System.Drawing.Color.Transparent;
            PibHeader.Name = "PibHeader";
            PibHeader.TabStop = false;
            ToolTip.SetToolTip(PibHeader, resources.GetString("PibHeader.ToolTip"));
            // 
            // PiBYoutube
            // 
            resources.ApplyResources(PiBYoutube, "PiBYoutube");
            PiBYoutube.BackColor = System.Drawing.Color.Black;
            PiBYoutube.Cursor = Cursors.Hand;
            PiBYoutube.Name = "PiBYoutube";
            PiBYoutube.TabStop = false;
            ToolTip.SetToolTip(PiBYoutube, resources.GetString("PiBYoutube.ToolTip"));
            PiBYoutube.Click += PiBYoutube_Click;
            // 
            // PiBDiscord
            // 
            resources.ApplyResources(PiBDiscord, "PiBDiscord");
            PiBDiscord.BackColor = System.Drawing.Color.Black;
            PiBDiscord.Cursor = Cursors.Hand;
            PiBDiscord.Name = "PiBDiscord";
            PiBDiscord.TabStop = false;
            ToolTip.SetToolTip(PiBDiscord, resources.GetString("PiBDiscord.ToolTip"));
            PiBDiscord.Click += PiBDiscord_Click;
            // 
            // PiBModDB
            // 
            resources.ApplyResources(PiBModDB, "PiBModDB");
            PiBModDB.BackColor = System.Drawing.Color.Black;
            PiBModDB.Cursor = Cursors.Hand;
            PiBModDB.Name = "PiBModDB";
            PiBModDB.TabStop = false;
            ToolTip.SetToolTip(PiBModDB, resources.GetString("PiBModDB.ToolTip"));
            PiBModDB.Click += PiBModDB_Click;
            // 
            // PiBThemeSwitcher
            // 
            resources.ApplyResources(PiBThemeSwitcher, "PiBThemeSwitcher");
            PiBThemeSwitcher.BackColor = System.Drawing.Color.Black;
            PiBThemeSwitcher.Cursor = Cursors.Hand;
            PiBThemeSwitcher.Name = "PiBThemeSwitcher";
            PiBThemeSwitcher.TabStop = false;
            ToolTip.SetToolTip(PiBThemeSwitcher, resources.GetString("PiBThemeSwitcher.ToolTip"));
            PiBThemeSwitcher.Click += PiBThemeSwitcher_Click;
            // 
            // ToolTip
            // 
            ToolTip.BackColor = System.Drawing.Color.Black;
            ToolTip.ForeColor = System.Drawing.Color.FromArgb(168, 190, 98);
            ToolTip.OwnerDraw = true;
            ToolTip.Draw += Tooltip_Draw;
            ToolTip.Popup += TooltipPopup;
            // 
            // LblWorkerFileName
            // 
            resources.ApplyResources(LblWorkerFileName, "LblWorkerFileName");
            LblWorkerFileName.Name = "LblWorkerFileName";
            ToolTip.SetToolTip(LblWorkerFileName, resources.GetString("LblWorkerFileName.ToolTip"));
            // 
            // BtnInstall
            // 
            resources.ApplyResources(BtnInstall, "BtnInstall");
            BtnInstall.BackColor = System.Drawing.Color.Black;
            BtnInstall.FlatAppearance.BorderSize = 0;
            BtnInstall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnInstall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnInstall.ForeColor = System.Drawing.Color.Transparent;
            BtnInstall.Name = "BtnInstall";
            BtnInstall.TabStop = false;
            ToolTip.SetToolTip(BtnInstall, resources.GetString("BtnInstall.ToolTip"));
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
            ToolTip.SetToolTip(LblWorkerIOTask, resources.GetString("LblWorkerIOTask.ToolTip"));
            // 
            // PibLoadingRing
            // 
            resources.ApplyResources(PibLoadingRing, "PibLoadingRing");
            PibLoadingRing.BackColor = System.Drawing.Color.Black;
            PibLoadingRing.Name = "PibLoadingRing";
            PibLoadingRing.TabStop = false;
            ToolTip.SetToolTip(PibLoadingRing, resources.GetString("PibLoadingRing.ToolTip"));
            // 
            // LabelLoadingPanel
            // 
            resources.ApplyResources(LabelLoadingPanel, "LabelLoadingPanel");
            LabelLoadingPanel.BackColor = System.Drawing.Color.Transparent;
            LabelLoadingPanel.Cursor = Cursors.WaitCursor;
            LabelLoadingPanel.ForeColor = System.Drawing.SystemColors.Control;
            LabelLoadingPanel.Name = "LabelLoadingPanel";
            LabelLoadingPanel.OutlineForeColor = System.Drawing.Color.Black;
            LabelLoadingPanel.OutlineWidth = 4F;
            ToolTip.SetToolTip(LabelLoadingPanel, resources.GetString("LabelLoadingPanel.ToolTip"));
            // 
            // PibLoadingBorder
            // 
            resources.ApplyResources(PibLoadingBorder, "PibLoadingBorder");
            PibLoadingBorder.BackColor = System.Drawing.Color.Transparent;
            PibLoadingBorder.Name = "PibLoadingBorder";
            PibLoadingBorder.TabStop = false;
            ToolTip.SetToolTip(PibLoadingBorder, resources.GetString("PibLoadingBorder.ToolTip"));
            // 
            // PiBTwitch
            // 
            resources.ApplyResources(PiBTwitch, "PiBTwitch");
            PiBTwitch.BackColor = System.Drawing.Color.Black;
            PiBTwitch.Cursor = Cursors.Hand;
            PiBTwitch.Name = "PiBTwitch";
            PiBTwitch.TabStop = false;
            ToolTip.SetToolTip(PiBTwitch, resources.GetString("PiBTwitch.ToolTip"));
            PiBTwitch.Click += PiBTwitch_Click;
            // 
            // PanelPlaceholder
            // 
            resources.ApplyResources(PanelPlaceholder, "PanelPlaceholder");
            PanelPlaceholder.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            PanelPlaceholder.Controls.Add(PiBVersion106);
            PanelPlaceholder.Name = "PanelPlaceholder";
            ToolTip.SetToolTip(PanelPlaceholder, resources.GetString("PanelPlaceholder.ToolTip"));
            // 
            // PiBVersion106
            // 
            resources.ApplyResources(PiBVersion106, "PiBVersion106");
            PiBVersion106.BackColor = System.Drawing.Color.Black;
            PiBVersion106.Cursor = Cursors.Hand;
            PiBVersion106.Name = "PiBVersion106";
            PiBVersion106.TabStop = false;
            ToolTip.SetToolTip(PiBVersion106, resources.GetString("PiBVersion106.ToolTip"));
            PiBVersion106.Click += PiBVersion106_Click;
            // 
            // LblModExplanation
            // 
            resources.ApplyResources(LblModExplanation, "LblModExplanation");
            LblModExplanation.Name = "LblModExplanation";
            LblModExplanation.OutlineForeColor = System.Drawing.Color.Black;
            LblModExplanation.OutlineWidth = 4F;
            ToolTip.SetToolTip(LblModExplanation, resources.GetString("LblModExplanation.ToolTip"));
            // 
            // NotifyContextMenu
            // 
            resources.ApplyResources(NotifyContextMenu, "NotifyContextMenu");
            NotifyContextMenu.Items.AddRange(new ToolStripItem[] { MenuItemLaunchGame, closeTheLauncherToolStripMenuItem });
            NotifyContextMenu.Name = "NotifyContextMenu";
            ToolTip.SetToolTip(NotifyContextMenu, resources.GetString("NotifyContextMenu.ToolTip"));
            // 
            // MenuItemLaunchGame
            // 
            resources.ApplyResources(MenuItemLaunchGame, "MenuItemLaunchGame");
            MenuItemLaunchGame.Name = "MenuItemLaunchGame";
            MenuItemLaunchGame.Click += MenuItemLaunchGame_Click;
            // 
            // closeTheLauncherToolStripMenuItem
            // 
            resources.ApplyResources(closeTheLauncherToolStripMenuItem, "closeTheLauncherToolStripMenuItem");
            closeTheLauncherToolStripMenuItem.Name = "closeTheLauncherToolStripMenuItem";
            closeTheLauncherToolStripMenuItem.Click += CloseTheLauncherToolStripMenuItem_Click;
            // 
            // PibMute
            // 
            resources.ApplyResources(PibMute, "PibMute");
            PibMute.BackColor = System.Drawing.Color.Transparent;
            PibMute.Cursor = Cursors.Hand;
            PibMute.Name = "PibMute";
            PibMute.TabStop = false;
            ToolTip.SetToolTip(PibMute, resources.GetString("PibMute.ToolTip"));
            PibMute.Click += PibMute_Click;
            // 
            // WinFormsMainMenuStrip
            // 
            resources.ApplyResources(WinFormsMainMenuStrip, "WinFormsMainMenuStrip");
            WinFormsMainMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, OptionsToolStripMenuItem, AdvancedToolStripMenuItem, SelectGameToolStripMenuItem, AboutToolStripMenuItem });
            WinFormsMainMenuStrip.Name = "WinFormsMainMenuStrip";
            ToolTip.SetToolTip(WinFormsMainMenuStrip, resources.GetString("WinFormsMainMenuStrip.ToolTip"));
            // 
            // FileToolStripMenuItem
            // 
            resources.ApplyResources(FileToolStripMenuItem, "FileToolStripMenuItem");
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { LaunchGameToolStripMenuItem, toolStripSeparator1, ExitToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            // 
            // LaunchGameToolStripMenuItem
            // 
            resources.ApplyResources(LaunchGameToolStripMenuItem, "LaunchGameToolStripMenuItem");
            LaunchGameToolStripMenuItem.Name = "LaunchGameToolStripMenuItem";
            LaunchGameToolStripMenuItem.Click += LaunchGameToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // ExitToolStripMenuItem
            // 
            resources.ApplyResources(ExitToolStripMenuItem, "ExitToolStripMenuItem");
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Click += CloseTheLauncherToolStripMenuItem_Click;
            // 
            // OptionsToolStripMenuItem
            // 
            resources.ApplyResources(OptionsToolStripMenuItem, "OptionsToolStripMenuItem");
            OptionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SettingsToolStripMenuItem, toolStripSeparator5, GameDesktopShortcutToolStripMenuItem, GameStartmenuShortcutsToolStripMenuItem, toolStripSeparator6, LauncherDesktopShortcutToolStripMenuItem, LauncherStartmenuShortcutToolStripMenuItem });
            OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            // 
            // SettingsToolStripMenuItem
            // 
            resources.ApplyResources(SettingsToolStripMenuItem, "SettingsToolStripMenuItem");
            SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            SettingsToolStripMenuItem.Click += SettingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // GameDesktopShortcutToolStripMenuItem
            // 
            resources.ApplyResources(GameDesktopShortcutToolStripMenuItem, "GameDesktopShortcutToolStripMenuItem");
            GameDesktopShortcutToolStripMenuItem.CheckOnClick = true;
            GameDesktopShortcutToolStripMenuItem.Name = "GameDesktopShortcutToolStripMenuItem";
            GameDesktopShortcutToolStripMenuItem.Click += GameDesktopShortcutToolStripMenuItem_Click;
            // 
            // GameStartmenuShortcutsToolStripMenuItem
            // 
            resources.ApplyResources(GameStartmenuShortcutsToolStripMenuItem, "GameStartmenuShortcutsToolStripMenuItem");
            GameStartmenuShortcutsToolStripMenuItem.CheckOnClick = true;
            GameStartmenuShortcutsToolStripMenuItem.Name = "GameStartmenuShortcutsToolStripMenuItem";
            GameStartmenuShortcutsToolStripMenuItem.Click += GameStartmenuShortcutsToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
            toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // LauncherDesktopShortcutToolStripMenuItem
            // 
            resources.ApplyResources(LauncherDesktopShortcutToolStripMenuItem, "LauncherDesktopShortcutToolStripMenuItem");
            LauncherDesktopShortcutToolStripMenuItem.CheckOnClick = true;
            LauncherDesktopShortcutToolStripMenuItem.Name = "LauncherDesktopShortcutToolStripMenuItem";
            LauncherDesktopShortcutToolStripMenuItem.Click += LauncherDesktopShortcutToolStripMenuItem_Click;
            // 
            // LauncherStartmenuShortcutToolStripMenuItem
            // 
            resources.ApplyResources(LauncherStartmenuShortcutToolStripMenuItem, "LauncherStartmenuShortcutToolStripMenuItem");
            LauncherStartmenuShortcutToolStripMenuItem.CheckOnClick = true;
            LauncherStartmenuShortcutToolStripMenuItem.Name = "LauncherStartmenuShortcutToolStripMenuItem";
            LauncherStartmenuShortcutToolStripMenuItem.Click += LauncherStartmenuShortcutToolStripMenuItem_Click;
            // 
            // AdvancedToolStripMenuItem
            // 
            resources.ApplyResources(AdvancedToolStripMenuItem, "AdvancedToolStripMenuItem");
            AdvancedToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenLauncherDirectoryToolStripMenuItem, openLauncherLogfileDirectoryToolStripMenuItem, toolStripSeparator2, OpenGameDirectoryToolStripMenuItem, toolStripSeparator3, OpenMapDirectoryToolStripMenuItem, OpenSaveDirectoryToolStripMenuItem, openReplayDirectoryToolStripMenuItem, toolStripSeparator4, RepairGameToolStripMenuItem });
            AdvancedToolStripMenuItem.Name = "AdvancedToolStripMenuItem";
            // 
            // OpenLauncherDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(OpenLauncherDirectoryToolStripMenuItem, "OpenLauncherDirectoryToolStripMenuItem");
            OpenLauncherDirectoryToolStripMenuItem.Name = "OpenLauncherDirectoryToolStripMenuItem";
            OpenLauncherDirectoryToolStripMenuItem.Click += OpenLauncherDirectoryToolStripMenuItem_Click;
            // 
            // openLauncherLogfileDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(openLauncherLogfileDirectoryToolStripMenuItem, "openLauncherLogfileDirectoryToolStripMenuItem");
            openLauncherLogfileDirectoryToolStripMenuItem.Name = "openLauncherLogfileDirectoryToolStripMenuItem";
            openLauncherLogfileDirectoryToolStripMenuItem.Click += OpenLauncherLogfileDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // OpenGameDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(OpenGameDirectoryToolStripMenuItem, "OpenGameDirectoryToolStripMenuItem");
            OpenGameDirectoryToolStripMenuItem.Name = "OpenGameDirectoryToolStripMenuItem";
            OpenGameDirectoryToolStripMenuItem.Click += OpenGameDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // OpenMapDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(OpenMapDirectoryToolStripMenuItem, "OpenMapDirectoryToolStripMenuItem");
            OpenMapDirectoryToolStripMenuItem.Name = "OpenMapDirectoryToolStripMenuItem";
            OpenMapDirectoryToolStripMenuItem.Click += OpenMapDirectoryToolStripMenuItem_Click;
            // 
            // OpenSaveDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(OpenSaveDirectoryToolStripMenuItem, "OpenSaveDirectoryToolStripMenuItem");
            OpenSaveDirectoryToolStripMenuItem.Name = "OpenSaveDirectoryToolStripMenuItem";
            OpenSaveDirectoryToolStripMenuItem.Click += OpenSaveDirectoryToolStripMenuItem_Click;
            // 
            // openReplayDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(openReplayDirectoryToolStripMenuItem, "openReplayDirectoryToolStripMenuItem");
            openReplayDirectoryToolStripMenuItem.Name = "openReplayDirectoryToolStripMenuItem";
            openReplayDirectoryToolStripMenuItem.Click += OpenReplayDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
            toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // RepairGameToolStripMenuItem
            // 
            resources.ApplyResources(RepairGameToolStripMenuItem, "RepairGameToolStripMenuItem");
            RepairGameToolStripMenuItem.Name = "RepairGameToolStripMenuItem";
            RepairGameToolStripMenuItem.Click += RepairGameToolStripMenuItem_Click;
            // 
            // SelectGameToolStripMenuItem
            // 
            resources.ApplyResources(SelectGameToolStripMenuItem, "SelectGameToolStripMenuItem");
            SelectGameToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { BFME1ToolStripMenuItem, BFME25ToolStripMenuItem });
            SelectGameToolStripMenuItem.Name = "SelectGameToolStripMenuItem";
            // 
            // BFME1ToolStripMenuItem
            // 
            resources.ApplyResources(BFME1ToolStripMenuItem, "BFME1ToolStripMenuItem");
            BFME1ToolStripMenuItem.Name = "BFME1ToolStripMenuItem";
            BFME1ToolStripMenuItem.Click += BFME1ToolStripMenuItem_Click;
            // 
            // BFME25ToolStripMenuItem
            // 
            resources.ApplyResources(BFME25ToolStripMenuItem, "BFME25ToolStripMenuItem");
            BFME25ToolStripMenuItem.Name = "BFME25ToolStripMenuItem";
            BFME25ToolStripMenuItem.Click += BFME25ToolStripMenuItem_Click;
            // 
            // AboutToolStripMenuItem
            // 
            resources.ApplyResources(AboutToolStripMenuItem, "AboutToolStripMenuItem");
            AboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { CreditsToolStripMenuItem, ChangelogLauncherToolStripMenuItem });
            AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            // 
            // CreditsToolStripMenuItem
            // 
            resources.ApplyResources(CreditsToolStripMenuItem, "CreditsToolStripMenuItem");
            CreditsToolStripMenuItem.Name = "CreditsToolStripMenuItem";
            CreditsToolStripMenuItem.Click += CreditsToolStripMenuItem_Click;
            // 
            // ChangelogLauncherToolStripMenuItem
            // 
            resources.ApplyResources(ChangelogLauncherToolStripMenuItem, "ChangelogLauncherToolStripMenuItem");
            ChangelogLauncherToolStripMenuItem.Name = "ChangelogLauncherToolStripMenuItem";
            ChangelogLauncherToolStripMenuItem.Click += ChangelogLauncherToolStripMenuItem_Click;
            // 
            // PBarActualFile
            // 
            resources.ApplyResources(PBarActualFile, "PBarActualFile");
            PBarActualFile.BackColor = System.Drawing.Color.Black;
            PBarActualFile.CustomText = null;
            PBarActualFile.DisplayStyle = ProgressBarDisplayText.Percentage;
            PBarActualFile.Game = Game.BFME2;
            PBarActualFile.ForeColor = System.Drawing.Color.Transparent;
            PBarActualFile.Name = "PBarActualFile";
            PBarActualFile.Style = ProgressBarStyle.Continuous;
            ToolTip.SetToolTip(PBarActualFile, resources.GetString("PBarActualFile.ToolTip"));
            // 
            // SysTray
            // 
            SysTray.BalloonTipIcon = ToolTipIcon.Info;
            resources.ApplyResources(SysTray, "SysTray");
            SysTray.MouseDoubleClick += SysTray_MouseDoubleClick;
            // 
            // WinFormsMainGUI
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.ActiveBorder;
            Controls.Add(LblModExplanation);
            Controls.Add(PBarActualFile);
            Controls.Add(BtnInstall);
            Controls.Add(WinFormsMainMenuStrip);
            Controls.Add(PibMute);
            Controls.Add(PiBTwitch);
            Controls.Add(LabelLoadingPanel);
            Controls.Add(LblWorkerIOTask);
            Controls.Add(LblWorkerFileName);
            Controls.Add(PiBThemeSwitcher);
            Controls.Add(PiBModDB);
            Controls.Add(PiBDiscord);
            Controls.Add(PiBYoutube);
            Controls.Add(PibHeader);
            Controls.Add(PibLoadingRing);
            Controls.Add(PibLoadingBorder);
            Controls.Add(PanelPlaceholder);
            DoubleBuffered = true;
            ForeColor = System.Drawing.SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "WinFormsMainGUI";
            ToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += BFME2_FormClosing;
            Load += WinFormsMainGUI_Load;
            Shown += BFME2_Shown;
            Resize += BFME2_Resize;
            ((System.ComponentModel.ISupportInitialize)PibHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBYoutube).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBDiscord).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBModDB).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBThemeSwitcher).EndInit();
            ((System.ComponentModel.ISupportInitialize)PibLoadingRing).EndInit();
            ((System.ComponentModel.ISupportInitialize)PibLoadingBorder).EndInit();
            ((System.ComponentModel.ISupportInitialize)PiBTwitch).EndInit();
            PanelPlaceholder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PiBVersion106).EndInit();
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
        private PictureBox PiBVersion106;
        private PictureBox PibMute;
        private ToolTip ToolTip;
        private Label LblWorkerFileName;
        private Button BtnInstall;
        private Label LblWorkerIOTask;
        private FlowLayoutPanel PanelPlaceholder;
        private NotifyIcon SysTray;
        private ContextMenuStrip NotifyContextMenu;
        private ToolStripMenuItem MenuItemLaunchGame;
        private ToolStripMenuItem closeTheLauncherToolStripMenuItem;
        private MenuStrip WinFormsMainMenuStrip;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem LaunchGameToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem OptionsToolStripMenuItem;
        private ToolStripMenuItem SettingsToolStripMenuItem;
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
        private ToolStripMenuItem ChangelogLauncherToolStripMenuItem;
        private ToolStripMenuItem openLauncherLogfileDirectoryToolStripMenuItem;
        private CustomLabel LabelLoadingPanel;
        private CustomLabel LblModExplanation;
        private CustomProgressBar PBarActualFile;
        private ToolStripMenuItem LauncherDesktopShortcutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem LauncherStartmenuShortcutToolStripMenuItem;
        private ToolStripMenuItem SelectGameToolStripMenuItem;
        private ToolStripMenuItem BFME1ToolStripMenuItem;
        private ToolStripMenuItem BFME25ToolStripMenuItem;
    }
}