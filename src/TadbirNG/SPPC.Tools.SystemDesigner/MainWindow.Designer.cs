namespace SPPC.Tools.SystemDesigner
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
            this.menuMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMetadataEntities = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMetadataListViews = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMetadataPermissions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMetadataReports = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMetadataLogSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMetadataMenus = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMetadataShortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWizards = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWizardsCrudManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateBlankController = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateFixDbScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManageSecurityTicket = new System.Windows.Forms.ToolStripMenuItem();
            this.setupEnvironmentWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMetadata,
            this.menuWizards,
            this.menuGenerate,
            this.menuManage});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(942, 28);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "Menu Bar";
            // 
            // menuMetadata
            // 
            this.menuMetadata.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMetadataEntities,
            this.menuMetadataListViews,
            this.menuMetadataPermissions,
            this.menuMetadataReports,
            this.menuMetadataLogSettings,
            this.menuMetadataMenus,
            this.menuMetadataShortcuts});
            this.menuMetadata.Name = "menuMetadata";
            this.menuMetadata.Size = new System.Drawing.Size(87, 24);
            this.menuMetadata.Text = "&Metadata";
            // 
            // menuMetadataEntities
            // 
            this.menuMetadataEntities.Name = "menuMetadataEntities";
            this.menuMetadataEntities.Size = new System.Drawing.Size(174, 26);
            this.menuMetadataEntities.Text = "&Entities";
            this.menuMetadataEntities.Click += new System.EventHandler(this.MetadataEntities_Click);
            // 
            // menuMetadataListViews
            // 
            this.menuMetadataListViews.Name = "menuMetadataListViews";
            this.menuMetadataListViews.Size = new System.Drawing.Size(174, 26);
            this.menuMetadataListViews.Text = "&List Views";
            this.menuMetadataListViews.Click += new System.EventHandler(this.MetadataListViews_Click);
            // 
            // menuMetadataPermissions
            // 
            this.menuMetadataPermissions.Name = "menuMetadataPermissions";
            this.menuMetadataPermissions.Size = new System.Drawing.Size(174, 26);
            this.menuMetadataPermissions.Text = "&Permissions";
            this.menuMetadataPermissions.Click += new System.EventHandler(this.MetadataPermissions_Click);
            // 
            // menuMetadataReports
            // 
            this.menuMetadataReports.Name = "menuMetadataReports";
            this.menuMetadataReports.Size = new System.Drawing.Size(174, 26);
            this.menuMetadataReports.Text = "&Reports";
            this.menuMetadataReports.Click += new System.EventHandler(this.MetadataReports_Click);
            // 
            // menuMetadataLogSettings
            // 
            this.menuMetadataLogSettings.Name = "menuMetadataLogSettings";
            this.menuMetadataLogSettings.Size = new System.Drawing.Size(174, 26);
            this.menuMetadataLogSettings.Text = "&Log Settings";
            this.menuMetadataLogSettings.Click += new System.EventHandler(this.MetadataLogSettings_Click);
            // 
            // menuMetadataMenus
            // 
            this.menuMetadataMenus.Name = "menuMetadataMenus";
            this.menuMetadataMenus.Size = new System.Drawing.Size(174, 26);
            this.menuMetadataMenus.Text = "&Menus";
            this.menuMetadataMenus.Click += new System.EventHandler(this.MetadataMenus_Click);
            // 
            // menuMetadataShortcuts
            // 
            this.menuMetadataShortcuts.Name = "menuMetadataShortcuts";
            this.menuMetadataShortcuts.Size = new System.Drawing.Size(174, 26);
            this.menuMetadataShortcuts.Text = "&Shortcuts";
            this.menuMetadataShortcuts.Click += new System.EventHandler(this.MetadataShortcuts_Click);
            // 
            // menuWizards
            // 
            this.menuWizards.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuWizardsCrudManager,
            this.setupEnvironmentWizardToolStripMenuItem});
            this.menuWizards.Name = "menuWizards";
            this.menuWizards.Size = new System.Drawing.Size(76, 24);
            this.menuWizards.Text = "Wizards";
            // 
            // menuWizardsCrudManager
            // 
            this.menuWizardsCrudManager.Name = "menuWizardsCrudManager";
            this.menuWizardsCrudManager.Size = new System.Drawing.Size(277, 26);
            this.menuWizardsCrudManager.Text = "CRUD Manager Wizard...";
            this.menuWizardsCrudManager.Click += new System.EventHandler(this.WizardsCrudManager_Click);
            // 
            // menuGenerate
            // 
            this.menuGenerate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGenerateBlankController,
            this.menuGenerateFixDbScript});
            this.menuGenerate.Name = "menuGenerate";
            this.menuGenerate.Size = new System.Drawing.Size(83, 24);
            this.menuGenerate.Text = "&Generate";
            // 
            // menuGenerateBlankController
            // 
            this.menuGenerateBlankController.Name = "menuGenerateBlankController";
            this.menuGenerateBlankController.Size = new System.Drawing.Size(199, 26);
            this.menuGenerateBlankController.Text = "API Controller...";
            this.menuGenerateBlankController.Click += new System.EventHandler(this.GenerateApiController_Click);
            // 
            // menuGenerateFixDbScript
            // 
            this.menuGenerateFixDbScript.Name = "menuGenerateFixDbScript";
            this.menuGenerateFixDbScript.Size = new System.Drawing.Size(199, 26);
            this.menuGenerateFixDbScript.Text = "Arabic Fix Script";
            this.menuGenerateFixDbScript.Click += new System.EventHandler(this.GenerateFixDbScript_Click);
            // 
            // menuManage
            // 
            this.menuManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManageSecurityTicket});
            this.menuManage.Name = "menuManage";
            this.menuManage.Size = new System.Drawing.Size(62, 24);
            this.menuManage.Text = "&Utility";
            // 
            // menuManageSecurityTicket
            // 
            this.menuManageSecurityTicket.Name = "menuManageSecurityTicket";
            this.menuManageSecurityTicket.Size = new System.Drawing.Size(187, 26);
            this.menuManageSecurityTicket.Text = "Security Ticket";
            this.menuManageSecurityTicket.Click += new System.EventHandler(this.ManageSecurityTicket_Click);
            // 
            // setupEnvironmentWizardToolStripMenuItem
            // 
            this.setupEnvironmentWizardToolStripMenuItem.Name = "setupEnvironmentWizardToolStripMenuItem";
            this.setupEnvironmentWizardToolStripMenuItem.Size = new System.Drawing.Size(277, 26);
            this.setupEnvironmentWizardToolStripMenuItem.Text = "Setup Environment Wizard...";
            this.setupEnvironmentWizardToolStripMenuItem.Click += new System.EventHandler(this.WizardsEnvSetup_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 643);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tadbir NG - System Designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuMetadata;
        private System.Windows.Forms.ToolStripMenuItem menuMetadataPermissions;
        private System.Windows.Forms.ToolStripMenuItem menuWizards;
        private System.Windows.Forms.ToolStripMenuItem menuGenerate;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateBlankController;
        private System.Windows.Forms.ToolStripMenuItem menuWizardsCrudManager;
        private System.Windows.Forms.ToolStripMenuItem menuManage;
        private System.Windows.Forms.ToolStripMenuItem menuManageSecurityTicket;
        private System.Windows.Forms.ToolStripMenuItem menuMetadataReports;
        private System.Windows.Forms.ToolStripMenuItem menuMetadataEntities;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateFixDbScript;
        private System.Windows.Forms.ToolStripMenuItem menuMetadataListViews;
        private System.Windows.Forms.ToolStripMenuItem menuMetadataLogSettings;
        private System.Windows.Forms.ToolStripMenuItem menuMetadataMenus;
        private System.Windows.Forms.ToolStripMenuItem menuMetadataShortcuts;
        private System.Windows.Forms.ToolStripMenuItem setupEnvironmentWizardToolStripMenuItem;
    }
}

