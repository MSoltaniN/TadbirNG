namespace SPPC.Tadbir.Tools.SystemDesigner
{
    partial class MainWindow
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSecurityPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.wizardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSecurity,
            this.wizardsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1006, 28);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "Menu Bar";
            // 
            // menuSecurity
            // 
            this.menuSecurity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSecurityPermission});
            this.menuSecurity.Name = "menuSecurity";
            this.menuSecurity.Size = new System.Drawing.Size(73, 24);
            this.menuSecurity.Text = "&Security";
            // 
            // menuSecurityPermission
            // 
            this.menuSecurityPermission.Name = "menuSecurityPermission";
            this.menuSecurityPermission.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.menuSecurityPermission.Size = new System.Drawing.Size(307, 26);
            this.menuSecurityPermission.Text = "&Permission Designer";
            // 
            // wizardsToolStripMenuItem
            // 
            this.wizardsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewWizardToolStripMenuItem});
            this.wizardsToolStripMenuItem.Name = "wizardsToolStripMenuItem";
            this.wizardsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.wizardsToolStripMenuItem.Text = "Wizards";
            // 
            // viewWizardToolStripMenuItem
            // 
            this.viewWizardToolStripMenuItem.Name = "viewWizardToolStripMenuItem";
            this.viewWizardToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.viewWizardToolStripMenuItem.Text = "View Wizard";
            this.viewWizardToolStripMenuItem.Click += new System.EventHandler(this.viewWizardToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tadbir NG - System Designer";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuSecurity;
        private System.Windows.Forms.ToolStripMenuItem menuSecurityPermission;
        private System.Windows.Forms.ToolStripMenuItem wizardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewWizardToolStripMenuItem;
    }
}

