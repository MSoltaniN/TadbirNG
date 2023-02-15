
namespace SPPC.Tools.SystemDesigner.Forms
{
    partial class MenuBrowserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tvMenus = new System.Windows.Forms.TreeView();
            this.btnNewChild = new System.Windows.Forms.Button();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.cmbPermission = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPermissionGroup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHotKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIconName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRouteUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTitleKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewSibling = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grpProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "All Menus :";
            // 
            // tvMenus
            // 
            this.tvMenus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvMenus.HideSelection = false;
            this.tvMenus.Location = new System.Drawing.Point(12, 56);
            this.tvMenus.Name = "tvMenus";
            this.tvMenus.Size = new System.Drawing.Size(382, 485);
            this.tvMenus.TabIndex = 1;
            this.tvMenus.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.Menus_BeforeSelect);
            this.tvMenus.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Menus_AfterSelect);
            // 
            // btnNewChild
            // 
            this.btnNewChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewChild.Location = new System.Drawing.Point(12, 556);
            this.btnNewChild.Name = "btnNewChild";
            this.btnNewChild.Size = new System.Drawing.Size(115, 35);
            this.btnNewChild.TabIndex = 2;
            this.btnNewChild.Text = "New Child";
            this.btnNewChild.UseVisualStyleBackColor = true;
            this.btnNewChild.Click += new System.EventHandler(this.NewChild_Click);
            // 
            // grpProperties
            // 
            this.grpProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProperties.Controls.Add(this.cmbPermission);
            this.grpProperties.Controls.Add(this.label7);
            this.grpProperties.Controls.Add(this.cmbPermissionGroup);
            this.grpProperties.Controls.Add(this.label6);
            this.grpProperties.Controls.Add(this.txtHotKey);
            this.grpProperties.Controls.Add(this.label5);
            this.grpProperties.Controls.Add(this.txtIconName);
            this.grpProperties.Controls.Add(this.label4);
            this.grpProperties.Controls.Add(this.txtRouteUrl);
            this.grpProperties.Controls.Add(this.label3);
            this.grpProperties.Controls.Add(this.txtTitleKey);
            this.grpProperties.Controls.Add(this.label2);
            this.grpProperties.Location = new System.Drawing.Point(414, 46);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(406, 495);
            this.grpProperties.TabIndex = 3;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Menu Properties";
            // 
            // cmbPermission
            // 
            this.cmbPermission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermission.FormattingEnabled = true;
            this.cmbPermission.Location = new System.Drawing.Point(16, 441);
            this.cmbPermission.Name = "cmbPermission";
            this.cmbPermission.Size = new System.Drawing.Size(379, 28);
            this.cmbPermission.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 412);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Permission :";
            // 
            // cmbPermissionGroup
            // 
            this.cmbPermissionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPermissionGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermissionGroup.FormattingEnabled = true;
            this.cmbPermissionGroup.Location = new System.Drawing.Point(16, 365);
            this.cmbPermissionGroup.Name = "cmbPermissionGroup";
            this.cmbPermissionGroup.Size = new System.Drawing.Size(379, 28);
            this.cmbPermissionGroup.TabIndex = 11;
            this.cmbPermissionGroup.SelectedIndexChanged += new System.EventHandler(this.PermissionGroup_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 336);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Permission Group :";
            // 
            // txtHotKey
            // 
            this.txtHotKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotKey.Location = new System.Drawing.Point(16, 291);
            this.txtHotKey.Name = "txtHotKey";
            this.txtHotKey.Size = new System.Drawing.Size(379, 27);
            this.txtHotKey.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Hot Key :";
            // 
            // txtIconName
            // 
            this.txtIconName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIconName.Location = new System.Drawing.Point(16, 219);
            this.txtIconName.Name = "txtIconName";
            this.txtIconName.Size = new System.Drawing.Size(379, 27);
            this.txtIconName.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Icon Name :";
            // 
            // txtRouteUrl
            // 
            this.txtRouteUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRouteUrl.Location = new System.Drawing.Point(16, 149);
            this.txtRouteUrl.Name = "txtRouteUrl";
            this.txtRouteUrl.Size = new System.Drawing.Size(379, 27);
            this.txtRouteUrl.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Route URL :";
            // 
            // txtTitleKey
            // 
            this.txtTitleKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitleKey.Location = new System.Drawing.Point(16, 77);
            this.txtTitleKey.Name = "txtTitleKey";
            this.txtTitleKey.Size = new System.Drawing.Size(379, 27);
            this.txtTitleKey.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Title Key :";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(254, 556);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(115, 35);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // btnNewSibling
            // 
            this.btnNewSibling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewSibling.Location = new System.Drawing.Point(133, 556);
            this.btnNewSibling.Name = "btnNewSibling";
            this.btnNewSibling.Size = new System.Drawing.Size(115, 35);
            this.btnNewSibling.TabIndex = 6;
            this.btnNewSibling.Text = "New Sibling";
            this.btnNewSibling.UseVisualStyleBackColor = true;
            this.btnNewSibling.Click += new System.EventHandler(this.NewSibling_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(714, 556);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 35);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(551, 556);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(131, 35);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "Generate Scripts";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(414, 556);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(131, 35);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh Tree";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // MenuBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(832, 603);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNewSibling);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grpProperties);
            this.Controls.Add(this.btnNewChild);
            this.Controls.Add(this.tvMenus);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "MenuBrowserForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Browse Application Menus";
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvMenus;
        private System.Windows.Forms.Button btnNewChild;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.ComboBox cmbPermission;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbPermissionGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHotKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIconName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRouteUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTitleKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNewSibling;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnRefresh;
    }
}