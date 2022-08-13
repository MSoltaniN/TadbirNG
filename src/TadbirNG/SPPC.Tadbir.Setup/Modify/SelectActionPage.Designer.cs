
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
            this.SuspendLayout();
            // 
            // radModify
            // 
            this.radModify.AutoSize = true;
            this.radModify.Location = new System.Drawing.Point(433, 15);
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
            this.radRemove.Location = new System.Drawing.Point(488, 54);
            this.radRemove.Name = "radRemove";
            this.radRemove.Size = new System.Drawing.Size(102, 24);
            this.radRemove.TabIndex = 1;
            this.radRemove.TabStop = true;
            this.radRemove.Text = "حذف برنامه";
            this.radRemove.UseVisualStyleBackColor = true;
            // 
            // SelectActionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radRemove);
            this.Controls.Add(this.radModify);
            this.Name = "SelectActionPage";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(600, 320);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radModify;
        private System.Windows.Forms.RadioButton radRemove;
    }
}
