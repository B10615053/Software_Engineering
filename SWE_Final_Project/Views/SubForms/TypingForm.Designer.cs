namespace SWE_Final_Project.Views.SubForms {
    partial class TypingForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancelAtTypingForm = new System.Windows.Forms.Button();
            this.btnConfirmAtTypingForm = new System.Windows.Forms.Button();
            this.txtLetUserEnterAtTypingForm = new System.Windows.Forms.TextBox();
            this.lblHintForUser = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancelAtTypingForm, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnConfirmAtTypingForm, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtLetUserEnterAtTypingForm, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHintForUser, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(536, 165);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCancelAtTypingForm
            // 
            this.btnCancelAtTypingForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancelAtTypingForm.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnCancelAtTypingForm.Location = new System.Drawing.Point(13, 107);
            this.btnCancelAtTypingForm.Margin = new System.Windows.Forms.Padding(13, 13, 7, 13);
            this.btnCancelAtTypingForm.Name = "btnCancelAtTypingForm";
            this.btnCancelAtTypingForm.Size = new System.Drawing.Size(248, 45);
            this.btnCancelAtTypingForm.TabIndex = 2;
            this.btnCancelAtTypingForm.Text = "Cancel";
            this.btnCancelAtTypingForm.UseVisualStyleBackColor = true;
            this.btnCancelAtTypingForm.Click += new System.EventHandler(this.BtnCancelAtTypingForm_Click);
            // 
            // btnConfirmAtTypingForm
            // 
            this.btnConfirmAtTypingForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConfirmAtTypingForm.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnConfirmAtTypingForm.Location = new System.Drawing.Point(275, 107);
            this.btnConfirmAtTypingForm.Margin = new System.Windows.Forms.Padding(7, 13, 13, 13);
            this.btnConfirmAtTypingForm.Name = "btnConfirmAtTypingForm";
            this.btnConfirmAtTypingForm.Size = new System.Drawing.Size(248, 45);
            this.btnConfirmAtTypingForm.TabIndex = 1;
            this.btnConfirmAtTypingForm.Text = "OK";
            this.btnConfirmAtTypingForm.UseVisualStyleBackColor = true;
            this.btnConfirmAtTypingForm.Click += new System.EventHandler(this.BtnConfirmAtTypingForm_Click);
            // 
            // txtLetUserEnterAtTypingForm
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtLetUserEnterAtTypingForm, 2);
            this.txtLetUserEnterAtTypingForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLetUserEnterAtTypingForm.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtLetUserEnterAtTypingForm.Location = new System.Drawing.Point(13, 60);
            this.txtLetUserEnterAtTypingForm.Margin = new System.Windows.Forms.Padding(13, 13, 13, 0);
            this.txtLetUserEnterAtTypingForm.Name = "txtLetUserEnterAtTypingForm";
            this.txtLetUserEnterAtTypingForm.Size = new System.Drawing.Size(510, 31);
            this.txtLetUserEnterAtTypingForm.TabIndex = 3;
            this.txtLetUserEnterAtTypingForm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLetUserEnterAtTypingForm_KeyPress);
            // 
            // lblHintForUser
            // 
            this.lblHintForUser.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblHintForUser, 2);
            this.lblHintForUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHintForUser.Font = new System.Drawing.Font("Consolas", 12F);
            this.lblHintForUser.Location = new System.Drawing.Point(13, 13);
            this.lblHintForUser.Margin = new System.Windows.Forms.Padding(13, 13, 13, 0);
            this.lblHintForUser.Name = "lblHintForUser";
            this.lblHintForUser.Size = new System.Drawing.Size(510, 34);
            this.lblHintForUser.TabIndex = 4;
            // 
            // TypingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 165);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TypingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TypingForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCancelAtTypingForm;
        private System.Windows.Forms.Button btnConfirmAtTypingForm;
        private System.Windows.Forms.TextBox txtLetUserEnterAtTypingForm;
        private System.Windows.Forms.Label lblHintForUser;
    }
}