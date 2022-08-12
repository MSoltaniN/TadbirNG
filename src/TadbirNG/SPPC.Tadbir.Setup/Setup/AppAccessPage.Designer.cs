
namespace SPPC.Tadbir.Setup
{
    partial class AppAccessPage
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
            this.grpAppAccess = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.radGlobal = new System.Windows.Forms.RadioButton();
            this.radLocal = new System.Windows.Forms.RadioButton();
            this.grpAppAccess.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAppAccess
            // 
            this.grpAppAccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAppAccess.Controls.Add(this.label8);
            this.grpAppAccess.Controls.Add(this.label4);
            this.grpAppAccess.Controls.Add(this.txtDomain);
            this.grpAppAccess.Controls.Add(this.radGlobal);
            this.grpAppAccess.Controls.Add(this.radLocal);
            this.grpAppAccess.Location = new System.Drawing.Point(3, 13);
            this.grpAppAccess.Name = "grpAppAccess";
            this.grpAppAccess.Size = new System.Drawing.Size(594, 125);
            this.grpAppAccess.TabIndex = 0;
            this.grpAppAccess.TabStop = false;
            this.grpAppAccess.Text = "دسترسی به برنامه";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(63, 78);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(56, 25);
            this.label8.TabIndex = 5;
            this.label8.Text = "http://";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(219, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "آدرس سرور (دامنه یا آی پی ثابت) :";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(119, 77);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(219, 27);
            this.txtDomain.TabIndex = 4;
            // 
            // radGlobal
            // 
            this.radGlobal.AutoSize = true;
            this.radGlobal.Location = new System.Drawing.Point(385, 78);
            this.radGlobal.Name = "radGlobal";
            this.radGlobal.Size = new System.Drawing.Size(200, 24);
            this.radGlobal.TabIndex = 2;
            this.radGlobal.TabStop = true;
            this.radGlobal.Text = "سراسری (روی شبکه اینترنت)";
            this.radGlobal.UseVisualStyleBackColor = true;
            this.radGlobal.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
            // 
            // radLocal
            // 
            this.radLocal.AutoSize = true;
            this.radLocal.Location = new System.Drawing.Point(364, 37);
            this.radLocal.Name = "radLocal";
            this.radLocal.Size = new System.Drawing.Size(221, 24);
            this.radLocal.TabIndex = 1;
            this.radLocal.TabStop = true;
            this.radLocal.Text = "محلی (فقط روی دستگاه سرور)";
            this.radLocal.UseVisualStyleBackColor = true;
            // 
            // AppAccessPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpAppAccess);
            this.Name = "AppAccessPage";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(600, 320);
            this.grpAppAccess.ResumeLayout(false);
            this.grpAppAccess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAppAccess;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.RadioButton radGlobal;
        private System.Windows.Forms.RadioButton radLocal;
    }
}
