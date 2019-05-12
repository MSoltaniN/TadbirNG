namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    partial class SelectViewModelForm
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
            this.tvViewModels = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tvViewModels
            // 
            this.tvViewModels.Location = new System.Drawing.Point(16, 48);
            this.tvViewModels.Name = "tvViewModels";
            this.tvViewModels.Size = new System.Drawing.Size(387, 475);
            this.tvViewModels.TabIndex = 0;
            this.tvViewModels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvViewModels_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "All view models :";
            // 
            // SelectViewModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvViewModels);
            this.Name = "SelectViewModelForm";
            this.Size = new System.Drawing.Size(420, 541);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvViewModels;
        private System.Windows.Forms.Label label1;
    }
}
