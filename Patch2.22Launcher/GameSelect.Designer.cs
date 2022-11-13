namespace PatchLauncher
{
    partial class GameSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSelect));
            this.BtnBFME1 = new System.Windows.Forms.Button();
            this.BtnBFME2 = new System.Windows.Forms.Button();
            this.BtnBFME2EP1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnBFME1
            // 
            this.BtnBFME1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBFME1.BackgroundImage")));
            this.BtnBFME1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBFME1.Location = new System.Drawing.Point(12, 12);
            this.BtnBFME1.Name = "BtnBFME1";
            this.BtnBFME1.Size = new System.Drawing.Size(330, 360);
            this.BtnBFME1.TabIndex = 0;
            this.BtnBFME1.TabStop = false;
            this.BtnBFME1.UseVisualStyleBackColor = true;
            this.BtnBFME1.Click += new System.EventHandler(this.BtnBFME1_Click);
            // 
            // BtnBFME2
            // 
            this.BtnBFME2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBFME2.BackgroundImage")));
            this.BtnBFME2.Enabled = false;
            this.BtnBFME2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBFME2.Location = new System.Drawing.Point(348, 12);
            this.BtnBFME2.Name = "BtnBFME2";
            this.BtnBFME2.Size = new System.Drawing.Size(330, 360);
            this.BtnBFME2.TabIndex = 1;
            this.BtnBFME2.TabStop = false;
            this.BtnBFME2.UseVisualStyleBackColor = true;
            this.BtnBFME2.Click += new System.EventHandler(this.BtnBFME2_Click);
            // 
            // BtnBFME2EP1
            // 
            this.BtnBFME2EP1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBFME2EP1.BackgroundImage")));
            this.BtnBFME2EP1.Enabled = false;
            this.BtnBFME2EP1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBFME2EP1.Location = new System.Drawing.Point(684, 12);
            this.BtnBFME2EP1.Name = "BtnBFME2EP1";
            this.BtnBFME2EP1.Size = new System.Drawing.Size(330, 360);
            this.BtnBFME2EP1.TabIndex = 2;
            this.BtnBFME2EP1.TabStop = false;
            this.BtnBFME2EP1.UseVisualStyleBackColor = true;
            this.BtnBFME2EP1.Click += new System.EventHandler(this.BtnBFME2EP1_Click);
            // 
            // GameSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 381);
            this.Controls.Add(this.BtnBFME2EP1);
            this.Controls.Add(this.BtnBFME2);
            this.Controls.Add(this.BtnBFME1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GameSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSelect";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnBFME1;
        private System.Windows.Forms.Button BtnBFME2;
        private System.Windows.Forms.Button BtnBFME2EP1;
    }
}