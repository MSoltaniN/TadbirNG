﻿
namespace SPPC.Tools.LicenseManager
{
    partial class InstanceInfoEditor
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBaseUrl = new System.Windows.Forms.TextBox();
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.txtInstanceKey = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveInstance = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(15, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "آدرس سرویس وب :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(15, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "آدرس سرور آفلاین مجوزها :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(15, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "شناسه برنامه :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(15, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "نسخه برنامه :";
            // 
            // txtBaseUrl
            // 
            this.txtBaseUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaseUrl.Location = new System.Drawing.Point(218, 29);
            this.txtBaseUrl.Name = "txtBaseUrl";
            this.txtBaseUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBaseUrl.Size = new System.Drawing.Size(446, 26);
            this.txtBaseUrl.TabIndex = 4;
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerUrl.Location = new System.Drawing.Point(218, 77);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtServerUrl.Size = new System.Drawing.Size(446, 26);
            this.txtServerUrl.TabIndex = 5;
            // 
            // txtInstanceKey
            // 
            this.txtInstanceKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstanceKey.Location = new System.Drawing.Point(218, 125);
            this.txtInstanceKey.Multiline = true;
            this.txtInstanceKey.Name = "txtInstanceKey";
            this.txtInstanceKey.ReadOnly = true;
            this.txtInstanceKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtInstanceKey.Size = new System.Drawing.Size(446, 92);
            this.txtInstanceKey.TabIndex = 6;
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(218, 240);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(222, 26);
            this.txtVersion.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.Location = new System.Drawing.Point(550, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 36);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // btnSaveInstance
            // 
            this.btnSaveInstance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveInstance.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveInstance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSaveInstance.Location = new System.Drawing.Point(424, 305);
            this.btnSaveInstance.Name = "btnSaveInstance";
            this.btnSaveInstance.Size = new System.Drawing.Size(120, 36);
            this.btnSaveInstance.TabIndex = 8;
            this.btnSaveInstance.Text = "ذخیره نمونه";
            this.btnSaveInstance.UseVisualStyleBackColor = true;
            this.btnSaveInstance.Click += new System.EventHandler(this.SaveInstanceButton_Click);
            // 
            // InstanceInfoEditor
            // 
            this.AcceptButton = this.btnSaveInstance;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(676, 349);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveInstance);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtInstanceKey);
            this.Controls.Add(this.txtServerUrl);
            this.Controls.Add(this.txtBaseUrl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MinimizeBox = false;
            this.Name = "InstanceInfoEditor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "اطلاعات نمونه برنامه";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBaseUrl;
        private System.Windows.Forms.TextBox txtServerUrl;
        private System.Windows.Forms.TextBox txtInstanceKey;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveInstance;
    }
}