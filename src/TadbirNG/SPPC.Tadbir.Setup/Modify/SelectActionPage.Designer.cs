
namespace SPPC.Tadbir.Setup
{
    partial class SelectActionPage
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
            this.radModify = new System.Windows.Forms.RadioButton();
            this.radRemove = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radModify
            // 
            this.radModify.AutoSize = true;
            this.radModify.Enabled = false;
            this.radModify.Location = new System.Drawing.Point(421, 37);
            this.radModify.Name = "radModify";
            this.radModify.Size = new System.Drawing.Size(157, 24);
            this.radModify.TabIndex = 0;
            this.radModify.TabStop = true;
            this.radModify.Text = "تغییر تنظیمات نصب";
            this.radModify.UseVisualStyleBackColor = true;
            // 
            // radRemove
            // 
            this.radRemove.AutoSize = true;
            this.radRemove.Location = new System.Drawing.Point(476, 76);
            this.radRemove.Name = "radRemove";
            this.radRemove.Size = new System.Drawing.Size(102, 24);
            this.radRemove.TabIndex = 1;
            this.radRemove.TabStop = true;
            this.radRemove.Text = "حذف برنامه";
            this.radRemove.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(587, 60);
            this.label1.TabIndex = 2;
            this.label1.Text = "برنامه تدبیر روی این ایستگاه کاری نصب شده است. لطفاً عملیات مورد نظر خود را انتخا" +
    "ب کرده و روی دکمه \"بعدی\" کلیک کنید.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radModify);
            this.groupBox1.Controls.Add(this.radRemove);
            this.groupBox1.Location = new System.Drawing.Point(7, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 121);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // SelectActionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "SelectActionPage";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(600, 320);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radModify;
        private System.Windows.Forms.RadioButton radRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
