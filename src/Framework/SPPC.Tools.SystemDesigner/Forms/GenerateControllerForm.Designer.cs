namespace SPPC.Tools.SystemDesigner.Forms
{
    partial class GenerateControllerForm
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnChangePath = new System.Windows.Forms.Button();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkWithImpl = new System.Windows.Forms.CheckBox();
            this.chkWithMethod = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtEntityArea = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsFiscal = new System.Windows.Forms.CheckBox();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Controller file will be generated in the following path :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Configured path : ";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(133, 45);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(357, 22);
            this.txtPath.TabIndex = 2;
            // 
            // btnChangePath
            // 
            this.btnChangePath.Location = new System.Drawing.Point(496, 44);
            this.btnChangePath.Name = "btnChangePath";
            this.btnChangePath.Size = new System.Drawing.Size(102, 25);
            this.btnChangePath.TabIndex = 3;
            this.btnChangePath.Text = "Change...";
            this.btnChangePath.UseVisualStyleBackColor = true;
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkWithImpl);
            this.grpOptions.Controls.Add(this.chkWithMethod);
            this.grpOptions.Location = new System.Drawing.Point(12, 172);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(586, 99);
            this.grpOptions.TabIndex = 9;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Generation Options";
            // 
            // chkWithImpl
            // 
            this.chkWithImpl.AutoSize = true;
            this.chkWithImpl.Location = new System.Drawing.Point(11, 63);
            this.chkWithImpl.Name = "chkWithImpl";
            this.chkWithImpl.Size = new System.Drawing.Size(262, 21);
            this.chkWithImpl.TabIndex = 1;
            this.chkWithImpl.Text = "Include starter CRUD implementation";
            this.chkWithImpl.UseVisualStyleBackColor = true;
            // 
            // chkWithMethod
            // 
            this.chkWithMethod.AutoSize = true;
            this.chkWithMethod.Location = new System.Drawing.Point(11, 36);
            this.chkWithMethod.Name = "chkWithMethod";
            this.chkWithMethod.Size = new System.Drawing.Size(221, 21);
            this.chkWithMethod.TabIndex = 0;
            this.chkWithMethod.Text = "Include starter CRUD methods";
            this.chkWithMethod.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Entity name :";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(133, 76);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(152, 22);
            this.txtEntityName.TabIndex = 5;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 300);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(115, 25);
            this.btnGenerate.TabIndex = 10;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(133, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtEntityArea
            // 
            this.txtEntityArea.Location = new System.Drawing.Point(133, 106);
            this.txtEntityArea.Name = "txtEntityArea";
            this.txtEntityArea.Size = new System.Drawing.Size(152, 22);
            this.txtEntityArea.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Entity area :";
            // 
            // chkIsFiscal
            // 
            this.chkIsFiscal.AutoSize = true;
            this.chkIsFiscal.Location = new System.Drawing.Point(302, 78);
            this.chkIsFiscal.Name = "chkIsFiscal";
            this.chkIsFiscal.Size = new System.Drawing.Size(263, 21);
            this.chkIsFiscal.TabIndex = 6;
            this.chkIsFiscal.Text = "Depends on fiscal period and branch";
            this.chkIsFiscal.UseVisualStyleBackColor = true;
            // 
            // GenerateControllerForm
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(610, 334);
            this.Controls.Add(this.chkIsFiscal);
            this.Controls.Add(this.txtEntityArea);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtEntityName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.btnChangePath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateControllerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate New API Controller";
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnChangePath;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkWithImpl;
        private System.Windows.Forms.CheckBox chkWithMethod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtEntityArea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIsFiscal;
    }
}