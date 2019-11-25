namespace SWE_Final_Project.Views.SubForms {
    partial class AlertForm {
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
            this.txtShowAlertMessage = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel_baseAtAlertForm = new System.Windows.Forms.TableLayoutPanel();
            this.btnConfirmAtAlertForm = new System.Windows.Forms.Button();
            this.tableLayoutPanel_baseAtAlertForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtShowAlertMessage
            // 
            this.txtShowAlertMessage.BackColor = System.Drawing.SystemColors.Control;
            this.txtShowAlertMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShowAlertMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtShowAlertMessage.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtShowAlertMessage.Location = new System.Drawing.Point(13, 13);
            this.txtShowAlertMessage.Margin = new System.Windows.Forms.Padding(13);
            this.txtShowAlertMessage.Multiline = true;
            this.txtShowAlertMessage.Name = "txtShowAlertMessage";
            this.txtShowAlertMessage.ReadOnly = true;
            this.txtShowAlertMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShowAlertMessage.Size = new System.Drawing.Size(512, 109);
            this.txtShowAlertMessage.TabIndex = 1;
            // 
            // tableLayoutPanel_baseAtAlertForm
            // 
            this.tableLayoutPanel_baseAtAlertForm.ColumnCount = 1;
            this.tableLayoutPanel_baseAtAlertForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_baseAtAlertForm.Controls.Add(this.txtShowAlertMessage, 0, 0);
            this.tableLayoutPanel_baseAtAlertForm.Controls.Add(this.btnConfirmAtAlertForm, 0, 1);
            this.tableLayoutPanel_baseAtAlertForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_baseAtAlertForm.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_baseAtAlertForm.Name = "tableLayoutPanel_baseAtAlertForm";
            this.tableLayoutPanel_baseAtAlertForm.RowCount = 2;
            this.tableLayoutPanel_baseAtAlertForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_baseAtAlertForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel_baseAtAlertForm.Size = new System.Drawing.Size(538, 205);
            this.tableLayoutPanel_baseAtAlertForm.TabIndex = 1;
            // 
            // btnConfirmAtAlertForm
            // 
            this.btnConfirmAtAlertForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmAtAlertForm.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnConfirmAtAlertForm.Location = new System.Drawing.Point(377, 148);
            this.btnConfirmAtAlertForm.Margin = new System.Windows.Forms.Padding(13);
            this.btnConfirmAtAlertForm.Name = "btnConfirmAtAlertForm";
            this.btnConfirmAtAlertForm.Size = new System.Drawing.Size(148, 44);
            this.btnConfirmAtAlertForm.TabIndex = 0;
            this.btnConfirmAtAlertForm.Text = "OK";
            this.btnConfirmAtAlertForm.UseVisualStyleBackColor = true;
            this.btnConfirmAtAlertForm.Click += new System.EventHandler(this.BtnConfirmAtAlertForm_Click);
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 205);
            this.Controls.Add(this.tableLayoutPanel_baseAtAlertForm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlertForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AlertForm";
            this.tableLayoutPanel_baseAtAlertForm.ResumeLayout(false);
            this.tableLayoutPanel_baseAtAlertForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtShowAlertMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_baseAtAlertForm;
        private System.Windows.Forms.Button btnConfirmAtAlertForm;
    }
}