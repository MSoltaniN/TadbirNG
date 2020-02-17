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
            this.menuGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateBlankController = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWizards = new System.Windows.Forms.ToolStripMenuItem();
            this.viewWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWizardsCrudManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManageLogCatalog = new System.Windows.Forms.ToolStripMenuItem();
            this.securityTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSecurity,
            this.menuGenerate,
            this.menuWizards,
            this.menuManage});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(754, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "Menu Bar";
            // 
            // menuSecurity
            // 
            this.menuSecurity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSecurityPermission});
            this.menuSecurity.Name = "menuSecurity";
            this.menuSecurity.Size = new System.Drawing.Size(61, 20);
            this.menuSecurity.Text = "&Security";
            // 
            // menuSecurityPermission
            // 
            this.menuSecurityPermission.Name = "menuSecurityPermission";
            this.menuSecurityPermission.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.menuSecurityPermission.Size = new System.Drawing.Size(254, 22);
            this.menuSecurityPermission.Text = "&Permission Designer";
            // 
            // menuGenerate
            // 
            this.menuGenerate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGenerateBlankController});
            this.menuGenerate.Name = "menuGenerate";
            this.menuGenerate.Size = new System.Drawing.Size(66, 20);
            this.menuGenerate.Text = "&Generate";
            // 
            // menuGenerateBlankController
            // 
            this.menuGenerateBlankController.Name = "menuGenerateBlankController";
            this.menuGenerateBlankController.Size = new System.Drawing.Size(157, 22);
            this.menuGenerateBlankController.Text = "API Controller...";
            this.menuGenerateBlankController.Click += new System.EventHandler(this.GenerateApiController_Click);
            // 
            // menuWizards
            // 
            this.menuWizards.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewWizardToolStripMenuItem,
            this.menuWizardsCrudManager});
            this.menuWizards.Name = "menuWizards";
            this.menuWizards.Size = new System.Drawing.Size(60, 20);
            this.menuWizards.Text = "Wizards";
            // 
            // viewWizardToolStripMenuItem
            // 
            this.viewWizardToolStripMenuItem.Name = "viewWizardToolStripMenuItem";
            this.viewWizardToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.viewWizardToolStripMenuItem.Text = "View Wizard";
            this.viewWizardToolStripMenuItem.Click += new System.EventHandler(this.viewWizardToolStripMenuItem_Click);
            // 
            // menuWizardsCrudManager
            // 
            this.menuWizardsCrudManager.Name = "menuWizardsCrudManager";
            this.menuWizardsCrudManager.Size = new System.Drawing.Size(203, 22);
            this.menuWizardsCrudManager.Text = "CRUD Manager Wizard...";
            this.menuWizardsCrudManager.Click += new System.EventHandler(this.WizardsCrudManager_Click);
            // 
            // menuManage
            // 
            this.menuManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManageLogCatalog,
            this.securityTicketToolStripMenuItem});
            this.menuManage.Name = "menuManage";
            this.menuManage.Size = new System.Drawing.Size(62, 20);
            this.menuManage.Text = "&Manage";
            // 
            // menuManageLogCatalog
            // 
            this.menuManageLogCatalog.Name = "menuManageLogCatalog";
            this.menuManageLogCatalog.Size = new System.Drawing.Size(180, 22);
            this.menuManageLogCatalog.Text = "Log Catalog";
            this.menuManageLogCatalog.Click += new System.EventHandler(this.ManageLogCatalog_Click);
            // 
            // securityTicketToolStripMenuItem
            // 
            this.securityTicketToolStripMenuItem.Name = "securityTicketToolStripMenuItem";
            this.securityTicketToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.securityTicketToolStripMenuItem.Text = "Security Ticket";
            this.securityTicketToolStripMenuItem.Click += new System.EventHandler(this.ManageSecurityTicket_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 586);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
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
        private System.Windows.Forms.ToolStripMenuItem menuWizards;
        private System.Windows.Forms.ToolStripMenuItem viewWizardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuGenerate;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateBlankController;
        private System.Windows.Forms.ToolStripMenuItem menuWizardsCrudManager;
        private System.Windows.Forms.ToolStripMenuItem menuManage;
        private System.Windows.Forms.ToolStripMenuItem menuManageLogCatalog;
        private System.Windows.Forms.ToolStripMenuItem securityTicketToolStripMenuItem;
    }
}

