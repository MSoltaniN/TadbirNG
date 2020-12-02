namespace SPPC.Tools.TadbirActivator
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLicenseInfo = new System.Windows.Forms.TextBox();
            this.btnActivate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "مشخصات برنامه";
            // 
            // txtLicenseInfo
            // 
            this.txtLicenseInfo.Location = new System.Drawing.Point(16, 139);
            this.txtLicenseInfo.Multiline = true;
            this.txtLicenseInfo.Name = "txtLicenseInfo";
            this.txtLicenseInfo.ReadOnly = true;
            this.txtLicenseInfo.Size = new System.Drawing.Size(635, 333);
            this.txtLicenseInfo.TabIndex = 4;
            // 
            // btnActivate
            // 
            this.btnActivate.Location = new System.Drawing.Point(16, 500);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(137, 39);
            this.btnActivate.TabIndex = 0;
            this.btnActivate.Text = "فعال سازی";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.Activate_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(159, 500);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 39);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "خروج";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(637, 62);
            this.label2.TabIndex = 2;
            this.label2.Text = "فعال سازی اولیه این محصول فقط به صورت آنلاین امکان پذیر است. لطفاً پیش از فعال سا" +
    "زی، از اتصال اینترنت دستگاه سرور اطمینان حاصل نمایید.";
            // 
            // MainWindow
            // 
            this.AcceptButton = this.btnActivate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(667, 549);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.txtLicenseInfo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "فعال ساز تدبیر تحت وب - پردازش موازی سامان";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLicenseInfo;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
    }
}

