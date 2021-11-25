namespace SPPC.Tools.SystemDesigner.Forms
{
    partial class TicketManager
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
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnShowValue = new System.Windows.Forms.Button();
            this.btnShowTicket = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Ticket :";
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(16, 45);
            this.txtTicket.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTicket.Multiline = true;
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTicket.Size = new System.Drawing.Size(575, 227);
            this.txtTicket.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 303);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current Value :";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(16, 328);
            this.txtValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtValue.Size = new System.Drawing.Size(575, 227);
            this.txtValue.TabIndex = 3;
            // 
            // btnShowValue
            // 
            this.btnShowValue.Location = new System.Drawing.Point(16, 580);
            this.btnShowValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShowValue.Name = "btnShowValue";
            this.btnShowValue.Size = new System.Drawing.Size(100, 42);
            this.btnShowValue.TabIndex = 4;
            this.btnShowValue.Text = "Show Value";
            this.btnShowValue.UseVisualStyleBackColor = true;
            this.btnShowValue.Click += new System.EventHandler(this.ShowValue_Click);
            // 
            // btnShowTicket
            // 
            this.btnShowTicket.Location = new System.Drawing.Point(124, 580);
            this.btnShowTicket.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShowTicket.Name = "btnShowTicket";
            this.btnShowTicket.Size = new System.Drawing.Size(100, 42);
            this.btnShowTicket.TabIndex = 5;
            this.btnShowTicket.Text = "Show Ticket";
            this.btnShowTicket.UseVisualStyleBackColor = true;
            this.btnShowTicket.Click += new System.EventHandler(this.ShowTicket_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(492, 580);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 42);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // TicketManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(611, 631);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnShowTicket);
            this.Controls.Add(this.btnShowValue);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTicket);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TicketManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TicketManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTicket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnShowValue;
        private System.Windows.Forms.Button btnShowTicket;
        private System.Windows.Forms.Button btnCancel;
    }
}