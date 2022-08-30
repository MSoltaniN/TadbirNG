
namespace SPPC.Tadbir.Setup
{
    partial class SettingsPage
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
            this.chkCreateShortcut = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtInstallPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkCreateShortcut
            // 
            this.chkCreateShortcut.AutoSize = true;
            this.chkCreateShortcut.Checked = true;
            this.chkCreateShortcut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateShortcut.Location = new System.Drawing.Point(384, 69);
            this.chkCreateShortcut.Name = "chkCreateShortcut";
            this.chkCreateShortcut.Size = new System.Drawing.Size(213, 24);
            this.chkCreateShortcut.TabIndex = 7;
            this.chkCreateShortcut.Text = "ایجاد میانبر برنامه روی میز کار";
            this.chkCreateShortcut.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(3, 35);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(94, 29);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "انتخاب...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // txtInstallPath
            // 
            this.txtInstallPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstallPath.Location = new System.Drawing.Point(103, 36);
            this.txtInstallPath.Name = "txtInstallPath";
            this.txtInstallPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtInstallPath.Size = new System.Drawing.Size(494, 27);
            this.txtInstallPath.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(470, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "مسیر نصب برنامه :";
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkCreateShortcut);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtInstallPath);
            this.Controls.Add(this.label2);
            this.Name = "SettingsPage";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(600, 320);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCreateShortcut;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtInstallPath;
        private System.Windows.Forms.Label label2;
    }
}
