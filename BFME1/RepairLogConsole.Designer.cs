namespace PatchLauncher
{
    partial class RepairLogConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepairLogConsole));
            TxtConsole = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // TxtConsole
            // 
            TxtConsole.BackColor = System.Drawing.Color.Black;
            TxtConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            TxtConsole.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            resources.ApplyResources(TxtConsole, "TxtConsole");
            TxtConsole.ForeColor = System.Drawing.Color.OrangeRed;
            TxtConsole.Name = "TxtConsole";
            TxtConsole.ReadOnly = true;
            // 
            // RepairLogConsole
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(TxtConsole);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            Name = "RepairLogConsole";
            ShowInTaskbar = false;
            FormClosing += RepairLogConsole_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.TextBox TxtConsole;
    }
}