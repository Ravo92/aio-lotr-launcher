namespace PatchLauncher
{
    partial class GameOptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameOptionsForm));
            BtnDefault = new System.Windows.Forms.Button();
            PibBorderLauncherOptions = new System.Windows.Forms.PictureBox();
            PibHeader = new System.Windows.Forms.PictureBox();
            LblOptions = new System.Windows.Forms.Label();
            PibBorderGameOptions = new System.Windows.Forms.PictureBox();
            LblGameSettings = new System.Windows.Forms.Label();
            LblAniTextureFiltering = new System.Windows.Forms.Label();
            ChkAniTextureFiltering = new System.Windows.Forms.Button();
            ChkTerrainLighting = new System.Windows.Forms.Button();
            LblTerrainLighting = new System.Windows.Forms.Label();
            Lbl2DShadows = new System.Windows.Forms.Label();
            Chk2DShadows = new System.Windows.Forms.Button();
            Lbl3DShadows = new System.Windows.Forms.Label();
            Chk3DShadows = new System.Windows.Forms.Button();
            LblShowProps = new System.Windows.Forms.Label();
            ChkShowProps = new System.Windows.Forms.Button();
            LblSmoothWaterBorder = new System.Windows.Forms.Label();
            ChkSmoothWaterBorder = new System.Windows.Forms.Button();
            LblDynamicLOD = new System.Windows.Forms.Label();
            ChkDynamicLOD = new System.Windows.Forms.Button();
            LblHeatEffects = new System.Windows.Forms.Label();
            ChkHeatEffects = new System.Windows.Forms.Button();
            LblShowAnimations = new System.Windows.Forms.Label();
            ChkShowAnimations = new System.Windows.Forms.Button();
            ResolutionX = new System.Windows.Forms.TextBox();
            ResolutionY = new System.Windows.Forms.TextBox();
            LblResolutionX = new System.Windows.Forms.Label();
            LblResolution = new System.Windows.Forms.Label();
            BtnApply = new System.Windows.Forms.Button();
            BtnCancel = new System.Windows.Forms.Button();
            LblInfoLOD = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)PibBorderLauncherOptions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PibHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PibBorderGameOptions).BeginInit();
            SuspendLayout();
            // 
            // BtnDefault
            // 
            BtnDefault.BackColor = System.Drawing.Color.Black;
            BtnDefault.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            BtnDefault.FlatAppearance.BorderSize = 0;
            BtnDefault.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnDefault.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnDefault.ForeColor = System.Drawing.Color.Transparent;
            BtnDefault.Location = new System.Drawing.Point(422, 734);
            BtnDefault.Name = "BtnDefault";
            BtnDefault.Size = new System.Drawing.Size(195, 51);
            BtnDefault.TabIndex = 3;
            BtnDefault.TabStop = false;
            BtnDefault.Text = "DEFAULT";
            BtnDefault.UseMnemonic = false;
            BtnDefault.UseVisualStyleBackColor = false;
            BtnDefault.Click += BtnDefault_Click;
            BtnDefault.MouseDown += BtnDefault_MouseDown;
            BtnDefault.MouseEnter += BtnDefault_MouseEnter;
            BtnDefault.MouseLeave += BtnDefault_MouseLeave;
            // 
            // PibBorderLauncherOptions
            // 
            PibBorderLauncherOptions.BackColor = System.Drawing.Color.Transparent;
            PibBorderLauncherOptions.Image = (System.Drawing.Image)resources.GetObject("PibBorderLauncherOptions.Image");
            PibBorderLauncherOptions.Location = new System.Drawing.Point(12, 115);
            PibBorderLauncherOptions.Name = "PibBorderLauncherOptions";
            PibBorderLauncherOptions.Size = new System.Drawing.Size(600, 400);
            PibBorderLauncherOptions.TabIndex = 9;
            PibBorderLauncherOptions.TabStop = false;
            PibBorderLauncherOptions.WaitOnLoad = true;
            // 
            // PibHeader
            // 
            PibHeader.BackColor = System.Drawing.Color.Transparent;
            PibHeader.BackgroundImage = (System.Drawing.Image)resources.GetObject("PibHeader.BackgroundImage");
            PibHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            PibHeader.ErrorImage = null;
            PibHeader.InitialImage = null;
            PibHeader.Location = new System.Drawing.Point(198, 0);
            PibHeader.Name = "PibHeader";
            PibHeader.Size = new System.Drawing.Size(775, 105);
            PibHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            PibHeader.TabIndex = 11;
            PibHeader.TabStop = false;
            PibHeader.WaitOnLoad = true;
            // 
            // LblOptions
            // 
            LblOptions.AutoSize = true;
            LblOptions.Location = new System.Drawing.Point(480, 7);
            LblOptions.Name = "LblOptions";
            LblOptions.Size = new System.Drawing.Size(49, 15);
            LblOptions.TabIndex = 12;
            LblOptions.Text = "Settings";
            // 
            // PibBorderGameOptions
            // 
            PibBorderGameOptions.BackColor = System.Drawing.Color.Transparent;
            PibBorderGameOptions.Image = (System.Drawing.Image)resources.GetObject("PibBorderGameOptions.Image");
            PibBorderGameOptions.Location = new System.Drawing.Point(620, 115);
            PibBorderGameOptions.Name = "PibBorderGameOptions";
            PibBorderGameOptions.Size = new System.Drawing.Size(520, 670);
            PibBorderGameOptions.TabIndex = 14;
            PibBorderGameOptions.TabStop = false;
            PibBorderGameOptions.WaitOnLoad = true;
            // 
            // LblGameSettings
            // 
            LblGameSettings.AutoSize = true;
            LblGameSettings.Location = new System.Drawing.Point(650, 125);
            LblGameSettings.Name = "LblGameSettings";
            LblGameSettings.Size = new System.Drawing.Size(82, 15);
            LblGameSettings.TabIndex = 15;
            LblGameSettings.Text = "Video Settings";
            // 
            // LblAniTextureFiltering
            // 
            LblAniTextureFiltering.AutoSize = true;
            LblAniTextureFiltering.Location = new System.Drawing.Point(675, 180);
            LblAniTextureFiltering.Name = "LblAniTextureFiltering";
            LblAniTextureFiltering.Size = new System.Drawing.Size(155, 15);
            LblAniTextureFiltering.TabIndex = 17;
            LblAniTextureFiltering.Text = "Anisotropic Texture Filtering";
            // 
            // ChkAniTextureFiltering
            // 
            ChkAniTextureFiltering.BackColor = System.Drawing.Color.Black;
            ChkAniTextureFiltering.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ChkAniTextureFiltering.FlatAppearance.BorderSize = 0;
            ChkAniTextureFiltering.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ChkAniTextureFiltering.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            ChkAniTextureFiltering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ChkAniTextureFiltering.ForeColor = System.Drawing.Color.Transparent;
            ChkAniTextureFiltering.Location = new System.Drawing.Point(635, 180);
            ChkAniTextureFiltering.Margin = new System.Windows.Forms.Padding(0);
            ChkAniTextureFiltering.Name = "ChkAniTextureFiltering";
            ChkAniTextureFiltering.Size = new System.Drawing.Size(29, 29);
            ChkAniTextureFiltering.TabIndex = 16;
            ChkAniTextureFiltering.TabStop = false;
            ChkAniTextureFiltering.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ChkAniTextureFiltering.UseMnemonic = false;
            ChkAniTextureFiltering.UseVisualStyleBackColor = false;
            ChkAniTextureFiltering.Click += ChkAniTextureFiltering_Click;
            ChkAniTextureFiltering.MouseDown += ChkAniTextureFiltering_MouseDown;
            ChkAniTextureFiltering.MouseEnter += ChkAniTextureFiltering_MouseEnter;
            ChkAniTextureFiltering.MouseLeave += ChkAniTextureFiltering_MouseLeave;
            // 
            // ChkTerrainLighting
            // 
            ChkTerrainLighting.BackColor = System.Drawing.Color.Black;
            ChkTerrainLighting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ChkTerrainLighting.FlatAppearance.BorderSize = 0;
            ChkTerrainLighting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ChkTerrainLighting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            ChkTerrainLighting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ChkTerrainLighting.ForeColor = System.Drawing.Color.Transparent;
            ChkTerrainLighting.Location = new System.Drawing.Point(635, 230);
            ChkTerrainLighting.Margin = new System.Windows.Forms.Padding(0);
            ChkTerrainLighting.Name = "ChkTerrainLighting";
            ChkTerrainLighting.Size = new System.Drawing.Size(29, 29);
            ChkTerrainLighting.TabIndex = 23;
            ChkTerrainLighting.TabStop = false;
            ChkTerrainLighting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ChkTerrainLighting.UseMnemonic = false;
            ChkTerrainLighting.UseVisualStyleBackColor = false;
            ChkTerrainLighting.Click += ChkTerrainLighting_Click;
            ChkTerrainLighting.MouseDown += ChkTerrainLighting_MouseDown;
            ChkTerrainLighting.MouseEnter += ChkTerrainLighting_MouseEnter;
            ChkTerrainLighting.MouseLeave += ChkTerrainLighting_MouseLeave;
            // 
            // LblTerrainLighting
            // 
            LblTerrainLighting.AutoSize = true;
            LblTerrainLighting.Location = new System.Drawing.Point(675, 230);
            LblTerrainLighting.Name = "LblTerrainLighting";
            LblTerrainLighting.Size = new System.Drawing.Size(89, 15);
            LblTerrainLighting.TabIndex = 24;
            LblTerrainLighting.Text = "Terrain Lighting";
            // 
            // Lbl2DShadows
            // 
            Lbl2DShadows.AutoSize = true;
            Lbl2DShadows.Location = new System.Drawing.Point(675, 330);
            Lbl2DShadows.Name = "Lbl2DShadows";
            Lbl2DShadows.Size = new System.Drawing.Size(71, 15);
            Lbl2DShadows.TabIndex = 28;
            Lbl2DShadows.Text = "2D Shadows";
            // 
            // Chk2DShadows
            // 
            Chk2DShadows.BackColor = System.Drawing.Color.Black;
            Chk2DShadows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            Chk2DShadows.FlatAppearance.BorderSize = 0;
            Chk2DShadows.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            Chk2DShadows.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            Chk2DShadows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Chk2DShadows.ForeColor = System.Drawing.Color.Transparent;
            Chk2DShadows.Location = new System.Drawing.Point(635, 330);
            Chk2DShadows.Margin = new System.Windows.Forms.Padding(0);
            Chk2DShadows.Name = "Chk2DShadows";
            Chk2DShadows.Size = new System.Drawing.Size(29, 29);
            Chk2DShadows.TabIndex = 27;
            Chk2DShadows.TabStop = false;
            Chk2DShadows.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            Chk2DShadows.UseMnemonic = false;
            Chk2DShadows.UseVisualStyleBackColor = false;
            Chk2DShadows.Click += Chk2DShadows_Click;
            Chk2DShadows.MouseDown += Chk2DShadows_MouseDown;
            Chk2DShadows.MouseEnter += Chk2DShadows_MouseEnter;
            Chk2DShadows.MouseLeave += Chk2DShadows_MouseLeave;
            // 
            // Lbl3DShadows
            // 
            Lbl3DShadows.AutoSize = true;
            Lbl3DShadows.Location = new System.Drawing.Point(675, 280);
            Lbl3DShadows.Name = "Lbl3DShadows";
            Lbl3DShadows.Size = new System.Drawing.Size(71, 15);
            Lbl3DShadows.TabIndex = 26;
            Lbl3DShadows.Text = "3D Shadows";
            // 
            // Chk3DShadows
            // 
            Chk3DShadows.BackColor = System.Drawing.Color.Black;
            Chk3DShadows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            Chk3DShadows.FlatAppearance.BorderSize = 0;
            Chk3DShadows.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            Chk3DShadows.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            Chk3DShadows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Chk3DShadows.ForeColor = System.Drawing.Color.Transparent;
            Chk3DShadows.Location = new System.Drawing.Point(635, 280);
            Chk3DShadows.Margin = new System.Windows.Forms.Padding(0);
            Chk3DShadows.Name = "Chk3DShadows";
            Chk3DShadows.Size = new System.Drawing.Size(29, 29);
            Chk3DShadows.TabIndex = 25;
            Chk3DShadows.TabStop = false;
            Chk3DShadows.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            Chk3DShadows.UseMnemonic = false;
            Chk3DShadows.UseVisualStyleBackColor = false;
            Chk3DShadows.Click += Chk3DShadows_Click;
            Chk3DShadows.MouseDown += Chk3DShadows_MouseDown;
            Chk3DShadows.MouseEnter += Chk3DShadows_MouseEnter;
            Chk3DShadows.MouseLeave += Chk3DShadows_MouseLeave;
            // 
            // LblShowProps
            // 
            LblShowProps.AutoSize = true;
            LblShowProps.Location = new System.Drawing.Point(675, 430);
            LblShowProps.Name = "LblShowProps";
            LblShowProps.Size = new System.Drawing.Size(69, 15);
            LblShowProps.TabIndex = 32;
            LblShowProps.Text = "Show Props";
            // 
            // ChkShowProps
            // 
            ChkShowProps.BackColor = System.Drawing.Color.Black;
            ChkShowProps.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ChkShowProps.FlatAppearance.BorderSize = 0;
            ChkShowProps.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ChkShowProps.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            ChkShowProps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ChkShowProps.ForeColor = System.Drawing.Color.Transparent;
            ChkShowProps.Location = new System.Drawing.Point(635, 430);
            ChkShowProps.Margin = new System.Windows.Forms.Padding(0);
            ChkShowProps.Name = "ChkShowProps";
            ChkShowProps.Size = new System.Drawing.Size(29, 29);
            ChkShowProps.TabIndex = 31;
            ChkShowProps.TabStop = false;
            ChkShowProps.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ChkShowProps.UseMnemonic = false;
            ChkShowProps.UseVisualStyleBackColor = false;
            ChkShowProps.Click += ChkShowProps_Click;
            ChkShowProps.MouseDown += ChkShowProps_MouseDown;
            ChkShowProps.MouseEnter += ChkShowProps_MouseEnter;
            ChkShowProps.MouseLeave += ChkShowProps_MouseLeave;
            // 
            // LblSmoothWaterBorder
            // 
            LblSmoothWaterBorder.AutoSize = true;
            LblSmoothWaterBorder.Location = new System.Drawing.Point(675, 380);
            LblSmoothWaterBorder.Name = "LblSmoothWaterBorder";
            LblSmoothWaterBorder.Size = new System.Drawing.Size(121, 15);
            LblSmoothWaterBorder.TabIndex = 30;
            LblSmoothWaterBorder.Text = "Smooth Water Border";
            // 
            // ChkSmoothWaterBorder
            // 
            ChkSmoothWaterBorder.BackColor = System.Drawing.Color.Black;
            ChkSmoothWaterBorder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ChkSmoothWaterBorder.FlatAppearance.BorderSize = 0;
            ChkSmoothWaterBorder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ChkSmoothWaterBorder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            ChkSmoothWaterBorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ChkSmoothWaterBorder.ForeColor = System.Drawing.Color.Transparent;
            ChkSmoothWaterBorder.Location = new System.Drawing.Point(635, 380);
            ChkSmoothWaterBorder.Margin = new System.Windows.Forms.Padding(0);
            ChkSmoothWaterBorder.Name = "ChkSmoothWaterBorder";
            ChkSmoothWaterBorder.Size = new System.Drawing.Size(29, 29);
            ChkSmoothWaterBorder.TabIndex = 29;
            ChkSmoothWaterBorder.TabStop = false;
            ChkSmoothWaterBorder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ChkSmoothWaterBorder.UseMnemonic = false;
            ChkSmoothWaterBorder.UseVisualStyleBackColor = false;
            ChkSmoothWaterBorder.Click += ChkSmoothWaterBorder_Click;
            ChkSmoothWaterBorder.MouseDown += ChkSmoothWaterBorder_MouseDown;
            ChkSmoothWaterBorder.MouseEnter += ChkSmoothWaterBorder_MouseEnter;
            ChkSmoothWaterBorder.MouseLeave += ChkSmoothWaterBorder_MouseLeave;
            // 
            // LblDynamicLOD
            // 
            LblDynamicLOD.AutoSize = true;
            LblDynamicLOD.Location = new System.Drawing.Point(675, 580);
            LblDynamicLOD.Name = "LblDynamicLOD";
            LblDynamicLOD.Size = new System.Drawing.Size(80, 15);
            LblDynamicLOD.TabIndex = 38;
            LblDynamicLOD.Text = "Dynamic LOD";
            // 
            // ChkDynamicLOD
            // 
            ChkDynamicLOD.BackColor = System.Drawing.Color.Black;
            ChkDynamicLOD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ChkDynamicLOD.FlatAppearance.BorderSize = 0;
            ChkDynamicLOD.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ChkDynamicLOD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            ChkDynamicLOD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ChkDynamicLOD.ForeColor = System.Drawing.Color.Transparent;
            ChkDynamicLOD.Location = new System.Drawing.Point(635, 580);
            ChkDynamicLOD.Margin = new System.Windows.Forms.Padding(0);
            ChkDynamicLOD.Name = "ChkDynamicLOD";
            ChkDynamicLOD.Size = new System.Drawing.Size(29, 29);
            ChkDynamicLOD.TabIndex = 37;
            ChkDynamicLOD.TabStop = false;
            ChkDynamicLOD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ChkDynamicLOD.UseMnemonic = false;
            ChkDynamicLOD.UseVisualStyleBackColor = false;
            ChkDynamicLOD.Click += ChkDynamicLOD_Click;
            ChkDynamicLOD.MouseDown += ChkDynamicLOD_MouseDown;
            ChkDynamicLOD.MouseEnter += ChkDynamicLOD_MouseEnter;
            ChkDynamicLOD.MouseLeave += ChkDynamicLOD_MouseLeave;
            // 
            // LblHeatEffects
            // 
            LblHeatEffects.AutoSize = true;
            LblHeatEffects.Location = new System.Drawing.Point(675, 530);
            LblHeatEffects.Name = "LblHeatEffects";
            LblHeatEffects.Size = new System.Drawing.Size(70, 15);
            LblHeatEffects.TabIndex = 36;
            LblHeatEffects.Text = "Heat Effects";
            // 
            // ChkHeatEffects
            // 
            ChkHeatEffects.BackColor = System.Drawing.Color.Black;
            ChkHeatEffects.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ChkHeatEffects.FlatAppearance.BorderSize = 0;
            ChkHeatEffects.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ChkHeatEffects.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            ChkHeatEffects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ChkHeatEffects.ForeColor = System.Drawing.Color.Transparent;
            ChkHeatEffects.Location = new System.Drawing.Point(635, 530);
            ChkHeatEffects.Margin = new System.Windows.Forms.Padding(0);
            ChkHeatEffects.Name = "ChkHeatEffects";
            ChkHeatEffects.Size = new System.Drawing.Size(29, 29);
            ChkHeatEffects.TabIndex = 35;
            ChkHeatEffects.TabStop = false;
            ChkHeatEffects.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ChkHeatEffects.UseMnemonic = false;
            ChkHeatEffects.UseVisualStyleBackColor = false;
            ChkHeatEffects.Click += ChkHeatEffects_Click;
            ChkHeatEffects.MouseDown += ChkHeatEffects_MouseDown;
            ChkHeatEffects.MouseEnter += ChkHeatEffects_MouseEnter;
            ChkHeatEffects.MouseLeave += ChkHeatEffects_MouseLeave;
            // 
            // LblShowAnimations
            // 
            LblShowAnimations.AutoSize = true;
            LblShowAnimations.Location = new System.Drawing.Point(675, 480);
            LblShowAnimations.Name = "LblShowAnimations";
            LblShowAnimations.Size = new System.Drawing.Size(100, 15);
            LblShowAnimations.TabIndex = 34;
            LblShowAnimations.Text = "Show Animations";
            // 
            // ChkShowAnimations
            // 
            ChkShowAnimations.BackColor = System.Drawing.Color.Black;
            ChkShowAnimations.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ChkShowAnimations.FlatAppearance.BorderSize = 0;
            ChkShowAnimations.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ChkShowAnimations.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            ChkShowAnimations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ChkShowAnimations.ForeColor = System.Drawing.Color.Transparent;
            ChkShowAnimations.Location = new System.Drawing.Point(635, 480);
            ChkShowAnimations.Margin = new System.Windows.Forms.Padding(0);
            ChkShowAnimations.Name = "ChkShowAnimations";
            ChkShowAnimations.Size = new System.Drawing.Size(29, 29);
            ChkShowAnimations.TabIndex = 33;
            ChkShowAnimations.TabStop = false;
            ChkShowAnimations.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ChkShowAnimations.UseMnemonic = false;
            ChkShowAnimations.UseVisualStyleBackColor = false;
            ChkShowAnimations.Click += ChkShowAnimations_Click;
            ChkShowAnimations.MouseDown += ChkShowAnimations_MouseDown;
            ChkShowAnimations.MouseEnter += ChkShowAnimations_MouseEnter;
            ChkShowAnimations.MouseLeave += ChkShowAnimations_MouseLeave;
            // 
            // ResolutionX
            // 
            ResolutionX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ResolutionX.Location = new System.Drawing.Point(635, 630);
            ResolutionX.Name = "ResolutionX";
            ResolutionX.Size = new System.Drawing.Size(70, 16);
            ResolutionX.TabIndex = 39;
            ResolutionX.TabStop = false;
            ResolutionX.Text = "1920";
            ResolutionX.WordWrap = false;
            // 
            // ResolutionY
            // 
            ResolutionY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ResolutionY.Location = new System.Drawing.Point(742, 630);
            ResolutionY.Name = "ResolutionY";
            ResolutionY.Size = new System.Drawing.Size(70, 16);
            ResolutionY.TabIndex = 40;
            ResolutionY.TabStop = false;
            ResolutionY.Text = "1080";
            ResolutionY.WordWrap = false;
            // 
            // LblResolutionX
            // 
            LblResolutionX.AutoSize = true;
            LblResolutionX.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblResolutionX.Location = new System.Drawing.Point(711, 626);
            LblResolutionX.Name = "LblResolutionX";
            LblResolutionX.Size = new System.Drawing.Size(25, 30);
            LblResolutionX.TabIndex = 41;
            LblResolutionX.Text = "X";
            // 
            // LblResolution
            // 
            LblResolution.AutoSize = true;
            LblResolution.Location = new System.Drawing.Point(820, 625);
            LblResolution.Name = "LblResolution";
            LblResolution.Size = new System.Drawing.Size(116, 15);
            LblResolution.TabIndex = 42;
            LblResolution.Text = "Set Game Resolution";
            // 
            // BtnApply
            // 
            BtnApply.BackColor = System.Drawing.Color.Black;
            BtnApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            BtnApply.FlatAppearance.BorderSize = 0;
            BtnApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnApply.ForeColor = System.Drawing.Color.Transparent;
            BtnApply.Location = new System.Drawing.Point(10, 734);
            BtnApply.Name = "BtnApply";
            BtnApply.Size = new System.Drawing.Size(195, 51);
            BtnApply.TabIndex = 46;
            BtnApply.TabStop = false;
            BtnApply.Text = "APPLY";
            BtnApply.UseMnemonic = false;
            BtnApply.UseVisualStyleBackColor = false;
            BtnApply.Click += BtnApply_Click;
            BtnApply.MouseDown += BtnApply_MouseDown;
            BtnApply.MouseEnter += BtnApply_MouseEnter;
            BtnApply.MouseLeave += BtnApply_MouseLeave;
            // 
            // BtnCancel
            // 
            BtnCancel.BackColor = System.Drawing.Color.Black;
            BtnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            BtnCancel.FlatAppearance.BorderSize = 0;
            BtnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            BtnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnCancel.ForeColor = System.Drawing.Color.Transparent;
            BtnCancel.Location = new System.Drawing.Point(216, 734);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new System.Drawing.Size(195, 51);
            BtnCancel.TabIndex = 47;
            BtnCancel.TabStop = false;
            BtnCancel.Text = "CANCEL";
            BtnCancel.UseMnemonic = false;
            BtnCancel.UseVisualStyleBackColor = false;
            BtnCancel.Click += BtnCancel_Click;
            BtnCancel.MouseDown += BtnCancel_MouseDown;
            BtnCancel.MouseEnter += BtnCancel_MouseEnter;
            BtnCancel.MouseLeave += BtnCancel_MouseLeave;
            // 
            // LblInfoLOD
            // 
            LblInfoLOD.AutoSize = true;
            LblInfoLOD.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblInfoLOD.Location = new System.Drawing.Point(12, 640);
            LblInfoLOD.Name = "LblInfoLOD";
            LblInfoLOD.Size = new System.Drawing.Size(478, 50);
            LblInfoLOD.TabIndex = 50;
            LblInfoLOD.Text = "Dynamic LOD in this game is bugged and should \r\ntherefore be disabled to have the best graphics-quality!";
            // 
            // GameOptionsForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1152, 798);
            Controls.Add(LblInfoLOD);
            Controls.Add(BtnCancel);
            Controls.Add(BtnApply);
            Controls.Add(BtnDefault);
            Controls.Add(LblResolution);
            Controls.Add(LblResolutionX);
            Controls.Add(ResolutionY);
            Controls.Add(ResolutionX);
            Controls.Add(LblDynamicLOD);
            Controls.Add(ChkDynamicLOD);
            Controls.Add(LblHeatEffects);
            Controls.Add(ChkHeatEffects);
            Controls.Add(LblShowAnimations);
            Controls.Add(ChkShowAnimations);
            Controls.Add(LblShowProps);
            Controls.Add(ChkShowProps);
            Controls.Add(LblSmoothWaterBorder);
            Controls.Add(ChkSmoothWaterBorder);
            Controls.Add(Lbl2DShadows);
            Controls.Add(Chk2DShadows);
            Controls.Add(Lbl3DShadows);
            Controls.Add(Chk3DShadows);
            Controls.Add(LblTerrainLighting);
            Controls.Add(ChkTerrainLighting);
            Controls.Add(LblAniTextureFiltering);
            Controls.Add(ChkAniTextureFiltering);
            Controls.Add(LblGameSettings);
            Controls.Add(PibBorderGameOptions);
            Controls.Add(LblOptions);
            Controls.Add(PibHeader);
            Controls.Add(PibBorderLauncherOptions);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "GameOptionsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Launcher and Game Settings";
            KeyDown += OptionsBFME1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)PibBorderLauncherOptions).EndInit();
            ((System.ComponentModel.ISupportInitialize)PibHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)PibBorderGameOptions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button BtnDefault;
        private System.Windows.Forms.PictureBox PibBorderLauncherOptions;
        private System.Windows.Forms.PictureBox PibHeader;
        private System.Windows.Forms.Label LblOptions;
        private System.Windows.Forms.PictureBox PibBorderGameOptions;
        private System.Windows.Forms.Label LblGameSettings;
        private System.Windows.Forms.Label LblAniTextureFiltering;
        private System.Windows.Forms.Button ChkAniTextureFiltering;
        private System.Windows.Forms.Button ChkTerrainLighting;
        private System.Windows.Forms.Label LblTerrainLighting;
        private System.Windows.Forms.Label Lbl2DShadows;
        private System.Windows.Forms.Button Chk2DShadows;
        private System.Windows.Forms.Label Lbl3DShadows;
        private System.Windows.Forms.Button Chk3DShadows;
        private System.Windows.Forms.Label LblShowProps;
        private System.Windows.Forms.Button ChkShowProps;
        private System.Windows.Forms.Label LblSmoothWaterBorder;
        private System.Windows.Forms.Button ChkSmoothWaterBorder;
        private System.Windows.Forms.Label LblDynamicLOD;
        private System.Windows.Forms.Button ChkDynamicLOD;
        private System.Windows.Forms.Label LblHeatEffects;
        private System.Windows.Forms.Button ChkHeatEffects;
        private System.Windows.Forms.Label LblShowAnimations;
        private System.Windows.Forms.Button ChkShowAnimations;
        private System.Windows.Forms.TextBox ResolutionX;
        private System.Windows.Forms.TextBox ResolutionY;
        private System.Windows.Forms.Label LblResolutionX;
        private System.Windows.Forms.Label LblResolution;
        private System.Windows.Forms.Button BtnApply;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label LblInfoLOD;
    }
}