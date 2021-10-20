namespace SPPC.Tools.SystemDesigner.Wizards.NewCrudEntityWizard
{
    partial class WizardOptionsPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkTsApiRouting = new System.Windows.Forms.CheckBox();
            this.chkTsViewModel = new System.Windows.Forms.CheckBox();
            this.chkPermEnum = new System.Windows.Forms.CheckBox();
            this.chkApiRoutes = new System.Windows.Forms.CheckBox();
            this.chkRepoImpl = new System.Windows.Forms.CheckBox();
            this.chkRepoInterface = new System.Windows.Forms.CheckBox();
            this.chkDbMapping = new System.Windows.Forms.CheckBox();
            this.chkViewModel = new System.Windows.Forms.CheckBox();
            this.chkModel = new System.Windows.Forms.CheckBox();
            this.chkWithImpl = new System.Windows.Forms.CheckBox();
            this.chkWithMethod = new System.Windows.Forms.CheckBox();
            this.chkController = new System.Windows.Forms.CheckBox();
            this.chkDbScript = new System.Windows.Forms.CheckBox();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkDbScript);
            this.grpOptions.Controls.Add(this.chkTsApiRouting);
            this.grpOptions.Controls.Add(this.chkTsViewModel);
            this.grpOptions.Controls.Add(this.chkPermEnum);
            this.grpOptions.Controls.Add(this.chkApiRoutes);
            this.grpOptions.Controls.Add(this.chkRepoImpl);
            this.grpOptions.Controls.Add(this.chkRepoInterface);
            this.grpOptions.Controls.Add(this.chkDbMapping);
            this.grpOptions.Controls.Add(this.chkViewModel);
            this.grpOptions.Controls.Add(this.chkModel);
            this.grpOptions.Controls.Add(this.chkWithImpl);
            this.grpOptions.Controls.Add(this.chkWithMethod);
            this.grpOptions.Controls.Add(this.chkController);
            this.grpOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOptions.Location = new System.Drawing.Point(0, 0);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(650, 420);
            this.grpOptions.TabIndex = 0;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Wizard Options";
            // 
            // chkTsApiRouting
            // 
            this.chkTsApiRouting.AutoSize = true;
            this.chkTsApiRouting.Location = new System.Drawing.Point(10, 316);
            this.chkTsApiRouting.Name = "chkTsApiRouting";
            this.chkTsApiRouting.Size = new System.Drawing.Size(240, 21);
            this.chkTsApiRouting.TabIndex = 12;
            this.chkTsApiRouting.Text = "Generate TypeScript API Routing";
            this.chkTsApiRouting.UseVisualStyleBackColor = true;
            // 
            // chkTsViewModel
            // 
            this.chkTsViewModel.AutoSize = true;
            this.chkTsViewModel.Location = new System.Drawing.Point(10, 289);
            this.chkTsViewModel.Name = "chkTsViewModel";
            this.chkTsViewModel.Size = new System.Drawing.Size(237, 21);
            this.chkTsViewModel.TabIndex = 11;
            this.chkTsViewModel.Text = "Generate TypeScript View Model";
            this.chkTsViewModel.UseVisualStyleBackColor = true;
            // 
            // chkPermEnum
            // 
            this.chkPermEnum.AutoSize = true;
            this.chkPermEnum.Location = new System.Drawing.Point(10, 262);
            this.chkPermEnum.Name = "chkPermEnum";
            this.chkPermEnum.Size = new System.Drawing.Size(210, 21);
            this.chkPermEnum.TabIndex = 10;
            this.chkPermEnum.Text = "Generate Permissions Enum";
            this.chkPermEnum.UseVisualStyleBackColor = true;
            this.chkPermEnum.CheckedChanged += new System.EventHandler(this.PermEnum_CheckedChanged);
            // 
            // chkApiRoutes
            // 
            this.chkApiRoutes.AutoSize = true;
            this.chkApiRoutes.Location = new System.Drawing.Point(10, 235);
            this.chkApiRoutes.Name = "chkApiRoutes";
            this.chkApiRoutes.Size = new System.Drawing.Size(168, 21);
            this.chkApiRoutes.TabIndex = 9;
            this.chkApiRoutes.Text = "Generate API Routing";
            this.chkApiRoutes.UseVisualStyleBackColor = true;
            // 
            // chkRepoImpl
            // 
            this.chkRepoImpl.AutoSize = true;
            this.chkRepoImpl.Location = new System.Drawing.Point(10, 208);
            this.chkRepoImpl.Name = "chkRepoImpl";
            this.chkRepoImpl.Size = new System.Drawing.Size(261, 21);
            this.chkRepoImpl.TabIndex = 8;
            this.chkRepoImpl.Text = "Generate Repository Implementation";
            this.chkRepoImpl.UseVisualStyleBackColor = true;
            // 
            // chkRepoInterface
            // 
            this.chkRepoInterface.AutoSize = true;
            this.chkRepoInterface.Location = new System.Drawing.Point(10, 181);
            this.chkRepoInterface.Name = "chkRepoInterface";
            this.chkRepoInterface.Size = new System.Drawing.Size(221, 21);
            this.chkRepoInterface.TabIndex = 7;
            this.chkRepoInterface.Text = "Generate Repository Interface";
            this.chkRepoInterface.UseVisualStyleBackColor = true;
            // 
            // chkDbMapping
            // 
            this.chkDbMapping.AutoSize = true;
            this.chkDbMapping.Location = new System.Drawing.Point(10, 125);
            this.chkDbMapping.Name = "chkDbMapping";
            this.chkDbMapping.Size = new System.Drawing.Size(170, 21);
            this.chkDbMapping.TabIndex = 5;
            this.chkDbMapping.Text = "Generate Db Mapping";
            this.chkDbMapping.UseVisualStyleBackColor = true;
            // 
            // chkViewModel
            // 
            this.chkViewModel.AutoSize = true;
            this.chkViewModel.Location = new System.Drawing.Point(10, 98);
            this.chkViewModel.Name = "chkViewModel";
            this.chkViewModel.Size = new System.Drawing.Size(165, 21);
            this.chkViewModel.TabIndex = 4;
            this.chkViewModel.Text = "Generate View Model";
            this.chkViewModel.UseVisualStyleBackColor = true;
            // 
            // chkModel
            // 
            this.chkModel.AutoSize = true;
            this.chkModel.Location = new System.Drawing.Point(10, 71);
            this.chkModel.Name = "chkModel";
            this.chkModel.Size = new System.Drawing.Size(132, 21);
            this.chkModel.TabIndex = 3;
            this.chkModel.Text = "Generate Model";
            this.chkModel.UseVisualStyleBackColor = true;
            // 
            // chkWithImpl
            // 
            this.chkWithImpl.AutoSize = true;
            this.chkWithImpl.Location = new System.Drawing.Point(413, 44);
            this.chkWithImpl.Name = "chkWithImpl";
            this.chkWithImpl.Size = new System.Drawing.Size(215, 21);
            this.chkWithImpl.TabIndex = 2;
            this.chkWithImpl.Text = "Starter CRUD implementation";
            this.chkWithImpl.UseVisualStyleBackColor = true;
            // 
            // chkWithMethod
            // 
            this.chkWithMethod.AutoSize = true;
            this.chkWithMethod.Location = new System.Drawing.Point(220, 44);
            this.chkWithMethod.Name = "chkWithMethod";
            this.chkWithMethod.Size = new System.Drawing.Size(174, 21);
            this.chkWithMethod.TabIndex = 1;
            this.chkWithMethod.Text = "Starter CRUD methods";
            this.chkWithMethod.UseVisualStyleBackColor = true;
            // 
            // chkController
            // 
            this.chkController.AutoSize = true;
            this.chkController.Location = new System.Drawing.Point(10, 44);
            this.chkController.Name = "chkController";
            this.chkController.Size = new System.Drawing.Size(180, 21);
            this.chkController.TabIndex = 0;
            this.chkController.Text = "Generate API Controller";
            this.chkController.UseVisualStyleBackColor = true;
            this.chkController.CheckedChanged += new System.EventHandler(this.Controller_CheckedChanged);
            // 
            // chkDbScript
            // 
            this.chkDbScript.AutoSize = true;
            this.chkDbScript.Location = new System.Drawing.Point(10, 153);
            this.chkDbScript.Name = "chkDbScript";
            this.chkDbScript.Size = new System.Drawing.Size(237, 21);
            this.chkDbScript.TabIndex = 6;
            this.chkDbScript.Text = "Generate CREATE TABLE Script";
            this.chkDbScript.UseVisualStyleBackColor = true;
            // 
            // WizardOptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpOptions);
            this.Name = "WizardOptionsPage";
            this.Size = new System.Drawing.Size(650, 420);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkController;
        private System.Windows.Forms.CheckBox chkWithMethod;
        private System.Windows.Forms.CheckBox chkWithImpl;
        private System.Windows.Forms.CheckBox chkViewModel;
        private System.Windows.Forms.CheckBox chkModel;
        private System.Windows.Forms.CheckBox chkDbMapping;
        private System.Windows.Forms.CheckBox chkTsApiRouting;
        private System.Windows.Forms.CheckBox chkTsViewModel;
        private System.Windows.Forms.CheckBox chkPermEnum;
        private System.Windows.Forms.CheckBox chkApiRoutes;
        private System.Windows.Forms.CheckBox chkRepoImpl;
        private System.Windows.Forms.CheckBox chkRepoInterface;
        private System.Windows.Forms.CheckBox chkDbScript;
    }
}
